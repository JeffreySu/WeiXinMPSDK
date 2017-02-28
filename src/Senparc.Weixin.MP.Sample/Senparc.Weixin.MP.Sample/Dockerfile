FROM microsoft/aspnetcore:1.0
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Senparc.Weixin.MP.Sample.dll"]
