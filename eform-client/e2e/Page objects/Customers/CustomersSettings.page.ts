import Page from '../Page';

export class CustomersSettingsPage extends Page {
  constructor() {
    super();
  }

  public get deleteCustomerBtn() {
    return browser.element('#cancelCreateBtn');
  }

  public get saveEditBtn() {
    return browser.element('#saveEditBtn');
  }

  public get cancelEditBtn() {
    return browser.element('#cancelEditBtn');
  }

  public get saveDeleteBtn() {
    return browser.element('#saveDeleteBtn');
  }

  public get cancelDeleteBtn() {
    return browser.element('#cancelDeleteBtn');
  }
}

const customersSettingsPage = new CustomersSettingsPage();
export default customersSettingsPage;
