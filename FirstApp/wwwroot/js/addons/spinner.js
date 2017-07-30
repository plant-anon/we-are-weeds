Spinner = {
	Show: function(id, parent, time, fade) {
	    var spinner = $('<div spinner spinner-id="' + id + '"><div class="contents"><i class="glyphicon glyphicon-refresh spinning"></i></div></div>');
	    var target = (parent ? $(parent) : $('body'));
        
	    //Fix for modals
	    if (parent.endsWith('.modal-content')) {
	        spinner.css('border-radius', '5px');
	    }

	    target.append(spinner);

		if(typeof(time) === 'number' && time > 0) {
			setTimeout((function(id, fade) {
				return function() {
					Spinner.Hide(id, fade);
				}
			})(id, fade), time);
		}

		return (function(id, fade) {
			return function() {
				Spinner.Hide(id, fade);
			}
		})(id, fade);
	},
	Hide: function(id, fade) {
	    var target = $('div[spinner-id="' + id + '"]');
	    var f = fade ? 300 : 0;

	    if (target) {
	        target.fadeOut(f);
		    setTimeout(function() {
		        target.remove();
		    }, ++f);
	    }
	}
};

//One-stop shop for all your page-change spinners!
//$(function () {
//    $(window).bind('beforeunload', function () {
//		Spinner.Show('page-change', 'body.body', null, 300);
//    });

//    $(window).bind('abort', function () {
//        Spinner.Hide('page-change');
//    });
//}); 