#!/bin/bash

if [ ! -d "/var/www/microting/eform-angular-basecustomer-plugin" ]; then
  cd /var/www/microting
  su ubuntu -c \
  "git clone https://github.com/microting/eform-angular-basecustomer-plugin.git -b stable"
fi

cd /var/www/microting/eform-angular-basecustomer-plugin
git pull
su ubuntu -c \
"dotnet restore eFormAPI/Plugins/Customers.Pn/Customers.Pn.sln"

echo "################## START GITVERSION ##################"
export GITVERSION=`git describe --abbrev=0 --tags | cut -d "v" -f 2`
echo $GITVERSION
echo "################## END GITVERSION ##################"
su ubuntu -c \
"dotnet publish eFormAPI/Plugins/Customers.Pn/Customers.Pn.sln -o out /p:Version=$GITVERSION --runtime linux-x64 --configuration Release"

if [ -d "/var/www/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/customers-pn"]; then
	su ubuntu -c \
	"rm -fR /var/www/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/customers-pn"
fi

su ubuntu -c \
"cp -av /var/www/microting/eform-angular-basecustomer-plugin/eform-client/src/app/plugins/modules/customers-pn /var/www/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/customers-pn"
su ubuntu -c \
"mkdir -p /var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/"

if [ -d "/var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/Customers"]; then
	su ubuntu -c \
	"rm -fR /var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/Customers"
fi

su ubuntu -c \
"cp -av /var/www/microting/eform-angular-basecustomer-plugin/eFormAPI/Plugins/Customers.Pn/Customers.Pn/out /var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/Customers"


echo "Recompile angular"
cd /var/www/microting/eform-angular-frontend/eform-client
su ubuntu -c \
"/var/www/microting/eform-angular-basecustomer-plugin/testinginstallpn.sh"
su ubuntu -c \
"export NODE_OPTIONS=--max_old_space_size=8192 && time GENERATE_SOURCEMAP=false npm run build"
echo "Recompiling angular done"
