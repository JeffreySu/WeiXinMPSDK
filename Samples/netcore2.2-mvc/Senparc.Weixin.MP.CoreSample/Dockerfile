FROM microsoft/dotnet:2.2-aspnetcore-runtime
ARG source
WORKDIR /app
#COPY . .
EXPOSE 80
COPY ${source:-bin/Release/netcoreapp2.2/publish/} .
ENTRYPOINT ["dotnet", "Senparc.Weixin.MP.CoreSample.dll"]