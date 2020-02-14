#!/bin/bash
cd ~
pwd

rm -fR Documents/workspace/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/customers-pn
rm -fR Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Tests/customer-settings
rm -fR Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Tests/customer-general
rm -fR Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Assets
rm -fR Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Page\ objects/Customers
rm -fR Documents/workspace/microting/eform-angular-frontend/eform-client/wdio-plugin-step2.conf.js

cp -av Documents/workspace/microting/eform-angular-basecustomer-plugin/eform-client/src/app/plugins/modules/customers-pn Documents/workspace/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/customers-pn
cp -av Documents/workspace/microting/eform-angular-basecustomer-plugin/eform-client/e2e/Tests/customer-settings Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Tests/customer-settings
cp -av Documents/workspace/microting/eform-angular-basecustomer-plugin/eform-client/e2e/Tests/customer-general Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Tests/customer-general
cp -av Documents/workspace/microting/eform-angular-basecustomer-plugin/eform-client/e2e/Assets/ Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Assets
cp -av Documents/workspace/microting/eform-angular-basecustomer-plugin/eform-client/e2e/Page\ objects/Customers Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Page\ objects/Customers
cp -av Documents/workspace/microting/eform-angular-basecustomer-plugin/eform-client/wdio-headless-plugin-step2.conf.js Documents/workspace/microting/eform-angular-frontend/eform-client/wdio-plugin-step2.conf.js

rm -fR Documents/workspace/microting/eform-angular-frontend/eFormAPI/Plugins/Customers.Pn

cp -av Documents/workspace/microting/eform-angular-basecustomer-plugin/eFormAPI/Plugins/Customers.Pn Documents/workspace/microting/eform-angular-frontend/eFormAPI/Plugins/Customers.Pn
