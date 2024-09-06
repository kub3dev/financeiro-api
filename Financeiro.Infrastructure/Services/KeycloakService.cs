using System;
using System.Net.Http.Json;
using Financeiro.Core.Domain.Dtos;
using Financeiro.Core.Services;
using Financeiro.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace Financeiro.Infrastructure.Services;

public class KeycloakService : IUserService
{
  private readonly IConfiguration _configuration;
  private static readonly HttpClient _httpClient = new HttpClient();
  private readonly string _realm = string.Empty;
  private readonly string _authServerUrl = string.Empty;
  private readonly string _resource = string.Empty;
  private readonly string _secret = string.Empty;

  public KeycloakService(IConfiguration configuration)
  {
    _configuration = configuration;
    _realm = _configuration["Keycloak:realm"] ?? string.Empty;
    _authServerUrl = _configuration["Keycloak:auth-server-url"] ?? string.Empty;
    _resource = _configuration["Keycloak:resource"] ?? string.Empty;
    _secret = _configuration["Keycloak:credentials:secret"] ?? string.Empty;

    _httpClient.BaseAddress = new Uri(_authServerUrl);
    // _httpClient.DefaultRequestHeaders.Accept.Clear();
    // _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
  }

  public async Task<string> Token(TokenUserRequestDto request)
  {
    var payload = new Dictionary<string, string>
    {
      { "grant_type", "password" },
      { "password", request.Password },
      { "username", request.Email },
      { "client_id", _resource },
      { "client_secret", _secret },
      { "scope", "roles" }
    };

    var content = new FormUrlEncodedContent(payload);

    var response = await _httpClient.PostAsync(string.Format("realms/{0}/protocol/openid-connect/token", _realm), content);

    if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());

    return (await response.Content.ReadAsStringAsync()).ToString();
  }
}