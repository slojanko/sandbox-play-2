@ECHO OFF
REM Courtesy of Stefan

SET game="SandboxPlay2.exe"

ECHO Run %game% with available Graphics API
ECHO 1 - Vulkan
ECHO 2 - DX11
ECHO 3 - DX12
ECHO 4 - OpenGL
ECHO 5 - OpenGLES2
ECHO 6 - OpenGLES3

CHOICE /C:123456

IF errorlevel 6 goto OpenGLES3
IF errorlevel 5 goto OpenGLES2
IF errorlevel 4 goto OpenGL
IF errorlevel 3 goto DX12
IF errorlevel 2 goto DX11
IF errorlevel 1 goto Vulkan

:Vulkan
ECHO Vulkan
%game% -force-vulkan
EXIT

:DX11
ECHO DX11
%game% -force-d3d11
EXIT

:DX12
ECHO DX12
%game% -force-d3d12
EXIT

:OpenGL
ECHO OpenGL
%game% -force-opengl
EXIT

:OpenGLES2
ECHO OpenGLES2
%game% -force-gles20
EXIT

:OpenGLES3
ECHO OpenGLES3
%game% -force-gles30
EXIT