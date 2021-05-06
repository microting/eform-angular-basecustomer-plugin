import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdvEntitySearchableGroupListRequestModel } from 'src/app/common/models';
import { CustomersPnFieldStatusEnum } from '../../enums';
import {
  CustomersPnSettingsModel,
  FieldPnUpdateModel,
  FieldsPnUpdateModel,
} from '../../models';
import {
  CustomersPnFieldsService,
  CustomersPnSettingsService,
} from '../../services';

@Component({
  selector: 'app-customers-pn-fields',
  templateUrl: './customers-pn-fields.component.html',
  styleUrls: ['./customers-pn-fields.component.scss'],
})
export class CustomersPnFieldsComponent implements OnInit {
  isChecked = false;
  fieldsUpdateModel: FieldsPnUpdateModel = new FieldsPnUpdateModel();
  customersPnSettingsModel: CustomersPnSettingsModel = new CustomersPnSettingsModel();
  advEntitySearchableGroupListRequestModel: AdvEntitySearchableGroupListRequestModel = new AdvEntitySearchableGroupListRequestModel();
  get fieldStatusEnum() {
    return CustomersPnFieldStatusEnum;
  }

  constructor(
    private customersFieldsService: CustomersPnFieldsService,
    private customersSettingsService: CustomersPnSettingsService,
    private router: Router
  ) {}

  ngOnInit() {
    this.advEntitySearchableGroupListRequestModel.pageSize = 15;
    this.getAllFields();
    this.getSettings();
  }

  getAllFields() {
    this.customersFieldsService.getAllFields().subscribe((data) => {
      if (data && data.success) {
        this.fieldsUpdateModel = data.model;
      }
    });
  }

  getSettings() {
    this.customersSettingsService.getAllSettings().subscribe((data) => {
      if (data && data.success) {
        this.customersPnSettingsModel = data.model;
      }
    });
  }

  updateSettings() {
    this.customersSettingsService
      .updateSettings(this.customersPnSettingsModel)
      .subscribe((data) => {
        if (data && data.success) {
          this.customersFieldsService
            .updateFields(this.fieldsUpdateModel)
            .subscribe((innerData) => {
              if (innerData && innerData.success) {
                this.router.navigate(['/plugins-settings']).then();
              }
            });
        }
      });
  }

  checkBoxChanged(e: any, field: FieldPnUpdateModel) {
    if (e.target && e.target.checked) {
      this.fieldsUpdateModel.fields.find((x) => x.id === field.id).fieldStatus =
        CustomersPnFieldStatusEnum.Enabled;
    } else if (e.target && !e.target.checked) {
      this.fieldsUpdateModel.fields.find((x) => x.id === field.id).fieldStatus =
        CustomersPnFieldStatusEnum.Disabled;
    }
  }

  onSelectedChanged(e: any) {
    this.customersPnSettingsModel.relatedEntityId = e.microtingUUID;
  }
}
