import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { TableHeaderElementModel } from 'src/app/common/models';
import { CustomersPnFieldStatusEnum } from '../../enums';
import {
  CustomerPnFullModel,
  CustomerPnModel,
  CustomersPnModel,
  FieldsPnUpdateModel,
} from '../../models';
import { CustomersPnFieldsService, CustomersPnService } from '../../services';
import { CustomersStateService } from '../store/customers-state-service';

@Component({
  selector: 'app-customers-pn-page',
  templateUrl: './customers-pn-page.component.html',
  styleUrls: ['./customers-pn-page.component.scss'],
})
export class CustomersPnPageComponent implements OnInit {
  @ViewChild('createCustomerModal', { static: false }) createCustomerModal;
  @ViewChild('editCustomerModal', { static: false }) editCustomerModal;
  @ViewChild('deleteCustomerModal', { static: false }) deleteCustomerModal;
  @Output() onCustomerDuplicated: EventEmitter<void> = new EventEmitter<void>();
  get fieldStatusEnum() {
    return CustomersPnFieldStatusEnum;
  }

  customersModel: CustomersPnModel = new CustomersPnModel();
  fieldsModel: FieldsPnUpdateModel = new FieldsPnUpdateModel();
  customerModel: CustomerPnFullModel = new CustomerPnFullModel();

  tableHeaders: TableHeaderElementModel[];

  constructor(
    private customersService: CustomersPnService,
    private customersFieldsService: CustomersPnFieldsService,
    public customersStateService: CustomersStateService
  ) {}

  ngOnInit() {
    this.getAllInitialData();
  }

  getAllInitialData() {
    this.customersFieldsService.getAllFields().subscribe((data) => {
      if (data && data.success) {
        this.fieldsModel = data.model;
        this.addTableHeaders();
        this.getAllCustomers();
      }
    });
  }

  getAllCustomers() {
    this.customersStateService.getAllCustomers().subscribe((result) => {
      if (result && result.success) {
        this.customersModel = result.model;
      }
    });
  }

  duplicateCustomer(customerId: number) {
    this.customersService.getSingleCustomer(customerId).subscribe((data) => {
      if (data && data.success) {
        this.customerModel = data.model;
        this.customerModel.relatedEntityId = null;
        this.customerModel.companyName += '_copy';
        this.customersService
          .createCustomer(this.customerModel)
          .subscribe((result) => {
            if (result && result.success) {
              this.customerModel = new CustomerPnFullModel();
              this.onCustomerDuplicated.emit();
            }
          });
      }
    });
  }

  changePage(offset: number) {
    this.customersStateService.changePage(offset);
    this.getAllCustomers();
  }

  sortTable(sort: string) {
    this.customersStateService.onSortTable(sort);
    this.getAllCustomers();
  }

  showCreateCustomerModal() {
    this.createCustomerModal.show();
  }

  showCopyCustomerModal(model: CustomerPnModel) {
    this.createCustomerModal.showCopy(model.id);
  }

  showEditCustomerModal(model: CustomerPnModel) {
    this.editCustomerModal.show(model.id);
  }

  showDeleteCustomerModal(model: CustomerPnModel) {
    this.deleteCustomerModal.show(model);
  }

  onSearchInputChanged(value: string) {
    this.customersStateService.updateNameFilter(value);
    this.getAllCustomers();
  }

  onCustomerDeleted() {
    this.customersStateService.onDelete();
    this.getAllCustomers();
  }

  private addTableHeaders() {
    this.tableHeaders = [
      this.fieldsModel.fields[0] &&
      this.fieldsModel.fields[0].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: 'Id',
            elementId: 'idTableHeader',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[3] &&
      this.fieldsModel.fields[3].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[3].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[4] &&
      this.fieldsModel.fields[4].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[4].name,
            elementId: 'CompanyNameTableHeader',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[5] &&
      this.fieldsModel.fields[5].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[5].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[6] &&
      this.fieldsModel.fields[6].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[6].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[7] &&
      this.fieldsModel.fields[7].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[7].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[8] &&
      this.fieldsModel.fields[8].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[8].name,
            elementId: '',
            sortable: true,
          }
        : null,

      this.fieldsModel.fields[9] &&
      this.fieldsModel.fields[9].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[9].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[11] &&
      this.fieldsModel.fields[11].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[11].name,
            elementId: 'ContactPersonTableHeader',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[10] &&
      this.fieldsModel.fields[10].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[10].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[16] &&
      this.fieldsModel.fields[16].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[16].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[15] &&
      this.fieldsModel.fields[15].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[15].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[14] &&
      this.fieldsModel.fields[14].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[14].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[17] &&
      this.fieldsModel.fields[17].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[17].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[18] &&
      this.fieldsModel.fields[18].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[18].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[19] &&
      this.fieldsModel.fields[19].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[19].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[20] &&
      this.fieldsModel.fields[20].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[20].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[21] &&
      this.fieldsModel.fields[21].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[21].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[22] &&
      this.fieldsModel.fields[22].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[22].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[23] &&
      this.fieldsModel.fields[23].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[23].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[24] &&
      this.fieldsModel.fields[24].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[24].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[25] &&
      this.fieldsModel.fields[25].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[25].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[26] &&
      this.fieldsModel.fields[26].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[26].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[27] &&
      this.fieldsModel.fields[27].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[27].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[28] &&
      this.fieldsModel.fields[28].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[28].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[29] &&
      this.fieldsModel.fields[29].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[29].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[30] &&
      this.fieldsModel.fields[30].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[30].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[31] &&
      this.fieldsModel.fields[31].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[31].name,
            elementId: '',
            sortable: true,
          }
        : null,
      this.fieldsModel.fields[32] &&
      this.fieldsModel.fields[32].fieldStatus === this.fieldStatusEnum.Enabled
        ? {
            name: this.fieldsModel.fields[32].name,
            elementId: '',
            sortable: true,
          }
        : null,
      { name: 'Actions', elementId: '', sortable: false },
    ];
  }

  onPageSizeChanged(pageSize: number) {
    this.customersStateService.updatePageSize(pageSize);
    this.getAllCustomers();
  }
}
