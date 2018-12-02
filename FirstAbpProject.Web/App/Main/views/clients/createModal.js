(function () {
    angular.module('app').controller('app.views.clients.createModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.client',
        function ($scope, $uibModalInstance, clientService) {
            var vm = this;

            vm.client = {
                isActive: true
            };

            vm.save = function () {
                clientService.create(vm.client)
                    .then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };
        }
    ]);
})();