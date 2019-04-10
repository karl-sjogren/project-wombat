import Service, { inject } from '@ember/service';
import signalR from 'signalr';
import fetch from 'fetch';

const STATE_CONNECTED = 1;

export default Service.extend({
  cashierService: inject(),
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

    this.set('hub', hub);
    this.connect();
  },

  joinCashierGroup(cashierId) {
    if(!!this.cashierId) {
      this.leaveCashierGroup();
    }
    this.set('cashierId', cashierId);

    if(this.hub.connection.connectionState === STATE_CONNECTED) {
      return this.hub.invoke('JoinCashierGroup', cashierId);
    }
    return Promise.resolve();
  },

  leaveCashierGroup() {
    if(!this.cashierId) {
      return Promise.resolve();
    }
   
    let cashierId = this.cashierId;
    this.set('cashierId', null);
    return this.hub.invoke('LeaveCashierGroup', cashierId);
  },

  updateOrderRow(orderId, productId, count) {
    if(this.hub.connection.connectionState !== STATE_CONNECTED) {
      throw new Error(`Can't update order row while not connected.`);
    }

    return this.hub.invoke('UpdateOrderRow', orderId, productId, count);
  },

  connect() {
    this.set('connecting', true);
    this.hub.start().then(() => {
      if(!!this.cashierId) {
        this.joinCashierGroup(this.cashierId).then(() => {
          if(!!this.order) {
            this.cashierService.setActiveOrder(this.cashier.id, this.order.id);
          }
        });
      }
      this.set('connected', true);
      this.set('connecting', false);
    }).catch(err => {
      this.set('connecting', false);
      window.setTimeout(() => this.connect(), 5000);
      console.error('An error occured while initializing signalr.', err);
    });
  },

  createOrder() {
    return fetch('/api/orders/', { method: 'POST' })
      .then(response => response.json());
  },

  getOrder(orderId) {
    return fetch('/api/orders/' + orderId)
      .then(response => response.json());
  },

  setOrderStatus(orderId, status) {
    return fetch('/api/orders/' + orderId, { method: 'PUT', body: `status=${status}`, headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
  }
});