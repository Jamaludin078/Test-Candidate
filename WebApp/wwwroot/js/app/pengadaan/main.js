$("#btn-add-item").on('click', function () {
    $('#modal-default').modal('show');
});

$("#btn-save-item").click(function () {

    var form = document.getElementById('frm-items');
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].value === '' && form.elements[i].hasAttribute('required')) {
            fnMandatory();
            $('#modal-default').modal('show');

            return false;
        }
    }

    AddToTheItemList();
    $('#modal-default').modal('hide');
});

function GenerateDetail() {
    var arr = [];

    $("#tbl-items").find("tr:gt(0)").each(function () {
        var values = {
            'nama_item': $(this).find("td:eq(0)").text(),
            'spesifikasi_item': $(this).find("td:eq(1)").text(),
            'jumlah': parseInt($(this).find("td:eq(2)").text())
        }

        arr.push(values);
    });

    return arr;
}

$("#btn-save").on('click', function (event) {
    event.preventDefault();

    var form = document.getElementById('frm-add');
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].value === '' && form.elements[i].hasAttribute('required')) {
            fnMandatory();
            return false;
        }
    }

    var items = {
        'pengadaan_no': "1",
        'pengadaan_name': $("#pengadaan_name").val(),
        'pengadaan_kategori': parseInt($("#pengadaan_kategori").val()),
        "tanggal_butuh": $("#tanggal_butuh").val(),// "2024-06-29T11:11:48.271Z",
        'PengadaanDetails': GenerateDetail()
    }

    if (items.PengadaanDetails.length == 0) {
        fnTableItems();

        return false;
    }

    bootbox.confirm({
        title: 'Konfirmasi',
        message: 'Apakah kamu yakin menyimpan data ini ?',
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
                $.LoadingOverlay("show", { size: 50, maxSize: 50, minSize: 50 });
                var token = $('input[name="__RequestVerificationToken"]').val();
                $.ajax({
                    type: "POST",
                    url: '/Pengadaan/Add',
                    data: JSON.stringify(items),
                    dataType: 'JSON',
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        if (result.success) {
                            bootbox.alert({
                                size: "small",
                                title: "Information",
                                message: result.message,
                                callback: function () {
                                    window.location.href = "/pengadaan/index"
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

    //$.ajax({
    //    type: "POST",
    //    url: '/Pengadaan/Add',
    //    data: JSON.stringify(items),
    //    dataType: 'JSON',
    //    contentType: 'application/json; charset=utf-8',
    //    success: function (data) {
    //        alert("S")
    //    },
    //    error: function () {
    //        alert("There is some problem to adding the data.");
    //    }
    //});
});

function ResetItem() {
    $("#nama-item").val('');
    $("#spesifikasi").val('');
    $("#qty").val('');
}
function RemoveItem(namaitem) {
    $(namaitem).closest('tr').remove();
}
function AddToTheItemList() {
    var tblItemList = $("#tbl-items");
    var namaitem = $("#nama-item").val();
    var spesifikasi = $("#spesifikasi").val();
    var qty = $("#qty").val();

    var ItemList = "<tr><td>" + namaitem +
        "<td>" + spesifikasi + "<td>" +
        parseFloat(qty).toFixed(2) +
        "<td class='text-center'> <input type='button' name='remove' value='Delete' onclick='RemoveItem(this)' /> </tr></tr>";

    tblItemList.append(ItemList);
    ResetItem();
}