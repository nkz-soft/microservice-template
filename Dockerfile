FROM mcr.microsoft.com/dotnet/nightly/aspnet:7.0-jammy-chiseled
EXPOSE 80
EXPOSE 81

WORKDIR /app
COPY ./src/NKZSoft.Template.Presentation.Starter/publish/app .

ENTRYPOINT ["dotnet", "NKZSoft.Template.Presentation.Starter.dll"]
