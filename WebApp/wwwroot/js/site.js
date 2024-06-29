// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    $(document).on("submit", ".form-ajax", function (event) {
        event.preventDefault();
        var form = $(this);
        var data = new FormData(this);

        bootbox.confirm({
            title: 'Confirmation',
            message: 'Are you sure want to submit this data?',
            size: 'small',
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm'
                }
            },
            callback: function (result) {
                if (result) {
                    $.LoadingOverlay("show", { size: 50, maxSize: 50, minSize: 50 });
                    var token = $('input[name="__RequestVerificationToken"]').val();
                    $.ajax({
                        type: 'POST',
                        url: form.action,
                        cache: false,
                        processData: false,
                        headers: { 'X-CSRF-TOKEN': token },
                        contentType: false,
                        traditional: true,
                        data: data,
                        success: function (result) {
                            if (result.success) {
                                bootbox.alert({
                                    size: "small",
                                    title: "Information",
                                    message: result.message,
                                    callback: function () {
                                        var redir = form.data("redirect");
                                        if (redir !== undefined)
                                            window.location = redir;
                                    }
                                });
                            } else {
                                if (result.message != undefined) {
                                    var title = "Warning";
                                    if (result.data && result.data.header) {
                                        title = result.data.header;
                                    }
                                    bootbox.alert({ size: "small", title: title, message: result.message });
                                }
                            }
                            $.LoadingOverlay("hide");
                        },
                        error: function (e, t, s) {
                            var title = "Error";
                            var errorMessage = e.statusText;
                            if (isJson(e.responseText)) {
                                var obj = JSON.parse(e.responseText)
                                if (obj && obj.message) {
                                    errorMessage = obj.message;
                                }

                                if (obj.data && obj.data.header) {
                                    title = obj.data.header;
                                }
                            }
                            else {
                                errorMessage = e.responseJSON?.errorMessage || e.responseJSON?.message || "Unknown error";
                            }

                            bootbox.alert({ size: "small", title: title, message: errorMessage });
                            $.LoadingOverlay("hide");
                        }
                    }).then(setTimeout(function () {
                        $.LoadingOverlay("hide");
                    }, 2e3));
                }
            }
        });

    });

    function callDelete(e) {
        var td = $(e.parentNode);
        var url_delete = td.data('url-delete');
        var url_back = td.data('url-back');

        //var token = $('input[name="__RequestVerificationToken"]').val();
        //if (!token) {
        //    token = 'None';
        //}
        bootbox.confirm({
            title: 'Confirmation',
            message: 'You are about to delete one record. Continue?',
            size: 'small',
            closeButton: false,
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        type: 'GET',
                        dataType: "json",
                        url: url_delete,
                        //contentType: "application/x-www-form-urlencoded",
                        //headers: { 'RequestVerificationToken': token },
                        success: function (response) {
                            var title = "Information";
                            if (response.success === false) {
                                title = "Warning";
                            }

                            bootbox.alert({
                                size: "small",
                                title: title,
                                message: response.message,
                                closeButton: false,
                                callback: function () {
                                    window.location.href = url_back;
                                }
                            });
                        },
                        error: function (e, t, s) {
                            var title = "Error";
                            if (e.responseText) {
                                var obj = JSON.parse(e.responseText)
                                if (obj && obj.message)
                                    var errorMessage = obj.message;
                                else
                                    var errorMessage = e.responseJSON.errorMessage || e.responseJSON.message || e.statusText || "Ooops, something went wrong !";

                                if (obj.data && obj.data.header) {
                                    title = obj.data.header;
                                }
                            }
                            else {
                                var errorMessage = e.statusText || e.message;
                            }

                            bootbox.alert({ size: "small", title: title, message: errorMessage });
                        }
                    }).then(setTimeout(function () {
                        $.LoadingOverlay("hide");
                    }, 2e3));;
                }
            }
        });
    }

    $('.select2').select2();

    function fnTableItems() {
        bootbox.alert({
            title: 'Perhatian !',
            message: 'data item masih kosong.',
            size: 'small',
            closeButton: false,
        });

        return false;
    };
    function fnMandatory() {
        bootbox.alert({
            title: 'Perhatian !',
            message: 'Field mandatory harus diisi.',
            size: 'small',
            closeButton: false,
        });

    }

    window.fnMandatory = fnMandatory;
    window.fnTableItems = fnTableItems;
    window.callDelete = callDelete;
});