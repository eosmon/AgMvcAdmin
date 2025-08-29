function callBind() {
    bindMfgGrid();
}


function bindScroll() {
    hsLoadGrid();
}


function bindMfgGrid() {

    var distId = $("#s1 option:selected").val();
    var onFeed = $("#c1").prop("checked") ? "true" : "false";
    var isParent = $("#c2").prop("checked") ? "true" : "false";

    var fd = new FormData();
    fd.append("DistId", distId);
    fd.append("IsOnFeed", onFeed);
    fd.append("IsParent", isParent);

    return $.ajax({
        cache: false,
        url: "/DataAdmin/GetManufGrid",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            setNavCounts(data.Count);

            $("#addRows").empty();


            if (!$.trim(data.Mfg)) {
                $("#trc").val(0);
            } else {
                var trc = data.Mfg.length;
                $("#trc").val(trc);

                var rc = "#FFF7E5";

                $.each(data.Mfg, function (i, item) {

                    var mId = item.ManufId;
                    var pId = item.ParentId;

                    var xSsi = item.IsOnSsi ? "X" : "";
                    var xWss = item.IsOnWss ? "X" : "";
                    var xLip = item.IsOnLip ? "X" : "";
                    var xDav = item.IsOnDav ? "X" : "";
                    var xRsr = item.IsOnRsr ? "X" : "";
                    var xBhc = item.IsOnBhc ? "X" : "";
                    var xGrn = item.IsOnGrn ? "X" : "";
                    var xZan = item.IsOnZan ? "X" : "";
                    var xMge = item.IsOnMge ? "X" : "";

                    var xIof = item.IsOnFeed;
                    var xAtv = item.IsActive;

                    var x1 = xIof === true ? "checked" : "";
                    var x2 = xAtv === true ? "checked" : "";

                    var sMfg = item.ManufName;
                    var sPar = item.ParentName;
                    var sUrl = item.ManufUrl;

                    var block = "<div data-id='" + mId + "' class='mfg-Row' style='background-color:" + rc + "'>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; font-weight:bold'>" + mId + "</div></div>";
                    block += "<div class='mfg-lBrd pt-4'><div style='text-align:center'><input type='text' class='menu-2 mu-w3' id='txtPar" + mId + "' value='" + pId + "'/></div></div>";
                    block += "<div class='mfg-lBrd pt-4'><div style='text-align:center'><input type='text' class='menu-2 mu-w4' id='txtMfg" + mId + "' value='" + sMfg + "'/></div></div>";
                    block += "<div class='mfg-lBrd'><div style='text-align:center'><a href='#' alt='' id='lnkPar" + mId + "'>" + sPar + "</a></div></div>";
                    block += "<div class='mfg-lBrd pt-4'><div style='text-align:center'><input type='text' class='menu-2 mu-w5' id='txtUrl" + mId + "' value='" + sUrl + "'/></div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center'><input type='checkbox' id='chkFeed" + mId + "' " + x1 + "/></div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center'><input type='checkbox' id='chkAtv" + mId + "' " + x2 + "/></div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; color:blue; font-weight:bold'>" + xSsi + "</div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; color:blue; font-weight:bold'>" + xWss + "</div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; color:blue; font-weight:bold'>" + xLip + "</div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; color:blue; font-weight:bold'>" + xDav + "</div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; color:blue; font-weight:bold'>" + xRsr + "</div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; color:blue; font-weight:bold'>" + xBhc + "</div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; color:blue; font-weight:bold'>" + xGrn + "</div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; color:blue; font-weight:bold'>" + xZan + "</div></div>";
                    block += "<div class='mfg-lBrd pt-9'><div style='text-align:center; color:blue; font-weight:bold'>" + xMge + "</div></div>";
                    block += "<div class='mfg-lBrd pt-6'><div style='text-align:center'><button class='btn btn-xs btn-org-sm' id='btnRowMfg" + mId + "' onclick='updateRow(" + mId + ")'>Update</button></div></div>";
                    block += "</div>";
                    $('#addRows').append(block);

                    rc = rc === "#FFF7E5" ? "#FAFAFB" : "#FFF7E5";

                });
            }

        },
        complete: function () {
            var c = $("#trc").val();
            c = numberWithCommas(c);
            $(".gun-count").text(c + ' Manufacturers Found');

            if (c === 0) {
                $("#noData").show();
            } else {
                $("#addRowHeader").show();
                $("#noData").hide();
            }
        },
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}



function setMenuMapCode(v) {
    $("#did").val(v);

    var fd = new FormData();
    fd.append("DistId", v);
 
    $.ajax({
        cache: false,
        url: "/DataAdmin/GetDistMapCodes",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindCodesMenu(data, "s3", "SELECT");
        },
        complete: function () {
            $("#s4").prop('selectedIndex', 0);
            $("#t2").val("");
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function assignMfg() {
    var distId = $("#s2 option:selected").val();
    var mapCode = $("#s3 option:selected").val();
    var mfgId = $("#s4 option:selected").val();

    var fd = new FormData();
    fd.append("DistId", distId);
    fd.append("ManufId", mfgId);
    fd.append("MapCode", mapCode);

    $.ajax({
        cache: false,
        url: "/DataAdmin/AssignToMfg",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            //bind menus
        },
        complete: function () {
            //$("#s4").prop('selectedIndex', 0);
            //$("#t2").val("");
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function createMfg() {
    var distId = $("#s2 option:selected").val();
    var mapCode = $("#s3 option:selected").val();
    var newMfg = $("#t2").val();

    alert(newMfg);

    var fd = new FormData();
    fd.append("DistId", distId);
    fd.append("NewMfg", newMfg);
    fd.append("MapCode", mapCode);

    $.ajax({
        cache: false,
        url: "/DataAdmin/CreateNewMfg",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            //bind menus
        },
        complete: function () {
            //$("#s4").prop('selectedIndex', 0);
            //$("#t2").val("");
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}



function bindCodesMenu(data, ctl, lbl) {
    var gt = $("#" + ctl);

    gt.find('option').remove().end().append("<option value='0'>- " + lbl + " -</option>").val("0");

    $.each(data, function (i, item) {
        gt.append("<option value=" + item.Link + ">" + item.MenuText + "</option>");
    });
}



function bindGenericMenu(data, ctl, lbl) {
    var gt = $("#" + ctl);

    gt.find('option').remove().end().append("<option value='0'>- ALL " + lbl + " -</option>").val("0");
    gt.selectpicker("refresh");

    $.each(data, function (i, item) {
        gt.append("<option value=" + item.IntValue + " data-subtext=(" + item.ItemCount + ")>" + item.MenuText + "</option>");
    });
    gt.selectpicker("refresh");
}


function updateRow(id) {

    var pid = $("#txtPar" + id);
    var mfg = $("#txtMfg" + id);
    var url = $("#txtUrl" + id);
    var onf = $("#chkFeed" + id);
    var atv = $("#chkAtv" + id);

    var p = $(pid).val();
    var m = $(mfg).val();
    var u = $(url).val();
    var f = $(onf).prop("checked") ? "true" : "false";
    var t = $(atv).prop("checked") ? "true" : "false";

    var fd = new FormData();
    fd.append("MfgId", id);
    fd.append("ParId", p);
    fd.append("MfgTx", m);
    fd.append("UrlTx", u);
    fd.append("IsDfd", f);
    fd.append("IsPar", t);

    $.ajax({
        cache: false,
        url: "/DataAdmin/UpdateManuf",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
        },
        complete: function () {

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function hsLoadGrid() {

    var onFeed = $("#c1").prop("checked") ? "true" : "false";
    var isParent = $("#c2").prop("checked") ? "true" : "false";
    var isActive = $("#c3").prop("checked") ? "true" : "false";

    var fd = new FormData();
    fd.append("IsOnFeed", onFeed);
    fd.append("IsParent", isParent);
    fd.append("IsActive", isActive);

    return $.ajax({
        cache: false,
        url: "/DataAdmin/HomeScrollGrid",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            $("#scrollRows").empty();

            if (data.SA) {
                $("#s1").val("true");
                msgHsStatus(true);
            } else {
                $("#s1").val("false");
                msgHsStatus(false);
            }

                var trc = data.MF.length;
                $("#trc").val(trc);

                var rc = "#FFF7E5";
                var ct = 1;

                $.each(data.MF, function (i, item) {

                    var id = item.ManufId;
                    var scAtv = item.IsScrollActive;
                    var mfgNm = item.ManufName;
                    var sigUl = item.ScrollImgUrl;
                    var bgClr = item.ScrollBgColor;
                    var mfUrl = item.ManufUrl;

                    var fnx = sigUl.lastIndexOf("/") + 1;
                    var igo = sigUl.substr(fnx);
                    var fName = igo.length > 0 ? igo : "";

 

                    var chk = scAtv ? "checked='checked'" : "";

                    var block = "<div style='background-color: " + rc + "' class='hs-row'  data-id='" + id + "'>";
                    block += "<div class='hs-row-item' style='text-align:center; font-weight:bold'>" + ct + "</div>";
                    block += "<div class='hs-row-item' style='text-align:center; font-weight:bold'>" + mfgNm + "</div>";
                    block += "<div class='hs-row-item' style='text-align:center'><input type='checkbox' id='ckAtv" + id + "' " + chk + "/></div>";
                    block += "<div style='padding-top:10px; text-align:center'><input type='text' class='ag-control input-sm pad-lft' style='width:85%' name='ColTx" + id + "' id='ColTx" + id + "' value='" + bgClr + "' onkeyup='checkHexCode(this.value, " + id + ")' maxlength='7'></div>";
                    block += "<div class='fileinput fileinput-new' data-provides='fileinput' style='margin-bottom: 0 !important; padding-top: 2px; padding-bottom: 2px'>";
                    block += "<div style='background:" + bgClr + "; text-align:center; max-width: 165px' class='fileinput-preview thumbnail logo-frame' data-trigger='fileinput' id='imgRow" + id + "' data-id='" + fName + "'>";
                    if (sigUl.length > 0) {
                        block += "<img src='" + sigUl + "' id='imgHs" + id + "' style='max-width:116px' />";
                    }
                    block += "</div>";
                    block += "<div class='hs-row-item hs-add'><span class='btn-file'>";
                    if (sigUl.length > 0) {
                        block += "<a id='lnkAdd" + id + "' href='#' class='fileinput-new uploadTxt' data-trigger='fileinput' style='display:none' onclick=\"swapLink(" + id + ", true, '')\">Add</a><input type='file' name='adImg" + id + "' id='adImg" + id + "'></span><a id='lnkNix" + id + "' href='#' class='btn fileinput-exists uploadTxt' data-dismiss='fileinput' style='position:relative; top: -5px; left: -3px; display:block' onclick=\"swapLink(" + id + ", false, '"+fName+"')\">Remove</a></div>";
                    } else {
                        block += "<a href='#' class='fileinput-new uploadTxt' data-trigger='fileinput'>Add</a><input type='file' name='adImg" + id + "' id='adImg" + id + "'></span><a href='#' class='btn fileinput-exists uploadTxt' data-dismiss='fileinput' style='position:relative; top: -5px; left: -3px' onclick='scrubRow(" + id + ")'>Remove</a></div>";
                    }

                    block += "</div>";
                    block += "<div style='padding-top:10px'><input type='text' class='ag-control input-sm pad-lft txt-wx' name='UrlTx" + id + "' id='UrlTx" + id + "' value='" + mfUrl + "'><span style='padding-left:10px'><a href='" + mfUrl + "' target='_blank' class='link12Blue'>view</a></span></div>";
                    block += "<div style='text-align:center; padding-top:8px'><button type='button' class='btn btn-xs btn-blue' style='width:50px !important' onclick='updateHs(" + id + ")'>Update</button></div>";
                    block += "</div>";
                    $('#scrollRows').append(block);

                    ct++;
                    rc = rc === "#FFF7E5" ? "#FAFAFB" : "#FFF7E5";

                });
            

        },
        complete: function () {
            var c = $("#trc").val();
            c = numberWithCommas(c);
            $(".gun-count").text(c + ' Manufacturers Found');

            if (c === 0) {
                $("#scrollNoData").show();
            } else {
                $("#scrollRowHeader").show();
                $("#scrollNoData").hide();
            }
        },
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}

function GetFilename(url) {
    if (url) {
        var m = url.toString().match(/.*\/(.+?)\./);
        if (m && m.length > 1) {
            return m[1];
        }
    }
    return "";
}



function checkHexCode(v, id) {

    if (v.length === 7) {

        var hex = new RegExp('^((0x){0,1}|#{0,1})([0-9A-F]{8}|[0-9A-F]{6})$');
        var t = hex.test(v);

        if (t) {
            var r = $("#imgRow" + id);
            $(r).css('background', v);

        } else {
            Lobibox.alert('error',
                {
                    title: "Invalid HTML Hexidecimal Format",
                    msg: '<b>' + v + '</b> is not a valid color format for HTML. Colors must be in the format <b>#FF00CC</b>.',
                    color: '#000000'
                });

        } 
    }
}

function swapLink(id, a, img) {

    var lAdd = $("#lnkAdd" + id);
    var lNix = $("#lnkNix" + id);


    if (!a) {

        var fd = new FormData();
        fd.append("Id", id);
        fd.append("Img", img);

        var imgId = "adImg" + id;
        var file = document.getElementById(imgId).files;
        if (file.length > 0) {
            fd.append('Files', file[0]);
        }

        $.ajax({
            cache: false,
            url: "/DataAdmin/NixScrollImg",
            type: "POST",
            contentType: false,
            processData: false,
            data: fd,
            success: function (data) {
                hsLoadGrid();
            },
            complete: function () {
                //Lobibox.alert('success',
                //    {
                //        title: 'Update Complete',
                //        msg: 'Image Deleted'
                //    });
            },
            error: function (err, data) {
                alert("Status : " + data.responseText);
            }
        });


        scrubRow(id);
        $(lAdd).show();
        $(lNix).hide();
    } else {
        $(lAdd).hide();
        $(lNix).show();
    }
}

function scrubRow(id) {
    var lCol = $("#ColTx" + id);
    var lChk = $("#ckAtv" + id);

    var r = $("#imgRow" + id);
    $(r).css("background-color", "");
    $(lChk).prop("checked", false);
    $(lCol).val("");

    //AJAX delete temp img
}


function updateHs(id) {

    var lChk = $("#ckAtv" + id);
    var lCol = $("#ColTx" + id).val();
    var lUrl = $("#UrlTx" + id).val();
    var iNam = $("#imgRow" + id).attr("data-id");
    var isCk = $(lChk).prop("checked") ? "true" : "false";

    var iRow = $("#imgRow" + id);
    var imgCt = $(iRow).has("img").length;

    var fd = new FormData();
    fd.append("MfgId", id);
    fd.append("IsAtv", isCk);
    fd.append("Color", lCol);
    fd.append("MfUrl", lUrl);
    fd.append("ImgNm", iNam);
    fd.append("ImgCt", imgCt);

    var imgId = "adImg" + id;
    var file = document.getElementById(imgId).files;
    if (file.length > 0) {
        fd.append('Files', file[0]);
    }

    $.ajax({
        cache: false,
        url: "/DataAdmin/UpdateHomeScroll",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            hsLoadGrid();
        },
        complete: function () {
            Lobibox.alert('success',
                {
                    title: 'Update Complete',
                    msg: 'Manufacturer logo information has been successfully updated'
                });
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function updateHsStatus(v) {

    var fd = new FormData();
    fd.append("IsAtv", v);

    $.ajax({
        cache: false,
        url: "/DataAdmin/UpdateHomeScrollStatus",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            var b = v === "true" ? true : false;
            msgHsStatus(b);
        },
        complete: function () {
            Lobibox.alert('success',
                {
                    title: 'Update Complete',
                    msg: 'Home Scroll Status Successfully Updated'
                });
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function msgHsStatus(b) {
    if (b) { $("#hsStat").css("display", "none"); } else { $("#hsStat").css("display", "inline-block"); }
}