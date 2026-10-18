import {NgModule} from '@angular/core';
import {MatDialogModule} from '@angular/material/dialog';
import {StoreModule} from '@ngrx/store';

import {CustomersPnFieldsService, CustomersPnService, CustomersPnSettingsService} from './services';
import {CustomersPnRouting} from './customers-pn.routing';
import {customersReducer} from './state';

@NgModule({
  imports: [
    MatDialogModule,
    CustomersPnRouting,
    StoreModule.forFeature('customersPn', {customersState: customersReducer}),
  ],
  providers: [
    CustomersPnService,
    CustomersPnFieldsService,
    CustomersPnSettingsService
  ]
})
export class CustomersPnModule {}
