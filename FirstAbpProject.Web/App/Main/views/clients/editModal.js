(function () {
    angular.module('app').controller('app.views.clients.editModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.client', 'id',
        function ($scope, $uibModalInstance, clientService, id) {
            var vm = this;

            vm.client = {
                isActive: true
            };

			var init = function () {
				clientService.get({ id: id })
					.then(function (result) {
						vm.client = result.data;
					});
			};

            vm.save = function () {
                clientService.update(vm.client)
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