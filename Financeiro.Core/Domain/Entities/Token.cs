using System;
using Newtonsoft.Json;

namespace Financeiro.Core.Domain.Entities;

public class Token
{
  [JsonProperty("access_token")]
  public string AccessToken { get; set; } = string.Empty;
  [JsonProperty("expires_in")]
  public int ExpiresIn { get; set; } = 0;
  [JsonProperty("refresh_expires_in")]
  public int RefreshExpiresIn { get; set; } = 0;
  [JsonProperty("refresh_token")]
  public string RefreshToken { get; set; } = string.Empty;
  [JsonProperty("token_type")]
  public string TokenType { get; set; } = string.Empty;
  [JsonProperty("not-before-policy")]
  public string NotBeforePolicy { get; set; } = string.Empty;
  [JsonProperty("session_state")]
  public string SessionState { get; set; } = string.Empty;
  [JsonProperty("scope")]
  public string Scope { get; set; } = string.Empty;
}

