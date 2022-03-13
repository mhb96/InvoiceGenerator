// Call the dataTables jQuery plugin

app.controller("dashboard",
    function ($scope, httpRequest) {
        $scope.init = function () {
            $scope.deleteAllTemp();
            $scope.status = '';
            $scope.getInvoices();
            $scope.dtOptions = { paging: true, searching: true, order: [[0, 'desc']] };
        }

        $scope.deleteAllTemp = function () {
            var requestModel = {
                url: '/api/invoice/deleteTemp'
            };
            httpRequest.post(requestModel);
        }

        $scope.getInvoices = function () {
            var requestModel = {
                url: '/api/invoices/get'
            };
            httpRequest.get(requestModel).then(
                function (result) {
                    let id = "dataTable";
                    $scope.invoices = result.data.invoices;
                },
                function (error) {
                    swalert('error', 'Error', `${error.data}`);
                });
        }

        $scope.deleteInvoice = function (id) {
                Swal.fire({
                    position: 'center',
                    icon: 'warning',
                    title: 'Delete',
                    html: 'Are you sure you want to delete this invoice?',
                    showConfirmButton: true,
                    confirmButtonText: 'Confirm',
                    confirmButtonColor: '#cc0000',
                    showCancelButton: true
                }).then((result) => {
                    if (result.isConfirmed) {
                        var requestModel = {
                            url: `/invoice/delete/${id}`
                        };
                        httpRequest.post(requestModel).then(
                            function (result) {
                                swalert('success', 'Success', 'Invoice deleted successfuly.', 'OK', '/')
                            },
                            function (error) {
                                swalert('error', 'Error', `${error.data}`);
                            });
                    }
                });
        }
    }    
);