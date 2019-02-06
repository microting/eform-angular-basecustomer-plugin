import Page from '../../Page objects/Page';

export class CustomersModalPage extends Page {
  constructor() {
    super();
  }

  public get createBtn() {
    return browser.element('#createCustomerBtn');
  }

  public get cancelCreateBtn() {
    return browser.element('#cancelCreateCustomerBtn');
  }

  public get deleteCustomerBtn() {
    return browser.element('#cancelCreateBtn');
  }

  public get saveEditBtn() {
    return browser.element('#saveEditBtn');
  }

  public get cancelEditBtn() {
    return browser.element('#cancelEditBtn');
  }

  public get saveDeleteBtn() {
    return browser.element('#customerSaveDeleteBtn');
  }

  public get cancelDeleteBtn() {
    return browser.element('#customerCancelDeleteBtn');
  }

  public get createCreatedByInput() {
    return browser.element('#createCreatedBy');
  }

  public get editCreatedByInput() {
    return browser.element('#editCreatedBy');
  }

  public get createCustomerNo() {
    return browser.element('#createCustomerNo');
  }

  public get editCustomerNo() {
    return browser.element('#editCustomerNo');
  }

  public get createContactPerson() {
    return browser.element('#createContactPerson');
  }

  public get editContactPerson() {
    return browser.element('#editContactPerson');
  }

  public get createCompanyName() {
    return browser.element('#createCompanyName');
  }

  public get editCompanyName() {
    return browser.element('#editCompanyName');
  }

  public get createCompanyAddress() {
    return browser.element('#createCompanyAddress');
  }

  public get editCompanyAddress() {
    return browser.element('#editCompanyAddress');
  }

  public get createZipCode() {
    return browser.element('#createZipCode');
  }

  public get editZipCode() {
    return browser.element('#editZipCode');
  }

  public get createCityName() {
    return browser.element('#createCityName');
  }

  public get editCityName() {
    return browser.element('#editCityName');
  }

  public get createPhone() {
    return browser.element('#createPhone');
  }

  public get editPhone() {
    return browser.element('#editPhone');
  }

  public get createEmail() {
    return browser.element('#createEmail');
  }

  public get editEmail() {
    return browser.element('#editEmail');
  }

  public createCustomer(data: any) {
    this.createCreatedByInput.setValue(data.createdBy);
    this.createCustomerNo.setValue(data.createCustomerNo);
    this.createContactPerson.setValue(data.createContactPerson);
    this.createCompanyName.setValue(data.createCompanyName);
    this.createCompanyAddress.setValue(data.createCompanyAddress);
    this.createZipCode.setValue(data.createZipCode);
    this.createCityName.setValue(data.createCityName);
    this.createPhone.setValue(data.createPhone);
    this.createEmail.setValue(data.createEmail);
    this.createBtn.click();
    browser.pause(16000);
  }

  public createEmptyCustomer() {
    this.createBtn.click();
    browser.pause(16000);
  }

}

const customersModalPage = new CustomersModalPage();
export default customersModalPage;
