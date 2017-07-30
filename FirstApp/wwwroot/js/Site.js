$(function() {
	$('#NavbarLogout').on('click', function(e) {
		e.preventDefault();

		$.ajax({
			type: 'post',
			url: '/logout',
			success: function(res) {
				if (res || status == "success") {
					if (!res.errored && res.payload) {
						return Toaster.Success(
							"Success",
							"(You) have been logged out. Please wait while we refresh the page."
						).selfDestruct(4000, function() {
							location.href = "/";
						});
					}
				}

				Toaster.Error("Error", res.exception).selfDestruct(5000);
			},
			fail: function() {
				Toaster.Error("Generic Error", "Unknown error encountered while attempting communications with the server. Check your web console for details.").selfDestruct(5000);
				console.log(arguments);
			}
		})
	})
})