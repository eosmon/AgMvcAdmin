
$(function () {
    $("#t18").datepicker({ onSelect: function () { $(this).valid(); } });
});

//$(function () {
//    $("#t23").datepicker({ onSelect: function () { $(this).valid(); } });
//});

$(document).ready(function () {
    $("form[data-form-validate='true']").each(function () {

        $(".selectpicker").selectpicker().change(function () {
            $(this).valid();
        });


        $(this).validate({
            rules: {
                TransCat: { required: true },
                LiveWeb: { required: true },
                AddToWeb: { required: true },
                SubCategory: { required: true },
                Manufacturer: { required: true },
                Caliber: { required: true },
                UpcCode: { required: true },
                SearchUpc: { required: true },
                BulletWeight: { required: true },
                BulletType: { required: true },
                RoundsPerBox: { required: true },
                Units: { required: true },
                UnitsWyo: {
                    required: true,
                    greaterThanZero: true 
                },
                UnitsCal: {
                    required: true,
                    greaterThanZero: true 
                },
                Condition: { required: true },
                AskPrice: { required: true },
                UnitCost: { required: true },
                TransType: { required: true },
                AcqDate: { required: true },
                Restock: { required: true },
                Chamber: { required: true },
                ShotSize: { required: true },
                TagName: { required: true }
            },
            messages: {
                TransCat: "Transaction Group Required",
                LiveWeb: "Live On Website Required",
                AddToWeb: "Add To Website Required",
                SubCategory: "Sub Category Required",
                Manufacturer: "Manufacturer Required",
                Caliber: "Caliber Required",
                UpcCode: "UPC Code Required",
                SearchUpc: "Web Search UPC Code Required",
                BulletWeight: "Bullet Weight Required",
                BulletType: "Bullet Type Required",
                RoundsPerBox: "Rounds Per Container Required",
                Units: "Unit Quantity Required",
                UnitsWyo: "Wyoming Units Required",
                UnitsCal: "California Units Required",
                Condition: "Condition Required",
                AskPrice: "Asking Price Required",
                UnitCost: "Unit Cost Required",
                TransType: "Transaction Type Required",
                AcqDate: "Acquisition Date Required",
                Restock: "Restock Unit Count Required",
                Chamber: "Shotgun Shell Length Required",
                ShotSize: "Shotgun Pellet Shot Size Required",
                TagName: "Service Tag Customer Name Required"
            },
            errorPlacement: function (error, element) {
                var id = element.attr("id");
                $('#error' + id).append(error);
                $('#' + id).css("background-color", "#FFFF00");
                var c1 = $('#' + id).closest('div').find('button');
                c1.css("background-color", "yellow");

            },
            highlight: function (element) { $(element).css('background', '#FFFF00'); },
            unhighlight: function (element) {
                $(element).css('background', '#ffffff');
                $(element).closest('div').find('button').css("background-color", "white");
            }

        });

    });
});


/* AUTOCOMPLETE */
(function ($) {
    // Custom autocomplete instance.
    $.widget("app.autocomplete", $.ui.autocomplete, {


        _renderItem: function (ul, item) {

            return $("<li></li>")
                .data("item.autocomplete", item)
                .append("<a>" + item.label + "</a>")
                .appendTo(ul);


        }

    });


    // Autocomplete FFL Name
    $(function () {
        $("#t34").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Inventory/FflByName",
                    data: "{ search: '" + request.term + "', state: '" + $("#s23").val() + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        response($.map(data,
                            function (item) {

                                var tn = item.FflNumber + " : <span style=\"color:yellow;font-weight:bold\">" + item.TradeName + "</span>";
                                var lbl = tn + ' - ' + item.FflAddress + ' ' + item.FflCityStZip + ' PH. ' + item.FflPhone;

                                return { label: lbl, value: item };
                            }));
                    },
                    complete: function () {
                        $("body").scrollTop($(document).height());
                    }
                });
            },
            focus: function (event, ui) {
                this.value = "";
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();

                var id = ui.item.value.FflId;
                var fc = ui.item.value.FflCode;
                var n = ui.item.value.TradeName;
                var a = ui.item.value.FflAddress;
                var c = ui.item.value.FflCityStZip;
                var fn = ui.item.value.FflNumber;
                var em = ui.item.value.FflEmail;

                var flc = ui.item.value.FflFullLic;
                var exp = ui.item.value.FflExpires;
                var lof = ui.item.value.FflOnFile;
                var vld = ui.item.value.FflIsValid;
                var onf = lof ? "Yes" : "No";

                $("#fcd").val(fc);

                if (!vld) { exp = exp + " (EXPIRED)"; }

                var txt = n + ' ' + a + ' ' + c + ' ' + fn;
                $("#t33").val(em);
                $("#t34").val(txt);

                //setConfirmation(n, flc, exp, onf, id);
            }
        });
    });



    // Autocomplete Customer Search
    $(function () {
        $("#t35").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Customer/FindCustomers",
                    data: "{ search: '" + request.term + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {

                        if (!data.length) {

                            var t = "<div style=\"width:100%; text-align:center; font-size:1.4em !important\">No Matches Found</div>";

                            var result = [{ label: t, value: response.term }];
                            return response(result);

                        }
                        else {
                            response($.map(data, function (item) {

                                var iDiv = item.ProfilePic.length > 0 ? "<img src=\"" + item.ProfilePic + "\" class=\"pic-cust\" />" : "<div style='text-align:center'>Customer Photo Not Found</div>";

                                var d = "<div style=\"display:inline-block; vertical-align:top\">" + iDiv;
                                d += "<div style='vertical-align:top;display: inline-block; vertical-align: middle; padding-left: 10px; font-size:13px !important'>";
                                d += "<div class=\"cust-ui-name\">" + item.StrFullName + "</div>";
                                d += "<div><b>" + item.StrFullAddr + "</b></div>";
                                d += "<div>" + item.StrEmailPhn + "</div>";
                                d += "</div>";
                                d += "</div>";

                                return { label: d, value: item };
                            }));

                        }


                    },
                    complete: function () {
                        $("body").scrollTop($(document).height());
                        $("#ui-id-3").append("<li class=\"ui-menu-item\" style=\"text-align:center !important; height:30px\"><a class=\"ui-Link\" onclick=\"newCustomer()\">Create New Customer</a></li>");
                        $("#ui-id-3").append("<li class=\"ui-menu-item\" style=\"text-align:center !important; height:30px\"><a class=\"ui-Link\" onclick=\"readBySwipe()\">Create Customer From ID</a></li>");
                    }
                });
            },
            focus: function (event, ui) {
                this.value = "";
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();

                var id = ui.item.value.CustomerId;
                var fn = ui.item.value.StrFullName;
                $("#cus").val(id);
                $("#t35").val(fn);
            }
        });
    });


})(jQuery);

 


