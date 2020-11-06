import loginPage from '../../Page objects/Login.page';
import customersPage, {CustomersRowObject} from '../../Page objects/Customers/Customers.page';
import customersSettingsPage from '../../Page objects/Customers/CustomersSettings.page';
import myEformsPage from '../../Page objects/MyEforms.page';
import pluginsPage from '../customer-settings/application-settings.plugins.page';


const expect = require('chai').expect;

describe('Customers plugin settings page', function () {
  before(function () {
    loginPage.open('/auth');
  });
  it('should select only company name, id and customer no for show', function () {
    loginPage.login();
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

    loginPage.open('/');
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
