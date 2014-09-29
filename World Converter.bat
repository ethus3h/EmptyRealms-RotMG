@echo off
echo [JSON Map Compiler] Successfully started.
set /p jm="JSON Map Name: "
copy %jm%.jm Json2wmapConv\bin\Debug
Json2wmapConv\bin\Debug\Json2wmapConv %jm%.jm %jm%.wmap
echo Compiled map!
pause