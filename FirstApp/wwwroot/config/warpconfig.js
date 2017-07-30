//TODO: Separate this out.
//See: FirstApp.CDN - I was experimenting with a class-based design, like React. Hated the idea of having to compile my scripts though, so I gave up pretty quick.
//I have a functioning copy in a separate solution, maybe I'll upload that to Github some time.


//This sets up the custom elements you see in the layouts
var ElementMapping = {
    'container': 'div container',
    'row': 'div row',
    'column': 'div column',
    'roundbutton': 'button class="btn" size="lg" btn-type="circle"',
    'actionbutton': function(elem, attrs) {
        var button = $('<button class="btn" size="lg" btn-type="circle" />');

        if (attrs['icon']) {
            var icon = attrs['icon'];

            if (icon.includes('fa-')) {
				$('<i class="fa {0}" />'.format(icon)).appendTo(button);
			} else if (icon.includes('material-')) {
				$('<i class="material-icons">{0}</i>'.format(icon.substring(9))).appendTo(button);
            }

            delete attrs['icon'];
        }

        return button;
    },
    'materialicon': 'i class=material-icons',
    'glyphicon': function(elem, attrs) {
        var icon = elem.text();
        elem.text('');

        var out = $('<i class=glyphicon />');
        out.toggleClass('glyphicon-' + icon);
        return out;
    },
    'card': function(elem, attributes) {
        var card = $('<div card></div>')

        if (attributes['card-title']) {
			var title = $('<div class=title expanded><p>{0}<p></div>'.format(attributes['card-title']));

            if (!attributes['media-card']) {
                title.attr('has-border', 'true');
            } else {
                title.attr('media-card', 'true');
            }
            title.appendTo(card);
		}

		if (attributes['title-image'] && attributes['title-image-size']) {
			var url = attributes['title-image'];
			var height = attributes['title-image-size'] + 'px';

			card.find('.title').css('height', height).css('background', 'url({0}) center / cover'.format(url)).attr('text-color', 'white');
			delete attributes['title-image'];
			delete attributes['title-image-size'];
		}

		if (attributes['title-color']) {
			card.find('.title').attr('color', attributes['color']);
			delete attributes['color'];
		}

		if (attributes['title-text-color']) {
			card.find('.title').attr('text-color', attributes['text-color']);
			delete attributes['text-color'];
		}

		var body = $('<div class=card-body />');

		if (attributes['color']) {
			body.attr('color', attributes['color']);
			delete attributes['color'];
		}

		body.appendTo(card);
        return {
            element: card,
            bodyPos: 'div.card-body'
        };
	},
	'formcomponent': function(elem, attrs) {
		var body = $('<div class="form-group" />')
		var type = attrs['type'];
		delete attrs['type'];

		var input = $('<{0} />'.format(type), attrs);

		Object.keys(attrs).forEach(function(key) { delete attrs[key]; });
		
		if (!elem.is(':empty')) {
			input.attr('placeholder', elem.text());
			elem.text('');
		}

		if (elem.is('[required]')) {
			input.attr('required', true);
		}

		input.attr('id', elem.attr('id'));
		delete attrs['id'];

		input.toggleClass('form-control input-md');
		input.appendTo(body);
		return body;
	},
    'input[id]': function(elem, attrs) {
		if (!attrs['name']) {
            attrs['name'] = attrs['id'];
        }
	},
	'chip': function(elem, attrs) {
		var chip = $('<div class=chip />');

		if (attrs['contact-label']) {
			var contact = $('<span class=contact />');
			chip.toggleClass('chip-contact');

			if (attrs['contact-color']) {
				contact.attr('color', attrs['contact-color']);
				delete attrs['contact-color'];
			} else {
				contact.attr('color', 'teal');
			}

			if (attrs['contact-text-color']) {
				contact.attr('color', attrs['contact-text-color']);
				delete attrs['contact-text-color'];
			} else {
				contact.attr('text-color', 'white');
			}

			contact.text(attrs['contact-label']);
			contact.appendTo(chip);

			delete attrs['contact-label'];
		}

		if (!elem.is(':empty')) {
			$('<span class="chip-text">{0}</span>'.format(elem.text())).appendTo(chip);
			elem.text('');
		}

		if (attrs['closable']) {
			$('<a class=chip-action action=close href="#/"><i class="material-icons">cancel</i></a>').appendTo(chip);
			chip.toggleClass('deletable');
			delete attrs['closeable'];
		}

		return chip;
	}
}

ElementMapping['navbar'] = function(elem, attrs) {
	var name = null;
	var logo = null;
	var navid = elem.attr('id');

	delete attrs.id;

	if (elem.is('[name]')) {
		name = elem.attr('name')
		delete attrs['name'];
	}

	if (elem.is('[logo]')) {
		logo = elem.attr('logo');
		delete attrs['logo'];
	}

	var base = $('<nav class="navbar navbar-inverse navbar-fixed-top"></nav>');
	var container = $('<container></container>');

	//Mobile nav button
	$('<button type=button class="navbar-toggle collapsed" data-toggle=collapse data-target="#{0}" aria-expanded=false aria-controls=navbar><span class=sr-only>Toggle navigation</span><span class="icon-bar"></span><span class=icon-bar></span><span class=icon-bar></span></button >'.format(navid))
		.appendTo(container);

	var title;

	if (logo) {
		var fmt = '<img src="{0}" alt="{1}" />';

		if (name) {
			if (elem.is('[logo-and-name]')) {
				fmt += "&nbsp;&nbsp;{1}";
				delete attrs['logo-and-name'];
			}
			title = fmt.format(logo, name)
		} else {
			if (elem.is('[logo-and-name]')) {
				fmt += "&nbsp;&nbsp;{1}";
				delete attrs['logo-and-name'];
			}
			title = fmt.format(logo, "Warp Project")
		}
	} else {
		title = name;
	}

	$('<a class="navbar-brand" href="/">{0}</a>'.format(title)).appendTo(container);
	$('<div id="{0}" class="collapse navbar-collapse"></div>'.format(navid)).appendTo(container);
	container.appendTo(base);

	return {
		element: base,
		bodyPos: '#' + navid
	}
};

ElementMapping['buttongroup'] = function(elem, attrs) {
	if (elem.is('[vertical]')) {
		delete attrs['vertical'];
		return '<div class="btn-group-vertical" />';
	}
	if (elem.is('[justified]')) {
		delete attrs['justified'];
		return '<div class="btn-group btn-group-justified" />';
	}
	return '<div class="btn-group" />';
}

ElementMapping['fab'] = function(elem, attrs) {
	var fab = $('<a class="btn" fab />');
	var body = $('<div fab-wrapper />');
	var content = $('<div fab-content />');

	if (elem.is('[icon]')) {
		$('<MaterialIcon fab-icon>{0}</MaterialIcon>'.format(attrs['icon'])).appendTo(fab);
		delete attrs['icon'];
	}

	if (elem.is('[title]')) {
		var title = $('<div fab-title />');
		$('<h3>{0}</h3>'.format(attrs['title'])).appendTo(title);
		title.appendTo(body);
		delete attrs['title'];
	}

	if (elem.is('[color]')) {
		body.find('div[fab-title]').attr('color', attrs['color']);
	}

	if (elem.is('[text-color]')) {
		body.find('div[fab-title] > h3').attr('text-color', attrs['text-color']);
	}

	content.appendTo(body);
	body.appendTo(fab);

	return {
		element: fab,
		bodyPos: 'div[fab-content]'
	};
}

ElementMapping['materialnav'] = function(elem, attrs) {

}