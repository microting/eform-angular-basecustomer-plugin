import { test, expect } from '@playwright/test';
import { LoginPage } from '../../Page objects/Login.page';
import { CustomersPage, CustomerCreateUpdate, CustomerRowObject } from './CustomersPage.page';
import { generateRandmString } from '../../helper-functions';

const customer: CustomerCreateUpdate = {
  companyName: generateRandmString(8),
  companyAddress: generateRandmString(10),
  phone: '55443322',
  contactPerson: generateRandmString(6),
};

test.describe('Customers - Copy', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto("/");
    await new LoginPage(page).login();
  });

  test('should copy a customer', async ({ page }) => {
    test.setTimeout(180000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    await customersPage.createCustomer(customer);
    const countAfterCreate = await customersPage.getRowCount();

    // Open copy modal, modify company name to avoid duplicate detection, then save
    const row = new CustomerRowObject(page, customersPage, countAfterCreate);
    await row.openCopyModal();
    const companyNameInput = page.locator('#createCompanyName');
    await companyNameInput.clear();
    await companyNameInput.fill(customer.companyName + '_copy');
    await customersPage.closeCreateModal(false);

    // Reload the page to ensure we see the latest data
    await page.reload();
    await page.locator('app-customers-pn-container').waitFor({ state: 'visible', timeout: 60000 });
    await page.waitForTimeout(2000);
    const countAfterCopy = await customersPage.getRowCount();
    expect(countAfterCopy).toBe(countAfterCreate + 1);
  });

  test('should cancel customer copy', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    const currentCount = await customersPage.getRowCount();
    if (currentCount > 0) {
      const row = new CustomerRowObject(page, customersPage, 1);
      await row.copy(true);

      const countAfterCancel = await customersPage.getRowCount();
      expect(countAfterCancel).toBe(currentCount);
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
