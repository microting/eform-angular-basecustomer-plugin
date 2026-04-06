import { Page, Locator } from '@playwright/test';

export class CustomersPage {
  constructor(private page: Page) {}

  customersPnButton(): Locator {
    return this.page.locator('#customers-pn');
  }

  async goToCustomers(): Promise<void> {
    try {
      await this.customersPnButton().waitFor({ state: 'visible', timeout: 10000 });
      await this.customersPnButton().click();
    } catch {
      // If nav button not visible, navigate directly
      await this.page.goto('/plugins/customers-pn');
    }
    await this.page.locator('app-customers-pn-container').waitFor({ state: 'visible', timeout: 60000 });
  }

  // Subheader buttons
  newCustomerBtn(): Locator {
    return this.page.locator('#newCustomerBtn');
  }

  importCustomerBtn(): Locator {
    return this.page.locator('#importCustomer');
  }

  searchInput(): Locator {
    return this.page.locator('#searchInput');
  }

  // Create modal locators
  createCompanyName(): Locator {
    return this.page.locator('#createCompanyName');
  }

  createCompanyAddress(): Locator {
    return this.page.locator('#createCompanyAddress');
  }

  createZipCode(): Locator {
    return this.page.locator('#createZipCode');
  }

  createCityName(): Locator {
    return this.page.locator('#createCityName');
  }

  createPhone(): Locator {
    return this.page.locator('#createPhone');
  }

  createEmail(): Locator {
    return this.page.locator('#createEmail');
  }

  createContactPerson(): Locator {
    return this.page.locator('#createContactPerson');
  }

  createEanCode(): Locator {
    return this.page.locator('#createEanCode');
  }

  createVatNumber(): Locator {
    return this.page.locator('#createVatNumber');
  }

  saveCreateBtn(): Locator {
    return this.page.locator('#saveCreateBtn');
  }

  cancelCreateBtn(): Locator {
    return this.page.locator('#cancelCreateBtn');
  }

  // Edit modal locators
  editCompanyName(): Locator {
    return this.page.locator('#editCompanyName');
  }

  editCompanyAddress(): Locator {
    return this.page.locator('#editCompanyAddress');
  }

  editPhone(): Locator {
    return this.page.locator('#editPhone');
  }

  editEmail(): Locator {
    return this.page.locator('#editEmail');
  }

  editContactPerson(): Locator {
    return this.page.locator('#editContactPerson');
  }

  saveEditBtn(): Locator {
    return this.page.locator('#saveEditBtn');
  }

  cancelEditBtn(): Locator {
    return this.page.locator('#cancelEditBtn');
  }

  // Delete modal locators
  confirmDeleteBtn(): Locator {
    return this.page.locator('#confirmDeleteBtn');
  }

  cancelDeleteBtn(): Locator {
    return this.page.locator('#cancelDeleteBtn');
  }

  // Table
  tableRows(): Locator {
    return this.page.locator('.mat-mdc-row');
  }

  async getRowCount(): Promise<number> {
    return this.tableRows().count();
  }

  async createCustomer(customer: CustomerCreateUpdate, clickCancel = false): Promise<void> {
    await this.openCreateModal(customer);
    await this.closeCreateModal(clickCancel);
  }

  async openCreateModal(customer?: CustomerCreateUpdate): Promise<void> {
    await this.newCustomerBtn().click();
    await this.cancelCreateBtn().waitFor({ state: 'visible' });
    if (customer) {
      if (customer.companyName) {
        await this.createCompanyName().fill(customer.companyName);
      }
      if (customer.companyAddress) {
        await this.createCompanyAddress().fill(customer.companyAddress);
      }
      if (customer.zipCode) {
        await this.createZipCode().fill(customer.zipCode);
      }
      if (customer.cityName) {
        await this.createCityName().fill(customer.cityName);
      }
      if (customer.phone) {
        await this.createPhone().fill(customer.phone);
      }
      if (customer.email) {
        await this.createEmail().fill(customer.email);
      }
      if (customer.contactPerson) {
        await this.createContactPerson().fill(customer.contactPerson);
      }
    }
  }

