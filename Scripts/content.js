

function showAddOg() {
    $("#rowAddOg").show();
    $("#rowNewOg").show();
    $("#rowUpt").hide();
}


function cancelOg() {
    $("#rowAddOg").hide();
    $("#rowNewOg").hide();
    $("#rowUpt").show();
}

function setMenus(v) {

    var fd = new FormData();
    fd.append("ContId", v);

    $.ajax({
        cache: false,
        url: "/Content/GetMenuPages",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            $("#s2").empty();
            bindGenericMenu(data, "s2", "PAGE");

        },
        complete: function () {

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function bindGenericMenu(data, ctl, lbl) {
    var gt = $("#" + ctl);

    gt.find('option').remove().end().append("<option value='0'>- SELECT " + lbl + " -</option>").val("0");

    $.each(data, function (i, item) {
        gt.append("<option value=" + item.Value + ">" + item.Text + "</option>");
    });
}


function setContent(v) {

    var fd = new FormData();
    fd.append("PageId", v);

    $.ajax({
        cache: false,
        url: "/Content/GetAllContent",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            $("#dvTips").hide();
            $('#dvCmp').hide();

            $("#dvNoTips").hide();
            $("#dvNoCmp").hide();
            $("#dvNoHome").hide();
            $("#dvHomeContent").hide();

            $("#img_1").empty();
            $('#dvTipRows').empty();
            $('#dvCmpRows').empty();
            $('#dvHomeRows').empty();
            $("#hiUl").val("");


            var cId = data.CT.Id;
            var sId = data.CT.StaticId;
            var dIs = data.CT.HasStatic;
            var pIh = data.CT.HasHeader;
            var dHp = data.CT.IsHomePage;
            var pNm = data.CT.PageName;
            var sTl = data.CT.SeoTitle;
            var sKw = data.CT.SeoKeywords;
            var sOg = data.CT.SeoOgType;
            var sDc = data.CT.SeoDesc;
            var hTl = data.CT.HeaderTitle;
            var hTx = data.CT.HeaderTxt;
            var hIg = data.CT.HeaderImg;
            var sTx = data.CT.StaticTxt;


            $("#t1").val(pNm);
            $("#t2").val(sKw);
            $("#t7").val(sOg);
            $("#t8").val(sTl);
            $("#t3").val(sDc);

            $("#t5").val(hTl);
            $("#t6").val(hTx);

            $("#stId").val(sId);

            if (hIg.length > 0) {
                $("#img_1").append("<img id='imgHdr' src='" + hIg + "'>");
                $("#hiUl").val(hIg);
                $(".fileinput-exists").show();
                $("#selImg").hide();
            }

            if (pIh) {
                $("#dvNoHdr").hide();
                $("#dvHdrAdd").hide();
                $("#dvHasHdr").show();
            } else {
                $("#dvNoHdr").show();
                $("#dvHdrAdd").show();
                $("#dvHasHdr").hide();
            }


            if (dIs) {
                $("#dvStcCnt").show();
                $("#rowBtnUpd").show();
                $("#rowBtnAdd").hide();
                $("#rowAddCnt").hide();
                $("#dvNoCnt").hide();

                CKEDITOR.instances.content.setData(sTx);

            } else {
                $("#dvStcCnt").hide();
                $("#dvNoCnt").show();
                $("#rowAddCnt").show();

                CKEDITOR.instances.content.setData("");
                
            }


            var htt = data.TT[0].HasToolTip;
            var rc = "#FFF7E5";
 
            if (htt) {

                $("#dvTips").show();
                $.each(data.TT,
                    function(i, item) {

                        var desc = item.TipDesc;
                        var txt = item.ToolTipTxt;

                        if (item.Id > 0) {
                            var block = "<div style='padding:2px; border-bottom:solid 1px black; background-color:" + rc + "' data-id='" + item.Id + "'>";
                            block += "<div style='width: 5%; display: inline-block; text-align: center'><span class='uploadTxt' href='#' onclick='deleteToolTip(this, " + item.Id + ")'>delete</span></div>";
                            block += "<div style='width: 5%; display: inline-block; text-align: center; color:#000000'>" + item.Id + "</div>";
                            block += "<div style='width: 15%; display: inline-block'><input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='model' id='tipDesc" + item.Id + "' value='" + desc + "'></div>";
                            block += "<div style='width: 69%; display: inline-block'><input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='model' id='tipTxt" + item.Id + "' value='" + txt + "'></div>";
                            block += "<div style='width: 6%; display: inline-block; text-align: center'><button type='button' class='btn btn-xs btn-blue' style='width:50px !important' onclick='updateToolTip(" + item.Id + ")'>Update</button></div>";
                            block += "</div>";
                            $('#dvTipRows').append(block);

                            rc = rc === "#FFF7E5" ? "bisque" : "#FFF7E5";
                        }
                    });


            } else {
                $("#dvNoTips").show();              
            }

            var l = data.CC.length;

            if (l > 0) {

                rc = "#FFF7E5";

                $("#dvCmp").show();
                $.each(data.CC, function (i, item) {

                    var id = item.Id;
                    var pos = item.PositionId;
                    var bnrCt = item.BannerCount;
                    var delay = item.ShowDelay;
                    var cNm = item.CampaignName;

                    var block = "<div style='border-bottom:solid 1px black; height:40px; background-color: " + rc + "'>";
                    block += "<div style='width: 59px; display: inline-block; text-align: center; margin-top:10px'><span class='uploadTxt' href='#' onclick='nixPgCmp(this, " + id + ")'>delete</span></div>";
                    block += "<div style='width: 80px; display: inline-block; text-align: center; color:#000000; margin:3px'>" + pos + "</div>";
                    block += "<div style='width: calc(100% - 355px); display: inline-block; color:#000000'>" + cNm + "</div>";
                    block += "<div style='width: 80px; display: inline-block; text-align: center; color:#000000; margin:3px'>" + bnrCt + "</div>";
                    block += "<div style='width: 81px; display: inline-block; text-align: center; color:#000000'>" + delay + "</div>";
                    block += "</div>";
                    $('#dvCmpRows').append(block);

                    rc = rc === "#FFF7E5" ? "bisque" : "#FFF7E5";
                });


            } else {
                $("#dvCmp").hide();
                $("#dvNoCmp").show();
            }

            if (dHp) {

                rc = "#FFF7E5";
                var lst1 = data.AO;
                var lst2 = data.SZ;
                var lst3 = data.BC;


                $("#dvHomeContent").show();
                $("#dvHome").show();
                $.each(data.HM, function(i, item) {

                    var hId = item.Id;
                    var hIhd = item.IsHeader;
                    var hPos = item.PositionId;
                    var hOpt = item.OptionId;
                    var hFtr = item.FeatureId;
                    var hPtx = item.PromoText;
                    var hSid = item.FeatureSizeId;
                    var hPcl = item.PromoColorId;
                    var hGrp = item.GroupId;

                    var chk = item.IsPromo ? "checked='checked'" : "";
                    var atv = item.IsActive ? "checked='checked'" : "";

                    var hrc = rc;
                    var nix = "onclick='nixFeatureItem(this, " + hId + ", true)'";
                    var upd = "onclick='updateFeatureItem(" + hId + ", true)'";
                    var gid = "group-id=" + hGrp + "";
                    var xid = "item-id=" + hId + "";
                    var lia = "onclick='routeAd(" + hId + ")'";
                    var lid = "view" + hId + "";
                    var lit = "preview";
                    var bcl = "home-row";
                    var ldp = "block";
                    var ds2 = "block";
                    var ftr = "xFtrId"+hId;

                    if (hIhd) {
                        hrc = "lightblue";
                        nix = "onclick='nixHomeItem(this, " + hId + ", true)'";
                        upd = "onclick='updateHomeConfig(" + hId + ", true)'";
                        gid = "";
                        xid = "row-id=" + hId + "";
                        lia = "onclick='addFtrRow(" + hGrp + ")'";
                        lid = "addRow" + hId + "";
                        lit = "add row";
                        bcl = "home-row-title";
                        ldp = hOpt === 3 ? "block" : "none";
                        ds2 = hOpt === 2 ? "block" : "none";
                        ftr = "FtrId" + hId;
                    }

                    var block = "<div id=rw" + hId + " style='background-color: " + hrc + "' class='" + bcl + "' " + xid + " " + gid + ">";
                    block += "<div class='home-row-item' style='padding-top: 6px; font-weight: bold'><a id='delHm" + hId + "' class='link12Blue' " + nix + ">delete</a></div>"; 
                    block += "<div class='home-row-item' style='padding-right: 3px;'>";
                    if (hIhd) {
                        block += "<select class='ag-control input-sm txt-ct-w1' id='sPosHm" + hId + "' name='posHm" + hId + "'>";
                        for (var a = 1; a < 16; a++) {
                            var s1 = '';
                            if (hPos === a) { s1 = 'selected'; }
                            block += "<option value='" + a + "' " + s1 + ">" + a + "</option>";
                        }
                        block += "</select>";
                    }
                    block += "</div>";
                    block += "<div class='home-row-item'>";
                    if (hIhd) {
                        block += "<select class='ag-control input-sm txt-ct-w1' id='sOpt" + hId + "' name='sOpt" + hId + "' onchange='setDisplay(this.value, " + hId + ")'>";
                        block += "<option value='-1'>-SELECT-</option>";
                        for (var b = 0; b < lst1.length; b++) {
                            var s2 = '';
                            if (hOpt === parseInt(lst1[b].Value)) {
                                s2 = 'selected';
                            }
                            block += "<option value='" + lst1[b].Value + "' " + s2 + ">" + lst1[b].Text + "</option>";
                        }
                        block += "</select>";
                    }
                    block += "</div>";
                    block += "<div class='home-row-item' style='position: relative; top:10px;'>";
                    if (hIhd) {
                            block += "<input type='checkbox' id='ckAtv" + hId + "' " + atv + " />";
                        }
                    block += "</div>";
                    block += "<div class='home-row-item' style='padding-right: 5px;'>";

                    block += "<input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='" + ftr + "' id='" + ftr + "' value='" + hFtr + "' onkeypress='return isNumber(event)' maxlength='6' style='display:" + ds2 + "'>";
                    
                    block += "</div>";
                    block += "<div class='home-row-item'>";

                    if (!hIhd) {
                        block += "<select class='ag-control input-sm txt-ct-w1' id='FtrSz" + hId + "' name='FtrSz" + hId + "'>";
                        block += "<option value='0'>-SELECT-</option>";
                        for (var c = 0; c < lst2.length; c++) {
                            var s3 = '';
                            if (hSid === parseInt(lst2[c].Value)) { s3 = 'selected'; }
                            block += "<option value='" + lst2[c].Value + "' " + s3 + ">" + lst2[c].Text + "</option>";
                        }
                        block += "</select>";
                    }
                    block += "</div>";
                    block += "<div class='home-row-item' style='position: relative; top:10px;'>";
                    if (!hIhd) {
                        block += "<input type='checkbox' id='ckPro" + hId + "' " + chk + " />";
                    }
                    block += "</div>";
                    block += "<div class='home-row-item'>";
                    if (!hIhd) {
                        block += "<select class='ag-control input-sm txt-ct-w1' id='ColId" + hId + "' name='ColId" + hId + "'>";
                        block += "<option value='0'>-SELECT-</option>";
                        for (var y = 0; y < lst3.length; y++) {
                            var s4 = '';
                            if (hPcl === parseInt(lst3[y].Value)) { s4 = 'selected'; }
                            block += "<option value='" + lst3[y].Value + "' " + s4 + ">" + lst3[y].Text + "</option>";
                        }
                        block += "</select>";
                    }
                    block += "</div>";
                    block += "<div class='home-row-item' style='padding-right: 5px;'>";
                    if (!hIhd) {
                        block += "<input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='PrTx" +hId +"' id='PrTx" +hId +"' value='" +hPtx +"'>";
                    }
                    block += "</div>";

                    block += "<div class='home-row-item'><button type='button' class='btn btn-xs btn-blue' style='width:50px !important' " + upd + ">Update</button></div>";
                    block += "<div class='home-row-item' style='padding-top: 6px; font-weight: bold'>";

                    block += "<a id='" + lid + "' class='link12Blue' "+lia+" style='display:"+ldp+"'>" + lit + "</a>";
  
                    block += "</div>";
                    block += "</div>";
                    $('#dvHomeRows').append(block);

                    rc = rc === "#FFF7E5" ? "bisque" : "#FFF7E5";
                });

            } else {
                $("#dvHome").hide();
                $("#dvNoHome").show();
            }


        },
        complete: function () {
            $("#pgId").val(v);
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });

}


function setDisplay(v, id) {

    // Hide All Child Rows
    $('div[group-id=' + id + ']').each(function () {
        $(this).hide();

        switch (v) {
            case "0":
            case "1":
                $(this).hide();
            case "2":
                $(this).hide();
                break;
            case "3":
                $(this).show();
                break;
        }
    });

    var f = $("#FtrId" + id);
    var a = $("#addRow" + id);
    var i = parseInt(v);

    if (i === 2) { $(f).show(); } else { $(f).hide(); }
    if (i === 3) { $(a).show(); } else { $(a).hide(); }


}




function showHeader() {
    $("#dvNoHdr").hide();
    $("#dvHdrAdd").hide();
    $("#dvHasHdr").show();

    $("#rowAddHdr").show();
    $("#rowUpdHdr").hide();
}



function addHeader() {
    var v = $("#pgId").val();
    var hTl = $("#t5").val();
    var hTx = $("#t6").val();

    var fd = new FormData();
    fd.append("PgId", v);
    fd.append("HdTtl", hTl);
    fd.append("HdTxt", hTx);

    var f = document.getElementById("ImgHse_1").files;
    if (f.length > 0) {
        fd.append('Files', f[0]);
    }

    $.ajax({
        cache: false,
        url: "/Content/AddPgHdr",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            $("#rowAddHdr").hide();
            $("#rowUpdHdr").show();
            Lobibox.alert('success',
                {
                    title: 'Complete',
                    msg: 'Page header added successfully'
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




function updateHeader() {
    var v = $("#pgId").val();
    var hTl = $("#t5").val();
    var hTx = $("#t6").val();

    var fd = new FormData();
    fd.append("PgId", v);
    fd.append("HdTtl", hTl);
    fd.append("HdTxt", hTx);

    var f = document.getElementById('ImgHse_1').files;
    if (f.length > 0) {
        fd.append('Files', f[0]);
    }

    $.ajax({
        cache: false,
        url: "/Content/UpdatePgHdr",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            Lobibox.alert('success',
                {
                    title: 'Update Complete',
                    msg: 'Page header successfully updated'
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


function nixHeaderImg() {

    Lobibox.confirm({
        title: "Delete Image?",
        msg: "This action cannot be undone - continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {
                var pgId = $("#pgId").val();
                var igNm = $("#hiUl").val();

                var fd = new FormData();
                fd.append("PgId", pgId);
                fd.append("IgNm", igNm);

                $.ajax({
                    cache: false,
                    url: "/Content/NixHeaderImage",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {
                        return false;
                    },
                    complete: function () {
                        $("#img_1").empty();
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });

}



function delHeader() {

    Lobibox.confirm({
        title: "Delete Header?",
        msg: "You are about to permanently delete the page content header. This action cannot be undone - continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {

                var pgId = $("#pgId").val();
                var igNm = $("#hiUl").val();

                var fd = new FormData();
                fd.append("PgId", pgId);
                fd.append("IgNm", igNm);

                $.ajax({
                    cache: false,
                    url: "/Content/NixHeader",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {
                        return false;
                    },
                    complete: function () {
                        setContent(pgId);
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}






function addToolTipRow() {

    var sz = $("#dvTipRows > div").length;
    var lr = "";
    var id = "";
    var bgc = "";


    if (sz > 0) {
        lr = $("#dvTipRows > div").last().css("background-color");
        id = $("#dvTipRows > div").last().attr("data-id");
        bgc = lr === 'rgb(255, 247, 229)' ? 'bisque' : '#FFF7E5';
        id = parseInt(id) + 1;
        alert(id);
    } else {
        $("#dvNoTips").hide();
        id = 0;
        bgc = "bisque";
    }

    var block = "<div style='padding:2px; border-bottom:solid 1px black; background-color: " + bgc + "'>";
    block += "<div style='width: 5%; display: inline-block; text-align: center'><span class='uploadTxt' href='#' onclick='deleteToolTip(this, 0)'>delete</span></div>";
    block += "<div style='width: 5%; display: inline-block; text-align: center; color:#000000'>0</div>";
    block += "<div style='width: 15%; display: inline-block'><input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='model' id='tipDesc" + id + "'></div>";
    block += "<div style='width: 69%; display: inline-block'><input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='model' id='tipTxt" + id + "'></div>";
    block += "<div style='width: 6%; display: inline-block; text-align: center'><button type='button' class='btn btn-xs btn-green' style='width:50px !important' onclick='addToolTip(" + id + ")'>Add</button></div>";
    block += "</div>";
    $('#dvTipRows').append(block);
    $("#dvTips").show();


    $("#dvTipRows").scrollTop($("#dvTipRows")[0].scrollHeight);
}

function addToolTip(id) {

    var pgId = $("#pgId").val();
    var desc = $("#tipDesc" + id);
    var txt = $("#tipTxt" + id);

    var d = $(desc).val();
    var t = $(txt).val();

    var fd = new FormData();
    fd.append("PgId", pgId);
    fd.append("TtDs", d);
    fd.append("TtTx", t);

    $.ajax({
        cache: false,
        url: "/Content/AddToolTip",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            Lobibox.alert('success',
                {
                    title: 'Tool Tip Added',
                    msg: 'Tool Tip has been successfully added'
                });

        },
        complete: function () {
            setContent(pgId);
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function updateToolTip(id) {

    var desc = $("#tipDesc" + id);
    var txt = $("#tipTxt" + id);

    var d = $(desc).val();
    var t = $(txt).val();

    var fd = new FormData();
    fd.append("Id", id);
    fd.append("TtDs", d);
    fd.append("TtTx", t);

    $.ajax({
        cache: false,
        url: "/Content/UpdToolTip",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            Lobibox.alert('success',
                {
                    title: 'Tool Tip Updated',
                    msg: 'Tool Tip has been successfully updated'
                });

        },
        complete: function () {

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}



function deleteToolTip(ev, id) {

    if (id > 0) {
        var fd = new FormData();
        fd.append("Id", id);

        $.ajax({
            cache: false,
            url: "/Content/NixToolTip",
            type: "POST",
            contentType: false,
            processData: false,
            data: fd,
            success: function (data) {
                Lobibox.alert('success',
                    {
                        title: 'Tool Tip Deleted',
                        msg: 'Tool Tip has been successfully removed'
                    });

            },
            complete: function () {
                var pgId = $("#pgId").val();
                setContent(pgId);
            },
            error: function (err, data) {
                alert("Status : " + data.responseText);
            }
        });
    }

    $(ev).parent().parent().hide();
}


function updateSeo() {
    
    var v = $("#pgId").val();
    var sPg = $("#t1").val();
    var sTl = $("#t8").val();
    var sKw = $("#t2").val();
    var sOg = $("#t7").val();
    var sDc = $("#t3").val();

    var fd = new FormData();
    fd.append("PgId", v);
    fd.append("PgNam", sPg);
    fd.append("PgTtl", sTl);
    fd.append("PgKwd", sKw);
    fd.append("PgOgt", sOg);
    fd.append("PgDsc", sDc);


    $.ajax({
        cache: false,
        url: "/Content/UpdatePgSeo",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            Lobibox.alert('success',
                {
                    title: 'Update Complete',
                    msg: 'SEO content successfully updated'
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

function getToolTips() {
    var v = $("#pgId").val();
}

function addCampaign() {
    addBannerRow();
}


function addBnrCmpRow() {

    var sz = $("#dvCmpRows > div").length;
    var lr = "";
    var id = "";
    var rc = "";


    if (sz > 0) {
        lr = $("#dvCmpRows > div").last().css("background-color");
        rc = lr === 'rgb(255, 247, 229)' ? 'bisque' : '#FFF7E5';
    } else {
        $("#dvNoCmp").hide();
        rc = "bisque";
    }

    var block = "<div style='border-bottom:solid 1px black; height:40px; background-color: " + rc + "'>";
    block += "<div style='width: 59px; display: inline-block; text-align: center; vertical-align:-webkit-baseline-middle'><span class='uploadTxt' href='#' onclick='nixPgCmp(this, 0)''>delete</span></div>";
    block += "<div style='width: 80px; display: inline-block; vertical-align:-webkit-baseline-middle; text-align:center'>";
    block += "<select class='ag-control input-sm txt-ct-w2' id='sPos0' name='sPos0' style='margin-top:5px'>";
    for (var j = 1; j < 11; j++) {
        block += "<option value='" + j + "'>" + j + "</option>";
    }
    block += "</select></div>";

    block += "<div style='width: calc(100% - 205px); display: inline-block; color:#000000; vertical-align:-webkit-baseline-middle'>";

    block += "<select class='ag-control input-sm txt-ct-w1' id='sCmp0' name='sCmp0' style='margin-top:5px'>";
    block += "</select></div>";
    block += "<div style='width: 50px; display: inline-block; text-align: center; padding-top:5px; vertical-align:top'><button type='button' class='btn btn-xs btn-green' style='width:50px !important' onclick='addPgCmp()'>Add</button></div>";
    block += "</div>";
    $('#dvCmpRows').append(block);

    getAvlCmps(); 

    $("#dvCmp").show();

    $("#dvCmpRows").scrollTop($("#dvCmpRows")[0].scrollHeight);
}

function addPgCmp() {
    var v = $("#pgId").val();
    var sPs = $("#sPos0 option:selected").val();
    var sCp = $("#sCmp0 option:selected").val();

    var fd = new FormData(); 
    fd.append("PgId", v);
    fd.append("sPos", sPs);
    fd.append("sCmp", sCp);

    $.ajax({
        cache: false,
        url: "/Content/AddPgCampaign",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            setContent(v);

        },
        complete: function () {

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });

}

function nixPgCmp(ev, v) {

    if (v > 0) {

        Lobibox.confirm({
            title: "Delete Campaign?",
            msg: "You are about to permanently delete this campaign. This action cannot be undone - continue?",
            modal: true,
            callback: function (lobibox, type) {
                if (type === 'no') {
                    return;
                } else {
                    var pid = $("#pgId").val();

                    var fd = new FormData();
                    fd.append("Id", v);

                    $.ajax({
                        cache: false,
                        url: "/Content/NixPgCampaign",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fd,
                        success: function (data) {
                            setContent(pid);
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
    } else {
        $(ev).parent().parent().hide();
    }
}


function getAvlCmps() {

    var v = $("#pgId").val();

    var fd = new FormData();
    fd.append("PgId", v);

    $.ajax({
        cache: false,
        url: "/Content/GetAvlCampaigns",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            $("#sCmp0").empty();
            bindGenericMenu(data, "sCmp0", "CAMPAIGN");

        },
        complete: function () {

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
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
        alert(id);
    } else {
        $("#dvNoBnr").hide();
        id = 0;
        bgc = "bisque";
    }

    var block = "<div style='padding:2px; border-bottom:solid 1px black; background-color: " + bgc + "'>";
    block += "<div style='display: inline-block; padding-top: 2px; width:155px'>";
    block += "<div class='fileinput fileinput-new img-loop' data-provides='fileinput' style='display: inline-block; margin-bottom:0 !important'>";
    block += "<div id='dvBnr_" + id + "' class='fileinput-preview thumbnail img-header-frame' data-trigger='fileinput'></div>";
    block += "<div class='checkForm' style='text-align: center'><span class='btn-file'><input type='file' name='agimg' id='ImgBnr_" + id + "' onchange='showImgCancel(this)'></span></div></div></div>";
    block += "<div style='display: inline-block; vertical-align: middle;  width:calc(100% - 305px) !important'>";
    block += "<div class='top-buffer'>";
    block += "<label for='txBrDsc" + id + "' class='label-bnr lbl-black'>Description: </label><input type='text' class='ag-control input-sm ctrl-brn-width pad-lft' name='model' id='txBrDsc" + id + "'>";
    block += "</div>";
    block += "<div class='top-buffer'>";
    block += "<label for='txBrUrl" + id + "' class='label-bnr lbl-black'>Nav URL: </label><input type='text' class='ag-control input-sm ctrl-brn-width pad-lft' name='model' id='txBrUrl" + id + "'>";
    block += "</div>";
    block += "<div class='top-buffer'>";
    block += "<label for='t5' class='label-bnr lbl-black'>Sort ID: </label><select style='margin-left:3px' class='ag-control input-sm ctrl-brn-width' id='sSrt" + id + "' name='sSrt" + id + "' onchange='setMenus(this.value)'><option value='1'>1</option></select>";
    block += "</div>";
    block += "</div>";
    block += "<div style='display: inline-block; vertical-align: bottom; width:150px; text-align:center; padding-bottom:10px'>";
    block += "<div class='feed-uplPicTxt' style='padding-bottom: 5px; display:none' id='dvCanImg'>";
    block += "<a href='#' class='fileinput-exists feed-uplPicTxt img-loop uploadTxt' title='' data-image='dvBnr_" + id + "' onclick='nixImg(this)'>Cancel Img</a>";
    block +="</div>";
    block += "<div id='btnWrpAdd'><div><button type='button' class='btn btn-xs btn-green2' onclick='addBnr(" + id + ")'>Add Banner</button></div><div style='padding-top:3px'><span class='link12Blue' onclick='CancelBnr()'>Cancel Add</span></div></div>";
    block += "<div id='btnWrpUpd' style='display:none'><div><button type='button' class='btn btn-xs btn-blue'>Update Banner</button></div>";
    block += "<div style='padding-top:3px'><button type='button' class='btn btn-xs btn-red2'>Delete Banner</button></div></div>";
    block += "</div>";
    block += "</div>";
    $('#dvBnrRows').append(block);
    $("#dvBnr").show();

    $("#dvBnrRows").scrollTop($("#dvBnrRows")[0].scrollHeight);
}


function showImgCancel(ev) {

    var pd = $(ev).attr('id');
    //alert(pd);

    $("#dvCanImg").show();
    //$("#" + pd).hide();

    //var sd = $("#" + pd).closest('div').find("[id^=editOpt]");
    //var sid = $(sd).attr('id');
    //$("#" + sid).show();

}


function nixImg(ev) {

    var o = ev.getAttribute("title");
    var d = ev.getAttribute("data-image");
    var imgDv = $("#" + d);
    var imgId = $(imgDv).find('img');
    $(imgId).attr("src", o);

}


function addBnr(id) {

    var bCmp = 1;
    var bDly = 0;
    var bDsc = $("#txBrDsc" + id).val();
    var bUrl = $("#txBrUrl" + id).val();
    var bImg = $("#dvBnr_" + id).find('img');
    var bSrt = $("#sSrt" + id);
    var sVal = $("option:selected", bSrt).val();
    var bSrc = 0;

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
        var pgId = $("#pgId").val();

        var fd = new FormData();
        fd.append("CpId", bCmp);
        fd.append("BrDy", bDly);
        fd.append("PgId", pgId);
        fd.append("BrId", id);
        fd.append("BrSt", sVal);
        fd.append("BrDs", bDsc);
        fd.append("BrNu", bUrl);
 

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


function showStaticContent() {

    CKEDITOR.instances.content.setData("");

    $("#dvStcCnt").show();
    $("#rowBtnAdd").show();
    $("#rowBtnUpd").hide();
    $("#dvNoCnt").hide();
    $("#rowAddCnt").hide();

    window.scrollTo(0, document.body.scrollHeight);

}


function updStaticContent() {

    var v = $("#stId").val();
    var d = CKEDITOR.instances.content.getData();
    var pgId = $("#pgId").val();

    var fd = new FormData();
    fd.append("sId", v);
    fd.append("sTxt", d);

   
    $.ajax({
        cache: false,
        url: "/Content/UpdStcTxt",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            setContent(pgId);
            Lobibox.alert('success',
                {
                    title: 'Update Complete',
                    msg: 'Content successfully updated'
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

function addStaticContent() {

    var d = CKEDITOR.instances.content.getData();
    var pgId = $("#pgId").val();

    var fd = new FormData();
    fd.append("pgId", pgId);
    fd.append("sTxt", d);


    $.ajax({
        cache: false,
        url: "/Content/AddStcTxt",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            setContent(pgId);
            Lobibox.alert('success',
                {
                    title: 'Complete',
                    msg: 'Content successfully added'
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


function nixStaticContent() {

    Lobibox.confirm({
        title: "Delete Content?",
        msg: "You are about to permanently delete all page content. This action cannot be undone - continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {
                var id = $("#stId").val();

                var fd = new FormData();
                fd.append("Id", id);

                $.ajax({
                    cache: false,
                    url: "/Content/DelStcTxt",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {
                        return false;
                    },
                    complete: function () {
                        var pgId = $("#pgId").val();
                        setContent(pgId);
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}

function updateHomeAll() {

    $(".home-row-title").each(function () {
        var id = $(this).attr("row-id");
        updateHomeConfig(id, false);
    });

    $(".home-row").each(function () {
        var id = $(this).attr("item-id");
        updateFeatureItem(id, false);
    });

    

    Lobibox.alert('success',
        {
            title: 'Update Complete',
            msg: 'Home page configuration successfully updated'
        });
}


function updateHomeConfig(v, show) {

    var xFtr = $("#FtrId" + v).val();
    var xPos = $("#sPosHm" + v).val();
    var xOpt = $("#sOpt" + v).val();
    var xChk = $("#ckAtv" + v);
    var xAtv = $(xChk).prop("checked") ? true : false;

    var fd = new FormData();
    fd.append("Id", v);
    fd.append("Pos", xPos);
    fd.append("Opt", xOpt);
    fd.append("Ftr", xFtr);
    fd.append("Atv", xAtv);
 
    $.ajax({
        cache: false,
        url: "/Content/UpdateHomeOption",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

        },
        complete: function () {
            if (show) {
                Lobibox.alert('success',
                    {
                        title: 'Update Complete',
                        msg: 'Home feature item successfully updated'
                    });
            }
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function addHomeConfig(v) {

    var xFtr = $("#FtrId" + v).val();
    var xPos = $("#sPosHm" + v).val();
    var xOpt = $("#sOpt" + v).val();
    var xChk = $("#ckAtv" + v);
    var xAtv = $(xChk).prop("checked") ? true : false;

    var fd = new FormData();
    fd.append("Pos", xPos);
    fd.append("Opt", xOpt);
    fd.append("Atv", xAtv);
    fd.append("Ftr", xFtr);
 
    $.ajax({
        cache: false,
        url: "/Content/AddHomeOption",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            Lobibox.alert('success',
                {
                    title: 'Content Added',
                    msg: 'Home feature item successfully added'
                });
        },
        complete: function () {
            var pid = $("#pgId").val();
            setContent(pid);
            $("#dvHomeRows").scrollTop($("#dvHomeRows")[0].scrollHeight);

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function addFtrRow(gid) {

    $.ajax({
        cache: false,
        url: "/Content/GetFeatureMenus",
        type: "POST",
        contentType: false,
        processData: false,
        success: function (data) {
            var lst1 = data.AO;
            var lst2 = data.SZ;
            var lst3 = data.BC;

            var fc = $('div[group-id=' + gid + ']');
            var rw = $('div[row-id=' + gid + ']');

            var fcl = $(fc).length;
            var rc;

            var bc = fc.last().css('background-color');
            if (bc === "rgb(255, 228, 196)") { rc = "#FFF7E5"; } else { rc = "bisque"; }

            var mxId = getMaxFtrId() + 1;

            var block = "<div id='rw0' style='background-color: " + rc + "' class='home-row'  item-id='" + mxId + "' group-id='" + gid + "'>";
            block += "<div class='home-row-item' style='padding-top: 6px; font-weight: bold'><a id='delHm0' class='link12Blue' onclick='nixFeatureItem(this, 0, false)'>delete</a></div>";
            block += "<div class='home-row-item' style='padding-right: 3px;'>";
            block += "</div>";
            block += "<div class='home-row-item'>";
            block += "</div>";
            block += "<div class='home-row-item'>";
            block += "</div>";
            block += "<div class='home-row-item' style='padding-right: 5px;'>";
            block += "<input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='xFtrId" + mxId + "' id='xFtrId" + mxId + "' onkeypress='return isNumber(event)' maxlength='6'>";
            block += "</div>";
            block += "<div class='home-row-item'>";

            block += "<select class='ag-control input-sm txt-ct-w1' id='FtrSz" + mxId + "' name='FtrSz" + mxId + "'>";
            block += "<option value='0'>-SELECT-</option>";
            for (var c = 0; c < lst2.length; c++) {
                block += "<option value='" + lst2[c].Value + "'>" + lst2[c].Text + "</option>";
            }
            block += "</select>";

            block += "</div>";
            block += "<div class='home-row-item'>";
            block += "<input type='checkbox' id='ckPro" + mxId + "' />";

            block += "</div>";
            block += "<div class='home-row-item'>";

            block += "<select class='ag-control input-sm txt-ct-w1' id='ColId" + mxId + "' name='ColId" + mxId + "'>";
            block += "<option value='0'>-SELECT-</option>";
            for (var y = 0; y < lst3.length; y++) {
                block += "<option value='" + lst3[y].Value + "'>" + lst3[y].Text + "</option>";
            }
            block += "</select>";

            block += "</div>";
            block += "<div class='home-row-item' style='padding-right: 5px;'>";
            block += "<input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='PrTx" + mxId + "' id='PrTx" + mxId + "'>";
            block += "</div>";

            block += "<div class='home-row-item'><button type='button' class='btn btn-xs btn-green' style='width:50px !important' onclick='addFeatureItem(" + gid + ", " + mxId + ")'>ADD</button></div>";
            block += "<div class='home-row-item' style='padding-top: 6px; font-weight: bold'>";
            block += "</div>";
            block += "</div>";

            if (fcl.length > 0) {
                fc.last().after(block);
            } else {
                rw.after(block);
            }





        }
    });
}


function addFeatureItem(grp, mx) {

    var xFtr = $("#xFtrId" + mx).val();
    var xFsz = $("#FtrSz" + mx).val();
    var xCol = $("#ColId" + mx).val();
    var xChk = $("#ckPro" + mx);
    var xTxt = $("#PrTx" + mx).val();
    var xPro = $(xChk).prop("checked") ? true : false;

    var fd = new FormData();
    fd.append("Grp", grp);
    fd.append("Ftr", xFtr);
    fd.append("Siz", xFsz);
    fd.append("Col", xCol);
    fd.append("IsP", xPro);
    fd.append("Txt", xTxt);


    $.ajax({
        cache: false,
        url: "/Content/AddFeatureItem",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            Lobibox.alert('success',
                {
                    title: 'Content Added',
                    msg: 'Home feature item successfully added'
                });
        },
        complete: function () {
            var pid = $("#pgId").val();
            setContent(pid);
            $("#dvHomeRows").scrollTop($("#dvHomeRows")[0].scrollHeight);

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}



function updateFeatureItem(id, show) {

    var xFtr = $("#xFtrId" + id).val();
    var xFsz = $("#FtrSz" + id).val();
    var xCol = $("#ColId" + id).val();
    var xChk = $("#ckPro" + id);
    var xTxt = $("#PrTx" + id).val();
    var xPro = $(xChk).prop("checked") ? true : false;

    var fd = new FormData();
    fd.append("Id", id);
    fd.append("Ftr", xFtr);
    fd.append("Siz", xFsz);
    fd.append("Col", xCol);
    fd.append("IsP", xPro);
    fd.append("Txt", xTxt);

    $.ajax({
        cache: false,
        url: "/Content/UpdateFeatureItem",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            if (show) {
                Lobibox.alert('success',
                    {
                        title: 'Item Added',
                        msg: 'Home feature item successfully added'
                    });
            }
        },
        complete: function () {
            var pid = $("#pgId").val();
            setContent(pid);
            $("#dvHomeRows").scrollTop($("#dvHomeRows")[0].scrollHeight);

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}





function nixHomeItem(ev, id, db) {

    if (db) {

        Lobibox.confirm({
            title: "Delete Feature Item?",
            msg: "You are about to permanently delete this item. This action cannot be undone - continue?",
            modal: true,
            callback: function (lobibox, type) {
                if (type === 'no') {
                    return;
                } else {
                    var pid = $("#pgId").val();
                    var fd = new FormData();
                    fd.append("Id", id);

                    $.ajax({
                        cache: false,
                        url: "/Content/NixHomeItem",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fd,
                        success: function (data) {
                            setContent(pid);
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
    else {
        $(ev).parent().parent().hide();
    }
}


function nixFeatureItem(ev, id, db) {

    if (db) {

        Lobibox.confirm({
            title: "Delete Feature Item?",
            msg: "You are about to permanently delete this item. This action cannot be undone - continue?",
            modal: true,
            callback: function (lobibox, type) {
                if (type === 'no') {
                    return;
                } else {
                    var pid = $("#pgId").val();
                    var fd = new FormData();
                    fd.append("Id", id);

                    $.ajax({
                        cache: false,
                        url: "/Content/NixFeatureItem",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fd,
                        success: function (data) {
                            setContent(pid);
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
    else {
        $(ev).parent().parent().hide();
    }
}


function getMaxId() {
    var max = 0;
    $("div").find(".home-row-title").each(function() {
        var a = $(this).attr("row-id");
        var c = parseInt(a);
        if (c > max) max = c;
    });
    return max;
}


function getMaxFtrId() {
    var max = 0;
    $("div").find(".home-row").each(function () {
        var a = $(this).attr("item-id");
        var c = parseInt(a);
        if (c > max) max = c;
    });
    return max;
}


function addHomeRow() {

    var fd = new FormData();

    $.ajax({
        cache: false,
        url: "/Content/AddHomeMenus",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            var lst1 = data.AO;
            var sz = $("#dvHomeRows > .home-row-title").length;
            var hId = "";
 

            if (sz > 0) {
                hId = getMaxId() + 1;
            } else {
                $("#dvNoHome").hide();
                hId = 0;
            }

            var block = "<div style='background-color:lightblue' class='home-row-title'  row-id='" + hId + "'>";
            block += "<div class='home-row-item' style='padding-top: 6px; font-weight: bold'><a id='delHm" + hId + "' class='link12Blue' onclick='nixHomeItem(this, " + hId + ", false)'>delete</a></div>";
            block += "<div class='home-row-item' style='padding-right: 3px;'>";
            block += "<select class='ag-control input-sm txt-ct-w1' id='sPosHm" + hId + "' name='posHm" + hId + "'>";
            for (var a = 1; a < 16; a++) {
                block += "<option value='" + a + "'>" + a + "</option>";
            }
            block += "</select></div>";
            block += "<div class='home-row-item'>";
            block += "<select class='ag-control input-sm txt-ct-w1' id='sOpt" + hId + "' name='sOpt" + hId + "' onchange='setDisplay(this.value, " + hId + ")'>";
            block += "<option value='0'>-SELECT-</option>";
            for (var b = 0; b < lst1.length; b++) {
                block += "<option value='" + lst1[b].Value + "'>" + lst1[b].Text + "</option>";
            }
            block += "</select></div>";
            block += "<div class='home-row-item' style='position: relative; top:10px;'><input type='checkbox' id='ckAtv" + hId + "' disabled='disabled' /></div>";
            block += "<div class='home-row-item' style='padding-right: 5px;'><input type='text' class='ag-control input-sm pad-lft txt-ct-w1' name='FtrId" + hId + "' id='FtrId"+hId+"' onkeypress='return isNumber(event)' maxlength='6'></div>";
            block += "<div class='home-row-item'>";
            block += "</div>";
            block += "<div class='home-row-item'></div>";
            block += "<div class='home-row-item'>";
            block += "</div>";
            block += "<div class='home-row-item' style='padding-right: 5px;'></div>";
            block += "<div class='home-row-item'><button type='button' class='btn btn-xs btn-green2' style='width:50px !important' onclick='addHomeConfig(" + hId + ")'>Add</button></div>";
            block += "<div class='home-row-item' style='padding-top: 6px; font-weight: bold'></div>";
            block += "</div>";
            $('#dvHomeRows').append(block);
            $("#dvHome").show();


            $("#dvHomeRows").scrollTop($("#dvHomeRows")[0].scrollHeight);



        },
        complete: function () {

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });




}