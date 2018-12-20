(function () {
    angular.module('app').controller('app.views.stores.editModal', [
		'$scope', '$uibModalInstance', 'abp.services.app.store', 'abp.services.app.client', 'abp.services.app.user', 'id',
		function ($scope, $uibModalInstance, storeService, clientService, userService, id) {
			var vm = this;

			vm.currentLanguage = abp.localization.currentLanguage.name;
			vm.users = [];
			vm.clients = [];
			vm.provinces = cityData;
			vm.cities = [];
			vm.districts = [];

			function getUsersByClientId() {
				userService.getUsersByClientId(vm.store.clientId).then(function (result) {
					vm.users = result.data.items;
				});
			}

			vm.changeClient = function () {
				if (vm.store.clientId) {
					getUsersByClientId(vm.store.clientId);
				} else {
					vm.users = [];
				}
			};

			vm.changeProvince = function () {
				if (vm.store.province) {
					vm.cities = vm.provinces.find(c => c.name === vm.store.province).city;
				} else {
					vm.cities = [];
				}
			};

			vm.changeCity = function () {
				if (vm.store.city) {
					vm.districts = vm.cities.find(c => c.name === vm.store.city).area;
				} else {
					vm.districts = [];
				}
			};

			var init = function () {
				clientService.getAllClients({}).then(function (result) {
					vm.clients = result.data.items;
				});
				storeService.get({ id: id })
					.then(function (result) {
						vm.store = result.data;
						vm.cities = vm.provinces.find(c => c.name === vm.store.province).city;
						vm.districts = vm.cities.find(c => c.name === vm.store.city).area;
						getUsersByClientId();
					});
			};

            vm.save = function () {
				storeService.update(vm.store)
                    .then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

			init();
        }
    ]);
})();