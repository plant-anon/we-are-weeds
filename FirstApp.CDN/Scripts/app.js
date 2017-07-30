import * as Warp from './warp';
import Navbar from './components/NavbarComponent';

var config = require('./warp.config');

(function($) {
	var registry = {
		Add: function(component) {
			if (component instanceof Warp.Component) {
				this.registry.push(component);
			} else throw new Error("Not a component!");
		},
		registry: []
	}

	config(registry, function() {

	});
	var elem = new Warp.WebComponent('script', {
		'@text': "alert('Hello World!');"
	});

	$('body').append(elem.render());
})(window.jQuery);