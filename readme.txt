Microsoft Windows [Version 10.0.19045.5965]
(c) Microsoft Corporation. All rights reserved.

d:\Projects\2025>dotnet new winforms -n OpenGLSharpGLDemo -f net7.0
The template "Windows Forms App" was created successfully.

Processing post-creation actions...
Restoring d:\Projects\2025\OpenGLSharpGLDemo\OpenGLSharpGLDemo.csproj:
  Determining projects to restore...
  Restored d:\Projects\2025\OpenGLSharpGLDemo\OpenGLSharpGLDemo.csproj (in 115 ms).
Restore succeeded.



d:\Projects\2025>cd OpenGLSharpGLDemo

d:\Projects\2025\OpenGLSharpGLDemo>dotnet add package SharpGL.WinForms --version 3.0.0
  Determining projects to restore...
  Writing C:\Users\sandr\AppData\Local\Temp\tmpen2kix.tmp
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'SharpGL.WinForms' into project 'd:\Projects\2025\OpenGLSharpGLDemo\OpenGLSharpGLDemo.csproj'.
info : Restoring packages for d:\Projects\2025\OpenGLSharpGLDemo\OpenGLSharpGLDemo.csproj...
info :   GET https://api.nuget.org/v3/vulnerabilities/index.json
info :   OK https://api.nuget.org/v3/vulnerabilities/index.json 37ms
info :   GET https://api.nuget.org/v3-vulnerabilities/2025.07.10.23.23.28/vulnerability.base.json
info :   GET https://api.nuget.org/v3-vulnerabilities/2025.07.10.23.23.28/2025.07.11.05.23.29/vulnerability.update.json
info :   OK https://api.nuget.org/v3-vulnerabilities/2025.07.10.23.23.28/vulnerability.base.json 67ms
info :   OK https://api.nuget.org/v3-vulnerabilities/2025.07.10.23.23.28/2025.07.11.05.23.29/vulnerability.update.json 107ms
info : Package 'SharpGL.WinForms' is compatible with all the specified frameworks in project 'd:\Projects\2025\OpenGLSharpGLDemo\OpenGLSharpGLDemo.csproj'.
info : PackageReference for package 'SharpGL.WinForms' version '3.0.0' added to file 'd:\Projects\2025\OpenGLSharpGLDemo\OpenGLSharpGLDemo.csproj'.
info : Writing assets file to disk. Path: d:\Projects\2025\OpenGLSharpGLDemo\obj\project.assets.json
log  : Restored d:\Projects\2025\OpenGLSharpGLDemo\OpenGLSharpGLDemo.csproj (in 649 ms).

d:\Projects\2025\OpenGLSharpGLDemo>
