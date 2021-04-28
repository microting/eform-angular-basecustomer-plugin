import loginPage from '../../Page objects/Login.page';
import customersPage, {
  CustomersRowObject,
} from '../../Page objects/Customers/Customers.page';

const expect = require('chai').expect;

describe('Customer modal', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
    customersPage.create();
  });
  it('should not delete customer if cancel was clicked', function () {
    const rowBeforeDeletion = customersPage.rowNum;
    const lastCustomer: CustomersRowObject = customersPage.getCustomer(
      rowBeforeDeletion
    );
    lastCustomer.delete(true);
    expect(
      rowBeforeDeletion,
      `Number of rows has changed after click cancel on deleting customer`
    ).equal(customersPage.rowNum);
  });
  it('should delete customer', function () {
    const rowBeforeDeletion = customersPage.rowNum;
    const lastCustomer: CustomersRowObject = customersPage.getCustomer(
      rowBeforeDeletion
    );
    lastCustomer.delete();
    expect(
      rowBeforeDeletion - 1,
      `Number of rows hasn't changed after deleting customer`
    ).equal(customersPage.rowNum);
  });
  after(function () {
    customersPage.clearTable();
  });
});
