import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgSelectModule} from '@ng-select/ng-select';
import {TranslateModule} from '@ngx-translate/core';
import {MtxGridModule} from '@ng-matero/extensions/grid';
import {MatDialogModule} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import {StoreModule} from '@ngrx/store';

import {CustomerPnLayoutComponent} from './layouts';
import {CustomersPnFieldsService, CustomersPnService, CustomersPnSettingsService} from './services';
import {CustomersPnRouting} from './customers-pn.routing';
import {SharedPnModule} from '../shared/shared-pn.module';
import {EformSharedModule} from 'src/app/common/modules/eform-shared/eform-shared.module';
import {customersReducer} from './state';

import {
  CustomerPnDeleteComponent,
  CustomerPnEditComponent,
  CustomersPnContainerComponent,
  CustomersPnTableComponent,
  CustomersPnFieldsComponent,
  CustomerPnAddComponent,
  CustomerPnImportComponent
} from './components';

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
    MtxGridModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatMenuModule,
    MatSlideToggleModule,
    MatTooltipModule,
    MatButtonModule,
    MatSelectModule,
    StoreModule.forFeature('customersPn', {customersState: customersReducer}),
  ],
  declarations: [
    CustomerPnLayoutComponent,
    CustomersPnContainerComponent,
    CustomersPnTableComponent,
    CustomerPnEditComponent,
    CustomerPnDeleteComponent,
    CustomersPnFieldsComponent,
    CustomerPnAddComponent,
    CustomerPnImportComponent
  ],
  providers: [
    CustomersPnService,
    CustomersPnFieldsService,
    CustomersPnSettingsService
  ]
})
export class CustomersPnModule {}
