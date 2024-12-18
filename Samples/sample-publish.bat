@echo off  
setlocal  
  
echo Publishing MP/Senparc.Weixin.Sample.MP/Senparc.Weixin.Sample.MP.net8.csproj...  
dotnet publish MP/Senparc.Weixin.Sample.MP/Senparc.Weixin.Sample.MP.net8.csproj -o ./SamplePublish/MP  
  
echo Publishing TenPayV2/Senparc.Weixin.Sample.TenPayV2/Senparc.Weixin.Sample.TenPayV2.net8.csproj...  
dotnet publish TenPayV2/Senparc.Weixin.Sample.TenPayV2/Senparc.Weixin.Sample.TenPayV2.net8.csproj -o ./SamplePublish/TenPayV2  
  
echo Publishing TenPayV3/Senparc.Weixin.Sample.TenPayV3/Senparc.Weixin.Sample.TenPayV3.net8.csproj...  
dotnet publish TenPayV3/Senparc.Weixin.Sample.TenPayV3/Senparc.Weixin.Sample.TenPayV3.net8.csproj -o ./SamplePublish/TenPayV3  
  
echo Publishing Work/Senparc.Weixin.Sample.Work/Senparc.Weixin.Sample.Work.net8.csproj...  
dotnet publish Work/Senparc.Weixin.Sample.Work/Senparc.Weixin.Sample.Work.net8.csproj -o ./SamplePublish/Work  
  
echo Publishing WxOpen/Senparc.Weixin.Sample.WxOpen/Senparc.Weixin.Sample.WxOpen.net8.csproj...  
dotnet publish WxOpen/Senparc.Weixin.Sample.WxOpen/Senparc.Weixin.Sample.WxOpen.net8.csproj -o ./SamplePublish/WxOpen  
  
echo All projects have been published.  
endlocal  
pause  