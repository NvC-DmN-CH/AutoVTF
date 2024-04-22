# What is this?

Keeps an eye on a folder, and when a **supported image**[^1] is modified, it updates (or creates) a VTF with the same name

[^1]:Supported image formats: PNG, BMP, TGA, JPG, JPEG, PSD


### Select a `materials` folder and press "Start"

![image](https://github.com/NvC-DmN-CH/AutoVTF/assets/56874047/0bb94b08-fbec-4dde-8e87-cfd8e6bd28f8)


---
### Quick editing thanks to Hammer++'s hotloading ability:
<img src="https://cdn.discordapp.com/attachments/1131362438227431428/1231853010662195270/update_new.gif?ex=66275404&is=66260284&hm=95074a4cfe90033fd262d627c2f443daea4978ebbcf4d3e0f028c315aa094e5f&" width="850"/>

Hammer++ displays changes when its window is clicked

---

### Drag-n-Dropping allows for a bunch of manual conversions:

![gif](https://github.com/NvC-DmN-CH/AutoVTF/assets/56874047/6edd8f1d-fb10-42ff-ba77-b2c9fc793d0e)




---

### The "Advanced" option shows a panel similar to VTFEdit:
![gif](https://github.com/NvC-DmN-CH/AutoVTF/assets/56874047/a75e51e1-1ee2-48db-93ec-2617cd65c6df)



I think that only these 6 flags have an actual impact. If I missed an useful flag let me know.



<br />

## Details
- Supports PSD files!
- Updates VTF files while preserving settings such as flags, image format, version, and takes into account presence of mipmaps.
- Images can be any size, they are automatically resized to nearest power of two
- Can create a basic VMT by dropping a VTF into the "Make Simple VMT" option ($basetexture points to VTF path relative to `materials/`)



<br />

## Todo
+ Can't make animated textures or envmaps yet
+ I couldn't find a way to support Gimp's native XCF file format
+ If VTFCmd fails for some reason currently it can't notify the program, so it silently fails.

<br />

## External dependencies information
Requires .NET 8.0

DevIL can be found >>> [here](https://sourceforge.net/projects/openil/files/DevIL%20Win32%20and%20Win64/DevIL-EndUser-x64-1.8.0.zip/download?use_mirror=phoenixnap)

VTFCmd is compiled from >>> [here](https://github.com/Sky-rym/VTFEdit-Reloaded)

How to compile VTFCmd: (or at least how I managed to compile it)
- Open `sln\vs2019\VTFLib.sln` solution with Visual Studio 2022
- Accept updating the projects
- Set Startup project to VTFCmd
- Inside `VTFCmd\VTFCmd.rc` replace `#include "afxres.h"` with `#include<windows.h>`
- Build for Release 64bit
- Output will be in `sln\vs2019\VTFCmd\x64\Release\`

NuGet packages:
- [Magick.NET](https://github.com/dlemstra/Magick.NET)
- [DarkUI](https://github.com/RobinPerris/DarkUI)

<br />

## Misc
I hope that this will be useful for people, because it has already made my workflow so much simpler than before.

This isn't meant to be a replacement for VTFEdit though, it's still very useful for previewing VTFs and setting some arcane settings.

If you found a bug or have a suggestion you can create an [issue](https://github.com/NvC-DmN-CH/AutoVTF/issues)

<br />

## Program is often falsely detected as virus
And I really don't know why, I promise that it's not!

If you want to be sure that there is nothing malicious, take a look at the source code and compile it (x86), and download/compile the external dependencies yourself.

If you have any idea how to fix this, please let me know
