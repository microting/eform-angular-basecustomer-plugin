import {
  Component,
  OnDestroy,
  OnInit,
  inject
} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {Overlay} from '@angular/cdk/overlay';
import {Store} from '@ngrx/store';
import {Subscription, Subject} from 'rxjs';
import {debounceTime} from 'rxjs/operators';
import {AutoUnsubscribe} from 'ngx-auto-unsubscribe';
import {MtxGridColumn} from '@ng-matero/extensions/grid';

import {CustomersPnFieldStatusEnum} from '../../enums';
import {
  CustomerPnModel,
  CustomersPnModel,
  FieldsPnUpdateModel,
} from '../../models';
import {CustomersPnFieldsService} from '../../services';
import {CustomersStateService} from '../store';
import {selectCustomersNameFilters} from '../../state';
import {CustomerPnAddComponent} from '../customer-pn-add/customer-pn-add.component';
import {CustomerPnEditComponent} from '../customer-pn-edit/customer-pn-edit.component';
import {CustomerPnDeleteComponent} from '../customer-pn-delete/customer-pn-delete.component';
import {dialogConfigHelper} from 'src/app/common/helpers';

@AutoUnsubscribe()
@Component({
  selector: 'app-customers-pn-container',
  templateUrl: './customers-pn-container.component.html',
  styleUrls: ['./customers-pn-container.component.scss'],
  standalone: false
})
export class CustomersPnContainerComponent implements OnInit, OnDestroy {
  private customersFieldsService = inject(CustomersPnFieldsService);
  public customersStateService = inject(CustomersStateService);
  private dialog = inject(MatDialog);
  private overlay = inject(Overlay);
  private store = inject(Store);

  customersModel: CustomersPnModel = new CustomersPnModel();
  fieldsModel: FieldsPnUpdateModel = new FieldsPnUpdateModel();
  tableHeaders: MtxGridColumn[] = [];

  selectCustomersNameFilters$ = this.store.select(selectCustomersNameFilters);
  nameSearchSubject = new Subject<string>();

  getAllFieldsSub$: Subscription;
  getAllCustomersSub$: Subscription;
  createModalSub$: Subscription;
  editModalSub$: Subscription;
  deleteModalSub$: Subscription;
  copyModalSub$: Subscription;
  nameSearchSub$: Subscription;

  ngOnInit() {
    this.nameSearchSub$ = this.nameSearchSubject
      .pipe(debounceTime(400))
      .subscribe(value => {
        this.customersStateService.updateNameFilter(value);
        this.getAllCustomers();
      });
    this.getAllInitialData();
  }

  ngOnDestroy(): void {}

  getAllInitialData() {
    this.getAllFieldsSub$ = this.customersFieldsService.getAllFields().subscribe((data) => {
      if (data && data.success) {
        this.fieldsModel = data.model;
        this.tableHeaders = this.buildTableHeaders();
        this.getAllCustomers();
      }
    });
  }

  getAllCustomers() {
    this.getAllCustomersSub$ = this.customersStateService.getAllCustomers().subscribe((result) => {
      if (result && result.success) {
        this.customersModel = result.model;
      }
    });
  }

  changePage(offset: number) {
    this.customersStateService.changePage(offset);
    this.getAllCustomers();
  }

  onSearchInputChanged(value: string) {
    this.nameSearchSubject.next(value);
  }

  onPageSizeChanged(pageSize: number) {
    this.customersStateService.updatePageSize(pageSize);
    this.getAllCustomers();
  }

  showCreateCustomerModal() {
    const modal = this.dialog.open(CustomerPnAddComponent, {
      ...dialogConfigHelper(this.overlay, {fields: this.fieldsModel}),
      minWidth: 600
    });
    this.createModalSub$ = modal.componentInstance.customerCreated.subscribe(() => {
      this.getAllCustomers();
    });
  }

  showCopyCustomerModal(model: CustomerPnModel) {
    const modal = this.dialog.open(CustomerPnAddComponent, {
      ...dialogConfigHelper(this.overlay, {customerId: model.id, fields: this.fieldsModel}),
      minWidth: 600
    });
    this.copyModalSub$ = modal.componentInstance.customerCreated.subscribe(() => {
      this.getAllCustomers();
    });
  }

  showEditCustomerModal(model: CustomerPnModel) {
    const modal = this.dialog.open(CustomerPnEditComponent, {
      ...dialogConfigHelper(this.overlay, {customerId: model.id, fields: this.fieldsModel}),
      minWidth: 600
    });
    this.editModalSub$ = modal.componentInstance.customerUpdate.subscribe(() => {
      this.getAllCustomers();
    });
  }

  showDeleteCustomerModal(model: CustomerPnModel) {
    const modal = this.dialog.open(CustomerPnDeleteComponent, {
      ...dialogConfigHelper(this.overlay, {customer: model, fields: this.fieldsModel})
    });
    this.deleteModalSub$ = modal.componentInstance.customerDeleted.subscribe(() => {
      this.customersStateService.onDelete();
      this.getAllCustomers();
    });
  }

  private buildTableHeaders(): MtxGridColumn[] {
    const fieldIndices = [0, 3, 4, 5, 6, 7, 8, 9, 11, 10, 16, 15, 14, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32];
    const toCamelCase = (s: string) => s.charAt(0).toLowerCase() + s.slice(1);
    const headers: MtxGridColumn[] = fieldIndices
      .filter(i => this.fieldsModel.fields[i]?.fieldStatus === CustomersPnFieldStatusEnum.Enabled)
      .map(i => ({
        header: this.fieldsModel.fields[i].name,
        field: toCamelCase(this.fieldsModel.fields[i].name),
        sortable: true,
      }));
    headers.push({header: 'Actions', field: 'actions', pinned: 'right', sortable: false});
    return headers;
  }
}
