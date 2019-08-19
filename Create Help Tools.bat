set msbuild="C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe"
if exist %msbuild% goto :start
set msbuild="C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"

:start
%msbuild% "Help Tools\Help Tools.sln" /p:Configuration=Release /t:Build
