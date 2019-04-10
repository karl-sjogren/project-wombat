import Service from '@ember/service';
import signalR from 'signalr';
import fetch from 'fetch';

const STATE_CONNECTED = 1;

export default Service.extend({
  order: null,
  cashierId: null,
  connecting: true,
  connected: false,

  init() {
    this._super(...arguments);

    let connectionUrl = `/hubs/cashier`;
    let hub = new signalR.HubConnectionBuilder()
      .withUrl(connectionUrl)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    hub.onclose(() => {
      this.set('connected', false);
      window.setTimeout(() => this.connect(), 5000);
    });

    hub.on('OrderActivated', orderId => {
      this.getOrder(orderId).then(order => {
        this.set('order', order);
      });
    });

    hub.on('OrderUpdated', order => {
      this.set('order', order);
    });

    this.set('hub', hub);
    this.connect();
  },

  joinCashierGroup(cashierId) {
    if(!!this.cashierId) {
      this.leaveCashierGroup();
    }
    this.set('cashierId', cashierId);

    if(this.hub.connection.connectionState === STATE_CONNECTED) {
      this.hub.invoke('JoinCashierGroup', cashierId);
    }
  },

  leaveCashierGroup() {
    if(!this.cashierId) {
      return;
    }
   
    this.hub.invoke('LeaveCashierGroup', this.cashierId);
    this.set('cashierId', null);
  },

  connect() {
    this.set('connecting', true);
    this.hub.start().then(() => {
      this.set('connected', true);
      this.set('connecting', false);
      if(!!this.cashierId) {
        this.joinCashierGroup(this.cashierId);
      }
    }).catch(err => {
      this.set('connecting', false);
      window.setTimeout(() => this.connect(), 5000);
      console.error('An error occured while initializing signalr.', err);
    });
  },

  getOrder(orderId) {
    return fetch('/api/orders/' + orderId)
      .then(response => response.json());
  }
});