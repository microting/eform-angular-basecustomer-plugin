import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {CustomersPnFieldStatusEnum} from '../../enums';
import {FieldPnUpdateModel, FieldsPnUpdateModel} from '../../models';
import {CustomersPnFieldsService} from '../../services';

@Component({
  selector: 'app-customers-pn-fields',
  templateUrl: './customers-pn-fields.component.html',
  styleUrls: ['./customers-pn-fields.component.scss']
})
export class CustomersPnFieldsComponent implements OnInit {
  isChecked = false;
  spinnerStatus = false;
  fieldsUpdateModel: FieldsPnUpdateModel = new FieldsPnUpdateModel();
  get fieldStatusEnum() { return CustomersPnFieldStatusEnum; }

  constructor(
    private customersFieldsService: CustomersPnFieldsService, private router: Router) { }

  ngOnInit() {
    this.getAllFields();
  }

  getAllFields() {
    this.spinnerStatus = true;
    this.customersFieldsService.getAllFields().subscribe((data) => {
      if (data && data.success) {
        this.fieldsUpdateModel = data.model;
      } this.spinnerStatus = false;
    });
  }

  updateFields() {
    this.spinnerStatus = true;
    this.customersFieldsService.updateFields(this.fieldsUpdateModel).subscribe((data) => {
      if (data && data.success) {
        this.router.navigate(['/plugins/customers-pn']).then();
      } this.spinnerStatus = false;
    });
  }

  checkBoxChanged(e: any, field: FieldPnUpdateModel) {
    if (e.target && e.target.checked) {
      this.fieldsUpdateModel.fields.find(x => x.id === field.id).fieldStatus = CustomersPnFieldStatusEnum.Enabled;
    } else if (e.target && !e.target.checked) {
      this.fieldsUpdateModel.fields.find(x => x.id === field.id).fieldStatus = CustomersPnFieldStatusEnum.Disabled;
    }
  }
}
