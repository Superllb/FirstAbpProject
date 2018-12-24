(function () {
	angular.module('app').controller('app.views.coolers.editModal', [
		'$scope', '$uibModalInstance', 'abp.services.app.cooler', 'id',
		function ($scope, $uibModalInstance, coolerService, id) {
			var vm = this;

			vm.currentLanguage = abp.localization.currentLanguage.name;
			vm.types = [];

			function getTypes() {
				coolerService.getDataTypes({}).then(function (result) {
					vm.types = result.data.map(item => {
						return {
							'id': item.value,
							'text': vm.currentLanguage === 'zh-CN' ? item.description : item.name
						};
					});
				});
			}

			var init = function () {
				coolerService.get({ id: id })
					.then(function (result) {
						vm.cooler = result.data;
						vm.cooler.dataType = vm.types.find(c => c.text == vm.cooler.dataType).id;
					});
			};

			vm.save = function () {
				coolerService.update(vm.cooler)
					.then(function () {
						abp.notify.info(App.localize('SavedSuccessfully'));
						$uibModalInstance.close();
					});
			};

			vm.cancel = function () {
				$uibModalInstance.dismiss({});
			};

			getTypes();
			init();
		}
	]);
})();