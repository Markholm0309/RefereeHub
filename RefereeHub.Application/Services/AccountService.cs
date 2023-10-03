using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RefereeHub.Domain.Helpers;
using RefereeHub.Domain.Interfaces.Services;
using RefereeHub.Domain.ViewModels;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace RefereeHub.Application.Services;

public class AccountService : IAccountService
{
    private readonly SymmetricSecurityKey _key;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountService(IConfiguration config, UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"] ?? string.Empty));
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ObjectResult> Register(AuthenticateRequest request)
    {
        if (await UserExists(request.Username)) return new BadRequestObjectResult("Username is taken");

        var user = request.Adapt<IdentityUser>();
        user.UserName = request.Username.ToLower();
        var result = await _userManager.CreateAsync(user, request.Password);

        switch (result.Succeeded)
        {
            case false:
                return new BadRequestObjectResult(result.Errors);
            case true:
                await _userManager.AddToRoleAsync(user, Roles.User);
                break;
        }

        return new OkObjectResult(new AuthenticateResponse(user.UserName, await CreateTokenAsync(user)));
    }

    public async Task<ObjectResult> Authenticate(AuthenticateRequest request)
    {
        var user = await _userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == request.Username.ToLower());

        if (user == null) return new UnauthorizedObjectResult("Invalid username");
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded) return new UnauthorizedObjectResult("Invalid password");

        if (user.UserName != null)
            return new OkObjectResult(new AuthenticateResponse(user.UserName, await CreateTokenAsync(user)));

        return new BadRequestObjectResult("Error message");
    }

    private async Task<string> CreateTokenAsync(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}