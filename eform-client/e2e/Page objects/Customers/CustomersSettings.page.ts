import Page from '../Page';

export class CustomersSettingsPage extends Page {
  constructor() {
    super();
  }

  public get deleteCustomerBtn() {
    $('#cancelCreateBtn').waitForDisplayed(20000);
$('#cancelCreateBtn').waitForClickable({timeout: 20000});
return $('#cancelCreateBtn');
  }

  public get saveEditBtn() {
    $('#saveEditBtn').waitForDisplayed(20000);
$('#saveEditBtn').waitForClickable({timeout: 20000});
return $('#saveEditBtn');
  }

  public get cancelEditBtn() {
    $('#cancelEditBtn').waitForDisplayed(20000);
$('#cancelEditBtn').waitForClickable({timeout: 20000});
return $('#cancelEditBtn');
  }

  public get saveDeleteBtn() {
    $('#saveDeleteBtn').waitForDisplayed(20000);
$('#saveDeleteBtn').waitForClickable({timeout: 20000});
return $('#saveDeleteBtn');
  }

  public get cancelDeleteBtn() {
    $('#cancelDeleteBtn').waitForDisplayed(20000);
$('#cancelDeleteBtn').waitForClickable({timeout: 20000});
return $('#cancelDeleteBtn');
  }
  public getCheckboxById(id: string) {
    return browser.element('#checkbox' + id);
  }

  public clickCheckboxById(id: string) {
    const el = browser.element('#mat-checkbox' + id);
    el.click();
    // browser.pause(1000);
  }
  public  getSearchField() {
    $('.ng-input > input').waitForDisplayed(20000);
$('.ng-input > input').waitForClickable({timeout: 20000});
return $('.ng-input > input');
  }
  public getListOfChoices() {
    return browser.$$('.ng-option');
  }
  public  selectedListField() {
    return browser.$('.ng-value .ng-value-label');
  }

  public saveSettings() {
    const saveSettingsBtn = browser.$('#saveSettingsBtn');
    saveSettingsBtn.click();
    browser.pause(6000);
  }
}

const customersSettingsPage = new CustomersSettingsPage();
export default customersSettingsPage;
