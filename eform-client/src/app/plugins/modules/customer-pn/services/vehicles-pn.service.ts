import {Injectable} from '@angular/core';
import {Headers, Http} from '@angular/http';
import 'rxjs/add/operator/map';
import {BaseService} from 'app/services/base.service';

import {Observable} from 'rxjs/Observable';
import {Router} from '@angular/router';
import {VehiclePnModel, VehiclesPnRequestModel} from '../models';

import {OperationDataResult, OperationResult} from 'app/modules/helpers/operation.models';

export let VehiclePnMethods = {
  VehiclePn: 'api/vehicles-pn',
  CreateVehiclePn: 'api/vehicles-pn/create-vehicle',
  UpdateVehiclePn: 'api/vehicles-pn/update-vehicle'
};

@Injectable()
export class VehiclesPnService extends BaseService {
  private headers: Headers;

  constructor(private _http: Http, router: Router) {
    super(_http, router);
    this.headers = new Headers();
    this.headers.append('Content-Type', 'application/json');
    this.headers.append('Accept', 'application/json');
  }

  getAllVehicles(model: VehiclesPnRequestModel): Observable<any> {
    return this.post(VehiclePnMethods.VehiclePn, model);
  }

  updateVehicle(model: VehiclePnModel): Observable<any> {
    return this.postModelOperationResult(VehiclePnMethods.UpdateVehiclePn, model);
  }

  createVehicle(model: VehiclePnModel): Observable<any> {
    return this.postModelOperationResult(VehiclePnMethods.CreateVehiclePn, model);
  }
}
