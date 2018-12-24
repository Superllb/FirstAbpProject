(function () {
	angular.module('app').controller('app.views.sloths.index', [
		'$scope', '$timeout', '$uibModal', '$state', '$stateParams', 'abp.services.app.sloth',
		function ($scope, $timeout, $uibModal, $state, $stateParams, slothService) {
			$("#globalMask").hide();
			var vm = this;
			vm.sloths = [];
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
					getSloths(pageNumber);
				}
			});

			function getSloths(pageNumber) {
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
				slothService.getAll(vm.mainPageParams).then(function (result) {
					vm.sloths = result.data.items;
					$('#paginator').jqPaginator('option', {
						totalCounts: result.data.totalCount,
						currentPage: pageNumber
					});
					vm.isTableLoading = false;
				});
				$stateParams.mainPageParams = null;
			}

			vm.openslothCreationModal = function () {
				var modalInstance = $uibModal.open({
					templateUrl: '/App/Main/views/sloths/createModal.cshtml',
					controller: 'app.views.sloths.createModal as vm',
					backdrop: 'static'
				});

				modalInstance.rendered.then(function () {
					$.AdminBSB.input.activate();
				});

				modalInstance.result.then(function () {
					getSloths();
				});
			};

			vm.openslothEditModal = function (sloth) {
				var modalInstance = $uibModal.open({
					templateUrl: '/App/Main/views/sloths/editModal.cshtml',
					controller: 'app.views.sloths.editModal as vm',
					backdrop: 'static',
					resolve: {
						id: function () {
							return sloth.id;
						}
					}
				});

				modalInstance.rendered.then(function () {
					$timeout(function () {
						$.AdminBSB.input.activate();
					}, 0);
				});

				modalInstance.result.then(function () {
					getSloths();
				});
			};

			vm.delete = function (sloth) {
				abp.message.confirm(
					abp.localization.localize('DeleteSloth') + " '" + sloth.id + "'?",
					abp.localization.localize('AreYouSure'),
					function (result) {
						if (result) {
							slothService.delete({ id: sloth.id })
								.then(function () {
									abp.notify.info(abp.localization.localize('ActionSuccess'));
									getSloths();
								});
						}
					});
			};

			vm.refresh = function () {
				getSloths();
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