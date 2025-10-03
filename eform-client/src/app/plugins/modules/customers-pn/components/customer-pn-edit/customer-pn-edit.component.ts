import {Component, EventEmitter, Inject, OnInit} from '@angular/core';
import {CustomerPnFieldsEnum, CustomersPnFieldStatusEnum} from '../../enums';
import {FieldsPnUpdateModel, CustomerPnFullModel} from '../../models';
import {CustomersPnService} from '../../services';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-customer-pn-edit',
  templateUrl: './customer-pn-edit.component.html',
  styleUrls: ['./customer-pn-edit.component.scss'],
  standalone: false
})
export class CustomerPnEditComponent implements OnInit {
  fieldsModel = new FieldsPnUpdateModel();
  customerUpdate: EventEmitter<void> = new EventEmitter<void>();

  get fieldsEnum() { return CustomerPnFieldsEnum; }

  selectedCustomerModel = new CustomerPnFullModel();

  constructor(
    private customersService: CustomersPnService,
    public dialogRef: MatDialogRef<CustomerPnEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {customerId: number, fields: FieldsPnUpdateModel}
  ) {
    if (data && data.fields) {
      this.fieldsModel = data.fields;
    }
  }

  ngOnInit() {
    if (this.data && this.data.customerId) {
      this.getSingleCustomer(this.data.customerId);
    }
  }

  hide() {
    this.dialogRef.close();
    this.selectedCustomerModel = new CustomerPnFullModel();
  }

  getSingleCustomer(customerId: number) {
    this.customersService.getSingleCustomer(customerId).subscribe(((data) => {
      if (data && data.success) {
        this.selectedCustomerModel = data.model;
      }

    }));
  }

  updateCustomer() {
    this.customersService.updateCustomer(this.selectedCustomerModel).subscribe(((data) => {
      if (data && data.success) {
        this.selectedCustomerModel = new CustomerPnFullModel();
        this.customerUpdate.emit();
        this.dialogRef.close(true);
      }

    }));
  }

  isFieldAvailable(field: number) {
    const foundField = this.fieldsModel.fields.find(x => x.name == CustomerPnFieldsEnum[field]);
    if (foundField) {
      return foundField.fieldStatus === CustomersPnFieldStatusEnum.Enabled;
    } else {
      return false;
    }
  }


}
