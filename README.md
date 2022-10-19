# ScreenshotX Plugin

Extending Room Girl and Room Studio's built-in screenshot functionalities.

Requires BepInEx IL2CPP.

**Features:**
 - Customizable hotkey (for Maker)
 - Customizable screenshot image size* (for Maker)
 - Customizable image filename prefix (for Maker)
 - Upscaling* (for Maker and Studio*)
 - Toggleable sound effect (for Maker)
 - Toggleable on-screen notification
 - Toggleable Watermark (for Studio and main game*)

**Download:**
 - Get latest version from the Releases page.

**Installation:**
 - Extract to your game folder or put the .dll file in `BepInEx\Plugins` for Room Girl or `Studio\BepInEx\Plugins` for Room Studio.

**Usage:**
 - Configure in Config Manager
 - Press hotkey to take screenshot (Default: F11).

 **Known Issues:**
 - Upscaling or setting image size larger than game windows resolution enlarges the image. It does not render image in higher resolution.
 - Upscaling in Studio removes the watermark regardless of Watermark setting.
 - In main game, reenabling watermark requires game restart.