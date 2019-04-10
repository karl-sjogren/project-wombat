import Component from '@ember/component';
import { computed } from '@ember/object';

export default Component.extend({
  total: computed('model.rows.{@each.count,rows.[]}', function() {
    return this.model.rows.reduce((value, orderRow) => value + (orderRow.product.cost * orderRow.count), 0);
  }),
});
