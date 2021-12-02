using DimBot;
using Newtonsoft.Json;
using System.IO;

DiscordSocketClient bot = new(new DiscordSocketConfig()
{
	GatewayIntents = (GatewayIntents)0b1111110000011, // https://discord.com/developers/docs/topics/gateway#list-of-intents
	MessageCacheSize = 100
});
DimSecret dimSecret = JsonConvert.DeserializeObject<DimSecret>(File.ReadAllText("dimsecret.json"));


Task Log(LogMessage msg)
{
	Console.WriteLine(msg.ToString());
	return Task.CompletedTask;
}

async Task HandleCommandAsync(SocketMessage msgParam)
{
	var msg = msgParam as SocketUserMessage;
	if (msg == null) return;

	// Create a number to track where the prefix ends and the command begins
	int argPos = 0;

	// Determine if the message is a command based on the prefix and make sure no bots trigger commands
	if (!(msg.HasStringPrefix(Missile.DefaultPrefix, ref argPos) ||
		msg.HasMentionPrefix(bot.CurrentUser, ref argPos)) ||
		msg.Author.IsBot)
		return;

	await Missile.s_cmdSvc.ExecuteAsync(
            context: new SocketCommandContext(bot, msg),
			argPos: argPos,
			services: null);
}

async Task MainAsync()
{
	bot.Log += Log;
	Missile.s_cmdSvc.Log += Log;

	await bot.LoginAsync(TokenType.Bot, dimSecret.token);
	await bot.StartAsync();

	bot.MessageReceived += HandleCommandAsync;
	await Missile.s_cmdSvc.AddModulesAsync(assembly: System.Reflection.Assembly.GetEntryAssembly(), services: null);

	await Task.Delay(-1);
}


MainAsync().Wait();