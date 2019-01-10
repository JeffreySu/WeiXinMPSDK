dotnet restore .
dotnet build .
dotnet publish -c Release -o bin\Release\netcoreapp2.2\publish\
docker image build --file Dockerfile --tag weixin-sample:3.0 .
docker run -p 5000:80 -e "ASPNETCORE_URLS=http://+:80"  -it --name weixinsample --rm weixin-sample:3.0