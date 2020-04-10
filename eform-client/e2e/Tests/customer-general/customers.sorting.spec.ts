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
    $('#spinner-animation').waitForDisplayed(20000, true);
    const firstRowId = Number($('#Id_0').getText());
    const secondRowId = Number($('#Id_1').getText());
    expect(firstRowId, 'first id bigger than second').lessThan(secondRowId);
  });
  it('sorts id\'s in reverse order', function () {
    customersPage.clickIdSort();
    $('#spinner-animation').waitForDisplayed(20000, true);
    const firstRowId = Number($('#Id_0').getText());
    const secondRowId = Number($('#Id_1').getText());
    expect(firstRowId, 'first lesser than second').greaterThan(secondRowId);
  });
  it('sorts customers by contact person', function () {
    // before sorting
    customersPage.clickContactSort();
    $('#spinner-animation').waitForDisplayed(20000, true);
    let firstRowContact = $('#ContactPerson_0');
    let secondRowContact = $('#ContactPerson_1');
    const contactsBefore = [firstRowContact, secondRowContact];
    // sort list and customers on page
    customersPage.clickContactSort();
    // get new values[
    $('#spinner-animation').waitForDisplayed(20000, true);
    firstRowContact = $('#ContactPerson_0');
    secondRowContact = $('#ContactPerson_1');
    // compare!
    const contactsAfter = [firstRowContact, secondRowContact];
    const result = (contactsBefore[0] !== contactsAfter[0]) && (contactsBefore[1] !== contactsAfter[1]);
    expect(result, 'First customer\'s contact in wrong order').equal(true);
  });
  it('sorts customers by contact person reversed', function () {
    // before sorting
    customersPage.clickContactSort();
    $('#spinner-animation').waitForDisplayed(20000, true);
    let firstRowContact = $('#ContactPerson_0');
    let secondRowContact = $('#ContactPerson_1');
    const contactsBefore = [firstRowContact, secondRowContact];
    // sort list and customers on page
    customersPage.clickContactSort();
    // get new values
    $('#spinner-animation').waitForDisplayed(20000, true);
    firstRowContact = $('#ContactPerson_0');
    secondRowContact = $('#ContactPerson_1');
    // compare!
    const contactsAfter = [firstRowContact, secondRowContact];
    const result = (contactsBefore[0] !== contactsAfter[0]) && (contactsBefore[1] !== contactsAfter[1]);
    expect(result, 'First customer\'s contact in wrong order').equal(true);
  });
  it('sorts customers by company name', function () {
    // before sorting
    customersPage.clickCompanySort();
    $('#spinner-animation').waitForDisplayed(20000, true);
    let firstRowName = $('#CompanyName_0');
    let secondRowName = $('#CompanyName_1');
    const namesBefore = [firstRowName, secondRowName];
    // sort list and customers on page
    customersPage.clickCompanySort();
    // get new values
    $('#spinner-animation').waitForDisplayed(20000, true);
    firstRowName = $('#CompanyName_0');
    secondRowName = $('#CompanyName_1');
    // compare!
    const namesAfter = [firstRowName, secondRowName];
    const result = (namesBefore[0] !== namesAfter[0]) && (namesBefore[1] !== namesAfter[1]);
    expect(result, 'First customer\'s company name in wrong order').equal(true);
  });
  it('sorts customers by company name reversed', function () {
    // before sorting
    customersPage.clickCompanySort();
    $('#spinner-animation').waitForDisplayed(20000, true);
    let firstRowName = $('#ContactPerson_0');
    let secondRowName = $('#ContactPerson_1');
    const namesBefore = [firstRowName, secondRowName];
    // sort list and customers on page
    customersPage.clickCompanySort();

    $('#spinner-animation').waitForDisplayed(20000, true);
    // get new values
    firstRowName = $('#ContactPerson_0');
    secondRowName = $('#ContactPerson_1');
    // compare!
    const namesAfter = [firstRowName, secondRowName];
    const result = (namesBefore[0] !== namesAfter[0]) && (namesBefore[1] !== namesAfter[1]);
    expect(result, 'First customer\'s company name in wrong order').equal(true);
  });
});
