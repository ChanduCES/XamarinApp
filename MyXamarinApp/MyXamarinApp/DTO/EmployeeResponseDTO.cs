using Newtonsoft.Json;

namespace MyXamarinApp.DTO
{
    public class EmployeeResponseDTO
    {
        [JsonProperty("empId")]
        public int EmpId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
