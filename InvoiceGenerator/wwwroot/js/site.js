// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var alert = function (type, message, timer = null, link = null, position = null) {

    var messages = "";

    if (typeof (message) !== "string") {
        message.forEach(function (value) {
            messages = messages + value + "<br/>";
        })
        message = messages;
    }

    swal.fire({
        position: position,// 'top-right',
        type: type,
        title: type == 'success' ? successMessage : type == 'error' ? errorMessage : null,
        html: message,
        showConfirmButton: true,
        confirmButtonText: window.okTR,
        showCancelButton: false,
        timer: timer,
        footer: link == null ? null : `<a class="btn btn-primary" href="${link}">Go to page</a>`
    });
};
/*
app.factory('httpRequest', function ($http) {
    return {
        request: function (input) {
            if (input.url == "" || input.url == null) alert("error", "The request url is not valid");

            return $http({
                method: input.method == null ? "POST" : input.method,
                url: input.url,
                data: input.model == null ? {} : input.model,
                headers: {
                    contentType: "application/json;charset=UTF-8"
                }
            });
        },
        send: function (input) {
            return this.request(input).then(function (result) {
                var isString = typeof (result.data);
                if (isString == "string" && result.data.includes("<html")) {
                    alert('error', `A system error happened! Please contact the system management`); return;
                }
                if (input.showSuccessMessage) alert('success', input.successMessage);

                if (input.successCallBack != null) input.successCallBack(result);
                else
                    return result;

            }, this.error)
        },
        error: function (error) {

            alert('error', `${errorMessage} : ${error.data["Message"]}`); error.status = 400; return error;
        }
    }
});*/