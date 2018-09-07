import {AfterViewInit, Component} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {LocaleService} from 'src/app/common/services/auth';
declare var require: any;

@Component({
  selector: 'app-customers-pn-layout',
  template: `<router-outlet></router-outlet>`
})
export class CustomerPnLayoutComponent implements AfterViewInit {
  constructor(private localeService: LocaleService, private translateService: TranslateService) {

  }

  ngAfterViewInit() {
    setTimeout(() => {
      const lang = this.localeService.getCurrentUserLocale();
      const i18n = require(`../i18n/${lang}.json`);
      this.translateService.setTranslation(lang, i18n, true);
    }, 1000);

  }
}
