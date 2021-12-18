// Call the dataTables jQuery plugin

app.controller("dashboard",
    function ($scope, httpRequest) {
        $scope.init = function () {
            $scope.getInvoices();
            $scope.dtOptions = { paging: true, searching: true, order: [[0, 'desc']] };
        }

        $scope.getInvoices = function () {
            var requestModel = {
                url: '/api/get/invoices'
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