function loadAmmo() {

    var mfgId = $("#mid").val();
    var calId = $("#cid").val();

    var fd = new FormData();
    fd.append("MfgId", mfgId);
    fd.append("CalId", calId);
 
    return $.ajax({
        cache: false,
        url: "/Inventory/GetAmmo",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            //setNavCounts(data.Count);

            $("#ammoRows").empty();

                var rc = "#FFF7E5";

                $.each(data, function (i, item) {

                    var aid = item.Id;
                    var cal = item.UnitsCa;
                    var why = item.UnitsWy;
                    var rpb = item.RoundsPerBox;
                    var rsk = item.Restocks;
                    var web = item.IsOnWeb;
                    var img = item.ImageUrl;
                    var mpn = item.MfgPartNumber;
                    var upc = item.UpcCode;
                    var mfg = item.ManufName;
                    var mdl = item.Model;
                    var dsc = item.ItemDesc;
                    var ifs = item.IsForSale;


                    var ow = "No";
                    var fs = "No";
                    if (web) { ow = "Yes"; }
                    if (ifs) { fs = "Yes"; }

                    var opt = ifs ? "Purchase(s)" : "Service(s)";

                    var au = upc.length > 0 ? ' UPC: ' + upc : '';
                    var nds = mdl + ', ' + dsc + ', MFG# ' + mpn + ', ' + rpb + ' Rounds Per Box, ' + au;


                    var imgUl = img.length > 0 ? "<img src='" + img + "' class='gun-img' alt=''>" : "";

                    var block = "<div data-id='" + aid + "' class='ammo-row' style='color:black;background-color:" + rc + "'>";
                    block += "<div class='ammo-bdr' style='padding-top:14px'><div class='gun-p1' style='text-align:center'><select id='atn" + aid + "' onchange='runAmmoAtn(this, " + aid + ", " + ifs + ")'  style='border-radius:4px; padding:3px'><option value='0'>-SELECT-</option><option value='1'>Update</option><option value='2'>Restock</option></select></div></div>";
                    block += "<div class='ammo-bdr' style='padding-top:14px'><div class='gun-p1' style='text-align:center'><div class='hide-it' id='hit-" + aid + "'><select id='cb" + aid + "'  style='border-radius:4px; padding:3px; display:none' onchange='getAmmoById(this.value," + ifs + ");'></select></div><span id='al" + aid + "'>" + rsk + " " + opt + "</span></div></div>";
                    block += "<div class='ammo-bdr'><div class='ammo-img-row'>" + imgUl + "</div></div>";
                    block += "<div class='ammo-bdr'><div class='gun-p1'><div><b style='color:blue'>" + mfg + "</b></div><div>" + nds + "</div></div></div>";
                    block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + fs + "</div></div>";
                    block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + ow + "</div></div>";
                    block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + cal + "</div></div>";
                    block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + why + "</div></div>";
                    block += "</div>";
                    $("#ammoRows").append(block);

                    rc = rc === "#FFF7E5" ? "#FAFAFB" : "#FFF7E5";

                });

            },
            error: function (err, result) {
                alert(err);
            },
            complete: function () {

            }
    });
}


function getPurchases(v) {

    var sp = $("#al" + v);
    var mu = $("#cb" + v);

    $.ajax({
        data: "{ itemId: '" + v + "', catId: '200'}",
        url: "/Inventory/InStockMenu",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            var c = result.length;
            if (c === 0) { return; }
            else {

                $(sp).hide();
                $(mu).show();

                $(mu).find("option").remove().end();
                mu.append("<option value=''>- SELECT -</option>");

                $.each(result, function (i, item) {
                    mu.append("<option value=" + item.Value + ">" + item.Text + "</option>");
                });
            }
        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            $("#dvRestockLeft").show();
            $("#dvRestockRight").show();
            $("#dvPics").show();
            $("#dvAmmoTtl").text("Create Ammunition Entry");
        }
    });
}

function setManuf(v) {
    $("#mid").val(v);
    loadAmmo();
}


function setCaliber(v) {
    $("#cid").val(v);
    loadAmmo();
}


function showToAdd(item) {

    $("#addMfg").hide();
    $("#addCal").hide();
    $("#addColor").hide();
    $("#addBulletType").hide();


    switch (item) {
    case 1:
        var v1 = $("#s1").val();
        if (v1 === "-1") {
            $("#t10").val("");
            $("#addMfg").show();
        }
        break;
    case 2:
        var v2 = $("#s2").val();
        if (v2 === "-1") {
            $("#t11").val("");
            $("#addCal").show();
        }
        break;
    case 3:
        var v3 = $("#s3").val();
        if (v3 === "-1") {
            $("#t12").val("");
            $("#addBulletType").show();
        }
        break;
    case 4:
        //var v4 = $("#s7").val();
        //if (v4 === "-1") {
        //    $("#t13").val("");
        //    $("#addColor").show();
        //}
        break;
    }

}

function showAddMfg() {
    var v1 = $("#s3").val();
    if (v1 === "-1") {
        $("#t1").val("");
        $("#addMfg").show();
    }
}


