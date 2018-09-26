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
