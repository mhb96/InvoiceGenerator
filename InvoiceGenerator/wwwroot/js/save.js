"use strict";

app.controller("save",
    function ($scope, httpRequest) {
        $scope.download = function (id) {

            var icon = $('#download');
            icon.removeClass("fas fa-download");
            icon.addClass("fa fa-spinner fa-spin");

            var requestModel = {
                url: `/api/invoice/download/${id}`
            };
            httpRequest.post(requestModel).then(
                function (result) {
                    $scope.fileName = result.data;
                    var element = document.createElement('a');
                    element.setAttribute('href', `/temp/${$scope.fileName}.pdf`);
                    element.setAttribute('download', `Invoice-${id}`);
                    document.body.appendChild(element);
                    element.click();
                    document.body.removeChild(element);
                    icon.removeClass("fa fa-spinner fa-spin");
                    icon.addClass("fas fa-download");
                },
                function (error) {
                    icon.removeClass("fa fa-spinner fa-spin");
                    icon.addClass("fas fa-download");
                    swalert('error', 'Error', `${error.data}`);
                });


        }
    }
);

