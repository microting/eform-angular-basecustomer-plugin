import Page from '../Page';
import customersPage from './Customers.page';

export class CustomersModalPage extends Page {
  constructor() {
    super();
  }

  public get createBtn() {
    browser.waitForExist('#createCustomerBtn', 50000);
    return browser.element('#createCustomerBtn');
  }

  public get cancelCreateBtn() {
    return browser.element('#cancelCreateCustomerBtn');
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
    return browser.element('#customerDeleteCancelBtn');
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
  public get createCompanyAddress2() {
    return browser.element('#createCompanyAddress2');
  }
  public get editCompanyAddress2() {
    return browser.element('#editCompanyAddress2');
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
  public get createEanCode() {
   return browser.element('#createEanCode');
  }
  public get editEanCode() {
    return browser.element('#editEanCode');
  }
  public get createVatNumber() {
    return browser.element('#createVatNumber');
  }
  public get editVatNumber() {
    return browser.element('#editVatNumber');
  }
  public get createCountryCode() {
    return browser.element('#createCountryCode');
  }
  public get editCountryCode() {
    return browser.element('#editCountryCode');
  }
  public get createCrmId() {
    return browser.element('#createCrmId');
  }
  public get editCrmId() {
    return browser.element('#editCrmId');
  }
  public get createCadastralNumber() {
    return browser.element('#createCadastralNumber');
  }
  public get editCadastralNumber() {
    return browser.element('#editCadastralNumber');
  }
  public get createPropertyNumber() {
    return browser.element('#createPropertyNumber');
  }
  public get editPropertyNumber() {
    return browser.element('#editPropertyNumber');
  }
  public get createApartmentNumber() {
    return browser.element('#createApartmentNumber');
  }
  public get editApartmentNumber() {
    return browser.element('#editApartmentNumber');
  }
  public get createCompletionYear() {
    return browser.element('#createCompletionYear');
  }
  public get editCompletionYear() {
    return browser.element('#editCompletionYear');
  }
  public get createFloorsWithLivingSpace() {
    return browser.element('#createFloorsWithLivingSpace');
  }
  public get editFloorsWithLivingSpace() {
    return browser.element('#editFloorsWithLivingSpace');
  }

  public createCustomer(data: any) {
    this.createCreatedByInput.setValue(data.createdBy);
    this.createCustomerNo.setValue(data.customerNo);
    this.createContactPerson.setValue(data.contactPerson);
    this.createCompanyName.setValue(data.companyName);
    this.createCompanyAddress.setValue(data.companyAddress);
    this.createZipCode.setValue(data.zipCode);
    this.createCityName.setValue(data.cityName);
    this.createPhone.setValue(data.phone);
    this.createEmail.setValue(data.email);
    this.createEanCode.setValue(data.eanCode);
    this.createVatNumber.setValue(data.vatNumber);
    this.createCountryCode.setValue(data.countryCode);
    this.createCadastralNumber.setValue(data.cadastralNumber);
    this.createPropertyNumber.setValue(data.propertyNumber);
    this.createApartmentNumber.setValue(data.apartmentNumber);
    this.createCompletionYear.setValue(data.completionYear);
    this.createFloorsWithLivingSpace.setValue(data.floorsWithLivingSpace);
    this.createBtn.click();
    browser.pause(16000);
  }

  public updateCustomer(data: any) {
    this.editCreatedByInput.setValue(data.createdBy);
    this.editCustomerNo.setValue(data.customerNo);
    this.editContactPerson.setValue(data.contactPerson);
    this.editCompanyName.setValue(data.companyName);
    this.editCompanyAddress.setValue(data.companyAddress);
    this.editZipCode.setValue(data.zipCode);
    this.editCityName.setValue(data.cityName);
    this.editPhone.setValue(data.phone);
    this.editEmail.setValue(data.email);
    this.editEanCode.setValue(data.eanCode);
    this.editVatNumber.setValue(data.vatNumber);
    this.editCountryCode.setValue(data.countryCode);
    this.editCadastralNumber.setValue(data.cadastralNumber);
    this.editPropertyNumber.setValue(data.propertyNumber);
    this.editApartmentNumber.setValue(data.apartmentNumber);
    this.editCompletionYear.setValue(data.completionYear);
    this.editFloorsWithLivingSpace.setValue(data.floorsWithLivingSpace);
    this.saveEditBtn.click();
    browser.pause(16000);
  }

  public createEmptyCustomer() {
    this.createBtn.click();
    browser.pause(16000);
  }

  public deleteCustomer() {
    this.saveDeleteBtn.click();
    browser.pause(16000);
  }

  public cleanup() {
    const deleteObject = customersPage.getCustomer(customersPage.rowNum());
    if (deleteObject != null) {
      browser.pause(8000);
      deleteObject.deleteBtn.click();
      browser.pause(4000);
      this.saveDeleteBtn.click();
      browser.pause(2000);
      browser.refresh();
    }
  }

}

const customersModalPage = new CustomersModalPage();
export default customersModalPage;
