import Controller from '@ember/controller';
import { computed } from '@ember/object';
import { inject } from '@ember/service';

export default Controller.extend({
  orderService: inject('cashierOrderService'),
  cashierService: inject(),

  disableEditing: computed('orderService.order.status', function() {
    if(!this.orderService.order) {
      return true;
    }

    if(this.orderService.order.status !== 'preparing') {
      return true;
    }

    return false;
  }),

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
