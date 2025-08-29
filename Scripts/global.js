jQuery.validator.addMethod("greaterThanZero", function (value, element) {
    return this.optional(element) || (parseFloat(value) > 0);
}, "Value cannot be zero");

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function isNumberNoZero(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode <= 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function isDecimal(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function numberWithCommas(number) {
    var parts = number.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}





function setNavCounts(data) {

    var all = numberWithCommas(data.MsAll);
    var mfg = numberWithCommas(data.MsManuf);
    var dup = numberWithCommas(data.MsDuplicates);
    var img = numberWithCommas(data.MsImage);

    $("#ct1").text("(" + mfg + ")");
    $("#ct2").text("(" + dup + ")");
    $("#ct3").text("(" + all + ")");
    $("#ct15").text("(" + img + ")");
}


function checkImages() {
    var errSz = 0;
    var errExt = 0;
    var imgNo = 1;

    for (var x = 0; x < 6; x++) {
        //var i = "AddGunPics_" + imgNo;
        var i = "Svc9_" + imgNo;
        var img = $("#img_" + imgNo);
        var q = document.getElementById(i).files;
        if (q.length > 0) {

            var size = getFileSize(q[0]);
            var extValid = getFileExt(q[0]);

            if (!extValid) {
                errExt++;
                $("#" + i).prop('disabled', true);
                img.css('background-color', 'yellow');
                $('#errorag').html('Only Images of type .jpg, .jpeg, .png, .gif can be uploaded');
            }
            if (size > 2048) {
                errSz++;
                $("#" + i).prop('disabled', true);
                img.css('background-color', 'yellow');
                $('#errorag').html('File Too Large! Image must be 2MB or less in file size');
            }
        };

        var tErr = errExt + errSz;
        //if (tErr == 0) { $("#btnAddGun").attr("disabled", false); } else { $("#btnAddGun").attr("disabled", "disabled"); }
        if (tErr == 0) { $("#btnGunSchedAdd").attr("disabled", false); } else { $("#btnGunSchedAdd").attr("disabled", "disabled"); }

        imgNo++;

        $("#" + i).css('pointer-events', 'auto');
    }
}


function revImg(ev) {

    var o = ev.getAttribute("title");
    var d = ev.getAttribute("data-image");
    var imgDv = $("#" + d);
    var imgId = $(imgDv).find('img');
    $(imgId).attr("src", o);

    var pd = $(ev).parent().attr('id');
    $("#" + pd).hide();

    var sd = $(ev).parent().parent().find("[id^=selImg]");
    var sid = $(sd).attr('id');
    $("#" + sid).show();
}


function clearPics() {
    var el = $(this).parent().find("span.btn-file").find("input[name='agimg']").attr("id");
    $("#" + el).removeAttr("disabled");

    var ct = imgErrCt();
    var x = $(this).parent().prev();
    x.css('background-color', '#333333');
    if (ct == 1) {
        $('#errorag').html('');
        $("#btnGunSchedAdd").attr("disabled", false);
    }
}

function getFileSize(f) {
    var size = parseFloat(f["size"] / 1024).toFixed(2);
    return size;
}


function getFileExt(f) {
    var fileExtension = f.name.split(".");
    fileExtension = fileExtension[fileExtension.length - 1].toLowerCase();
    var arrayExtensions = ["jpg", "jpeg", "png", "gif"];
    var extValid = arrayExtensions.lastIndexOf(fileExtension) != -1;
    return extValid;
}


/** BOUND BOOK COMMON **/

function showIfAdd(item) {

        switch (item) {
            case 1:
                $("#addMfg").hide();
                var iv = $("#s7").val();
                if (iv === "-1") {
                    $("#t30").val("");
                    $("#addMfg").show();
                }
                break;
            case 2:
                $("#addImpt").hide();
                var ix = $("#s8").val();
                if (ix === "-1") {
                    $("#t31").val("");
                    $("#addImpt").show();
                }
                break;
            case 3:
                $("html, body").animate({ scrollTop: $(document).height() }, "fast");
                $("#addCal").hide();
                var iz = $("#s10").val();
                if (iz === "-1") {
                    $("#t32").val("");
                    $("#addCal").show();
                }
                break;
            case 4:
                $("#addLockMfg").hide();
                var iw = $("#s29").val();
                if (iw === "- SELECT -") { return; }
                if (iw === "-1") {
                    $("#t33").val("");
                    $("#addLockMfg").show();
                } else {
                    getLockModels(iw, 0);
                }
                break;
            case 5:
                $("#addLockModel").hide();
                var iu = $("#s30").val();
                if (iu === "-1") {
                    $("#t34").val("");
                    $("#addLockModel").show();
                }
                break;
        }
    
}

function addManuf(i) {

    var m = $("#t30");
    var s = $("#s7");
    var dv = $("#addMfg");
    var txt1 = "Manufacturer";
    var isImp = false;

    if (i === 2) {
        m = $("#t31");
        s = $("#s8");
        dv = $("#addImpt");
        txt1 = "Importer";
        isImp = true;
    }
    var n = m.val();

    $.ajax({
        data: "{ newmfg: '" + n + "', imp: '" + isImp + "'}",
        url: "/Inventory/AddManufacturer",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: txt1 + " Already Exists",
                        msg: '<b>' + n + '</b> cannot be added because that ' + txt1 + ' already exists',
                        color: '#000000'
                    });
            } else {


                var si = response.SelectedId;

                $(s).find("option").remove().end();
                s.append("<option>- SELECT -</option>");
                s.append("<option value=\"-1\">- ADD NEW " + txt1.toUpperCase() + " -</option>");

                $.each(response.Manuf, function (i, item) {
                    s.append("<option value=" + item.ManufId + ">" + item.ManufName + "</option>");
                });
                dv.hide();
                s.val(si);
                s.selectpicker("refresh");

            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });

}

function addCaliber() {

    //var s = $("#s10");
    var st = $("#s25").val();
    var m = $("#t32");
    var n = m.val();


    $.ajax({
        data: "{ newcal: '" + n + "', std: '" + st + "'}",
        url: "/Inventory/AddCaliber",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: "Caliber Already Exists",
                        msg: '<b>' + n + '</b> cannot be added because that caliber already exists',
                        color: '#000000'
                    });
            } else {

                var si = response.SelectedId;

                $("#s10").find("option").remove().end();
                $("#s10").append("<option>- SELECT -</option>");
                $("#s10").append("<option value=\"-1\">- ADD NEW CALIBER -</option>");

                $.each(response.Caliber, function (i, item) {
                    $("#s10").append("<option value=" + item.CaliberId + ">" + item.CaliberName + "</option>");
                });
                $("#addCal").hide();
                $("#s10").val(si);
                $("#s10").selectpicker("refresh");

            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });

}


function trimJunk(j) {
    return j.replace(/</g, "&lt;").replace(/>/g, "&gt;");
}


function makeUpcCode(field) {

    var v = ("#" + field);

    $.ajax({
        url: "/Inventory/MakeUpcCode",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            //var responsed = response.IsDuplicate;
            $(v).val(response);

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}


