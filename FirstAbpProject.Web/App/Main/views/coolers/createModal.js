(function () {
    angular.module('app').controller('app.views.coolers.createModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.cooler', 'abp.services.app.client', 'abp.services.app.user',
        function ($scope, $uibModalInstance, coolerService, clientService, userService) {
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
                userService.getUsersByClientId(vm.cooler.clientId).then(function (result) {
                    vm.users = result.data.items;
                });
            }

            vm.changeClient = function () {
                if (vm.cooler.clientId) {
                    getUsersByClientId(vm.cooler.clientId);
                } else {
                    vm.users = [];
                }
            };

            vm.changeProvince = function () {
                if (vm.cooler.province) {
                    vm.cities = vm.provinces.find(c => c.name === vm.cooler.province).city;
                } else {
                    vm.cities = [];
                }
            };

            vm.changeCity = function () {
                if (vm.cooler.city) {
                    vm.districts = vm.cities.find(c => c.name === vm.cooler.city).area;
                } else {
                    vm.districts = [];
                }
            };

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

            getClients();
        }
    ]);
})();