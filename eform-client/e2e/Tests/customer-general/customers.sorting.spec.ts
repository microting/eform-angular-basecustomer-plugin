import loginPage from '../../Page objects/Login.page';
import customersPage from '../../Page objects/Customers/Customers.page';
import { testSorting } from '../../Helpers/helper-functions';

describe('Customers sorting', function () {
  before(function () {
    loginPage.open('/');
    loginPage.login();
    customersPage.goToCustomersPage();
    customersPage.createDummyCustomers(3);
  });
  it('sorts customers by Id', function () {
    testSorting(customersPage.idTableHeader, 'idCustomer', 'Id');
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
  after(function () {
    customersPage.clearTable();
  });
});
