import { Injectable } from '@angular/core';
import { persistState, Store, StoreConfig } from '@datorama/akita';
import {
  FiltrationStateModel,
  CommonPaginationState,
} from 'src/app/common/models';

export interface CustomersState {
  pagination: CommonPaginationState;
  filters: FiltrationStateModel;
  total: number;
}

export function createInitialState(): CustomersState {
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

const customersPersistStorage = persistState({
  include: ['customers'],
  key: 'customersPn',
  preStorageUpdate(storeName, state) {
    return {
      pagination: state.pagination,
      filters: state.filters,
    };
  },
});

@Injectable({ providedIn: 'root' })
@StoreConfig({ name: 'customers', resettable: true })
export class CustomersStore extends Store<CustomersState> {
  constructor() {
    super(createInitialState());
  }
}

export const customersPersistProvider = {
  provide: 'persistStorage',
  useValue: customersPersistStorage,
  multi: true,
};
