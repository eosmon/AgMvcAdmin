function loadFeatures() {

    $.ajax({
        cache: false,
        url: "/Content/AllFeatures",
        type: "POST",
        contentType: false,
        processData: false,
        success: function (data) {

            $("#dvFeatureRows").empty();


            if (data.length > 0) {
                var rc = "#FFF7E5";

                $.each(data, function (i, item) {

                    var id = item.Id;
                    var mstId = item.MasterId;
                    var cat = item.CategoryName;
                    var sub = item.SubCategoryName;
                    var img = item.ImageUrl;
                    var desc = item.ItemDesc;
                    var upc = item.UpcCode;

                    var sale = item.OnSale ? "<b style='color:red'>Yes</b>" : "No";
                    var used = item.IsUsed ? "<b style='color:darkblue'>Used</b>" : "New";


                    var block = "<div class='feature-row' style='background-color: " + rc + "'>";
                    block += "<div class='sale-feature-item'><span class='link12Blue' href='#' onclick=\"nixFeature('" + id + "')\">" + id + "</span></div>";
                    block += "<div class='sale-feature-item'>" + mstId + "</div>";
                    block += "<div class='sale-feature-item'>" + used + "</div>";
                    block += "<div class='sale-feature-item'>" + sale + "</div>";
                    block += "<div style='text-align: center; vertical-align:top; padding-top: 4px; padding-bottom: 4px;'><img src='" + img + "' class='sale-img' alt=''></div>";
                    block += "<div style='padding-left:5px; padding-top:5px;'><div><b style='color:blue'>" + cat + " - " + sub + "</b></div>  <div>" + desc + "</div> <div style='display:inline-block'>UPC: <b>" + upc + " </b></div> </div>";
                    block += "</div>";



                    $('#dvFeatureRows').append(block);

                    rc = rc === "#FFF7E5" ? "bisque" : "#FFF7E5";

                });
            }
            else
            {
                $("#dvFeature").hide();
                $("#dvNoFeature").show();
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


function searchSale() {
    var cnd = $("#s1 :selected").val();
    var cat = $("#s2 :selected").val();
    var mfg = $("#s3 :selected").val();
    var cal = $("#s4 :selected").val();
    var sub = $("#s5 :selected").val();
    var sal = $("#s6 :selected").val();

    var fd = new FormData();
    fd.append("cndId", cnd);
    fd.append("catId", cat);
    fd.append("mfgId", mfg);
    fd.append("calId", cal);
    fd.append("subId", sub);
    fd.append("salId", sal);

    $.ajax({
        cache: false,
        url: "/Content/SearchSaleItems",
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fd,
        success: function(data) {
            $("#saleRows").empty();
            $("#dvNoData").hide();

            if (data.Item!=null) {

                $("#dvFtrGrid").show();

                var rc = "#3366CC";

                $.each(data.Item,
                    function(i, item) {

                        var mstId = item.MasterId;
                        var units = item.Units;
                        var msrp = item.Msrp === 0 ? "" : item.Msrp.toFixed(2);
                        var askPr = item.AskingPrice.toFixed(2);
                        var salPr = item.SalePrice === 0 ? "" : item.SalePrice.toFixed(2);
                        var proGr = "$" + item.GrossProfit.toFixed(2);
                        var marGr = Math.floor(item.GrossMargin * 100) + "%";
                        var manuf = item.ManufName;
                        var image = item.ImageUrl;
                        var sbcNm = item.SubCategoryName;
                        var itmDs = item.ItemDesc;
                        var mfgPn = item.MfgPartNumber;
                        var upcCd = item.UpcCode;
                        var onSal = item.OnSale;
                        var datSt = onSal ? item.StrStartDate : "";
                        var datEd = onSal ? item.StrEndDate : "";

                        var isSal = "";
                        var noSal = "selected";


                        if (onSal) {
                            isSal = "selected";
                            noSal = "";
                        }

                        var block = "<div data-id='" + mstId + "' class='sale-row' style='color:black;background-color:" + rc + "'>";
                        block += "<div class='sale-bdr'><div class='sale-p1'><a class='link12White' href='#' onclick='addFeatureItem(" + mstId + ")'>Add</a></div></div>";
                        block += "<div class='sale-bdr'><div class='sale-img-cell'><img src='" + image + "' class='sale-img' alt=''></div></div>";
                        block += "<div class='sale-bdr' style='padding:3px; color:white'><div><b style='color:yellow'>" + manuf + "</b> - <b>" + sbcNm + "</b> Item: <b>" + mstId + "</b></div>  <div>" + itmDs + "</div>  <div><div style='display:inline-block'>UPC: <b>" + upcCd + " </b></div> <div style='display:inline-block'> MFG#: <b>" + mfgPn + "</b></div></div> </div>";
                        block += "<div class='sale-bdr' style='text-align:center'> <div class='sale-item-pad'><select id='cboSale" + mstId + "' class='ag-control input-sm' style='width:100%'><option value='true' " + isSal + ">Yes</option><option value='false' " + noSal + ">No</option></select></div> <div class='sale-item-pad' style='padding-bottom:3px'><input id='txtSale" + mstId + "' type='text' class='ag-control input-sm' value='" + salPr + "' placeholder='Sale Price' onkeypress='return isDecimal(event)' maxlength='9'></div> </div>";
                        block += "<div class='sale-bdr'><div class='sale-p1'>" + units + "</div></div>";
                        block += "<div class='sale-bdr'><div class='sale-item-pad'><input id='txtMsrp" +
                            mstId +
                            "' type='text' class='ag-control input-sm' value='" +
                            msrp +
                            "' placeholder='MSRP' onkeypress='return isDecimal(event)' maxlength='9'></div> <div class='sale-item-pad'><input id='txtPrice" +
                            mstId +
                            "' type='text' class='ag-control input-sm' value='" +
                            askPr +
                            "' placeholder='Price' onkeypress='return isDecimal(event)' maxlength='9'></div> </div>";
                        block += "<div class='sale-bdr'><div class='sale-p2'> <div class='sale-margin'>" +
                            marGr +
                            "</div><div class='sale-profit'>" +
                            proGr +
                            "</div> </div></div>";
                        block += "<div class='sale-bdr'><div class='sale-item-pad'><input id='txtStart" +
                            mstId +
                            "' type='date' class='ag-control input-sm' value='" +
                            datSt +
                            "' placeholder='Sale Beg Date' style='color:blue'></div><div class='sale-item-pad'><input id='txtEnd" +
                            mstId +
                            "' type='date' class='ag-control input-sm' value='" +
                            datEd +
                            "' placeholder='Sale End Date' style='color:red'></div> </div>";
                        block += "<div class='sale-bdr'><div class='sale-p3'><button id='btn" +
                            mstId +
                            "' type='button' class='btn btn-xs btn-tan' style='width:60px !important' onclick='updateSaleItem(" +
                            mstId +
                            ")'>Update</button></div></div>";
                        block += "</div>";
                        $("#saleRows").append(block);

                        rc = rc === "#3366CC" ? "#6699CC" : "#3366CC";
                    });

            }
            else {
                $("#dvFtrGrid").hide();
                $("#dvNoData").show();
            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
 
        }
    });
}

function updateSaleItem(v) {

    var os = $("#cboSale" + v).val();
    var ts = $("#txtSale" + v).val();
    var ms = $("#txtMsrp" + v).val();
    var pr = $("#txtPrice" + v).val();
    var sd = $("#txtStart" + v).val();
    var ed = $("#txtEnd" + v).val();

    var fd = new FormData();
    fd.append("MstId", v);
    fd.append("OnSal", os);
    fd.append("PrSal", ts);
    fd.append("PrMsr", ms);
    fd.append("PrAsk", pr);
    fd.append("StDat", sd);
    fd.append("EdDat", ed);

        $.ajax({
            cache: false,
            url: "/Content/UpdateSaleItem",
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fd,
            success: function (data) {
                searchSale();
            },
            error: function (err, data) {
                alert(err);
            },
            complete: function () {
                Lobibox.alert('success',
                    {
                        title: 'Complete',
                        msg: 'Item updated successfully'
                    });
            }
        });
}

function addFeatureItem(v) {

    var fd = new FormData();
    fd.append("MstId", v);

    $.ajax({
        cache: false,
        url: "/Content/CreateFeatureItem",
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fd,
        success: function (data) {
            loadFeatures();
            $("#dvFeatureRows").scrollTop($("#dvFeatureRows")[0].scrollHeight);
            $("#saleRows").empty();
            $("#dvFtrGrid").hide();
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            Lobibox.alert('success',
                {
                    title: 'Complete',
                    msg: 'Feature item added successfully'
                });
        }
    });

}

function nixFeature(id) {
    
    Lobibox.confirm({
        title: "Delete Feature Campaign?",
        msg: "You are about to permanently delete this campaign. This action cannot be undone - continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {

                var fd = new FormData();
                fd.append("Id", id);

                $.ajax({
                    cache: false,
                    url: "/Content/NixFeatureCampaign",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {
                        loadFeatures();
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

function getGroupMenus(v) {

    var d = $("#s3");
    var f = $("#s5");

    $("#mid").val(d);
    $(d).find("option").remove().end();
    $(f).find("option").remove().end();
    d.append("<option value=''>- SELECT -</option>");
    f.append("<option value=''>- SELECT -</option>");
    
    var fd = new FormData();
    fd.append("catId", v);

    $.ajax({
        cache: false,
        url: "/Content/GetGroupMenus",
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fd,
        success: function (data) {
            showMfg();
            var c = data.length;
            if (c === 0) { return; }
            else
            {
                $.each(data.Mfg, function (i, item) {
                    d.append("<option value=" + item.Value + ">" + item.Text + "</option>");
                });

                $.each(data.Cat, function (i, item) {
                    f.append("<option value=" + item.Value + ">" + item.Text + "</option>");
                });
            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#s3").selectpicker("refresh");
            flushSearch();
        }
    });
}

function showMfg() {

    $("#dvMfg").hide(); $("#dvCtg").hide(); $("#dvOns").hide(); $("#dvCal").hide(); $("#dvBtn").hide();

    $("#s6").prop("selectedIndex", 0);

    var z = $("#s2").prop("selectedIndex");
    if (z > 0) {
        $("#dvMfg").show();
    }

}

function showOther() {
    var z = $("#s3").prop("selectedIndex");
    if (z > 0) {
        var x = $("#s2").prop("selectedIndex");
        $("#dvOns").show();
        $("#dvCtg").show();
        $("#dvBtn").show();
        if (x < 3) { $("#dvCal").show(); } else { $("#dvCal").hide(); }
    } else {
        $("#dvOns").hide();
        $("#dvCtg").hide();
        $("#dvBtn").hide();
    }
}

function getMenuCal(v) {

    showOther();

    var z = $("#s2").prop("selectedIndex");
    if (z === 3) { return; }

    var cx = $("#s4");
    $(cx).find("option").remove().end();
    cx.append("<option value=''>- SELECT -</option>");
    var v1 = $("#s2").val();

    $.ajax({
        data: "{ catId: '" + v1 + "', mfgId: '" + v + "'}",
        url: "/Content/GetCalMenu",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            var cx = result.length;
            if (cx === 0) { return; }
            else {
                $.each(result, function (i, item) {
                    $("#s4").append("<option value=" + item.Link + ">" + item.MenuText + "</option>");
                });
            }
        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            flushSearch();
        }
    });
}


function getMenuSubCat(v) {

    var d = $("#s5");
    $("#mid").val(d);
    $(d).find("option").remove().end();
    d.append("<option value=''>- SELECT -</option>");

    $.ajax({
        data: "{ catId: '" + v + "'}",
        url: "/Content/GetSubMenu",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            var c = result.length;
            if (c === 0) { return; }
            else {
                $.each(result, function (i, item) {
                    d.append("<option value=" + item.Value + ">" + item.Text + "</option>");
                });
            }
        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
        }
    });
}

function showSearch() {
    $("#dvSrchBlock").show();
}

function flushSearch() {
    $("#saleRows").empty();
    $("#dvNoData").hide();
    $("#dvFtrGrid").hide();
}