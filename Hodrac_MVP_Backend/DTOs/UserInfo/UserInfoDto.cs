using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hodrac_MVP_Backend.DTOs.UserInfo
{
    public class UserInfoDto
    {
        [EmailAddress]
        [JsonPropertyName("userEmail")]
        public string UserEmail { get; set; }
    }
}
