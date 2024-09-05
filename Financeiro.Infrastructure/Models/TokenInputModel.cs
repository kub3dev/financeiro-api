using System;

namespace Financeiro.Infrastructure.Models;

public class TokenInputModel
{
  public string GrantType { get; set; } = string.Empty;
  public string ClientId { get; set; } = string.Empty;
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string ClientSecret { get; set; } = string.Empty;
  public string Scope { get; set; } = string.Empty;
}