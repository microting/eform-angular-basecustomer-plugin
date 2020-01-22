import loginPage from '../../Page objects/Login.page';
import customersPage, {CustomersRowObject} from '../../Page objects/Customers/Customers.page';
import customersSettingsPage from '../../Page objects/Customers/CustomersSettings.page';
import myEformsPage from '../../Page objects/MyEforms.page';
import pluginsPage from '../customer-settings/application-settings.plugins.page';


const expect = require('chai').expect;

describe('Customers plugin settings page', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
  });
  // it('should create searchable list', function () {
  //   customersPage.Navbar.advancedDropdown();
  //   browser.pause(10000);
  //   customersPage.Navbar.clickonSubMenuItem('Søgbar Lister');
  //   browser.pause(10000);
  //   const newSearchListBtn = browser.$('#createEntitySearchBtn');
  //   const numberOfListsBefore = browser.$$('#tableBody > tr').length;
  //   newSearchListBtn.click();
  //   browser.pause(8000);
  //   const listName = 'My testing list';
  //   const fieldElement = browser.$('#createName');
  //   fieldElement.addValue(listName);
  //   const confirmBtn = browser.$('#entitySearchCreateSaveBtn');
  //   confirmBtn.click();
  //   browser.pause(8000);
  //   const numberOfListsAfter = browser.$$('#tableBody > tr').length;
  //   expect(numberOfListsAfter, 'Number of rows is less than expected').equal(numberOfListsBefore + 1);
  // });
  // it('should configure customers pn to use searchable list', function () {
  //   const nameOfList = 'My testing list';
  //   customersPage.goToCustomersPage();
  //   browser.pause(9000);
  //   customersPage.settingsCustomerBtn.click();
  //   browser.pause(3000);
  //   const searchField = customersSettingsPage.getSearchField();
  //   searchField.addValue(nameOfList);
  //   const listChoices = customersSettingsPage.getListOfChoices();
  //   const choice = listChoices[0];
  //   browser.pause(8000);
  //   choice.click();
  //   const fieldToCheck = customersSettingsPage.selectedListField();
  //   expect(fieldToCheck.getText(), 'Searchable list is not selected').equal('My testing list');
  // });
  it('should select only company name, id and customer no for show', function () {
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    browser.pause(4000);

    const plugin = pluginsPage.getFirstPluginRowObj();
    plugin.settingsBtn.click();
    browser.pause(4000);
    const customerCheckbox =  customersSettingsPage.getCheckboxById('3');
    const companyNameCheckbox =  customersSettingsPage.getCheckboxById('4');
    const idCheckbox =  customersSettingsPage.getCheckboxById('22');

    customersSettingsPage.clickCheckboxById('1');
    customersSettingsPage.clickCheckboxById('2');
    customersSettingsPage.clickCheckboxById('5');
    customersSettingsPage.clickCheckboxById('6');
    customersSettingsPage.clickCheckboxById('7');
    customersSettingsPage.clickCheckboxById('8');
    customersSettingsPage.clickCheckboxById('9');
    customersSettingsPage.clickCheckboxById('10');
    customersSettingsPage.clickCheckboxById('11');
    customersSettingsPage.clickCheckboxById('12');
    customersSettingsPage.clickCheckboxById('13');
    customersSettingsPage.clickCheckboxById('14');
    customersSettingsPage.clickCheckboxById('15');
    customersSettingsPage.clickCheckboxById('16');
    customersSettingsPage.clickCheckboxById('17');
    customersSettingsPage.clickCheckboxById('18');
    customersSettingsPage.clickCheckboxById('19');
    customersSettingsPage.clickCheckboxById('20');
    customersSettingsPage.clickCheckboxById('21');
    customersSettingsPage.clickCheckboxById('23');
    customersSettingsPage.clickCheckboxById('24');
    customersSettingsPage.clickCheckboxById('25');
    customersSettingsPage.clickCheckboxById('26');
    customersSettingsPage.clickCheckboxById('27');
    browser.pause(2000);
    customersSettingsPage.saveSettings();
    //browser.pause(4000);
    //customersPage.goToCustomersPage();
    browser.pause(4000);

    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    browser.pause(4000);

    plugin.settingsBtn.click();
    browser.pause(4000);
    expect(customerCheckbox.getValue(), 'Customer number checkbox is\'t set').equal('true');
    expect(companyNameCheckbox.getValue(), 'Company name checkbox is\'t set').equal('true');
    expect(idCheckbox.getValue(), 'Id checkbox is\'t set').equals('true');
    customersSettingsPage.saveSettings();
  });
  it ('checks out all the checkboxes', function () {
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    browser.pause(4000);

    const plugin = pluginsPage.getFirstPluginRowObj();
    plugin.settingsBtn.click();
    browser.pause(4000);
    for (let i = 1; i < 23; i++) {
      if (customersSettingsPage.getCheckboxById(i).getValue() === 'true') {
        continue;
      } else {
        customersSettingsPage.clickCheckboxById(i);
      }
    }
    browser.pause(2000);
    customersSettingsPage.saveSettings();
  });
  it('should deactivate unnecessary fields', function () {
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    browser.pause(4000);

    const plugin = pluginsPage.getFirstPluginRowObj();
    plugin.settingsBtn.click();
    browser.pause(10000);
    customersSettingsPage.clickCheckboxById('17');
    customersSettingsPage.clickCheckboxById('18');
    customersSettingsPage.clickCheckboxById('19');
    customersSettingsPage.clickCheckboxById('20');
    customersSettingsPage.clickCheckboxById('21');
    customersSettingsPage.clickCheckboxById('1');

    browser.pause(2000);
    customersSettingsPage.saveSettings();
  });
});
