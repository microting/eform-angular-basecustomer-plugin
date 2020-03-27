import Page from '../Page';

export class CustomersImportPage extends Page {
  constructor() {
    super();
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
    $('#files').waitForDisplayed(20000);
$('#files').waitForClickable({timeout: 20000});
return $('#files');
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
    // browser.pause(4000);
  }
}

const customersImportPage = new CustomersImportPage();
export default customersImportPage;
