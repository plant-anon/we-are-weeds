class Component { }

class WarpComponent extends Component {
	constructor(type) {
		this.type = type;
	}
}

class WebComponent extends Component {
	constructor(tag, attributes) {
		this.type = tag;
		this.attributes = {};
		this.eventHandlers = [];

		if (attributes) {
			var attrs = Object.keys(attributes).map(x => x.toLowerCase());

			if (attrs.includes('@text')) {
				this.text = attributes['@text'];
				delete attributes['@text'];
			}

			if (attrs.includes('@value')) {
				this.value = attributes['@value'];
				delete attributes['@value'];
			}

			this.attributes = attributes;
		}
	}

	setAttribute(attr, val) {
		this.attributes[attr] = val;
	}

	getAttribute(attr) {
		return this.attributes[attr];
	}

	on(event, handler) {
		this.eventHandlers.push({
			event: event,
			handler: handler
		});
	}

	once(event, handler) {
		this.eventHandlers.push({
			event: event,
			handler: handler,
			once: true
		});
	}

	render() {
		var elem = $(`<${this.type} />`, this.attributes);

		for (var handle in this.eventHandlers) {
			if (handle.once) {
				elem.one(handle.event, handle.handler);
			} else {
				elem.on(handle.event, handle.handler);
			}
		}

		if (this.text)
			elem.text(this.text);

		if (this.value)
			elem.val(this.value);

		return elem;
	}
}

export {
	Component,
	WarpComponent,
	WebComponent
};