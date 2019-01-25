import loginPage from '../../Page objects/Login.page';
import myEformsPage from '../../Page objects/MyEforms.page';

import {expect} from 'chai';
import pluginsPage from './application-settings.plugins.page';

describe('Application settings page - site header section', function () {
    before(function () {
        loginPage.open('/auth');
    });
    it('should create searchable list', function () {
        loginPage.login();
        // myEformsPage.Navbar.advancedDropdown();
        // myEformsPage.Navbar.clickonSubMenuItem('Plugin Settings');
        // browser.pause(8000);
        //
        // const plugin = pluginsPage.getFirstPluginRowObj();
        // expect(plugin.id).equal(1);
        // expect(plugin.name).equal('Microting Customers plugin');
        // expect(plugin.version).equal('1.0.0.0');
        // expect(plugin.status).equal('Deaktiveret');
        // // expect()

    });

    it('should configure customers pn to use searchable list', function () {

    });

    it('should create a single customer', function () {
        // create customer
        // validate it's created
        // go to searchable list and validate that the list contains the newly created customer
    });

    it('should edit a customer', function () {
        // edit the customer
        // validate that the customer is updated
        // go to searchable list and validate that the entity is updated
    });

    it('should delete a customer', function () {
        // delete a customer
        // validate that the warning dialog is shown
        // validate that if pressing cancel, the customer is still in the list
        // delete the customer and click ok
        // validate that the list is now empty
        // go to searchable list and validate that the list no longer contains any customers
    });

    it('should import a list of 2 customers', function () {
        // click import
        // mark correct collumns for import
        // click on import
        // refresh page
        // validate that the list contains 2 customers
        // go to searchable list and validate that both customers are in the list
        // delete both customers
        // go to searchable list and validate that the list is now empty
    });

    it('should select only companyname and customer no for show', function () {
        // go to customer settings
        // unselect all checkboxes except companyname and customner no
        // save changes
        // validate that there is only shown companyname and customer no in the headers section
    });

});
