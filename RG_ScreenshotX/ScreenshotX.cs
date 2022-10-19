using System;
using System.Text;
//using System.Linq;
using BepInEx;
using HarmonyLib;
using BepInEx.IL2CPP;
using UnityEngine;
using Manager;
//using RG.Config;
//using UnityEngine.SceneManagement;
using CharaCustom;
using RG;
using Illusion.Unity;
using Illusion.Unity.Component;
using Illusion.IO;

namespace ScreenshotX
{
    [BepInProcess("RoomGirl.exe")]
    public partial class ScreenshotX : BasePlugin
    {
        public const string PluginName = "RG ScreenshotX";
        public const string GUID = "kky.RG.ScreenshotX";

        private static class Hooks
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(CustomControl), nameof(CustomControl.Update))]
            private static void AddScreenshotKey(CustomControl __instance)
            {
                bool busy = __instance.customBase.IsInputFocused();
                if (!busy)
                {
                    busy = Manager.Scene.IsFadeNow;
                }
                /*if (!busy)
                {
                    busy = Manager.Scene.Overlaps.Any((Manager.Scene.IOverlap o) => o is ExitDialog || o is ConfirmDialog);
                }
                if (!busy)
                {
                    busy = Manager.Scene.Overlaps.Any((Manager.Scene.IOverlap o) => o is ConfigWindow) || ConfigWindow.IsActive;
                }
                if (!busy)
                {
                    busy = Manager.Scene.Overlaps.Any((Manager.Scene.IOverlap o) => o is ShortcutViewDialog) || ShortcutViewDialog.IsActive;
                }*/
                if (!busy)
                {
                    if (Input.GetKeyDown(ScreenshotHotkey.Value))
                    {
                        if (EnableScreenshot.Value)
                        {
                            //gameScreenshot.capRate = Upscale.Value;
                            if (ScreenshotSound.Value) Illusion.Game.Utils.Sound.PlaySystemSE(SystemSE.Capture);
                            string _path = CreateCaptureFileName();
                            //gameScreenshot.Capture(_path);
                            ScreenShotEx.Capture(_path, false, Width.Value, Height.Value, Upscale.Value);
                            LogScreenshotMessage("Writing screenshot to " + _path.Substring(Application.dataPath.Length + 4));
                            return;
                        }
                    }
                }
            }

            [HarmonyPrefix, HarmonyPatch(typeof(GameScreenShot), nameof(GameScreenShot.CaptureFunc))]
            private static void ClearGSSWatermark(GameScreenShot __instance)
            {
                gssmark = __instance.texCapMark;
                if (!WaterMark.Value)
                {
                    Log.LogInfo("ClearGSSWatermark ran");
                    Log.LogInfo($"texCapMark before clearing: {__instance.texCapMark}");
                    __instance.texCapMark = null;
                    Log.LogInfo($"texCapMark cleared: {__instance.texCapMark}");
                }
            }

            /*[HarmonyPostfix, HarmonyPatch(typeof(Illusion.Unity.Component.GameScreenShot), nameof(Illusion.Unity.Component.GameScreenShot.Capture))]
            private static void AddBackGSSWatermark(Illusion.Unity.Component.GameScreenShot __instance)
            {
                Log.LogInfo("AddBackGSSWatermark ran");
                Log.LogInfo($"texCapMark before adding: {__instance.texCapMark}");
                __instance.texCapMark = gssmark;
                Log.LogInfo($"texCapMark added: {__instance.texCapMark}");
            }*/
        }

        public static string CreateCaptureFileName()
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            stringBuilder.Append(UserData.Create("cap"));
            stringBuilder.Append(Prefix.Value);
            DateTime now = DateTime.Now;
            stringBuilder.Append(now.Year.ToString("0000"));
            stringBuilder.Append(now.Month.ToString("00"));
            stringBuilder.Append(now.Day.ToString("00"));
            stringBuilder.Append(now.Hour.ToString("00"));
            stringBuilder.Append(now.Minute.ToString("00"));
            stringBuilder.Append(now.Second.ToString("00"));
            stringBuilder.Append(now.Millisecond.ToString("000"));
            stringBuilder.Append(".png");
            return stringBuilder.ToString();
        }
    }
}
