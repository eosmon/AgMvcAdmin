




function getImages() {

    var gunsPerPg = $("#gunsPerPg").val();
    var curPage = $("#curPg").val();
    var startRow = 1;

    if (gunsPerPg == null) { gunsPerPg = 100; }
    var pgTtl = ((curPage * gunsPerPg) - gunsPerPg) + 1;
    if (curPage > 1) { startRow = pgTtl; }

    $("#ttlPgs").val(startRow); //set total pages


    var mfgId = /^\d+$/.test($("#mid").val()) ? $("#mid").val() : 0;
    var gtpId = /^\d+$/.test($("#gtp").val()) ? $("#gtp").val() : 0;
    var calId = /^\d+$/.test($("#cid").val()) ? $("#cid").val() : 0;
 
    var isMis = $("#c1").prop("checked") ? "true" : "false";


    var fd = new FormData();
    fd.append("MfgId", mfgId);
    fd.append("GtpId", gtpId);
    fd.append("CalId", calId);
    fd.append("IsMis", isMis);
    fd.append("GunsPerPg", gunsPerPg);
    fd.append("StartRow", startRow);
  

    return $.ajax({
        cache: false,
        url: "/DataAdmin/GetImages",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            setNavCounts(data.Count);

            $("#addRows").empty();

            if (!$.trim(data.Imgs)) {
                $("#gunCountHeader").text("0 Items Found");
                $("#ttlRowCt").val(0);
            } else {
                var trc = data.Count.ResultCount;
                $("#ttlRowCt").val(trc);


                $.each(data.Imgs, function (i, item) {

                    var mstId = item.MasterId;
                    var upc = item.UpcCode;
                    var mpn = item.MfgPartNumber;
                    var mfn = item.ManufName;
                    var desc = item.ItemDesc;
                    var xSsi = item.IsSsi;
                    var xWss = item.IsWss;
                    var xLip = item.IsLip;
                    var xDav = item.IsDav;
                    var xRsr = item.IsRsr;
                    var xBhc = item.IsBhc;
                    var xZan = item.IsZan;
                    var xMge = item.IsMge;
                    var xHse = item.IsHse;
                    var xAlt = item.IsAlt;
                    var xCur = item.IsCur;
                    var isHp = item.IsHsePath;

                    var imgSsi = item.ImgSsi;
                    var imgWss = item.ImgWss;
                    var imgLip = item.ImgLip;
                    var imgDav = item.ImgDav;
                    var imgRsr = item.ImgRsr;
                    var imgBhc = item.ImgBhc;
                    var imgZan = item.ImgZan;
                    var imgMge = item.ImgMge;
                    var imgHse = item.ImgHse;
                    var imgAlt = item.ImgAlt;
                    var imgCur = item.ImgCur;

                    if (!xCur) { imgCur = "/Common/Images/NoImg.jpg"; }
                    if (!xHse) { imgHse = "/Common/Images/AddImg.jpg"; }

                    var imgDoc = "imgDoc_" + mstId;
                    var imgFile = "ImgFile" + mstId;
                    var ticks = Math.floor(Math.random() * 99999999);
                    var newImgName = 'ALT' + ticks;
                    var newDesc = mfn + ": " + desc + " UPC: " + upc + " MPN: " + mpn;
                    //var hDsp = xHse ? "inline-block" : "none";

                    //var js = isHp ? "updHseImg(this)" : "addNewImg(this)";
                    var sAlt = xAlt ? "inline-block" : "none";
                    var js = "addNewImg(this)";

                    var block = "<div class='dragdrop' data-id='" + mstId + "' style='display: inline-grid; width:100%; background:black; border-bottom:solid 2px white'>";
                    block += "<div style='color:white; background:mediumblue; padding:2px; padding-left:7px'><b><span style='color:yellow'>" + mstId + "</b></span> - " + newDesc + "</div>";
                    block += "<div style='clear:both'>";
                    if (xSsi) {
                        block += "<div id='iSsi" + mstId + "' class='images-imgFrame'><div><img id='IgSsi" + mstId + "' src='" + imgSsi + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 1)'>SSI</a></div></div>";
                    }
                    if (xWss) {
                        block += "<div id='iWss" + mstId + "' class='images-imgFrame'><div><img id='IgWss" + mstId + "' src='" + imgWss + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 2)'>WSS</a></div></div>";
                    }
                    if (xLip) {
                        block += "<div id='iLip" + mstId + "' class='images-imgFrame'><div><img id='IgLip" + mstId + "' src='" + imgLip + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 3)'>LIP</a></div></div>";
                    }
                    if (xDav) {
                        block += "<div id='iDav" + mstId + "' class='images-imgFrame'><div><img id='IgDav" + mstId + "' src='" + imgDav + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 5)'>DAV</a></div></div>";
                    }
                    if (xRsr) {
                        block += "<div id='iRsr" + mstId + "' class='images-imgFrame'><div><img id='IgRsr" + mstId + "' src='" + imgRsr + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 6)'>RSR</a></div></div>";
                    }
                    if (xBhc) {
                        block += "<div id='iBhc" + mstId + "' class='images-imgFrame'><div><img id='IgBhc" + mstId + "' src='" + imgBhc + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 8)'>BHC</a></div></div>";
                    }
                    if (xZan) {
                        block += "<div id='iZan" + mstId + "' class='images-imgFrame'><div><img id='IgZan" + mstId + "' src='" + imgZan + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 12)'>ZAN</a></div></div>";
                    }
                    if (xMge) {
                        block += "<div id='iMge" + mstId + "' class='images-imgFrame'><div><img id='IgMge" + mstId + "' src='" + imgMge + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 13)'>MGE</a></div></div>";
                    }
                    if (xHse) {
                        block += "<div id='iHse" + mstId + "' class='images-imgFrame'><div><img id='IgHse" + mstId + "' src='" + imgHse + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 25)'>HSE</a></div></div>";
                    }
                    //if (xAlt) {
                    block += "<div id='iAlt" + mstId + "' class='images-imgFrame' style='display:"+sAlt+"'><div><img id='IgAlt" + mstId + "' src='" + imgAlt + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div><a class='feed-uplPicTxt' href='#' onclick='updateImg(" + mstId + ", 25)'>ALT</a></div></div>";
                    //}

                    block += "<div id='iCur" + mstId + "' class='images-imgFrame' style='float:right'><div><img id='IgCur" + mstId + "' src='" + imgCur + "' alt='' style='width:77px; height:auto; border-radius:4px; border:solid 1px #00FF00'></div><div id='curTxt" + mstId + "'>Current Img</div></div>";

                    block += "<div class='images-imgFrame' style='float:right; background:darkgray'>";
                    block += "<div id='divImg" + mstId + "' class='fileinput fileinput-new img-loop block-pics' data-provides='fileinput' style='max-width:80px'>";
                    block += "<div id='" + imgDoc + "' class='fileinput-preview thumbnail picFrame' data-trigger='fileinput' style='width: 77px; height: 58px; border:solid 1px #00FF00'><img id='img" + mstId + "' src='" + imgHse + "' style='width:80px' />";
                    block += "</div>";
                    block += "<div style='margin-top:-7px'>";
                    block += "<span class='btn-file'><a href='#' id='selImg" + mstId + "' class='fileinput-new feed-uplPicTxt' data-trigger='fileinput' onclick='showEdit(this)'>Select Image</a><input type='file' name='imgstateid' id='" + imgFile + "'></span>";
                    block += "<div class='feed-uplPicTxt' style='padding-top: 2px' id='editOpt" + mstId + "'>";
                    block += "<a href='#' class='fileinput-exists feed-uplPicTxt img-loop' title='" + imgCur + "' data-image='" + imgDoc + "' onclick='revImg(this)'>Cancel</a>";
                    block += "<span class='fileinput-exists feed-uplPicTxt img-loop' data-dismiss='fileinput'> | </span>";

                    block += "<a href='#' class='fileinput-exists feed-uplPicTxt img-loop' data-image='" + imgFile + "' data-new-image='" + newImgName + "' data-id='" + mstId + "' data-file='" + imgCur + "' onclick='" + js + "'>Update</a>";

                    block += "</div>";
                    block += "</div>";
                    block += "</div>";
                    block += "</div>";
                    block += "</div>";

                    block += "</div>";
                    $('#addRows').append(block);

                });
            }
        },
        complete: function () {
            var c = $("#ttlRowCt").val();
            c = numberWithCommas(c);
            $(".images-count").text(c + ' Guns Found');
            $(".selectpicker").selectpicker("refresh");

            if (c === 0) {

            } else {
                $("#divPage").show();
                $("#noData").hide();
                setAllPagers();
                showPgRange();
            }
        },
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}


