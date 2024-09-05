using System;
using Financeiro.Core.Domain.Dtos;

namespace Financeiro.Core.Services;

public interface IUserService
{
  Task<TokenUserResponseDto?> Token(TokenUserRequestDto request);
}
