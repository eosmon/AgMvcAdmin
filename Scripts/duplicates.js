//jQuery.fn.swap = function (b) {
//    b = jQuery(b)[0];
//    var a = this[0];
//    var t = a.parentNode.insertBefore(document.createTextNode(''), a);
//    b.parentNode.insertBefore(a, b);
//    t.parentNode.insertBefore(b, t);
//    t.parentNode.removeChild(t);
//    return this;
//};


//$(".dragdrop").draggable({ revert: true, helper: "clone" });

//$(".dragdrop").droppable({
//    accept: ".dragdrop",
//    activeClass: "dragdrop-hover",
//    hoverClass: "dragdrop-active",
//    drop: function (event, ui) {

//        var draggable = ui.draggable, droppable = $(this),
//            dragPos = draggable.position(), dropPos = droppable.position();

//        draggable.css({
//            left: dropPos.left + 'px',
//            top: dropPos.top + 'px'
//        });

//        droppable.css({
//            left: dragPos.left + 'px',
//            top: dragPos.top + 'px'
//        });
//        draggable.swap(droppable);
//        droppable.hide();
//        //AJAX Update SQL here
//    }
//});



function setFilter(v) {

    setMenusAll(v);

    $("#s2").prop('selectedIndex', 0);
    $("#s3").prop('selectedIndex', 0);
    $("#s4").prop('selectedIndex', 0);
    $("#mid").val("0");
    $("#gtp").val("0");
    $("#cid").val("0");
}


function ShowRows() {
    getData();
}


function getData() {

    loadAjax();

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
    var filId = /^\d+$/.test($("#fid").val()) ? $("#fid").val() : 1;
    var txtSrch = $("#TxtSch").val();


    var fd = new FormData();
    fd.append("MfgId", mfgId);
    fd.append("GtpId", gtpId);
    fd.append("CalId", calId);
    fd.append("FilId", filId);
    fd.append("GunsPerPg", gunsPerPg);
    fd.append("StartRow", startRow);
    fd.append("TxtSch", txtSrch);

    return $.ajax({
        cache: false,
        url: "/DataAdmin/GetDuplicates",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            cookRows(data.Count, data.Guns);
        },
        complete: function () {
            var c = $("#ttlRowCt").val();
            c = numberWithCommas(c);
            $(".gun-count").text(c + ' Guns Found');
            $(".selectpicker").selectpicker("refresh");

            if (c === 0) {

            } else {
                $("#divPage").show();
                $("#noData").hide();
                setAllPagers();
                showPgRange();
            }

            closeModal();
        },
        error: function (ts) {
            alert(ts.responseText);
        }
    });
}

function noneFound() {
    $("#noData").show();
    $("#divPage").hide();
    $("#addRowHeader").hide();
    var m = "No guns were found with your search parameters or with the keyword(s) selected: \"" + t + "\"";

    $("#noData").text(m);
}


function cookRows(dCt, dGuns) {
    $("#addRows").empty();


    if (!$.trim(dGuns)) {
        $("#gunCountHeader").text("0 Guns Found");
        //noneFound();
        $("#ttlRowCt").val(0);
    } else {
        setNavCounts(dCt);
        var trc = dGuns[0].Filters.TotalRowCount;
        $("#ttlRowCt").val(trc);


        $.each(dGuns, function (i, item) {

            var gid = item.Id;
            var mstId = item.MasterId;
            var imgMs = item.ImageName.length === 0 ? true : false;
            var imgTx = item.ImageName;
            var mfgTx = item.ManufName;
            var dscTx = item.Description;
            var actTx = item.ActionType;
            var calTx = item.CaliberTitle;
            var capId = item.CapacityInt;
            var mdlTx = item.ModelName;
            var finTx = item.FinishName;
            var brlDm = item.BarrelDec.toFixed(3);
            var mpnTx = item.MfgPartNumber;
            var upcTx = item.UpcCode;

            var ssi = item.Filters.IsSsi ? "X" : "";
            var wss = item.Filters.IsWss ? "X" : "";
            var lip = item.Filters.IsLip ? "X" : "";
            var dav = item.Filters.IsDav ? "X" : "";
            var rsr = item.Filters.IsRsr ? "X" : "";
            var bhc = item.Filters.IsBhc ? "X" : "";
            var grn = item.Filters.IsGrn ? "X" : "";
            var zan = item.Filters.IsZan ? "X" : "";
            var mge = item.Filters.IsMge ? "X" : "";


            var block = "<div class='dragdrop' data-id='" + mstId + "' style='display: inline-grid; grid-template-columns: 42px 100px 208px 100px 100px 90px 27px 27px 27px 27px 27px 27px 27px 27px 27px'>";
            block += "<div style='vertical-align:middle; text-align:center'><div style='padding-top:3px;padding-bottom:3px'><a class='dupEdit-Link' href='#' id='edit" + mstId + "'>" + mstId + "</a></div><div style='padding-bottom:3px; padding-top:4px'><input type='text' id='gun" + mstId + "' value='" + mstId + "' style='width:40px; color:black; text-align:center'/></div><div><a class='dupEdit-Link' href='#' id='upd" + mstId + "' onclick='replaceItem(this, " + mstId + ") '>Update</a></div></div>";
            block += "<div style='border-left: solid 1px black'><img src='" + imgTx + "' alt='' style='width:97px;height:auto'></div>";
            block += "<div style='border-left: solid 1px black; vertical-align:top; padding-left:4px'><div><b>" + mfgTx + "</b><div><b>" + mdlTx + "</b></div></div><div>" + dscTx + "</div><div>" + actTx + "</div></div>";
            block += "<div style='border-left: solid 1px black; vertical-align:top; padding-left:4px'><div>" + calTx + "</div><div>" + capId + "-RD</div><div>" + finTx + " Finish</div><div>" + brlDm + "\" Barrel</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + mpnTx + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + upcTx + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + ssi + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + wss + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + lip + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + dav + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + rsr + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + bhc + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + grn + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + zan + "</div></div>";
            block += "<div class='dup-lBrd'><div class='dup-itemAlign'>" + mge + "</div></div>";
            block += "</div>";
            $('#addRows').append(block);

        });

    }
}