function addNewImg(ev) {


    var d = ev.getAttribute("data-image");
    var ni = ev.getAttribute("data-new-image");
    var id = ev.getAttribute("data-id");
    var f = document.getElementById(d).files;

    if (f.length === 1) {

        var fileData = new FormData();
        fileData.append(d, f[0]);
        fileData.append('MasterId', id);
        fileData.append('NewImg', ni);

        $.ajax({
            cache: false,
            url: "/DataAdmin/UpdateImage",
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (data) {
                var dtm = new Date().getTime();
                var url = data.ImgUrl + "?" + dtm;
                var divImg = $("#imgDoc_" + id);
                var showImg = $("#selImg" + id);
                var hideImg = $("#editOpt" + id);
                var altIg = "#IgAlt" + id;
                var curIg = "#IgCur" + id;
                var alt = "#iAlt" + id;

                var f = $("#editOpt4").find('a:first');
                $(f).attr("title", url);
                
                $(hideImg).hide();
                $(showImg).show();
 

                $(alt).show();
                $(curIg).attr("src", url);
                $(altIg).attr("src", url);

                $(divImg).css("border-color", "#00FF00");

                Lobibox.alert('success',
                    {
                        title: 'Image Updated',
                        msg:
                            'New Gun Image Successfuly Updated!'
                    });


            },
            complete: function () {

            },
            error: function (err, data) {
                alert("Status : " + data.responseText);
            }
        });
    }
}

