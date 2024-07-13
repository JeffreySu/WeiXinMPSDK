# deploy-sample-net8.ps1  
param (  
    [string]$sourcePath,  
    [string]$destinationPath,  
    [string]$server,  
    [string]$username,  
    [string]$privateKeyPath  
)  
  
# 复制文件到服务器  
function Copy-Files {  
    param (  
        [string]$source,  
        [string]$destination,  
        [string]$server,  
        [string]$username,  
        [string]$privateKey  
    )  
  
    # 使用SCP命令上传文件  
    $scpCommand = "scp -i `${privateKey} -r `${source} `${username}@`${server}:${destination}"  
    Invoke-Expression $scpCommand  
}  
  
# 排除特定文件  
function Exclude-Files {  
    param (  
        [string]$source,  
        [string]$destination  
    )  
  
    $filesToExclude = @("Web.Config", "appsettings.json")  
    foreach ($file in $filesToExclude) {  
        $sourceFile = Join-Path -Path $source -ChildPath $file  
        if (Test-Path -Path $sourceFile) {  
            # 将文件移动到临时路径，以便稍后恢复  
            $tempPath = Join-Path -Path $env:TEMP -ChildPath $file  
            Move-Item -Path $sourceFile -Destination $tempPath  
        }  
    }  
  
    Copy-Files -source $source -destination $destination -server $server -username $username -privateKey $privateKeyPath  
  
    # 恢复被排除的文件  
    foreach ($file in $filesToExclude) {  
        $tempPath = Join-Path -Path $env:TEMP -ChildPath $file  
        if (Test-Path -Path $tempPath) {  
            $destinationFile = Join-Path -Path $destination -ChildPath $file  
            Move-Item -Path $tempPath -Destination $destinationFile  
        }  
    }  
}  
  
# 调用函数  
Exclude-Files -source $sourcePath -destination $destinationPath  
