using Newtonsoft.Json;

namespace MyXamarinApp.DTO
{
    public class EmployeeRequestDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
