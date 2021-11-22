set -x
apt-get update && apt-get install wget bsdtar -y
cd /lm-data-sdk-dotnet/docs
mkdir docfx
wget -qO- https://github.com/dotnet/docfx/releases/download/v2.58.4/docfx.zip  | bsdtar -xvf- -C ./docfx
pwd
mono docfx/docfx.exe
mono docfx/docfx.exe  serve _site
rm -rf docfx