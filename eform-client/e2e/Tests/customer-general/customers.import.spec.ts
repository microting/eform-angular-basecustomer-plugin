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
    const customersBefore = customersImportPage.numberOfCustomers;
    const importButton = customersPage.importCustomerBtn();
    importButton.click();
    browser.pause(8000);
    browser.chooseFile('#files', localPath + '/e2e/Assets/Import-test.csv');
    browser.pause(8000);
    // press 'continue import' button
    customersImportPage.continueImport();
    const customersAfter = customersImportPage.numberOfCustomers;
    expect(customersAfter, 'Number of customers is not bigger than before').greaterThan(customersBefore)
  });
  it('should not import customers', function () {
    const localPath = process.cwd();
    const customersBefore = customersImportPage.numberOfCustomers;
    const importButton = customersPage.importCustomerBtn();
    importButton.click();
    browser.pause(8000);
    browser.chooseFile('#files', localPath + '\\e2e\\Assets\\Import-test.csv');
    browser.pause(8000);
    customersImportPage.cancelImport();

    const customersAfter = customersImportPage.numberOfCustomers;
    expect(customersAfter, 'Number of customers is\'t same as before').equal(customersBefore);
  });
});
