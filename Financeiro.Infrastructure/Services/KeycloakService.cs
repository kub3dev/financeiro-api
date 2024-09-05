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

  public async Task<TokenUserResponseDto?> Token(TokenUserRequestDto request)
  {
    IEnumerable<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
    data.Append(new KeyValuePair<string, string>("grant_type", "password"));
    data.Append(new KeyValuePair<string, string>("username", request.Email));
    data.Append(new KeyValuePair<string, string>("password", request.Password));
    data.Append(new KeyValuePair<string, string>("client_id", _resource));
    data.Append(new KeyValuePair<string, string>("client_secret", _secret));
    data.Append(new KeyValuePair<string, string>("scope", "roles"));

    using (var content = new FormUrlEncodedContent(data))
    {
      content.Headers.Clear();
      content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

      using HttpResponseMessage response = await _httpClient.PutAsync(string.Format("realms/{0}/protocol/openid-connect/token", _realm), content);

      if (!response.IsSuccessStatusCode) return null;

      return await response.Content.ReadFromJsonAsync<TokenUserResponseDto>();
    }
  }
}