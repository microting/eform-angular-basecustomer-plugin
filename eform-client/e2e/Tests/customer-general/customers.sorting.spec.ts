import loginPage from '../../Page objects/Login.page';
import customersPage from '../../Page objects/Customers/Customers.page';
import { testSorting } from '../../Helpers/helper-functions';

describe('Customers sorting', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
  });
  it('sorts customers by id', function () {
    testSorting(customersPage.IdTableHeader, 'idCustomer', 'Id');
  });
  it('sorts customers by contact person', function () {
    testSorting(
      customersPage.contactPersonTableHeader,
      'contactPerson',
      'contact person'
    );
  });
  it('sorts customers by company name', function () {
    testSorting(
      customersPage.companyNameTableHeader,
      'companyName',
      'company name'
    );
  });
});
