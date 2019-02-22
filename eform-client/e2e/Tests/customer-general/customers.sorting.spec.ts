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
    const firstRowId = parseInt( $('#tableBody').$('tr:nth-child(1)').$('#Id').getText(), 10);
    const secondRowId = parseInt( $('#tableBody').$('tr:nth-child(2)').$('#Id').getText(), 10);
    browser.pause(2000);
    expect(firstRowId, 'first bigger than second').lessThan(secondRowId);
  });
  it('sorts id\'s in reverse order', function () {
    customersPage.clickIdSort();
    const firstRowId = parseInt( $('#tableBody').$('tr:nth-child(1)').$('#Id').getText(), 10);
    const secondRowId = parseInt( $('#tableBody').$('tr:nth-child(2)').$('#Id').getText(), 10);
    browser.pause(2000);
    expect(firstRowId, 'first lesser than second').greaterThan(secondRowId);
  });
  it('sorts customers by contact person', function () {
    // ContactPersonTableHeader
    customersPage.clickContactSort();
    const firstRowContact = $('#tableBody').$('tr:nth-child(1)').$('#ContactPerson').getText();
    const secondRowContact = $('#tableBody').$('tr:nth-child(2)').$('#ContactPerson').getText();
    const result = firstRowContact <= secondRowContact;
    expect(result, 'First customer\'s contact in wrong order').true;
  });
  it('sorts customers by contact person reversed', function () {
    // ContactPersonTableHeader
    customersPage.clickContactSort();
    const firstRowContact = $('#tableBody').$('tr:nth-child(1)').$('#ContactPerson').getText();
    const secondRowContact = $('#tableBody').$('tr:nth-child(2)').$('#ContactPerson').getText();
    const result = firstRowContact >= secondRowContact;
    expect(result, 'First customer\'s contact in wrong order').true;
  });
  it('sorts customers by company name', function () {
    // CompanyNameTableHeader
    customersPage.clickCompanySort();
    const firstRowContact = $('#tableBody').$('tr:nth-child(1)').$('#CompanyName').getText();
    const secondRowContact = $('#tableBody').$('tr:nth-child(2)').$('#CompanyName').getText();
    const result = firstRowContact <= secondRowContact;
    expect(result, 'First customer\'s Company name in wrong order').true;
  });
  it('sorts customers by company name reversed', function () {
    // CompanyNameTableHeader
    customersPage.clickCompanySort();
    const firstRowContact = $('#tableBody').$('tr:nth-child(1)').$('#CompanyName').getText();
    const secondRowContact = $('#tableBody').$('tr:nth-child(2)').$('#CompanyName').getText();
    const result = firstRowContact >= secondRowContact;
    expect(result, 'First customer\'s Company name in wrong order').true;
  });
});
