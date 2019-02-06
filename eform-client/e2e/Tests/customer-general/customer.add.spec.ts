import loginPage from '../../Page objects/Login.page';
import {generateRandmString} from '../../Helpers/helper-functions';
import customersPage, {CustomersRowObject} from './Customers.page';
import customersModalPage from './CustomersModal.page';

const expect = require('chai').expect;

describe('Customers plugin page should add new customer', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
  });
  it('with all empty fields', function () {
    customersPage.newCustomerBtn.click();
    browser.pause(6000);
    const rowCountBeforeCreation = customersPage.rowNum;
    customersModalPage.createEmptyCustomer();
    const rowCountAfterCreation = customersPage.rowNum;
    expect(rowCountAfterCreation, 'Number of rows hasn\'t changed after creating new customer').equal(rowCountBeforeCreation + 1);
  });
  it('with all fields', function () {
    customersPage.newCustomerBtn.click();
    browser.pause(6000);
    const customerObject = {
      createdBy: 'John Smith',
      createCustomerNo: '1',
      createContactPerson: 'Samantha Black',
      createCompanyName: 'BMW',
      createCompanyAddress: 'ABC Street 22',
      createZipCode: '021551',
      createCityName: 'Odense',
      createPhone: '123124',
      createEmail: 'user@user.com'
    };
    const rowCountBeforeCreation = customersPage.rowNum;
    customersModalPage.createCustomer(customerObject);
    const rowCountAfterCreation = customersPage.rowNum;
    browser.pause(2000);
    expect(rowCountAfterCreation, 'Number of rows hasn\'t changed after creating new user').equal(rowCountBeforeCreation + 1);
    const lastCustomer: CustomersRowObject = customersPage.getCustomer(customersPage.rowNum);
    expect(lastCustomer.createdBy, 'Created by of created customer is incorrect').equal(customerObject.createdBy);
    expect(lastCustomer.customerNo, 'Customer number of created customer is incorrect').equal(customerObject.createCustomerNo);
    expect(lastCustomer.contactPerson, 'Contact person of created customer is incorrect').equal(customerObject.createContactPerson);
    expect(lastCustomer.companyName, 'Company name of created customer is incorrect').equal(customerObject.createCompanyName);
    expect(lastCustomer.companyAddress, 'Company address of created customer is incorrect').equal(customerObject.createCompanyAddress);
    expect(lastCustomer.zipCode, 'Zip code of created customer is incorrect').equal(customerObject.createZipCode);
    expect(lastCustomer.cityName, 'City name of created customer is incorrect').equal(customerObject.createCityName);
    expect(lastCustomer.phone, 'Phone of created customer is incorrect').equal(customerObject.createPhone);
    expect(lastCustomer.email, 'Email of created customer is incorrect').equal(customerObject.createEmail);
    browser.pause(6000);
  });
});
describe('Customers plugin page should not add new customer', function () {
  it('if cancel is clicked', function () {
    customersPage.newCustomerBtn.click();
    browser.pause(6000);
    customersModalPage.cancelCreateBtn.click();
  });
});
