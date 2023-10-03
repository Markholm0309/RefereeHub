using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.ViewModels;

namespace RefereeHub.Domain.Interfaces.Services;

public interface IAccountService
{
    Task<ObjectResult> Register(AuthenticateRequest request);
    Task<ObjectResult> Authenticate(AuthenticateRequest request);
}