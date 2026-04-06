import { test, expect } from '@playwright/test';
import { LoginPage } from '../../Page objects/Login.page';
import { CustomersPage, CustomerCreateUpdate, CustomerRowObject } from './CustomersPage.page';
import { generateRandmString } from '../../helper-functions';

const customer: CustomerCreateUpdate = {
  companyName: generateRandmString(8),
  companyAddress: generateRandmString(10),
  zipCode: '54321',
  cityName: generateRandmString(6),
  phone: '87654321',
  email: generateRandmString(5) + '@test.com',
  contactPerson: generateRandmString(8),
};

const updatedCustomer: CustomerCreateUpdate = {
  companyName: generateRandmString(8) + '_edited',
  phone: '11112222',
  email: generateRandmString(5) + '@edited.com',
};

test.describe('Customers - Edit', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto("/");
    await new LoginPage(page).login();
  });

  test('should edit an existing customer', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    await customersPage.createCustomer(customer);

    const rowCount = await customersPage.getRowCount();
    const row = new CustomerRowObject(page, customersPage, rowCount);
    await row.edit(updatedCustomer);

    const rowText = await row.getRowText();
    expect(rowText).toContain(updatedCustomer.companyName);
  });

  test('should cancel customer edit', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    const rowCount = await customersPage.getRowCount();
    if (rowCount > 0) {
      const row = new CustomerRowObject(page, customersPage, 1);
      const originalText = await row.getRowText();
      await row.edit({ companyName: 'ShouldNotAppear' }, true);
      const afterText = await row.getRowText();
      expect(afterText).toBe(originalText);
    }
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
