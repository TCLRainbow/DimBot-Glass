using System.Diagnostics;

namespace DimBot
{
    internal static class Missile
    {
        internal const string Sponsor = "世界の未来はあなたの手にある https://streamlabs.com/pythonic_rainbow/tip https://www.patreon.com/ChingDim";
        internal static Stopwatch Uptime = Stopwatch.StartNew();
        internal const ulong MyGuildId = 285366651312930817;

        internal static Color RamdomColor()
        {
            Random r = new();
            return new Color(r.Next(256), r.Next(256), r.Next(256));
        }
    }
}
