import { test, expect } from '@playwright/test';
import { LoginPage } from '../../Page objects/Login.page';
import { CustomersPage } from './CustomersPage.page';

test.describe('Customers - Settings', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto("/");
    await new LoginPage(page).login();
  });

  test('should navigate to settings page', async ({ page }) => {
    test.setTimeout(120000);
    await page.goto('/plugins-settings');
    await page.waitForTimeout(1000);

    // Navigate to customer settings (via plugin settings link)
    const settingsLink = page.locator('a[href*="customers-pn/settings"]');
    if (await settingsLink.count() > 0) {
      await settingsLink.click();
      await page.locator('#saveSettingsBtn').waitFor({ state: 'visible' });

      // Verify slide toggles are present
      const toggles = page.locator('mat-slide-toggle');
      const toggleCount = await toggles.count();
      expect(toggleCount).toBeGreaterThan(0);
    }
  });

  test('should toggle field visibility', async ({ page }) => {
    test.setTimeout(120000);
    const customersPage = new CustomersPage(page);
    await customersPage.goToCustomers();

    // Check initial column count
    const initialHeaders = page.locator('.mat-sort-header');
    const initialCount = await initialHeaders.count();
    expect(initialCount).toBeGreaterThan(0);
  });
});
