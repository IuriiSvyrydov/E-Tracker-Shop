using System.Text.Json.Serialization;

namespace E_Tracker.Application.Features.Commands.AppUser.FaceBookLogin;

public class FaceBookAccessTokenValidation
{
    [JsonPropertyName("data")]
    public FaceBookAccessTokenValidationData Data { get; set; }
  
}

public class FaceBookAccessTokenValidationData
{
    [JsonPropertyName("is_valid")]
    public bool IsValid { get; set; }
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
}