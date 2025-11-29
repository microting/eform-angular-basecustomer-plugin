import {Component, EventEmitter, OnInit,
  inject
} from '@angular/core';
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
  private customersService = inject(CustomersPnService);
  public dialogRef = inject(MatDialogRef<CustomerPnAddComponent>);
  private data = inject<{customerId?: number, fields: FieldsPnUpdateModel}>(MAT_DIALOG_DATA);

  customerCreated: EventEmitter<void> = new EventEmitter<void>();
  fieldsModel = new FieldsPnUpdateModel();
  get fieldsEnum() { return CustomerPnFieldsEnum; }
  newCustomerModel: CustomerPnFullModel = new CustomerPnFullModel();

  

  ngOnInit() {
    if (this.data && this.data.fields) {
      this.fieldsModel = this.data.fields;
    }
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
