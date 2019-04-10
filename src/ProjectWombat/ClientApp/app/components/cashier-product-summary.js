import Component from '@ember/component';
import { computed } from '@ember/object';

export default Component.extend({
  total: computed('model.@each.count', function() {
    return this.model.reduce((value, product) => value + (product.cost * product.count), 0);
  }),
});
