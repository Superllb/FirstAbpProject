(function () {
    angular.module('app').controller('app.views.coolers.index', [
        '$scope', '$timeout', '$uibModal', '$state', '$stateParams', 'abp.services.app.cooler',
        function ($scope, $timeout, $uibModal, $state, $stateParams, coolerService) {
            $("#globalMask").hide();
            var vm = this;
            vm.coolers = [];
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
                    getCoolers(pageNumber);
                }
            });

            function getCoolers(pageNumber) {
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
                coolerService.getAll(vm.mainPageParams).then(function (result) {
                    vm.coolers = result.data.items;
                    $('#paginator').jqPaginator('option', {
                        totalCounts: result.data.totalCount,
                        currentPage: pageNumber
                    });
                    vm.isTableLoading = false;
                });
                $stateParams.mainPageParams = null;
            }

            vm.openCoolerCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/coolers/createModal.cshtml',
                    controller: 'app.views.coolers.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getCoolers();
                });
            };

            vm.openCoolerEditModal = function (client) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/coolers/editModal.cshtml',
                    controller: 'app.views.coolers.editModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return cooler.id;
                        }
                    }
                });

                modalInstance.rendered.then(function () {
                    $timeout(function () {
                        $.AdminBSB.input.activate();
                    }, 0);
                });

                modalInstance.result.then(function () {
                    getCoolers();
                });
            };

            vm.delete = function (cooler) {
                abp.message.confirm(
                    abp.localization.localize('DeleteCooler') + " '" + client.slothId + "'?",
                    abp.localization.localize('AreYouSure'),
                    function (result) {
                        if (result) {
                            coolerService.delete({ id: cooler.id })
                                .then(function () {
                                    abp.notify.info(abp.localization.localize('ActionSuccess'));
                                    getCoolers();
                                });
                        }
                    });
            }

            vm.refresh = function () {
                getCoolers();
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