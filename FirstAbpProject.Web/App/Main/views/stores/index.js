(function () {
    angular.module('app').controller('app.views.stores.index', [
        '$scope', '$timeout', '$uibModal', '$state', '$stateParams', 'abp.services.app.store',
        function ($scope, $timeout, $uibModal, $state, $stateParams, storeService) {
            $("#globalMask").hide();
            var vm = this;
            vm.stores = [];
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
                    getStores(pageNumber);
                }
            });

            function getStores(pageNumber) {
                if (!pageNumber) {
                    pageNumber = 1;
                }
                if ($stateParams.mainPageParams != null) {
                    vm.mainPageParams = $stateParams.mainPageParams;
                    vm.filter = $stateParams.mainPageParams.filter;

                    if (vm.mainPageParams.skipCount > 0 && vm.mainPageParams.maxResultCount > 0) {
                        pageNumber = (vm.mainPageParams.skipCount / vm.mainPageParams.maxResultCount) + 1;
                    }
                } else {
                    vm.mainPageParams = { skipCount: (pageNumber - 1) * perPageCount, maxResultCount: perPageCount, filter: vm.filter };
                }

                vm.isTableLoading = true;
                storeService.getAll(vm.mainPageParams).then(function (result) {
                    vm.stores = result.data.items;
                    $('#paginator').jqPaginator('option', {
                        totalCounts: result.data.totalCount,
                        currentPage: pageNumber
                    });
                    vm.isTableLoading = false;
                });
                $stateParams.mainPageParams = null;
            }

            vm.openStoreCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/stores/createModal.cshtml',
                    controller: 'app.views.stores.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getStores();
                });
            };

            vm.openStoreEditModal = function (store) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/stores/editModal.cshtml',
                    controller: 'app.views.stores.editModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return store.id;
                        }
                    }
                });

                modalInstance.rendered.then(function () {
                    $timeout(function () {
                        $.AdminBSB.input.activate();
                    }, 0);
                });

                modalInstance.result.then(function () {
                    getStores();
                });
            };

            vm.delete = function (store) {
                abp.message.confirm(
                    abp.localization.localize('DeleteStore') + " '" + client.slothId + "'?",
                    abp.localization.localize('AreYouSure'),
                    function (result) {
                        if (result) {
                            storeService.delete({ id: stores.id })
                                .then(function () {
                                    abp.notify.info(abp.localization.localize('ActionSuccess'));
                                    getStores();
                                });
                        }
                    });
            }

            vm.refresh = function () {
                getStores();
            };

            vm.refreshByEnter = function (e) {
                var keycode = window.event ? e.keyCode : e.which;
                if (keycode == 13) {
                    vm.refresh();
                }
            };
        }
    ]);
})();