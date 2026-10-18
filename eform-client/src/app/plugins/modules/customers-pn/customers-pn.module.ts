import {NgModule} from '@angular/core';
import {StoreModule} from '@ngrx/store';

import {CustomersPnFieldsService, CustomersPnService, CustomersPnSettingsService} from './services';
import {CustomersPnRouting} from './customers-pn.routing';
import {customersReducer} from './state';

@NgModule({
  imports: [
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
