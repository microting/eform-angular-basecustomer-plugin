import { test, expect } from '@playwright/test';
import { LoginPage } from '../../Page objects/Login.page';
import { CustomersPage, CustomerCreateUpdate, CustomerRowObject } from './CustomersPage.page';
import { generateRandmString } from '../../helper-functions';

const customer: CustomerCreateUpdate = {
  companyName: generateRandmString(8),
  companyAddress: generateRandmString(10),
  zipCode: '12345',
  cityName: generateRandmString(6),
  phone: '12345678',
  email: generateRandmString(5) + '@test.com',
  contactPerson: generateRandmString(8),
};

test.describe('Customers - Create', () => {
  test.beforeEach(async ({ page }) => {
    await new LoginPage(page).login();
  });

  test('should create a customer with all fields', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    const initialCount = await customersPage.getRowCount();
    await customersPage.createCustomer(customer);

    const newCount = await customersPage.getRowCount();
    expect(newCount).toBe(initialCount + 1);

    const lastRow = new CustomerRowObject(page, customersPage, newCount);
    const rowText = await lastRow.getRowText();
    expect(rowText).toContain(customer.companyName);
  });

  test('should cancel customer creation', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    const initialCount = await customersPage.getRowCount();
    await customersPage.createCustomer(customer, true);

    const newCount = await customersPage.getRowCount();
    expect(newCount).toBe(initialCount);
  });

  test.afterAll(async ({ browser }) => {
    const page = await browser.newPage();
    await new LoginPage(page).login();
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();
    await customersPage.clearTable();
    await page.close();
  });
});
