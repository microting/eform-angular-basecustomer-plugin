import loginPage from '../../Page objects/Login.page';
import {generateRandmString} from '../../Helpers/helper-functions';
import customersPage, {CustomersRowObject} from './Customers.page';
import customersModalPage from './CustomersModal.page';

const expect = require('chai').expect;

describe('Customer modal', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
  });
  it('should delete customer', function () {
    const rowBeforeDeletion = customersPage.rowNum;
    const lastCustomer: CustomersRowObject = customersPage.getCustomer(rowBeforeDeletion);
    lastCustomer.deleteBtn.waitForVisible(5000);
    lastCustomer.deleteBtn.leftClick();
    browser.pause(6000);
    customersModalPage.deleteCustomerBtn.click();
    browser.pause(6000);
    expect(rowBeforeDeletion, 'Number of rows hasn\'t changed after deleting customer').equal(rowBeforeDeletion + 1);
  });
  it('should not delete customer if cancel was clicked', function () {
    const lastCustomer: CustomersRowObject = customersPage.getCustomer(customersPage.rowNum);
    lastCustomer.deleteBtn.waitForVisible(5000);
    lastCustomer.deleteBtn.leftClick();
    browser.pause(6000);
    customersModalPage.cancelDeleteBtn.click();
  });
});
