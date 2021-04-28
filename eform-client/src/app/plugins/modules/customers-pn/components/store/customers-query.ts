import { Injectable } from '@angular/core';
import { Query } from '@datorama/akita';
import { CustomersState, CustomersStore } from './customers-store';

@Injectable({ providedIn: 'root' })
export class CustomersQuery extends Query<CustomersState> {
  constructor(protected store: CustomersStore) {
    super(store);
  }

  get pageSetting() {
    return this.getValue();
  }

  selectNameFilter$ = this.select((state) => state.pagination.nameFilter);
  selectPageSize$ = this.select((state) => state.pagination.pageSize);
  selectIsSortDsc$ = this.select((state) => state.pagination.isSortDsc);
  selectSort$ = this.select((state) => state.pagination.sort);
  selectOffset$ = this.select((state) => state.pagination.offset);
}
