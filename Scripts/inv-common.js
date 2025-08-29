function abort() {
    location.reload();
}

function showHideActive(v) {
    if (v === "true") {
        $("#dvLiveWeb").show();
        showPics();
    }
    else {
        $("#dvLiveWeb").hide();
        hidePics();
    }
}

function showPics() {
    for (var i = 2; i < 7; i++) { var f = "#Fi_" + i; $(f).show(); }
}

function hidePics() {
    for (var i = 2; i < 7; i++) { var f = "#Fi_" + i; $(f).hide(); }
}


function wipeInvImg(i, cat) {

    Lobibox.confirm({
        title: "Delete Image!",
        msg: "Permanently delete this image? This action cannot be undone",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {

                $('[id^="ImgHse_"]').val('');

                var ih = $("#ImgHse_" + i);
                var img = $(ih).attr("orig-img");
                var isi = $("#isi").val();
                var ttp = $("#ttpId").val();

                var fd = new FormData();
                fd.append("Id", isi);
                fd.append("Idx", i);
                fd.append("Cat", cat);
                fd.append("Ttp", ttp);
                fd.append("Img", img);

                $.ajax({
                    cache: false,
                    url: "/Inventory/NixImage",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (d) {

                        $('[id^="ImgM_"]').empty();

                        if (d.ImgHse1.length > 0) { $("#ImgM_1").append("<img src='" + d.ImgHse1 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_1").attr("orig-img", d.Io1); } else { $("#delCol_1").hide(); }
                        if (d.ImgHse2.length > 0) { $("#ImgM_2").append("<img src='" + d.ImgHse2 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_2").attr("orig-img", d.Io2); } else { $("#delCol_2").hide(); }
                        if (d.ImgHse3.length > 0) { $("#ImgM_3").append("<img src='" + d.ImgHse3 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_3").attr("orig-img", d.Io3); } else { $("#delCol_3").hide(); }
                        if (d.ImgHse4.length > 0) { $("#ImgM_4").append("<img src='" + d.ImgHse4 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_4").attr("orig-img", d.Io4); } else { $("#delCol_4").hide(); }
                        if (d.ImgHse5.length > 0) { $("#ImgM_5").append("<img src='" + d.ImgHse5 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_5").attr("orig-img", d.Io5); } else { $("#delCol_5").hide(); }
                        if (d.ImgHse6.length > 0) { $("#ImgM_6").append("<img src='" + d.ImgHse6 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_6").attr("orig-img", d.Io6); } else { $("#delCol_6").hide(); }
                    },
                    complete: function () {
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}


 


function dropItem() {

    var v = $("#isi").val();
    var id = parseInt(v);
    if (id === 0) { return; }

    Lobibox.confirm({
        title: "Delete This Item?",
        msg: "You are about to permanently delete this item and all restocks. This action is permanent and cannot be undone - continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {

                var fd = new FormData();
                fd.append("Id", id);

                $.ajax({
                    cache: false,
                    url: "/Inventory/NixItem",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {
                        return false;
                    },
                    complete: function () {
                        location.reload();
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}
