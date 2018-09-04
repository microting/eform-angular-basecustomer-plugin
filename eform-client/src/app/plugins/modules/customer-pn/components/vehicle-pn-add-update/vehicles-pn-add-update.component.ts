import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {NotifyPnService} from '../../../shared/services/notify-pn.service';
import {ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';

import {VehiclePnModel} from '../../models';
import {VehiclesPnService} from '../../services';

@Component({
  selector: 'vehicles-pn-add-update-pn-page',
  templateUrl: './vehicles-pn-add-update.component.html'
})
export class VehiclesPnAddUpdateComponent implements OnInit {
  @Input() selectedVehicleModel: VehiclePnModel = new VehiclePnModel();
  @ViewChild('createVehicleModal') createVehicleModal: ModalComponent;
  @ViewChild('editVehicleModal') editVehicleModal: ModalComponent;
  @Output() onVehicleCreatedUpdated: EventEmitter<void> = new EventEmitter<void>();

  newVehicleModel: VehiclePnModel = new VehiclePnModel();

  constructor(private vehiclesService: VehiclesPnService, private notifyService: NotifyPnService) {

  }

  ngOnInit() {

  }

  showCreateVehicleModal() {
    this.createVehicleModal.open();
  }

  showEditVehicleModal() {
    this.editVehicleModal.open();
  }

  updateVehicle() {
    this.vehiclesService.updateVehicle(this.selectedVehicleModel).subscribe(((data) => {
      if (data && data.success) {
        this.editVehicleModal.dismiss();
        this.notifyService.success({text: data.message || 'Error'});
      } else {
        this.notifyService.error({text: data.message || 'Error'});
      }
    }));
  }

  createVehicle() {
    this.vehiclesService.createVehicle(this.newVehicleModel).subscribe(((data) => {
      if (data && data.success) {
        this.newVehicleModel = new VehiclePnModel();
        this.onVehicleCreatedUpdated.emit();
        this.createVehicleModal.dismiss();
        this.notifyService.success({text: data.message || 'Error'});
      } else {
        this.notifyService.error({text: data.message || 'Error'});
      }
    }));
  }

  cancelUpdateVehicle() {
    this.editVehicleModal.dismiss();
    this.onVehicleCreatedUpdated.emit();
  }
}
