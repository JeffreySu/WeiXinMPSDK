FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Senparc.Weixin.MP.CoreSample/Senparc.Weixin.MP.CoreSample.csproj", "Senparc.Weixin.MP.CoreSample/"]
COPY ["../../src/Senparc.Weixin.MP.MvcExtension/Senparc.Weixin.MP.MvcExtension/Senparc.Weixin.MP.MvcExtension.vs2017.csproj", "../../src/Senparc.Weixin.MP.MvcExtension/Senparc.Weixin.MP.MvcExtension/"]
COPY ["../../src/Senparc.Weixin.MP/Senparc.Weixin.MP/Senparc.Weixin.MP.vs2017.csproj", "../../src/Senparc.Weixin.MP/Senparc.Weixin.MP/"]
COPY ["../../src/Senparc.Weixin/Senparc.Weixin/Senparc.Weixin.vs2017.csproj", "../../src/Senparc.Weixin/Senparc.Weixin/"]
COPY ["../../src/Senparc.Weixin.TenPay/Senparc.Weixin.TenPay/Senparc.Weixin.TenPay.vs2017.csproj", "../../src/Senparc.Weixin.TenPay/Senparc.Weixin.TenPay/"]
COPY ["../../src/Senparc.WebSocket/src/Senparc.WebSocket/Senparc.WebSocket/Senparc.WebSocket.vs2017.csproj", "../../src/Senparc.WebSocket/src/Senparc.WebSocket/Senparc.WebSocket/"]
COPY ["../../src/Senparc.Weixin.Cache/Senparc.Weixin.Cache.Redis/Senparc.Weixin.Cache.Redis.vs2017.csproj", "../../src/Senparc.Weixin.Cache/Senparc.Weixin.Cache.Redis/"]
COPY ["../../src/Senparc.Weixin.Work/Senparc.Weixin.Work/Senparc.Weixin.Work.vs2017.csproj", "../../src/Senparc.Weixin.Work/Senparc.Weixin.Work/"]
COPY ["../../src/Senparc.Weixin.WxOpen/src/Senparc.Weixin.WxOpen/Senparc.Weixin.WxOpen/Senparc.Weixin.WxOpen.vs2017.csproj", "../../src/Senparc.Weixin.WxOpen/src/Senparc.Weixin.WxOpen/Senparc.Weixin.WxOpen/"]
COPY ["../../src/Senparc.Weixin.Open/Senparc.Weixin.Open/Senparc.Weixin.Open.vs2017.csproj", "../../src/Senparc.Weixin.Open/Senparc.Weixin.Open/"]
COPY ["../../src/Senparc.Weixin.Cache/Senparc.Weixin.Cache.Memcached/Senparc.Weixin.Cache.Memcached.vs2017.csproj", "../../src/Senparc.Weixin.Cache/Senparc.Weixin.Cache.Memcached/"]
COPY ["../Senparc.Weixin.MP.Sample/Senparc.Weixin.MP.Sample.CommonService/Senparc.Weixin.MP.CoreSample.CommonService.csproj", "../Senparc.Weixin.MP.Sample/Senparc.Weixin.MP.Sample.CommonService/"]
RUN dotnet restore "Senparc.Weixin.MP.CoreSample/Senparc.Weixin.MP.CoreSample.csproj"
COPY . .
WORKDIR "/src/Senparc.Weixin.MP.CoreSample"
RUN dotnet build "Senparc.Weixin.MP.CoreSample.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Senparc.Weixin.MP.CoreSample.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Senparc.Weixin.MP.CoreSample.dll"]