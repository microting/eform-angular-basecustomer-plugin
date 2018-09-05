import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {CustomersPnService} from '../../services';

@Component({
  selector: 'app-customer-pn-delete',
  templateUrl: './customer-pn-delete.component.html',
  styleUrls: ['./customer-pn-delete.component.scss']
})
export class CustomerPnDeleteComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onCustomerDeleted: EventEmitter<void> = new EventEmitter<void>();
  selectedCustomerId: number;
  spinnerStatus = false;
  constructor(private customersService: CustomersPnService) { }

  ngOnInit() {
  }

  show(customerId: number) {
    this.selectedCustomerId = customerId;
    this.frame.show();
  }

  deleteCustomer() {
    this.spinnerStatus = true;
    this.customersService.deleteCustomer(this.selectedCustomerId).subscribe(((data) => {
      if (data && data.success) {
        this.onCustomerDeleted.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }

}
