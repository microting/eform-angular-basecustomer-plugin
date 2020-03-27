import {PageWithNavbarPage} from '../../Page objects/PageWithNavbar.page';

class ApplicationSettingsPluginsPage extends PageWithNavbarPage {
    constructor() {
        super();
    }

    getFirstPluginRowObj(): PluginRowObject {
        return new PluginRowObject(1);
    }



    goTopluginSettings() {
      browser.$('#plugin-settings-btn').click();
        $('#spinner-animation').waitForDisplayed(90000, true);
    }

    settingsMenu() {
      return browser.$('#PluginDropDown');
    }

    getChoices() {
      return browser.$$('.ng-option');
    }
    saveChangesBtn() {
      browser.$('#saveBtn').click();
    $('#spinner-animation').waitForDisplayed(90000, true);
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
