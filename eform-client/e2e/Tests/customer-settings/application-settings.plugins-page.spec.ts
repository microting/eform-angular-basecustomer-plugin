import loginPage from '../../Page objects/Login.page';
import myEformsPage from '../../Page objects/MyEforms.page';

import {expect} from 'chai';

describe('Application settings page - site header section', function () {
    before(function () {
        loginPage.open('/auth');
    });
    it('should go to plugin settings page', function () {
       loginPage.login();
       myEformsPage.Navbar.advancedDropdown();
       myEformsPage.Navbar.clickonSubMenuItem('PluginSettings');
       browser.pause(8000);
       // expect()

    });
});
