import Swal from "../theme/vendor/sweetalert2/src/sweetalert2.js"

swalert = function (type, title, message, confirmButtonText = "OK", link = null, showCancelButton = false, timer = null, position = null) {
    Swal.fire({
        position: position,// 'top-right',
        icon: type,
        title: title,
        html: message,
        showConfirmButton: true,
        confirmButtonText: confirmButtonText,
        showCancelButton: showCancelButton,
        timer: timer
    }).then(function () {
        if(link != null) window.location.href = link;
    });
};

swalert2 = function (type, title, message, confirmButtonText = "OK", link = null, showCancelButton = false, timer = null, position = null) {
    Swal.fire({
        position: position,// 'top-right',
        icon: type,
        title: title,
        html: message,
        showConfirmButton: true,
        confirmButtonText: confirmButtonText,
        showCancelButton: showCancelButton,
        timer: timer
    }).then((result) => {
        if (result.isConfirmed) {
            if (link != null) window.location.href = link;
        }
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