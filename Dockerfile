FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 80
EXPOSE 81

WORKDIR /app
COPY . ./publish/app

ENTRYPOINT ["dotnet", "NKZSoft.Template.Presentation.REST.dll"]