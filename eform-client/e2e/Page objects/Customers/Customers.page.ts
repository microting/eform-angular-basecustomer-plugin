import { PageWithNavbarPage } from '../PageWithNavbar.page';
import { generateRandmString } from '../../Helpers/helper-functions';

export class CustomersPage extends PageWithNavbarPage {
  constructor() {
    super();
  }

  public get rowNum(): number {
    return browser.$$('#mainTableBody > tr').length;
  }

  public get idTableHeader() {
    const ele = $('#idTableHeader');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get contactPersonTableHeader() {
    const ele = $('#contactPersonTableHeader');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get companyNameTableHeader() {
    const ele = $('#companyNameTableHeader');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCustomerBtn() {
    const ele = $('#createCustomerBtn');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get cancelCreateCustomerBtn() {
    const ele = $('#cancelCreateCustomerBtn');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get saveEditBtn() {
    const ele = $('#saveEditBtn');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get cancelEditBtn() {
    const ele = $('#cancelEditBtn');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get customerSaveDeleteBtn() {
    const ele = $('#customerSaveDeleteBtn');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get customerDeleteCancelBtn() {
    const ele = $('#customerDeleteCancelBtn');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCreatedByInput() {
    const ele = $('#createCreatedBy');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCreatedByInput() {
    const ele = $('#editCreatedBy');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCustomerNo() {
    const ele = $('#createCustomerNo');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCustomerNo() {
    const ele = $('#editCustomerNo');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createContactPerson() {
    const ele = $('#createContactPerson');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editContactPerson() {
    const ele = $('#editContactPerson');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCompanyName() {
    const ele = $('#createCompanyName');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCompanyName() {
    const ele = $('#editCompanyName');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCompanyAddress() {
    const ele = $('#createCompanyAddress');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCompanyAddress() {
    const ele = $('#editCompanyAddress');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCompanyAddress2() {
    const ele = $('#createCompanyAddress2');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCompanyAddress2() {
    const ele = $('#editCompanyAddress2');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createZipCode() {
    const ele = $('#createZipCode');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editZipCode() {
    const ele = $('#editZipCode');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCityName() {
    const ele = $('#createCityName');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCityName() {
    const ele = $('#editCityName');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createPhone() {
    const ele = $('#createPhone');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editPhone() {
    const ele = $('#editPhone');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createEmail() {
    const ele = $('#createEmail');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editEmail() {
    const ele = $('#editEmail');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createEanCode() {
    const ele = $('#createEanCode');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editEanCode() {
    const ele = $('#editEanCode');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createVatNumber() {
    const ele = $('#createVatNumber');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editVatNumber() {
    const ele = $('#editVatNumber');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCountryCode() {
    const ele = $('#createCountryCode');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCountryCode() {
    const ele = $('#editCountryCode');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCrmId() {
    const ele = $('#createCrmId');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCrmId() {
    const ele = $('#editCrmId');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCadastralNumber() {
    const ele = $('#createCadastralNumber');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCadastralNumber() {
    const ele = $('#editCadastralNumber');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createPropertyNumber() {
    const ele = $('#createPropertyNumber');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editPropertyNumber() {
    const ele = $('#editPropertyNumber');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createApartmentNumber() {
    const ele = $('#createApartmentNumber');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editApartmentNumber() {
    const ele = $('#editApartmentNumber');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createCompletionYear() {
    const ele = $('#createCompletionYear');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editCompletionYear() {
    const ele = $('#editCompletionYear');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get createFloorsWithLivingSpace() {
    const ele = $('#createFloorsWithLivingSpace');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get editFloorsWithLivingSpace() {
    const ele = $('#editFloorsWithLivingSpace');
    ele.waitForDisplayed({ timeout: 20000 });
    // ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get newCustomerBtn() {
    const ele = $('#newCustomerBtn');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get importCustomerBtn() {
    const ele = $('#importCustomer');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public get customersButton() {
    const ele = $('#customers-pn');
    ele.waitForDisplayed({ timeout: 20000 });
    ele.waitForClickable({ timeout: 20000 });
    return ele;
  }

  public getCustomerValue(selector: any, row: number) {
    if (selector.includes('Id')) {
      return parseInt(
        $('#mainTableBody')
          .$(`tr:nth-child(${row})`)
          .$('#' + selector)
          .getText(),
        10
      );
    } else {
      return $('#mainTableBody')
        .$(`tr:nth-child(${row})`)
        .$('#' + selector)
        .getText();
    }
  }

  getCustomer(num): CustomersRowObject {
    return new CustomersRowObject(num);
  }

  public goToCustomersPage() {
    this.customersButton.click();
    $('#spinner-animation').waitForDisplayed({ timeout: 20000, reverse: true });
    this.newCustomerBtn.waitForClickable({ timeout: 20000 });
  }

  public openCreateModal(data?: CreateUpdateCustomer) {
    this.newCustomerBtn.click();
    this.cancelCreateCustomerBtn.waitForClickable({ timeout: 20000 });
    if (data) {
      if (data.createdBy) {
        this.createCreatedByInput.setValue(data.createdBy);
      }
      if (data.customerNo) {
        this.createCustomerNo.setValue(data.customerNo);
      }
      if (data.contactPerson) {
        this.createContactPerson.setValue(data.contactPerson);
      }
      if (data.companyName) {
        this.createCompanyName.setValue(data.companyName);
      }
      if (data.companyAddress) {
        this.createCompanyAddress.setValue(data.companyAddress);
      }
      if (data.zipCode) {
        this.createZipCode.setValue(data.zipCode);
      }
      if (data.cityName) {
        this.createCityName.setValue(data.cityName);
      }
      if (data.phone) {
        this.createPhone.setValue(data.phone);
      }
      if (data.email) {
        this.createEmail.setValue(data.email);
      }
      if (data.eanCode) {
        this.createEanCode.setValue(data.eanCode);
      }
      if (data.vatNumber) {
        this.createVatNumber.setValue(data.vatNumber);
      }
      if (data.countryCode) {
        this.createCountryCode.setValue(data.countryCode);
      }
      if (data.cadastralNumber) {
        this.createCadastralNumber.setValue(data.cadastralNumber);
      }
      if (data.propertyNumber) {
        this.createPropertyNumber.setValue(data.propertyNumber);
      }
      if (data.apartmentNumber) {
        this.createApartmentNumber.setValue(data.apartmentNumber);
      }
      if (data.completionYear) {
        this.createCompletionYear.setValue(data.completionYear);
      }
      if (data.floorsWithLivingSpace) {
        this.createFloorsWithLivingSpace.setValue(data.floorsWithLivingSpace);
      }
    } else {
      this.createCreatedByInput.setValue(generateRandmString());
    }
  }

  public closeCreateModal(clickCancel = false) {
    if (clickCancel) {
      this.cancelCreateCustomerBtn.click();
    } else {
      this.createCustomerBtn.click();
    }
    this.newCustomerBtn.waitForClickable({ timeout: 20000 });
  }

  public create(customer?: CreateUpdateCustomer, clickCancel = false) {
    this.openCreateModal(customer);
    this.closeCreateModal(clickCancel);
  }

  createDummyCustomers(num: number) {
    for (let i = 0; i < num; i++) {
      const customer = new CreateUpdateCustomer();
      customer.contactPerson = generateRandmString();
      customer.companyName = generateRandmString();
      this.create(customer);
    }
  }

  public clearTable() {
    browser.pause(2000);
    const rowCount = this.rowNum;
    for (let i = 1; i <= rowCount; i++) {
      const firstRowObject = this.getFirstRowObject();
      firstRowObject.delete();
    }
  }

  getFirstRowObject(): CustomersRowObject {
    return this.getCustomer(1);
  }

  public goToImportBtn() {
    this.importCustomerBtn.click();
    $('#spinner-animation').waitForDisplayed({ timeout: 20000, reverse: true });
  }
}

const customersPage = new CustomersPage();
export default customersPage;

export class CustomersRowObject {
  constructor(rowNumber) {
    this.element = $$('#mainTableBody > tr')[rowNumber - 1];
    if (this.element) {
      this.id = +this.element.$('#idCustomer').getText();
      this.createdBy = this.element.$('#createdBy').getText();
      this.customerNo = this.element.$('#customerNo').getText();
      this.contactPerson = this.element.$('#contactPerson').getText();
      this.companyName = this.element.$('#companyName').getText();
      this.companyAddress = this.element.$('#companyAddress').getText();
      this.zipCode = this.element.$('#zipCode').getText();
      this.cityName = this.element.$('#cityName').getText();
      this.phone = this.element.$('#phone').getText();
      this.email = this.element.$('#email').getText();
      this.vatNumber = this.element.$('#vatNumber').getText();
      this.countryCode = this.element.$('#countryCode').getText();
      this.cadastralNumber = this.element.$('#cadastralNumber').getText();
      this.crmId = this.element.$('#crmId').getText();
      this.propertyNumber = +this.element.$('#propertyNumber').getText();
      this.apartmentNumber = +this.element.$('#apartmentNumber').getText();
      this.completionYear = +this.element.$('#completionYear').getText();
      this.floorsWithLivingSpace = +this.element
        .$('#floorsWithLivingSpace')
        .getText();
      this.editBtn = this.element.$('#editCustomerBtn');
      this.copyBtn = this.element.$('#copyCustomerBtn');
      this.deleteBtn = this.element.$('#deleteCustomerBtn');
    }
  }

  public element: WebdriverIO.Element;
  public id: number;
  public version: number;
  // public updatedByUserId: number;
  public createdBy: string;
  public customerNo: string;
  public contactPerson: string;
  public companyName: string;
  public companyAddress: string;
  public companyAddress2: string;
  public zipCode: string;
  public cityName: string;
  public phone: string;
  public email: string;
  public vatNumber: string;
  public countryCode: string;
  public cadastralNumber: string;
  public crmId: string;
  public propertyNumber: number;
  public apartmentNumber: number;
  public completionYear: number;
  public floorsWithLivingSpace: number;
  public editBtn: WebdriverIO.Element;
  public copyBtn: WebdriverIO.Element;
  public deleteBtn: WebdriverIO.Element;

  public openDeleteModal() {
    this.deleteBtn.click();
    customersPage.customerDeleteCancelBtn.waitForClickable({ timeout: 20000 });
  }

  public closeDeleteModal(clickCancel = false) {
    if (clickCancel) {
      customersPage.customerDeleteCancelBtn.click();
    } else {
      customersPage.customerSaveDeleteBtn.click();
    }
    customersPage.newCustomerBtn.waitForClickable({ timeout: 20000 });
  }

  public delete(clickCancel = false) {
    this.openDeleteModal();
    this.closeDeleteModal(clickCancel);
  }

  public openEditModal(data?: CreateUpdateCustomer) {
    this.editBtn.click();
    customersPage.cancelEditBtn.waitForClickable({ timeout: 20000 });
    if (data) {
      if (data.createdBy) {
        customersPage.editCreatedByInput.setValue(data.createdBy);
      }
      if (data.customerNo) {
        customersPage.editCustomerNo.setValue(data.customerNo);
      }
      if (data.contactPerson) {
        customersPage.editContactPerson.setValue(data.contactPerson);
      }
      if (data.companyName) {
        customersPage.editCompanyName.setValue(data.companyName);
      }
      if (data.companyAddress) {
        customersPage.editCompanyAddress.setValue(data.companyAddress);
      }
      if (data.zipCode) {
        customersPage.editZipCode.setValue(data.zipCode);
      }
      if (data.cityName) {
        customersPage.editCityName.setValue(data.cityName);
      }
      if (data.phone) {
        customersPage.editPhone.setValue(data.phone);
      }
      if (data.email) {
        customersPage.editEmail.setValue(data.email);
      }
      if (data.eanCode) {
        customersPage.editEanCode.setValue(data.eanCode);
      }
      if (data.vatNumber) {
        customersPage.editVatNumber.setValue(data.vatNumber);
      }
      if (data.countryCode) {
        customersPage.editCountryCode.setValue(data.countryCode);
      }
      if (data.cadastralNumber) {
        customersPage.editCadastralNumber.setValue(data.cadastralNumber);
      }
      if (data.propertyNumber) {
        customersPage.editPropertyNumber.setValue(data.propertyNumber);
      }
      if (data.apartmentNumber) {
        customersPage.editApartmentNumber.setValue(data.apartmentNumber);
      }
      if (data.completionYear) {
        customersPage.editCompletionYear.setValue(data.completionYear);
      }
      if (data.floorsWithLivingSpace) {
        customersPage.editFloorsWithLivingSpace.setValue(
          data.floorsWithLivingSpace
        );
      }
    }
  }

  public closeEditModal(clickCancel = false) {
    if (clickCancel) {
      customersPage.cancelEditBtn.click();
    } else {
      customersPage.saveEditBtn.click();
    }
    customersPage.newCustomerBtn.waitForClickable({ timeout: 20000 });
  }

  public edit(customer?: CreateUpdateCustomer, clickCancel = false) {
    this.openEditModal(customer);
    this.closeEditModal(clickCancel);
  }

  public openCopyModal(customer?: CreateUpdateCustomer) {
    this.copyBtn.click();
    customersPage.cancelCreateCustomerBtn.waitForClickable({ timeout: 20000 });
    if (customer) {
      if (customer.createdBy && this.createdBy !== customer.createdBy) {
        customersPage.createCreatedByInput.setValue(customer.createdBy);
      }
      if (customer.customerNo && this.customerNo !== customer.customerNo) {
        customersPage.createCustomerNo.setValue(customer.customerNo);
      }
      if (
        customer.contactPerson &&
        this.contactPerson !== customer.contactPerson
      ) {
        customersPage.createContactPerson.setValue(customer.contactPerson);
      }
      if (customer.companyName && this.companyName !== customer.companyName) {
        customersPage.createCompanyName.setValue(customer.companyName);
      }
      if (
        customer.companyAddress &&
        this.companyAddress !== customer.companyAddress
      ) {
        customersPage.createCompanyAddress.setValue(customer.companyAddress);
      }
      if (customer.zipCode && this.zipCode !== customer.zipCode) {
        customersPage.createZipCode.setValue(customer.zipCode);
      }
      if (customer.cityName && this.cityName !== customer.cityName) {
        customersPage.createCityName.setValue(customer.cityName);
      }
      if (customer.phone && this.phone !== customer.phone) {
        customersPage.createPhone.setValue(customer.phone);
      }
      if (customer.email && this.email !== customer.email) {
        customersPage.createEmail.setValue(customer.email);
      }
      if (customer.eanCode) {
        customersPage.createEanCode.setValue(customer.eanCode);
      }
      if (customer.vatNumber && this.vatNumber !== customer.vatNumber) {
        customersPage.createVatNumber.setValue(customer.vatNumber);
      }
      if (customer.countryCode && this.countryCode !== customer.countryCode) {
        customersPage.createCountryCode.setValue(customer.countryCode);
      }
      if (
        customer.cadastralNumber &&
        this.cadastralNumber !== customer.cadastralNumber
      ) {
        customersPage.createCadastralNumber.setValue(customer.cadastralNumber);
      }
      if (
        customer.propertyNumber &&
        this.propertyNumber !== customer.propertyNumber
      ) {
        customersPage.createPropertyNumber.setValue(customer.propertyNumber);
      }
      if (
        customer.apartmentNumber &&
        this.apartmentNumber !== customer.apartmentNumber
      ) {
        customersPage.createApartmentNumber.setValue(customer.apartmentNumber);
      }
      if (
        customer.completionYear &&
        this.completionYear !== customer.completionYear
      ) {
        customersPage.createCompletionYear.setValue(customer.completionYear);
      }
      if (
        customer.floorsWithLivingSpace &&
        this.floorsWithLivingSpace !== customer.floorsWithLivingSpace
      ) {
        customersPage.createFloorsWithLivingSpace.setValue(
          customer.floorsWithLivingSpace
        );
      }
    }
  }

  public closeCopyModal(clickCancel = false) {
    if (clickCancel) {
      customersPage.cancelCreateCustomerBtn.click();
    } else {
      customersPage.createCustomerBtn.click();
    }
    customersPage.newCustomerBtn.waitForClickable({ timeout: 20000 });
  }

  copy(customer?: CreateUpdateCustomer, clickCancel = false) {
    this.openCopyModal(customer);
    this.closeCopyModal(clickCancel);
  }
}

export class CreateUpdateCustomer {
  createdBy: string | null;
  customerNo: string | null;
  contactPerson: string | null;
  companyName: string | null;
  companyAddress: string | null;
  zipCode: string | null;
  cityName: string | null;
  phone: string | null;
  email: string | null;
  eanCode: string | null;
  vatNumber: string | null;
  countryCode: string | null;
  cadastralNumber: string | null;
  propertyNumber: number | null;
  apartmentNumber: number | null;
  completionYear: number | null;
  floorsWithLivingSpace: number | null;
}
