FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

COPY ./publish /publish
WORKDIR /publish
EXPOSE 27016
ENTRYPOINT ["dotnet", "TemplateLight.API.dll"]