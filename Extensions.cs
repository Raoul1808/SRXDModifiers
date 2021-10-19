using HarmonyLib;

namespace SRXDModifiers
{
    public static class Extensions
    {
        public static void PatchAll<T>(this Harmony harmony) => harmony.PatchAll(typeof(T));
    }
}
