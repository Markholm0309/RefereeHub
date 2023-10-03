using System.ComponentModel.DataAnnotations;

namespace RefereeHub.Domain.ViewModels;

public class AuthenticateRequest
{
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }
}