//function updHseImg(ev) {

//    var d = ev.getAttribute("data-image");
//    var id = ev.getAttribute("data-id");
//    var df = ev.getAttribute("data-file");
//    var fn = df.replace(/^.*[\\\/]/, '');
//    var f = document.getElementById(d).files;

//    if (f.length === 1) {

//        var fileData = new FormData();
//        fileData.append('Files', f[0]);
//        fileData.append('MasterId', id);
//        fileData.append('ImgName', fn);

//        $.ajax({
//            cache: false,
//            url: "/DataAdmin/UpdateHseImage",
//            type: "POST",
//            contentType: false, // Not to set any content header
//            processData: false, // Not to process data
//            data: fileData,
//            success: function (data) {
//                var dtm = new Date();
//                var url = data.ImgUrl + "?" + dtm;
//                var divImg = $("#imgDoc_" + id);
//                var showImg = $("#selImg" + id);
//                var hideImg = $("#editOpt" + id);
//                //var imgId = $(divImg).find('img');
//                var hseIg = "#IgHse" + id;
//                var curIg = "#IgCur" + id;
//                var hse = "#iHse" + id;

//                var f = $("#editOpt4").find('a:first');
//                $(f).attr("title", url);

//                $(curIg).attr("src", url);
//                $(hseIg).attr("src", url);

//                $(hideImg).hide();
//                $(showImg).show();
//                $(hse).show();

//                $(divImg).css("border-color", "#00FF00");

//                Lobibox.alert('success',
//                    {
//                        title: 'Image Updated',
//                        msg:
//                            'New Gun Image Successfuly Updated!'
//                    });


//            },
//            complete: function () {

//            },
//            error: function (err, data) {
//                alert("Status : " + data.responseText);
//            }
//        });
//    }
//}



