import Component from '@ember/component';
import { computed } from '@ember/object';
import { inject } from '@ember/service';

export default Component.extend({
  tagName: 'li',
  orderService: inject('cashier-order-service'),
  showButtons: false,

  total: computed('model.count', function() {
    return this.model.count * this.model.cost;
  }),

  actions: {
    increase() {
      this.incrementProperty('model.count');
      this.orderService.updateOrderRow(this.order.id, this.model.id, this.model.count);
    },

    decrease() {
      this.decrementProperty('model.count');
      if(this.model.count < 0) {
        this.set('model.count', 0);
      }
      this.orderService.updateOrderRow(this.order.id, this.model.id, this.model.count);
    }
  }
});
