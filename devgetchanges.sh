#!/bin/bash

cd ~

if [ -d "Documents/workspace/microting/eform-angular-basecustomer-plugin/eform-client/src/app/plugins/modules/customers-pn" ]; then
	rm -fR Documents/workspace/microting/eform-angular-basecustomer-plugin/eform-client/src/app/plugins/modules/customers-pn
fi

cp -av Documents/workspace/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/customers-pn Documents/workspace/microting/eform-angular-basecustomer-plugin/eform-client/src/app/plugins/modules/customers-pn

if [ -d "Documents/workspace/microting/eform-angular-basecustomer-plugin/eFormAPI/Plugins/Customers.Pn" ]; then
	rm -fR Documents/workspace/microting/eform-angular-basecustomer-plugin/eFormAPI/Plugins/Customers.Pn
fi

cp -av Documents/workspace/microting/eform-angular-frontend/eFormAPI/Plugins/Customers.Pn Documents/workspace/microting/eform-angular-basecustomer-plugin/eFormAPI/Plugins/Customers.Pn
