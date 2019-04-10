import Service from '@ember/service';
import fetch from 'fetch';

export default Service.extend({
  getProducts() {
    return fetch('/api/products/')
      .then(response => response.json())
      .then(products => products.map(product => {
        product.count = 0;
        return product;
      }));
  }
});
