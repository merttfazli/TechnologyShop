using Microsoft.AspNetCore.Mvc;

namespace ChatbotApi.Controllers
{
    namespace YourNamespace.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class BotController : ControllerBase
        {
            private readonly HttpClient _httpClient;

            public BotController()
            {
                _httpClient = new HttpClient();
            }

            [HttpPost]
            public async Task<IActionResult> Post([FromBody] YourRequestModel requestModel)
            {
                // Flask uygulamanıza istek yapma
                var response = await _httpClient.PostAsJsonAsync("http://127.0.0.1:5041/predict", new { message = requestModel.Message });

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<YourResponseModel>(content);
                    return Ok(responseObject);
                }
                return BadRequest();
            }
        }

        public class YourRequestModel
        {
            public string Message { get; set; }
        }

        public class YourResponseModel
        {
            public string Answer { get; set; }
        }
    }
}
