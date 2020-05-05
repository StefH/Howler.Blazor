@ECHO OFF
SET tmp_folder=docs-temp
SET www_folder=%tmp_folder%/wwwroot
SET docs_folder=docs

dotnet publish examples/Howler.Blazor-WASM-AudioPlayer -c Release -o %tmp_folder%

RD /S /Q %docs_folder%

robocopy %www_folder% %docs_folder% /E /MOVE