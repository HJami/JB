# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY Release /app
ENTRYPOINT [ "dotnet", "JobBoardAPI.dll"]