function checkSubCats() {

    $("#divSubCat").hide();
    var v1 = $("#s1").val();

    $.ajax({
        data: "{ catId: '" + v1 + "'}",
        url: "/Inventory/GetSubCats",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            var c = result.length;
            if (c === 0) { return; }
            else {
                $("#divSubCat").show();

                var d = $("#s2");
                $(d).find("option").remove().end();
                d.append("<option value=''>- SELECT -</option>");

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


function runAmmoAtn(el, id, ifs) {

    var opt = parseInt(el.value);
    $("#aid").val(id);

    $("#dvBlanket").hide();
    $("span[id^='al']").show();
 
    switch (opt) {
        case 0:
            var sp = $("#al" + id);
            var mu = $("#cb" + id);
            $(sp).show();
            $(mu).hide();
            return;
        case 1:
            getPurchases(id);
            break;
        case 2:
            showRestock(id, ifs);
            $("select[id^='cb']").hide();
            break;
        case 3:
            flushAmmo();
            break;
    }

    el.selectedIndex = 0;

    var cb = "#hit-" + id;
    $(".hide-it").show();
    $(".hide-it").not(cb).hide();
    $("select[id^='cb']").prop("selectedIndex", 0);
}

function hideAllPanels() {

    $("#btnAddNew").hide();
    $("#btnUpdate").hide();
    $("#dvBtnDelete").hide();
    $("#btnRestock").hide();
    $("#dvAmmo").hide();
    $("#dvRestock").hide();

    $("#rowBtnAdd").hide();
    $("#rowBtnUpd").hide();
    $("#rowBtnRsk").hide();
}



function wipeAmmoImg() {

    Lobibox.confirm({
        title: "Delete Image!",
        msg: "Permanently delete this image? This action cannot be undone",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {
                var id = $("#mstId").val();
                var img = $("#CusImg_1").find("img").attr("src");

                var fd = new FormData();
                fd.append("Id", id);
                fd.append("Img", img);

                $.ajax({
                    cache: false,
                    url: "/Inventory/NixAmmoImage",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {

                    },
                    complete: function () {
                        $("#CusImg_1").empty();
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}




function showAmmoAtn(opt) {

    formatCmp();
    hideAllPanels();
    $("#dvBlanket").show();


    switch (opt) {
    case "":
        $("#dvBlanket").hide();
    return;
    case 1:        
        $("#dvAmmoTtl").text("Update Ammunition Entry");
        $("#dvAmmo").show();
        //$("#pnlCmpR").hide();
        $("#pnlCmpL").css("width", "100%");
        $("#rowBtnUpd").show();    
            
        break;
    case 3:
        $("#btnUpdate").hide();
        $("#dvBtnDelete").hide();
        $("#btnRestock").show();
        $("#dvAcct").show();
        $("#dvBooks").show();
        $("#lbUnts").text("ADD Units:");
        break;
    case 4:
        resetAmmo();
        break;
    }
}

function setTransType(v)
{
    resetAmmo();
    setTtp(v);
}

 


function resetAmmo()
{
    formatCmp();
    hideAllPanels();
    $("#dvBlanket").show();

    startOver();
    $("#btnCancel").css("display", "inline-block");
    $("#dvAmmoTtl").text("Create Ammunition Entry");
    $("#dvAmmo").show();
    $("#btnAddNew").show();
    $("#rowBtnAdd").show();
    $("#delCol").hide();

    $("#dvRestockLeft").show();
    $("#dvRestockRight").show();
    $("#dvPics").show();
}


function formatCmp() {
    $("#pnlCmpL").show();
    //$("#pnlCmpR").show();
    $("#pnlCmpL").css("width", "50%");
    //$("#pnlCmpR").css("width", "50%");
}


function showRestock(v, ifs) {

    flushRestock();

    $("#isi").val(v);

    $("#dvTax").hide();
    $("#dvGotTax").hide();

    $("#dvCnd").hide();
    $("#btnUpdate").hide();
    $("#dvBtnDelete").hide();
    $("#btnAddNew").hide();

    $("#dvBlanket").show();
    $("#dvAcct").show();
    $("#btnRestock").show();
    $("#rowBtnUpd").show();

    $("#dvRestockLeft").hide();
    $("#dvRestockRight").hide();
    $("#dvPics").hide();
    $("#dvAmmoTtl").text("Restock Ammunition Entry");

    $("#s11").prop("selectedIndex", 0);
    $("#s11 > option").each(function () { this.style.display = "block"; });

    if (ifs) {
        $("#s11 > option").each(function () {
            var i = parseInt(this.value);
            if (i > 102) { this.style.display = "none"; }
            $("#taxCustName").hide();
        });
    } else {
        $("#s11 > option").each(function () {
            var i = parseInt(this.value);
            if (i === 101 || i === 102) { this.style.display = "none"; }
            $("#taxCustName").show();
        });
    }

}


//function getAmmoRestock(v) {
//    var fd = new FormData();
//    fd.append("Id", v);

//    $.ajax({
//        cache: false,
//        url: "/Inventory/GetAmmoProduct",
//        type: "POST",
//        contentType: false,
//        processData: false,
//        data: fd,
//        success: function (d) {

//            $("#dvAmmo").hide();
//            $("#dvAcct").show();
//            $("#btnUpdate").css("display", "inline-block");
//            $("#dvBtnDelete").css("display", "inline-block");


//            var m1 = d.AcctModel.ItemCost.toFixed(2);
//            var m2 = d.AcctModel.FreightCost.toFixed(2);
//            var m3 = d.AcctModel.ItemFees.toFixed(2);
//            var m4 = d.AcctModel.SellerTaxAmount.toFixed(2);
//            var m5 = d.AcctModel.SellerCollectedTax;

//            var b1 = d.BookModel.TransTypeId;
//            var b2 = d.BookModel.TransId;
//            var b3 = d.BookModel.AcqFullName;
//            var b4 = d.BookModel.AcqCustType;
//            var b5 = d.BookModel.AcqFflNumber;
//            var b6 = d.BookModel.AcqCaAvNumber;
//            var b7 = d.BookModel.AcqAddress;
//            var b8 = d.BookModel.DispFullName;
//            var b9 = d.BookModel.DispCustType;
//            var b10 = d.BookModel.DispFflNumber;
//            var b11 = d.BookModel.DispCaAvNumber;
//            var b12 = d.BookModel.DispAddress;
//            var b13 = d.BookModel.StrDateAcq;
//            var b14 = d.BookModel.StrDateDsp;
//            //var b15 = d.BookModel.IsDisposed;

//            var a1 = d.CostBasisId;
//            var a8 = d.ConditionId;
//            var a9 = d.AcctModel.UnitsCal;
//            var a21 = d.AcctModel.UnitsWyo;
 
//            var a16 = d.TagSku;
//            var x1 = m5 ? "true" : "false";
 
//            $("#t3").val(m1);
//            $("#t9").val(a9);
//            $("#t30").val(a21);
//            $("#t7").val(m2); 
//            $("#t8").val(m3); 

//            $("#s8").val(a8);
//            $("#s10").val(x1);
//            $("#s11").val(b1);

//            $("#s6").val(b4);
//            $("#s7").val(b9);

//            $("#t14").val(b5);
//            $("#t15").val(b6);
//            $("#t16").val(b3);
//            $("#t17").val(b7);
//            $("#t18").val(b13);
//            $("#t19").val(b10);
//            $("#t20").val(b11);
//            $("#t21").val(b8);
//            $("#t22").val(b12);
//            $("#t23").val(b14);
 
//            $("#t25").val(m4);

//            $("#stkId").val(b2);

//            switch(b4) {
//            case "1":
//                $("#acqFfl").show();
//                break;
//            case "2":
//                $("#acqAv").show();
//                break;
//            case "3":
//            case "4":
//                $("#acqAdr").show();
//                break;
//            }

//            var ci = parseInt(a9);
//            if (ci > 0) { $("#dvBooks").show(); } else { $("#dvBooks").hide(); }

//        },
//        error: function (err, data) {
//            alert(err);
//        },
//        complete: function () {
//            $("#aid").val(v);
//        }
//    });
//}    



function getAmmoById(v, b) {

    $("#cbid").val(v);

    var fd = new FormData();
    fd.append("Id", v);
    fd.append("Sa", b);

    $.ajax({
        cache: false,
        url: "/Inventory/GetAmmoProduct",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {

            $("#btnRestock").hide();
            $("#dvBlanket").show();
            $("#dvAmmo").show();
            $("#dvAcct").show();
            $("#btnUpdate").css("display", "inline-block");
            $("#dvBtnDelete").css("display", "inline-block");
            $("#dvCnd").show();
            //$("#dvGroup").hide();

            var m1 = d.AcctModel.ItemCost.toFixed(2);
            var m2 = d.AcctModel.FreightCost.toFixed(2);
            var m3 = d.AcctModel.ItemFees.toFixed(2);
            var m4 = d.AcctModel.SellerTaxAmount.toFixed(2);
            var m5 = d.AcctModel.SellerCollectedTax;
            var m7 = d.AcctModel.AskingPrice.toFixed(2);
            var m8 = d.AcctModel.CustPricePaid.toFixed(2);
            var m9 = d.AcctModel.CustomerId;
            var m10 = d.AcctModel.Msrp.toFixed(2);
            var m11 = d.AcctModel.LocationId;

            var b2 = d.BookModel.TransId;
            var b3 = d.BookModel.SupplierID;
            var b4 = d.BookModel.AcqTypeId;
            var b5 = d.BookModel.TransTypeId;
            var b6 = d.BookModel.AcqFflCode;
            var b7 = d.BookModel.AcqStateId;
            var b8 = d.BookModel.CustomerName;
            var b9 = d.BookModel.AcqName;
            var b10 = d.BookModel.AcqEmail;
            var b11 = d.BookModel.StrDateAcq;
 
            var b16 = d.BookModel.IsSale;

 
            var d15 = d.Images.PicId;
            var d16 = d.Images.ImgHse1;
            var d17 = d.Images.ImgHse2;
            var d18 = d.Images.ImgHse3;
            var d19 = d.Images.ImgHse4;
            var d20 = d.Images.ImgHse5;
            var d21 = d.Images.ImgHse6;
            var d22 = d.Images.Io1;
            var d23 = d.Images.Io2;
            var d24 = d.Images.Io3;
            var d25 = d.Images.Io4;
            var d26 = d.Images.Io5;
            var d27 = d.Images.Io6;

            var a1 = d.InStockId;
            var a2 = d.CaliberId;
            var a3 = d.AmmoManufId;
            var a4 = d.BulletTypeId;
            var a5 = d.GrainWeight;
            var a6 = d.RoundsPerBox;
            var a7 = d.SubCategoryId;
            var a8 = d.ConditionId;
            var a9 = d.AcctModel.UnitsCal;
            var a10 = d.IsSlug;
            var a11 = d.IsOnWeb;
            var a12 = d.UpcCode;
            var a13 = d.IsActualPpt;
            var a14 = d.ItemDesc;
            var a15 = d.MfgPartNumber;
            var a16 = d.TagSku;
            var a17 = d.IsActive;

            var a18 = d.Chamber.toFixed(3);
            var a19 = d.ShotSizeWeight.toFixed(2);
            var a20 = d.WebSearchUpc;
            var a21 = d.AcctModel.UnitsWyo;

            $("#s11 > option").each(function () { this.style.display = "block"; });
            //$("#pts").val(m11);

            setCostInfo(b5.toString());
            flushTax(m5.toString());
            showStateOpt(m11.toString());

            if (a13) { $("#dvCaPpt").show(); } else { $("#dvCaPpt").hide(); }


            if (b16) {
                $("#s11 > option").each(function () {
                    var i = parseInt(this.value); if (i > 102) { this.style.display = "none"; }
                });

            } else {
                $("#s11 > option").each(function () { var i = parseInt(this.value); if (i === 101 || i === 102) { this.style.display = "none"; } });
            }

            if (a11) { showHideActive('true'); } else { showHideActive('false'); }

            var x0 = a11 ? "true" : "false";
            var x1 = m5 ? "true" : "false";
            var x2 = a10 ? "true" : "false";
            var x3 = a17 ? "true" : "false";
            var x4 = a13 ? "true" : "false";


            $("#s1").val(a3);  
            $("#s2").val(a2); 
            $("#s3").val(a4);  
            $("#s5").val(x0); 
            $("#s8").val(a8);
            $("#s9").val(a7);
            $("#s10").val(x1);
            $("#s11").val(b5);
            $("#s12").val(x2);
            $("#s17").val(x3);
            $("#s18").val(m11);
            $("#s21").val(b4);
            $("#s23").val(b7);
            $("#s42").val(x4);
            
            $("#t1").val(a15);
            $("#t2").val(m7); 
            $("#t3").val(m1);
            $("#t4").val(a14);
            $("#t5").val(a5); 
            $("#t6").val(a6); 
            $("#t7").val(m2); 
            $("#t8").val(m3); 
            $("#t9").val(a9);

            $("#t18").val(b11);
            $("#t24").val(a12);
            $("#t25").val(m4);
            $("#t27").val(a18);
            $("#t28").val(a19);
            $("#t29").val(a20);
            $("#t30").val(a21);
            $("#t31").val(m10);
            $("#t33").val(b10);
            $("#t35").val(b8);
            $("#t47").val(m8);
           

            $("#sup").val(b3);
            $("#cus").val(m9);
            $("#fcd").val(b6);
            $("#isi").val(a1);
            $("#ttp").val(b5);

            $("#stkId").val(b2);
            
            $("#s1").selectpicker("refresh");
            $("#s2").selectpicker("refresh");
            $("#s3").selectpicker("refresh");

            $("#findFfl").hide();
            $("#supSource").hide();

            switch (b4) {
                case 1:
                    $("#t34").val(b9);
                    $("#findFfl").show();
                    break;
                default:
                    $("#t52").val(b9);
                    $("#supSource").show();
                    break;
            }


            $('[id^="ImgM_"]').empty();

            if (d16.length > 0) { $("#ImgM_1").append("<img src='" + d16 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_1").attr("orig-img", d22); } else { $("#delCol_1").hide(); }
            if (d17.length > 0) { $("#ImgM_2").append("<img src='" + d17 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_2").attr("orig-img", d23); } else { $("#delCol_2").hide(); }
            if (d18.length > 0) { $("#ImgM_3").append("<img src='" + d18 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_3").attr("orig-img", d24); } else { $("#delCol_3").hide(); }
            if (d19.length > 0) { $("#ImgM_4").append("<img src='" + d19 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_4").attr("orig-img", d25); } else { $("#delCol_4").hide(); }
            if (d20.length > 0) { $("#ImgM_5").append("<img src='" + d20 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_5").attr("orig-img", d26); } else { $("#delCol_5").hide(); }
            if (d21.length > 0) { $("#ImgM_6").append("<img src='" + d21 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_6").attr("orig-img", d27); } else { $("#delCol_6").hide(); }

            $("#dvMakeUpc").hide();

            setShotgun(a7);

            if (b16) {
                $("#dvAddWeb").show();
                $("#s11 > option").each(function () {
                    var i = parseInt(this.value); if (i > 102) { this.style.display = "none"; }
                });
            } else {
                $("#s5").val("false");
                $("#dvAddWeb").hide();
                $("#taxCustName").show();
                $("#s11 > option").each(function () { var i = parseInt(this.value); if (i === 101 || i === 102) { this.style.display = "none"; } });
            }

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#rowBtnUpd").show();
        }
    });
}



function addAmmoItem() {

    var v1 = $("#t1").val();    // MfgPartNumber
    var v2 = $("#t5").val();    // GrainWeight
    var v3 = $("#t9").val();    // Units: CA
    var v4 = $("#t30").val();   // Units: WY
    var v5 = $("#t6").val();    // Round Per Box
    var v6 = $("#t2").val();    // Price
    var v7 = $("#t4").val();    // Description
    var v8 = $("#t3").val();    // Cost
    var v9 = $("#t7").val();    // Freight
    var v10 = $("#t8").val();   // Fees

    var v15 = $("#t18").val();  // Acq Date

    var v21 = $("#t24").val();  // UPC Code
    var v22 = $("#t25").val();  // Tax Collected
    var v23 = $("#t27").val();  // Chamber Size
    var v24 = $("#t28").val();  // Shot Size
    var v25 = $("#t29").val();  // Web Search UPC
    var v26 = $("#t35").val();  // Customer Name
    var v27 = $("#t47").val();  // Customer Paid
    var v28 = $("#t33").val();  // FFL Email


    var s1 = $("#s1").val();    // MfgID
    var s2 = $("#s2 :selected").val();  // CalID
    var s3 = $("#s3").val();    // BulletTypeID
    var s4 = $("#s5 :selected").val(); //AddToWeb
    var s5 = $("#s8 :selected").val();  // ConditionID
    var s6 = $("#s9 :selected").val(); // SubCatId
    var s7 = $("#s10 :selected").val(); // SellerCollTax
    var s8 = $("#s11 :selected").val();  // TransTypeID
    var s9 = $("#s12 :selected").val();  // IsSlug
    var s10 = $("#s17 :selected").val(); // IsActive
    var s11 = $("#s18").val();    // LocationID
    var s12 = $("#s42").val();    // IsPPT
    var s14 = $("#s21").val();    // AcqSourceID

    var h1 = $("#cus").val();
    var h2 = $("#sup").val();
    var h3 = $("#fcd").val();

    var fd = new FormData();
    fd.append("MfgPn", v1);   
    fd.append("TtpId", s8);
    fd.append("SctId", s6);
    fd.append("MfgId", s1);
    fd.append("CalId", s2);
    fd.append("Price", v6);
    fd.append("Ucost", v8);
    fd.append("Descr", v7);
    fd.append("SClTx", s7);
    fd.append("AddWb", s4);
    fd.append("AcqDt", v15);
    fd.append("UpcCd", v21);
    fd.append("UpcWb", v25);
    fd.append("GrWgt", v2);
    fd.append("BulTp", s3);
    fd.append("RdpBx", v5);
    fd.append("Frght", v9);
    fd.append("Ufees", v10);
    fd.append("UntCa", v3);
    fd.append("UntWy", v4);
    fd.append("TaxAm", v22);
    fd.append("CndId", s5);
    fd.append("Email", v28);
    fd.append("AcqSc", s14);
    fd.append("CusId", h1);
    fd.append("SupId", h2);
    fd.append("FflCd", h3);
    fd.append("CusPd", v27);
    fd.append("LocId", s11);
    fd.append("iSlug", s9);
    fd.append("iActv", s10);
    fd.append("iPptr", s12);
    fd.append("Chamb", v23);
    fd.append("ShtSz", v24);
    fd.append("TgCus", v26);
    
 

    $("input[id^=ImgHse_]").each(function () {
        var fileId = $(this).attr("id");
        var oImg = $("#" + fileId).attr("orig-img");
        var f = document.getElementById(fileId).files;
        if (f.length > 0) {
            fd.append(fileId, f[0]);
            fd.append('GroupId', fileId);
            fd.append('OrigImg', oImg);
        }
    });


    $.ajax({
        cache: false,
        url: "/Inventory/AddAmmo",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            writeTag(data);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#divShadow").hide();
            $("#divConfirmData").hide();
            $("#divDone").show();
        }
    });
}

function writeTag(d) {
 
    var fSz = 0;
    var ml = d.MfgName.length;

    if (ml < 6) { fSz = "22px"; }
    if (ml > 5 && ml < 9) { fSz = "20px"; }
    if (ml > 8 && ml < 12) { fSz = "16px"; }
    if (ml > 11 && ml < 19) { fSz = "13px"; }
    if (ml > 18 && ml < 42) { fSz = "10px"; }
    if (ml > 41) { fSz = "9px"; }

    $("#tagMfg").css("font-size", fSz);

    $("#tagMfg").text(d.MfgName);
    $("#tagDsc").text(d.Category);
    $("#tagCal").text(d.Caliber);
    $("#tagTyp").text(d.BulType);
    $("#tagRds").text(d.RdsPerBx);
    $("#tagMpn").text(d.MfgPartNum);
    $("#tagSku").text(d.TagSku);
    $("#tagSvc").text(d.SvcType);
    $("#tagSvc").css("color", "blue");
    $("#brcTxt").text(d.TagSku);
    $("#tagPrc").removeAttr("style");

    if (d.IsSale) {
        $("#tagConfType").show();
        $("#tagConfCap").show();
        $("#tagConfMfg").show();
        $("#tagService").hide();
        $("#tagOurPrc").show();
        $("#tagPrc").text(d.TagPrice).formatCurrency();
    } else {
        $("#tagConfType").hide();
        $("#tagConfCap").hide();
        $("#tagConfMfg").hide();
        $("#tagService").show();
        $("#tagOurPrc").hide();
        $("#tagPrc").text(d.SvcName);
        $("#tagPrc").css("width", "200px");
        $("#tagPrc").css("font-weight", "800");
    }
}


function updateAmmoItem() {

    var isi = $("#isi").val();
    var cbi = $("#cbid").val();
    var mfg = $("#s1").val();
    var clb = $("#s2").val();
    var btp = $("#s3").val();
    var web = $("#s5").val();
    var cnd = $("#s8").val();
    var sct = $("#s9").val();
    var sgt = $("#s10").val();
    var ttp = $("#s11").val();
    var slg = $("#s12").val();
    var atv = $("#s17").val();
    var loc = $("#s18").val();
    var acq = $("#s21").val();

    var mpn = $("#t1").val();
    var prc = $("#t2").val();
    var cos = $("#t3").val();
    var dsc = $("#t4").val();
    var bwt = $("#t5").val();
    var rpb = $("#t6").val();
    var frt = $("#t7").val();
    var fee = $("#t8").val();
    var uca = $("#t9").val();
    var adt = $("#t18").val();
    var upc = $("#t24").val();
    var txc = $("#t25").val();
    var chb = $("#t27").val();
    var shz = $("#t28").val();
    var wup = $("#t29").val();
    var uwy = $("#t30").val();
    var msr = $("#t31").val();
    var eml = $("#t33").val();
    var nam = $("#t35").val();
    var cpd = $("#t47").val();


    var cus = $("#cus").val();
    var sup = $("#sup").val();
    var fcd = $("#fcd").val();
    var ipt = $("#s42").val();

    if (loc === "1") { uwy = 0; }
    if (loc === "2") { uca = 0; }

    var fd = new FormData();
    fd.append("Isi", isi);
    fd.append("Cbi", cbi); 
    fd.append("Atv", atv);
    fd.append("Loc", loc);

    fd.append("Acq", acq);

    fd.append("Sct", sct);
    fd.append("Mfg", mfg);
    fd.append("Clb", clb);
    fd.append("Btp", btp);
    fd.append("Bwt", bwt);
    fd.append("Rpb", rpb);
    fd.append("Cnd", cnd);
    fd.append("Ttp", ttp);
    fd.append("UCa", uca);
    fd.append("UWy", uwy);
    fd.append("Msr", msr);
    fd.append("Txc", txc);
    fd.append("Shz", shz);
    fd.append("Prc", prc);
    fd.append("Cos", cos);
    fd.append("Chb", chb);
    fd.append("Frt", frt);
    fd.append("Fee", fee);
    fd.append("Cpd", cpd);
    fd.append("Web", web);
    fd.append("Slg", slg);
    fd.append("Ipt", ipt);
    fd.append("Sgt", sgt);
    fd.append("Mpn", mpn);
    fd.append("Dsc", dsc);
    fd.append("Upc", upc);
    fd.append("Wup", wup);
    fd.append("Adt", adt);
    fd.append("Cus", cus);
    fd.append("Sup", sup);
    fd.append("Fcd", fcd);
    fd.append("Eml", eml);
    fd.append("TgCus", nam);
 
    $("input[id^=ImgHse_]").each(function () {
        var fileId = $(this).attr("id");
        var oImg = $("#" + fileId).attr("orig-img");
        var f = document.getElementById(fileId).files;
        if (f.length > 0) {
            fd.append(fileId, f[0]);
            fd.append('GroupId', fileId);
            fd.append('OrigImg', oImg);
        }
    });

    $.ajax({
        cache: false,
        url: "/Inventory/UpdateAmmoItem",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            writeTag(data);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#divShadow").hide();
            $("#divConfirmData").hide();
            $("#divDone").show();
        }
    });
}

function restockAmmo() {

    var iv = $("#form-ammo").valid();
    if (!iv) { return; }

    var inStk = $("#isi").val();
    var ttpId = $("#s11").val();
    var untCa = $("#t9").val();
    var untWy = $("#t30").val();
    var locId = $("#s18").val();
    var acqId = $("#s21").val();

    var taxAm = $("#t25").val();
    var iFrgt = $("#t7").val();
    var iCost = $("#t3").val();
    var iFees = $("#t8").val();
    var cusPd = $("#t47").val();

    var gotTx = $("#s10").val(); 
    var aqDat = $("#t18").val();
    var email = $("#t33").val();
    var cusNm = $("#t35").val();
    var isPpt = $("#s42").val(); //Is PPT

    var cusId = $("#cus").val();
    var supId = $("#sup").val();
    var fflCd = $("#fcd").val();

    var isOwb = $("#sp6").text();
    var iow = isOwb === "YES" ? true : false;

    var fd = new FormData();
    fd.append("InStk", inStk);
    fd.append("TtpId", ttpId);
    fd.append("UntCa", untCa);
    fd.append("UntWy", untWy);
    fd.append("CusId", cusId);
    fd.append("SupId", supId);
    fd.append("FflCd", fflCd);
    fd.append("AcqTp", acqId);
    fd.append("LocId", locId);
    fd.append("TaxAm", taxAm);
    fd.append("FrgtA", iFrgt);
    fd.append("CostA", iCost);
    fd.append("FeesA", iFees);
    fd.append("CusPd", cusPd);
    fd.append("TgCus", cusNm);
    fd.append("Email", email);
    fd.append("GotTx", gotTx);
    fd.append("IsOwb", iow);
    fd.append("CaPpt", isPpt);
    fd.append("AqDat", aqDat);


    $.ajax({
        cache: false,
        url: "/Inventory/RestockAmmo",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            writeTag(data);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#divShadow").hide();
            $("#divConfirmData").hide();
            $("#divDone").show();
        }
    });
}

function deleteAmmo(v) {

    Lobibox.confirm({
        title: "Delete Ammunition Item?",
        msg: "You are about to permanently delete this ammunition item. This action cannot be undone - continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {

                var fd = new FormData();
                fd.append("Id", v);

                $.ajax({
                    cache: false,
                    url: "/Inventory/NixAmmo",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {
                        return false;
                    },
                    complete: function () {
                        loadAmmo();
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}

function addAmmoManuf() {

    var n = $("#t10").val();
    var s = $("#s1");

    $.ajax({
        data: "{ newMfg: '" + n + "', sectId: '1'}",
        url: "/Inventory/AddOtherManuf",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: "Ammunition Manufacturer Already Exists",
                        msg: '<b>' + n + '</b> cannot be added because that ammunition manufacturer already exists',
                        color: '#000000'
                    });
            } else {

                var si = response.Manuf[0].SelectedManufId;

                $(s).find("option").remove().end();
                s.append("<option>- SELECT -</option>");
                s.append("<option value=\"-1\">- ADD NEW MANUFACTURER -</option>");

                $.each(response.Manuf, function (i, item) {
                    s.append("<option value=" + item.ManufId + ">" + item.ManufName + "</option>");
                });
                $("#addMfg").hide();
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



function addMerchandiseItem() {

    var a = $("#s1").val();
    var b = $("#s2").val();
    var c = $("#s3").val();
    var d = $("#s4").val();
    var e = $("#t2").val();
    var f = $("#t3").val();
    var g = $("#t8").val();
    var h = $("#t4").val();
    var i = $("#t9").val();
    var j = "";
    var k = $("#t5").val();
    var l = $("#t6").val();
    var m = $("#t7").val();
    var n = $("#chkTag").prop("checked");
    var o = $("#tagMfg").text();
    var p = $("#tagGrp").text();
    var q = $("#tagCls").text();
    var r = $("#tagCnd").text();

    var ts = $("#tagSku").text();


    $.ajax({
        data: "{cat:'" + a + "', scId:'" + b + "', mfg:'" + c + "', cnd:'" + d + "', mpn:'" + e + "', prc: '" + f + "',  unt: '" + g + "', ttl:'" + h + "', dsc: '" + i + "', img: '" + j + "', sku: '" + ts + "', cst: '" + k + "', frt: '" + l + "', fee: '" + m + "', tag: '" + n + "', mfgN: '" + o + "', catN: '" + p + "', sctN: '" + q + "', cndN: '" + r + "'}",
        url: "/Inventory/AddMerchandise",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#divShadow").hide();
            $("#divConfirmData").hide();
            $("#divDone").show();
        }
    });
}

function addCaliber() {
    
    var n = $("#t11").val();
    var c = $("#s4").val();

    $.ajax({
        data: "{ newcal: '" + n + "', std: '" + c + "'}",
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

                $("#s2").find("option").remove().end();
                $("#s2").append("<option>- SELECT -</option>");
                $("#s2").append("<option value=\"-1\">- ADD NEW CALIBER -</option>");

                $.each(response.Caliber, function (i, item) {
                    $("#s2").append("<option value=" + item.CaliberId + ">" + item.CaliberName + "</option>");
                });
                $("#addCal").hide();
                $("#s2").val(si);
                $("#s2").selectpicker("refresh");

            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });

}

function addBullet() {

    var n = $("#t12").val();
    var p = $("#t13").val();

    $.ajax({
        data: "{ bulletType: '" + n + "', code: '" + p + "'}",
        url: "/Inventory/AddBullet",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: "Bullet Type Already Exists",
                        msg: '<b>' + n + '</b> cannot be added because that bullet type already exists',
                        color: '#000000'
                    });
            } else {

                var si = response.SelectedId;

                $("#s3").find("option").remove().end();
                $("#s3").append("<option>- SELECT -</option>");
                $("#s3").append("<option value=\"-1\">- ADD NEW BULLET TYPE -</option>");

                $.each(response.Bullet, function (i, item) {
                    $("#s3").append("<option value=" + item.BulletTypeId + ">" + item.BulletFullName + "</option>");
                });
                $("#addBulletType").hide();
                $("#s3").val(si);
                $("#s3").selectpicker("refresh");

            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });

}

function confirm(v) {

    var errCt = 0;

    var iv = $("#form-ammo").valid();
    if (!iv) { errCt++; }

    if (errCt > 0) {
        Lobibox.alert('error',
            {
                title: "Form Updates Required",
                msg: 'Please correct the form errors to continue',
                color: '#000000'
            });
        return;
    }


    $("#divShadow").hide();
    $("#divConfirmData").show();

    $("#rowBtnAdd").hide();
    $("#rowBtnUpd").hide();
    $("#rowBtnRsk").hide();

    switch(v) {
        case 1:
            $("#rowBtnAdd").show();
            break;
        case 2:
            $("#rowBtnUpd").show();
            break;
    }

    var i1 = parseFloat($("#t2").val()); //price
    var i2 = parseFloat($("#t3").val()); //cost
    var i3 = parseFloat($("#t7").val()); //frt
    var i4 = parseFloat($("#t8").val()); //fees
    var i5 = parseFloat($("#t9").val()); //units ca
    var i6 = parseFloat($("#t30").val()); //units wy

    i1 = isNaN(i1) ? 0.00 : i1;
    i2 = isNaN(i2) ? 0.00 : i2;
    i3 = isNaN(i3) ? 0.00 : i3;
    i4 = isNaN(i4) ? 0.00 : i4;
    i5 = isNaN(i5) ? 0 : i5;
    i6 = isNaN(i6) ? 0 : i6;

    var loc = $("#s18").val();
    if (loc === "2") { i5 = 0; } //WY: NO CA Units
    if (loc === "1") { i6 = 0; } //CA: NO WY Units

    var dt1 = new Date();
    var dd = String(dt1.getDate()).padStart(2, '0');
    var mm = String(dt1.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = dt1.getFullYear();
    var vdt = mm + '/' + dd + '/' + yyyy;
    var aqn = "CA USE ONLY";
    //var aqt = "CA USE ONLY";

    if (i5 > 0) {
        vdt = $("#t18").val();
        //aqn = $("#t16").val();
        //aqt = $("#s6 :selected").text();
    }

    var mfg = $("#s1 :selected").text(); /* manuf */
    var cal = $("#s2 :selected").text(); /* caliber */
    var btp = $("#s3 :selected").text(); /* bul type */
    var web = $("#s5 :selected").text(); /* on web */
    var cat = $("#s9 :selected").text(); /* is slug */
    var svc = $("#s11 :selected").text(); /* trans type */
    var slg = $("#s12 :selected").text(); /* is slug */
    var sct = $("#s9").val(); /* ammo sub cat */
    var ttp = $("#s11").val(); /* trans type */
    var mpn = $("#t1").val();
    var dsc = $("#t4").val();
    var grn = $("#t5").val();
    var rpb = $("#t6").val();
    var chb = $("#t27").val();
    var ssz = $("#t28").val();
    var cus = $("#t35").val();

    if (sct === "9") {
        $("#dvIsBullet").css("display", "none");
        $("#dvIsShot").css("display", "inline-block");

    } else {
        $("#dvIsBullet").css("display", "inline-block");
        $("#dvIsShot").css("display", "none");
    }

    var iow = "NO";
    var isg = slg ? "YES" : "NO";
    var grw = grn + " GR";


    if (parseInt(ttp) > 2) {
        $("#dvCust").css("display", "inline-block");
    } else {
        $("#dvCust").css("display", "none");
        iow = web === "true" ? "YES" : "NO";
    }

 

    $("#sp0").text(svc);
    $("#sp1").text(i1).formatCurrency();
    $("#sp2").text(i2).formatCurrency();
    $("#sp3").text(i3).formatCurrency();
    $("#sp4").text(i4).formatCurrency();
    $("#sp5").text(cat);
    $("#sp6").text(iow);
    $("#sp7").text(cus);
    $("#sp8").text(mfg);
    $("#sp9").text(mpn);
    $("#sp10").text(grw);
    $("#sp11").text(btp);
    $("#sp12").text(isg);
    $("#sp13").text(ssz);
    $("#sp14").text(chb);
    $("#sp15").text(cal);
    $("#sp16").text(rpb);
    $("#sp17").text(i5);
    $("#sp18").text(i6);
    $("#sp19").text(dsc);

    $("#sp20").text(vdt);
    $("#sp21").text(aqn);
    $("#sp22").text(aqt);
}

function setTagSku() {

    var iv = $("#form-ammo").valid();
    if (!iv) { return; }

    var sc = $("#s9 :selected").val(); /* subcategory */
    var tt = $("#s11 :selected").val(); /* transfer type */
    var so = $("#stkId").val();
    var tto = $("#ttp").val();

    if (tt === tto) { /* sku has not changed, keep it original */
        //showConfirm(so);
        $("#divShadow").hide();
        $("#divConfirmData").show();
        $("#divDone").hide();
    } else {
        var cg = 200; /* category */

        var fd = new FormData();
        fd.append("trnTp", tt);
        fd.append("catId", cg);
        fd.append("sctId", sc);

        $.ajax({
            cache: false,
            url: "/Inventory/SetSku",
            type: "POST",
            contentType: false,
            processData: false,
            data: fd,
            success: function (result) {
                //showConfirm(result.sku);
                $("#stkId").val(result.sku);
            },
            error: function (err, data) {
                alert(err);
            },
            complete: function () {
                $("#divShadow").hide();
                $("#divConfirmData").show();
                $("#divDone").hide();
            }
        });
    }
}


function getRestockData() {

    var iv = $("#form-ammo").valid();
    if (!iv) { return; }

    var id = $("#aid").val();
 
    var fd = new FormData();
    fd.append("Aid", id);
 
    $.ajax({
        cache: false,
        url: "/Inventory/AmmoTagRestock",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (result) {
            confirmRestock(result);
 
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            //$("#divShadow").hide();
            $("#divConfirmData").show();
            $("#divDone").hide();
        }
    });
    
}





function confirmRestock(d) {
    
    $("#rowBtnAdd").hide();
    $("#rowBtnUpd").hide();
    $("#rowBtnRsk").show();

    var i1 = parseFloat($("#t3").val()); //cost
    var i2 = parseFloat($("#t7").val()); //frt
    var i3 = parseFloat($("#t8").val()); //fees
    var i4 = parseFloat($("#t9").val()); //units ca
    var i5 = parseFloat($("#t30").val()); //units wy

    i1 = isNaN(i1) ? 0.00 : i1;
    i2 = isNaN(i2) ? 0.00 : i2;
    i3 = isNaN(i3) ? 0.00 : i3;
    i4 = isNaN(i4) ? 0.00 : i4;
    i5 = isNaN(i5) ? 0.00 : i5;

    var dt1 = new Date();
    var dd = String(dt1.getDate()).padStart(2, '0');
    var mm = String(dt1.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = dt1.getFullYear();
    var vdt = mm + '/' + dd + '/' + yyyy;
    var aqn = "CA USE ONLY";
    var aqt = "CA USE ONLY";


    if (i4 > 0) {
        vdt = $("#t18").val();
        //aqn = $("#t16").val();
        //aqt = $("#s6 :selected").text(); 

    }

    var gr = d.GrainWt + " GR";
    var ssz = d.ShotSzWt + " oz";
    var chb = d.Chamber + '"';
    var cus = $("#t35").val();
    var svc = $("#s11 :selected").text(); /* transfer type */
    var ttp = $("#s11").val(); /* svc type */
    var iow = (d.IsOnWeb) ? "YES" : "NO";
    var isg = (d.IsSlug) ? "YES" : "NO";

    if (d.IsShtgn) {
        $("#dvIsShot").css("display", "inline-block");
        $("#dvIsBullet").css("display", "none");
        
    } else {
        $("#dvIsShot").css("display", "none");
        $("#dvIsBullet").css("display", "inline-block");
    }


    if (parseInt(ttp) > 2) {
        $("#dvCust").css("display", "inline-block");
    } else {
        $("#dvCust").css("display", "none");
    }

    $("#sp0").text(svc);
    $("#sp1").text(d.TagPrice).formatCurrency();
    $("#sp2").text(i1).formatCurrency();
    $("#sp3").text(i2).formatCurrency();
    $("#sp4").text(i3).formatCurrency();

    $("#sp5").text(d.TagCat);
    $("#sp6").text(iow);
    $("#sp7").text(cus);

    $("#sp8").text(d.MfgName);
    $("#sp9").text(d.MfgPartNum);
    $("#sp10").text(gr);
    $("#sp11").text(d.BulType);
    $("#sp12").text(isg);
    $("#sp13").text(ssz);
    $("#sp14").text(chb);
    $("#sp15").text(d.Caliber);
    $("#sp16").text(d.RdsPerBx);
    $("#sp17").text(i4);
    $("#sp18").text(i5);
    $("#sp19").text(d.ItemDesc);
    $("#sp20").text(vdt);
    $("#sp21").text(aqn);
    $("#sp22").text(aqt);

}





 

function startOver() {
    $("#divDone").hide();
    $("#divShadow").show();
    $("#dvCnd").show();
    $("#form-ammo")[0].reset();
    $("#s1").selectpicker("refresh");
    $("#s2").selectpicker("refresh");
    $("#s3").selectpicker("refresh");
    $("#CusImg_1").empty();
    $("#delCol").hide();
    $("#dvMakeUpc").show();
}

function showHideActive(v)
{
    if (v === "true") { $("#dvLiveWeb").show(); } else { $("#dvLiveWeb").hide(); }
}



function hideToEdit() {
    $("#divShadow").show();
    $("#divConfirmData").hide();
}




function setShotgun(v) {

    var x = parseInt(v);

    if (x === 9) {
        $("#dvBullet").hide();
        $("#dvShotgun").show();
    } else {
        $("#dvBullet").show();
        $("#dvShotgun").hide();
    }
}

function showCaReq() {
    var v = $("#t9").val();
    var i = parseInt(v);

    if (i > 0) { $("#dvBooks").show(); } else {
        $("#dvBooks").hide();
    }
}


function flushTax(v) {

    if (v === "true") {
        $("#dvTax").show();
    }
    else
    {
        $("#t25").val("0.00");
        $("#dvTax").hide();
    }
 
}

function flushAmmo() {
    $("#form-ammo")[0].reset();
    $("#CusImg_1").empty();
    $("#s1").selectpicker("refresh");
    $("#s2").selectpicker("refresh");
    $("#s3").selectpicker("refresh");
}

function flushRestock() {

    $("#dvAcct").find('input:text').val("");
    $("#dvAcct").find('select').prop('selectedIndex', 0);
    $("#dvBooks").find('input:text').val("");
    $("#dvBooks").find('select').prop('selectedIndex', 0);
}

function setTtp(o) {

    // show all
    $("#s5").prop("selectedIndex", 0);
    $("#s11").prop("selectedIndex", 0);
    $("#s11 > option").each(function () { this.style.display = "block"; });
    $("#dvAddWeb").show();

    var v = parseInt(o);
 

    // show sales
    if (v === 1) {
        $("#dvAddWeb").show();
        $("#taxCustName").hide();
        $("#s11 > option").each(function () { var i = parseInt(this.value); if (i > 102) { this.style.display = "none"; } });
        showPics();
    }

    // show service
    if (v === 2) {
        $("#s5").val("false");
        $("#dvAddWeb").hide();
        $("#taxCustName").show();
        $("#s11 > option").each(function () { var i = parseInt(this.value); if (i === 101 || i === 102) { this.style.display = "none"; } });
        $("#t3").val("0.00");
        hidePics();
    }

}


function showStateOpt(v)
{
    var c = $("#dvCA");
    var w = $("#dvWY");

    switch (v)
    {
        case "1":
            c.show();
            w.hide();
            break;
        case "2":
            c.hide();
            w.show();
            break;
        default:
            c.show();
            w.show();
            break;
    }
}


function cookAmmoTags() {

    var fd = new FormData();
    fd.append("TgMfg", $("#tagMfg").text());
    fd.append("TgCat", $("#tagDsc").text());
    fd.append("TgCal", $("#tagCal").text());
    fd.append("TgTyp", $("#tagTyp").text());
    fd.append("TgRpb", $("#tagRds").text());
    fd.append("TgMpn", $("#tagMpn").text());
    fd.append("TgPrc", $("#tagPrc").text());
    fd.append("TgSku", $("#tagSku").text());
    fd.append("Cnt", $("#s16 option:selected").val());
    fd.append("Sal", $("#s11 option:selected").val());

    $.ajax({
        cache: false,
        url: "/Print/CookAmmoTag",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

        },
        complete: function () {
            //$(ig).empty();
            //$(cl).hide();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function setCostInfo(v) {

    $("#ttp").val("");
    $("#dvTax").hide();
    $("#dvGotTax").hide();
    $("#dvCaPpt").hide();

    switch (v) {
        default:
            showAmmoCost();
            $("#dvUnitCost").show();
            $("#taxCustName").hide();
            $("#dvFees").show();
            break;
        case "102":
            $("#dvCustPd").hide();
            $("#taxCustName").show();
            $("#dvUnitCost").hide();
            $("#dvFees").show();
            break;
        case "103":
            hideAmmoCost();
            $("#dvCaPpt").show();
            $("#dvCustPd").show();
            $("#dvGotTax").show();
            $("#dvUnitCost").hide();
            $("#s42").prop("selectedIndex", 0);
            break;
        case "104":
            $("#dvCustPd").hide();
            $("#dvPrcInfo").hide();
            $("#dvFees").show();
            $("#dvUnitCost").hide();
            break;
        case "105":
            hideAmmoCost();
            $("#dvCustPd").hide();
            $("#dvUnitCost").hide();
            break;
        case "106":
            hideAmmoCost();
            $("#dvCustPd").hide();
            $("#dvUnitCost").hide();
            $("#dvFees").show();
            break;
        case "107":
            hideAmmoCost();
            $("#dvCustPd").hide();
            $("#dvUnitCost").show();
            $("#dvFees").show();
            break;
    }
}

 


function showAmmoCost() {
    $("#dvCustPd").hide();
    $("#dvPrcInfo").show();
    $("#dvFees").show();
}

function hideAmmoCost() {
    $("#taxCustName").show();
    $("#dvPrcInfo").hide();
    $("#dvFees").hide();
}


function showTax()
{
    $("#dvGotTax").show();
    $("#dvTax").show();
}

function hideTax() {
    $("#dvGotTax").hide();
    $("#dvTax").hide();
}


function setAcqSource(v) {

    $("#supSource").hide();
    $("#findFfl").hide();

    var l = parseInt(v);
    switch (l) {
        default:
            $("#supSource").hide();
            break;
        case 1: //FFL COMM
            $("#supSource").hide();
            $("#findFfl").show();
            break;
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
            $("#supSource").show();
            break;
    }
}


function clearFflInfo() {
    $("#s23").prop('selectedIndex', 0);
    $("#t33").val("");
    $("#t34").val("");
    $("#fcd").val("0");
}


function ammoCust() {
    var c = $("#cus").val();
    var x = parseInt(c);
    if (x === 0) { $("#t35").val(""); }
}


function ammoSupp() {
    var c = $("#sup").val();
    var x = parseInt(c);
    if (x === 0) { $("#t52").val(""); }
}


function ammoFfl() {
    var c = $("#fcd").val();
    var x = parseInt(c);
    if (x === 0) { $("#t34").val(""); }
}
 