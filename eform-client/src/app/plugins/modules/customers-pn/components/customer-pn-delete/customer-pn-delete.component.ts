import {Component, EventEmitter, OnInit,
  inject
} from '@angular/core';
import {CustomerPnFieldsEnum} from 'src/app/plugins/modules/customers-pn/enums';
import {CustomerPnModel} from 'src/app/plugins/modules/customers-pn/models/customer';
import {FieldsPnUpdateModel} from 'src/app/plugins/modules/customers-pn/models/field';
import {CustomersPnService} from '../../services';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-customer-pn-delete',
  templateUrl: './customer-pn-delete.component.html',
  styleUrls: ['./customer-pn-delete.component.scss'],
  standalone: false
})
export class CustomerPnDeleteComponent implements OnInit {
  private customersService = inject(CustomersPnService);
  public dialogRef = inject(MatDialogRef<CustomerPnDeleteComponent>);
  private data = inject<{customer: CustomerPnModel, fields: FieldsPnUpdateModel}>(MAT_DIALOG_DATA);

  customerDeleted: EventEmitter<void> = new EventEmitter<void>();
  selectedCustomer: CustomerPnModel = new CustomerPnModel();
  fields: FieldsPnUpdateModel = new FieldsPnUpdateModel();
  get fieldsEnum() { return CustomerPnFieldsEnum; }

  

  ngOnInit() {
    if (this.data) {
      this.selectedCustomer = this.data.customer;
      if (this.data.fields) {
        this.fields = this.data.fields;
      }
    }
  }

  hide() {
    this.dialogRef.close();
  }

  deleteCustomer() {
    this.customersService.deleteCustomer(this.selectedCustomer.id).subscribe(((data) => {
      if (data && data.success) {
        this.customerDeleted.emit();
        this.dialogRef.close(true);
      }
    }));
  }

}
