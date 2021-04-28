import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  OperationDataResult,
  OperationResult,
} from 'src/app/common/models/operation.models';
import {
  CustomerPnFullModel,
  CustomersPnImportModel,
  CustomersPnModel,
  CustomersPnRequestModel,
} from '../models';
import { ApiBaseService } from 'src/app/common/services';

export let CustomerPnMethods = {
  CustomerPn: 'api/customers-pn/customers',
};

@Injectable()
export class CustomersPnService {
  constructor(private apiBaseService: ApiBaseService) {}

  getAllCustomers(
    model: CustomersPnRequestModel
  ): Observable<OperationDataResult<CustomersPnModel>> {
    return this.apiBaseService.get(CustomerPnMethods.CustomerPn, model);
  }

  getSingleCustomer(
    customerId: number
  ): Observable<OperationDataResult<CustomerPnFullModel>> {
    return this.apiBaseService.get(
      CustomerPnMethods.CustomerPn + '/' + customerId
    );
  }

  updateCustomer(model: CustomerPnFullModel): Observable<OperationResult> {
    return this.apiBaseService.put(CustomerPnMethods.CustomerPn, model);
  }

  createCustomer(model: CustomerPnFullModel): Observable<OperationResult> {
    return this.apiBaseService.post(CustomerPnMethods.CustomerPn, model);
  }

  deleteCustomer(customerId: number): Observable<OperationResult> {
    return this.apiBaseService.delete(
      CustomerPnMethods.CustomerPn + '/' + customerId
    );
  }

  importCustomer(model: CustomersPnImportModel): Observable<OperationResult> {
    return this.apiBaseService.post(
      CustomerPnMethods.CustomerPn + '/import',
      model
    );
  }
}
