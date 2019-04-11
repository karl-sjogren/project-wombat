import Route from '@ember/routing/route';
import { inject } from '@ember/service';

export default Route.extend({
  intl: inject(),

  beforeModel() {
    return this.intl.setLocale(['sv-SE', 'en-us']);
  }
});