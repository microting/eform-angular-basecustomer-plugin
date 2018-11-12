import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AdminGuard, AuthGuard} from 'src/app/common/guards';
import {CustomerPnLayoutComponent} from './layouts';
import {CustomersPnPageComponent, CustomersPnFieldsComponent} from './components';

export const routes: Routes = [
  {
    path: '',
    component: CustomerPnLayoutComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        component: CustomersPnPageComponent
      },
      {
        path: 'settings',
        canActivate: [AdminGuard],
        component: CustomersPnFieldsComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomersPnRouting {}