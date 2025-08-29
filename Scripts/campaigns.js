
function empCmpRows() {
    $('div[id^="dvBnrCur"]').empty();
    $('div[id^="dvBnrAvl"]').empty();
    $('div[id^="rowAddBnr"]').hide();
}

function loadAll() {
    
    $.ajax({
        cache: false,
        url: "/Content/AllCmpBnr",
        type: "POST",
        contentType: false,
        processData: false,
        success: function (data) {

            $('#dvCpnRows').empty();
            $('#dvBnrRows').empty();

            var rc = "#FFF7E5";

            if (data.CP != null) {

                $("#dvCpn").show();

                $.each(data.CP, function (i, item) {

                        var id = item.CampaignId;
                        var cNam = item.CampaignName;
                        var tOut = item.ShowDelay;

                        var block = "<div style='padding:2px; border-bottom:solid 1px black; background-color: " + rc + "'>";
                        block += "<div style='width: 5%; display: inline-block; text-align: center'><span class='uploadTxt' href='#' onclick='nixCampaign(this, " + id + ", true)'>delete</span></div>";
                        block += "<div style='width: 5%; display: inline-block; text-align: center; color:#000000'><span class='idLnk-txt' onclick='showAddedBanners(false, " + id + ")'>" + id + "</span></div>";
                        block += "<div style='width: 59%; display: inline-block'><input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='cmpName" + id + "' id='cmpName" + id + "' value='" + cNam + "'></div>";
                        block += "<div style='width: 25%; display: inline-block'>";
                        block += "<select class='ag-control input-sm txt-ct-w1' id='sTmo" + id + "' name='sTmo" + id + "'>";
                        block += "<option value='0'>-SELECT-</option>";
                        for (var j = 0; j < 11; j++) {
                            var b = j + '000';
                            var sel = '';
                            if (tOut === parseInt(b)) {
                                sel = 'selected';
                            }
                            block += "<option value='"+b+"' " + sel + ">" + j + "</option>";
                        }
                        block += "</select></div>";
                        block += "<div style='width: 6%; display: inline-block; text-align: center'><button type='button' class='btn btn-xs btn-blue' style='width:50px !important' onclick='updateCampaign(" + id + ")'>Update</button></div>";
                        block += "</div>";

                        block += "<div id='rowAddBnr" + id + "' style='width: 100%; display:none; background:blue; margin-bottom:-4px'>";
                        block += "<div id='dvBnrCur" + id + "' style='width: 50%; display: inline-block; overflow-y: scroll; height: 300px'></div>";
                        block += "<div id='dvBnrAvl" + id + "' style='width: 50%; display: inline-block; float: right; overflow-y: scroll; height: 300px'></div>";
                        block += "</div>";



                        $('#dvCpnRows').append(block);

                        rc = rc === "#FFF7E5" ? "bisque" : "#FFF7E5";
                        
                    });


            }
            else
            {
                $("#dvCpn").hide();
                $("#dvNoCpn").show();
            }


            if (data.BN != null) {

                $("#dvNoBnr").hide();
                $("#dvBnr").show();

                $.each(data.BN, function (i, item) {

                    var bid = item.BannerId;
                    var dsc = item.ItemDesc;
                    var nav = item.NavToUrl;
                    var img = item.ImageUrl;
                    var nWn = item.NewWindow;

                    var fnx = img.lastIndexOf("/") + 1;
                    var igo = img.substr(fnx);
 

                    var selY = '';
                    var selN = '';

                    if (nWn) { selY = 'selected';  } else { selN = 'selected'; }


                    var block = "<div style='padding:2px; border-bottom:solid 1px black; background-color: " + rc + "' data-id='" + bid + "'>";
                    block += "<div style='width: 55px; display: inline-block; text-align: center'><span class='uploadTxt' href='#' onclick=\"nixBanner(this, " + bid + ", true, '" + igo + "')\">delete</span></div>";
                    block += "<div style='display: inline-block; padding-top: 2px; width:155px'>";
                    block += "<div class='fileinput fileinput-new img-loop' data-provides='fileinput' style='display: inline-block; margin-bottom:0 !important'>";
                    block += "<div id='dvBnr_" + bid + "' class='fileinput-preview thumbnail img-header-frame' data-trigger='fileinput'><img id='img" + bid + "' src='" + img + "' /></div>";
                    block += "<div class='checkForm' style='text-align: center'><span class='btn-file'><input type='file' name='agimg' id='ImgBnr_" + bid + "' onchange='showImgCancel(this)'></span></div></div></div>";
                    block += "<div style='display: inline-block; vertical-align: middle;  width:calc(100% - 310px) !important'>";
                    block += "<div class='top-buffer'>";
                    block += "<label for='txBrDsc" + bid + "' class='label-bnr lbl-black'>Description: </label><input type='text' class='ag-control input-sm ctrl-brn-width pad-lft' name='model' id='txBrDsc" + bid + "' value='" + dsc + "'>";
                    block += "</div>";
                    block += "<div class='top-buffer'>";
                    block += "<label for='txBrUrl" + bid + "' class='label-bnr lbl-black'>Nav URL: </label><input type='text' class='ag-control input-sm ctrl-brn-width pad-lft' name='model' id='txBrUrl" + bid + "' value='" + nav + "'>";
                    block += "</div>";

                    block += "<div class='top-buffer'>";
                    block += "<label for='sNwWin" + bid + "' class='label-bnr lbl-black'>New Window: </label><select class='ag-control input-sm ctrl-brn-width pad-lft' id='sNwWin" + bid + "' name='sNwWin" + bid + "'>";
                    block += "<option value='0' " + selN + ">NO</option>";
                    block += "<option value='1' " + selY + ">YES</option>";
                    block += "</select></div>";

                    block += "</div>";
                    block += "<div style='display: inline-block; vertical-align: bottom; width:100px; text-align:center; padding-bottom:10px'>";
                    block += "<div class='feed-uplPicTxt' style='padding-bottom: 5px; display:none' id='dvCanImg'>";
                    block += "<a href='#' class='fileinput-exists feed-uplPicTxt img-loop uploadTxt' title='' data-image='dvBnr_" + bid + "' onclick='nixImg(this)'>Cancel Img</a>";
                    block += "</div>";
                    block += "<div id='btnWrpAdd'><div><button type='button' class='btn btn-xs btn-green2' onclick='updateBanner(" + bid + ")' style='width:50px !important'>Update</button></div></div>";
                    block += "</div>";
                    block += "</div>";
                    $('#dvBnrRows').append(block);

                    rc = rc === "#FFF7E5" ? "bisque" : "#FFF7E5";
                });


            }
            else
            {
                $("#dvBnr").hide();
                $("#dvNoBnr").show();
            }


        },
        complete: function () {
            return false;

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function showAddedBanners(ko, id) {

    var rAb = $("#rowAddBnr" + id);

    if (!ko) {
        var vis = $(rAb).css("display");
        if (vis === "block") { $(rAb).hide(); return; }
    }

    empCmpRows();
    $('div[id^="rowAddBnr"]').hide();

    var fd = new FormData();
    fd.append("Id", id);

    $.ajax({
        cache: false,
        url: "/Content/CmpById",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            $(rAb).scrollTop($(rAb)[0].scrollHeight);

            var rCur = $("#dvBnrCur" + id);
            var rAvl = $("#dvBnrAvl" + id);

            var isAvl = data.AA;
            var isCur = data.AC;

            if (!isAvl) {
                var dn = "<div style='padding:5px; text-align:center'>All existing banners have been added</div>";
                $(rAvl).append(dn);
            } else {

                var rc1 = "dodgerblue";

                $.each(data.CA, function (i, item) {
                    var block1 = "<div style='display: inline-table; background:"+rc1+"; width: 100%; height: 40px; border-bottom:solid 1px black'>";
                    block1 += "<div class='cmpGrid-rowC' style='width:10%; background:black'><span class='cmpLink' onclick='addBnrToCpn(" + id + ", " + item.BannerId + ")'>add</span></div>";
                    block1 += "<div class='cmpGrid-rowC' style='width:10%'>" + item.BannerId + "</div>";
                    block1 += "<div class='cmpGrid-rowC' style='width:30%'><img src='" + item.ImageUrl + "' alt='' style='width:100px;height:auto'></div>";
                    block1 += "<div class='cmpGrid-row' style='width:50%'>" + item.ItemDesc + "</div>";
                    block1 += "</div>";
                    $(rAvl).append(block1);

                    rc1 = rc1 === "dodgerblue" ? "lightskyblue" : "dodgerblue";

                });
            }


            if (!isCur) {
                var cn = "<div style='padding:5px; text-align:center'>No banners have been assigned to this campaign</div>";
                $(rCur).append(cn);
            } else {

                var rc2 = "lightskyblue";

                $.each(data.CC, function (i, item) {
                    var block2 = "<div style='display: inline-table; background:" + rc2 + "; width: 100%; height: 40px; border-bottom:solid 1px black'>";
                    block2 += "<div class='cmpGrid-rowC' style='width:11%; background:black'><span class='cmpLink' style='padding-right:2px' onclick='nixBnrFromCpn(" + id + ", " + item.Id + ")'>drop</span></div>";
                    block2 += "<div class='cmpGrid-rowC' style='width:9%; padding-left:3px'>" + item.BannerId + "</div>";
                    block2 += "<div class='cmpGrid-rowC' style='width:30%'><img src='" + item.ImageUrl + "' alt='' style='width:100px;height:auto'></div>";
                    block2 += "<div class='cmpGrid-row' style='width:38%'>" + item.ItemDesc + "</div>";
                    block2 += "<div class='cmpGrid-row' style='width:12%; padding-right:3px'>";
                    block2 += "<select class='ag-control input-sm txt-ct-w1' id='sSto" + item.Id + "' name='sSto" + item.Id + "' onchange='sortBnrCpn(" + item.Id + ", this)'>";
                    block2 += "<option value='0'>-SELECT-</option>";
                    for (var j = 0; j < data.CC.length+1; j++) {
                        var sel = '';
                        if (item.SortId === j) {
                            sel = 'selected';
                        }
                        block2 += "<option value='" + j + "' " + sel + ">" + j + "</option>";
                    }
                    block2 += "</select></div>";
                    block2 += "</div>";
                    $(rCur).append(block2);

                    rc2 = rc2 === "lightskyblue" ? "dodgerblue" : "lightskyblue";

                });
            }


        },
        complete: function () {
            $(rAb).show();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });



}


function addBnrToCpn(cmpId, bnrId) {

    var fd = new FormData();
    fd.append("CpId", cmpId);
    fd.append("BrId", bnrId);

    $.ajax({
        cache: false,
        url: "/Content/AddCmpBnr",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

        },
        complete: function () {
            showAddedBanners(true, cmpId);
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function nixBnrFromCpn(cmpId, id) {

    var fd = new FormData();
    fd.append("Id", id);


    $.ajax({
        cache: false,
        url: "/Content/NixCmpBnr",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

        },
        complete: function () {
            showAddedBanners(true, cmpId);
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function sortBnrCpn(id, ev) {

    var val = $("option:selected", ev).val();

    var fd = new FormData();
    fd.append("Id", id);
    fd.append("StId", val);

    $.ajax({
        cache: false,
        url: "/Content/SortCmpBnr",
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




function addCampaignRow() {

    empCmpRows();

    var sz = $("#dvCpnRows > div").length;
    var lr = "";
    var id = "";
    var bgc = "";

    if (sz > 0) {
        lr = $("#dvCpnRows > div").last().css("background-color");
        id = $("#dvCpnRows > div").last().attr("data-id");
        bgc = lr === 'rgb(255, 247, 229)' ? 'bisque' : '#FFF7E5';
        id = parseInt(id) + 1;
    } else {
        $("#dvNoCpn").hide();
        id = 0;
        bgc = "bisque";
    }

    var block = "<div style='padding:2px; border-bottom:solid 1px black; background-color: " + bgc + "' data-id='" + id + "'>";
    block += "<div style='width: 5%; display: inline-block; text-align: center'><span class='uploadTxt' href='#' onclick='nixCampaign(this, " + id + ", false)'>delete</span></div>";
    block += "<div style='width: 5%; display: inline-block; text-align: center; color:#000000'>0</div>";
    block += "<div style='width: 59%; display: inline-block'><input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='cmpName" + id + "' id='cmpName" + id + "'></div>";
    block += "<div style='width: 25%; display: inline-block'>";
    block += "<select class='ag-control input-sm txt-ct-w1' id='sTmo" + id + "' name='sTmo" + id + "'>";
    block += "<option value='0'>-SELECT-</option>";
    for (var j = 0; j < 11; j++) {
        var b = j + '000';
        block += "<option value='" + b + "'>" + j + "</option>";
    }   
    block += "</select></div>";
    block += "<div style='width: 6%; display: inline-block; text-align: center'><button type='button' class='btn btn-xs btn-green' style='width:50px !important' onclick='addCampaign(" + id + ")'>Add</button></div>";
    block += "</div>";
    $('#dvCpnRows').append(block);
    $("#dvCpn").show();


    $("#dvCpnRows").scrollTop($("#dvCpnRows")[0].scrollHeight);
}


function addCampaign(id) {

    var cNm = $("#cmpName" + id).val();
    var bSl = $("#sTmo" + id);
    var cVl = $("option:selected", bSl).val();

    var n = cNm.length;
    var v = cVl.length;

    if (n === 0 || v === 0) {
        Lobibox.alert('error',
            {
                title: 'Error: Campign ' + parseInt(id + 1) + ' Missing Data',
                msg: 'Please verify a campaign name and timeout value are selected'
            });
    }
    else {
        var fd = new FormData();
        fd.append("CpTx", cNm);
        fd.append("CpDy", cVl);

        $.ajax({
            cache: false,
            url: "/Content/AddCmp",
            type: "POST",
            contentType: false,
            processData: false,
            data: fd,
            success: function (data) {
                Lobibox.alert('success',
                    {
                        title: 'Complete',
                        msg: 'Campaign successfully added'
                    });
            },
            complete: function () {
                loadAll();

            },
            error: function (err, data) {
                alert("Status : " + data.responseText);
            }
        });
    }
}


function updateCampaign(id) {

    var cNm = $("#cmpName" + id).val();
    var bSl = $("#sTmo" + id);
    var cVl = $("option:selected", bSl).val();

    var fd = new FormData();
    fd.append("Id", id);
    fd.append("CpTx", cNm);
    fd.append("CpDy", cVl);

    $.ajax({
        cache: false,
        url: "/Content/UpdCmp",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            Lobibox.alert('success',
                {
                    title: 'Campaign Updated',
                    msg: 'Campaign has been successfully updated'
                });

        },
        complete: function () {

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function nixCampaign(ev, id, db) {

    if (db) {

        Lobibox.confirm({
            title: "Delete Campaign?",
            msg: "You are about to permanently delete this campaign. This action cannot be undone - continue?",
            modal: true,
            callback: function(lobibox, type) {
                if (type === 'no') {
                    return;
                } else {
                    empCmpRows();
                    var fd = new FormData();
                    fd.append("Id", id);

                    $.ajax({
                        cache: false,
                        url: "/Content/NixCpn",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fd,
                        success: function(data) {
                            Lobibox.alert('success',
                                {
                                    title: 'Campaign Deleted',
                                    msg: 'Campaign has been successfully removed'
                                });

                        },
                        complete: function() {
                            loadAll();
                        },
                        error: function(err, data) {
                            alert("Status : " + data.responseText);
                        }
                    });
                }
            }
        });
    }
    else
    {
        empCmpRows();
        $(ev).parent().parent().hide();        
    }
}


function addBannerRow() {

    var sz = $("#dvBnrRows > div").length;
    var lr = "";
    var id = "";
    var bgc = "";



    if (sz > 0) {
        lr = $("#dvBnrRows > div").last().css("background-color");
        id = $("#dvBnrRows > div").last().attr("data-id");
        bgc = lr === 'rgb(255, 247, 229)' ? 'bisque' : '#FFF7E5';
        id = parseInt(id) + 1;
    } else {
        $("#dvNoBnr").hide();
        id = 0;
        bgc = "bisque";
    }


    var block = "<div style='padding:2px; border-bottom:solid 1px black; background-color: " + bgc + "' data-id='" + id + "'>";
    block += "<div style='width: 55px; display: inline-block; text-align: center'><span class='uploadTxt' href='#' onclick='nixBanner(this, " + id + ", false, '')'>delete</span></div>";
    block += "<div style='display: inline-block; padding-top: 2px; width:155px'>";
    block += "<div class='fileinput fileinput-new img-loop' data-provides='fileinput' style='display: inline-block; margin-bottom:0 !important'>";
    block += "<div id='dvBnr_" + id + "' class='fileinput-preview thumbnail img-header-frame' data-trigger='fileinput'></div>";
    block += "<div class='checkForm' style='text-align: center'><span class='btn-file'><input type='file' name='agimg' id='ImgBnr_" + id + "' onchange='showImgCancel(this)'></span></div></div></div>";
    block += "<div style='display: inline-block; vertical-align: middle;  width:calc(100% - 310px) !important'>";
    block += "<div class='top-buffer'>";
    block += "<label for='txBrDsc" + id + "' class='label-bnr lbl-black'>Description: </label><input type='text' class='ag-control input-sm ctrl-brn-width pad-lft' name='model' id='txBrDsc" + id + "'>";
    block += "</div>";
    block += "<div class='top-buffer'>";
    block += "<label for='txBrUrl" + id + "' class='label-bnr lbl-black'>Nav URL: </label><input type='text' class='ag-control input-sm ctrl-brn-width pad-lft' name='model' id='txBrUrl" + id + "'>";
    block += "</div>";

    block += "<div class='top-buffer'>";
    block += "<label for='sNwWin" + id + "' class='label-bnr lbl-black'>New Window: </label><select class='ag-control input-sm ctrl-brn-width pad-lft' id='sNwWin" + id + "' name='sNwWin" + id + "'>";
    block += "<option value='0'>NO</option>";
    block += "<option value='1'>YES</option>";
    block += "</select></div>";

    block += "</div>";
    block += "<div style='display: inline-block; vertical-align: bottom; width:100px; text-align:center; padding-bottom:10px'>";
    block += "<div class='feed-uplPicTxt' style='padding-bottom: 5px; display:none' id='dvCanImg'>";
    block += "<a href='#' class='fileinput-exists feed-uplPicTxt img-loop uploadTxt' title='' data-image='dvBnr_" + id + "' onclick='nixImg(this)'>Cancel Img</a>";
    block += "</div>";
    block += "<div id='btnWrpAdd'><div><button type='button' class='btn btn-xs btn-green2' onclick='addBnr(" + id + ")' style='width:50px !important'>Add</button></div></div>";
    block += "</div>";
    block += "</div>";
    $('#dvBnrRows').append(block);
    $("#dvBnr").show();

    $("#dvBnrRows").scrollTop($("#dvBnrRows")[0].scrollHeight);
}


function addBnr(id) {

    var bDsc = $("#txBrDsc" + id).val();
    var bUrl = $("#txBrUrl" + id).val();
    var bImg = $("#dvBnr_" + id).find('img');
    var bSrc = 0;

    var bNw = $("#sNwWin" + id);
    var bVl = $("option:selected", bNw).val();

    if (bImg.length > 0) {
        bSrc = $(bImg).attr("src").length;
    }

    var bd = bDsc.length;
    var bu = bUrl.length;

    if (bd === 0 || bu === 0 || bSrc === 0) {
        Lobibox.alert('error',
            {
                title: 'Error: Banner ' + parseInt(id + 1) + ' Missing Data',
                msg: 'Please verify the banner image is uploaded and all text fields contain values'
            });
    } else {

        var src = "ImgBnr_" + id;

        var fd = new FormData();
        fd.append("BrId", id);
        fd.append("BrDs", bDsc);
        fd.append("BrNu", bUrl);
        fd.append("BrNw", bVl);


        var f = document.getElementById(src).files;
        if (f.length > 0) {
            fd.append('Files', f[0]);
        }

        $.ajax({
            cache: false,
            url: "/Content/AddBnr",
            type: "POST",
            contentType: false,
            processData: false,
            data: fd,
            success: function (data) {
                loadAll();
                Lobibox.alert('success',
                    {
                        title: 'Complete',
                        msg: 'Banner successfully added'
                    });
            },
            complete: function () {
                return false;

            },
            error: function (err, data) {
                alert("Status : " + data.responseText);
            }
        });
    }
}



function nixBanner(ev, id, db, img) {

    if (db) {

        Lobibox.confirm({
            title: "Delete Banner?",
            msg: "You are about to permanently delete this banner. This action cannot be undone - continue?",
            modal: true,
            callback: function(lobibox, type) {
                if (type === 'no') {
                    return;
                } else {
                    var fd = new FormData();
                    fd.append("Id", id);
                    fd.append("Img", img);

                    $.ajax({
                        cache: false,
                        url: "/Content/NixBnr",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fd,
                        success: function(data) {
                            Lobibox.alert('success',
                                {
                                    title: 'Banner Deleted',
                                    msg: 'Banner has been successfully removed'
                                });

                        },
                        complete: function() {
                            loadAll();
                        },
                        error: function(err, data) {
                            alert("Status : " + data.responseText);
                        }
                    });
                }
            }
        });
    }
    else
    {
        $(ev).parent().parent().hide();
    }
}


function updateBanner(id) {

    var bDsc = $("#txBrDsc" + id).val();
    var bUrl = $("#txBrUrl" + id).val();
    var bImg = $("#dvBnr_" + id).find('img');

    var bNw = $("#sNwWin" + id);
    var bVl = $("option:selected", bNw).val();

    //var bSrc = 0;

    //if (bImg.length > 0) {
    //    bSrc = $(bImg).attr("src").length;
    //}

    var fd = new FormData();
    fd.append("Id", id);
    fd.append("BrDs", bDsc);
    fd.append("BrUl", bUrl);
    fd.append("BrNw", bVl);

    if (bImg.length > 0) {
        var src = "ImgBnr_" + id;
        var f = document.getElementById(src).files;
        if (f.length > 0) {
            fd.append('Files', f[0]);
        }
    }

    $.ajax({
        cache: false,
        url: "/Content/UpdBnr",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            Lobibox.alert('success',
                {
                    title: 'Banner Updated',
                    msg: 'Banner has been successfully updated'
                });

        },
        complete: function () {

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


