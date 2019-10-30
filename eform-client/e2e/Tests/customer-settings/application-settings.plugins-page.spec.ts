import loginPage from '../../Page objects/Login.page';
import myEformsPage from '../../Page objects/MyEforms.page';
import pluginPage from '../../Page objects/Plugin.page';

import {expect} from 'chai';
import pluginsPage from './application-settings.plugins.page';

describe('Application settings page - site header section', function () {
    before(function () {
        loginPage.open('/auth');
    });
    it('should go to plugin settings page', function () {
        loginPage.login();
        myEformsPage.Navbar.advancedDropdown();
        myEformsPage.Navbar.clickonSubMenuItem('Plugins');
        browser.waitForExist('#plugin-name', 50000);
        browser.pause(10000);

        const plugin = pluginsPage.getFirstPluginRowObj();
        expect(plugin.id).equal(1);
        expect(plugin.name).equal('Microting Customers plugin');
        expect(plugin.version).equal('1.0.0.0');
        expect(plugin.status, 'Plugin must be deactivated').equal('Deaktiveret');
        // expect()

    });
    it('should activate the plugin', function () {
        pluginPage.pluginSettingsBtn.click();
        browser.waitForVisible('#PluginDropDown', 40000);
        pluginPage.selectValue('PluginDropDown', 'PluginDropDown', 'Aktiveret');
        pluginPage.saveBtn.click();
        browser.pause(50000); // We need to wait 50 seconds for the plugin to create db etc.
        browser.refresh();

        loginPage.login();
        myEformsPage.Navbar.advancedDropdown();
        myEformsPage.Navbar.clickonSubMenuItem('Plugins');
        browser.waitForExist('#plugin-name', 50000);
        browser.pause(10000);
    
        const plugin = pluginsPage.getFirstPluginRowObj();
        expect(plugin.id).equal(1);
        expect(plugin.name).equal('Microting Customers plugin');
        expect(plugin.version).equal('1.0.0.0');
        expect(plugin.status).equal('Aktiveret');
        expect(browser.element(`//*[contains(text(), 'Kunder')]`).isExisting()).equal(true);

    });
});
