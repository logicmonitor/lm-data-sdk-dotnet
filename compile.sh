set -x
apt-get update && apt-get install wget bsdtar -y
cd /lm-data-sdk-dotnet/Docfx
mkdir docfx
wget -qO- https://github.com/dotnet/docfx/releases/download/v2.58.4/docfx.zip  | bsdtar -xvf- -C ./docfx
pwd
mono docfx/docfx.exe
rm -rf docfx