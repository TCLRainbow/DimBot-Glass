using System.Diagnostics;

namespace DimBot.cmd
{
    public class Misc : ModuleBase<SocketCommandContext>
    {

        [Command("bot")]
        [Summary("Displays bot info")]
        public async Task Bot()
        {
            Random r = new();

            Process p = Process.GetCurrentProcess();
            float allocated = p.WorkingSet64 / 1024 / 1024f;
            float used = p.PrivateMemorySize64 / 1024 / 1024f;

            GuildEmote[] emotes = Context.Client.GetGuild(Missile.s_myGuildId).Emotes.Where(e => e.Name.StartsWith("sayu")).ToArray();
            GuildEmote emote = emotes[r.Next(emotes.Length)];

            await ReplyAsync(embed: new EmbedBuilder()
            {
                Title = Missile.s_sponsor,
                Author = new EmbedAuthorBuilder()
                {
                    Name = "Click here to let me join your server! [Open Beta]",
                    Url = "https://discord.com/api/oauth2/authorize?client_id=574617418924687419&permissions=8&scope=bot"
                },
                Color = Missile.RandomColor()
            }
                .AddField("Guild count", Context.Client.Guilds.Count, inline: true)
                .AddField("Uptime", Missile.s_uptime.Elapsed, inline: true)
                .AddField(".NET", Environment.Version.ToString(), inline: true)
                .AddField("Discord.NET", DiscordConfig.Version, inline: true)
                .AddField("Codename", "Arknights", inline: true)
                .AddField("Dev blog", "[Instagram](https://www.instagram.com/techdim) | [YouTube](https://www.youtube.com/channel/UCTuGoJ-MoQuSYVgtmJTa3-w)", inline: true)
                .AddField("Source code", "[GitHub](https://github.com/TCLRainbow/DimBot-Helenite)", inline: true)
                .AddField("Discord server", "[6PjhjCD](https://discord.gg/6PjhjCD)", inline: true)
                // .AddField("CPU", cpu.TotalMilliseconds, inline: true)
                .AddField("Process RAM usage / allocated (MiB)", $"{used} / {allocated}", inline: true)
                .WithFooter(text: string.Concat("Mood: ", emote.Name.AsSpan(4)))
                .WithImageUrl(emote.Url)
                .Build());
        }

        [Command("ping"), Alias("noel")]
        [Summary("Listens to my heartbeat (gateway latency & total message reaction latency")]
        public async Task Ping()
        {
            IUserMessage msg = await ReplyAsync($"Average: {Context.Client.Latency}ms");
            Stopwatch sw = Stopwatch.StartNew();
            await msg.AddReactionAsync(new Emoji("📡"));
            long elapsed = sw.ElapsedMilliseconds;
            sw.Stop();
            string old = msg.Content;
            await msg.ModifyAsync(m => m.Content = $"{old} Immediate response: {elapsed}ms"); 
        }

        [Command("sponsor")]
        [Summary("$.$")]
        public async Task Sponsor() => await ReplyAsync(Missile.s_sponsor);

        [Command("msglink")]
        [Summary("Gives the full URL to a message")]
        public async Task Msglink(IMessage msg) => await ReplyAsync(msg.GetJumpUrl());

    }
}
