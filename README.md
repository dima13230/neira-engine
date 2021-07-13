# **KailashEngine** #

### **Youtube Demo** ###

[![Alt text](https://img.youtube.com/vi/kcQ22UdTSbQ/0.jpg)](https://www.youtube.com/watch?v=kcQ22UdTSbQ)


### **Features** ###

* Collada importing from Blender via custom python export script (triggers Blender's collada loader + some addons for lights and physics) supporting:
  * Mesh, lights, rigid body physics
  * Mesh movement and skeletal animation
  * Light movement animation
  * Diffuse, normal, specular, parallax, displacement mapping. Textures are imported as bindless GL textures
* Bullet Physics integration for rigid bodies (BulletSharp)
* Deferred Rendering pipeline
* Spot, point and directional soft shadows with variable penumbra (VSM + mipmaps + pcss style stuff)
* HDR with adaptive exposure
* Lens effects like bloom and lens flare
* Depth of Field with Bokeh
* Camera and object motion blur
* Precomputed Atmospheric Scattering
* Voxel based global illumination
* ..and maybe some other things, I don't know :)

# **Controls:** #

### **Keyboard:** ###

* WASD / CTRL / Space = Movement
* F = Flashlight
* Z = Reel in picked object
* X = Throw picked object
* R = Reset Scene

#### **Toggles** ####
* Tab = No Clip
* CAPS = Mouse Lock

### **Mouse** ###

* Left Click = Pick / move physical object / Move camera when mouse unlocked
* Right Click = Zoom camera / Poke the Goat!
* MIddle Click = Take Screenshot

# **Exporting for Kaillash in Blender** #

### **Notes** ###
Blender meshes must have the following to work with Kailash:
* UV Mapping / Texture Coordinates
* At least 1 material

If you are starting from a fresh blend, make sure to set all paths to relative: 
File > External Data > Make all paths relative

### **Exporting** ###
1. Create your scene
1. Save the blend in KailashEngine2/KailashEngine/Resources/Blender/ (important for relative paths)
1. Open up a Text Editor panel 
1. Open the file KailashEngine2/KailashEngine/Resources/Blender/Scripts/export_collada.py
1. Hover the mouse over the Text Editor panel and hit Alt + P (this will run the script)

The script will create 3 files in KailashEngine2/KailashEngine/Resources/Scene/<blend_file_name>/

### Workarounds ####
Blender's Collada exporter misses a lot of things. Below are some workarounds I've used to get things to export

**Textures**

Set the following texture influences for effect in Kailash

* Parallax Mapping: Diffuse > Intensity
* Displacement Mapping: Shading > Ambient

# **Running Scene in Kailash** #
1. Put the blend's filename (without extension) in KailashEngine.Client.Scene Method: load
1. Recompile and Run