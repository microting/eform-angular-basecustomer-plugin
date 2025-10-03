import {
  CustomersPnState,
} from '../';
import {createSelector} from '@ngrx/store';

const selectCustomersPn =
  (state: {customersPn: CustomersPnState}) => state.customersPn;
export const selectCustomers =
    createSelector(selectCustomersPn, (state) => state.customersState);
export const selectCustomersFilters =
    createSelector(selectCustomers, (state) => state.filters);
export const selectCustomersPagination =
    createSelector(selectCustomers, (state) => state.pagination);
export const selectCustomersPaginationSort =
    createSelector(selectCustomers, (state) => state.pagination.sort);
export const selectCustomersPaginationIsSortDsc =
    createSelector(selectCustomers, (state) => state.pagination.isSortDsc ? 'desc' : 'asc');
export const selectCustomersNameFilters =
    createSelector(selectCustomers, (state) => state.filters.nameFilter);
export const selectCustomersPaginationPageSize =
    createSelector(selectCustomers, (state) => state.pagination.pageSize);
export const selectCustomersTotal =
    createSelector(selectCustomers, (state) => state.total);
