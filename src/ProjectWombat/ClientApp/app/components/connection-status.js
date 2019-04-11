import Component from '@ember/component';
import { computed } from '@ember/object';

export default Component.extend({
  connectedService: null, // This should NOT be injected but passed along,

  isConnecting: computed('connectedService.connecting', function() {
    return this.connectedService.connecting;
  }),

  isConnected: computed('connectedService.connecting', function() {
    return this.connectedService.connected;
  }),

  isDisconnected: computed('connectedService.connecting', function() {
    return !this.connectedService.connecting && !this.connectedService.connected;
  })
});
