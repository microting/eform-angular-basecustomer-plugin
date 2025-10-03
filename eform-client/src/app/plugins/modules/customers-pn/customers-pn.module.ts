import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';
import { StoreModule } from '@ngrx/store';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CustomerPnLayoutComponent } from './layouts';
import {
  CustomersPnFieldsService,
  CustomersPnService,
  CustomersPnSettingsService,
} from './services';
import { CustomersPnRouting } from './customers-pn.routing';
import { SharedPnModule } from '../shared/shared-pn.module';
import {
  CustomerPnAddComponent,
  CustomerPnDeleteComponent,
  CustomerPnEditComponent,
  CustomerPnImportComponent,
  CustomersPnFieldsComponent,
  CustomersPnPageComponent,
} from './components';
import { EformSharedModule } from 'src/app/common/modules/eform-shared/eform-shared.module';
import { CustomersStateService } from './components/store';
import { customersReducer } from './state';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedPnModule,
    CustomersPnRouting,
    TranslateModule,
    NgSelectModule,
    EformSharedModule,
    RouterModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    StoreModule.forFeature('customersPn', {
      customersState: customersReducer,
    }),
  ],
  declarations: [
    CustomerPnLayoutComponent,
    CustomersPnPageComponent,
    CustomerPnEditComponent,
    CustomerPnDeleteComponent,
    CustomersPnFieldsComponent,
    CustomerPnAddComponent,
    CustomerPnImportComponent,
  ],
  providers: [
    CustomersPnService,
    CustomersPnFieldsService,
    CustomersPnSettingsService,
    CustomersStateService,
  ],
})
export class CustomersPnModule {}
