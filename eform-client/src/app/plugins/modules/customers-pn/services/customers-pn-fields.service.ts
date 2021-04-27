import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  OperationDataResult,
  OperationResult,
} from 'src/app/common/models/operation.models';
import { FieldsPnUpdateModel } from '../models';
import { ApiBaseService } from 'src/app/common/services';

export let CustomerPnFieldsMethods = {
  CustomerPnFields: 'api/fields-pn',
};

@Injectable()
export class CustomersPnFieldsService {
  constructor(private apiBaseService: ApiBaseService) {}

  getAllFields(): Observable<OperationDataResult<FieldsPnUpdateModel>> {
    return this.apiBaseService.get(CustomerPnFieldsMethods.CustomerPnFields);
  }

  updateFields(model: FieldsPnUpdateModel): Observable<OperationResult> {
    return this.apiBaseService.put(
      CustomerPnFieldsMethods.CustomerPnFields,
      model
    );
  }
}
