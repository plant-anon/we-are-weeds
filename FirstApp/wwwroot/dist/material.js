$.fn.center = function() {
	var x = Math.max(0, (($(window).width() - ($(this).outerWidth() / 2)) / 2)),
		y = Math.max(0, (($(window).height() - ($(this).outerHeight() / 2)) / 2));

	return { x: x, y: y };
};

$.fn.MoveToCenter = function() {
	var pos = $(this).center();
	var center = $('body').center();

	$(this).animate({
		right: center.x,
		bottom: center.y,
	});
};

$.fn.showSpinner = function() {

}

$(function() {
	$('.ripple').on('click', function(e) {
		(function(event) {
			event.preventDefault();

			var $div = $('<div class="ripple-effect" />');
			var btnOffset = $(this).offset();
			var xPos = event.pageX - btnOffset.left;
			var yPos = event.pageY - btnOffset.top;
			
			var $ripple = $(".ripple-effect");

			$ripple.css("height", $(this).height());
			$ripple.css("width", $(this).height());
			var color = '#fff';

			$div.css({
				top: yPos - ($ripple.height() / 2),
				left: xPos - ($ripple.width() / 2),
				background: color
			}).appendTo($(this));

			setTimeout(function() { $div.remove() }, 2000);
		}).call(this, e);
	});

	$(document).on('click', '.chip > .chip-action', function(e) {
		if ($(this).is('[action="close"]')) {
			e.preventDefault();

			(function(elem) {
				elem.closest('.chip').animate({
					visible: 'toggle',
					opacity: '0',
					easing: 'easeInOutQuint',
					complete: function() {
						elem.remove();
					}
				}, 200);
			})($(this))
		}
	});
});

$(function() {
	$('[activates="popup"][activation-target]').each(function() {
		var target = $(this).attr('')
	});

	var pageOverlay = $('<div dark-page-overlay />');
	pageOverlay.hide().prependTo('body');

	pageOverlay.on('click', function() {
		pageOverlay.hide();

		if (pageOverlay['__targett']) {
			pageOverlay['__targett'].removeClass('active');
			delete pageOverlay['__targett'];
		}
	});

	$('.btn[fab]').on('click', function(e) {
		pageOverlay['__targett'] = $(this);

		$(this).addClass('active');
		pageOverlay.show();
	});

	//Variables
	var overlay = $("#overlay"),
		fab = $(".fab"),
		cancel = $("#cancel"),
		submit = $("#submit");

	//fab click
	fab.on('click', openFAB);
	overlay.on('click', closeFAB);
	cancel.on('click', closeFAB);

	function openFAB(event) {
		if (event) event.preventDefault();
		fab.addClass('active');
		overlay.addClass('dark-overlay');

	}

	function closeFAB(event) {
		if (event) {
			event.preventDefault();
			event.stopImmediatePropagation();
		}

		fab.removeClass('active');
		overlay.removeClass('dark-overlay');

	}
});