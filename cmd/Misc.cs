using System.Diagnostics;
using System.Linq;
using System;

namespace DimBot.cmd
{
    public class Misc : ModuleBase<SocketCommandContext>
    {

        [Command("bot")]
        public async Task bot()
        {
            Random r = new();

            Process p = Process.GetCurrentProcess();
            float allocated = p.WorkingSet64 / 1024 / 1024f;
            float used = p.PrivateMemorySize64 / 1024 / 1024f;

            GuildEmote[] emotes = Context.Client.GetGuild(Missile.MyGuildId).Emotes.Where(e => e.Name.StartsWith("sayu")).ToArray();
            GuildEmote emote = emotes[r.Next(emotes.Length)];

            await ReplyAsync(embed: new EmbedBuilder()
            {
                Title = Missile.Sponsor,
                Author = new EmbedAuthorBuilder()
                {
                    Name = "Click here to let me join your server! [Open Beta]",
                    Url = "https://discord.com/api/oauth2/authorize?client_id=574617418924687419&permissions=8&scope=bot"
                },
                Color = Missile.RamdomColor()
            }
                .AddField("Guild count", Context.Client.Guilds.Count, inline: true)
                .AddField("Uptime", Missile.Uptime.Elapsed, inline: true)
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
        public async Task ping()
        {
            IUserMessage msg = await ReplyAsync($"Average: {Context.Client.Latency}ms");
            Stopwatch sw = Stopwatch.StartNew();
            await msg.AddReactionAsync(new Emoji("📡"));
            long elapsed = sw.ElapsedMilliseconds;
            sw.Stop();
            string old = msg.Content;
            await msg.ModifyAsync(m => m.Content = $"{old} Immediate response: {elapsed}ms"); 
        }
    }
}
