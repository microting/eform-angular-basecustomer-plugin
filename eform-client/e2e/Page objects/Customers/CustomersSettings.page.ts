import Page from '../Page';

export class CustomersSettingsPage extends Page {
  constructor() {
    super();
  }

  public get deleteCustomerBtn() {
    const ele = $('#cancelCreateBtn');
    ele.waitForDisplayed({timeout: 20000});
    ele.waitForClickable({timeout: 20000});
    return ele;
  }

  public get saveEditBtn() {
    const ele = $('#saveEditBtn');
    ele.waitForDisplayed({timeout: 20000});
    ele.waitForClickable({timeout: 20000});
    return ele;
  }

  public get cancelEditBtn() {
    const ele = $('#cancelEditBtn');
    ele.waitForDisplayed({timeout: 20000});
    ele.waitForClickable({timeout: 20000});
    return ele;
  }

  public get saveDeleteBtn() {
    const ele = $('#saveDeleteBtn');
    ele.waitForDisplayed({timeout: 20000});
    ele.waitForClickable({timeout: 20000});
    return ele;
  }

  public get cancelDeleteBtn() {
    const ele = $('#cancelDeleteBtn');
    ele.waitForDisplayed({timeout: 20000});
    ele.waitForClickable({timeout: 20000});
    return ele;
  }
  public getCheckboxById(id: string) {
    const ele = $('#checkbox' + id);
    ele.waitForDisplayed({ timeout: 20000 });
    return ele;
  }

  public clickCheckboxById(id: string) {
    const ele = $('#mat-checkbox' + id);
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    ele.click();
    // browser.pause(1000);
  }
  public  getSearchField() {
    const ele = $('.ng-input > input');
    ele.waitForDisplayed({timeout: 20000});
    ele.waitForClickable({timeout: 20000});
    return ele;
  }
  public getListOfChoices() {
    return browser.$$('.ng-option');
  }
  public  selectedListField() {
    return browser.$('.ng-value .ng-value-label');
  }

  public saveSettings() {
    const saveSettingsBtn = $('#saveSettingsBtn');
    saveSettingsBtn.waitForDisplayed({ timeout: 20000 });
    saveSettingsBtn.waitForClickable({ timeout: 20000 });
    saveSettingsBtn.click();
    //$('.ng-input > input').waitForDisplayed({timeout: 20000});
    //$('.ng-input > input').waitForClickable({timeout: 20000});
  }
}

const customersSettingsPage = new CustomersSettingsPage();
export default customersSettingsPage;
