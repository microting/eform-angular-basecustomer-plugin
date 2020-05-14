import loginPage from '../../Page objects/Login.page';
import {generateRandmString} from '../../Helpers/helper-functions';
import customersPage, {CustomersRowObject} from '../../Page objects/Customers/Customers.page';
import customersModalPage from '../../Page objects/Customers/CustomersModal.page';

const expect = require('chai').expect;

describe('Customer modal', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
  });
  it('should delete customer', function () {
    const rowBeforeDeletion = customersPage.rowNum();
    const lastCustomer: CustomersRowObject = customersPage.getCustomer(rowBeforeDeletion);
    lastCustomer.deleteBtn.waitForDisplayed({timeout: 3000});
    lastCustomer.deleteBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    customersModalPage.deleteCustomer();
    $('#mainTableBody').waitForDisplayed({timeout: 20000});
    const rowAfterDeletion = customersPage.rowNum();
    expect(rowBeforeDeletion, 'Number of rows hasn\'t changed after deleting customer').equal(rowAfterDeletion + 1);
  });
  it('should not delete customer if cancel was clicked', function () {
    const lastCustomer: CustomersRowObject = customersPage.getCustomer(customersPage.rowNum());
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    lastCustomer.deleteBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
    customersModalPage.cancelDeleteBtn.click();
  });
});
