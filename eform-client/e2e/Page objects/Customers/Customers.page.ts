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
    if (selector.includes('Id')) {
      return  parseInt( $('#mainTableBody').$(`tr:nth-child(${row})`).$('#' + selector).getText(), 10);
    } else {
      return $('#mainTableBody').$(`tr:nth-child(${row})`).$('#' + selector).getText();
    }
  }

  getCustomer(num): CustomersRowObject {
    return new CustomersRowObject(num);
  }

  public get newCustomerBtn() {
    $('#newCustomerBtn').waitForDisplayed(20000);
$('#newCustomerBtn').waitForClickable({timeout: 20000});
return $('#newCustomerBtn');
  }

  public get customersSettingsBtn() {
    $('#firstName').waitForDisplayed(20000);
$('#firstName').waitForClickable({timeout: 20000});
return $('#firstName');
  }

  public get importCustomersSettingsBtn() {
    $('#lastName').waitForDisplayed(20000);
$('#lastName').waitForClickable({timeout: 20000});
return $('#lastName');
  }

  // same purpose as previous method?
  public  importCustomerBtn() {
    $('#importCustomer').waitForDisplayed(20000);
$('#importCustomer').waitForClickable({timeout: 20000});
return $('#importCustomer');
  }

  public  goToImportBtn() {
    this.importCustomerBtn().click();
    browser.pause(4000);
  }

  public get saveImportCustomersBtn() {
    $('#saveCreateBtn').waitForDisplayed(20000);
$('#saveCreateBtn').waitForClickable({timeout: 20000});
return $('#saveCreateBtn');
  }

  public get cancelImportCustomersBtn() {
    $('#saveCreateBtn').waitForDisplayed(20000);
$('#saveCreateBtn').waitForClickable({timeout: 20000});
return $('#saveCreateBtn');
  }

  public get deleteCustomerBtn() {
    $('#cancelCreateBtn').waitForDisplayed(20000);
$('#cancelCreateBtn').waitForClickable({timeout: 20000});
return $('#cancelCreateBtn');
  }

  public get editCustomerBtn() {
    $('#editCustomerBtn').waitForDisplayed(20000);
$('#editCustomerBtn').waitForClickable({timeout: 20000});
return $('#editCustomerBtn');
  }

  public get customersButton() {
    $('#customers-pn').waitForDisplayed(20000);
$('#customers-pn').waitForClickable({timeout: 20000});
return $('#customers-pn');
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
    $('#customerSaveDeleteBtn').waitForDisplayed(20000);
$('#customerSaveDeleteBtn').waitForClickable({timeout: 20000});
return $('#customerSaveDeleteBtn');
  }
}

const customersPage = new CustomersPage();
export default customersPage;

export class CustomersRowObject {
  constructor(rowNumber) {
    this.createdBy = $$('#CreatedBy_' + (rowNumber - 1))[0].getText();
    this.customerNo = $$('#CustomerNo_' + (rowNumber - 1))[0].getText();
    this.contactPerson = $$('#ContactPerson_' + (rowNumber - 1))[0].getText();
    this.companyName = $$('#CompanyName_' + (rowNumber - 1))[0].getText();
    this.companyAddress = $$('#CompanyAddress_' + (rowNumber - 1))[0].getText();
    this.zipCode = $$('#ZipCode_' + (rowNumber - 1))[0].getText();
    this.cityName = $$('#CityName_' + (rowNumber - 1))[0].getText();
    this.email = $$('#Email_' + (rowNumber - 1))[0].getText();
    this.phone = $$('#Phone_' + (rowNumber - 1))[0].getText();
    this.editBtn = $$('#editCustomerBtn_' + (rowNumber - 1))[0];
    this.copyBtn = $$('#copyCustomerBtn_' + (rowNumber - 1))[0];
    this.deleteBtn = $$('#deleteCustomerBtn_' + (rowNumber - 1))[0];
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
  public copyBtn;
  public deleteBtn;
}
