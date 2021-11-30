using Newtonsoft.Json;
using System.IO;

DiscordSocketClient bot = new();
DimSecret dimSecret = JsonConvert.DeserializeObject<DimSecret>(File.ReadAllText("dimsecret.json"));
CommandService cmdSvc = new();

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
	if (!(msg.HasStringPrefix("t.", ref argPos) ||
		msg.HasMentionPrefix(bot.CurrentUser, ref argPos)) ||
		msg.Author.IsBot)
		return;

	await cmdSvc.ExecuteAsync(
			context: new SocketCommandContext(bot, msg),
			argPos: argPos,
			services: null);
}

async Task MainAsync()
{
	bot.Log += Log;

	await bot.LoginAsync(TokenType.Bot, dimSecret.token);
	await bot.StartAsync();

	bot.MessageReceived += HandleCommandAsync;
	await cmdSvc.AddModulesAsync(assembly: System.Reflection.Assembly.GetEntryAssembly(), services: null);

	await Task.Delay(-1);
}


MainAsync().Wait();