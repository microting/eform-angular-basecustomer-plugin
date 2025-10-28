import loginPage from '../../Page objects/Login.page';
import customersSettingsPage from '../../Page objects/Customers/CustomersSettings.page';
import myEformsPage from '../../Page objects/MyEforms.page';
import pluginPage from '../../Page objects/Plugin.page';

const expect = require('chai').expect;

describe('Customers plugin settings page', function () {
  before(function () {
    loginPage.open('/auth');
  });
  it('should select only company name, id and customer no for show', function () {
    loginPage.login();
    myEformsPage.Navbar.goToPluginsPage();
    const spinnerAnimation = $('#spinner-animation');
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });

    const plugin = pluginPage.getFirstPluginRowObj();
    const settingsBtn = plugin.settingsBtn;
    settingsBtn.waitForDisplayed({ timeout: 20000 });
    settingsBtn.waitForClickable({ timeout: 20000 });
    settingsBtn.click();
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });
    const customerCheckbox = customersSettingsPage.getCheckboxById(
      'CustomerNo'
    );
    const companyNameCheckbox = customersSettingsPage.getCheckboxById(
      'CompanyName'
    );
    const idCheckbox = customersSettingsPage.getCheckboxById('Id');

    const checkboxIds = [
      'VatNumber',
      'CreatedBy',
      'CompanyAddress',
      'CompanyAddress2',
      'ZipCode',
      'CityName',
      'Phone',
      'Email',
      'ContactPerson',
      'Description',
      'EanCode',
      'CountryCode',
      'UpdatedByUserId',
      'CrmId',
      'CadastralNumber',
      'PropertyNumber',
      'ApartmentNumber',
      'CompletionYear',
      'FloorsWithLivingSpace',
      'CadastralType',
      'Version',
      'CreatedAt',
      'UpdatedAt',
      'WorkflowState',
      'CreatedByUserId',
      'CreatedDate',
    ];
    for (let i = 0; i < checkboxIds.length; i++) {
      customersSettingsPage.clickCheckboxById(checkboxIds[i]);
    }

    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });
    customersSettingsPage.saveSettings();
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });

    loginPage.open('/');
    myEformsPage.Navbar.goToPluginsPage();
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });

    const settingsBtn2 = plugin.settingsBtn;
    settingsBtn2.waitForDisplayed({ timeout: 20000 });
    settingsBtn2.waitForClickable({ timeout: 20000 });
    settingsBtn2.click();
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });
    expect(
      customerCheckbox.getValue(),
      `Customer number checkbox is't set`
    ).equal('true');
    expect(
      companyNameCheckbox.getValue(),
      `Company name checkbox is't set`
    ).equal('true');
    expect(idCheckbox.getValue(), `Id checkbox is't set`).equals('true');
    customersSettingsPage.saveSettings();
  });
  it('checks out all the checkboxes', function () {
    loginPage.open('/');
    myEformsPage.Navbar.goToPluginsPage();
    const spinnerAnimation = $('#spinner-animation');
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });

    const plugin = pluginPage.getFirstPluginRowObj();
    const settingsBtn = plugin.settingsBtn;
    settingsBtn.waitForDisplayed({ timeout: 20000 });
    settingsBtn.waitForClickable({ timeout: 20000 });
    settingsBtn.click();
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });

    const checkboxIds = [
      'VatNumber',
      'CreatedBy',
      'CompanyAddress',
      'CompanyAddress2',
      'ZipCode',
      'CityName',
      'Phone',
      'Email',
      'ContactPerson',
      'Description',
      'EanCode',
      'CountryCode',
      'UpdatedByUserId',
      'CrmId',
      'CadastralNumber',
      'PropertyNumber',
      'ApartmentNumber',
      'CompletionYear',
      'FloorsWithLivingSpace',
      'CadastralType',
      'Version',
      'CreatedAt',
      'UpdatedAt',
      'WorkflowState',
      'CreatedByUserId',
      'CreatedDate',
    ];
    for (let i = 0; i < checkboxIds.length; i++) {
      customersSettingsPage.clickCheckboxById(checkboxIds[i]);
    }
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });
    customersSettingsPage.saveSettings();
  });
  it('should deactivate unnecessary fields', function () {
    loginPage.open('/');
    myEformsPage.Navbar.goToPluginsPage();
    const spinnerAnimation = $('#spinner-animation');
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });

    const plugin = pluginPage.getFirstPluginRowObj();
    const settingsBtn = plugin.settingsBtn;
    settingsBtn.waitForDisplayed({ timeout: 20000 });
    settingsBtn.waitForClickable({ timeout: 20000 });
    settingsBtn.click();
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });
    const checkboxIds = [
      'Version',
      'CreatedAt',
      'UpdatedAt',
      'UpdatedByUserId',
      'WorkflowState',
      'CreatedByUserId',
      'CreatedDate',
    ];
    for (let i = 0; i < checkboxIds.length; i++) {
      customersSettingsPage.clickCheckboxById(checkboxIds[i]);
    }
    // customersSettingsPage.clickCheckboxById('VatNumber');
    // customersSettingsPage.clickCheckboxById('CreatedBy');

    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });
    customersSettingsPage.saveSettings();
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });
  });
  it('should cleanup fields', function () {
    loginPage.open('/');
    myEformsPage.Navbar.goToPluginsPage();
    const spinnerAnimation = $('#spinner-animation');
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });

    const plugin = pluginPage.getFirstPluginRowObj();
    const settingsBtn = plugin.settingsBtn;
    settingsBtn.waitForDisplayed({ timeout: 20000 });
    settingsBtn.waitForClickable({ timeout: 20000 });
    settingsBtn.click();
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });

    const checkboxIds = [
      'VatNumber',
      'CreatedBy',
      'CompanyAddress',
      'CompanyAddress2',
      'ZipCode',
      'CityName',
      'Phone',
      'Email',
      'ContactPerson',
      'Description',
      'EanCode',
      'CountryCode',
      'CrmId',
      'CadastralNumber',
      'PropertyNumber',
      'ApartmentNumber',
      'CompletionYear',
      'FloorsWithLivingSpace',
      'CadastralType',
    ];
    for (let i = 0; i < checkboxIds.length; i++) {
      customersSettingsPage.clickCheckboxById(checkboxIds[i]);
    }
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });
    customersSettingsPage.saveSettings();
    spinnerAnimation.waitForDisplayed({ timeout: 20000, reverse: true });
  });
});
