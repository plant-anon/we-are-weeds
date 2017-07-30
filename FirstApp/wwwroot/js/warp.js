_warp = (function(window, $) {
	if (!String.prototype.format) {
		String.prototype.format = function() {
			var args = arguments;
			return this.replace(/{(\d+)}/g, function(match, number) {
				return typeof args[number] !== 'undefined' ? args[number] : match;
			});
		};
	}

	//Move an element
	$.fn.moveTo = function(position) {
		this.appendTo(position);
	}

	//Change one element into another, while preserving attributes and events
	$.fn.changeElementType = function(newType) {
		var attrs = {};

		$.each(this[0].attributes, function(idx, attr) {
			attrs[attr.nodeName] = attr.value;
		});

		var replacement;
		if (typeof (newType) === 'function') {
			var parsed = newType(this, attrs);

			if (parsed === null || typeof (parsed) === 'undefined') {
				replacement = this;
			} else {
				if (typeof (parsed) === 'string') {
					replacement = $(newType(this, attrs));
				} else if (typeof (parsed) === 'object') {
					if (parsed.element) {
						replacement = $(parsed.element);
						replacement['__bodyPos'] = parsed['bodyPos'];
						replacement['__doneHandler'] = parsed['done'];
					} else {
						replacement = $(parsed);
					}
				} else {
					replacement = $(this.prop('tagName'));
				}
			}
		} else {
			if (newType[0] === '<') {
				replacement = $(newType);
			} else {
				replacement = $("<" + newType + "/>");
			}
		}

		for (var prop in attrs) {
			if (prop.toLowerCase() === 'class') {
				replacement.toggleClass(attrs[prop], true);
			} else {
				replacement.attr(prop, attrs[prop]);
			}
		}

		$.each($._data(this[0], 'events'), function() {
			// iterate registered handler of original
			$.each(this, function() {
				replacement.bind(this.type, this.handler);
			});
		});

		this.replaceWith(function() {
			if (replacement['__bodyPos']) {
				var f = replacement.find(replacement['__bodyPos']);
				if (f.length) { f.append($(this).contents()); }

				if (replacement['__doneHandler']) {
					replacement['__doneHandler'](replacement);
				}

				return replacement;
			}

			replacement.append($(this).contents());

			if (replacement['__doneHandler']) {
				replacement['__doneHandler'](replacement);
			}

			replacement.prop('___warped', true);
			return replacement;
		});
	}


	//Recursive parsing until it's all been warped
	while (true) {
		var finished = 0;
		var total = Object.keys(ElementMapping).length;

		for (var elem in ElementMapping) {
			var found = $(elem);

			if (found.length) {
				if ($(found[found.length - 1]).prop('___warped')) { //In case you warp something into the same CSS-selectable type
					finished++
					break;
				}

				found.each(function() {
					$(this).changeElementType(ElementMapping[elem])
				});
			} else {
				finished++;
			}
		}

		if (finished === total) break;
	}

	return console.log('_WARP fabricated');
})(window, window.jQuery);