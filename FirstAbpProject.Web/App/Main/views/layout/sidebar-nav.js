(function () {
    var controllerId = 'app.views.layout.sidebarNav';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', 'appSession',
        function ($rootScope, $state, appSession) {
            var vm = this;
            var lastItem = null;

            vm.menuItems = [
                createMenuItem(App.localize("HomePage"), "", "home", "home"),
				createMenuItem(App.localize("Stores"), "Pages.Stores", "store_mall_directory", "stores"),
				createMenuItem(App.localize("Coolers"), "Pages.Coolers", "kitchen", "coolers"),
				createMenuItem(App.localize("Sloths"), "Pages.Sloths", "device_hub", "sloths"),
                createMenuItem(App.localize("Clients"), "Pages.Clients", "group", "clients"),
                createMenuItem(App.localize("Tenants"), "Pages.Tenants", "business", "tenants"),
                createMenuItem(App.localize("Users"), "Pages.Users", "supervisor_account", "", [
                    createMenuItem(App.localize("Users"), "Pages.Users", "person", "users"),
                    createMenuItem(App.localize("Roles"), "Pages.Roles", "assignment_ind", "roles")
                ])  
            ];

            vm.showMenuItem = function (menuItem) {
                if (menuItem.permissionName) {
                    return abp.auth.isGranted(menuItem.permissionName);
                }

                return true;
            }

            vm.showGlobalMask = function (menuItem) {
                if (lastItem != menuItem) {
                    $("#globalMask").show();
                }
                lastItem = menuItem;
            };

            function createMenuItem(name, permissionName, icon, route, childItems) {
                return {
                    name: name,
                    permissionName: permissionName,
                    icon: icon,
                    route: route,
                    items: childItems
                };
            }
        }
    ]);
})();