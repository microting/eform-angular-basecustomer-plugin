import { AfterContentInit, Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AuthStateService } from 'src/app/common/store';
import { translates } from './../i18n/translates';

@Component({
  selector: 'app-customers-pn-layout',
  template: `<router-outlet></router-outlet>`,
})
export class CustomerPnLayoutComponent implements AfterContentInit, OnInit {
  constructor(
    private translateService: TranslateService,
    private authStateService: AuthStateService
  ) {}

  ngOnInit() {}

  ngAfterContentInit() {
    const lang = this.authStateService.currentUserLocale;
    const i18n = translates[lang];
    this.translateService.setTranslation(lang, i18n, true);
  }
}
