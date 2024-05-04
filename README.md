# What is this?

Keeps an eye on a folder, and when a **supported image** is modified, it updates (or creates) a VTF with the same name

- Supported image formats: PNG, BMP, TGA, JPG, JPEG, PSD

<br />


### Select a `materials` folder and press "Start"

![image](https://github.com/NvC-DmN-CH/AutoVTF/assets/56874047/0bb94b08-fbec-4dde-8e87-cfd8e6bd28f8)


---
### Quick editing thanks to Hammer++'s hotloading ability:
<img src="https://cdn.discordapp.com/attachments/1131362438227431428/1231853010662195270/update_new.gif?ex=6637cec4&is=66367d44&hm=0a1692a8bd443aa47af6ae257739cd6db70cb5f4414cf1724868ccf1f7dcd03f&" width="850"/>

(it works this way when any supported image file is updated, this is just an example of   photoshop updating a .psd file)

---

### Drag-n-Dropping allows for a bunch of manual conversions:

![gif](https://github.com/NvC-DmN-CH/AutoVTF/assets/56874047/6edd8f1d-fb10-42ff-ba77-b2c9fc793d0e)

- Dragging a VTF:
  - Allows exporting to PNG, TGA or PSD
  - `Make Simple VMT` with LightmappedGeneric shader and $basetexture pointing to VTF, relative to `materials/`


- Dragging an image:
  - `Lossless` Makes VTF with BGRA8888 compression if image is transparent, or RGB888 if opaque
  - `Compressed` Makes VTF with DXT5 compression if image is transparent, or DXT1 if opaque
  - These presets are set to version `7.1` and mipmap generation is enabled with `HANNING` filtering

---

### The `Advanced` option shows a panel similar to VTFEdit:
![gif](https://github.com/NvC-DmN-CH/AutoVTF/assets/56874047/a75e51e1-1ee2-48db-93ec-2617cd65c6df)



I think that only these 6 flags have an actual impact. If I missed an useful flag let me know.

Same with the image formats that this program accepts:
  - BGRA8888
  - RGB888
  - IA88
  - I8
  - DXT5
  - DXT1
  - RGB565 (todo: is this format supported in any branch of the source engine?)

<br />

## Details
- Updates VTF files while preserving the settings (flags, image format, version).
    - If the VTF has no mipmaps, the updated VTF will also not have mipmaps. If it has, the updated VTF will have mipmaps with the `HANNING` filtering.
    - The image format of the updated VTF can alternate between having alpha or not having alpha, depending on whether the input image has any transparent pixels or it's fully opaque. For example, a VTF with `I8` image format will become `IA88` if the updated content has any transparency, and will go back to `I8` if it's updated with a fully opaque content. And the same for the other format pairs (`DXT5/DXT1`, `BGRA8888/RGB888`, `IA88/I8`) and the special case pair: (`BGRA8888/RGB565`) because there is no equivalent of `RGB565` with alpha. If the program tries to update a VTF which has any other image format, the updated VTF will default to having an image format of `BGRA8888` (lossless with alpha).

- Images can be any size, they are automatically resized to the nearest power of two



<br />

## Todo
+ Can't make animated textures or envmaps yet
+ I couldn't find a way to support Gimp's native XCF file format
+ Currently the program can't be notified if VTFCmd fails for some reason, so it just silently fails

<br />

## External dependencies information
Requires .NET 8.0

VTFCmd is compiled from >>> [here](https://github.com/Sky-rym/VTFEdit-Reloaded)

How to compile VTFCmd: (or at least how I managed to compile it)
- Open `sln\vs2019\VTFLib.sln` solution with Visual Studio 2022
- Accept updating the projects
- Set Startup project to VTFCmd
- Inside `VTFCmd\VTFCmd.rc` replace `#include "afxres.h"` with `#include<windows.h>`
- Build for Release 64bit
- Output will be in `sln\vs2019\VTFCmd\x64\Release\`
  - in the AutoVTF build folder, make a new folder called `vtfcmd`. Copy VTFCmd.exe and VTFLib.dll there

- VTFCmd requires DevIL, which can be found >>> [here](https://sourceforge.net/projects/openil/files/DevIL%20Win32%20and%20Win64/DevIL-EndUser-x64-1.8.0.zip/download?use_mirror=phoenixnap)
  - Put DevIL.dll, ILU.dll, ILUT.dll in the `vtfcmd` folder

<br />

NuGet packages:
- [Magick.NET](https://github.com/dlemstra/Magick.NET)
- [DarkUI](https://github.com/RobinPerris/DarkUI)

<br />

## Misc
I hope that this will be useful to people, because it has already made my workflow so much simpler than before.

This isn't meant to be a replacement for VTFEdit though, it's still very useful for previewing VTFs and setting some arcane settings.

If you found a bug or have a suggestion you can create an [issue](https://github.com/NvC-DmN-CH/AutoVTF/issues)

<br />

## Program is often falsely detected as virus
And I really don't know why, I promise that it's not!

If you want to be sure that there is nothing malicious, take a look at the source code and compile it, and download/compile the external dependencies yourself.

If you have any idea how to fix this, please let me know
