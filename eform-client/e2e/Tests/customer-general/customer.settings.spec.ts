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
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});

    const plugin = pluginsPage.getFirstPluginRowObj();
    plugin.settingsBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    const customerCheckbox =  customersSettingsPage.getCheckboxById('CustomerNo');
    const companyNameCheckbox =  customersSettingsPage.getCheckboxById('CompanyName');
    const idCheckbox =  customersSettingsPage.getCheckboxById('Id');

    customersSettingsPage.clickCheckboxById('VatNumber');
    customersSettingsPage.clickCheckboxById('CreatedBy');
    customersSettingsPage.clickCheckboxById('CompanyAddress');
    customersSettingsPage.clickCheckboxById('CompanyAddress2');
    customersSettingsPage.clickCheckboxById('ZipCode');
    customersSettingsPage.clickCheckboxById('CityName');
    customersSettingsPage.clickCheckboxById('Phone');
    customersSettingsPage.clickCheckboxById('Email');
    customersSettingsPage.clickCheckboxById('ContactPerson');
    customersSettingsPage.clickCheckboxById('Description');
    customersSettingsPage.clickCheckboxById('EanCode');
    customersSettingsPage.clickCheckboxById('CountryCode');
    customersSettingsPage.clickCheckboxById('UpdatedByUserId');
    customersSettingsPage.clickCheckboxById('CrmId');
    customersSettingsPage.clickCheckboxById('CadastralNumber');
    customersSettingsPage.clickCheckboxById('PropertyNumber');
    customersSettingsPage.clickCheckboxById('ApartmentNumber');
    customersSettingsPage.clickCheckboxById('CompletionYear');
    customersSettingsPage.clickCheckboxById('FloorsWithLivingSpace');
    customersSettingsPage.clickCheckboxById('CadastralType');
    customersSettingsPage.clickCheckboxById('Version');
    customersSettingsPage.clickCheckboxById('CreatedAt');
    customersSettingsPage.clickCheckboxById('UpdatedAt');
    customersSettingsPage.clickCheckboxById('WorkflowState');
    customersSettingsPage.clickCheckboxById('CreatedByUserId');
    customersSettingsPage.clickCheckboxById('CreatedDate');
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    customersSettingsPage.saveSettings();
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});

    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});

    plugin.settingsBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    expect(customerCheckbox.getValue(), 'Customer number checkbox is\'t set').equal('true');
    expect(companyNameCheckbox.getValue(), 'Company name checkbox is\'t set').equal('true');
    expect(idCheckbox.getValue(), 'Id checkbox is\'t set').equals('true');
    customersSettingsPage.saveSettings();
  });
  it ('checks out all the checkboxes', function () {
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});

    const plugin = pluginsPage.getFirstPluginRowObj();
    plugin.settingsBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    customersSettingsPage.clickCheckboxById('VatNumber');
    customersSettingsPage.clickCheckboxById('CreatedBy');
    customersSettingsPage.clickCheckboxById('CompanyAddress');
    customersSettingsPage.clickCheckboxById('CompanyAddress2');
    customersSettingsPage.clickCheckboxById('ZipCode');
    customersSettingsPage.clickCheckboxById('CityName');
    customersSettingsPage.clickCheckboxById('Phone');
    customersSettingsPage.clickCheckboxById('Email');
    customersSettingsPage.clickCheckboxById('ContactPerson');
    customersSettingsPage.clickCheckboxById('Description');
    customersSettingsPage.clickCheckboxById('EanCode');
    customersSettingsPage.clickCheckboxById('CountryCode');
    customersSettingsPage.clickCheckboxById('UpdatedByUserId');
    customersSettingsPage.clickCheckboxById('CrmId');
    customersSettingsPage.clickCheckboxById('CadastralNumber');
    customersSettingsPage.clickCheckboxById('PropertyNumber');
    customersSettingsPage.clickCheckboxById('ApartmentNumber');
    customersSettingsPage.clickCheckboxById('CompletionYear');
    customersSettingsPage.clickCheckboxById('FloorsWithLivingSpace');
    customersSettingsPage.clickCheckboxById('CadastralType');
    customersSettingsPage.clickCheckboxById('Version');
    customersSettingsPage.clickCheckboxById('CreatedAt');
    customersSettingsPage.clickCheckboxById('UpdatedAt');
    customersSettingsPage.clickCheckboxById('WorkflowState');
    customersSettingsPage.clickCheckboxById('CreatedByUserId');
    customersSettingsPage.clickCheckboxById('CreatedDate');
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    customersSettingsPage.saveSettings();
  });
  it('should deactivate unnecessary fields', function () {
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});

    const plugin = pluginsPage.getFirstPluginRowObj();
    plugin.settingsBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    // customersSettingsPage.clickCheckboxById('VatNumber');
    // customersSettingsPage.clickCheckboxById('CreatedBy');
    customersSettingsPage.clickCheckboxById('Version');
    customersSettingsPage.clickCheckboxById('CreatedAt');
    customersSettingsPage.clickCheckboxById('UpdatedAt');
    customersSettingsPage.clickCheckboxById('UpdatedByUserId');
    customersSettingsPage.clickCheckboxById('WorkflowState');
    customersSettingsPage.clickCheckboxById('CreatedByUserId');
    customersSettingsPage.clickCheckboxById('CreatedDate');

    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    customersSettingsPage.saveSettings();
  });
});
