@echo off

color 66

echo =======================================

echo = 代码合并工具 github.com/atnet/devfw =

echo =======================================

set dir=%~dp0

set megdir=%dir%\dll\

if exist "%megdir%merge.exe" (

  echo 生成中,请稍等...
  cd %dir%Senparc.Weixin.MP.BuildOutPut\

echo  /keyfile:%dir%ops.cms.snk>nul

"%megdir%merge.exe" /closed /ndebug /targetplatform:v4 /target:dll /out:%dir%dist\weixin.dll Senparc.Weixin.dll Senparc.Weixin.MP.dll Senparc.Weixin.MP.Util.dll


  echo 完成!输出到:%dir%dist\weixin.dll

)


pause