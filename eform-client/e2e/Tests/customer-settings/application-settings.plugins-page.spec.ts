import loginPage from '../../Page objects/Login.page';
import myEformsPage from '../../Page objects/MyEforms.page';
import pluginPage from '../../Page objects/Plugin.page';

import {expect} from 'chai';
import pluginsPage from './application-settings.plugins.page';

describe('Application settings page - site header section', function () {
    before(function () {
        loginPage.open('/auth');
        loginPage.login();
    });
    it('should go to plugin settings page', function () {
        myEformsPage.Navbar.advancedDropdown();
        myEformsPage.Navbar.clickonSubMenuItem('Plugins');
        browser.waitForVisible('#plugin-id', 80000);

        const plugin = pluginsPage.getFirstPluginRowObj();
        expect(plugin.id).equal(1);
        expect(plugin.name).equal('Microting Customers plugin');
        expect(plugin.version).equal('1.0.0.0');
        expect(plugin.status, 'Plugin must be deactivated').equal('Deaktiveret');
        // expect()

    });
    it('should activate the plugin', function () {
        pluginPage.pluginSettingsBtn.click();
        // browser.pause(8000);
        browser.waitForVisible('#PluginDropDown', 80000);
        pluginPage.selectValue('PluginDropDown', 'PluginDropDown', 'Aktiveret');
        // browser.pause(8000);
        pluginPage.saveBtn.click();
        browser.pause(120000);
        browser.refresh();

        browser.waitForVisible('#plugin-id', 80000);
        const plugin = pluginsPage.getFirstPluginRowObj();
        expect(plugin.id).equal(1);
        expect(plugin.name).equal('Microting Customers plugin');
        expect(plugin.version).equal('1.0.0.0');
        expect(plugin.status).equal('Aktiveret');
        expect(browser.element(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Kunder')]`).isExisting()).equal(true);

        // validate that the customers index page is shown with all fields active in the header
    });
});
