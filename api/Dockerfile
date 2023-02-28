FROM mcr.microsoft.com/dotnet/sdk:7.0 AS sdk
WORKDIR /app

COPY . ./

ENV DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1

ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://+:5000"]