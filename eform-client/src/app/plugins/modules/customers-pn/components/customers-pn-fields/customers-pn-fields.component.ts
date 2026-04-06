import {Component, OnDestroy, OnInit, inject} from '@angular/core';
import {Router} from '@angular/router';
import {AutoUnsubscribe} from 'ngx-auto-unsubscribe';
import {MatSlideToggleChange} from '@angular/material/slide-toggle';
import {Subscription} from 'rxjs';

import {CustomersPnFieldStatusEnum} from '../../enums';
import {
  CustomersPnSettingsModel,
  FieldPnUpdateModel,
  FieldsPnUpdateModel,
} from '../../models';
import {
  CustomersPnFieldsService,
  CustomersPnSettingsService,
} from '../../services';

@AutoUnsubscribe()
@Component({
  selector: 'app-customers-pn-fields',
  templateUrl: './customers-pn-fields.component.html',
  styleUrls: ['./customers-pn-fields.component.scss'],
  standalone: false
})
export class CustomersPnFieldsComponent implements OnInit, OnDestroy {
  private customersFieldsService = inject(CustomersPnFieldsService);
  private customersSettingsService = inject(CustomersPnSettingsService);
  private router = inject(Router);

  fieldsUpdateModel: FieldsPnUpdateModel = new FieldsPnUpdateModel();
  customersPnSettingsModel: CustomersPnSettingsModel = new CustomersPnSettingsModel();
  get fieldStatusEnum() {
    return CustomersPnFieldStatusEnum;
  }

  getAllFieldsSub$: Subscription;
  getSettingsSub$: Subscription;
  updateSettingsSub$: Subscription;

  ngOnInit() {
    this.getAllFields();
    this.getSettings();
  }

  ngOnDestroy(): void {}

  getAllFields() {
    this.getAllFieldsSub$ = this.customersFieldsService.getAllFields().subscribe((data) => {
      if (data?.success) {
        this.fieldsUpdateModel = data.model;
      }
    });
  }

  getSettings() {
    this.getSettingsSub$ = this.customersSettingsService.getAllSettings().subscribe((data) => {
      if (data?.success) {
        this.customersPnSettingsModel = data.model;
      }
    });
  }

  updateSettings() {
    this.updateSettingsSub$ = this.customersSettingsService
      .updateSettings(this.customersPnSettingsModel)
      .subscribe((data) => {
        if (data?.success) {
          this.customersFieldsService
            .updateFields(this.fieldsUpdateModel)
            .subscribe((innerData) => {
              if (innerData?.success) {
                this.router.navigate(['/plugins-settings']).then();
              }
            });
        }
      });
  }

  onFieldToggle(event: MatSlideToggleChange, field: FieldPnUpdateModel) {
    const target = this.fieldsUpdateModel.fields.find(x => x.id === field.id);
    if (target) {
      target.fieldStatus = event.checked
        ? CustomersPnFieldStatusEnum.Enabled
        : CustomersPnFieldStatusEnum.Disabled;
    }
  }
}
