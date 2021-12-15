"use strict";

app.controller("login",
    function ($scope, httpRequest) {
        $scope.init = function () {
        }

        $scope.login = function () {
            var model = {
                username: $scope.username,
                password: $scope.password
            };
            var requestModel = {
                url: '/user/login',
                model: model
            };
            httpRequest.post(requestModel).then(
                function () {
                    window.location.href = "/home/index";
                },
                function (error) {
                    $scope.username = null;
                    $scope.password = null;
                    swalert('error', 'Error', `${error.data}`);
                });
        };
    }
);