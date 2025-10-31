# OpenGL SharpGL Demo

[![.NET](https://img.shields.io/badge/.NET-6.0%2B-blue)](https://dotnet.microsoft.com/download)
[![SharpGL](https://img.shields.io/badge/SharpGL-2.4.3-green)](https://www.nuget.org/packages/SharpGL)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A lightweight **OpenGL demo using SharpGL in WPF (.NET)**. This project demonstrates a rotating 3D cube with interactive camera controls, custom GLSL shaders, and clean separation of rendering logic.

Ideal for learning modern OpenGL in C# with SharpGL.

---

## Features

- **Modern OpenGL Pipeline** (3.3+ core profile via SharpGL)
- **GLSL Shaders** (vertex + fragment)
- **Interactive Camera** – orbit with mouse, zoom with scroll
- **Smooth Rotation Animation**
- **Error-checked OpenGL Calls**
- **Clean, modular C# code**

---

## Video demo
https://github.com/user-attachments/assets/35315e4f-b117-413a-9ee8-7f3063d1cbba

---

## Prerequisites

- [.NET 6.0+ SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ (or VS Code + C# Dev Kit)
- Graphics card with **OpenGL 3.3+** support
- Windows (WPF-based; can be adapted to WinUI/Core)

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/bask0xff/OpenGLSharpGLDemo.git
cd OpenGLSharpGLDemo
```
### 2. Restore NuGet Packages
```bash
dotnet restore
```
### 3. Build & Run
```bash
dotnet run
```
| Or open OpenGLSharpGLDemo.sln in Visual Studio and press F5.

## Controls
| Action         | Input            |
|----------------|------------------|
| Rotate View    | Left Mouse Drag  |
| Zoom           | Mouse Wheel      |
| Reset Camera   | Press **R**      |
## Project Structure
```
OpenGLSharpGLDemo/
├── OpenGLSharpGLDemo.csproj
├── App.xaml / App.xaml.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs           # Input, camera, render loop
├── Renderers/
│   └── CubeRenderer.cs          # VAO, VBO, draw calls
├── Shaders/
│   ├── vertex.glsl
│   └── fragment.glsl
├── Utils/
│   └── GLHelper.cs              # Shader loading, error checks
└── screenshots/                 # Add demo images here
```
## Key Code Snippets
### Vertex Shader (Shaders/vertex.glsl)
```glsl
#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

out vec3 fragColor;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0);
    fragColor = aColor;
}
```
### Render Loop (excerpt from MainWindow.xaml.cs)
```csharp
private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
{
    var gl = openGLControl.OpenGL;

    gl.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

    // Rotate cube
    var model = Matrix4.CreateRotationY(rotationAngle);
    rotationAngle += 0.01f;

    gl.UseProgram(shaderProgram);
    GLHelper.SetMatrix4(gl, "model", model);
    GLHelper.SetMatrix4(gl, "view", camera.GetViewMatrix());
    GLHelper.SetMatrix4(gl, "projection", camera.GetProjectionMatrix(Width, Height));

    cubeRenderer.Draw(gl);
}
```
## Dependencies

| Package       | Purpose               | Install Command                                      |
|---------------|-----------------------|------------------------------------------------------|
| SharpGL       | OpenGL bindings       | `dotnet add package SharpGL --version 2.4.3`         |
| SharpGL.WPF   | WPF OpenGL control    | `dotnet add package SharpGL.WPF --version 2.4.3`     |

## Extending the Project
- Add Textures: Load images with System.Drawing or ImageSharp, upload to GPU.
- Lighting: Add normals + Phong shader.
- Load Models: Use AssimpNet to import .obj.
- Instancing: Render 1000+ cubes efficiently.

## Troubleshooting
| Issue                        | Solution                                                                 |
|------------------------------|--------------------------------------------------------------------------|
| Black screen                 | Update GPU drivers; check OpenGL version via `gl.GetString(GL_VERSION)`  |
| Shader fails to compile      | Check console logs; fix GLSL syntax                                      |
| Mouse input not working      | Ensure `openGLControl` has focus (`Focusable="True"`)                    |
| Build errors                 | Run `dotnet clean && dotnet restore`                                     |

## Contributing
1. Fork the repo
2. Create a branch: git checkout -b feature/cool-shader
3. Commit: git commit -m "Add normal mapping"
4. Push and open a Pull Request

## License
This project is licensed under the MIT License – see LICENSE for details.

## Acknowledgments
- SharpGL (https://github.com/dwmkerr/sharpgl) by Dave Kerr
- LearnOpenGL.com – excellent OpenGL tutorials
- OpenGL community & contributors

Happy rendering!
