import loginPage from '../../Page objects/Login.page';
import {generateRandmString} from '../../Helpers/helper-functions';
import customersPage, {CustomersRowObject} from '../../Page objects/Customers/Customers.page';
import customersModalPage from '../../Page objects/Customers/CustomersModal.page';
import {Guid} from "guid-typescript";

const expect = require('chai').expect;

describe('Customers plugin page should update customer', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
  });
  it('with all empty fields', function () {
    const lastCustomerBeforeEdit = customersPage.getCustomer(customersPage.rowNum);
    lastCustomerBeforeEdit.editBtn.waitForVisible(3000);
    lastCustomerBeforeEdit.editBtn.click();
    browser.pause(3000);
    const customerObject = {
      createdBy: Guid.create().toString(),
      customerNo: Guid.create().toString(),
      contactPerson: Guid.create().toString(),
      companyName: Guid.create().toString(),
      companyAddress: Guid.create().toString(),
      zipCode: Guid.create().toString(),
      cityName: Guid.create().toString(),
      phone: Guid.create().toString(),
      email: Guid.create().toString()
    };
    customersModalPage.updateCustomer(customerObject);
    const lastCustomerAfterEdit = customersPage.getCustomer(customersPage.rowNum);
    expect(lastCustomerAfterEdit.createdBy, '').equal(customerObject.createdBy);
    expect(lastCustomerAfterEdit.customerNo, '').equal(customerObject.customerNo);
    expect(lastCustomerAfterEdit.contactPerson, '').equal(customerObject.contactPerson);
    expect(lastCustomerAfterEdit.companyName, '').equal(customerObject.companyName);
    expect(lastCustomerAfterEdit.companyAddress, '').equal(customerObject.companyAddress);
    expect(lastCustomerAfterEdit.zipCode, '').equal(customerObject.zipCode);
    expect(lastCustomerAfterEdit.cityName, '').equal(customerObject.cityName);
    expect(lastCustomerAfterEdit.phone, '').equal(customerObject.phone);
    expect(lastCustomerAfterEdit.email, '').equal(customerObject.email);

  });
  it('with all fields', function () {
    const lastCustomer: CustomersRowObject = customersPage.getCustomer(customersPage.rowNum);
  });
});
describe('Customers plugin page should not update customer', function () {
  it('if cancel is clicked', function () {

  });
});
