using System.Diagnostics;

namespace DimBot
{
    internal static class Missile
    {
        internal const string s_sponsor = "世界の未来はあなたの手にある <https://streamlabs.com/pythonic_rainbow/tip> <https://www.patreon.com/ChingDim>";
        internal static Stopwatch s_uptime = Stopwatch.StartNew();
        internal const ulong s_myGuildId = 285366651312930817;
        internal static CommandService s_cmdSvc = new();

#if DEBUG
        internal const string DefaultPrefix = "t.";
#else
        internal const string DefaultPrefix = "d.";

#endif

        internal static Color RandomColor()
        { 
            Random r = new();
            return new Color(r.Next(256), r.Next(256), r.Next(256));
        }

        internal static CommandInfo GetCommand(string name) => s_cmdSvc.Commands.First(c => c.Name == name);
    }
}
