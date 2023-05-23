window.toastrFunctions = {

	showToastrInfo: function () {
		toastr.info("Test toast");
	},
	showToastrInfoOptions: function (message, options) {
		console.log('showToastrInfoOptions::', options);
		toastr.options = options;
		toastr.success(message);
	},
	showToastrInfoParms: function (message, options) {
		console.log('options::', options);
		toastr.options = options;
		toastr.warning(message);
	},
}
