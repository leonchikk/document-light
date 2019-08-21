FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

COPY ./publish /publish
WORKDIR /publish
EXPOSE 5000
ENTRYPOINT ["dotnet", "TemplateLight.API.dll"]