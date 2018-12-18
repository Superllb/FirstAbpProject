(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider', '$locationProvider', '$qProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider, $qProvider) {
            $locationProvider.hashPrefix('');
            $urlRouterProvider.otherwise('/');
            $qProvider.errorOnUnhandledRejections(false);

            if (abp.auth.hasPermission("Pages.Clients")) {
                $stateProvider
                    .state("clients", {
                        url: "/clients",
                        templateUrl: "/App/Main/views/clients/index.cshtml",
                        menu: "Clients" //Matches to name of 'Clients' menu in FirstAbpProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise("/clients");
            }

            if (abp.auth.hasPermission('Pages.Users')) {
                $stateProvider
                    .state('users', {
                        url: '/users',
                        templateUrl: '/App/Main/views/users/index.cshtml',
                        menu: 'Users' //Matches to name of 'Users' menu in FirstAbpProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/users');
            }

            if (abp.auth.hasPermission('Pages.Roles')) {
                $stateProvider
                    .state('roles', {
                        url: '/roles',
                        templateUrl: '/App/Main/views/roles/index.cshtml',
                        menu: 'Roles' //Matches to name of 'Tenants' menu in FirstAbpProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/roles');
            }

            if (abp.auth.hasPermission('Pages.Tenants')) {
                $stateProvider
                    .state('tenants', {
                        url: '/tenants',
                        templateUrl: '/App/Main/views/tenants/index.cshtml',
                        menu: 'Tenants' //Matches to name of 'Tenants' menu in FirstAbpProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/tenants');
            }

            if (abp.auth.hasPermission('Pages.Stores')) {
                $stateProvider
                    .state('stores', {
                        url: '/stores',
                        templateUrl: '/App/Main/views/stores/index.cshtml',
                        menu: 'Stores' //Matches to name of 'Tenants' menu in FirstAbpProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/stores');
            }

            if (abp.auth.hasPermission('Pages.Coolers')) {
                $stateProvider
                    .state('coolers', {
                        url: '/coolers',
                        templateUrl: '/App/Main/views/coolers/index.cshtml',
                        menu: 'Coolers' //Matches to name of 'Tenants' menu in FirstAbpProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/coolers');
            }

            if (abp.auth.hasPermission('Pages.Sloths')) {
                $stateProvider
                    .state('sloths', {
                        url: '/sloths',
                        templateUrl: '/App/Main/views/sloths/index.cshtml',
                        menu: 'Sloths' //Matches to name of 'Tenants' menu in FirstAbpProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/sloths');
            }

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/Main/views/home/home.cshtml',
                    menu: 'Home' //Matches to name of 'Home' menu in FirstAbpProjectNavigationProvider
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in FirstAbpProjectNavigationProvider
                });

            $urlRouterProvider.otherwise('/');

            abp.localization.defaultSourceName = 'FirstAbpProject';
        }
    ]);

})();