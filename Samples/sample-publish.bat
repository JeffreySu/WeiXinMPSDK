@echo off  
setlocal  
  
echo Publishing MP/Senparc.Weixin.Sample.MP...  
dotnet publish MP/Senparc.Weixin.Sample.MP  
  
set ProjectFolders=(TenPayV2, TenPayV3, WxOpen)  
  
for %%F in %ProjectFolders% do (  
    echo Publishing %%F...  
    dotnet publish %%F  
)  
  
echo All projects have been published.  
endlocal  
pause  