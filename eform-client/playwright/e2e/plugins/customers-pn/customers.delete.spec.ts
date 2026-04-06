import { test, expect } from '@playwright/test';
import { LoginPage } from '../../Page objects/Login.page';
import { CustomersPage, CustomerCreateUpdate, CustomerRowObject } from './CustomersPage.page';
import { generateRandmString } from '../../helper-functions';

test.describe('Customers - Delete', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto("/");
    await new LoginPage(page).login();
  });

  test('should delete a customer', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    const customer: CustomerCreateUpdate = {
      companyName: generateRandmString(8),
      companyAddress: generateRandmString(10),
      phone: '99887766',
    };
    await customersPage.createCustomer(customer);
    const countAfterCreate = await customersPage.getRowCount();

    const row = new CustomerRowObject(page, customersPage, countAfterCreate);
    await row.delete();

    const countAfterDelete = await customersPage.getRowCount();
    expect(countAfterDelete).toBe(countAfterCreate - 1);
  });

  test('should cancel customer deletion', async ({ page }) => {
    test.setTimeout(180000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    const customer: CustomerCreateUpdate = {
      companyName: generateRandmString(8),
      companyAddress: generateRandmString(10),
      phone: '99887711',
    };
    await customersPage.createCustomer(customer);
    const countAfterCreate = await customersPage.getRowCount();

    const row = new CustomerRowObject(page, customersPage, countAfterCreate);
    await row.delete(true);

    const countAfterCancel = await customersPage.getRowCount();
    expect(countAfterCancel).toBe(countAfterCreate);
  });

  test.afterAll(async ({ browser }) => {
    const page = await browser.newPage();
    await page.goto("/");
    await new LoginPage(page).login();
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();
    await customersPage.clearTable();
    await page.close();
  });
});
