'use strict';

const EmberApp = require('ember-cli/lib/broccoli/ember-app');

module.exports = function(defaults) {
  let app = new EmberApp(defaults, {
    lessOptions: {
      sourceMap: {
        sourceMapFileInline: true
      }
    }
  });

  app.import('node_modules/@aspnet/signalr/dist/browser/signalr.js', {
    using: [
      { transformation: 'amd', as: 'signalr' }
    ]
  });

  return app.toTree();
};
