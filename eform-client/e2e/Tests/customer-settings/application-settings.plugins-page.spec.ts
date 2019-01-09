import loginPage from '../../Page objects/Login.page';

import {expect} from 'chai';

describe('Application settings page - site header section', function () {
    before(function () {
        loginPage.open('/auth');
    });
    it('should go to plugin settings page', function () {
       loginPage.login();
    });
});
