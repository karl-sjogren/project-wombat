import { module, test } from 'qunit';
import { setupTest } from 'ember-qunit';

module('Unit | Route | cashier', function(hooks) {
  setupTest(hooks);

  test('it exists', function(assert) {
    let route = this.owner.lookup('route:cashier');
    assert.ok(route);
  });
});
