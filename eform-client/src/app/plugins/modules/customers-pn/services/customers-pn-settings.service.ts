import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  OperationDataResult,
  OperationResult,
} from 'src/app/common/models/operation.models';
import { CustomersPnSettingsModel } from '../models';
import { ApiBaseService } from 'src/app/common/services';

export let CustomerPnSettingsMethods = {
  CustomersPnSettings: 'api/customers-pn/settings',
};

@Injectable()
export class CustomersPnSettingsService {
  constructor(private apiBaseService: ApiBaseService) {}

  getAllSettings(): Observable<OperationDataResult<CustomersPnSettingsModel>> {
    return this.apiBaseService.get(
      CustomerPnSettingsMethods.CustomersPnSettings
    );
  }

  updateSettings(model: CustomersPnSettingsModel): Observable<OperationResult> {
    return this.apiBaseService.post(
      CustomerPnSettingsMethods.CustomersPnSettings,
      model
    );
  }
}
