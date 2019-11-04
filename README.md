# eform-angular-basecustomer-plugin
Build status
[![Build Status](https://travis-ci.org/microting/eform-angular-basecustomer-plugin.svg?branch=master)](https://travis-ci.org/microting/eform-angular-basecustomer-plugin)

## NOTICE! this is not a standalone project and needs to be used with eForm Angular Frontend.

## Translating eform-angular-basecustomer-plugin

| Language | C# part | Angular part |
| ------------- |:-----:|:-----:|
| English | 100% | 100%|
| Danish | 100% | 100% |
| German | 0% | 0% |

# Plugin manual installation tutorial
Extract zip content to a root application directory.
On front-end part plugins will be included into `eform-client/src/app/plugins/modules` To include module into front-end application add routing block to plugins.routing.module.ts


```
{
    ...
},
{
    path: 'customers-pn',
    canActivate: [AuthGuard],
    loadChildren: './modules/customers-pn/customers-pn.module#CustomersPnModule'
}
```

On the back-end part no need to do anything if you’re unpacking plugin binaries to `eFormApi/eFormAPI/Plugins`. 
If building plugin from source code – you’re need to open solution of plugin and build it in **Visual Studio 2017**.
If you’re need to implement any changes – go to `eFormAPI/Plugins/Customers.Pn`. Open solution, make changes and build it.

# Plugin installation guide:
First clone og download eform-angular-frontend via: <a href="https://github.com/microting/eform-angular-frontend"></a>

Second you clone down the project via a Git GUI og download the Zip file.
If you chose to download the zip file, extract the file.

In your folder you will find two folders named: eFormAPI and eform-client.

## For backend/C#:

Go to eFormAPI/Plugins

Copy Customers.Pn.

Go to your eform-angular-frontend folder.

Go to eFormAPI/Plugins.

Paste Customers.Pn.

Go to Customers.Pn and open Customers.Pn.sln in either Visual Studio og JetBrains Rider.

Rebuild the solution.

## For Frontend/Angular:

Go to eform-client/src/app/plugins/modules

Copy customers-pn

Go to you eform-angular-frontend folder.

Go to eform-client/src/app/plugins/modules.

Paste customers-pn

Add the following to 'eform-client/src/app/plugins/modules/plugins.routing.ts':
```
	{
    path: 'customers-pn',
    canActivate: [AuthGuard],
    loadChildren: './modules/customers-pn/customers-pn.module#CustomersPnModule'
	}
```
Your plugins.routing.ts should look like this now:

```
import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from 'src/app/common/guards';

export const routes: Routes = [
  {
    path: 'customers-pn',
    canActivate: [AuthGuard],
    loadChildren: './modules/customers-pn/customers-pn.module#CustomersPnModule'
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PluginsRouting {
}
```

# Plugin automatic installation

This is expection you to have eform-angular-frontend installed as default in /var/www/microting/

```
cd /var/www/microting/
curl -s -L https://raw.githubusercontent.com/microting/eform-angular-basecustomer-plugin/stable/install.sh | sudo bash
```

## Contributing

1. Do a fork
2. Clone your fork onto your own computer
3. Checkout/create a new branch for your relevant issue
4. Apply your changes and tests
5. Commit your changes and push to github
6. Create a pull-request

### Pull requests

To enable us to quickly review and accept your pull requests, always create one pull request per issue and link the issue in the pull request. Never merge multiple requests in one unless they have the same root cause. Be sure to follow our coding guidelines and keep code changes as small as possible. Avoid pure formatting changes to code that has not been modified otherwise. Pull requests should contain tests whenever possible.

Pull-reuqsts that do not pass tests, will not be accepted.

### Where to contribute

Check out the [full issues list](https://github.com/microting/eform-angular-basecustomer-plugin/issues) for a list of all potential areas for contributions.

To improve the chances to get a pull request merged you should select an issue that is labelled with the [help_wanted](https://github.com/microting/eform-angular-basecustomer-plugin/issues?q=is%3Aissue+is%3Aopen+label%3Ahelp_wanted) or [bug](https://github.com/microting/eform-angular-basecustomer-plugin/issues?q=is%3Aissue+is%3Aopen+label%3Abug) labels. If the issue you want to work on is not labelled with `help-wanted` or `bug`, you can start a conversation with the issue owner asking whether an external contribution will be considered.
	
### Suggestions

We're also interested in your feedback for the future of Microting eForm SDK. You can submit a suggestion or feature request through the issue tracker. To make this process more effective, we're asking that these include more information to help define them more clearly.

## Microting Open Source Code of Conduct

This project has adopted the [Microting Open Source Code of Conduct](https://www.microting.com/microting-open-source-code-of-conduct). Contact opencode@microting.com with any additional questions or comments.
	

## License

The MIT License (MIT)

Copyright (c) 2007-2019 Microting A/S

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
