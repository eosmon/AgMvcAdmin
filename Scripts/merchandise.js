$(function () {
    $("#t18").datepicker({ onSelect: function () { $(this).valid(); } });
});


$(document).ready(function () {
    $("form[data-form-validate='true']").each(function () {

        //$(".selectpicker").selectpicker().change(function () {
        //    $(this).valid();
        //});


        $(this).validate({
            rules: {
                TransCat: { required: true },
                LiveWeb: { required: true },
                Category: { required: true },
                Manufacturer: { required: true },
                ModelName: { required: true },
                UpcCode: { required: true },
                ConditionId: { required: true },
                CaLegal: { required: true },
                AddToWeb: { required: true },
                AskPrice: { required: true },
                ShippingSize: { required: true },
                Pounds: { required: true },
                Description: { required: true },
                UnitsWy: {
                    required: true,
                    greaterThanZero: true
                },
                UnitsCa: {
                    required: true,
                    greaterThanZero: true
                },
                UnitCost: { required: true },
                TransType: { required: true }
            },
            messages: {
                TransCat: "Transaction Group Required",
                LiveWeb: "Live On Web Required",
                Category: "Category Required",
                Manufacturer: "Manufacturer Required",
                ModelName: "Model Name Required",
                UpcCode: "Enter a UPC Code or create from link",
                ConditionId: "Item Condition Required",
                CaLegal: "CA Legal Option Required",
                AddToWeb: "Add To Website Option Required",
                AskPrice: "Asking Price Required",
                ShippingSize: "Shipping Size Required",
                Pounds: "Shipping Pounds Required",
                Description: "Description Required",
                UnitsCa: "California Units Required",
                UnitsWy: "Wyoming Units Required",
                UnitCost: "Unit Cost Required",
                TransType: "Trans Type Required"
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



})(jQuery);




function addMerch() {

    var iv = $("#form-merch").valid();
    if (!iv) { return; }

    var sCatId = $("#s3 :selected").val();
    var sMfgId = $("#s4 :selected").val();
    var sCndId = $("#s5 :selected").val();
    var sTypId = $("#s6 :selected").val();
    var sClrId = $("#s7 :selected").val();
    var sAddWb = $("#s8 :selected").val();
    var isCaOk = $("#s9 :selected").val();
    var shipSz = $("#s10 :selected").val();
    var tOzs = $("#s11 :selected").val();
    var sTax = $("#s12 :selected").val();
    var sAtv = $("#s15 :selected").val();
    var sAcq = $("#s21 :selected").val();

    var tMpn = $("#t3").val();
    var tDes = $("#t4").val();
    var tUpc = $("#t5").val();
    var tUca = $("#t6").val();
    var tLds = $("#t7").val();
    var tPrc = $("#t8").val();
    var tMsr = $("#t31").val();
    var tCst = $("#t9").val();
    var tFrt = $("#t10").val();
    var tFee = $("#t11").val();
    var tMdl = $("#t12").val();
    var tIpb = $("#t14").val(); //ship items per box
    var tLbs = $("#t15").val(); //ship lbs
    var tUwy = $("#t16").val();
    var tWup = $("#t17").val();
    var tDat = $("#t18").val();  // Acq Date
    var tCol = $("#t25").val();
    var tEml = $("#t33").val();
    var tCus = $("#t35").val();

    var cusId = $("#cus").val();
    var cusPd = $("#t47").val();
    var locId = $("#s18").val();
    var supId = $("#sup").val();
    var fflCd = $("#fcd").val();



    var fd = new FormData();
    fd.append("Sct", sCatId);
    fd.append("Mid", sMfgId);
    fd.append("Cnd", sCndId);
    fd.append("Ttp", sTypId);
    fd.append("Col", sClrId);
    fd.append("Box", shipSz);

    fd.append("Cok", isCaOk);
    fd.append("Owb", sAddWb);
    fd.append("Atv", sAtv);
    fd.append("Acq", sAcq);
    fd.append("Mpn", tMpn);
    fd.append("Des", tDes);
    fd.append("Upc", tUpc); 
    fd.append("Wup", tWup);
    fd.append("Uca", tUca);
    fd.append("Uwy", tUwy);

    fd.append("Lbs", tLbs);
    fd.append("Ozs", tOzs);
    fd.append("Ipb", tIpb);

    fd.append("Lds", tLds);
    fd.append("Prc", tPrc);
    fd.append("Msr", tMsr);
    fd.append("Cst", tCst);
    fd.append("Frt", tFrt);
    fd.append("Fee", tFee);
    fd.append("Mdl", tMdl);
    fd.append("Sgt", sTax);
    fd.append("Tcl", tCol);
    fd.append("Cus", tCus);
    fd.append("Eml", tEml);
    fd.append("Adt", tDat);

    fd.append("CusId", cusId);
    fd.append("CusPd", cusPd);
    fd.append("LocId", locId);
    fd.append("SupId", supId);
    fd.append("FflCd", fflCd);

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
        url: "/Inventory/AddMerchandise",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            writeMerchTag(data);
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


function getItemById(v, b) {
    
    $("#cbid").val(v);

    var fd = new FormData();
    fd.append("Id", v);
    fd.append("Sa", b);

    $.ajax({
        cache: false,
        url: "/Inventory/GetMerchById",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            $("#dvBlanket").show();
            $("#dvMerch").show();
            $("#dvMerchAcct").show();
            $("#btnRestock").hide();
            $("#btnUpdate").css("display", "inline-block");
            $("#dvBtnDelete").css("display", "inline-block");
            $("#dvCnd").show();

            var x2 = data.AcctMdl.UnitsCal;
            var x3 = data.AcctMdl.UnitsWyo;
            var x4 = data.AcctMdl.ItemCost;
            var x5 = data.AcctMdl.FreightCost;
            var x6 = data.AcctMdl.ItemFees;
            var x7 = data.AcctMdl.SellerTaxAmount;
            var x10 = data.AcctMdl.AskingPrice.toFixed(2);
            var x11 = data.AcctMdl.SellerCollectedTax;
            var x12 = data.AcctMdl.CustPricePaid.toFixed(2);
            var x13 = data.AcctMdl.CustomerId;
            var x14 = data.AcctMdl.Msrp.toFixed(2);
            var x15 = data.AcctMdl.SupplierId;

            var z1 = data.BookMdl.LocationId;
            var z2 = data.BookMdl.TransTypeId;
            var z3 = data.BookMdl.AcqFflCode;
            var z4 = data.BookMdl.AcqTypeId;
            var z5 = data.BookMdl.AcqFflStateId;
            var z6 = data.BookMdl.AcqName;
            var z7 = data.BookMdl.AcqEmail;
            var z8 = data.BookMdl.CustomerName;
            var z9 = data.BookMdl.StrDateAcq;
            var z10 = data.BookMdl.IsSale;



            var d0 = data.InStockId;
            var d2 = data.SubCategoryId;
            var d3 = data.ManufId;
            var d4 = data.ColorId;
            var d5 = data.CaOkay;
            
            var d7 = data.MfgPartNumber;
            var d8 = data.UpcCode;
            var x1 = data.WebSearchUpc;
            var d9 = data.ItemDesc;
            var d10 = data.LongDesc;
            var d11 = data.ModelName;
            var d12 = data.IsOnWeb;
            var d13 = data.ConditionId;
            var d14 = data.IsActive;
            var d15 = data.Images.PicId;
            var d16 = data.Images.ImgHse1;
            var d17 = data.Images.ImgHse2;
            var d18 = data.Images.ImgHse3;
            var d19 = data.Images.ImgHse4;
            var d20 = data.Images.ImgHse5;
            var d21 = data.Images.ImgHse6;


            var d22 = data.ShippingBoxId;
            var d23 = data.ItemsPerBox;
            var d24 = data.ShippingLbs;
            var d25 = data.ShippingOzs;
            var d26 = data.TagSku;

            var ozs = d25.toFixed(2);

            var d27 = data.Images.Io1;
            var d28 = data.Images.Io2;
            var d29 = data.Images.Io3;
            var d30 = data.Images.Io4;
            var d31 = data.Images.Io5;
            var d32 = data.Images.Io6;

            $("#s6 > option").each(function () { this.style.display = "block"; });
            $("#cus").val(x13);

            setCostInfo(z2.toString());
            flushTax(x11.toString());
            showStateOpt(z1.toString());


            if (z10) {
                $("#s6 > option").each(function () { var i = parseInt(this.value); if (i > 102) { this.style.display = "none"; } });
            }
            else
            {
                $("#s6 > option").each(function () { var i = parseInt(this.value); if (i === 101 || i === 102) { this.style.display = "none"; } });
            }

            if (d12) { showHideActive('true'); } else { showHideActive('false'); }

            var ca = d5 ? "true" : "false";
            var aw = d12 ? "true" : "false";
            var gt = x11 ? "true" : "false";
            var ia = d14 ? "true" : "false";


            $("#s3").val(d2);
            $("#s4").val(d3);
            $("#s5").val(d13);
            $("#s6").val(z2);
            $("#s7").val(d4);
            $("#s8").val(aw);
            $("#s9").val(ca);
            $("#s10").val(d22);
            $("#s11").val(ozs);
            $("#s12").val(gt);
            $("#s15").val(ia);
            $("#s18").val(z1);
            $("#s21").val(z4);
            $("#s23").val(z5);

            $("#t3").val(d7); //mpn
            $("#t4").val(d9); //desc
            $("#t5").val(d8); //upc
            $("#t6").val(x2); //units: ca
            $("#t7").val(d10); //long desc
            $("#t9").val(x4); //cost
            $("#t10").val(x5); //freight
            $("#t11").val(x6); //fees
            $("#t12").val(d11); //model
            $("#t14").val(d23); //ship items/box
            $("#t15").val(d24); //ship lbs
            $("#t16").val(x3); //units: wy
            $("#t17").val(x1); //search upc
            $("#t18").val(z9); //acq date
            $("#t8").val(x10); //price
            $("#s4").selectpicker("refresh");
            $("#t25").val(x7); //tax amt
            $("#t31").val(x14); //MSRP
            $("#t33").val(z7); //FFL Email
            $("#t35").val(z8); //svc customer

            $("#t47").val(x12); //cust price paid
            $("#cus").val(x13); //cust id
            $("#sup").val(x15); //supp id
            $("#fcd").val(z3); // ffl code

            switch (z4) {
                case 1:
                    $("#t34").val(z6);
                    $("#findFfl").show();
                    break;
                default:
                    $("#t52").val(z6);
                    $("#supSource").show();
                    break;
            }

            $('[id^="ImgM_"]').empty();

            if (d16.length > 0) { $("#ImgM_1").append("<img src='" + d16 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_1").attr("orig-img", d27); } else { $("#delCol_1").hide(); }
            if (d17.length > 0) { $("#ImgM_2").append("<img src='" + d17 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_2").attr("orig-img", d28); } else { $("#delCol_2").hide(); }
            if (d18.length > 0) { $("#ImgM_3").append("<img src='" + d18 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_3").attr("orig-img", d29); } else { $("#delCol_3").hide(); }
            if (d19.length > 0) { $("#ImgM_4").append("<img src='" + d19 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_4").attr("orig-img", d30); } else { $("#delCol_4").hide(); }
            if (d20.length > 0) { $("#ImgM_5").append("<img src='" + d20 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_5").attr("orig-img", d31); } else { $("#delCol_5").hide(); }
            if (d21.length > 0) { $("#ImgM_6").append("<img src='" + d21 + "' alt='' data-id='" + d15 + "' />"); $("#ImgHse_6").attr("orig-img", d32); } else { $("#delCol_6").hide(); }

            $("#s4").selectpicker("refresh");

            $("#isi").val(d0);
            $("#ttp").val(z2);
            $("#stkId").val(d26);

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#rowBtnUpd").show();
        }
    });
}


function flushTax(v) {

    if (v === "true") {
        $("#dvTax").show();
    }
    else {
        $("#t25").val("0.00");
        $("#dvTax").hide();
    }

}


function writeMerchTag(d) {

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
    $("#tagCat").text(d.Category);
    $("#tagDsc").text(d.ItemDesc);
    $("#tagCnd").text(d.Condition);
    $("#tagMpn").text(d.MfgPartNum);
    $("#tagSku").text(d.TagSku);
    $("#tagSvc").text(d.SvcType);
    $("#tagSvc").css("color", "blue");
    $("#brcTxt").text(d.TagSku);
    $("#tagPrc").removeAttr("style");

    if (d.IsSale) {
        $("#tagService").hide();
        $("#tagOurPrc").show();
        $("#tagPrc").css("padding-top", "15px");
        $("#tagOurPrc").css("padding-top", "15px");
        $("#tagPrc").text(d.TagPrice).formatCurrency();
    } else {
        $("#tagService").show();
        $("#tagOurPrc").hide();
        $("#tagPrc").text(d.SvcName);
        $("#tagPrc").css("width", "200px");
        $("#tagPrc").css("font-weight", "800");
    }
}

function updateMerch() {

    var iv = $("#form-merch").valid();
    if (!iv) { return; }

    var isi = $("#isi").val();
    var cbi = $("#cbid").val();
    var sku = $("#stkId").val();
    var mid = $("#mchId").val();

    var cat = $("#s3 :selected").val(); // Catg ID
    var mfg = $("#s4 :selected").val(); // Mfg ID
    var cnd = $("#s5 :selected").val(); // Cnd ID
    var ttp = $("#s6 :selected").val(); // Trans Type ID
    var col = $("#s7 :selected").val(); // Color
    var web = $("#s8 :selected").val(); // Add Web
    var ica = $("#s9 :selected").val(); // CA Ok
    var box = $("#s10 :selected").val(); // Box Size
    var ozs = $("#s11 :selected").val(); // Wgt Ozs
    var sgt = $("#s12 :selected").val(); // Seller Coll Tax
    var atv = $("#s15 :selected").val(); // Live On Website
    var acq = $("#s21 :selected").val(); // Supplier Acq Type

    var mpn = $("#t3").val();
    var des = $("#t4").val();
    var upc = $("#t5").val();
    var uca = $("#t6").val();
    var lds = $("#t7").val();
    var prc = $("#t8").val();
    var msr = $("#t31").val();
    var cst = $("#t9").val();
    var frt = $("#t10").val();
    var fee = $("#t11").val();
    var mdl = $("#t12").val();
    var upb = $("#t14").val(); //ship items per box
    var lbs = $("#t15").val(); //ship lbs
    var uwy = $("#t16").val();
    var wsu = $("#t17").val(); // web search upc
    var dat = $("#t18").val();  // Acq Date
    var tax = $("#t25").val(); // sales tax collected
    var eml = $("#t33").val(); // email
    var cus = $("#t35").val(); // customer

    var loc = $("#s18").val();
    var cusId = $("#cus").val();
    var supId = $("#sup").val();
    var fflCd = $("#fcd").val();
    var cusPd = $("#t47").val();

    if (loc === "1") { uwy = 0; }
    if (loc === "2") { uca = 0; }


    var fd = new FormData();
    fd.append("Isi", isi);
    fd.append("Cbi", cbi);
    fd.append("Sct", cat);
    fd.append("Mfg", mfg);
    fd.append("Cnd", cnd);
    fd.append("Ttp", ttp);
    fd.append("Col", col);
    fd.append("Box", box);

    fd.append("Cok", ica);
    fd.append("Owb", web);
    fd.append("Mpn", mpn);
    fd.append("Des", des);
    fd.append("Upc", upc);
    fd.append("Wsu", wsu);
    fd.append("Uca", uca);
    fd.append("Uwy", uwy);
    fd.append("Atv", atv);
    
    fd.append("Lbs", lbs);
    fd.append("Ozs", ozs);
    fd.append("Ipb", upb);

    fd.append("Lds", lds);
    fd.append("Prc", prc);
    fd.append("Msr", msr);
    fd.append("Cst", cst);
    fd.append("Frt", frt);
    fd.append("Fee", fee);
    fd.append("Mdl", mdl);
    fd.append("Sgt", sgt);
    fd.append("Tcl", tax);
    fd.append("Sku", sku);
    fd.append("Cus", cus);
    fd.append("Adt", dat);
    fd.append("Eml", eml);

    fd.append("CusId", cusId);
    fd.append("SupId", supId);
    fd.append("FflCd", fflCd);
    fd.append("CusPd", cusPd);
    fd.append("AcqTp", acq);
    fd.append("LocId", loc);


    $("input[id^=ImgHse_]").each(function () {
        var fileId = $(this).attr("id");
        var oImg = $("#"+fileId).attr("orig-img");
        var f = document.getElementById(fileId).files;
        if (f.length > 0) {
            fd.append(fileId, f[0]);
            fd.append('GroupId', fileId);
            fd.append('OrigImg', oImg);
        }
    });

    $.ajax({
        cache: false,
        url: "/Inventory/UpdateMerchItem",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            writeMerchTag(data);
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

function deleteItem(v) {

    Lobibox.confirm({
        title: "Delete This Item?",
        msg: "You are about to permanently delete this item. This action cannot be undone - continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {

                var fd = new FormData();
                fd.append("MstId", v);

                $.ajax({
                    cache: false,
                    url: "/Inventory/NixMerchandise",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {
                        return false;
                    },
                    complete: function () {
                        loadMerchandise();
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}

function restockMerch() {

    var iv = $("#form-merch").valid();
    if (!iv) { return; }

    var mid = $("#mchId").val();
    var ttp = $("#s6 :selected").val(); // Trans Type ID
    var sgt = $("#s12 :selected").val(); // Seller Coll Tax
    var acq = $("#s21 :selected").val(); // Supplier Acq Type

    var uca = $("#t6").val(); //ca units
    var uwy = $("#t16").val();//wy units
    var cst = $("#t9").val(); //cost
    var frt = $("#t10").val(); //freight
    var fee = $("#t11").val(); //fees
    var dat = $("#t18").val();  // Acq Date
    var cpd = $("#t47").val(); //cust transfer price
    var tax = $("#t25").val(); // sales tax collected
    var eml = $("#t33").val(); // email
    var cus = $("#t35").val(); // cust name
    var loc = $("#s18").val(); // loc id
    var cid = $("#cus").val(); // cust id
    var sid = $("#sup").val(); // supp id
    var fcd = $("#fcd").val(); // ffl code

 

    var isOwb = $("#sp6").text();
    var iow = isOwb === "YES" ? true : false;

    var fd = new FormData();
    fd.append("Mid", mid);
    fd.append("Ttp", ttp);
    fd.append("Uca", uca);
    fd.append("UWy", uwy);
    fd.append("Cst", cst);
    fd.append("Cid", cid);
    fd.append("Sid", sid);
    fd.append("Fcd", fcd);
    fd.append("Acq", acq);
    fd.append("Cpd", cpd);
    fd.append("Frt", frt);
    fd.append("Fee", fee);
    fd.append("Tax", tax);
    fd.append("Sgt", sgt);
    fd.append("Cus", cus);
    fd.append("Iow", iow);
    fd.append("Loc", loc);
    fd.append("Adt", dat);
    fd.append("Eml", eml);

 


    $.ajax({
        cache: false,
        url: "/Inventory/RestockMerch",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            writeMerchTag(data);
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


//function getInvById(v) {

//    if (v === "") {
//        $("#btnUpdate").hide();
//        $("#btnRestock").hide();
//        $("#dvBtnDelete").hide();
//        $("#dvMerchAcct").hide();
//        return;
//    }

//    $("#stkId").val(v);

//    flushAcct();

//    var fd = new FormData();
//    fd.append("Id", v);

//    $.ajax({
//        cache: false,
//        url: "/Inventory/GetMerchAcct",
//        type: "POST",
//        contentType: false,
//        processData: false,
//        data: fd,
//        success: function (data) {

//            var d1 = data.TransTypeId;
//            var d2 = data.ConditionId;
//            var d3 = data.Units;

//            var d4 = data.AcctMdl.ItemCost.toFixed(2);
//            var d5 = data.AcctMdl.FreightCost.toFixed(2);
//            var d6 = data.AcctMdl.ItemFees.toFixed(2);
 
//            $("#s5").val(d2); //cond
//            $("#s6").val(d1); //trans type
//            $("#t6").val(d3); //units
//            $("#t9").val(d4); //cost
//            $("#t10").val(d5); //freight
//            $("#t11").val(d6); //fees
 

//        },
//        error: function (err, data) {
//            alert(err);
//        },
//        complete: function () {
//            $("#btnUpdate").css("display", "inline-block");
//            $("#dvBtnDelete").css("display", "inline-block");
//            $("#dvMerchAcct").show();
//        }
//    });
//}



 

function runAtn(el, id, ifs) {

    var opt = parseInt(el.value);
    $("#mchId").val(id);

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



function showAtn(opt) {

    hideAllPanels();
    $("#dvBlanket").show();

    switch (opt) {
    case "":
        $("#dvBlanket").hide();
        return;
    case "1":
        $("#dvMerchTtl").text("Update Merchandise Entry");
        $("#dvMerch").show();
        $("#pnlCmpR").hide();
        $("#pnlCmpL").css("width", "100%");
        $("#rowBtnUpd").show();
        break;
    case "3":
        $("#dvMerchAcct").show();
        $("#btnRestock").css("display", "inline-block");
        $("#btnCancel").css("display", "inline-block");
        break;
    case "4":
            setMerch();
        break;
    }
}


function loadMerchandise() {

    var mfgId = $("#mid").val();
    var catId = $("#catId").val();

    var fd = new FormData();
    fd.append("MfgId", mfgId);
    fd.append("CatId", catId);

    return $.ajax({
        cache: false,
        url: "/Inventory/GetMerchandise",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            //setNavCounts(data.Count);

            $("#merchRows").empty();

            var rc = "#FFF7E5";

            $.each(data, function (i, item) {

                var id = item.Id;
                var uCa = item.UnitsCa;
                var uWy = item.UnitsWy;
                var rsk = item.Restocks;
                var img = item.ImageUrl;
                var mpn = item.MfgPartNumber;
                var upc = item.UpcCode;
                var mfg = item.ManufName;
                var sct = item.SubCatName;
                var dsc = item.ItemDesc;
                var ifs = item.IsForSale;
                var owb = item.IsOnWeb;

                var ow = "No";
                var fs = "No";
                if (owb) { ow = "Yes"; }
                if (ifs) { fs = "Yes"; }

                var opt = ifs ? "Purchase(s)" : "Service(s)";

                var au = upc.length > 0 ? ' UPC: ' + upc : '';
                var nds = dsc + ', MFG# ' + mpn + au;

                var imgUl = img.length > 0 ? "<img src='" + img + "' class='gun-img' alt=''>" : "";



                var block = "<div data-id='" + id + "' class='ammo-row' style='color:black;background-color:" + rc + "'>";
                block += "<div class='ammo-bdr' style='padding-top:14px'><div class='gun-p1' style='text-align:center'><select id='atn" + id + "' onchange='runAtn(this, " + id + ", " + ifs + ")'  style='border-radius:4px; padding:3px'><option value='0'>-SELECT-</option><option value='1'>Update</option><option value='2'>Restock</option></select></div></div>";
                block += "<div class='ammo-bdr' style='padding-top:14px'><div class='gun-p1' style='text-align:center'><div class='hide-it' id='hit-" + id + "'><select id='cb" + id + "'  style='border-radius:4px; padding:3px; display:none' onchange='getItemById(this.value," + ifs + ");'></select></div><span id='al" + id + "'>" + rsk + " " + opt + "</span></div></div>";
                block += "<div class='ammo-bdr'><div class='ammo-img-row'>" + imgUl + "</div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1'><div><b style='color:blue'>" + mfg + "</b></div><div>" + nds + "</div><div><b>" + sct + "</b></div></div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + fs + "</div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + ow + "</div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + uCa + "</div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + uWy + "</div></div>";
                block += "</div>";


                $('#merchRows').append(block);

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

function addMerchManuf() {

    var n = $("#t2").val();
    var s = $("#s4");
 

    $.ajax({
        data: "{ newMfg: '" + n + "', sectId: '2'}",
        url: "/Inventory/AddOtherManuf",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: "Manufacturer Already Exists",
                        msg: '<b>' + n + '</b> cannot be added because that manufacturer already exists',
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


function addColor() {

    var n = $("#t13").val();
    var s = $("#s7");


    $.ajax({
        data: "{ color: '" + n + "'}",
        url: "/Inventory/AddColor",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: "Color Already Exists",
                        msg: '<b>' + n + '</b> cannot be added because that color already exists',
                        color: '#000000'
                    });
            } else {


                var si = response.SelectedId;

                $(s).find("option").remove().end();
                s.append("<option>- SELECT -</option>");
                s.append("<option value=\"-1\">- ADD NEW COLOR -</option>");

                $.each(response.Color, function (i, item) {
                    s.append("<option value=" + item.ColorId + ">" + item.ColorName + "</option>");
                });
                $("#addColor").hide();
                s.val(si);

            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });

}


function setTagSku() {

    var iv = $("#form-merch").valid();
    if (!iv) { return; }

    var sc = $("#s3 :selected").val(); /* subcategory */
    var tt = $("#s6 :selected").val(); /* transfer type */
    var so = $("#stkId").val();
    var tto = $("#ttp").val();

    if (tt === tto) { /* sku has not changed, keep it original */
        confirmData(so);
        $("#divShadow").hide();
        $("#divConfirmData").show();
        $("#divDone").hide();
    } else {
        var cg = 300; /* category */

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
                confirmData(result.sku);
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






function confirmData(sku) {

    var z1 = parseInt($("#t6").val());
    var z2 = parseFloat($("#t8").val());
    var z3 = parseFloat($("#t9").val());
    var z4 = parseFloat($("#t10").val());
    var z5 = parseFloat($("#t11").val());

    var cat = $("#s3 :selected").text();
    var mfg = $("#s4 :selected").text();
    var cnd = $("#s5 :selected").text();

    var mpn = $("#t3").val();
    var dsc = $("#t4").val();
    var upc = $("#t5").val();
    var lds = $("#t7").val();

    z1 = isNaN(z1) ? 0 : z1;
    z2 = isNaN(z2) ? 0.00 : z2;
    z3 = isNaN(z3) ? 0.00 : z3;
    z4 = isNaN(z4) ? 0.00 : z4;
    z5 = isNaN(z5) ? 0.00 : z5;


    $("#sp1").text(z2).formatCurrency();
    $("#sp2").text(z3).formatCurrency();
    $("#sp3").text(z4).formatCurrency();
    $("#sp4").text(z5).formatCurrency();
    $("#sp5").text(cat);
    $("#sp6").text(mfg);
    $("#sp7").text(mpn);
    $("#sp8").text(upc);
    $("#sp9").text(cnd);
    $("#sp10").text(z1);
    $("#sp11").text(dsc);
    $("#sp12").text(lds);

    if (mfg.length === 0) { $("#tagMfg").hide(); } else { $("#tagMfg").show(); }
    if (cat.length === 0) { $("#tagLineCat").hide(); } else { $("#tagLineCat").show(); }
    if (dsc.length === 0) { $("#tagLineDsc").hide(); } else { $("#tagLineDsc").show(); }
    if (cnd.length === 0) { $("#tagLineCond").hide(); } else { $("#tagLineCond").show(); }
    if (mpn.length === 0) { $("#tagLineMpn").hide(); } else { $("#tagLineMpn").show(); }

    var nMpn = mpn;
    var nDes = dsc;

    if (mpn.length > 20) { nMpn = mpn.substring(0, 25) + "..."; }
    if (dsc.length > 60) { nDes = dsc.substring(0, 40) + "..."; }

    var fz = setFontSz(mfg);

    $("#tagMfg").css("font-size", fz);

    $("#tagMfg").text(mfg);
    $("#tagCat").text(cat);
    $("#tagDsc").text(nDes);
    $("#tagCnd").text(cnd);
    $("#tagMpn").text(nMpn);
    $("#tagSku").text(sku);
    $("#brcTxt").text(sku);

    $("#tagPrc").text(z2).formatCurrency();

}

function setFontSz(l) {

    var ml = l.length;
    var r = '';

    if (ml < 6) { r = "22px"; }
    else if (ml > 5 && ml < 9) { r = "20px"; }
    else if (ml > 8 && ml < 12) { r = "16px"; }
    else if (ml > 11 && ml < 19) { r = "13px"; }
    else if (ml > 18 && ml < 41) { r = "10px"; }
    else if (ml >= 41) { r = "9px"; }

    return r;
}


function getPurchases(v) {

    var sp = $("#al" + v);
    var mu = $("#cb" + v);

    $.ajax({
        data: "{ itemId: '" + v + "', catId: '300'}",
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
            $("#dvMerchTtl").text("Create Merchandise Entry");

        }
    });
}



function startOver() {
    $("#divDone").hide();
    $("#divShadow").show();
    $("#dvCnd").show();
    $("#form-merch")[0].reset();
    $("#s4").selectpicker("refresh");
    $("div[id^=ImgM_]").empty();
    $("span[id^=delCol_]").hide();
    $("input[id^=ImgHse_]").empty();
    $("#spCreate").hide();
}


function hideAllPanels() {

    $("#btnAddNew").hide();
    $("#btnUpdate").hide();
    $("#dvBtnDelete").hide();
    $("#btnRestock").hide();
    $("#dvMerchAcct").hide();
    $("#dvMerch").hide();

    $("#rowBtnAdd").hide();
    $("#rowBtnUpd").hide();
}

function hideToEdit() {
    $("#divShadow").show();
    $("#divConfirmData").hide();
}


function resetMerchandise() {
    $("#divDone").hide();
    $("#divShadow").show();
    $("#form-other-stuff")[0].reset();
    $("#s3").selectpicker("refresh");
}


//function wipeImg(i) {

//    Lobibox.confirm({
//        title: "Delete Image!",
//        msg: "Permanently delete this image? This action cannot be undone",
//        modal: true,
//        callback: function (lobibox, type) {
//            if (type === 'no') {
//                return;
//            } else {

//                $('[id^="ImgHse_"]').val('');

//                var ig = $("#ImgM_" + i);
//                var cl = $("#delCol_" + i);
//                var ih = $("#ImgHse_" + i); 
//                //var row = $(ig).find("img").attr("data-id");
//                //var img = $(ig).find("img").attr("src");
//                var img = $(ih).attr("orig-img");
//                var isi = $("#isi").val();

//                var fd = new FormData();
//                fd.append("Id", isi);
//                fd.append("Idx", i);
//                fd.append("Sect", 6);
//                fd.append("Img", img);

//                $.ajax({
//                    cache: false,
//                    url: "/Inventory/NixImage",
//                    type: "POST",
//                    contentType: false,
//                    processData: false,
//                    data: fd,
//                    success: function (d) {

//                        $('[id^="ImgM_"]').empty();

//                        if (d.ImgHse1.length > 0) { $("#ImgM_1").append("<img src='" + d.ImgHse1 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_1").attr("orig-img", d.Io1); } else { $("#delCol_1").hide(); }
//                        if (d.ImgHse2.length > 0) { $("#ImgM_2").append("<img src='" + d.ImgHse2 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_2").attr("orig-img", d.Io2); } else { $("#delCol_2").hide(); }
//                        if (d.ImgHse3.length > 0) { $("#ImgM_3").append("<img src='" + d.ImgHse3 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_3").attr("orig-img", d.Io3); } else { $("#delCol_3").hide(); }
//                        if (d.ImgHse4.length > 0) { $("#ImgM_4").append("<img src='" + d.ImgHse4 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_4").attr("orig-img", d.Io4); } else { $("#delCol_4").hide(); }
//                        if (d.ImgHse5.length > 0) { $("#ImgM_5").append("<img src='" + d.ImgHse5 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_5").attr("orig-img", d.Io5); } else { $("#delCol_5").hide(); }
//                        if (d.ImgHse6.length > 0) { $("#ImgM_6").append("<img src='" + d.ImgHse6 + "' alt='' data-id='" + isi + "' />"); $("#ImgHse_6").attr("orig-img", d.Io6); } else { $("#delCol_6").hide(); }
//                    },
//                    complete: function () {
//                    },
//                    error: function (err, data) {
//                        alert("Status : " + data.responseText);
//                    }
//                });
//            }
//        }
//    });
//}

function flushForm() {
    $("#form-merch")[0].reset();
}

function flushAcct() {

    $("#dvMerchAcct").find('input:text').val("");
    $("#dvMerchAcct").find('select').prop('selectedIndex', 0);
}

function showToAdd(item) {


    $("#addColor").hide();

    switch (item) {
    case 1:
        var v1 = $("#s4").val();
        if (v1 === "-1") {
            $("#t2").val("");
            $("#addMfg").show();
        }
        break;
    case 4:
        var v4 = $("#s7").val();
        if (v4 === "-1") {
            $("#t13").val("");
            $("#addColor").show();
        }
        break;
    }

}

function showTax(v) {
    if (v === "false") {
        $("#dvTax").hide();
        $("#t25").val("0.00");
    } else {
        $("#dvTax").show();
    }
}

function setManuf(v) {
    $("#mid").val(v);
    loadMerchandise();
}


function setSubCat(v) {
    $("#catId").val(v);
    loadMerchandise();
}

function showRestock(v, ifs) {

    flushRestock();

    $("#mchId").val(v);

    $("#dvTax").hide();
    $("#dvGotTax").hide();

    $("#dvBlanket").show();
    $("#dvMerchAcct").show();
    $("#btnRestock").show();

    $("#dvCnd").hide();
    $("#btnUpdate").hide();
    $("#dvBtnDelete").hide();

    $("#dvRestockLeft").hide();
    $("#dvRestockRight").hide();
    $("#dvPics").hide();
    $("#dvMerchTtl").text("Restock Merchandise Entry");

    $("#s6").prop("selectedIndex", 0);
    $("#s6 > option").each(function () { this.style.display = "block"; });

    if (ifs) {
        $("#s6 > option").each(function () {
            var i = parseInt(this.value);
            if (i > 102) { this.style.display = "none"; }
            $("#taxCustName").hide();
        });
    } else {
        $("#s6 > option").each(function () {
            var i = parseInt(this.value);
            if (i === 101 || i === 102) { this.style.display = "none"; }
            $("#taxCustName").show();
        });
    }
}

function confirm() {

    var iv = $("#form-merch").valid();
    if (!iv) { return; }

    var id = $("#mchId").val();

    var fd = new FormData();
    fd.append("Mid", id);

    $.ajax({
        cache: false,
        url: "/Inventory/MerchTagRestock",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            showRestockConfirm(data);

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

function showRestockConfirm(d) {

    $("#rowBtnAdd").hide();
    $("#rowBtnUpd").hide();
    $("#rowBtnRsk").show();

    var i1 = parseFloat($("#t9").val()); //units ca
    var i2 = parseFloat($("#t16").val()); //units wy
    var i3 = parseFloat($("#t9").val()); //cost
    var i4 = parseFloat($("#t10").val()); //frt
    var i5 = parseFloat($("#t11").val()); //fees

    i1 = isNaN(i1) ? 0 : i1;
    i2 = isNaN(i2) ? 0 : i2;
    i3 = isNaN(i3) ? 0.00 : i3;
    i4 = isNaN(i4) ? 0.00 : i4;
    i5 = isNaN(i5) ? 0.00 : i5;

 
    var cus = $("#t35").val();
    var svc = $("#s6 :selected").text(); /* transfer type */

    var iow = (d.IsOnWeb) ? "YES" : "NO";

    if (d.IsSale) { $("#dvCust").css("display", "none"); } else { $("#dvCust").css("display", "inline-block"); }

    $("#sp1").text(d.TagPrice).formatCurrency();
    $("#sp2").text(i3).formatCurrency();
    $("#sp3").text(i4).formatCurrency();
    $("#sp4").text(i5).formatCurrency();

    $("#sp5").text(svc);
    $("#sp6").text(iow);
    $("#sp7").text(cus);
    $("#sp8").text(d.Category);
    $("#sp9").text(d.MfgName);
    $("#sp10").text(d.MfgPartNum);
    $("#sp11").text(d.UpcCode);
    $("#sp12").text(d.Condition);
    $("#sp13").text(i1);
    $("#sp14").text(i2);
    $("#sp15").text(d.ItemDesc);
    $("#sp16").text(d.LongDesc);
}


function flushRestock() {

    $("#dvMerchAcct").find('input:text').val("");
    $("#dvMerchAcct").find('select').prop('selectedIndex', 0);
}

function setTransType(v) {
    setMerch();
    setTtp(v);
}

function setMerch() {

    hideAllPanels();
    $("#dvBlanket").show();

    startOver();
    $("#dvMerchTtl").text("Create Merchandise Entry");
    $("#dvMerch").show();
    $("#dvMerchAcct").show();
    $("#btnAddNew").css("display", "inline-block");
    $("#btnCancel").css("display", "inline-block");
    $("#rowBtnAdd").show();

    $("#dvRestockLeft").show();
    $("#dvRestockRight").show();
    $("#dvPics").show();


}



function setTtp(o) {

    // show all
    $("#s8").prop("selectedIndex", 0);
    $("#s6").prop("selectedIndex", 0);
    $("#s6 > option").each(function () { this.style.display = "block"; });
    $("#dvAddWeb").show();

    var v = parseInt(o);


    // show sales
    if (v === 1) {
        $("#dvAddWeb").show();
        $("#taxCustName").hide();
        $("#s6 > option").each(function () { var i = parseInt(this.value); if (i > 102) { this.style.display = "none"; } });
        showPics();
    }

    // show service
    if (v === 2) {
        $("#s8").val("false");
        $("#dvAddWeb").hide();
        $("#taxCustName").show();
        $("#s6 > option").each(function () { var i = parseInt(this.value); if (i === 101 || i === 102) { this.style.display = "none"; } });
        $("#t8").val("0.00");
        hidePics();
    }

}

function verifyMerch(v) {

    var errCt = 0;

    var iv = $("#form-merch").valid();
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

    switch (v) {
    case 1:
        $("#rowBtnAdd").show();
        break;
    case 2:
        $("#rowBtnUpd").show();
        break;
    }

    var i1 = parseFloat($("#t8").val()); //price
    var i2 = parseFloat($("#t9").val()); //cost
    var i3 = parseFloat($("#t10").val()); //frt
    var i4 = parseFloat($("#t11").val()); //fees
    var i5 = parseFloat($("#t6").val()); //units ca
    var i6 = parseFloat($("#t16").val()); //units wy

    i1 = isNaN(i1) ? 0.00 : i1;
    i2 = isNaN(i2) ? 0.00 : i2;
    i3 = isNaN(i3) ? 0.00 : i3;
    i4 = isNaN(i4) ? 0.00 : i4;
    i5 = isNaN(i5) ? 0 : i5;
    i6 = isNaN(i6) ? 0 : i6;

    var loc = $("#s18").val();
    if (loc === "2") { i5 = 0; } //WY: NO CA Units
    if (loc === "1") { i6 = 0; } //CA: NO WY Units

    var cat = $("#s3 :selected").text(); 
    var mfg = $("#s4 :selected").text(); /* manuf */
    var cnd = $("#s5 :selected").text(); /* cond */
    var ttp = $("#s6 :selected").text(); /* trans type */
    var web = $("#s8 :selected").text(); /* on web */
    var sal = $("#s6").val(); /* is sale */

    var mpn = $("#t3").val();
    var dsc = $("#t4").val();
    var upc = $("#t5").val();
    //var uca = $("#t6").val();
    var lds = $("#t7").val();
    //var uwy = $("#t16").val();
    var cus = $("#t35").val();

    if (parseInt(sal) > 2) { $("#dvCust").css("display", "inline-block"); } else { $("#dvCust").css("display", "none"); }

    $("#sp1").text(i1).formatCurrency();
    $("#sp2").text(i2).formatCurrency();
    $("#sp3").text(i3).formatCurrency();
    $("#sp4").text(i4).formatCurrency();
    $("#sp5").text(ttp);
    $("#sp6").text(web);
    $("#sp7").text(cus);
    $("#sp8").text(cat);
    $("#sp9").text(mfg);
    $("#sp10").text(mpn);
    $("#sp11").text(upc);
    $("#sp12").text(cnd);
    $("#sp13").text(i5);
    $("#sp14").text(i6);
    $("#sp15").text(dsc);
    $("#sp16").text(lds);

}


function cookMerchTags() {

    var fd = new FormData();
    fd.append("TgMfg", $("#tagMfg").text());
    fd.append("TgCat", $("#tagCat").text());
    fd.append("TgDsc", $("#tagDsc").text());
    fd.append("TgCnd", $("#tagCnd").text());
    fd.append("TgMpn", $("#tagMpn").text());
    fd.append("TgSvc", $("#tagSvc").text());
    fd.append("TgPrc", $("#tagPrc").text());
    fd.append("TgSku", $("#tagSku").text());
    fd.append("Cnt", $("#s14 option:selected").val());
    fd.append("Sal", $("#s6 option:selected").val());

    $.ajax({
        cache: false,
        url: "/Print/CookMerchTag",
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

    switch (v) {
        default:
            showMerchCost();
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
            hideMerchCost();
            $("#dvCustPd").show();
            $("#dvGotTax").show();
            $("#dvUnitCost").hide();
            break;
        case "104":
            showMerchCost();
            $("#dvUnitCost").hide();
            break;
        case "105":
            hideMerchCost();
            $("#dvCustPd").hide();
            $("#dvUnitCost").hide();
            break;
        case "106":
            hideMerchCost();
            $("#dvCustPd").hide();
            $("#dvUnitCost").hide();
            $("#dvFees").show();
            break;
        case "107":
            hideMerchCost();
            $("#dvCustPd").hide();
            $("#dvUnitCost").show();
            $("#dvFees").show();
            break;
    }
}





function showMerchCost() {
    $("#dvCustPd").hide();
    $("#dvPrcInfo").show();
    $("#dvCostInfo").show();
}

function hideMerchCost() {
    $("#taxCustName").show();
    $("#dvPrcInfo").hide();
    $("#dvFees").hide();
}


function showStateOpt(v) {
    var c = $("#dvCA");
    var w = $("#dvWY");

    switch (v) {
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

function merchCust() {
    var c = $("#cus").val();
    var x = parseInt(c);
    if (x === 0) { $("#t35").val(""); }
}


function merchSupp() {
    var c = $("#sup").val();
    var x = parseInt(c);
    if (x === 0) { $("#t52").val(""); }
}


function merchFfl() {
    var c = $("#fcd").val();
    var x = parseInt(c);
    if (x === 0) { $("#t34").val(""); }
}



