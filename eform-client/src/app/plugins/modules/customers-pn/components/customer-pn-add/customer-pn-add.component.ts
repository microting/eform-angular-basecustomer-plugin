import {Component, EventEmitter, Inject, OnInit} from '@angular/core';
import {CustomerPnFieldsEnum, CustomersPnFieldStatusEnum} from '../../enums';
import {FieldsPnUpdateModel, CustomerPnFullModel} from '../../models';
import {CustomersPnService} from '../../services';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-customer-pn-add',
  templateUrl: './customer-pn-add.component.html',
  styleUrls: ['./customer-pn-add.component.scss'],
  standalone: false
})
export class CustomerPnAddComponent implements OnInit {
  customerCreated: EventEmitter<void> = new EventEmitter<void>();
  fieldsModel = new FieldsPnUpdateModel();
  get fieldsEnum() { return CustomerPnFieldsEnum; }
  newCustomerModel: CustomerPnFullModel = new CustomerPnFullModel();

  constructor(
    private customersService: CustomersPnService,
    public dialogRef: MatDialogRef<CustomerPnAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {customerId?: number, fields: FieldsPnUpdateModel}
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
    this.newCustomerModel = new CustomerPnFullModel();
  }

  getSingleCustomer(customerId: number) {
    this.customersService.getSingleCustomer(customerId).subscribe(((data) => {
      if (data && data.success) {
        this.newCustomerModel = data.model;
      }

    }));
  }

  createCustomer() {
    this.newCustomerModel.relatedEntityId = null;
    this.customersService.createCustomer(this.newCustomerModel).subscribe(((data) => {
      if (data && data.success) {
        this.newCustomerModel = new CustomerPnFullModel();
        this.customerCreated.emit();
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
