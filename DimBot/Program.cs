using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DimBot
{
    class Program
    {
        public static void Main() => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            DiscordSocketClient client = new();
            client.Log += Log;
            Secrets secret = JsonConvert.DeserializeObject<Secrets>(File.ReadAllText("config.json"));

            await client.LoginAsync(TokenType.Bot, secret.token);
            await client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
