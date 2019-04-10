import Controller from '@ember/controller';
import { inject } from '@ember/service';

export default Controller.extend({
  orderService: inject('cashierOrderService'),
  cashierService: inject(),

  actions: {
    newOrder() {
      this.orderService.createOrder().then(order => {
        this.cashierService.setActiveOrder(this.cashier.id, order.id);
      });
    },

    setStatus(status) {
      this.orderService.setOrderStatus(this.orderService.order.id, status);
      this.set('orderService.order.status', status);
    }
  }
});
