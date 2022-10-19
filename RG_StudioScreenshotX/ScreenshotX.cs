using BepInEx;
using HarmonyLib;
using BepInEx.IL2CPP;
using Studio;

namespace ScreenshotX
{
    [BepInProcess("RoomStudio.exe")]
    public partial class ScreenshotX : BasePlugin
    {
        public const string PluginName = "Room Studio ScreenshotX";
        public const string GUID = "kky.RG.Studio.ScreenshotX";

        private static class Hooks
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(GameScreenShot), nameof(GameScreenShot.CaptureFunc))]
            private static void ClearWatermark(GameScreenShot __instance)
            {
                __instance._capMark = WaterMark.Value;
                __instance._capRate = Upscale.Value;
                string _path = __instance.savePath;
                LogScreenshotMessage("Writing screenshot to " + _path.Substring(UnityEngine.Application.dataPath.Length + 7));
            }
        }
    }
}
