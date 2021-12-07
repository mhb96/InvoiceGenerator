import Swal from "../theme/vendor/sweetalert2/src/sweetalert2.js"

swalert = function (type, title, message, confirmButtonText = "OK", showCancelButton = false, timer = null, link = null, position = null) {
    Swal.fire({
        position: position,// 'top-right',
        icon: type,
        title: title,
        html: message,
        showConfirmButton: true,
        confirmButtonText: confirmButtonText,
        showCancelButton: showCancelButton,
        timer: timer,
        footer: link == null ? null : `<a class="btn btn-primary" href="${link}">Go to page</a>`
    });
};

app.factory('httpRequest', function ($http) {

    var httpRequest = {};

    httpRequest.post = function (input) {
        return $http.post(input.url, input.model)
    }

    httpRequest.get = function (input) {
        return $http.get(input.url, input.model)
    }

    httpRequest.postForm = function (input) {
        var config = {
            headers: {
                "Content-Type": undefined,
            }
        };
        return $http.post(input.url, input.model, config)
    }

    return httpRequest;
});