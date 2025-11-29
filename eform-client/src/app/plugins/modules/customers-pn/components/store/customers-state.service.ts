import {Injectable,
  inject
} from '@angular/core';
import {Observable, tap} from 'rxjs';
import {
  CommonPaginationState,
  OperationDataResult,
  PaginationModel,
  SortModel,
} from 'src/app/common/models';
import {updateTableSort, getOffset} from 'src/app/common/helpers';
import {CustomersPnService} from '../../services';
import {Store} from '@ngrx/store';
import {
  selectCustomersFilters,
  selectCustomersPagination,
  customersUpdateTotalCustomers,
  customersUpdateFilters,
  customersUpdatePagination,
  CustomersFiltrationModel,
  selectCustomersPaginationPageSize,
  selectCustomersNameFilters,
  selectCustomersTotal,
} from '../../state';
import {CustomersPnModel} from '../../models';
import {map} from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class CustomersStateService {
  private store = inject(Store);
  private service = inject(CustomersPnService);

  private selectCustomersFilters$ = this.store.select(selectCustomersFilters);
  private selectCustomersPagination$ = this.store.select(selectCustomersPagination);
  currentPagination: CommonPaginationState;
  currentFilters: CustomersFiltrationModel;

  
  constructor() {
    this.selectCustomersPagination$.subscribe(x => this.currentPagination = x);
    this.selectCustomersFilters$.subscribe(x => this.currentFilters = x);
  }

  getAllCustomers(): Observable<OperationDataResult<CustomersPnModel>> {
    return this.service
      .getAllCustomers({
        ...this.currentFilters,
        ...this.currentPagination,
        sortColumnName: this.currentPagination.sort,
        name: this.currentFilters.nameFilter,
      })
      .pipe(
        tap((response) => {
          if (response && response.success && response.model) {
            this.store.dispatch(customersUpdateTotalCustomers(response.model.total));
          }
        })
      );
  }

  updateNameFilter(nameFilter: string) {
    this.store.dispatch(customersUpdateFilters({nameFilter: nameFilter}));
    this.store.dispatch(customersUpdatePagination({...this.currentPagination, offset: 0}));
  }

  updatePageSize(pageSize: number) {
    this.store.dispatch(customersUpdatePagination({...this.currentPagination, pageSize: pageSize}));
    this.checkOffset();
  }

  getPageSize(): Observable<number> {
    return this.store.select(selectCustomersPaginationPageSize);
  }

  getSort(): Observable<SortModel> {
    return this.selectCustomersPagination$.pipe(
      map(pagination => new SortModel(pagination.sort, pagination.isSortDsc))
    );
  }

  getNameFilter(): Observable<string> {
    return this.store.select(selectCustomersNameFilters);
  }

  changePage(offset: number) {
    this.store.dispatch(customersUpdatePagination({...this.currentPagination, offset: offset}));
  }

  onDelete() {
    const currentTotal = this.currentPagination.total || 0;
    this.store.dispatch(customersUpdateTotalCustomers(currentTotal - 1));
    this.checkOffset();
  }

  onSortTable(sort: string) {
    const localPageSettings = updateTableSort(
      sort,
      this.currentPagination.sort,
      this.currentPagination.isSortDsc
    );
    this.store.dispatch(customersUpdatePagination({...this.currentPagination, ...localPageSettings}));
  }

  checkOffset() {
    const currentTotal = this.currentPagination.total || 0;
    const newOffset = getOffset(
      this.currentPagination.pageSize,
      this.currentPagination.offset,
      currentTotal
    );
    if (newOffset !== this.currentPagination.offset) {
      this.store.dispatch(customersUpdatePagination({...this.currentPagination, offset: newOffset}));
    }
  }

  getPagination(): Observable<PaginationModel> {
    return this.store.select(selectCustomersTotal).pipe(
      map(total => new PaginationModel(
        total,
        this.currentPagination.pageSize,
        this.currentPagination.offset
      ))
    );
  }
}
