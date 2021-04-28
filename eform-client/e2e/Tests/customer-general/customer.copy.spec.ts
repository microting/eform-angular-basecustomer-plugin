import loginPage from '../../Page objects/Login.page';
import customersPage, {
  CreateUpdateCustomer,
  CustomersRowObject,
} from '../../Page objects/Customers/Customers.page';

const expect = require('chai').expect;

const customerObject: CreateUpdateCustomer = {
  createdBy: 'John Smith',
  customerNo: '1',
  contactPerson: 'Jack Black',
  companyName: 'BMW',
  companyAddress: 'ABC Street 22',
  zipCode: '021551',
  cityName: 'Odense',
  phone: '123124',
  email: 'user@user.com',
  eanCode: '784565',
  vatNumber: '821456',
  countryCode: 'DK',
  cadastralNumber: 'eal10230',
  propertyNumber: 1235,
  apartmentNumber: 52,
  completionYear: 1960,
  floorsWithLivingSpace: 3,
};

describe('Customers plugin page', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
  });
  // it('should add new customer with all empty fields', function () {
  //   $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
  //   const rowCountBeforeCreation = browser.$$('#mainTableBody > tr').length;
  //   customersPage.newCustomerBtn.click();
  //   $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
  //   customersModalPage.createEmptyCustomer();
  //   $('#spinner-animation').waitForDisplayed({timeout: 20000, reverse: true});
  //   const rowCountAfterCreation = browser.$$('#mainTableBody > tr').length;
  //   expect(rowCountAfterCreation, 'Number of rows hasn\'t changed after creating new customer').equal(rowCountBeforeCreation + 1);
  // });
  it('should add new customer with all fields', function () {
    const rowCountBeforeCreation = customersPage.rowNum;
    customersPage.create(customerObject);
    const rowCountAfterCreation = customersPage.rowNum;
    expect(
      rowCountAfterCreation,
      `Number of rows hasn't changed after creating new user`
    ).equal(rowCountBeforeCreation + 1);
  });
  it('should make a copy of the customer', function () {
    const rowNumBeforeCopy = customersPage.rowNum;
    const customer = customersPage.getCustomer(customersPage.rowNum);
    customer.copy({
      ...customerObject,
      companyAddress: customerObject.companyAddress + '_copy',
    });
    expect(rowNumBeforeCopy + 1, 'customer not copied').eq(
      customersPage.rowNum
    );
    const lastCustomer: CustomersRowObject = customersPage.getCustomer(
      customersPage.rowNum
    );
    expect(
      lastCustomer.companyAddress,
      'Created by of created customer is incorrect'
    ).equal(customer.companyAddress + '_copy');
    expect(
      lastCustomer.createdBy,
      'Created by of created customer is incorrect'
    ).equal(customerObject.createdBy);
    expect(
      lastCustomer.customerNo,
      'Customer number of created customer is incorrect'
    ).equal(customerObject.customerNo);
    expect(
      lastCustomer.contactPerson,
      'Contact person of created customer is incorrect'
    ).equal(customerObject.contactPerson);
    expect(
      lastCustomer.companyName,
      'Company name of created customer is incorrect'
    ).equal(customerObject.companyName);
    expect(
      lastCustomer.zipCode,
      'Zip code of created customer is incorrect'
    ).equal(customerObject.zipCode);
    expect(
      lastCustomer.cityName,
      'City name of created customer is incorrect'
    ).equal(customerObject.cityName);
    expect(lastCustomer.phone, 'Phone of created customer is incorrect').equal(
      customerObject.phone
    );
    expect(lastCustomer.email, 'Email of created customer is incorrect').equal(
      customerObject.email
    );
  });
  after(function () {
    customersPage.clearTable();
  });
});
