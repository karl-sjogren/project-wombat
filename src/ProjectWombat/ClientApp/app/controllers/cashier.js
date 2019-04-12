import Controller from '@ember/controller';
import { computed, observer, set } from '@ember/object';
import { inject } from '@ember/service';

export default Controller.extend({
  orderService: inject('cashierOrderService'),
  cashierService: inject(),

  noActiveOrder: computed('orderService.order', function() {
    return !this.orderService.order;
  }),

  disableEditing: computed('orderService.order.status', function() {
    if(!this.orderService.order) {
      return true;
    }

    if(this.orderService.order.status !== 'preparing') {
      return true;
    }

    return false;
  }),

  orderIdObserver: observer('orderSerice.order.id', function() {
    this.products.forEach(product => {
      set(product, 'count', 0);
    })
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

      if(status === 'paid') {
        this.orderService.set('order', null);
      }
    }
  }
});
