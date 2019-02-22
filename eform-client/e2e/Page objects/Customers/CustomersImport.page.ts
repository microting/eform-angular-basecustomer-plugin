import Page from '../Page';

export class CustomersImportPage extends Page {
  constructor() {
    super();
  }

  public get saveImportCustomersBtn() {
    return browser.element('#saveCreateBtn');
  }

  public get cancelImportCustomersBtn() {
    return browser.element('#saveCreateBtn');
  }

  public continueImport() {
    browser.element('#continueImportBtn').click();
    browser.pause(8000);
  }

  public cancelImport() {
    browser.element('#cancelImportBtn').click();
    browser.pause(8000);
  }

  public get numberOfCustomers(): Int {
    return browser.$$('#customersToImport > tr').length;
  }

  public chooseFileBtn() {
    return browser.element('#files');
  }
  public getField(index: number) {
    const fieldsList = browser.$$('thead > tr > th');
    return fieldsList[index];
  }

  public chooseFromDropdown(fieldName: string, fieldNumber: number) {
    const field = this.getField(fieldNumber);
    field.click();
    // ng-dropdown-panel
    // const choicesList = browser.$$('.ng-dropdown-panel-items');
    browser.$('span=' + fieldName).click();
    browser.pause(4000);
  }
}

const customersImportPage = new CustomersImportPage();
export default customersImportPage;
