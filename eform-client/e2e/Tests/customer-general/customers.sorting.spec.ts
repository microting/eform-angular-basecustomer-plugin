import loginPage from '../../Page objects/Login.page';
import customersPage from '../../Page objects/Customers/Customers.page';
const expect = require('chai').expect;


describe('Customers sorting', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
  });
  it('sorts customers by id', function () {
    customersPage.clickIdSort();
    const firstRowId = customersPage.getCustomerValue('Id_0', 1);
    const secondRowId = customersPage.getCustomerValue('Id_1', 2);
    $('#spinner-animation').waitForDisplayed(20000, true);
    expect(firstRowId, 'first id bigger than second').lessThan(secondRowId);
  });
  it('sorts id\'s in reverse order', function () {
    customersPage.clickIdSort();
    const firstRowId = customersPage.getCustomerValue('Id_0', 1);
    const secondRowId = customersPage.getCustomerValue('Id_1', 2);
    $('#spinner-animation').waitForDisplayed(20000, true);
    expect(firstRowId, 'first lesser than second').greaterThan(secondRowId);
  });
  it('sorts customers by contact person', function () {
    // before sorting
    customersPage.clickContactSort();
    let firstRowContact = customersPage.getCustomerValue('ContactPerson_0', 1);
    let secondRowContact = customersPage.getCustomerValue('ContactPerson_1', 2);
    const contactsBefore = [firstRowContact, secondRowContact];
    // sort list and customers on page
    customersPage.clickContactSort();
    // get new values[
    firstRowContact = customersPage.getCustomerValue('ContactPerson_0', 1);
    secondRowContact = customersPage.getCustomerValue('ContactPerson_1', 2);
    // compare!
    const contactsAfter = [firstRowContact, secondRowContact];
    const result = (contactsBefore[0] !== contactsAfter[0]) && (contactsBefore[1] !== contactsAfter[1]);
    expect(result, 'First customer\'s contact in wrong order').equal(true);
  });
  it('sorts customers by contact person reversed', function () {
    // before sorting
    customersPage.clickContactSort();
    let firstRowContact = customersPage.getCustomerValue('ContactPerson_0', 1);
    let secondRowContact = customersPage.getCustomerValue('ContactPerson_1', 2);
    const contactsBefore = [firstRowContact, secondRowContact];
    // sort list and customers on page
    customersPage.clickContactSort();
    // get new values
    firstRowContact = customersPage.getCustomerValue('ContactPerson_0', 1);
    secondRowContact = customersPage.getCustomerValue('ContactPerson_1', 2);
    // compare!
    const contactsAfter = [firstRowContact, secondRowContact];
    const result = (contactsBefore[0] !== contactsAfter[0]) && (contactsBefore[1] !== contactsAfter[1]);
    expect(result, 'First customer\'s contact in wrong order').equal(true);
  });
  it('sorts customers by company name', function () {
    // before sorting
    customersPage.clickCompanySort();
    let firstRowName = customersPage.getCustomerValue('CompanyName_0', 1);
    let secondRowName = customersPage.getCustomerValue('CompanyName_1', 2);
    const namesBefore = [firstRowName, secondRowName];
    // sort list and customers on page
    customersPage.clickCompanySort();
    // get new values
    firstRowName = customersPage.getCustomerValue('CompanyName_0', 1);
    secondRowName = customersPage.getCustomerValue('CompanyName_1', 2);
    // compare!
    const namesAfter = [firstRowName, secondRowName];
    const result = (namesBefore[0] !== namesAfter[0]) && (namesBefore[1] !== namesAfter[1]);
    expect(result, 'First customer\'s company name in wrong order').equal(true);
  });
  it('sorts customers by company name reversed', function () {
    // before sorting
    customersPage.clickCompanySort();
    let firstRowName = customersPage.getCustomerValue('ContactPerson_0', 1);
    let secondRowName = customersPage.getCustomerValue('ContactPerson_1', 2);
    const namesBefore = [firstRowName, secondRowName];
    // sort list and customers on page
    customersPage.clickCompanySort();

    // get new values
    firstRowName = customersPage.getCustomerValue('ContactPerson_0', 1);
    secondRowName = customersPage.getCustomerValue('ContactPerson_1', 2);
    // compare!
    const namesAfter = [firstRowName, secondRowName];
    const result = (namesBefore[0] !== namesAfter[0]) && (namesBefore[1] !== namesAfter[1]);
    expect(result, 'First customer\'s company name in wrong order').equal(true);
  });
});
