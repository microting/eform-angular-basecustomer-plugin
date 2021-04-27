import Page from '../Page';
import { PageWithNavbarPage } from '../PageWithNavbar.page';

export class CustomersPage extends PageWithNavbarPage {
  constructor() {
    super();
  }

  public rowNum(): number {
    return browser.$$('#mainTableBody > tr').length;
  }

  public get IdTableHeader() {
    const ele = $('#IdTableHeader');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get contactPersonTableHeader() {
    const ele = $('#ContactPersonTableHeader');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get companyNameTableHeader() {
    const ele = $('#CompanyNameTableHeader');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public getCustomerValue(selector: any, row: number) {
    if (selector.includes('Id')) {
      return parseInt(
        $('#mainTableBody')
          .$(`tr:nth-child(${row})`)
          .$('#' + selector)
          .getText(),
        10
      );
    } else {
      return $('#mainTableBody')
        .$(`tr:nth-child(${row})`)
        .$('#' + selector)
        .getText();
    }
  }

  getCustomer(num): CustomersRowObject {
    return new CustomersRowObject(num);
  }

  public get newCustomerBtn() {
    $('#newCustomerBtn').waitForDisplayed({ timeout: 20000 });
    $('#newCustomerBtn').waitForClickable({ timeout: 20000 });
    return $('#newCustomerBtn');
  }

  public get customersSettingsBtn() {
    $('#firstName').waitForDisplayed({ timeout: 20000 });
    $('#firstName').waitForClickable({ timeout: 20000 });
    return $('#firstName');
  }

  public get importCustomersSettingsBtn() {
    $('#lastName').waitForDisplayed({ timeout: 20000 });
    $('#lastName').waitForClickable({ timeout: 20000 });
    return $('#lastName');
  }

  // same purpose as previous method?
  public importCustomerBtn() {
    $('#importCustomer').waitForDisplayed({ timeout: 20000 });
    $('#importCustomer').waitForClickable({ timeout: 20000 });
    return $('#importCustomer');
  }

  public goToImportBtn() {
    this.importCustomerBtn().click();
    $('#spinner-animation').waitForDisplayed({ timeout: 20000, reverse: true });
  }

  public get saveImportCustomersBtn() {
    $('#saveCreateBtn').waitForDisplayed({ timeout: 20000 });
    $('#saveCreateBtn').waitForClickable({ timeout: 20000 });
    return $('#saveCreateBtn');
  }

  public get cancelImportCustomersBtn() {
    $('#saveCreateBtn').waitForDisplayed({ timeout: 20000 });
    $('#saveCreateBtn').waitForClickable({ timeout: 20000 });
    return $('#saveCreateBtn');
  }

  public get deleteCustomerBtn() {
    $('#cancelCreateBtn').waitForDisplayed({ timeout: 20000 });
    $('#cancelCreateBtn').waitForClickable({ timeout: 20000 });
    return $('#cancelCreateBtn');
  }

  public get editCustomerBtn() {
    $('#editCustomerBtn').waitForDisplayed({ timeout: 20000 });
    $('#editCustomerBtn').waitForClickable({ timeout: 20000 });
    return $('#editCustomerBtn');
  }

  public get customersButton() {
    $('#customers-pn').waitForDisplayed({ timeout: 20000 });
    $('#customers-pn').waitForClickable({ timeout: 20000 });
    return $('#customers-pn');
  }

  public get settingsCustomerBtn() {
    return browser.$('#settingsCustomerBtn');
  }

  public goToCustomerSettings() {
    const elem = browser.$('button .btn .btn-danger');
    elem.click();
    $('#spinner-animation').waitForDisplayed({ timeout: 20000, reverse: true });
  }

  public goToCustomersPage() {
    this.customersButton.click();
    $('#spinner-animation').waitForDisplayed({ timeout: 20000, reverse: true });
  }

  public get saveDeleteBtn() {
    $('#customerSaveDeleteBtn').waitForDisplayed({ timeout: 20000 });
    $('#customerSaveDeleteBtn').waitForClickable({ timeout: 20000 });
    return $('#customerSaveDeleteBtn');
  }
}

const customersPage = new CustomersPage();
export default customersPage;

export class CustomersRowObject {
  constructor(rowNumber) {
    this.element = $$('#mainTableBody > tr')[rowNumber - 1];
    if (this.element) {
      this.id = +this.element.$('#idCustomer').getText();
      this.createdBy = this.element.$('#createdBy').getText();
      this.customerNo = this.element.$('#customerNo').getText();
      this.contactPerson = this.element.$('#contactPerson').getText();
      this.companyName = this.element.$('#companyName').getText();
      this.companyAddress = this.element.$('#companyAddress').getText();
      this.zipCode = this.element.$('#zipCode').getText();
      this.cityName = this.element.$('#cityName').getText();
      this.email = this.element.$('#email').getText();
      this.phone = this.element.$('#phone').getText();
      this.editBtn = this.element.$('#editCustomerBtn');
      this.copyBtn = this.element.$('#copyCustomerBtn');
      this.deleteBtn = this.element.$('#deleteCustomerBtn');
    }
  }

  public element: WebdriverIO.Element;
  public id: number;
  public version: number;
  public updatedByUserId: number;
  public createdBy: string;
  public customerNo: string;
  public contactPerson: string;
  public companyName: string;
  public companyAddress: string;
  public zipCode: string;
  public cityName: string;
  public email: string;
  public phone: string;
  public editBtn: WebdriverIO.Element;
  public copyBtn: WebdriverIO.Element;
  public deleteBtn: WebdriverIO.Element;
}
