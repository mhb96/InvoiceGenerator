"use strict";

app.controller("register",
    function ($scope, httpRequest) {

        $scope.init = function () {

            $scope.userName = null;
            $scope.password = null;
            $scope.firstName = null;
            $scope.lastName = null;
            $scope.companyName = null;
            $scope.email = null;
            $scope.contactNo = null;
            $scope.address = null;

            $scope.userNameWarn = null;
            $scope.passwordWarn = null;
            $scope.confirmPasswordWarn = null;
            $scope.firstNameWarn = null;
            $scope.lastNameWarn = null;
            $scope.companyNameWarn = null;
            $scope.emailWarn = null;
            $scope.contactNoWarn = null;
            $scope.addressWarn = null;
            $scope.vatWarn = null;

            $scope.userNameError = true;
            $scope.passwordError = true;
            $scope.confirmPasswordError = true;
            $scope.firstNameError = true;
            $scope.lastNameError = true;
            $scope.companyNameError = true;
            $scope.emailError = true;
            $scope.contactNoError = true;
            $scope.addressError = true;
            $scope.vatError = false;

            $scope.getCurrencies();
            $scope.error = true;
        }

        $scope.getCurrencies = function () {
            var requestModel = {
                url: '/api/user/getCurrencies'
            };
            httpRequest.get(requestModel).then(
                function (result) {
                    $scope.currencies = result.data;
                    $scope.currency = $scope.currencies.find(c => c.id === 50);
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        }

        $scope.checkUserName = function () {
            if ($scope.userName) {
                var re = new RegExp("^[a-zA-Z0-9]+([a-zA-Z0-9._]*[a-zA-Z0-9]+)?$");
                if (re.test($scope.userName)) {
                    $scope.userNameWarn = null;
                    $scope.userNameError = false;
                } else {
                    $scope.userNameError = true;
                    $scope.userNameWarn = "Username can only contain letters or numbers with '_' and '.' in between.";
                }
            }
            else { $scope.userNameWarn = null; $scope.userNameError = true; }
            $scope.checkErrors();
        }

        $scope.checkPassword = function () {
            if ($scope.password) {
                var re = new RegExp("^[a-zA-Z0-9,._!$@#%^&*()]+$");
                if (re.test($scope.password)) {
                    $scope.passwordWarn = null;
                    $scope.passwordError = false;
                } else {
                    $scope.passwordError = true;
                    $scope.passwordWarn = "Password can only contain numbers, letters and special characters: , . _ ! $ @ # % ^ & * ( )";
                }
            }
            else { $scope.passwordWarn = null; $scope.passwordError = true; }
            $scope.checkErrors();
        }

        $scope.checkConfirmPassword = function () {
            if ($scope.confirmPassword) {
                var re = new RegExp("^[a-zA-Z0-9,._!$@#%^&*()]+$");
                if (re.test($scope.confirmPassword)) {
                    $scope.confirmPasswordWarn = null;
                    $scope.confirmPasswordError = false;
                } else {
                    $scope.confirmPasswordError = true;
                    $scope.confirmPasswordWarn = "Password can only contain numbers, letters and special characters: . _ ! $ @ # % ^ & * ( )";
                }
            }
            else { $scope.confirmPasswordWarn = null; $scope.confirmPasswordError = true; }
            $scope.checkErrors();
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
                var re = new RegExp("^[+]?[0-9]+([0-9 -]*[0-9]+)?$");
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
            if (!$scope.userNameError &&
                !$scope.passwordError &&
                !$scope.confirmPasswordError &&
                !$scope.firstNameError &&
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
            formData.append('currencyId', $scope.currency.id);
            formData.append('vat', $scope.vat);
            var requestModel = {
                url: '/user/register',
                model: formData
            };
            httpRequest.postForm(requestModel).then(
                function () {
                    swalert('success', 'Success', 'Registration successful!', 'Proceed to dashboard', `${window.dashboardUrl}`);
                },
                function (error) {
                    $scope.password = null;
                    $scope.confirmPassword = null;
                    swalert('error', 'Error', `${error.data}`);
                });
        };
    }
);