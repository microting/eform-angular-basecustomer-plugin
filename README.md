# Plugin installation tutorial
Extract zip content to a root application directory.
On front-end part plugins will be included into `eform-client/src/app/plugins/modules` To include module into front-end application add routing block to plugins.routing.module.ts


```
{
    ...
},
{
    path: 'case-management-pn',
    canActivate: [AuthGuard],
    loadChildren: './modules/case-management-pn/case-management-pn.module#CaseManagementPnModule'
}
```

After that add link to the menu. Go to `eform-client/src/app/components/navigation/navigation.component.ts` and paste if you want to be it in menu

```
{
    ...
},
{
          name: this.translateService.instant('Case Management'),
          e2eId: 'case-management-pn',
          submenus: [
            {
              name: this.translateService.instant('Calendar'),
              e2eId: 'case-management-pn-calendar',
              link: '/plugins/case-management-pn/calendar'
            },
            {
              name: this.translateService.instant('Cases'),
              e2eId: 'case-management-pn-cases',
              link: '/plugins/case-management-pn/cases'
            },
            {
              name: this.translateService.instant('Settings'),
              e2eId: 'case-management-pn-settings',
              link: '/plugins/case-management-pn/settings',
              guard: 'admin'
            }
          ]
}
```

On the back-end part no need to do anything if you’re unpacking plugin binaries to `eFormApi/eFormAPI/Plugins`. 
If building plugin from source code – you’re need to open solution of plugin and build it in **Visual Studio 2017**.
If you’re need to implement any changes – go to `eFormAPI/Plugins/CaseManagement.Pn`. Open solution, make changes and build it.

## Configuration steps

1. Create searchable group
2. Create template from XML with searchable group id
3. Select date to as first case column and case name as second case column in templates
4. Bind all customers to selected list in Customers plugin
5. Bind Case management plugin to template from step 2
