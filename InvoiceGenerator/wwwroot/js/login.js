﻿"use strict";

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
                method: "POST",
                url: '/home/login',
                model: model
            };
            httpRequest.post(requestModel).then(
                function () { },
                function (error) {
                    $scope.username = null;
                    $scope.password = null;
                    swalert('error', `${error.data}`, 'Error');
                });
        };
    }
);