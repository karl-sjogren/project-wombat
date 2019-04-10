import Route from '@ember/routing/route';
import { inject } from '@ember/service';
import { hash } from 'rsvp';

export default Route.extend({
  cashierService: inject(),
  productService: inject(),

  model(params) {
    return hash({
      cashier: this.cashierService.getCashier(params.cashier_id),
      products: this.productService.getProducts()
    });
  },

  setupController(controller, models) {
    controller.setProperties(models);

    controller.orderService.joinCashierGroup(models.cashier.id);
  },

  resetController(controller) {
    controller.orderService.leaveCashierGroup();
  }
});
