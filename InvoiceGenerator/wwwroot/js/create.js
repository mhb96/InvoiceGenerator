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
            $scope.vat = 0;
            $scope.vatError = false;
            $scope.updateDueDate();
            //$scope.getUserDetails();
        }

        $scope.getUserDetails = function () {
            var requestModel = {
                url: '/api/get/invoiceUserDetails'
            };
            httpRequest.get(requestModel).then(
                function (result) {
                    $scope.userCompanyName = result.data.companyName;
                    $scope.userCompanyAddress = result.data.address;
                    $scope.userCompanyPhone = result.data.contactNo;
                    $scope.userCompanyEmail = result.data.email;
                    $scope.vat = result.data.vat;
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
            if ($scope.itemName === "") { swalert('error', 'Error', `Item/Service name cannot be empty.`); return; }
            if ($scope.itemQty === 0) { swalert('error', 'Error', `Quantity/Hours cannot be zero.`); return;}
            if ($scope.itemPrice === 0) { swalert('error', 'Error', `Unit Price cannot be zero.`); return; }
            if ($scope.itemQty === undefined) { swalert('error', 'Error', `Invalid quantity/hours entered. <br>Value can only be positive with 0.5 intervals`); return; }
            if ($scope.itemPrice === undefined) { swalert('error', 'Error', `Invalid Unit Price entered. <br>Value can only be positive with 0.01 intervals`); return; }

            var item = {
                name: $scope.itemName,
                quantity: $scope.itemQty,
                unitPrice: $scope.itemPrice.toFixed(2),
                total: ($scope.itemPrice * $scope.itemQty).toFixed(2),
            }
            $scope.items.push(item);

            $scope.updateSummary();
            $scope.itemName = "";
            $scope.itemQty = null;
            $scope.itemPrice = null;
            return;
        }

        $scope.updateSummary = function () {
            $scope.subTotal = Number(0);
            $scope.items.forEach(item =>
                $scope.subTotal = (Number(item.total) + Number($scope.subTotal)).toFixed(2)
            );
            $scope.total = (Number($scope.subTotal) + (Number($scope.vat) / 100 * Number($scope.subTotal))).toFixed(2)
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

        $scope.create = function () {
            if ($scope.items.length === 0) { swalert('error', 'Error', `Item list cannot be empty.`); return; }
            if ($scope.dateError) { swalert('error', 'Error', `Due Date cannot be less than Date Of Invoice.`); return; }
            if ($scope.dueDate === undefined) { swalert('error', 'Error', `Invalid due date.`); return; }
            if ($scope.createdDate === undefined) { swalert('error', 'Error', `Invalid created date.`); return; }
            if ($scope.vatError) { swalert('error', 'Error', `Invalid VAT entered. <br>Value can only exist between 0-100 with 0.01 intervals`); return; }
            if ($scope.subTotal === undefined || $scope.subTotal === 0 || $scope.subTotal === null) { swalert('error', 'Error', `subTotal Fee cannot be zero or undefined.`); return; }
            if ($scope.total === undefined || $scope.total === 0 || $scope.total === null) { swalert('error', 'Error', `Total Fee cannot be zero or undefined.`); return; }
            if ($scope.companyName === "" || $scope.companyName === null) { swalert('error', 'Error', `Company Name cannot be empty.`); return; }

            var invoice = {
                clientName: $scope.clientName,
                companyName: $scope.companyName,
                address: $scope.address,
                phoneNumber: $scope.phoneNumber,
                emailAddress: $scope.emailAddress,
                createdDate: $scope.createdDate,
                dueDate: $scope.dueDate,

                items: $scope.items,
                vat: $scope.vat,
                totalFee: $scope.total,

                comment: $scope.comment
            }

            var requestModel = {
                url: '/api/invoice/create',
                model: invoice
            };
            httpRequest.post(requestModel).then(
                function () {
                    swalert('success', 'Success', 'Invoice created successfuly.', '<a style="color:#ffffff" href="/home/index">OK</a>')
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        };
    }
);

