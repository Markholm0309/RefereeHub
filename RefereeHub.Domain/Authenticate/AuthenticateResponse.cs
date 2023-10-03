using System.ComponentModel.DataAnnotations;

namespace RefereeHub.Domain.ViewModels;

public class AuthenticateResponse
{
    public AuthenticateResponse(string username, string token)
    {
        Username = username;
        Token = token;
    }
    
    [Required] public string Username { get; set; }
    [Required] public string Token { get; set; }
}