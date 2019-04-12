import Component from '@ember/component';
import { computed } from '@ember/object';

export default Component.extend({
  qrCodeUrl: computed('model.id', function() {
    return `/api/orders/${this.model.id}/swish-qr-code`;
  })
});
