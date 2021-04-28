import loginPage from '../../Page objects/Login.page';
import { generateRandmString } from '../../Helpers/helper-functions';
import customersPage, {
  CreateUpdateCustomer,
} from '../../Page objects/Customers/Customers.page';

const expect = require('chai').expect;

describe('Customers plugin page', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
    customersPage.create();
  });
  it('should update customer with all fields', function () {
    const lastCustomerBeforeEdit = customersPage.getCustomer(
      customersPage.rowNum
    );
    const customerObject: CreateUpdateCustomer = {
      createdBy: generateRandmString(),
      customerNo: generateRandmString(),
      contactPerson: generateRandmString(),
      companyName: generateRandmString(),
      companyAddress: generateRandmString(),
      zipCode: generateRandmString(),
      cityName: generateRandmString(),
      phone: generateRandmString(),
      email: generateRandmString(),
      eanCode: generateRandmString(),
      vatNumber: generateRandmString(),
      countryCode: generateRandmString(),
      cadastralNumber: generateRandmString(),
      propertyNumber: Math.floor(Math.random() * 100),
      apartmentNumber: Math.floor(Math.random() * 25),
      completionYear: Math.floor(Math.random() * 100),
      floorsWithLivingSpace: Math.floor(Math.random() * 5),
    };
    lastCustomerBeforeEdit.edit(customerObject);
    const lastCustomerAfterEdit = customersPage.getCustomer(
      customersPage.rowNum
    );
    expect(
      lastCustomerAfterEdit.createdBy,
      'Created by of updated customer is incorrect'
    ).equal(customerObject.createdBy);
    expect(
      lastCustomerAfterEdit.customerNo,
      'Customer number of updated customer is incorrect'
    ).equal(customerObject.customerNo);
    expect(
      lastCustomerAfterEdit.contactPerson,
      'Contact person of updated customer is incorrect'
    ).equal(customerObject.contactPerson);
    expect(
      lastCustomerAfterEdit.companyName,
      'Company name of updated customer is incorrect'
    ).equal(customerObject.companyName);
    expect(
      lastCustomerAfterEdit.companyAddress,
      'Company address of updated customer is incorrect'
    ).equal(customerObject.companyAddress);
    expect(
      lastCustomerAfterEdit.zipCode,
      'Zip code of updated customer is incorrect'
    ).equal(customerObject.zipCode);
    expect(
      lastCustomerAfterEdit.cityName,
      'City name of updated customer is incorrect'
    ).equal(customerObject.cityName);
    expect(
      lastCustomerAfterEdit.phone,
      'Phone of updated customer is incorrect'
    ).equal(customerObject.phone);
    expect(
      lastCustomerAfterEdit.email,
      'Email of updated customer is incorrect'
    ).equal(customerObject.email);
  });
  it('should not update customer if cancel is clicked', function () {
    const lastCustomerBeforeEdit = customersPage.getCustomer(
      customersPage.rowNum
    );
    lastCustomerBeforeEdit.edit(null, true);
    const lastCustomerAfterEdit = customersPage.getCustomer(
      customersPage.rowNum
    );
    expect(
      lastCustomerAfterEdit.createdBy,
      'Created by of updated customer is incorrect'
    ).equal(lastCustomerBeforeEdit.createdBy);
    expect(
      lastCustomerAfterEdit.customerNo,
      'Customer number of updated customer is incorrect'
    ).equal(lastCustomerBeforeEdit.customerNo);
    expect(
      lastCustomerAfterEdit.contactPerson,
      'Contact person of updated customer is incorrect'
    ).equal(lastCustomerBeforeEdit.contactPerson);
    expect(
      lastCustomerAfterEdit.companyName,
      'Company name of updated customer is incorrect'
    ).equal(lastCustomerBeforeEdit.companyName);
    expect(
      lastCustomerAfterEdit.companyAddress,
      'Company address of updated customer is incorrect'
    ).equal(lastCustomerBeforeEdit.companyAddress);
    expect(
      lastCustomerAfterEdit.zipCode,
      'Zip code of updated customer is incorrect'
    ).equal(lastCustomerBeforeEdit.zipCode);
    expect(
      lastCustomerAfterEdit.cityName,
      'City name of updated customer is incorrect'
    ).equal(lastCustomerBeforeEdit.cityName);
    expect(
      lastCustomerAfterEdit.phone,
      'Phone of updated customer is incorrect'
    ).equal(lastCustomerBeforeEdit.phone);
    expect(
      lastCustomerAfterEdit.email,
      'Email of updated customer is incorrect'
    ).equal(lastCustomerBeforeEdit.email);
  });
  after(function () {
    customersPage.clearTable();
  });
});
