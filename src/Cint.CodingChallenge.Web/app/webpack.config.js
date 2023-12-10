const path = require('path');

module.exports = {
    entry: {
        main: './src/app.js'
    },
    output: {
        publicPath: "/js/",
        path: path.join(__dirname, './../wwwroot/js/'),
        filename: 'site.js'
    }
};
