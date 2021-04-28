import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule } from '@ngx-translate/core';
import { MDBRootModule } from 'angular-bootstrap-md';
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
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { EformSharedModule } from 'src/app/common/modules/eform-shared/eform-shared.module';
import { customersPersistProvider } from './components/store/customers-store';
import { CustomersStateService } from './components/store/customers-state-service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedPnModule,
    CustomersPnRouting,
    TranslateModule,
    MDBRootModule,
    NgSelectModule,
    FontAwesomeModule,
    EformSharedModule,
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
    customersPersistProvider,
  ],
})
export class CustomersPnModule {}
