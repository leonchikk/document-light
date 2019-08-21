FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

# RUN dotnet dev-certs https

# COPY --from=build /root/.dotnet/corefx/cryptography /root/.dotnet/corefx/cryptography

ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS http://+:27015;
EXPOSE 27015

COPY ./publish /publish
WORKDIR /publish

ENTRYPOINT ["dotnet", "DocumentLight.API.dll"]