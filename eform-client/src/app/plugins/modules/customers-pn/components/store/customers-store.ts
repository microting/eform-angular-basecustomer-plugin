import { Injectable } from '@angular/core';
import { persistState, Store, StoreConfig } from '@datorama/akita';
import { CommonPaginationState } from 'src/app/common/models/common-pagination-state';

export interface CustomersState {
  pagination: CommonPaginationState;
}

export function createInitialState(): CustomersState {
  return <CustomersState>{
    pagination: {
      pageSize: 10,
      sort: 'Id',
      isSortDsc: false,
      nameFilter: '',
      offset: 0,
    },
  };
}

const customersPersistStorage = persistState({
  include: ['customersPnСustomers'],
  key: 'pluginsStore',
});

@Injectable({ providedIn: 'root' })
@StoreConfig({ name: 'customersPnСustomers', resettable: true })
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
