import Route from '@ember/routing/route';
import { inject } from '@ember/service';
import { hash } from 'rsvp';

export default Route.extend({
  cashierService: inject(),

  model() {
    return hash({
      cashiers: this.cashierService.getCashiers()
    });
  },

  setupController(controller, models) {
    controller.setProperties(models);
  }
});
