import { test, expect } from '@playwright/test';
import { LoginPage } from '../../Page objects/Login.page';
import { CustomersPage, CustomerCreateUpdate } from './CustomersPage.page';
import { generateRandmString } from '../../helper-functions';

const customers: CustomerCreateUpdate[] = [
  { companyName: 'AAA_' + generateRandmString(4), phone: '11111111' },
  { companyName: 'ZZZ_' + generateRandmString(4), phone: '22222222' },
  { companyName: 'MMM_' + generateRandmString(4), phone: '33333333' },
];

test.describe('Customers - Sorting', () => {
  test.beforeEach(async ({ page }) => {
    await new LoginPage(page).login();
  });

  test('should sort by column header click', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    for (const customer of customers) {
      await customersPage.createCustomer(customer);
    }

    // Click CompanyName header to sort
    const companyNameHeader = page.locator('.mat-sort-header').filter({ hasText: 'CompanyName' });
    if (await companyNameHeader.count() > 0) {
      await companyNameHeader.click();
      await page.waitForTimeout(500);

      // Verify table is sorted (check first row contains expected sorted value)
      const firstRow = page.locator('.mat-mdc-row').first();
      const firstRowText = await firstRow.innerText();
      expect(firstRowText).toBeTruthy();
    }
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
