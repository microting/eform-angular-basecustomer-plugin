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
    browser.pause(10000);
    customersPage.Navbar.clickonSubMenuItem('Søgbar Lister');
    browser.pause(10000);
    const newSearchListBtn = browser.$('#createEntitySearchBtn');
    const numberOfListsBefore = customersPage.rowNum();
    newSearchListBtn.click();
    browser.pause(8000);
    const listName = 'My testing list';
    const fieldElement = browser.$('#createName');
    fieldElement.addValue(listName);
    const confirmBtn = browser.$('#entitySearchCreateSaveBtn');
    confirmBtn.click();
    browser.pause(8000);
    const numberOfListsAfter = customersPage.rowNum();
    expect(numberOfListsAfter, 'Number of rows is less than expected').greaterThan(numberOfListsBefore);
  });
  it('should configure customers pn to use searchable list', function () {
    const nameOfList = 'My testing list';
    customersPage.goToCustomersPage();
    browser.pause(9000);
    customersPage.settingsCustomerBtn.click();
    browser.pause(3000);
    const searchField = customersSettingsPage.getSearchField();
    searchField.addValue(nameOfList);
    const listChoices = customersSettingsPage.getListOfChoices();
    const choice = listChoices[0];
    browser.pause(8000);
    choice.click();
    const fieldToCheck = customersSettingsPage.selectedListField();
    expect(fieldToCheck.getText(), 'Searchable list is not selected').equal('My testing list');
    browser.pause(4000);
  });
  it('should select only company name, id and customer № for show', function () {
    const customerCheckbox =  customersSettingsPage.getCheckboxById('9');
    const companyNameCheckbox =  customersSettingsPage.getCheckboxById('10');
    const idCheckbox =  customersSettingsPage.getCheckboxById('18');

    if (customerCheckbox.getValue() === 'false') {
      customersSettingsPage.clickCheckboxById('9');
    }
    if (companyNameCheckbox.getValue() === 'false') {
      customersSettingsPage.clickCheckboxById('10');
    }
    if (idCheckbox.getValue() === 'false') {
      customersSettingsPage.clickCheckboxById('18');
    }

    expect(customerCheckbox.getValue(), 'Customer number checkbox is\'t set').equal('true');
    expect(companyNameCheckbox.getValue(), 'Company name checkbox is\'t set').equal('true');
    expect(idCheckbox.getValue(), 'Id checkbox is\'t set').equals('true');
  });
  it ('checks out all the checkboxes', function () {
    for (let i = 1; i < 19; i++) {
      if (i === 9 || i === 10 || i === 18) {
        continue;
      }
      customersSettingsPage.clickCheckboxById(i);
    }
  });
});
