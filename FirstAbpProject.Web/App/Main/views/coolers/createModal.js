(function () {
    angular.module('app').controller('app.views.coolers.createModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.cooler', 'abp.services.app.client', 'abp.services.app.user',
        function ($scope, $uibModalInstance, coolerService, clientService, userService) {
            var vm = this;

            vm.currentLanguage = abp.localization.currentLanguage.name;
            vm.users = [];
            vm.clients = [];

            function getClients () {
                clientService.getAllClients({}).then(function (result) {
                    vm.clients = result.data.items;
                });
            }

            function getUsersByClientId () {
                userService.getUsersByClientId(vm.coler.clientId).then(function (result) {
                    vm.users = result.data.items;
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

            getClients();
        }
    ]);
})();