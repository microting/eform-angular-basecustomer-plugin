import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {
  OperationDataResult,
  PaginationModel,
  SortModel,
  FiltrationStateModel,
  CommonPaginationState,
} from 'src/app/common/models';
import { updateTableSort, getOffset } from 'src/app/common/helpers';
import { map } from 'rxjs/operators';
import { CustomersPnService } from '../../services';
import { CustomersPnModel } from '../../models';

export interface CustomersState {
  pagination: CommonPaginationState;
  filters: FiltrationStateModel;
  total: number;
}

function createInitialState(): CustomersState {
  return <CustomersState>{
    pagination: {
      pageSize: 10,
      sort: 'Id',
      isSortDsc: false,
      offset: 0,
    },
    filters: {
      nameFilter: '',
    },
    total: 0,
  };
}

@Injectable({ providedIn: 'root' })
export class CustomersStateService {
  private state$ = new BehaviorSubject<CustomersState>(createInitialState());

  constructor(
    private service: CustomersPnService
  ) {}

  private get state(): CustomersState {
    return this.state$.value;
  }

  private updateState(update: Partial<CustomersState>) {
    this.state$.next({ ...this.state, ...update });
  }

  getAllCustomers(): Observable<OperationDataResult<CustomersPnModel>> {
    return this.service
      .getAllCustomers({
        ...this.state.pagination,
        ...this.state.filters,
        sortColumnName: this.state.pagination.sort,
        name: this.state.filters.nameFilter,
      })
      .pipe(
        map((response) => {
          if (response && response.success && response.model) {
            this.updateState({ total: response.model.total });
          }
          return response;
        })
      );
  }

  updateNameFilter(nameFilter: string) {
    this.updateState({
      filters: {
        ...this.state.filters,
        nameFilter: nameFilter,
      },
      pagination: {
        ...this.state.pagination,
        offset: 0,
      },
    });
  }

  updatePageSize(pageSize: number) {
    this.updateState({
      pagination: {
        ...this.state.pagination,
        pageSize: pageSize,
      },
    });
    this.checkOffset();
  }

  getPageSize(): Observable<number> {
    return this.state$.pipe(map(state => state.pagination.pageSize));
  }

  getSort(): Observable<SortModel> {
    return this.state$.pipe(
      map(state => new SortModel(state.pagination.sort, state.pagination.isSortDsc))
    );
  }

  getNameFilter(): Observable<string> {
    return this.state$.pipe(map(state => state.filters.nameFilter));
  }

  changePage(offset: number) {
    this.updateState({
      pagination: {
        ...this.state.pagination,
        offset: offset,
      },
    });
  }

  onDelete() {
    this.updateState({ total: this.state.total - 1 });
    this.checkOffset();
  }

  onSortTable(sort: string) {
    const localPageSettings = updateTableSort(
      sort,
      this.state.pagination.sort,
      this.state.pagination.isSortDsc
    );
    this.updateState({
      pagination: {
        ...this.state.pagination,
        isSortDsc: localPageSettings.isSortDsc,
        sort: localPageSettings.sort,
      },
    });
  }

  checkOffset() {
    const newOffset = getOffset(
      this.state.pagination.pageSize,
      this.state.pagination.offset,
      this.state.total
    );
    if (newOffset !== this.state.pagination.offset) {
      this.updateState({
        pagination: {
          ...this.state.pagination,
          offset: newOffset,
        },
      });
    }
  }

  getPagination(): Observable<PaginationModel> {
    return this.state$.pipe(
      map(state => new PaginationModel(
        state.total,
        state.pagination.pageSize,
        state.pagination.offset
      ))
    );
  }
}
