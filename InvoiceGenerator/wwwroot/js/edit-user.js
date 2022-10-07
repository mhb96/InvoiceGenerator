"use strict";

app.controller("edit",
    function ($scope, httpRequest) {
        $scope.init = function () {
            $scope.firstNameWarn = null;
            $scope.lastNameWarn = null;
            $scope.companyNameWarn = null;
            $scope.emailWarn = null;
            $scope.contactNoWarn = null;
            $scope.addressWarn = null;
            $scope.vatWarn = null;

            $scope.firstNameError = false;
            $scope.lastNameError = false;
            $scope.companyNameError = false;
            $scope.emailError = false;
            $scope.contactNoError = false;
            $scope.addressError = false;
            $scope.vatError = false;

            $scope.getCurrencies();
            $scope.error = false;
        }

        $scope.showPassword = function () {
            var password = $("#password");
            if ($("#password")[0].type === "password")
                $("#password")[0].type = "text";
            else
                password[0].type = "password";
        }

        $scope.getCurrencies = function () {
            var requestModel = {
                url: '/api/user/getCurrencies'
            };
            httpRequest.get(requestModel).then(
                function (result) {
                    $scope.currencies = result.data;
                    $scope.getDetails();
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        }

        $scope.checkFirstName = function () {
            if ($scope.firstName) {
                var re = new RegExp("^[a-zA-Z]+([a-zA-Z. ]*[a-zA-Z]+)?$");
                if (re.test($scope.firstName)) {
                    $scope.firstNameWarn = null;
                    $scope.firstNameError = false;
                } else {
                    $scope.firstNameError = true;
                    $scope.firstNameWarn = "First name can only contain letters with spaces and periods in between.";
                }
            }
            else { $scope.firstNameWarn = null; $scope.firstNameError = true; }
            $scope.checkErrors();
        }

        $scope.checkLastName = function () {
            if ($scope.lastName) {
                var re = new RegExp("^[a-zA-Z]+([a-zA-Z. ]*[a-zA-Z]+)?$");
                if (re.test($scope.lastName)) {
                    $scope.lastNameWarn = null;
                    $scope.lastNameError = false;
                } else {
                    $scope.lastNameError = true;
                    $scope.lastNameWarn = "Last name can only contain letters with spaces and periods in between.";
                }
            }
            else { $scope.lastNameWarn = null; $scope.lastNameError = true; }
            $scope.checkErrors();
        }

        $scope.checkCompanyName = function () {
            if ($scope.companyName) {
                var re = new RegExp("^[ -~]*$");
                if (re.test($scope.companyName)) {
                    $scope.companyNameWarn = null;
                    $scope.companyNameError = false;
                } else {
                    $scope.companyNameError = true;
                    $scope.companyNameWarn = "Please enter valid characters.";
                }
            }
            else { $scope.companyNameWarn = null; $scope.companyNameError = true; }
            $scope.checkErrors();
        }

        $scope.checkEmail = function () {
            if ($scope.email) {
                var re = new RegExp("^.+[@]+.+$");
                if (re.test($scope.email)) {
                    $scope.emailWarn = null;
                    $scope.emailError = false;
                } else {
                    $scope.emailError = true;
                    $scope.emailWarn = "Please enter a valid email.";
                }
            }
            else { $scope.emailWarn = null; $scope.emailError = true; }
            $scope.checkErrors();
        }

        $scope.checkContactNo = function () {
            if ($scope.contactNo) {
                var re = new RegExp("^([+]?[0-9]+([0-9 -]*[0-9]+))?$");
                if (re.test($scope.contactNo)) {
                    $scope.contactNoWarn = null;
                    $scope.contactNoError = false;
                } else {
                    $scope.contactNoError = true;
                    $scope.contactNoWarn = "Contact number can only contain numbers with spaces and '-' between them and '+' as prefix.";
                }
            }
            else { $scope.contactNoWarn = null; $scope.contactNoError = true; }
            $scope.checkErrors();
        }

        $scope.checkAddress = function () {
            if ($scope.address) {
                var re = new RegExp("^[ -~]*$");
                if (re.test($scope.address)) {
                    $scope.addressWarn = null;
                    $scope.addressError = false;
                } else {
                    $scope.addressError = true;
                    $scope.addressWarn = "Please enter valid characters.";
                }
            }
            else { $scope.addressWarn = null; $scope.addressError = true; }
            $scope.checkErrors();
        }

        $scope.checkVat = function () {
            if ($scope.vat === undefined) {
                $scope.vatError = true;
                $scope.vatWarn = "VAT can only exist between 0-100 with 0.01 intervals";
            }
            else if ($scope.vat > 100) {
                $scope.vatError = true;
                $scope.vatWarn = "Vat cannot exceed 100%";
            }
            else {
                $scope.vatError = false;
                $scope.vatWarn = null;
            }
            $scope.checkErrors();
        }

        $scope.checkErrors = function () {
            if (!$scope.firstNameError &&
                !$scope.lastNameError &&
                !$scope.companyNameError &&
                !$scope.emailError &&
                !$scope.contactNoError &&
                !$scope.vatError &&
                !$scope.addressError) {
                $scope.error = false;
            }
            else { $scope.error = true; }
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
                    $scope.currencyId = result.data.currencyId;
                    $scope.currency = $scope.currencies.find(c => c.id == $scope.currencyId);
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
            formData.append('currencyId', $scope.currency.id);
            var requestModel = {
                url: '/api/user/editDetails',
                model: formData
            };
            httpRequest.postForm(requestModel).then(
                function () {
                    swalert('success', 'Success', 'Updated successfully!', 'Ok', `${document.referrer}`);
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        };
    }
);