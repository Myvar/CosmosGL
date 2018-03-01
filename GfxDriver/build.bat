cd %1
cd GfxDriver
cd tmp

if exist "C:\Windows\Sysnative\bash.exe" (
  set bashexe=C:\Windows\Sysnative\bash.exe
) else (
  set bashexe=C:\Windows\System32\bash.exe
)

%bashexe% -c "rm ./*.o && gcc -c ../src/*.c -I ../include -masm=intel -m32 -std=gnu99 -fno-builtin -ffreestanding -w -O2 -Wall -Wextra -nostartfiles -nostdlib -fno-stack-protector"

cd ..
del /f gfx.so
%bashexe% -c "ld -m elf_i386 -r -T linker.ld -o ./gfx.so ./tmp/*.o"