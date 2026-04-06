import {Component, EventEmitter, OnDestroy, OnInit, inject} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {AutoUnsubscribe} from 'ngx-auto-unsubscribe';
import {Subscription} from 'rxjs';

import {CustomerPnFieldsEnum, CustomersPnFieldStatusEnum} from '../../enums';
import {FieldsPnUpdateModel, CustomerPnFullModel} from '../../models';
import {CustomersPnService} from '../../services';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@AutoUnsubscribe()
@Component({
  selector: 'app-customer-pn-edit',
  templateUrl: './customer-pn-edit.component.html',
  styleUrls: ['./customer-pn-edit.component.scss'],
  standalone: false
})
export class CustomerPnEditComponent implements OnInit, OnDestroy {
  private customersService = inject(CustomersPnService);
  private fb = inject(FormBuilder);
  public dialogRef = inject(MatDialogRef<CustomerPnEditComponent>);
  private data = inject<{customerId: number, fields: FieldsPnUpdateModel}>(MAT_DIALOG_DATA);

  fieldsModel = new FieldsPnUpdateModel();
  customerUpdate: EventEmitter<void> = new EventEmitter<void>();
  get fieldsEnum() { return CustomerPnFieldsEnum; }
  customerForm: FormGroup;
  private customerId: number;

  getSingleSub$: Subscription;
  updateSub$: Subscription;

  ngOnInit() {
    this.customerForm = this.fb.group({
      id: [null],
      createdBy: [''],
      customerNo: [''],
      companyName: [''],
      companyAddress: [''],
      companyAddress2: [''],
      zipCode: [''],
      cityName: [''],
      phone: [''],
      email: [''],
      contactPerson: [''],
      description: [''],
      eanCode: [''],
      vatNumber: [''],
      countryCode: [''],
      crmId: [null],
      cadastralNumber: [''],
      propertyNumber: [null],
      apartmentNumber: [null],
      completionYear: [null],
      floorsWithLivingSpace: [null],
      relatedEntityId: [null],
    });

    if (this.data?.fields) {
      this.fieldsModel = this.data.fields;
    }
    if (this.data?.customerId) {
      this.customerId = this.data.customerId;
      this.getSingleCustomer(this.customerId);
    }
  }

  ngOnDestroy(): void {}

  hide() {
    this.dialogRef.close();
  }

  getSingleCustomer(customerId: number) {
    this.getSingleSub$ = this.customersService.getSingleCustomer(customerId).subscribe((data) => {
      if (data?.success) {
        this.customerForm.patchValue(data.model);
      }
    });
  }

  updateCustomer() {
    const model: CustomerPnFullModel = {
      ...new CustomerPnFullModel(),
      ...this.customerForm.value,
    };
    this.updateSub$ = this.customersService.updateCustomer(model).subscribe((data) => {
      if (data?.success) {
        this.customerUpdate.emit();
        this.dialogRef.close(true);
      }
    });
  }

  isFieldAvailable(field: number) {
    const foundField = this.fieldsModel.fields.find(x => x.name === CustomerPnFieldsEnum[field]);
    return foundField?.fieldStatus === CustomersPnFieldStatusEnum.Enabled;
  }
}
