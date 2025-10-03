import {CommonPaginationState} from 'src/app/common/models';
import {Action, createReducer, on} from '@ngrx/store';
import {
  customersUpdateFilters,
  customersUpdatePagination,
  customersUpdateTotalCustomers
} from './customers.actions';

export interface CustomersFiltrationModel {
  nameFilter: string;
}

export interface CustomersState {
  pagination: CommonPaginationState;
  filters: CustomersFiltrationModel;
  total: number;
}

export const customersInitialState: CustomersState = {
  pagination: {
    pageSize: 10,
    sort: 'Id',
    isSortDsc: false,
    offset: 0,
    pageIndex: 0,
    total: 0,
  },
  filters: {
    nameFilter: '',
  },
  total: 0,
};

export const _customersReducer = createReducer(
  customersInitialState,
  on(customersUpdateFilters, (state, {payload}) => ({
    ...state,
    filters: {
      ...state.filters,
      ...payload,
    },
  })),
  on(customersUpdatePagination, (state, {payload}) => ({
    ...state,
    pagination: { ...state, ...payload, },
  })),
  on(customersUpdateTotalCustomers, (state, {payload}) => ({
      ...state,
      pagination: {
        ...state.pagination,
        total: payload,
      },
      total: payload,
    }
  )),
);

export function customersReducer(state: CustomersState | undefined, action: Action) {
  return _customersReducer(state, action);
}
