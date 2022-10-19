using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using BepInEx.IL2CPP;
#if RG
using UnityEngine;
using Illusion.Unity.Component;
#elif RoomStudio
using Studio;
#endif

namespace ScreenshotX
{
    [BepInPlugin(GUID, PluginName, Version)]
    public partial class ScreenshotX : BasePlugin
    {
        public const string Version = "1.0.0";

        internal static new ManualLogSource Log;

#if RG
        private static Texture gssmark;
        private static Texture ssmark;

        private static ConfigEntry<bool> EnableScreenshot { get; set; }
        private static ConfigEntry<KeyCode> ScreenshotHotkey { get; set; }
        private static ConfigEntry<bool> Alpha { get; set; }
        private static ConfigEntry<int> Width { get; set; }
        private static ConfigEntry<int> Height { get; set; }
        private static ConfigEntry<bool> ScreenshotSound { get; set; }
        private static ConfigEntry<string> Prefix { get; set; }
#endif
        private static ConfigEntry<int> Upscale { get; set; }
        private static ConfigEntry<bool> WaterMark { get; set; }
        private static ConfigEntry<bool> ScreenshotMessage { get; set; }

        private static GameScreenShot gameScreenshot = new GameScreenShot();

        public override void Load()
        {
            Log = base.Log;
#if RG
            EnableScreenshot = Config.Bind("Character Creator", "Enable", true, "Enable screen capture in Character Creator");
            ScreenshotHotkey = Config.Bind("Character Creator", "Hotkey", KeyCode.F11, "Hotkey for screen capture");
            //Alpha = Config.Bind("Character Creator", "Transparent Background", false, "Make background transparent when enabled");
            Width = Config.Bind("Character Creator", "Width", Screen.width, "Screenshot image width");
            Height = Config.Bind("Character Creator", "Height", Screen.height, "Screenshot image height");
#endif
            Upscale = Config.Bind((ScreenshotX.PluginName == "RG ScreenshotX") ? "Character Creator" : "General", "Upscale", 1, new ConfigDescription("Multiply image resolution. Upscaling removes watermark regardless of Watermark setting.", new AcceptableValueRange<int>(1, 10)));
            WaterMark = Config.Bind((ScreenshotX.PluginName == "RG ScreenshotX") ? "Main Game" : "General", "Watermark", false, "Include watermark on screenshot.");
            ScreenshotMessage = Config.Bind((ScreenshotX.PluginName == "RG ScreenshotX") ? "Character Creator" : "General", "Show messages on screen", true, "Whether screenshot messages will be displayed on screen. Messages will still be written to the log.");
#if RG
            ScreenshotSound = Config.Bind("Character Creator", "Play camera sound effect", true, "Whether camera sound plays when screenshot is taken.");
            Prefix = Config.Bind("Character Creator", "Filename Prefix", "RG_", new ConfigDescription("String to append in front of screenshot filename.", null, "Advanced"));
#endif

            Harmony.CreateAndPatchAll(typeof(Hooks), GUID);
        }
        private static void LogScreenshotMessage(string text)
        {
            if (ScreenshotMessage.Value)
                Log.LogMessage(text);
            else
                Log.LogInfo(text);
        }
    }
}