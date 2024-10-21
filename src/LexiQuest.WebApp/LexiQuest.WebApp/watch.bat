:: только с этой коммандой работает цветной вывод в dotnet watch https://github.com/serilog/serilog-sinks-console/issues/64#issuecomment-558128107
set DOTNET_WATCH_SUPPRESS_LAUNCH_BROWSER=1
%~d0
cd %~dp0
dotnet watch run --no-hot-reload