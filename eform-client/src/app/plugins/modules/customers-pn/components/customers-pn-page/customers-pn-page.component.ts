import {Component, OnInit, ViewChild} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {UserInfoModel} from 'src/app/common/models';
import {AuthService, LocaleService} from 'src/app/common/services/auth';
import {CustomersPnFieldStatusEnum, CustomerPnFieldsEnum} from '../../enums';
import {CustomerPnModel, CustomersPnModel, CustomersPnRequestModel, FieldsPnUpdateModel} from '../../models';
import {CustomersPnFieldsService, CustomersPnService} from '../../services';

declare var require: any;

@Component({
  selector: 'app-customers-pn-page',
  templateUrl: './customers-pn-page.component.html',
  styleUrls: ['./customers-pn-page.component.scss']
})
export class CustomersPnPageComponent implements OnInit {
  @ViewChild('createCustomerModal') createCustomerModal;
  @ViewChild('editCustomerModal') editCustomerModal;
  @ViewChild('deleteCustomerModal') deleteCustomerModal;
  get fieldStatusEnum() { return CustomersPnFieldStatusEnum; }
  get fieldStatusEnumString() { return CustomersPnFieldStatusEnum; }
  get fieldsEnum() { return CustomerPnFieldsEnum; }


  customersRequestModel: CustomersPnRequestModel = new CustomersPnRequestModel();
  customersModel: CustomersPnModel = new CustomersPnModel();
  fieldsModel: FieldsPnUpdateModel = new FieldsPnUpdateModel();

  spinnerStatus = false;
  constructor(private customersService: CustomersPnService,
              private customersFieldsService: CustomersPnFieldsService,
              private translateService: TranslateService,
              private localeService: LocaleService,
              private authService: AuthService) {

  }

  get currentRole(): string {
    return this.authService.currentRole;
  }

  ngOnInit() {
    this.setTranslation();
    this.getAllInitialData();
  }

  setTranslation() {
    const lang = this.localeService.getCurrentUserLocale();
    const i18n = require(`../../i18n/${lang}.json`);
    this.translateService.setTranslation(lang, i18n, true);
  }

  showCreateCustomerModal() {
    this.createCustomerModal.show();
  }

  showEditCustomerModal(model: CustomerPnModel) {
    this.editCustomerModal.show(model.id);
  }

  showDeleteCustomerModal(model: CustomerPnModel) {
    this.deleteCustomerModal.show(model.id);
  }

  getAllInitialData() {
    this.spinnerStatus = true;
    this.customersFieldsService.getAllFields().subscribe((data) => {
      if (data && data.success) {
        this.fieldsModel = data.model;
        this.customersService.getAllCustomers(this.customersRequestModel).subscribe((result => {
          if (result && result.success) {
            this.customersModel = result.model;
          }
          this.spinnerStatus = false;
        }));
      } this.spinnerStatus = false;
    });
  }

  getAllCustomers() {
    this.customersService.getAllCustomers(this.customersRequestModel).subscribe((result => {
      if (result && result.success) {
        this.customersModel = result.model;
      }
      this.spinnerStatus = false;
    }));
  }

  changePage(e: any) {
    if (e || e === 0) {
      this.customersRequestModel.offset = e;
      if (e === 0) {
        this.customersRequestModel.pageIndex = 0;
      } else {
        this.customersRequestModel.pageIndex = Math.floor(e / this.customersRequestModel.pageSize);
      }
      this.getAllCustomers();
    }
  }

  sortByColumn(columnName: string, sortedByDsc: boolean) {
    this.customersRequestModel.sortColumnName = columnName;
    this.customersRequestModel.isSortDsc = sortedByDsc;
    this.getAllCustomers();
  }


}
