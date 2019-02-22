import Page from '../Page';
import {PageWithNavbarPage} from '../PageWithNavbar.page';

export class CustomersPage extends PageWithNavbarPage {
  constructor() {
    super();
  }

  public rowNum(): number {
    return browser.$$('#mainTableBody > tr').length;
  }
  public clickIdSort() {
    browser.$('#IdTableHeader').click();
    browser.pause(4000);
  }
  public clickContactSort() {
    browser.$('#ContactPersonTableHeader').click();
    browser.pause(4000);
  }

  public clickCompanySort() {
    browser.$('#CompanyNameTableHeader').click();
    browser.pause(4000);
  }

  public getCustomerValue(selector: any, row: number) {
    if (selector === 'Id') {
      return  parseInt( $('#mainTableBody').$(`tr:nth-child(${row})`).$('#' + selector).getText(), 10);
    } else {
      return $('#mainTableBody').$(`tr:nth-child(${row})`).$('#' + selector).getText();
    }
  }

  getCustomer(num): CustomersRowObject {
    return new CustomersRowObject(num);
  }

  public get newCustomerBtn() {
    return browser.element('#newCustomerBtn');
  }

  public get customersSettingsBtn() {
    return browser.element('#firstName');
  }

  public get importCustomersSettingsBtn() {
    return browser.element('#lastName');
  }

  // same purpose as previous method?
  public  importCustomerBtn() {
    return browser.element('#importCustomer');
  }

  public  goToImportBtn() {
    this.importCustomerBtn().click();
    browser.pause(4000);
  }

  public get saveImportCustomersBtn() {
    return browser.element('#saveCreateBtn');
  }

  public get cancelImportCustomersBtn() {
    return browser.element('#saveCreateBtn');
  }

  public get deleteCustomerBtn() {
    return browser.element('#cancelCreateBtn');
  }

  public get editCustomerBtn() {
    return browser.element('#editCustomerBtn');
  }

  public get customersButton() {
    return browser.element('#customers-pn');
  }

  public get settingsCustomerBtn() {
    return browser.$('#settingsCustomerBtn');
  }

  public goToCustomerSettings() {
    const elem = browser.$('button .btn .btn-danger');
    elem.click();
    browser.pause(4000);
  }

  public goToCustomersPage() {
    this.customersButton.click();
    browser.pause(20000);
  }

  public get saveDeleteBtn() {
    return browser.element('#customerSaveDeleteBtn');
  }
}

const customersPage = new CustomersPage();
export default customersPage;

export class CustomersRowObject {
  constructor(rowNumber) {
    this.createdBy = $$('#CreatedBy')[rowNumber - 1].getText();
    this.customerNo = $$('#CustomerNo')[rowNumber - 1].getText();
    this.contactPerson = $$('#ContactPerson')[rowNumber - 1].getText();
    this.companyName = $$('#CompanyName')[rowNumber - 1].getText();
    this.companyAddress = $$('#CompanyAddress')[rowNumber - 1].getText();
    this.zipCode = $$('#ZipCode')[rowNumber - 1].getText();
    this.cityName = $$('#CityName')[rowNumber - 1].getText();
    this.email = $$('#Email')[rowNumber - 1].getText();
    this.phone = $$('#Phone')[rowNumber - 1].getText();
    this.editBtn = $$('#editCustomerBtn')[rowNumber - 1];
    this.deleteBtn = $$('#deleteCustomerBtn')[rowNumber - 1];
  }

  public id;
  public version;
  public updatedByUserId;
  public createdBy;
  public customerNo;
  public contactPerson;
  public companyName;
  public companyAddress;
  public zipCode;
  public cityName;
  public email;
  public phone;
  public editBtn;
  public deleteBtn;
}
