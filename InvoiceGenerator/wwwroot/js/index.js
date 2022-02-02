// Call the dataTables jQuery plugin

app.controller("dashboard",
    function ($scope, httpRequest) {
        $scope.init = function () {
            $scope.deleteAllTemp();
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
    }    
);