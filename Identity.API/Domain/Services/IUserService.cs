using System;
using Identity.Domain.Dtos;

namespace Identity.Services;

public interface IUserService
{
  Task<string> Token(TokenUserRequestDto request);
}
