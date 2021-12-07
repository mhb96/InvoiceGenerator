"use strict";


app.controller("register",
    function ($scope, httpRequest) {
        $scope.init = function () {
           
        }

        $scope.create = function () {

            if ($scope.password != $scope.confirmPassword) {
                swalert('error',"Passwords do not match", 'Error');
                return;
            }

            var formData = new FormData();
            formData.append('companyLogo', $('#logo')[0].files[0]);
            formData.append('userName', $scope.userName);
            formData.append('password', $scope.password);
            formData.append('firstName', $scope.firstName);
            formData.append('lastName', $scope.lastName);
            formData.append('companyName', $scope.companyName);
            formData.append('email', $scope.email);
            formData.append('contactNo', $scope.contactNo);
            formData.append('address', $scope.address);
            formData.append('vat', $scope.vat);
            var requestModel = {
                url: '/user/register',
                model: formData
            };
            httpRequest.postForm(requestModel).then(
                function () {
                    swalert('success', 'Success', 'Registration successful!', `<a class="text-light" href="${window.dashboardUrl}">Proceed to dashboard</a>`)
                },
                function (error) {
                    $scope.password = null;
                    swalert('error', 'Error', `${error.data}`);
                });
        };
    }
);