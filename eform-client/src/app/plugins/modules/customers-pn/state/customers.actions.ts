import {createAction} from '@ngrx/store';
import {CommonPaginationState} from 'src/app/common/models';
import {CustomersFiltrationModel} from './customers.reducer';

export const customersUpdateFilters = createAction(
  '[Customers] Update Filters',
  (payload: CustomersFiltrationModel) => ({payload})
);

export const customersUpdatePagination = createAction(
  '[Customers] Update Pagination',
  (payload: CommonPaginationState) => ({payload})
);

export const customersUpdateTotalCustomers = createAction(
  '[Customers] Update Total Customers',
  (payload: number) => ({payload})
);
