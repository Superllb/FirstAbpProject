(function () {
    angular.module('app').controller('app.views.users.createModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.user', 'abp.services.app.client',
        function ($scope, $uibModalInstance, userService, clientService) {
            var vm = this;

            vm.user = {
                isActive: true
            };

            vm.currentLanguage = abp.localization.currentLanguage.name;
            vm.users = [];
            vm.clients = [];
            vm.roles = [];

            function getUsers() {
                userService.getAll({}).then(function (result) {
                    vm.users = result.data.items;
                });
            }

            function getClients() {
                clientService.getAll({}).then(function (result) {
                    vm.clients = result.data.items;
                });
            }

            function getRoles() {
                userService.getRoles()
                    .then(function (result) {
                        vm.roles = result.data.items;
                    });
            }

            vm.save = function () {
                var assingnedRoles = [];

                for (var i = 0; i < vm.roles.length; i++) {
                    var role = vm.roles[i];
                    if (!role.isAssigned) {
                        continue;
                    }

                    assingnedRoles.push(role.name);
                }

                vm.user.roleNames = assingnedRoles;
                userService.create(vm.user)
                    .then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

            getUsers();
            getRoles();
            getClients();
        }
    ]);
})();