  async closeCreateModal(clickCancel = false): Promise<void> {
    if (clickCancel) {
      await this.cancelCreateBtn().click();
    } else {
      const [_response] = await Promise.all([
        this.page.waitForResponse(
          r => r.url().includes('/api/customers-pn/customers') && r.request().method() === 'POST'
        ),
        this.saveCreateBtn().click(),
      ]);
    }
    await this.page.waitForTimeout(500);
  }

  async clearTable(): Promise<void> {
    const count = await this.getRowCount();
    for (let i = count; i > 0; i--) {
      const row = new CustomerRowObject(this.page, this, 1);
      await row.delete();
    }
  }
}

export class CustomerRowObject {
  constructor(
    private page: Page,
    private parentPage: CustomersPage,
    private rowNum?: number,
    private customerName?: string
  ) {
    this.rowNum = rowNum ?? 1;
  }

  private getRowLocator(): Locator {
    if (this.customerName) {
      return this.page
        .locator('.mat-mdc-row')
        .filter({ hasText: this.customerName })
        .first();
    }
    return this.page.locator('.mat-mdc-row').nth((this.rowNum ?? 1) - 1);
  }

  async openActionMenu(): Promise<void> {
    const row = this.getRowLocator();
    await row.locator('[id^="actionMenu"]').click();
  }

  async openEditModal(): Promise<void> {
    await this.openActionMenu();
    await this.page.locator('[id^="editCustomerBtn"]').first().click();
    await this.parentPage.cancelEditBtn().waitFor({ state: 'visible' });
  }

  async openDeleteModal(): Promise<void> {
    await this.openActionMenu();
    await this.page.locator('[id^="deleteCustomerBtn"]').first().click();
    await this.parentPage.confirmDeleteBtn().waitFor({ state: 'visible' });
  }

  async openCopyModal(): Promise<void> {
    await this.openActionMenu();
    await this.page.locator('[id^="copyCustomerBtn"]').first().click();
    await this.parentPage.cancelCreateBtn().waitFor({ state: 'visible' });
  }

  async delete(clickCancel = false): Promise<void> {
    await this.openDeleteModal();
    if (clickCancel) {
      await this.parentPage.cancelDeleteBtn().click();
    } else {
      const [_response] = await Promise.all([
        this.page.waitForResponse(
          r => r.url().includes('/api/customers-pn/customers') && r.request().method() === 'DELETE'
        ),
        this.parentPage.confirmDeleteBtn().click(),
      ]);
    }
    await this.page.waitForTimeout(500);
  }

  async edit(customer: CustomerCreateUpdate, clickCancel = false): Promise<void> {
    await this.openEditModal();
    if (customer.companyName) {
      await this.parentPage.editCompanyName().clear();
      await this.parentPage.editCompanyName().fill(customer.companyName);
    }
    if (customer.phone) {
      await this.parentPage.editPhone().clear();
      await this.parentPage.editPhone().fill(customer.phone);
    }
    if (customer.email) {
      await this.parentPage.editEmail().clear();
      await this.parentPage.editEmail().fill(customer.email);
    }
    if (clickCancel) {
      await this.parentPage.cancelEditBtn().click();
    } else {
      const [_response] = await Promise.all([
        this.page.waitForResponse(
          r => r.url().includes('/api/customers-pn/customers') && r.request().method() === 'PUT'
        ),
        this.parentPage.saveEditBtn().click(),
      ]);
    }
    await this.page.waitForTimeout(500);
  }

  async copy(clickCancel = false): Promise<void> {
    await this.openCopyModal();
    if (clickCancel) {
      await this.parentPage.cancelCreateBtn().click();
    } else {
      await this.parentPage.closeCreateModal(false);
    }
  }

  async getRowText(): Promise<string> {
    return this.getRowLocator().innerText();
  }
}

export class CustomerCreateUpdate {
  companyName?: string;
  companyAddress?: string;
  zipCode?: string;
  cityName?: string;
  phone?: string;
  email?: string;
  contactPerson?: string;
}
