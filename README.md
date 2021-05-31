# LivBeatSaberSynchronizer
An incredibly basic LIV camera plugin that synchronizes successful Beat Saber note cuts with emission and rim light color changes on VRM avatars' materials using the VRM/MToon shader.

## Example
https://user-images.githubusercontent.com/85083151/120126550-40ae3500-c182-11eb-8b5c-1d43c8ba2873.mp4

## Installation

### Dependencies
- Windows 10
- Beat Saber with HTTP Status mod
- LIV

### Guide
1. Copy the [DLL camera plugin](https://github.com/KemonoIO/LivBeatSaberSynchronizer/releases) to %UserProfile%\Documents\LIV\Plugins\CameraBehaviours\LivBeatSaberSynchronizer.dll.
2. Select a camera in LIV.
3. Activate the LivBeatSaberSynchronizer plugin.

### Configuration
After the first activation, a JSON configuration file will be created at %UserProfile%\Documents\LIV\Plugins\CameraBehaviours\LivBeatSaberSynchronizer.json.

#### colorLeft
Color to emit on a successful left hand cut, an array of four decimal values: red, green, blue, and strength.

Defaults to [1.0, 0.0, 0.0, 5.0].

#### colorNeutral
Color to emit in menu, an array of four decimal values: red, green, blue, and strength.

Defaults to [0.0, 0.0, 0.0, 1.0].

#### colorRight
Color to emit on a successful right hand cut, an array of four decimal values: red, green, blue, and strength.

Defaults to [0.0, 0.1, 0.4, 5.0].

#### cameraFov
Camera field of view, a decimal value.

Defaults to 60.0.

#### cameraPosition
Camera position, an array of three decimal values: x, y, and z.

Defaults to [1.0, 1.4, -3.5].

#### cameraRotation
Camera rotation, an array of four decimal values: x, y, z, and w.

Defaults to [0.0, -0.2, 0.0, 1.0].

## FAQ

### How does it work?
1. On plugin activation, a list of all VRM/MToon materials with a non-black (0, 0, 0) _EmissionColor property is stored.
2. Events from [Beat Saber Http Status](https://github.com/opl-/beatsaber-http-status) are listened for on ws://localhost:6557/socket.
3. Note cut and menu events are translated into a color provided by JSON configuration.
4. Each of the stored materials' _EmissionColor and _RimColor properties are set to the color.

### Why not use a shared memory space?
I do not want to maintain a Beat Saber mod and the web socket has high enough precision.

### Why is your code awful?
My day job is not C# and I barely know what I am doing.

### Why does this exist?
Hopefully, for someone else to substantially improve upon or borrow.

### License?
[Unlicense](https://unlicense.org/). Do whatever you want. Credit "KemonoIO" if you feel like it.

## Known Issues
If the avatar is changed, the plugin will have to be deactivated and reactivated to refresh the material list.
