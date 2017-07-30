Toaster = {
	toasts: {}
}

//Multiline comment -> String
Toaster.Template = '<div class="toaster"><div class="popup-head"><a class="popup-close glyphicon glyphicon-remove pull-right"></a><span class="popup-icon pull-left"></span><h3 class="popup-title"></h3></div><p class="popup-body"></p></div>';

Toaster.Themes = {
	'default': {
		colors: {
			primary: '#337ab7',
			secondary: '#2e6da4',
			font: '#fff'
		},
		icon: 'glyphicon glyphicon-asterisk'
	},
	'info': {
		colors: {
			primary: '#46b8da',
			secondary: '#31b0d5',
			font: '#fff'
		},
		icon: 'glyphicon glyphicon-time'
	},
	'success': {
		colors: {
			primary: '#5cb85c',
			secondary: '#4cae4c',
			font: '#fff'
		},
		icon: 'glyphicon glyphicon-ok'
	},
	'warning': {
		colors: {
			primary: '#f0ad4e',
			secondary: '#eea236',
			font: '#fff'
		},
		icon: 'glyphicon glyphicon-asterisk'
	},
	'error': {
		colors: {
			primary: '#d9534f',
			secondary: '#d43f3a',
			font: '#fff'
		},
		icon: 'glyphicon glyphicon-alert'
	}
}

Toaster.guid = function() {
	function s4() {
		return Math.floor((1 + Math.random()) * 0x10000)
			.toString(16)
			.substring(1);
	}
	return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}

$(function() {
    /*
     * Generic popup creator
     * Arguments: [htmlstring Title, htmlstring Message, (Optional) string Style<default, success, warning, info, error>]
     * Returns: Object to allow for self-destruct timer setting, editing and deletion
     */
	Toaster.Notify = function() {
		if (arguments.length < 2) throw Error('Arguments required for popup: [string Title, string Message]');
		var instance = $(this.Template);

		var title = arguments[0];
		var msg = arguments[1];
		var style = arguments[2] || 'default';
		var id = Toaster.guid();
		var theme = Toaster.Themes[style] || Toaster.Themes['default'];

		instance[0].style['background-color'] = theme.colors.primary;
		instance[0].style['border-bottom'] = '10px solid ' + theme.colors.secondary;
		instance[0].style['color'] = theme.colors.font;
		instance.find('.popup-icon').toggleClass(theme.icon);

		instance.find('.popup-title').html(title);
		instance.find('.popup-body').html(msg);

		instance.attr('guid', id).toggleClass(style).hide();
		instance.appendTo($('body')).show('blind', {}, 300);

		var obj = {
			id: id,
			theme: style,
			inst: {
				instance: instance,
				selfDestruct: function(time, callback) {
					if (!time) return this.remove(callback);

					(function(inst, time, callback) {
						setTimeout(function() {
							inst.remove(callback);
						}, time);
					})(this, time, callback);
				},
				remove: function(callback) {
					this.instance.hide('blind', {}, 300, function() {
						var id = $(this).attr('guid');
						$(this).remove();
						delete Toaster.toasts[id];
						if (callback) callback();
					});
					return null;
				},
				setTitle: function(title) {
					if (title)
						this.instance.find('.popup-title').html(title);
					return this;
				},
				setBody: function(message) {
					if (message)
						this.instance.find('.popup-body').html(message);
					return this;
				},
				setTheme: function(theme) {
					var oldTheme = Toaster.Themes[this.theme];
					theme = (typeof ('theme') === 'string') ? Toaster.Themes[theme] : Toaster.Themes['default'];

					instance[0].style['background-color'] = theme.colors.primary;
					instance[0].style['border-bottom'] = '10px solid ' + theme.colors.secondary;
					instance[0].style['color'] = theme.colors.font;
					instance.find('.popup-icon').toggleClass(oldTheme.icon + ' ' + theme.icon); //Turn off old icon and enable new one
				}
			}
		};

		Toaster.toasts[id] = obj;

		return obj.inst;
	}

	//Magic function to call the Toaster.Notify() function with success theme
	Toaster.Success = function() {
		if (arguments.length < 2) throw Error('Arguments required for popup: [string Title, string Message]');
		var args = Array.prototype.slice.call(arguments);
		args[2] = 'success';
		return Toaster.Notify.apply(Toaster, args);
	}

	//Magic function to call the Toaster.Notify() function with info theme
	Toaster.Info = function() {
		if (arguments.length < 2) throw Error('Arguments required for popup: [string Title, string Message]');
		var args = Array.prototype.slice.call(arguments);
		args[2] = 'info';
		return Toaster.Notify.apply(Toaster, args);
	}

	//Magic function to call the Toaster.Notify() function with warning theme
	Toaster.Warning = function() {
		if (arguments.length < 2) throw Error('Arguments required for popup: [string Title, string Message]');
		var args = Array.prototype.slice.call(arguments);
		args[2] = 'warning';
		return Toaster.Notify.apply(Toaster, args);
	}

	//Magic function to call the Toaster.Notify() function with error theme
	Toaster.Error = function() {
		if (arguments.length < 2) throw Error('Arguments required for popup: [string Title, string Message]');
		var args = Array.prototype.slice.call(arguments);
		args[2] = 'error';
		return Toaster.Notify.apply(Toaster, args);
	}

	//Eat toast by ID
	Toaster.remove = function(id) {
		if (!Toaster.toasts[id]) return console.error('Could not find toast \'' + id + '\'');
		Toaster.toasts[id].inst.remove();
	}

	//Click handler for toaster close
	$('body').on('click', '.toaster a.popup-close', function(e) {
		e.preventDefault();
		var toast = $(this).closest('.toaster');
		var id = toast.attr('guid');
		Toaster.remove(id);
	});
});