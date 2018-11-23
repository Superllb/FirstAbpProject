(function () {
    var controllerId = 'app.views.layout.topbar';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', 'appSession',
        function ($rootScope, $state, appSession) {
            var vm = this;
            vm.languages = [];
            vm.currentLanguage = {};

            function init() {
                vm.languages = abp.localization.languages;
                vm.currentLanguage = abp.localization.currentLanguage;
            }

            vm.changeLanguage = function (languageName) {
                location.href = abp.appPath + 'AbpLocalization/ChangeCulture?cultureName=' + languageName + '&returnUrl=' + window.location.pathname + window.location.hash;
            }

			vm.userEmailAddress = appSession.user.emailAddress;
			vm.getShownUserName = function() {
				if (!abp.multiTenancy.isEnabled) {
					return appSession.user.userName;
				} else {
					if (appSession.tenant) {
						return appSession.tenant.tenancyName + '\\' + appSession.user.userName;
					} else {
						return '.\\' + appSession.user.userName;
					}
				}
			}

            init();

        }
    ]);
})();