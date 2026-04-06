import {Component, EventEmitter, OnDestroy, OnInit, inject} from '@angular/core';
import {AutoUnsubscribe} from 'ngx-auto-unsubscribe';
import {Subscription} from 'rxjs';

import {CustomerPnFieldsEnum} from '../../enums';
import {CustomerPnModel} from '../../models/customer';
import {FieldsPnUpdateModel} from '../../models/field';
import {CustomersPnService} from '../../services';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@AutoUnsubscribe()
@Component({
  selector: 'app-customer-pn-delete',
  templateUrl: './customer-pn-delete.component.html',
  styleUrls: ['./customer-pn-delete.component.scss'],
  standalone: false
})
export class CustomerPnDeleteComponent implements OnInit, OnDestroy {
  private customersService = inject(CustomersPnService);
  public dialogRef = inject(MatDialogRef<CustomerPnDeleteComponent>);
  private data = inject<{customer: CustomerPnModel, fields: FieldsPnUpdateModel}>(MAT_DIALOG_DATA);

  customerDeleted: EventEmitter<void> = new EventEmitter<void>();
  selectedCustomer: CustomerPnModel = new CustomerPnModel();
  fields: FieldsPnUpdateModel = new FieldsPnUpdateModel();
  get fieldsEnum() { return CustomerPnFieldsEnum; }

  deleteSub$: Subscription;

  ngOnInit() {
    if (this.data) {
      this.selectedCustomer = this.data.customer;
      if (this.data.fields) {
        this.fields = this.data.fields;
      }
    }
  }

  ngOnDestroy(): void {}

  hide() {
    this.dialogRef.close();
  }

  deleteCustomer() {
    this.deleteSub$ = this.customersService.deleteCustomer(this.selectedCustomer.id).subscribe((data) => {
      if (data?.success) {
        this.customerDeleted.emit();
        this.dialogRef.close(true);
      }
    });
  }
}
