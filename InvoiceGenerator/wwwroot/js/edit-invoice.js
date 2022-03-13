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
            if ($scope.items.length >= 12) { swalert('error', 'Error', `Cannot add more than 12 items.`); return; }
            if ($scope.itemName === "") { swalert('error', 'Error', `Item/Service name cannot be empty.`); return; }
            if ($scope.itemQty === 0) { swalert('error', 'Error', `Quantity/Hours cannot be zero.`); return;}
            if ($scope.itemPrice === 0) { swalert('error', 'Error', `Unit Price cannot be zero.`); return; }
            if ($scope.itemQty === undefined) { swalert('error', 'Error', `Invalid quantity/hours entered. <br>Value can only be positive with 0.5 intervals`); return; }
            if ($scope.itemPrice === undefined) { swalert('error', 'Error', `Invalid Unit Price entered. <br>Value can only be positive with 0.01 intervals`); return; }

            var item = {
                name: $scope.itemName,
                quantity: $scope.itemQty,
                unitPrice: $scope.itemPrice,
                total: ($scope.itemPrice * $scope.itemQty).toFixed(2),
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
            if ($scope.items.length > 12) { swalert('error', 'Error', `Cannot have more than 12 items.`); return; }
            if ($scope.userCompanyName === "") { swalert('error', 'Error', `Your company name cannot be empty.`); return; }
            if ($scope.userContactNo === "") { swalert('error', 'Error', `Your company contact number cannot be empty.`); return; }
            if ($scope.userCompanyAddress === "") { swalert('error', 'Error', `Your company address cannot be empty.`); return; }
            if ($scope.userCompanyEmail === "") { swalert('error', 'Error', `Your company email cannot be empty.`); return; }
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

