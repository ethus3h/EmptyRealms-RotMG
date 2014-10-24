@echo off
set /p jm="Jm file: "
copy %jm%.jm Json2wmapConv\bin\Debug
Json2wmapConv\bin\Debug\Json2wmapConv %jm%.jm %jm%.wmap
echo Map successfully compiled
pause