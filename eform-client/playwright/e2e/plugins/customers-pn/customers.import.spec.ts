import { test, expect } from '@playwright/test';
import { LoginPage } from '../../Page objects/Login.page';
import { CustomersPage } from './CustomersPage.page';

test.describe('Customers - Import', () => {
  test.beforeEach(async ({ page }) => {
    await new LoginPage(page).login();
  });

  test('should navigate to import page', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    await customersPage.importCustomerBtn().click();
    await page.locator('#csvFileInput').waitFor({ state: 'attached' });

    // Verify import page elements are present
    const importBtn = page.locator('#importBtn');
    await expect(importBtn).toBeVisible();
    await expect(importBtn).toBeDisabled();
  });

  test('should cancel import and return to customer list', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    await customersPage.importCustomerBtn().click();
    await page.locator('#cancelImportBtn').click();

    await page.locator('app-customers-pn-container').waitFor({ state: 'visible' });
  });
});
