using System.Text.Json.Serialization;

namespace E_Tracker.Application.DTOs.Facebook;

public class FaceBookTokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
}