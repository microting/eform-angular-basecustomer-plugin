import { AfterContentInit, Component, OnInit,
  inject
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Store } from '@ngrx/store';
import { addPluginToVisited, selectPluginsVisitedPlugins } from 'src/app/state';
import { take } from 'rxjs';
import { translates } from './../i18n/translates';

@Component({
  selector: 'app-customers-pn-layout',
  template: `<router-outlet></router-outlet>`,
  standalone: false
})
export class CustomerPnLayoutComponent implements AfterContentInit, OnInit {
  private translateService = inject(TranslateService);
  private store = inject(Store);

  private pluginName = 'customers-pn';

  
  constructor() {
    this.store.select(selectPluginsVisitedPlugins)
      .pipe(take(1))
      .subscribe(x => {
        // check current plugin in activated plugin
        if (x.findIndex(y => y === this.pluginName) === -1) {
          // add all plugin translates one time
          Object.keys(translates).forEach(locale => {
            this.translateService.setTranslation(locale, translates[locale], true);
          });
          // add plugin to visited plugins
          this.store.dispatch(addPluginToVisited(this.pluginName));
        }
      });
  }

  ngOnInit() {}

  ngAfterContentInit() {}
}
