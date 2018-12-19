(function () {
    angular.module('app').controller('app.views.stores.createModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.store', 'abp.services.app.client', 'abp.services.app.user',
        function ($scope, $uibModalInstance, storeService, clientService, userService) {
            var vm = this;

            vm.currentLanguage = abp.localization.currentLanguage.name;
            vm.users = [];
            vm.clients = [];
            vm.provinces = cityData;
            vm.cities = [];
            vm.districts = [];

            function getClients () {
                clientService.getAllClients({}).then(function (result) {
                    vm.clients = result.data.items;
                });
            }

            function getUsersByClientId () {
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

            vm.save = function () {
                storeService.create(vm.store)
                    .then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

            getClients();
        }
    ]);
})();