"use strict";

app.controller("create",
    function ($scope, httpRequest) {
        $scope.init = function () {
            $scope.items = [];
            $scope.createdDate = new Date();
            $scope.dueDate = new Date();
            $scope.dateError = false;
            $scope.itemName = "";
            $scope.itemQty = null;
            $scope.itemPrice = null;
            $scope.subTotal = 0;
            $scope.total = 0;
            $scope.totalDue = 0;
            $scope.feePaid = 0;
            $scope.vat = 0;
            $scope.vatError = false;
            $scope.updateDueDate();
            $scope.getCurrencies();

            $scope.clientNameWarn = null;
            $scope.companyNameWarn = null;
            $scope.addressWarn = null;
            $scope.phoneNumberWarn = null;
            $scope.emailAddressWarn = null;

            $scope.clientNameError = false;
            $scope.companyNameError = true;
            $scope.addressError = false;
            $scope.phoneNumberError = false;
            $scope.emailAddressError = false;

            $scope.error = true;
            $scope.itemNameError = false;
        }

        $scope.clientNameCheck = function () {
            if ($scope.clientName) {
                var re = new RegExp("^[a-zA-Z]+([a-zA-Z. ]*[a-zA-Z]+)?$");
                if (re.test($scope.clientName)) {
                    $scope.clientNameWarn = null;
                    $scope.clientNameError = false;
                } else {
                    $scope.clientNameError = true;
                    $scope.clientNameWarn = "Client name can only contain letters with spaces and periods in between.";
                }
            }
            else {
                $scope.clientNameWarn = null;
                $scope.clientNameError = false;
            }
            $scope.checkErrors();
        }

        $scope.companyNameCheck = function () {
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
            else {
                $scope.companyNameWarn = null;
                $scope.companyNameError = true;
            }
            $scope.checkErrors();
        }

        $scope.addressCheck = function () {
            if ($scope.address ) {
                var re = new RegExp("^[ -~]*$");
                if (re.test($scope.address)) {
                    $scope.addressWarn = null;
                    $scope.addressError = false;
                } else {
                    $scope.addressError = true;
                    $scope.addressWarn = "Please enter valid characters.";
                }
            }
            else {
                $scope.addressWarn = null;
                $scope.addressError = false;
            }
            $scope.checkErrors();
        }

        $scope.phoneNumberCheck = function () {
            if ($scope.phoneNumber) {
                var re = new RegExp("^[+]?[0-9]+([0-9 -]*[0-9]+)?$");
                if (re.test($scope.phoneNumber)) {
                    $scope.phoneNumberWarn = null;
                    $scope.phoneNumberError = false;
                } else {
                    $scope.phoneNumberError = true;
                    $scope.phoneNumberWarn = "Contact number can only contain numbers with spaces and '-' between them and '+' as prefix.";
                }
            }
            else {
                $scope.phoneNumberWarn = null;
                $scope.phoneNumberError = false;
            }
            $scope.checkErrors();
        }

        $scope.emailAddressCheck = function () {
            if ($scope.emailAddress) {
                var re = new RegExp("^.+[@]+.+$");
                if (re.test($scope.emailAddress)) {
                    $scope.emailAddressWarn = null;
                    $scope.emailAddressError = false;
                } else {
                    $scope.emailAddressError = true;
                    $scope.emailAddressWarn = "Please enter a valid email.";
                }
            }
            else {
                $scope.emailAddressWarn = null;
                $scope.emailAddressError = false;
            }
            $scope.checkErrors();
        }

        $scope.itemNameCheck = function () {
            if ($scope.itemName) {
                var re = new RegExp("^[ -~]*$");
                if (re.test($scope.itemName)) {
                    $scope.itemNameWarn = null;
                    $scope.itemNameError = false;
                } else {
                    $scope.itemNameError = true;
                    $scope.itemNameWarn = "Please enter valid characters.";
                }
            }
            else {
                $scope.itemNameWarn = null;
                $scope.itemNameError = false;
            }
        }

        $scope.checkErrors = function () {
            if (!$scope.clientNameError &&
                !$scope.companyNameError &&
                !$scope.addressError &&
                !$scope.phoneNumberError &&
                !$scope.emailAddressError) {
                $scope.error = false;
            }
            else { $scope.error = true; }
        }

        $scope.editUserRedirect = function () {
            swalert2('warning', 'Warning', 'All unsaved data will be lost! <br> Continue?', 'Yes', `${window.editUserUrl}`, true);
        }

        $scope.getUserDetails = function () {
            var requestModel = {
                url: '/api/user/getUserInvoiceDetails'
            };
            httpRequest.get(requestModel).then(
                function (result) {
                    $scope.userCompanyName = result.data.companyName;
                    $scope.userCompanyAddress = result.data.address;
                    $scope.userCompanyPhone = result.data.contactNo;
                    $scope.userCompanyEmail = result.data.email;
                    $scope.logo = result.data.logo;
                    $scope.vat = result.data.vat;
                    $scope.currencyId = result.data.currencyId;
                    $scope.currency = $scope.currencies.find(c => c.id == $scope.currencyId);
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        }

        $scope.getCurrencies = function () {
            var requestModel = {
                url: '/api/user/getCurrencies'
            };
            httpRequest.get(requestModel).then(
                function (result) {
                    $scope.currencies = result.data;
                    $scope.getUserDetails();
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        }

        $scope.updateDueDate = function () {
            if ($scope.createdDate === undefined) { swalert('error', 'Error', `Invalid created date.`); return; }
            var dd = $scope.createdDate.getDate().toString().padStart(2, "0");;
            var mm = ($scope.createdDate.getMonth() + 1).toString().padStart(2, "0");
            var yyyy = $scope.createdDate.getFullYear();
            var today = yyyy + '-' + mm + '-' + dd;
            document.querySelector("#endDate").setAttribute('min', today);
            $scope.validateDate();
        }

        $scope.validateDate = function () {
            if ($scope.createdDate === undefined) { swalert('error', 'Error', `Invalid created date.`); return; }
            if ($scope.dueDate === undefined) { swalert('error', 'Error', `Invalid due date.`); return; }
            if ($scope.createdDate > $scope.dueDate)
                $scope.dateError = true;
            else $scope.dateError = false;
        }

        $scope.addItem = function () {
            if ($scope.items.length >= 10) { swalert('error', 'Error', `Cannot add more than 10 items.`); return; }
            if ($scope.itemName === "" || !$scope.itemName) { swalert('error', 'Error', `Item/Service name cannot be empty.`); return; }
            if ($scope.itemQty === 0 || $scope.itemQty === null) { swalert('error', 'Error', `Quantity/Hours cannot be zero.`); return; }
            if ($scope.itemPrice === 0 || $scope.itemPrice === null) { swalert('error', 'Error', `Unit Price cannot be zero.`); return; }
            if ($scope.itemQty === undefined) { swalert('error', 'Error', `Invalid quantity/hours entered. <br>Value can only be positive with 0.5 intervals`); return; }
            if ($scope.itemQty > 999999999) { swalert('error', 'Error', `Invalid quantity/hours entered. <br>Value cannot exceed 999999999`); return; }
            if ($scope.itemPrice === undefined) { swalert('error', 'Error', `Invalid Unit Price entered. <br>Value can only be positive with 0.01 intervals`); return; }
            if ($scope.itemPrice > 999999999) { swalert('error', 'Error', `Invalid Unit Price entered. <br>Value cannot exceed 999999999`); return; }

            var item = {
                name: $scope.itemName,
                quantity: $scope.itemQty,
                unitPrice: $scope.itemPrice,
                total: ($scope.itemPrice * $scope.itemQty).toFixed(2),
            }
            var x = +item.total + +$scope.subTotal;
            if (x > 1000000000.00) {
                swalert('error', 'Error', `SubTotal fee cannot be greater than 1,000,000,000.`); return;
            }

            $scope.items.push(item);

            $scope.updateSummary();
            $scope.itemName = "";
            $scope.itemQty = null;
            $scope.itemPrice = null;
            return;
        }

        $scope.deleteItem = function (itemPos) {
            $scope.items.splice(itemPos, 1);
            $scope.updateSummary();
            return;
        }

        $scope.vatChange = function () {
            if ($scope.vat === undefined) { $scope.vatError = true; swalert('error', 'Error', `Invalid VAT entered. <br>Value can only exist between 0-100 with 0.01 intervals`); return; }
            if ($scope.vat == null) { $scope.vat = 0 }
            $scope.vatError = false;
            $scope.updateSummary();
            return;
        }

        $scope.feePaidChange = function () {
            if ($scope.feePaid === undefined) { $scope.feePaidError = true; swalert('error', 'Error', `Invalid Fee Paid entered.`); return; }
            if ($scope.feePaid < 0) { $scope.feePaidError = true; swalert('error', 'Error', `Invalid Fee Paid entered. <br>Value cannot be less than zero.`); return; }
            if ($scope.feePaid > $scope.total) { $scope.feePaidError = true; swalert('error', 'Error', `Invalid Fee Paid entered. <br>Value cannot be greater than total fee.`); return; }
            $scope.feePaidError = false;
            $scope.updateSummary();
            return;
        }

        $scope.updateSummary = function () {
            let subTotal = 0;
            let total = 0;
            let totalDue = 0;
            $scope.subTotal = 0;
            $scope.items.forEach(item =>
                subTotal = (item.quantity * item.unitPrice) + subTotal
            );
            $scope.subTotal = subTotal.toFixed(2);
            total = subTotal + ($scope.vat / 100 * subTotal);
            $scope.total = total.toFixed(2);
            totalDue = total - $scope.feePaid;
            $scope.totalDue = totalDue.toFixed(2);
            return;
        }

        $scope.create = function () {
            if ($scope.items.length === 0) { swalert('error', 'Error', `Item list cannot be empty.`); return; }
            if ($scope.items.length > 10) { swalert('error', 'Error', `Cannot contain more than 10 items.`); return; }
            if ($scope.dateError) { swalert('error', 'Error', `Due Date cannot be less than Date Of Invoice.`); return; }
            if ($scope.dueDate === undefined) { swalert('error', 'Error', `Invalid due date.`); return; }
            if ($scope.createdDate === undefined) { swalert('error', 'Error', `Invalid created date.`); return; }
            if ($scope.vatError) { swalert('error', 'Error', `Invalid VAT entered. <br>Value can only exist between 0-100 with 0.01 intervals`); return; }
            if ($scope.subTotal === undefined || $scope.subTotal === 0 || $scope.subTotal === null) { swalert('error', 'Error', `subTotal Fee cannot be zero or undefined.`); return; }
            if ($scope.total === undefined || $scope.total === 0 || $scope.total === null) { swalert('error', 'Error', `Total Fee cannot be zero or undefined.`); return; }
            if ($scope.feePaid === undefined || $scope.feePaid === null) { swalert('error', 'Error', `Invalid Fee Paid entered.`); return; }
            if ($scope.feePaid < 0) { swalert('error', 'Error', `Invalid Fee Paid entered. <br>Value cannot be less than zero.`); return; }
            if ($scope.feePaid > $scope.total) { swalert('error', 'Error', `Invalid Fee Paid entered. <br>Value cannot be greater than total fee.`); return; }
            if ($scope.companyName === "" || $scope.companyName === null || $scope.companyName === undefined) { swalert('error', 'Error', `Company Name cannot be empty.`); return; }

            var invoice = {
                clientName: $scope.clientName,
                companyName: $scope.companyName,
                address: $scope.address,
                phoneNumber: $scope.phoneNumber,
                emailAddress: $scope.emailAddress,
                createdDate: $scope.createdDate,
                dueDate: $scope.dueDate,

                currencyId: $scope.currency.id,
                items: $scope.items,
                vat: $scope.vat,
                totalFee: $scope.total,
                feePaid: $scope.feePaid,

                comment: $scope.comment
            }

            var requestModel = {
                url: '/api/invoice/create',
                model: invoice
            };
            httpRequest.post(requestModel).then(
                function (result) {
                    swalert('success', 'Success', 'Invoice created successfuly.', 'OK', '/invoice/view/' + result.data)
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        };
    }
);