/* GUNS PER PAGE */
function setPageCount(count) {
    $("#gunsPerPg").val(count);
    $("#curPg").val(1);
    ShowRows();
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
        ShowRows();
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
        ShowRows();
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
        ShowRows();
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


function replaceItem(ev, ri) {

    var njq = $("#gun" + ri);
    var nid = $(njq).val();

    if (parseInt(ri) === parseInt(nid)) {
        Lobibox.alert('error',
            {
                title: "No Updates Made",
                msg: "Original and replacement ID cannot be the same value",
                color: '#000000'
            });
        return;
    }

    Lobibox.confirm({
        title: "Replace Gun <b>" + ri + "</b> ?",
        msg: "You are moving gun <b>" + ri + "</b> to record <b>" + nid + "</b>. This action cannot be undone. Do you wish to continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {

                var fileData = new FormData();
                fileData.append("OldGunId", ri);
                fileData.append("NewMstId", nid);

                $.ajax({
                    cache: false,
                    url: "/DataAdmin/ReplaceDuplicate",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fileData,
                    success: function () {

                    },
                    error: function (err, result) {
                        alert('there was an error');
                    },
                    complete: function () {
                        location.reload();
                    }
                });
            }
        }
    });
}

function setMenusAll(v) {

    $("#fid").val(v);

    var fd = new FormData();
    fd.append("FilId", v);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetDupMenus",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindGenericMenu(data.Manuf, "s2", "MANUFACTURERS");
            bindGenericMenu(data.GunType, "s3", "GUN TYPES");
            bindGenericMenu(data.Caliber, "s4", "CALIBERS");

        },
        complete: function () {
            $("#mid").val("0");
            $("#gtp").val("0");
            $("#cid").val("0");
            $("#t1").val("");
            $("#TxtSch").val("");
            $(".selectpicker").selectpicker("refresh");
            ShowRows();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}




function setMenuMfg(v) {
 
    $("#mid").val(v);
    var fid = $("#fid").val();

    var fd = new FormData();
    fd.append("FilId", fid);    
    fd.append("MfgId", v);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetDupMenuMfg",
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
            $("#t1").val("");
            $(".selectpicker").selectpicker("refresh");
            ShowRows();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setMenuGunType(v) {
    $("#gtp").val(v);
    var fid = $("#fid").val();
    var mfgId = $("#mid").val();
 
    var fd = new FormData();
    fd.append("FilId", fid);
    fd.append("MfgId", mfgId);
    fd.append("GtpId", v);

    $.ajax({
        cache: false,
        url: "/DataAdmin/SetDupMenuGtp",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            bindGenericMenu(data.Caliber, "s4", "CALIBERS");
        },
        complete: function () {
            $("#cid").val("0");
            $("#t1").val("");
            $(".selectpicker").selectpicker("refresh");
            ShowRows();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setMenuCaliber(v) {
    $("#cid").val(v);
    ShowRows();
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

function clearTxt() {
    $("#t1").val("");
}


function searchTxt() {

    var t = $("#t1").val();
    t = trimJunk(t);
    $("#TxtSch").val(t);
    ShowRows();
}