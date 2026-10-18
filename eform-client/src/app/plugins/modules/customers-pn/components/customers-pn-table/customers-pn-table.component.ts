import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
  inject
} from '@angular/core';
import {AsyncPipe} from '@angular/common';
import {Store} from '@ngrx/store';
import {TranslatePipe} from '@ngx-translate/core';
import {MatIconButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon';
import {MatMenu, MatMenuItem, MatMenuTrigger} from '@angular/material/menu';
import {Sort} from '@angular/material/sort';
import {MtxGrid, MtxGridColumn} from '@ng-matero/extensions/grid';
import {AuthStateService} from 'src/app/common/store';

import {CustomerPnModel, CustomersPnModel} from '../../models';
import {
  selectCustomersPaginationSort,
  selectCustomersPaginationIsSortDsc,
} from '../../state';
import {CustomersStateService} from '../store';

@Component({
  selector: 'app-customers-pn-table',
  templateUrl: './customers-pn-table.component.html',
  styleUrls: ['./customers-pn-table.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    AsyncPipe,
    TranslatePipe,
    MtxGrid,
    MatIconButton,
    MatIcon,
    MatMenu,
    MatMenuItem,
    MatMenuTrigger
  ]
})
export class CustomersPnTableComponent {
  private store = inject(Store);
  public customersStateService = inject(CustomersStateService);
  public authStateService = inject(AuthStateService);

  @Input() customersModel: CustomersPnModel = new CustomersPnModel();
  @Input() tableHeaders: MtxGridColumn[] = [];

  @Output() showEditCustomerModal = new EventEmitter<CustomerPnModel>();
  @Output() showDeleteCustomerModal = new EventEmitter<CustomerPnModel>();
  @Output() showCopyCustomerModal = new EventEmitter<CustomerPnModel>();
  @Output() sortUpdated = new EventEmitter<void>();

  selectCustomersPaginationSort$ = this.store.select(selectCustomersPaginationSort);
  selectCustomersPaginationIsSortDsc$ = this.store.select(selectCustomersPaginationIsSortDsc);

  sortTable(sort: Sort) {
    this.customersStateService.onSortTable(sort.active);
    this.sortUpdated.emit();
  }

  onShowEditCustomerModal(customer: CustomerPnModel) {
    this.showEditCustomerModal.emit(customer);
  }

  onShowDeleteCustomerModal(customer: CustomerPnModel) {
    this.showDeleteCustomerModal.emit(customer);
  }

  onShowCopyCustomerModal(customer: CustomerPnModel) {
    this.showCopyCustomerModal.emit(customer);
  }
}
