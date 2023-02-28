using PlatformService.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpclient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpclient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            try
            {
                var httpContent = new StringContent(JsonSerializer.Serialize(plat), Encoding.UTF8, "application/json");

                var response = await _httpclient.PostAsync($"{_configuration["CommandService"]}", httpContent);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine("Sync POST to CommandService was OK!");
                else
                    Console.WriteLine("Sync POST to CommandService was NOT OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
