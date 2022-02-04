"use strict";

app.controller("edit",
    function ($scope, httpRequest) {
        $scope.init = function () {
            $scope.getDetails();
        }

        $scope.showPassword = function () {
            var password = $("#password");
            if ($("#password")[0].type === "password")
                $("#password")[0].type = "text";
            else
                password[0].type = "password";
        }

        $scope.getDetails = function () {
            var requestModel = {
                url: '/api/user/getDetails'
            };
            httpRequest.get(requestModel).then(
                function (result) {
                    $scope.userName = result.data.userName;
                    $scope.password = result.data.password;
                    $scope.firstName = result.data.firstName;
                    $scope.lastName = result.data.lastName;
                    $scope.companyName = result.data.companyName;
                    $scope.address = result.data.address;
                    $scope.contactNo = result.data.contactNo;
                    $scope.email = result.data.email;
                    $scope.companyLogo = result.data.companyLogo;
                    $scope.vat = result.data.vat;
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        }

        $scope.confirm = function () {
            var formData = new FormData();
            formData.append('companyLogo', $('#logo')[0].files[0]);
            formData.append('firstName', $scope.firstName);
            formData.append('lastName', $scope.lastName);
            formData.append('companyName', $scope.companyName);
            formData.append('email', $scope.email);
            formData.append('contactNo', $scope.contactNo);
            formData.append('address', $scope.address);
            formData.append('vat', $scope.vat);
            var requestModel = {
                url: '/api/user/editDetails',
                model: formData
            };
            httpRequest.postForm(requestModel).then(
                function () {
                    swalert('success', 'Success', 'Updated successfully!', 'Proceed to dashboard', `${window.dashboardUrl}`);
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        };
    }
);