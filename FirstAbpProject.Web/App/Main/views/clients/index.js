(function () {
    angular.module('app').controller('app.views.clients.index', [
        '$scope', '$timeout', '$uibModal', '$state', '$stateParams', 'abp.services.app.client',
        function ($scope, $timeout, $uibModal, $state, $stateParams, clientService) {
            $("#globalMask").hide();
            var vm = this;
            vm.clients = [];
            vm.filter = '';
            vm.mainPageParams = $stateParams.mainPageParams || null;
            vm.isTableLoading = true;
            
            var perPageCount = 10;
            $('#paginator').jqPaginator({
                totalCounts: 0,
                pageSize: perPageCount,
                visiblePages: 6,
                currentPage: 1,
                first: '<li class="first"><a href="javascript:;">' + abp.localization.localize('First') + '</a></li>',
                prev: '<li class="prev"><a href="javascript:;">' + abp.localization.localize('Previous') + '</a></li>',
                page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                next: '<li class="next"><a href="javascript:;">' + abp.localization.localize('Next') + '</a></li>',
                last: '<li class="last"><a href="javascript:;">' + abp.localization.localize('Last') + '</a></li>',
                total: '<li class="last"><a href="javascript:;">' + abp.localization.localize('Total') + ': {{totalCounts}}</a></li>',
                onPageChange: function (pageNumber, type) {
                    getClients(pageNumber);
                }
            });

            function getClients(pageNumber) {
                if (!pageNumber) {
                    pageNumber = 1;
                }
                if ($stateParams.mainPageParams !== null) {
                    vm.mainPageParams = $stateParams.mainPageParams;
                    vm.filter = $stateParams.mainPageParams.filter;

                    if (vm.mainPageParams.skipCount > 0 && vm.mainPageParams.maxResultCount > 0) {
                        pageNumber = vm.mainPageParams.skipCount / vm.mainPageParams.maxResultCount + 1;
                    }
                } else {
                    vm.mainPageParams = { skipCount: (pageNumber - 1) * perPageCount, maxResultCount: perPageCount, filter: vm.filter };
                }

                vm.isTableLoading = true;
                clientService.getAll(vm.mainPageParams).then(function (result) {
                    vm.clients = result.data.items;
                    $('#paginator').jqPaginator('option', {
                        totalCounts: result.data.totalCount,
                        currentPage: pageNumber
                    });
                    vm.isTableLoading = false;
                });
                $stateParams.mainPageParams = null;
            } 

            vm.openClientCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/clients/createModal.cshtml',
                    controller: 'app.views.clients.createModal as vm',
                    backdrop: 'static'
                });
                
                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getClients();
                });
            };

            vm.openClientEditModal = function (client) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/clients/editModal.cshtml',
                    controller: 'app.views.clients.editModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return client.id;
                        }
                    }
                });

                modalInstance.rendered.then(function () {
                    $timeout(function () {
                        $.AdminBSB.input.activate();
                    }, 0);
                });

                modalInstance.result.then(function () {
                    getClients();
                });
            };

			vm.delete = function (client) {
				abp.message.confirm(
					abp.localization.localize('DeleteClient') + " '" + client.name + "'?",
					abp.localization.localize('AreYouSure'),
					function (result) {
						if (result) {
							clientService.delete({ id: client.id })
								.then(function () {
									abp.notify.info(abp.localization.localize('ActionSuccess'));
									getClients();
								});
						}
					});
			};

            vm.refresh = function () {
                getClients();
            };

            vm.refreshByEnter = function (e) {
                var keycode = window.event ? e.keyCode : e.which;
                if (keycode === 13) {
                    vm.refresh();
                }
            };   
        }
    ]);
})();