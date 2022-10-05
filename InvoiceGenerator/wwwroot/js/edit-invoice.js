"use strict";
app.controller("edit",
    function ($scope, httpRequest) {
        $scope.init = function (id) {
            $scope.id = id;
            $scope.createdDate = new Date();
            $scope.dueDate = new Date();
            $scope.dateError = false;
            $scope.itemName = "";
            $scope.itemQty = null;
            $scope.itemPrice = null;
            $scope.subTotal = 0;
            $scope.total = 0;
            $scope.vat = 0;
            $scope.vatError = false;
            $scope.updateDueDate();
            $scope.getCurrencies();
            $scope.feePaid = 0;


            $scope.userCompanyNameWarn = null;
            $scope.userCompanyAddressWarn = null;
            $scope.userContactNoWarn = null;
            $scope.userCompanyEmailWarn = null;

            $scope.userCompanyNameError = false;
            $scope.userCompanyAddressError = false;
            $scope.userContactNoError = false;
            $scope.userCompanyEmailError = false;

            $scope.clientNameWarn = null;
            $scope.clientCompanyNameWarn = null;
            $scope.clientAddressWarn = null;
            $scope.clientPhoneNumberWarn = null;
            $scope.clientEmailAddressWarn = null;

            $scope.clientNameError = false;
            $scope.clientCompanyNameError = false;
            $scope.clientAddressError = false;
            $scope.clientPhoneNumberError = false;
            $scope.clientEmailAddressError = false;

            $scope.itemNameError = false;
        }

        $scope.getInvoiceDetails = function () {
            var requestModel = {
                url: '/api/invoice/getInvoiceDetails/'+ $scope.id
            };
            httpRequest.get(requestModel).then(
                function (result) {
                    $scope.invoiceNo = result.data.invoiceNo;
                    $scope.userCompanyName = result.data.userCompanyName;
                    $scope.userCompanyAddress = result.data.userAddress;
                    $scope.userContactNo = result.data.userContactNo;
                    $scope.userCompanyEmail = result.data.userEmail;

                    $scope.logo = result.data.userCompanyLogo;
                    $scope.vat = result.data.vat;
                    $scope.currencyId = result.data.currencyId;
                    $scope.currency = $scope.currencies.find(c => c.id == $scope.currencyId);

                    $scope.clientName = result.data.clientName;
                    $scope.clientCompanyName = result.data.clientCompanyName;
                    $scope.clientAddress = result.data.clientAddress;
                    $scope.clientPhoneNumber = result.data.clientPhoneNumber;
                    $scope.clientEmailAddress = result.data.clientEmailAddress;

                    $scope.createdDate = $scope.parseDate(result.data.createdDate);
                    $scope.dueDate = $scope.parseDate(result.data.dueDate);
                    $scope.comment = result.data.comment;

                    $scope.items = result.data.items;
                    angular.forEach($scope.items, function (value, key) {
                        value.total = value.quantity * value.unitPrice;
                    });

                    $scope.feePaid = result.data.feePaid;
                    $scope.updateSummary();
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
                    $scope.getInvoiceDetails();
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        }

        //user info checks
        $scope.checkUserCompanyName = function () {
            if ($scope.userCompanyName) {
                var re = new RegExp("^[ -~]*$");
                if (re.test($scope.userCompanyName)) {
                    $scope.userCompanyNameWarn = null;
                    $scope.userCompanyNameError = false;
                } else {
                    $scope.userCompanyNameError = true;
                    $scope.userCompanyNameWarn = "Please enter valid characters.";
                }
            }
            else { $scope.userCompanyNameWarn = "Your company name cannot be empty."; $scope.userCompanyNameError = true; }
            $scope.checkErrors();
        }

        $scope.checkUserCompanyAddress = function () {
            if ($scope.userCompanyAddress) {
                var re = new RegExp("^[ -~]*$");
                if (re.test($scope.userCompanyAddress)) {
                    $scope.userCompanyAddressWarn = null;
                    $scope.userCompanyAddressError = false;
                } else {
                    $scope.userCompanyAddressError = true;
                    $scope.userCompanyAddressWarn = "Please enter valid characters.";
                }
            }
            else { $scope.userCompanyAddressWarn = "Your company address cannot be empty."; $scope.userCompanyAddressError = true; }
            $scope.checkErrors();
        }

        $scope.checkUserCompanyEmail = function () {
            if ($scope.userCompanyEmail) {
                var re = new RegExp("^.+[@]+.+$");
                if (re.test($scope.userCompanyEmail)) {
                    $scope.userCompanyEmailWarn = null;
                    $scope.userCompanyEmailError = false;
                } else {
                    $scope.userCompanyEmailError = true;
                    $scope.userCompanyEmailWarn = "Please enter a valid email.";
                }
            }
            else { $scope.userCompanyEmailWarn = "Your company email cannot be empty."; $scope.userCompanyEmailError = true; }
            $scope.checkErrors();
        }

        $scope.checkUserContactNo = function () {
            if ($scope.userContactNo) {
                var re = new RegExp("^[+]?[0-9]+([0-9 -]*[0-9]+)?$");
                if (re.test($scope.userContactNo)) {
                    $scope.userContactNoWarn = null;
                    $scope.userContactNoError = false;
                } else {
                    $scope.userContactNoError = true;
                    $scope.userContactNoWarn = "Contact number can only contain numbers with spaces and '-' between them and '+' as prefix.";
                }
            }
            else { $scope.userContactNoWarn = "Your company contact number cannot be empty."; $scope.userContactNoError = true; }
            $scope.checkErrors();
        }

        //client info checks

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

        $scope.clientCompanyNameCheck = function () {
            if ($scope.clientCompanyName) {
                var re = new RegExp("^[ -~]*$");
                if (re.test($scope.clientCompanyName)) {
                    $scope.clientCompanyNameWarn = null;
                    $scope.clientCompanyNameError = false;
                } else {
                    $scope.clientCompanyNameError = true;
                    $scope.clientCompanyNameWarn = "Please enter valid characters.";
                }
            }
            else {
                $scope.clientCompanyNameWarn = "Client company name cannot be empty.";
                $scope.clientCompanyNameError = true;
            }
            $scope.checkErrors();
        }

        $scope.clientAddressCheck = function () {
            if ($scope.clientAddress) {
                var re = new RegExp("^[ -~]*$");
                if (re.test($scope.clientAddress)) {
                    $scope.clientAddressWarn = null;
                    $scope.clientAddressError = false;
                } else {
                    $scope.clientAddressError = true;
                    $scope.clientAddressWarn = "Please enter valid characters.";
                }
            }
            else {
                $scope.clientAddressWarn = null;
                $scope.clientAddressError = false;
            }
            $scope.checkErrors();
        }

        $scope.clientPhoneNumberCheck = function () {
            if ($scope.clientPhoneNumber) {
                var re = new RegExp("^[+]?[0-9]+([0-9 -]*[0-9]+)?$");
                if (re.test($scope.clientPhoneNumber)) {
                    $scope.clientPhoneNumberWarn = null;
                    $scope.clientPhoneNumberError = false;
                } else {
                    $scope.clientPhoneNumberError = true;
                    $scope.clientPhoneNumberWarn = "Phone number can only contain numbers with spaces and '-' between them and '+' as prefix.";
                }
            }
            else {
                $scope.clientPhoneNumberWarn = null;
                $scope.clientPhoneNumberError = false;
            }
            $scope.checkErrors();
        }

        $scope.clientEmailAddressCheck = function () {
            if ($scope.clientEmailAddress) {
                var re = new RegExp("^.+[@]+.+$");
                if (re.test($scope.clientEmailAddress)) {
                    $scope.clientEmailAddressWarn = null;
                    $scope.clientEmailAddressError = false;
                } else {
                    $scope.clientEmailAddressError = true;
                    $scope.clientEmailAddressWarn = "Please enter a valid email.";
                }
            }
            else {
                $scope.clientEmailAddressWarn = null;
                $scope.clientEmailAddressError = false;
            }
            $scope.checkErrors();
        }

        //others
        $scope.editUserRedirect = function () {
            swalert2('warning', 'Warning', 'All unsaved data will be lost! <br> Continue?', 'Yes', `${window.editUserUrl}`, true);
        }

        $scope.checkErrors = function () {
            if (!$scope.userCompanyNameError &&
                !$scope.userCompanyAddressError &&
                !$scope.userContactNoError &&
                !$scope.userCompanyEmailError &&

                !$scope.clientNameError &&
                !$scope.clientCompanyNameError &&
                !$scope.clientAddressError &&
                !$scope.clientPhoneNumberError &&
                !$scope.clientEmailAddressError) {
                $scope.error = false;
            }
            else { $scope.error = true; }
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
            if (Date.parse($scope.createdDate) > Date.parse($scope.dueDate))
                $scope.dateError = true;
            else $scope.dateError = false;
        }

        $scope.parseDate = function (date) {
            if (date === undefined) { swalert('error', 'Error', `Invalid created date.`); return; }
            var dd = `${date[0]}${date[1]}`;
            var mm = `${date[3]}${date[4]}`;
            var yyyy = `${date[6]}${date[7]}${date[8]}${date[9]}`;
            var parseDate = new Date(`${yyyy}-${mm}-${dd}`);
            return parseDate;
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

        $scope.edit = function () {
            if ($scope.items.length === 0) { swalert('error', 'Error', `Item list cannot be empty.`); return; }
            if ($scope.items.length > 10) { swalert('error', 'Error', `Cannot have more than 10 items.`); return; }
            if ($scope.userCompanyName === "" || !$scope.userCompanyName) { swalert('error', 'Error', `Your company name cannot be empty.`); return; }
            if ($scope.userContactNo === "" || !$scope.userContactNo) { swalert('error', 'Error', `Your company contact number cannot be empty.`); return; }
            if ($scope.userCompanyAddress === "" || !$scope.userCompanyAddress) { swalert('error', 'Error', `Your company address cannot be empty.`); return; }
            if ($scope.userCompanyEmail === "" || !$scope.userCompanyEmail) { swalert('error', 'Error', `Your company email cannot be empty.`); return; }
            if ($scope.dateError) { swalert('error', 'Error', `Due Date cannot be less than Date Of Invoice.`); return; }
            if ($scope.dueDate === undefined) { swalert('error', 'Error', `Invalid due date.`); return; }
            if ($scope.createdDate === undefined) { swalert('error', 'Error', `Invalid created date.`); return; }
            if ($scope.vatError) { swalert('error', 'Error', `Invalid VAT entered. <br>Value can only exist between 0-100 with 0.01 intervals`); return; }
            if ($scope.subTotal === undefined || $scope.subTotal === 0 || $scope.subTotal === null) { swalert('error', 'Error', `subTotal Fee cannot be zero or undefined.`); return; }
            if ($scope.total === undefined || $scope.total === 0 || $scope.total === null) { swalert('error', 'Error', `Total Fee cannot be zero or undefined.`); return; }
            if ($scope.feePaid === undefined || $scope.feePaid === null) { swalert('error', 'Error', `Invalid Fee Paid entered.`); return; }
            if ($scope.feePaid < 0) { swalert('error', 'Error', `Invalid Fee Paid entered. <br>Value cannot be less than zero.`); return; }
            if ($scope.feePaid > $scope.total) { swalert('error', 'Error', `Invalid Fee Paid entered. <br>Value cannot be greater than total fee.`); return; }
            if ($scope.clientCompanyName === "" || $scope.clientCompanyName === null || $scope.clientCompanyName === undefined) { swalert('error', 'Error', `Client's Company Name cannot be empty.`); return; }

            var invoice = {
                userCompanyName: $scope.userCompanyName,
                userAddress: $scope.userCompanyAddress,
                userEmail: $scope.userCompanyEmail,
                userContactNo: $scope.userContactNo,

                clientName: $scope.clientName,
                companyName: $scope.clientCompanyName,
                address: $scope.clientAddress,
                phoneNumber: $scope.clientPhoneNumber,
                emailAddress: $scope.clientEmailAddress,

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
                url: `/api/invoice/edit/${$scope.id}`,
                model: invoice
            };
            httpRequest.post(requestModel).then(
                function (result) {
                    swalert('success', 'Success', 'Invoice updated successfuly.', 'OK', '/invoice/view/' + result.data)
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        };
    }
);

