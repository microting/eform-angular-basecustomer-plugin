import {PageWithNavbarPage} from '../../Page objects/PageWithNavbar.page';

class ApplicationSettingsPluginsPage extends PageWithNavbarPage {
  constructor() {
    super();
  }

  getFirstPluginRowObj(): PluginRowObject {
    browser.pause(500);
    return new PluginRowObject(1);
  }



  goTopluginSettings() {
    $('#plugin-settings-btn').click();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
  }

  settingsMenu() {
    return $('#PluginDropDown');
  }

  getChoices() {
    return $$('.ng-option');
  }
  saveChangesBtn() {
    $('#saveBtn').click();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
  }
}


const pluginsPage = new ApplicationSettingsPluginsPage();
export default pluginsPage;

class PluginRowObject {
  constructor(rowNum) {
    if ($$('#plugin-id')[rowNum - 1]) {
      this.id = +$$('#plugin-id')[rowNum - 1].getText();
      this.name = $$('#plugin-name')[rowNum - 1].getText();
      this.version = $$('#plugin-version')[rowNum - 1].getText();
      this.settingsBtn = $$('#plugin-settings-link')[rowNum - 1];
    }
  }

  id: number;
  name;
  version;
  status;
  settingsBtn;
}
