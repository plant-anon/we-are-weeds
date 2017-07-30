import * as Warp from '../warp';

class NavbarComponent extends Warp.WarpComponent {
	render(element, attributes) {
		var name = "Project Name";
		var navid = element.attr('id');

		delete attributes.id;

		if (element.attr('name')) { name = elem.attr('name') }

		var base = $('<nav class="navbar navbar-inverse navbar-fixed-top"></nav>');
		var container = $('<container></container>');

		//Mobile nav button
		$('<button type=button class="navbar-toggle collapsed" data-toggle=collapse data-target="#' + navid + '" aria-expanded=false aria-controls=navbar><span class=sr-only>Toggle navigation</span><span class="icon-bar"></span><span class=icon-bar></span><span class=icon-bar></span></button >')
			.appendTo(container);

		$('<a class="navbar-brand" href="#">' + name + '</a>').appendTo(container);
		$('<div id="' + navid + '" class="collapse navbar-collapse"></div>').appendTo(container);
		container.appendTo(base);

		return {
			element: base,
			bodyPos: '#' + navid
		}
	}
}

export default NavbarComponent;