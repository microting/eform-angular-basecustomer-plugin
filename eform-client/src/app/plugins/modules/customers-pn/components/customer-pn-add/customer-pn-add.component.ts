import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {CustomerPnFieldsEnum, CustomersPnFieldStatusEnum} from '../../enums';
import {FieldsPnUpdateModel, CustomerPnFullModel} from '../../models';
import {CustomersPnService} from '../../services';

@Component({
  selector: 'app-customer-pn-add',
  templateUrl: './customer-pn-add.component.html',
  styleUrls: ['./customer-pn-add.component.scss']
})
export class CustomerPnAddComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onCustomerCreated: EventEmitter<void> = new EventEmitter<void>();
  @Input() fieldsModel = new FieldsPnUpdateModel();
  get fieldsEnum() { return CustomerPnFieldsEnum; }
  newCustomerModel: CustomerPnFullModel = new CustomerPnFullModel();
  spinnerStatus = false;

  constructor(private customersService: CustomersPnService) { }

  ngOnInit() {
  }

  show() {
    this.newCustomerModel = new CustomerPnFullModel();
    this.frame.show();
  }

  createCustomer() {
    this.spinnerStatus = true;
    this.customersService.createCustomer(this.newCustomerModel).subscribe(((data) => {
      if (data && data.success) {
        this.newCustomerModel = new CustomerPnFullModel();
        this.onCustomerCreated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
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
