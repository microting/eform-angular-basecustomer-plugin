import {Component, EventEmitter, OnInit,
  inject
} from '@angular/core';
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
  private customersService = inject(CustomersPnService);
  public dialogRef = inject(MatDialogRef<CustomerPnEditComponent>);
  private data = inject<{customerId: number, fields: FieldsPnUpdateModel}>(MAT_DIALOG_DATA);

  fieldsModel = new FieldsPnUpdateModel();
  customerUpdate: EventEmitter<void> = new EventEmitter<void>();

  get fieldsEnum() { return CustomerPnFieldsEnum; }

  selectedCustomerModel = new CustomerPnFullModel();

  

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
