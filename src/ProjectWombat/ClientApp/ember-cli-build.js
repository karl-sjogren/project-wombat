'use strict';

const EmberApp = require('ember-cli/lib/broccoli/ember-app');
const autoprefixer = require('autoprefixer');

module.exports = function(defaults) {
  let app = new EmberApp(defaults, {
    lessOptions: {
      sourceMap: {
        sourceMapFileInline: true
      }
    },
    postcssOptions: {
      compile: {
        enabled: false,
        browsers: ['last 3 versions'], // this will override config found in config/targets.js
      },
      filter: {
        enabled: true,
        plugins: [
          {
            module: autoprefixer,
            options: {
              browsers: ['last 2 versions'] // this will override the config, but just for this plugin
            }
          }
        ]
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
