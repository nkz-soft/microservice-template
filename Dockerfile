FROM mcr.microsoft.com/dotnet/nightly/aspnet:9.0-noble-chiseled
EXPOSE 8080
EXPOSE 8081

USER app

WORKDIR /home/app

COPY ./src/NKZSoft.Template.Presentation.Starter/publish/app .

ENTRYPOINT ["dotnet", "NKZSoft.Template.Presentation.Starter.dll"]
