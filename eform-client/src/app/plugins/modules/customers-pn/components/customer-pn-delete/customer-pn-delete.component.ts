import {Component, EventEmitter, Inject, OnInit} from '@angular/core';
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
  customerDeleted: EventEmitter<void> = new EventEmitter<void>();
  selectedCustomer: CustomerPnModel = new CustomerPnModel();
  fields: FieldsPnUpdateModel = new FieldsPnUpdateModel();
  get fieldsEnum() { return CustomerPnFieldsEnum; }

  constructor(
    private customersService: CustomersPnService,
    public dialogRef: MatDialogRef<CustomerPnDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {customer: CustomerPnModel, fields: FieldsPnUpdateModel}
  ) {
    if (data) {
      this.selectedCustomer = data.customer;
      if (data.fields) {
        this.fields = data.fields;
      }
    }
  }

  ngOnInit() {
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
