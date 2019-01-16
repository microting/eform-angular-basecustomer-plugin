# Plugin installation tutorial
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

After that add link to the menu. Go to `eform-client/src/app/components/navigation/navigation.component.ts` and paste if you want to be it in menu

```
{
    ...
},
{
     name: 'Customers Pn',  // here goes plugin name
     link: '/plugins/customers-pn', 	// here goes plugin link
     e2eId: '', // here goes e2eid for testing
     submenus: []
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