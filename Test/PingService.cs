
namespace Test;

public class PingService : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await PingToUrl();
            if (response.IsSuccessStatusCode)
            {
                SendToUseFromTelegramBot("Server is working");
            }
            else
            {
                SendToUseFromTelegramBot($"Server is not working: {response.StatusCode}");
            }
            await Task.Delay(1000, stoppingToken);
        }
    }

    public async Task<HttpResponseMessage> PingToUrl()
    {
        var client = new HttpClient();
        return await client.GetAsync("https://savdo.uzavtosanoat.uz/t/ap/stream/ph&models");
    }

    public async void SendToUseFromTelegramBot(string message)
    {
        var client = new HttpClient();
        string token = "6566749542:AAGbDcOEnTywiTjdoAJ3_yp41_dHtxw7Dz4";
        long userId = 1614764463;
        await client.GetAsync($"https://api.telegram.org/bot{token}/sendMessage?chat_id={userId}&text={message}");
    }
}