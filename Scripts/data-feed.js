 

function chkMissOnly(ev) {

    var ec = $(ev).is(':checked');
    var v = "true";

    if (ec) { v = "false"; }
    $("#mOnly").val(v);

    cookGunsOnly();
    resetMenus();
}


function makeUpdates(i) {
    updateItem(i);
    Lobibox.alert('success',
        {
            title: 'Update Complete',
            msg:
                'Gun ID <b>' + i + '</b> Has Been Updated Successfully'
        });

    return false;
}


function updateAllItems() {
    openFeed();
    $(".feed-Item").each(function() {
        var id = $(this).attr('data-id');
        updateItem(id);
    });
    closeFeed();
    Lobibox.alert('success',
        {
            title: 'Bulk Update Complete!',
            msg:
                'All Items Have Been Updated Successfully'
        });

    return false;
}


function updateItem(i) {

    var cap = $("#iCap" + i).val();
    var brl = $("#iBrl" + i).val();
    var ovl = $("#iOvl" + i).val();
    var chm = $("#iChm" + i).val();
    var wgt = $("#iWgt" + i).val();

    var gtp = $("#sGunType" + i).val();
    var clb = $("#sCaliber" + i).val();
    var atn = $("#sAction" + i).val();
    var fin = $("#sFinish" + i).val();
    var cnd = $("#sCond" + i).val();

    var mdl = $("#txtMdl" + i).text();
    var upc = $("#txtUpc" + i).text();
    var mpn = $("#txtMpn" + i).text();
    var dsc = $("#txtDsc" + i).text();
    var lds = $("textarea#txtLds" + i).val();

    var cAtv = $("#chkAtv_" + i).prop("checked");
    var cVer = $("#chkVer_" + i).prop("checked");
    var cHid = $("#chkHid_" + i).prop("checked");
    var cOfd = $("#chkOfd_" + i).prop("checked");
    var cMdl = $("#chkMdl_" + i).prop("checked");
    var cOvs = $("#chkOvs_" + i).prop("checked");
    var cHca = $("#chkHca_" + i).prop("checked");
    var cCok = $("#chkCal_" + i).prop("checked");
    var cObx = $("#chkObx_" + i).prop("checked");
    var cPpw = $("#chkPpw_" + i).prop("checked");
    var cFfl = $("#chkFfl_" + i).prop("checked");
    var cUsd = $("#chkUsd_" + i).prop("checked");
    var cRst = $("#chkRst_" + i).prop("checked");
    var cCnr = $("#chkCnr_" + i).prop("checked");
    var cSar = $("#chkSar_" + i).prop("checked");
    var cSsp = $("#chkSsp_" + i).prop("checked");
    var cPpt = $("#chkPpt_" + i).prop("checked");
    var cLeo = $("#chkLeo_" + i).prop("checked");

    mdl = mdl === "MISSING" ? "" : mdl;
    mpn = mpn === "MISSING" ? "" : mpn;
    dsc = dsc === "MISSING" ? "" : dsc;


    var fileData = new FormData();
    fileData.append('Id', i);
    fileData.append('Cap', cap);
    fileData.append('Brl', brl);
    fileData.append('Ovl', ovl);
    fileData.append('Chm', chm);
    fileData.append('Wgt', wgt);
    fileData.append('Mdl', mdl);
    fileData.append('Upc', upc);
    fileData.append('Mpn', mpn);
    fileData.append('Dsc', dsc);
    fileData.append('Lds', lds);
    fileData.append('Atv', cAtv);
    fileData.append('Ver', cVer);
    fileData.append('Hid', cHid);
    fileData.append('Ofd', cOfd);
    fileData.append('Cur', cMdl);
    fileData.append('Ovs', cOvs);
    fileData.append('Hca', cHca);
    fileData.append('Cok', cCok);
    fileData.append('Obx', cObx);
    fileData.append('Ppw', cPpw);
    fileData.append('Ffl', cFfl);
    fileData.append('Usd', cUsd);
    fileData.append('Rst', cRst);
    fileData.append('Cnr', cCnr);
    fileData.append('Sar', cSar);
    fileData.append('Ssp', cSsp);
    fileData.append('Ppt', cPpt);
    fileData.append('Leo', cLeo);

    fileData.append('Gtp', gtp);
    fileData.append('Cal', clb);
    fileData.append('Atn', atn);
    fileData.append('Fin', fin);
    fileData.append('Cnd', cnd);

    $.ajax({
        cache: false,
        url: "/DataAdmin/UpdateDataItem",
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (data) {

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });


}


//function findArrKey(arr, val) {

//    var xArr = [];
//    xArr = arr;

//    var cc = xArr.find(x => x.Name === val).Id;
//    alert(cc);
 
//}


function updateImg(ev) {
    

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
                var dtm = new Date();
                var url = data.ImgUrl+"?"+dtm;
                var divImg = $("#imgDoc_" + id);
                var showImg = $("#selImg" + id);
                var hideImg = $("#editOpt" + id);
                var imgId = $(divImg).find('img');

                var f = $("#editOpt4").find('a:first');
                $(f).attr("title", url);
                //var g = $(f).attr('title');
                //alert(g);


                $(imgId).attr("src", url);

                $(hideImg).hide();
                $(showImg).show();

                $(divImg).css("border-color", "#00FF00");

                Lobibox.alert('success',
                    {
                        title: 'Image Updated',
                        msg:
                            'New Gun Image Successfuly Updated!'
                    });


            },
            error: function (err, data) {
                alert("Status : " + data.responseText);
            }
        });
    }
}


