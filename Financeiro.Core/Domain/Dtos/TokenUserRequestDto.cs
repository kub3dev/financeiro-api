namespace Financeiro.Core.Domain.Dtos;

public record class TokenUserRequestDto
{
  public string Email { get; init; } = string.Empty;
  public string Password { get; init; } = string.Empty;
}
