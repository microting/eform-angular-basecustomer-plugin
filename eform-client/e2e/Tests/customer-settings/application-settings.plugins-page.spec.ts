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
       browser.pause(8000);

      const plugin = pluginsPage.getFirstPluginRowObj();
      expect(plugin.id).equal(1);
      expect(plugin.name).equal('Microting Customers plugin');
      expect(plugin.version).equal('1.0.0.0');
      expect(plugin.status).equal('Deaktiveret');
       // expect()

    });
    it('should activate the plugin', function () {
      pluginPage.pluginSettingsBtn.click();
      browser.pause(8000);
      pluginPage.selectValue('PluginDropDown', 'PluginDropDown', 'Aktiveret');
      browser.pause(8000);
      pluginPage.saveBtn.click();
      browser.pause(40000);
      browser.refresh();

      const plugin = pluginsPage.getFirstPluginRowObj();
      expect(plugin.id).equal(1);
      expect(plugin.name).equal('Microting Customers plugin');
      expect(plugin.version).equal('1.0.0.0');
      expect(plugin.status).equal('Aktiveret');
        // click on plugin settings
      pluginsPage.goTopluginSettings();
        // enter connectionstring for customers plugin
        // select activate
      const menu = browser.$('#PluginDropDown');
      menu.click();
      const choices = browser.$$('.ng-option');
      choices[0].click();
      const saveBtn = browser.$('#saveBtn');
        // save changes
      saveBtn.click();
        // see that the plugin is marked active
      const plugin = pluginsPage.getFirstPluginRowObj();
      expect(plugin.status).equal('Aktiveret');
        // validate that the customers menu entry is now visible
      const customersMenu = browser.$('#customers-pn').getText();
      expect(customersMenu, 'Customers not present in header menu').equal( 'Kunder' );
        // validate that the customers index page is shown with all fields active in the header
    });
});
