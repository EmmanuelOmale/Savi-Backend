using System.Text.Json;

namespace Savi.Data.DTO
{
    public class PayStackResponseDto
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public JsonElement? Data { get; set; }
    }
}
