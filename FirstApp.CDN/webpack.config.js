var webpack = require('webpack');
var path = require("path");

module.exports = {
	devtool: 'source-map',
	entry: [
		"babel-polyfill",
		"./Scripts/app"
	],
	output: {
		publicPath: "/js/",
		path: path.join(__dirname, "/wwwroot/js/"),
		filename: "main.build.js"
	},
	module: {
		loaders: [{
			exclude: /node_modules/,
			loader: "babel-loader",
			query: {
				presets: ["es2015", "stage-1"]
			}
		}]
	},
	plugins: [
		new webpack.HotModuleReplacementPlugin(),
		new webpack.NoEmitOnErrorsPlugin(),
		new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
		new webpack.DefinePlugin({
			'process.env': {
				// This has effect on the react lib size
				'NODE_ENV': JSON.stringify('production'),
			}
		}),
		//new webpack.optimize.UglifyJsPlugin()
	]
};