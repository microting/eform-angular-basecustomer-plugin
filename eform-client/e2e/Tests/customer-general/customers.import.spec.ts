import loginPage from '../../Page objects/Login.page';
import customersPage, {CustomersRowObject} from '../../Page objects/Customers/Customers.page';
import customersImportPage from '../../Page objects/Customers/CustomersImport.page';

const expect = require('chai').expect;

describe('Customers plugin import page', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
  });
  it('should import customers list', function () {
    const localPath = process.cwd();
    const customersBefore = customersPage.rowNum();
    customersPage.goToImportBtn();
    $('#files').addValue(localPath + '/e2e/Assets/Import-test.csv');
    //browser.chooseFile('#files', localPath + '/e2e/Assets/Import-test.csv');
    $('#row_0').waitForDisplayed({timeout: 20000});
    $('#row_1').waitForDisplayed({timeout: 20000});
    $('#row_2').waitForDisplayed({timeout: 20000});
    // check if two customers ready to be imported
    const customersN = customersImportPage.numberOfCustomers;
    expect(customersN, 'After choosing file, two customers must appear in table').equal(3);
    // choose Company name;Address; Zip; City; Contact
    customersImportPage.chooseFromDropdown('CompanyName', 0);
    customersImportPage.chooseFromDropdown('CompanyAddress', 1);
    customersImportPage.chooseFromDropdown('ZipCode', 2);
    customersImportPage.chooseFromDropdown('CityName', 3);
    customersImportPage.chooseFromDropdown('ContactPerson', 4);

    customersImportPage.continueImport();
    // refresh the page and check number of customers
    loginPage.open('/plugins/customers-pn');
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    const customersAfter = customersPage.rowNum();
    expect(customersAfter, 'Number of customers is not bigger than before').greaterThan(customersBefore);
  });
  it('should not import customers', function () {
    const localPath = process.cwd();
    const customersBefore = customersImportPage.numberOfCustomers;
    const importButton = customersPage.importCustomerBtn();
    importButton.click();
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    $('#files').addValue(localPath + '/e2e/Assets/Import-test.csv');
    //browser.chooseFile('#files', localPath + '/e2e/Assets/Import-test.csv');
    $('#row_0').waitForDisplayed({timeout: 20000});
    $('#row_1').waitForDisplayed({timeout: 20000});
    $('#row_2').waitForDisplayed({timeout: 20000});
    customersImportPage.cancelImport();

    const customersAfter = customersImportPage.numberOfCustomers;
    expect(customersAfter, 'Number of customers is\'t same as before').equal(customersBefore);
  });
});
