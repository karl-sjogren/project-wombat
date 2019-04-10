import Component from '@ember/component';
import { computed } from '@ember/object';

export default Component.extend({
  qrCodeUrl: computed('model.orderId', function() {
    return '/api/orders/1234/swish-qr-code';
  })
});
