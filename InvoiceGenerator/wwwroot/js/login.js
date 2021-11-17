"use strict";
app.controller("login",
    function ($scope, $http) {
        $scope.init = function () {
            console.log("123");
            $scope.username = null;
            $scope.password = null;
        }

        $scope.login = function () {
            var model = {
                Username : $scope.username,
                Password : $scope.password
            };
            $http({
                method: "POST",
                url: `/home/login`,
                data: model
            }).then(function () {},
                function (error) {
                    alert('error', `Invalid user credentials.`);
             });
        }
    }
);