function showEdit(ev) {

    var pd = $(ev).attr('id');
    $("#" + pd).hide();

    var sd = $("#" + pd).closest('div').find("[id^=editOpt]");
    var sid = $(sd).attr('id');
    $("#" + sid).show();
 
}


function backToLbl(ev) {

    var txt = '';

    switch (ev.type) {
        case 'text':
            txt = $(ev).val();
            break;
        case 'select-one':
            txt = $("option:selected", ev).text();
            break;
        default:
            break;
    }

    var z = $(ev).parent().parent().find("div.feed-Data");


    z.empty();
    if (txt.length > 0) {
        //alert('yes len');
        z.text(txt);
    } else {
        //alert('empty');
        z.append("<font color='red'>MISSING</font>");
    }

    z.show();
    $(ev).parent().hide();
}


function setOption(ev) {
    
    var z = $(ev).parent().parent().find("div.feed-Data");
    var t = $("option:selected", ev).text();

    var id = $(ev).attr("data-id");
    var sect = $(ev).attr("title");
    var val = $("option:selected", ev).val();

    z.text(t);
    z.show();
    $(ev).parent().hide();

    updateMenuItem(sect, id, val);
}


function updateMenuItem(sect, id, val) {

    var fileData = new FormData();
    fileData.append('MstId', id);
    fileData.append('CatId', sect);
    fileData.append('ValId', val);

    $.ajax({
        cache: false,
        url: "/DataAdmin/UpdateMenuItem",
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function(data) {
            
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function searchTxt() {

    var t = $("#t1").val();
    t = trimJunk(t);
    $("#TxtSch").val(t);
    cookGunsOnly();
}

function clearTxt() {
    $("#t1").val("");
}


function showOption(ev) {

    var n = ev.id;
    var x = [];

    if (n.indexOf("Mfg") != -1) { x = manuf; }
    if (n.indexOf("Gt") != -1) { x = gunTypes; }
    if (n.indexOf("Cal") != -1) { x = calibers; }
    if (n.indexOf("Atn") != -1) { x = actions; }
    if (n.indexOf("Fsh") != -1) { x = finishes; }
    if (n.indexOf("Cnd") != -1) { x = conditions; }


    var t = $(ev).html();

    var dv = $(ev).parents("[id^=divRw]").find("div.feed-DataRow");
    var cbo = $(ev).parents("[id^=divRw]").find("select");

    dv.show();
    $(ev).hide();

    cbo.empty();


    for (var i = 0; i <= x.length; i++) {
        var s = x[i].Name === t ? "selected" : "";
        cbo.append('<option value="' + x[i].Id + '" ' + s + '>' + x[i].Name + '</option>');
    }
}

function showTxtBx(ev) {
    var dv = $(ev).parents("[id^=divRw]").find("div.feed-DataRow");
    var txt = $(ev).parents("[id^=divRw]").find("input");

    dv.show();
    $(ev).hide();
}


function setMsChk() {
    $("#c4").prop("checked", true);

    $("#mAll").val("false");
    $("#mGtp").val("false");
    $("#mCal").val("false");
    $("#mCap").val("false");
    $("#mAtn").val("false");
    $("#mFin").val("false");
    $("#mMdl").val("false");
    $("#mDsc").val("false");
    $("#mLds").val("false");
    $("#mBrl").val("false");
    $("#mOvl").val("false");
    $("#mWgt").val("false");
    $("#mImg").val("false");
}


function Initialize() {
    cookGunsOnly();
}

/** REGION EXPERIMENTAL **/

function getDataParams() {

    var gunsPerPg = $("#gunsPerPg").val();
    var curPage = $("#curPg").val();
    var startRow = 1;

    var txtSrch = $("#TxtSch").val();

    if (gunsPerPg == null) { gunsPerPg = 100; }
    var pgTtl = ((curPage * gunsPerPg) - gunsPerPg) + 1;
    if (curPage > 1) { startRow = pgTtl; }

    $("#ttlPgs").val(startRow); //set total pages

    var mfgId = /^\d+$/.test($("#mid").val()) ? $("#mid").val() : 0;
    var gtpId = /^\d+$/.test($("#gtp").val()) ? $("#gtp").val() : 0;
    var calId = /^\d+$/.test($("#cid").val()) ? $("#cid").val() : 0;
    var atnId = /^\d+$/.test($("#aid").val()) ? $("#aid").val() : 0;
    var dysBk = /^\d+$/.test($("#dbk").val()) ? $("#dbk").val() : 0;

    var bp = baseFilterParams();
    bp.append("MfgId", mfgId);
    bp.append("GtpId", gtpId);
    bp.append("CalId", calId);
    bp.append("AtnId", atnId);
    bp.append("DysBk", dysBk);
    bp.append("GunsPerPg", gunsPerPg);
    bp.append("StartRow", startRow);
    bp.append("TxtSch", txtSrch);

    return bp;
}

function cookGuns(dCt, dGuns) {
    $("#addGunWrap").empty();


    if (!$.trim(dGuns)) {
        $("#ttlRowCt").val(0);
    } else {
        setNavCounts(dCt);
        setCounters(dCt);
        var trc = dGuns[0].Filters.TotalRowCount;


        $("#ttlRowCt").val(trc);

        $.each(dGuns, function (i, item) {

            var xId = item.MasterId;
            var isMs = item.ItemMissing;
            var isImgMs = item.ImageName.length === 0 ? true : false;
            var xMfg = item.ManufName;
            var xImg = item.ImageName;
            var xBrl = item.BarrelDec.toFixed(3);
            var xOvl = item.OverallDec.toFixed(3);
            var xChm = item.ChamberDec.toFixed(3);
            var xWgt = (parseFloat(item.WeightLb) + parseFloat(item.WeightOz)).toFixed(2);
            //var wgtOz = item.WeightOz;
            var xCap = item.CapacityInt;


            var xType = item.GunTypeId > 0 ? item.GunType : "<font color='red'>MISSING</font>";
            var xCal = item.CaliberId > 0 ? item.CaliberTitle : "<font color='red'>MISSING</font>";
            var xAtn = item.ActionId > 0 ? item.ActionType : "<font color='red'>MISSING</font>";

            //var xType = item.GunType;
            //var xCal = item.CaliberTitle.length > 0 ? item.CaliberTitle : "<font color='red'>MISSING</font>";
            //var xAtn = item.ActionType;
            var xFin = item.FinishName.length > 0 ? item.FinishName : "<font color='red'>MISSING</font>";
            var xCnd = item.CondName;

            var xMdl = item.ModelName.length > 0 ? item.ModelName : "<font color='red'>MISSING</font>";
            var xMdlTxt = item.ModelName;
            var xUpc = item.UpcCode.length > 0 ? item.UpcCode : "<font color='red'>MISSING</font>";
            var xUpcTxt = item.UpcCode;
            var xMpn = item.MfgPartNumber.length > 0 ? item.MfgPartNumber : "<font color='red'>MISSING</font>";
            var xMpnTxt = item.MfgPartNumber;
            var xDsc = item.Description.length > 0 ? item.Description : "<font color='red'>MISSING</font>";
            var xDscTxt = item.Description;
            var xLds = item.LongDescription;

            var c1 = item.IsActive;
            var c2 = item.IsVerified;
            var c3 = item.IsHidden;
            var c4 = item.Filters.IsOnDataFeed;
            var c5 = item.IsCurModel;
            var c6 = item.Filters.IsLeo;
            var c7 = item.CaRestrict.CaHide;
            var c8 = item.CaRestrict.CaOkay;
            var c9 = item.OrigBox;
            var c10 = item.OrigPaperwork;
            var c11 = item.IsReqFfl;
            var c12 = item.IsUsed;
            var c13 = item.CaRestrict.CaRosterOk;
            var c14 = item.CaRestrict.CaCurioOk;
            var c15 = item.CaRestrict.CaSglActnOk;
            var c16 = item.CaRestrict.CaSglShotOk;
            var c17 = item.CaRestrict.CaPptOk;

            var x1 = c1 === true ? "checked" : "";
            var x2 = c2 === true ? "checked" : "";
            var x3 = c3 === true ? "checked" : "";
            var x4 = c4 === true ? "checked" : "";
            var x5 = c5 === true ? "checked" : "";
            var x6 = c6 === true ? "checked" : "";
            var x7 = c7 === true ? "checked" : "";
            var x8 = c8 === true ? "checked" : "";
            var x9 = c9 === true ? "checked" : "";
            var x10 = c10 === true ? "checked" : "";
            var x11 = c11 === true ? "checked" : "";
            var x12 = c12 === true ? "checked" : "";
            var x13 = c13 === true ? "checked" : "";
            var x14 = c14 === true ? "checked" : "";
            var x15 = c15 === true ? "checked" : "";
            var x16 = c16 === true ? "checked" : "";
            var x17 = c17 === true ? "checked" : "";


            var cls1 = xCap === 0 ? "feed-Mis" : "feed-Txt45";
            var cls2 = xBrl > 0 ? "feed-Txt45" : "feed-Mis";
            var cls3 = xOvl > 0 ? "feed-Txt45" : "feed-Mis";
            var cls4 = xWgt > 0 ? "feed-Txt45" : "feed-Mis";


            var clrM = isMs === true ? "red" : "yellow";
            var clrI = isImgMs === true ? "red" : "#00FF00";
            var imgDoc = "imgDoc_" + xId;
            var imgFile = "ImgFile" + xId;
            //var dtm = new Date().getTime();
            //var newImgName = item.ManufId + item.UpcCode + "_" + dtm;

            var ticks = Math.floor(Math.random() * 99999999);
            var newImgName = 'ALT' + ticks;

            var block = "<div class='feed-Item' style='border: solid 1px " + clrM + "' data-id='" + xId + "'>";
            block += "<div id='divRw0' class='feed-body'>";
            block += "<div class='feed-Data' style='text-align: center; padding-top: 7px; margin: 0 auto' id='muMfg'  onclick='showOption(this)'>" + xMfg + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<select name='s1' class='feed-CboSm' style='font-size: 9px; width: 200px' onchange='setOption(this)' onblur='backToLbl(this)' title='1' data-id='" + xId + "'>";
            block += "<option value='0'>- SELECT MANUFACTURER -</option>";
            block += "</select>";
            block += "</div></div>";
            block += "<div>";
            block += "<div class='feed-picRowLt'>";
            block += "<div id='divImg" + xId + "' class='fileinput fileinput-new img-loop block-pics' data-provides='fileinput' style='max-width:112px'>";
            block += "<div id='" + imgDoc + "' class='fileinput-preview thumbnail picFrame' data-trigger='fileinput' style='margin-left:3px; min-height: 85px; min-width: 110px; border:solid 1px " + clrI + "'><img id='img" + xId + "' src='" + xImg + "' style='width:100px' />";
            block += "</div>";
            block += "<div class='feed-imgFrame'>";
            block += "<span class='btn-file'><a href='#' id='selImg" + xId + "' class='fileinput-new feed-uplPicTxt' data-trigger='fileinput' onclick='showEdit(this)'>Select Image</a><input type='file' name='imgstateid' id='" + imgFile + "'></span>";
            block += "<div class='feed-uplPicTxt' style='padding-top: 2px' id='editOpt" + xId + "'>";
            block += "<a href='#' class='fileinput-exists feed-uplPicTxt img-loop' title='" + xImg + "' data-image='" + imgDoc + "' onclick='revImg(this)'>Cancel</a>";
            block += "<span class='fileinput-exists feed-uplPicTxt img-loop' data-dismiss='fileinput'> | </span>";
            block += "<a href='#' class='fileinput-exists feed-uplPicTxt img-loop' data-image='" + imgFile + "' data-new-image='" + newImgName + "' data-id='" + xId + "' onclick='updateImg(this)'>Update</a>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-picRowRt'>";
            block += "<div class='feed-decRow'>";
            block += "<div class='feed-LblSm feed-DecW'>Capacity:</div>";
            block += "<div class='feed-Row47'>";
            block += "<input type='text' class='" + cls1 + "' value='" + xCap + "' onkeypress='return isNumber(event)'  maxlength='3' id='iCap" + xId + "'>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-decRow'>";
            block += "<div class='feed-LblSm feed-DecW'>Barrel:</div>";
            block += "<div class='feed-Row47'>";
            block += "<input type='text' class='" + cls2 + "' value='" + xBrl + "' onkeypress='return isDecimal(event)'  maxlength='7' id='iBrl" + xId + "'>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-decRow'>";
            block += "<div class='feed-LblSm feed-DecW'>Overall:</div>";
            block += "<div class='feed-Row47'>";
            block += "<input type='text' class='" + cls3 + "' value='" + xOvl + "' onkeypress='return isDecimal(event)'  maxlength='7' id='iOvl" + xId + "'>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-decRow'>";
            block += "<div class='feed-LblSm feed-DecW'>Chamber:</div>";
            block += "<div class='feed-Row47'>";
            block += "<input type='text' class='feed-Txt45' value='" + xChm + "' onkeypress='return isDecimal(event)'  maxlength='7' id='iChm" + xId + "'>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-decRow'>";
            block += "<div class='feed-LblSm feed-DecW'>Weight:</div>";
            block += "<div class='feed-Row47'>";
            block += "<input type='text' class='" + cls4 + "' value='" + xWgt + "' onkeypress='return isDecimal(event)'  maxlength='6' id='iWgt" + xId + "'>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div style='clear: both'>";
            block += "<div id='rowGunType' class='feed-decRow'>";
            block += "<div class='feed-RwMnLt'>";
            block += "<div class='feed-LblSm feed-MainW'>Gun Type:</div>";
            block += "</div>";
            block += "<div id='divRw1' class='feed-RwMnRt'>";
            block += "<div class='feed-Data' style='padding-top: 5px; padding-left: 3px' id='muGt" + xId + "' onclick='showOption(this)'>" + xType + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<select name='s2' id='sGunType" + xId + "' class='feed-CboSm' style='font-size: 10px; max-width: 156px' onchange='setOption(this)' onblur='backToLbl(this)' title='2' data-id='" + xId + "'>";
            block += "<option value='0'>- SELECT GUN TYPE -</option>";
            block += "</select>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div id='rowCaliber' class='feed-decRow'>";
            block += "<div class='feed-RwMnLt'>";
            block += "<div class='feed-LblSm feed-MainW'>Caliber:</div>";
            block += "</div>";
            block += "<div id='divRw2' class='feed-RwMnRt'>";
            block += "<div class='feed-Data' style='padding-top: 5px; padding-left: 3px' id='muCal" + xId + "' onclick='showOption(this)'>" + xCal + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<select name='s3' id='sCaliber" + xId + "' class='feed-CboSm' style='font-size: 10px; max-width: 156px' onchange='setOption(this)' onblur='backToLbl(this)' title='3' data-id='" + xId + "'>";
            block += "<option value='0'>- SELECT CALIBER -</option>";
            block += "</select>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div id='rowAction' class='feed-decRow'>";
            block += "<div class='feed-RwMnLt'>";
            block += "<div class='feed-LblSm feed-MainW'>Action:</div>";
            block += "</div>";
            block += "<div id='divRw3' class='feed-RwMnRt'>";
            block += "<div class='feed-Data' style='padding-top: 5px; padding-left: 3px' id='muAtn" + xId + "' onclick='showOption(this)'>" + xAtn + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<select name='s4' id='sAction" + xId + "' class='feed-CboSm' style='font-size: 10px; max-width: 156px' onchange='setOption(this)' onblur='backToLbl(this)' title='4' data-id='" + xId + "'>";
            block += "<option value='0'>- SELECT ACTION -</option>";
            block += "</select>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div id='rowFinish' class='feed-decRow'>";
            block += "<div class='feed-RwMnLt'>";
            block += "<div class='feed-LblSm feed-MainW'>Finish:</div>";
            block += "</div>";
            block += "<div id='divRw4' class='feed-RwMnRt'>";
            block += "<div class='feed-Data' style='padding-top: 5px; padding-left: 3px' id='muFsh" + xId + "' onclick='showOption(this)'>" + xFin + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<select name='s5' id='sFinish" + xId + "' class='feed-CboSm' style='font-size: 10px; max-width: 156px' onchange='setOption(this)' onblur='backToLbl(this)' title='5' data-id='" + xId + "'>";
            block += "<option value='0'>- SELECT FINISH -</option>";
            block += "</select>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div id='rowCondition' class='feed-decRow'>";
            block += "<div class='feed-RwMnLt'>";
            block += "<div class='feed-LblSm feed-MainW'>Condition:</div>";
            block += "</div>";
            block += "<div id='divRw5' class='feed-RwMnRt'>";
            block += "<div class='feed-Data' style='padding-top: 5px; padding-left: 3px' id='muCnd" + xId + "' onclick='showOption(this)'>" + xCnd + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<select name='s3' id='sCond" + xId + "' class='feed-CboSm' style='font-size: 10px; max-width: 156px' onchange='setOption(this)' onblur='backToLbl(this)' title='6' data-id='" + xId + "'>";
            block += "<option value='0'>- SELECT CONDITION -</option>";
            block += "</select>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-decRow'>";
            block += "<div class='feed-RwMnLt'>";
            block += "<div class='feed-LblSm feed-MainW'>Model:</div>";
            block += "</div>";
            block += "<div id='divRw6' class='feed-RwMnRt'>";
            block += "<div class='feed-Data' style='padding-top: 5px; padding-left: 3px' id='txtMdl" + xId + "' onclick='showTxtBx(this)'>" + xMdl + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<input type='text' class='feed-TxtM' value='" + xMdlTxt + "' onblur='backToLbl(this)' />";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-decRow'>";
            block += "<div class='feed-RwMnLt'>";
            block += "<div class='feed-LblSm feed-MainW'>UPC Code:</div>";
            block += "</div>";
            block += "<div id='divRw7' class='feed-RwMnRt'>";
            block += "<div class='feed-Data' style='padding-top: 5px; padding-left: 3px' id='txtUpc" + xId + "' onclick='showTxtBx(this)'>" + xUpc + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<input type='text' class='feed-TxtM' value='" + xUpcTxt + "' onblur='backToLbl(this)' />";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-decRow'>";
            block += "<div class='feed-RwMnLt'>";
            block += "<div class='feed-LblSm feed-MainW'>MFG#:</div>";
            block += "</div>";
            block += "<div id='divRw8' class='feed-RwMnRt'>";
            block += "<div class='feed-Data' style='padding-top: 5px; padding-left: 3px' id='txtMpn" + xId + "' onclick='showTxtBx(this)'>" + xMpn + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<input type='text' class='feed-TxtM' value='" + xMpnTxt + "' onblur='backToLbl(this)' />";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-decRow' style='min-height: 38px'>";
            block += "<div class='feed-RwMnLt'>";
            block += "<div class='feed-LblSm feed-MainW'>Desc:</div>";
            block += "</div>";
            block += "<div id='divRw9' class='feed-RwMnRt'>";
            block += "<div class='feed-Data' style='padding-top: 5px; padding-left: 3px' id='txtDsc" + xId + "' onclick='showTxtBx(this)'>" + xDsc + "</div>";
            block += "<div class='feed-DataRow'>";
            block += "<input type='text' class='feed-TxtM' value='" + xDscTxt + "' onblur='backToLbl(this)' />";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-decRow'>";
            block += "<textarea cols='10' rows='5' class='feed-TxtA' id='txtLds" + xId + "'>" + xLds + "</textarea>";
            block += "</div>";
            block += "<div id='chkArr'>";
            block += "<div class='feed-ChkCol'>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkAtv_" + xId + "' " + x1 + " /></div>";
            block += "<div class='feed-LblCk'>Active</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkVer_" + xId + "' " + x2 + " /></div>";
            block += "<div class='feed-LblCk'>Verified</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkHid_" + xId + "' " + x3 + " /></div>";
            block += "<div class='feed-LblCk'>Hide</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkOfd_" + xId + "' " + x4 + " /></div>";
            block += "<div class='feed-LblCk'>On Feed</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkMdl_" + xId + "' " + x5 + " /></div>";
            block += "<div class='feed-LblCk'>Cur Mdl</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkLeo_" + xId + "' " + x6 + " /></div>";
            block += "<div class='feed-LblCk'>LEO Only</div>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-ChkCol'>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkHca_" + xId + "' " + x7 + " /></div>";
            block += "<div class='feed-LblCk'>Hide CA</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkCal_" + xId + "' " + x8 + " /></div>";
            block += "<div class='feed-LblCk'>CA Legal</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkObx_" + xId + "' " + x9 + " /></div>";
            block += "<div class='feed-LblCk'>Orig Box</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkPpw_" + xId + "' " + x10 + " /></div>";
            block += "<div class='feed-LblCk'>Orig Pwk</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkFfl_" + xId + "' " + x11 + " /></div>";
            block += "<div class='feed-LblCk'>Req FFL</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkUsd_" + xId + "' " + x12 + " /></div>";
            block += "<div class='feed-LblCk'>Used</div>";
            block += "</div>";
            block += "</div>";
            block += "<div class='feed-ChkCol'>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkRst_" + xId + "' " + x13 + " /></div>";
            block += "<div class='feed-LblCk'>CA Roster</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkCnr_" + xId + "' " + x14 + " /></div>";
            block += "<div class='feed-LblCk'>CA C&R</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkSar_" + xId + "' " + x15 + " /></div>";
            block += "<div class='feed-LblCk'>CA SA Rev</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkSsp_" + xId + "' " + x16 + " /></div>";
            block += "<div class='feed-LblCk'>CA SS Pst</div>";
            block += "</div>";
            block += "<div>";
            block += "<div class='feed-ChkA'><input type='checkbox' id='chkPpt_" + xId + "' " + x17 + " /></div>";
            block += "<div class='feed-LblCk'>CA PPT</div>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "<div style='padding-bottom: 10px'>";
            block += "<div class='feed-RowBtn'>ID: <b style='color: #00FFFF'>" + xId + "</b></div>";
            block += "<div class='feed-RowBtn'>";
            block += "<button class='feed-BtnBl' id='btnSub" + xId + "' onclick='return makeUpdates(" + xId + ")'>Update</button>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            block += "</div>";
            //var block = "<div class='feed-Item' style='border: solid 1px " + clrM + "'>" + xMfg + "</div>";
            $('#addGunWrap').append(block);

        });

        //var footPgr = "<div style='display:block;padding-top:10px'><div class='paging' id='pagerDt2'></div></div>";
        //$('#addGunWrap').append(footPgr);
    }
}


function cookMenus(data) {
    bindMenuManuf(data);
    bindMenuGunTypes(data);
    bindMenuCaliber(data);
    bindMenuAction(data);
    bindMenuDaysPast(data);
}


function loadRestricted() {
    cookGunsAndMenus();
    clearWait();
}


function cookGunsAndMenus() {

    openFeed();

    var fd = getDataParams();

    return $.ajax({
        cache: false,
        url: "/DataAdmin/GetGunsMenus",
        type: "POST",
        contentType: false,  
        processData: false, 
        data: fd,
        success: function (data) {
            cookGuns(data.Count, data.Guns);

            bindGenericMenu(data.Manuf, "s2", "MANUFACTURERS");
            bindGenericMenu(data.GunType, "s3", "GUN TYPES");
            bindGenericMenu(data.Caliber, "s4", "CALIBERS");
            bindGenericMenu(data.Action, "s5", "ACTIONS");
            bindGenericMenu(data.DaysPast, "s6", "DATES");
        },
        complete: function () {
            var c = $("#ttlRowCt").val();
            c = numberWithCommas(c);
            $(".gun-count").text(c + ' Guns Found');
            $("#mid").val("0");
            $("#gtp").val("0");
            $("#cid").val("0");
            $("#aid").val("0");
            $("#dbk").val("0");
            $("#t1").val("");
            $(".selectpicker").selectpicker("refresh");

            if (c === 0) {
                noneFound();
            } else {
                $("#divPage").show();
                $("#noData").hide();
                setAllPagers();
                showPgRange();
            }

            closeFeed();
        },
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}


function cookGunsOnly() {

    openFeed();

    var fd = getDataParams();

    return $.ajax({
        cache: false,
        url: "/DataAdmin/GetDataGrid",
        type: "POST",
        contentType: false,  
        processData: false, 
        data: fd,
        success: function (data) {
            cookGuns(data.Count, data.Guns);
        },
        complete: function () {
            var c = $("#ttlRowCt").val();
            c = numberWithCommas(c);
            $(".gun-count").text(c + ' Guns Found');
            $(".selectpicker").selectpicker("refresh");

            if (c === 0) {
                noneFound();
            } else {
                $("#divPage").show();
                $("#noData").hide();
                setAllPagers();
                showPgRange();
            }

            closeFeed();
        },
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}



/** END EXPERIMENTAL **/





function noneFound() {
    $("#noData").show();
    $("#divPage").hide();
    var m = "No guns were found with your search parameters or with the keyword(s) selected: \"" + t + "\"";

    $("#noData").text(m);
}




function  setCounters(data) {

    var mAll = data.MsAll;
    var mGtp = data.MsGunType;
    var mCal = data.MsCaliber;
    var mCap = data.MsCapacity;
    var mAtn = data.MsAction;
    var mFin = data.MsFinish;
    var mMdl = data.MsModel;
    var mDsc = data.MsDesc;
    var mLds = data.MsLgDesc;
    var mBrl = data.MsBarrel;
    var mOvl = data.MsOverall;
    var mWgt = data.MsWeight;
    var mImg = data.MsImage;
    var cLgl = data.CaLegal;
    var cRst = data.CaRoster;
    var cSar = data.CaSaRev;
    var cSsp = data.CaSsPst;
    var cCur = data.CaCurio;
    var cPvt = data.CaPvtPt;

    mAll = numberWithCommas(mAll);
    mGtp = numberWithCommas(mGtp);
    mCal = numberWithCommas(mCal);
    mCap = numberWithCommas(mCap);
    mAtn = numberWithCommas(mAtn);
    mFin = numberWithCommas(mFin);
    mMdl = numberWithCommas(mMdl);
    mDsc = numberWithCommas(mDsc);
    mLds = numberWithCommas(mLds);
    mBrl = numberWithCommas(mBrl);
    mOvl = numberWithCommas(mOvl);
    mWgt = numberWithCommas(mWgt);
    mImg = numberWithCommas(mImg);
    cLgl = numberWithCommas(cLgl);
    cRst = numberWithCommas(cRst);
    cSar = numberWithCommas(cSar);
    cSsp = numberWithCommas(cSsp);
    cCur = numberWithCommas(cCur);
    cPvt = numberWithCommas(cPvt);

    $("#ct3").text("(" + mAll + ")");
    $("#ct4").text("(" + mGtp + ")");
    $("#ct5").text("(" + mCal + ")");
    $("#ct6").text("(" + mCap + ")");
    $("#ct7").text("(" + mAtn + ")");
    $("#ct8").text("(" + mFin + ")");
    $("#ct9").text("(" + mMdl + ")");
    $("#ct10").text("(" + mDsc + ")");
    $("#ct11").text("(" + mLds + ")");
    $("#ct12").text("(" + mBrl + ")");
    $("#ct13").text("(" + mOvl + ")");
    $("#ct14").text("(" + mWgt + ")");
    $("#ct15").text("(" + mImg + ")");
    $("#ct16").text("(" + cLgl + ")");
    $("#ct17").text("(" + cRst + ")");
    $("#ct18").text("(" + cSar + ")");
    $("#ct19").text("(" + cSsp + ")");
    $("#ct20").text("(" + cCur + ")");
    $("#ct21").text("(" + cPvt + ")");

}


/* GUNS PER PAGE */
function setPageCount(count) {
    $("#gunsPerPg").val(count);
    $("#curPg").val(1);
    cookGunsOnly();

}


function setAllPagers() {
    setPaging("#pagerDt1");
    setPaging("#pagerDt2");
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
        cookGunsOnly();
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
        cookGunsOnly();
    }
}


function navNext(sender) {
    var id = $(sender).closest("div").prop("id");

    var item = $("#" + id + " a.active").index();

    //var gpp = $("#gunsPerPg").val();
    //var trc = $("#ttlRowCt").val();

    var trc = parseInt($("#ttlRowCt").val());
    var gpp = parseInt($("#gunsPerPg").val());
    var ttlPages = Math.ceil(trc / gpp);

    if (item === ttlPages) { return; }
    else {

        item++;
        $("#curPg").val(item);
        cookGunsOnly();
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

function baseFilterParams() {

    var r1 = $("#c1").prop("checked") ? "true" : "false";
    var r2 = $("#c2").prop("checked") ? "true" : "false";
    var r3 = $("#c3").prop("checked") ? "true" : "false";
    var r4 = $("#c4").prop("checked") ? "true" : "false";
    var r5 = $("#c5").prop("checked") ? "true" : "false";
    var r6 = $("#c6").prop("checked") ? "true" : "false";
    var r7 = $("#c7").prop("checked") ? "true" : "false";
    var r8 = $("#c8").prop("checked") ? "true" : "false";
    var r9 = $("#c9").prop("checked") ? "true" : "false";
    var r10 = $("#c10").prop("checked") ? "true" : "false";
    var r11 = $("#c11").prop("checked") ? "true" : "false";
    var r12 = $("#c12").prop("checked") ? "true" : "false";

    var fileData = new FormData();
    fileData.append("PageUrl", window.location.href);
    fileData.append("IsCaAwRest", r1);
    fileData.append("IsHidden", r2);
    fileData.append("IsCurModel", r3);
    fileData.append("MissOrAll", r4);
    fileData.append("IsOnDataFeed", r5);
    fileData.append("IsCaLegal", r6);
    fileData.append("IsCaRoster", r7);
    fileData.append("IsCaSaRev", r8);
    fileData.append("IsCaSsPst", r9);
    fileData.append("IsCaCurRel", r10);
    fileData.append("IsCaPpt", r11);
    fileData.append("IsLeo", r12);

    return fileData;
}


function resetMenus() {

    var fd = baseFilterParams();
 
    $.ajax({
        cache: false,
        url: "/DataAdmin/SetAllMenus",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindMenuManuf(data);
            bindMenuGunTypes(data);
            bindMenuCaliber(data);
            bindMenuAction(data);
            bindMenuDaysPast(data);

        },
        complete: function () {
            $("#mid").val("0");
            $("#gtp").val("0");
            $("#cid").val("0");
            $("#aid").val("0");
            $("#dbk").val("0");
            $("#t1").val("");
            $(".selectpicker").selectpicker("refresh");
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setMenuMfg(v) {
    $("#mid").val(v);
    var fd = baseFilterParams();
    fd.append("MfgId", v);
 
    $.ajax({
        cache: false,
        url: "/DataAdmin/SetMenuMfg",
        type: "POST",
        contentType: false,  
        processData: false, 
        data: fd,
        success: function (data) {
            bindMenuGunTypes(data);
            bindMenuCaliber(data);
            bindMenuAction(data);
            bindMenuDaysPast(data);

        },
        complete: function () {
            $("#gtp").val("0");
            $("#cid").val("0");
            $("#aid").val("0");
            $("#dbk").val("0");
            $("#t1").val("");
            $(".selectpicker").selectpicker("refresh");
            cookGunsOnly();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setMenuGunType(v) {
    $("#gtp").val(v);
    var mfgId = $("#mid").val();
    var fd = baseFilterParams();
    fd.append("MfgId", mfgId);
    fd.append("GunTypeId", v);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetGunType",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindMenuCaliber(data);
            bindMenuAction(data);
            bindMenuDaysPast(data);
        },
        complete: function () {
            $("#cid").val("0");
            $("#aid").val("0");
            $("#dbk").val("0");
            $("#t1").val("");
            $(".selectpicker").selectpicker("refresh");
            cookGunsOnly();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setMenuCaliber(v) {
    $("#cid").val(v);
    var mfgId = $("#mid").val();
    var gtpId = $("#gtp").val();
    var fd = baseFilterParams();
    fd.append("MfgId", mfgId);
    fd.append("CaliberId", v);
    fd.append("GunTypeId", gtpId);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetCaliber",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindMenuAction(data);
            bindMenuDaysPast(data);
        },
        complete: function () {
            $("#aid").val("0");
            $("#dbk").val("0");
            $("#t1").val("");
            $(".selectpicker").selectpicker("refresh");
            cookGunsOnly();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setMenuAction(v) {
    $("#aid").val(v);
    var calId = $("#cid").val();
    var mfgId = $("#mid").val();
    var gtpId = $("#gtp").val();
    var fd = baseFilterParams();
    fd.append("MfgId", mfgId);
    fd.append("CaliberId", calId);
    fd.append("GunTypeId", gtpId);
    fd.append("ActionId", v);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetAction",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindMenuDaysPast(data);
        },
        complete: function () {
            $("#dbk").val("0");
            $("#t1").val("");
            $(".selectpicker").selectpicker("refresh");
            cookGunsOnly();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setMenuDaysBack(v) {
    $("#dbk").val(v);
    var calId = $("#cid").val();
    var mfgId = $("#mid").val();
    var gtpId = $("#gtp").val();
    var atnId = $("#aid").val();
    var fd = baseFilterParams();
    fd.append("MfgId", mfgId);
    fd.append("CaliberId", calId);
    fd.append("GunTypeId", gtpId);
    fd.append("ActionId", atnId);
    fd.append("DaysBack", v);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetDaysBack",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
        },
        complete: function () {
            $("#t1").val("");
            $(".selectpicker").selectpicker("refresh");
            cookGunsOnly();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
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


function bindMenuManuf(data) {

    var gt = $("#s2");

    gt.find('option').remove().end().append("<option value='0'>- ALL MANUFACTURERS -</option>").val("0");
    gt.selectpicker("refresh");

    $.each(data.Manuf, function (i, item) {
        alert(item.MenuText);
        gt.append("<option value=" + item.IntValue + " data-subtext=(" + item.ItemCount + ")>" + item.MenuText + "</option>");
        gt.selectpicker("refresh");
    });
}


function bindMenuGunTypes(data) {

    var gt = $("#s3");

    gt.find('option').remove().end().append("<option value='0'>- ALL GUN TYPES -</option>").val("0");
    gt.selectpicker("refresh");

    $.each(data.GunType, function (i, item) {
        gt.append("<option value=" + item.IntValue + " data-subtext=(" + item.ItemCount + ")>" + item.MenuText + "</option>");
        gt.selectpicker("refresh");
    });
}


function bindMenuCaliber(data) {

    var cm = $("#s4");
    cm.find('option').remove().end().append("<option value='0'>- ALL CALIBERS -</option>").val("0");
    cm.selectpicker("refresh");

    $.each(data.Caliber, function (i, item) {
        cm.append("<option value=" + item.IntValue + " data-subtext=(" + item.ItemCount + ")>" + item.MenuText + "</option>");
        cm.selectpicker("refresh");
    });
}

function bindMenuAction(data) {

    var at = $("#s5");
    at.find('option').remove().end().append("<option value='0'>- ALL ACTIONS -</option>").val("0");
    at.selectpicker("refresh");

    $.each(data.Action, function (i, item) {
        at.append("<option value=" + item.IntValue + " data-subtext=(" + item.ItemCount + ")>" + item.MenuText + "</option>");
        at.selectpicker("refresh");
    });
}

function bindMenuDaysPast(data) {

    var dp = $("#s6");
    dp.find('option').remove().end().append("<option value='0'>- ALL DATES -</option>").val("0");
    dp.selectpicker("refresh");

    $.each(data.DaysPast, function (i, item) {
        dp.append("<option value=" + item.IntValue + " data-subtext=(" + item.ItemCount + ")>" + item.MenuText + "</option>");
        dp.selectpicker("refresh");
    });
}