import Service from '@ember/service';
import fetch from 'fetch';

export default Service.extend({
  getCashier(id) {
    return fetch('/api/cashiers/' + encodeURIComponent(id))
      .then(response => response.json());
  },

  getCashiers() {
    return fetch('/api/cashiers/')
      .then(response => response.json());
  },

  setActiveOrder(id, orderId) {
    return fetch(`/api/cashiers/${encodeURIComponent(id)}/${encodeURIComponent(orderId)}`, { method: 'PUT' })
  }
});
