(function () {
    angular.module('app').controller('app.views.coolers.createModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.cooler', 'id',
        function ($scope, $uibModalInstance, coolerService, id) {
			var vm = this;
			vm.storeId = id;

            vm.currentLanguage = abp.localization.currentLanguage.name;
			vm.types = [];
			vm.cooler = {
				isOnline: false,
				isQa: false
			};

            function getTypes () {
				coolerService.getDataTypes({}).then(function (result) {
					vm.types = result.data.map(item => {
						return {
							'id': item.value,
							'text': vm.currentLanguage === 'zh-CN' ? item.description : item.name
						};
					});
                });
            }

            vm.save = function () {
                coolerService.create(vm.cooler)
                    .then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

			getTypes();
        }
    ]);
})();