function updateImg(mstId, dstId) {

    var p = '';
    var i = '';

    switch(dstId) {
        case 1:
            p = "#iSsi" + mstId;
            i = "#IgSsi" + mstId;
            break;
        case 2:
            p = "#iWss" + mstId;
            i = "#IgWss" + mstId;
            break;
        case 3:
            p = "#iLip" + mstId;
            i = "#IgLip" + mstId;
            break;
        case 5:
            p = "#iDav" + mstId;
            i = "#IgDav" + mstId;
            break;
        case 6:
            p = "#iRsr" + mstId;
            i = "#IgRsr" + mstId;
            break;
        case 8:
            p = "#iBhc" + mstId;
            i = "#IgBhc" + mstId;
            break;
        case 9:
            p = "#iGrn" + mstId;
            i = "#IgGrn" + mstId;
            break;
        case 12:
            p = "#iZan" + mstId;
            i = "#IgZan" + mstId;
            break;
        case 13:
            p = "#iMge" + mstId;
            i = "#IgMge" + mstId;
            break;
        case 25:
            p = "#iHse" + mstId;
            i = "#IgHse" + mstId;
            break;
    }


    var fd = new FormData();
    fd.append("MstId", mstId);
    fd.append("DstId", dstId);

    $.ajax({
        cache: false,
        url: "/DataAdmin/UpdateFeedImage",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

        },
        complete: function () {
            var c = "#iCur" + mstId;
            var ct = "#curTxt" + mstId;
            var cur = "#IgCur" + mstId;
            var oi = $(i).attr("src");

            $(cur).attr("src");

            clearAllBg(mstId);
            $(p).css("background-color", "#00FF00");
            $(c).css("background-color", "#00FF00");

            $(ct).text("Updated!");
            $(ct).css("color", "black");
            $(ct).css("font-weight", "bold");

            $(cur).attr("src", oi);
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function clearAllBg(mstId) {
    $("#iSsi" + mstId).css("background-color", "black");
    $("#iWss" + mstId).css("background-color", "black");
    $("#iLip" + mstId).css("background-color", "black");
    $("#iDav" + mstId).css("background-color", "black");
    $("#iRsr" + mstId).css("background-color", "black");
    $("#iBhc" + mstId).css("background-color", "black");
    $("#iGrn" + mstId).css("background-color", "black");
    $("#iZan" + mstId).css("background-color", "black");
    $("#iMge" + mstId).css("background-color", "black");
    $("#iHse" + mstId).css("background-color", "black");
    $("#iCur" + mstId).css("background-color", "black");
}

function setImgMenus() {

    var chk = $("#c1").prop("checked") ? "true" : "false";

    var fd = new FormData();
    fd.append("IsMis", chk);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetImgMenuAll",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindGenericMenu(data.Manuf, "s2", "MANUFACTURERS");
            bindGenericMenu(data.GunType, "s3", "GUN TYPES");
            bindGenericMenu(data.Caliber, "s4", "CALIBERS");
            getImages();
        },
        complete: function () {
            $("#mid").val("0");
            $("#gtp").val("0");
            $("#cid").val("0");
            $(".selectpicker").selectpicker("refresh");
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setImgMenuMfg(v) {

    $("#mid").val(v);
    var chk = $("#c1").prop("checked") ? "true" : "false";

    var fd = new FormData();
    fd.append("MfgId", v);
    fd.append("IsMis", chk);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetImgMenuMfg",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindGenericMenu(data.GunType, "s3", "GUN TYPES");
            bindGenericMenu(data.Caliber, "s4", "CALIBERS");

        },
        complete: function () {
            $("#gtp").val("0");
            $("#cid").val("0");
            $(".selectpicker").selectpicker("refresh");
            getImages();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setMenuGunType(v) {
    $("#gtp").val(v);
    var mfgId = $("#mid").val();
    var chk = $("#c1").prop("checked") ? "true" : "false";

    var fd = new FormData();
    fd.append("MfgId", mfgId);
    fd.append("GtpId", v);
    fd.append("IsMis", chk);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetImgMenuGtp",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindGenericMenu(data.Caliber, "s4", "CALIBERS");
        },
        complete: function () {
            $("#cid").val("0");
            $(".selectpicker").selectpicker("refresh");
            getImages();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setMenuCaliber(v) {
    $("#cid").val(v);
    getImages();
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


function bindAll() {
    $("#mid").val("0");
    $("#gtp").val("0");
    $("#cid").val("0");
    setImgMenus();
}


/* GUNS PER PAGE */
function setPageCount(count) {
    $("#gunsPerPg").val(count);
    $("#curPg").val(1);
    getImages();
}


function setAllPagers() {
    setPaging("#pagerDt1");
}

function setPaging(pg) {

    $(pg).empty();

    var trc = $("#ttlRowCt").val();
    var gpp = $("#gunsPerPg").val();
    var cp = $("#curPg").val();

    var icp = parseInt(cp);
    var iGpp = parseInt(gpp);
    var iTrc = parseInt(trc);

    //set defaults
    if (icp === 0) { icp = 1; }
    if (iGpp == null) { iGpp = 100; }
    var ttlPages = Math.ceil(iTrc / iGpp);

    $(pg + " a").removeClass("active").removeClass("isDisabled");
    $(pg).append("<a id='pp' href='#' onclick='navBack(this)'>&laquo;</a>");

    for (var z = 1; z < ttlPages + 1; z++) { $(pg).append("<a class='bluff' href='#' onclick='navToPg(this)'>" + z + "</a>"); }

    if (icp < 10) { $(pg + " a.bluff:gt(9)").hide(); }
    else {
        var num = parseInt(icp);
        var l = 0;
        var u = 0;

        if (icp % 10 === 0) {
            l = num - 10;
            u = num - 1;

        } else {
            var ns = cp.substr(0, cp.length - 1);
            var nb = (ns + "0");
            l = parseInt(nb);
            u = parseInt(nb) + 9;
        }

        $(pg + " a.bluff:gt(0)").show();
        $(pg + " a.bluff:lt(" + (l) + ")").hide();
        $(pg + " a.bluff:gt(" + (u) + ")").hide();
    }

    $(pg + " a.bluff:eq(" + (icp - 1) + ")").addClass("isDisabled").addClass("active");
    $(pg).append("<a id='np' href='#' onclick='navNext(this)'>&raquo;</a>");
}


/* PAGER: NAVIGATE BY PAGE NUMBER*/
function navToPg(sender) {

    if ($(sender).hasClass("active")) { return; }
    else {
        var item = $(sender).index();
        $("#curPg").val(item);
        getImages();
    }
}


function navBack(sender) {
    var id = $(sender).closest("div").prop("id");
    var item = $("#" + id + " a.active").index();

    if (item === 1) { return; } else {
        item--;
        $("#curPg").val(item);
        $("#" + id + " a").removeClass("active");

        /* SHOW PREV PAGE GROUP AT MAX TAB*/
        if (item % 10 === 0) {
            $("#" + id + " a.bluff").show();
            $("#" + id + " a.bluff:gt(" + (item - 1) + ")").hide();
            if (item > 10) { $(sender + " a.bluff:lt(" + (item - 10) + ")").hide(); }
        }
        $("#" + id + " a.bluff:eq(" + (item - 1) + ")").addClass("active");
        getImages();
    }
}


function navNext(sender) {
    var id = $(sender).closest("div").prop("id");

    var item = $("#" + id + " a.active").index();
    var trc = parseInt($("#ttlRowCt").val());
    var gpp = parseInt($("#gunsPerPg").val());
    var ttlPages = Math.ceil(trc / gpp);

    if (item === ttlPages) { return; }
    else {

        item++;
        $("#curPg").val(item);
        getImages();
    }
}


function showPgRange() {

    var curPg = parseInt($("#curPg").val());
    var trc = parseInt($("#ttlRowCt").val());
    var gpp = parseInt($("#gunsPerPg").val());
    var toPg = parseInt($("#ttlPgs").val());

    if (curPg === 0) { curPg = 1; }

    var tr = (gpp + toPg) - 1;
    var ttlPages = Math.ceil(trc / gpp);

    if (curPg === ttlPages) { tr = (trc - gpp) + gpp; }

    var txt = "(" + toPg + "-" + tr + " Shown Below)";

    $(".range-count").text(txt);
}