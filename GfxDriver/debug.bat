call build.bat
cd ..
del /f VirtualDrive\gfx.so
xcopy /f GfxDriver\gfx.so VirtualDrive
FatSync\bin\Debug\FatSync.exe Vbox\VirtualDrive.vhd VirtualDrive
pause