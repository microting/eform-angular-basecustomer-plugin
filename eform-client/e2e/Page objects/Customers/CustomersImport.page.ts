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
    return browser.$$('td > tr').length;
  }

  public chooseFileBtn() {
    return browser.element('#files');
  }


}

const customersImportPage = new CustomersImportPage();
export default customersImportPage;
