import { Page, Locator } from '@playwright/test';

export class LoginPage {
  constructor(private page: Page) {}

  usernameInput(): Locator {
    return this.page.locator('#username');
  }

  passwordInput(): Locator {
    return this.page.locator('#password');
  }

  loginBtn(): Locator {
    return this.page.locator('#loginBtn');
  }

  async login(username = 'admin@admin.com', password = 'Qq1234567$'): Promise<void> {
    await this.page.goto('/');
    await this.usernameInput().fill(username);
    await this.passwordInput().fill(password);
    await this.loginBtn().click();
    await this.page.locator('#sign-out-dropdown').waitFor({ state: 'visible', timeout: 30000 });
  }
}
