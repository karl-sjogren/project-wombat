import EmberRouter from '@ember/routing/router';
import config from './config/environment';

const Router = EmberRouter.extend({
  location: config.locationType,
  rootURL: config.rootURL
});

Router.map(function() {
  this.route('cashier', { path: 'cashier/:cashier_id' });
  this.route('customer', { path: 'cashier/:cashier_id/customer' });
  this.route('cashiers');
});

export default Router;
