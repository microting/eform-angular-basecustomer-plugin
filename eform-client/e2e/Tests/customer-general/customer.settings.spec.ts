import loginPage from '../../Page objects/Login.page';
import customersPage, {CustomersRowObject} from '../../Page objects/Customers/Customers.page';
import customersSettingsPage from '../../Page objects/Customers/CustomersSettings.page';

const expect = require('chai').expect;

describe('Customers plugin settings page', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
  });
  it('should create searchable list', function () {
    customersPage.Navbar.advancedDropdown();
    browser.pause(4000);
    customersPage.Navbar.clickonSubMenuItem('Søgbar Lister');
    browser.pause(4000);
    const newSearchListBtn = $('#createEntitySearchBtn');
    const numberOfListsBefore = browser.$$('#tableBody > tr').length;
    newSearchListBtn.click();
    browser.pause(4000);
    const listName = 'My testing list';
    const fieldElement = $('#createName');
    fieldElement.addValue(listName);
    const confirmBtn = $('#entitySearchCreateSaveBtn');
    confirmBtn.click();
    browser.pause(4000);
    const numberOfListsAfter = browser.$$('#tableBody > tr').length;
    expect(numberOfListsAfter, 'Number of rows is less than expected').equal(numberOfListsBefore + 1);
  });
  it('should configure customers pn to use searchable list', function () {
    const nameOfList = 'My testing list';
    customersPage.goToCustomersPage();
    browser.pause(3000);
    const settingsBtn = $('#settingsCustomerBtn');
    settingsBtn.click();
    const searchField = $('.ng-input > input');
    searchField.addValue(nameOfList);
    const listChoices = $$('.ng-option');
    const choice = listChoices[0];
    browser.pause(8000);
    choice.click();
    const fieldToCheck = $('.ng-value .ng-value-label');
    expect(fieldToCheck.getText(), 'Searchable list is selected').equal('My testing list');
  });
  it('should select only company name, id and customer № for show', function () {
    const customerCheckbox =  customersSettingsPage.getCheckboxById('9');
    const companyNameCheckbox =  customersSettingsPage.getCheckboxById('10');
    const idCheckbox =  customersSettingsPage.getCheckboxById('18');

    // rewrite with $(selector).isEnabled()
    if (customerCheckbox.getValue() === 'false') {
      customersSettingsPage.clickOnChecboxById('9');
    }
    if (companyNameCheckbox.getValue() === 'false') {
      customersSettingsPage.clickOnChecboxById('10');
    }
    if (idCheckbox.getValue() === 'false') {
      customersSettingsPage.clickOnChecboxById('18');
    }
    expect(customerCheckbox.getValue(), 'Customer number checkbox is\'t set').equal('true');
    expect(companyNameCheckbox.getValue(), 'Company name checkbox is\'t set').equal('true');
    expect(idCheckbox.getValue(), 'Id checkbox is\'t set').equal('true');
  });
  it ('checks out all the checkboxes', function () {
    for (let i = 1; i < 19; i++) {
      if (i === 9 || i === 10 || i === 18) {
        continue;
      }
      customersSettingsPage.clickOnChecboxById(i);
    }
  });
});
