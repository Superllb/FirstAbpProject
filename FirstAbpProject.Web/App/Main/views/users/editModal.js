(function () {
    angular.module('app').controller('app.views.users.editModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.user', 'abp.services.app.client', 'id',
        function ($scope, $uibModalInstance, userService, clientService, id) {
            var vm = this;

            vm.user = {
                isActive: true
            };

            vm.currentLanguage = abp.localization.currentLanguage.name;
            vm.users = [];
            vm.clients = [];
            vm.roles = [];

			var setAssignedRoles = function (user, roles) {
				for (var i = 0; i < roles.length; i++) {
					var role = roles[i];
					role.isAssigned = $.inArray(role.name, user.roles) >= 0;
				}
			};

			var init = function () {
				userService.getAll({}).then(function (result) {
					vm.users = result.data.items;
				});
				clientService.getAll({}).then(function (result) {
					vm.clients = result.data.items;
				});
				userService.getRoles()
					.then(function (result) {
						vm.roles = result.data.items;

						userService.get({ id: id })
							.then(function (result) {
								vm.user = result.data;
								var leader = vm.user.leaderName == null ? null : vm.users.find(u => u.userName == vm.user.leaderName);
								if (leader) {
									$scope.vm.user.leaderId = leader.id;
								}
								if (vm.currentLanguage == 'zh-CN' && vm.user.clientName) {
									$scope.vm.user.clientId = vm.clients.find(c => c.name == vm.user.clientName).id;
								}
								else if (vm.user.clientName) {
									$scope.vm.user.clientId = vm.clients.find(c => c.nameEn == vm.user.clientName).id;
								}
								setAssignedRoles(vm.user, vm.roles);
							});
					});
			};

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
                userService.update(vm.user)
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