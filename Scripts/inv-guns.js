/* VALIDATE FORM SECTIONS HERE */
$(document).ready(function () {

    $("form[name='form-locTrans']").validate({
        rules: {
            Location: { required: true },
            TransType: { required: true }
        },
        messages: {
            Location: "Location Required",
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

    $("form[name='form-gunBasics']").validate({
        rules: {
            ActionId: { required: true },
            AddToWeb: { required: true },
            Barrel: { required: true },
            Capacity: { required: true },
            FinishId: { required: true },
            Description: { required: true }
        },
        messages: {
            ActionId: "Gun Action Required",
            AddToWeb: "Add To Website: Required",
            Barrel: "Barrel Length Required",
            Capacity: "Gun Capacity Required",
            FinishId: "Finish Required",
            Description: "Description Required"
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

    $("form[name='form-gunSpecs']").validate({
        rules: {
            ConditionId: { required: true },
            MfgPartNumber: { required: true },
            Model: { required: true },
            UpcCode: { required: true },
            WeightLb: { required: true },
            WebSrchUpc: { required: true },
        },
        messages: {
            ConditionId: "Gun Condition Required",
            MfgPartNumber: "Mfg Part Number Required",
            Model: "Gun Model Required",
            UpcCode: "Upc Code Required",
            WeightLb: "Gun Weight (Lb) Required",
            WebSrchUpc: "Web Search Upc Code Required"
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

    $("form[name='form-acct']").validate({
        rules: {
            CustName: { required: true },
            AskPrice: { required: true },
            Cost: { required: true },
            OldSku: { required: true },
            UseOldSku: { required: true }
        },
        messages: {
            CustName: "Customer Name Required",
            AskPrice: "Asking Price Required",
            Cost: "Cost Required",
            OldSku: "Old Sku Required",
            UseOldSku: "Select Old Sku Required"
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

    $("form[name='form-calif']").validate({
        rules: {
            CaPptSale: { required: true },
            PptSeller: { required: true }
        },
        messages: {
            CaPptSale: "CA PPT Sale Required",
            PptSeller: "CA PPT Seller Required"
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

    $("form[name='form-book']").validate({
        rules: {
            AcqDate: { required: true },
            AcqSellerSrc: { required: true },
            AcqFflSrc: { required: true },
            FflWhse: { required: true },
            FflState: { required: true },
            FflName: { required: true },
            SerialNumber: { required: true },
            Manufacturer: { required: true },
            BookModel: { required: true },
            GunType: { required: true },
            Caliber: { required: true }
 
        },
        messages: {
            AcqDate: "Acquisition Date Required",
            AcqSellerSrc: "Acquisition Seller: Selection Required",
            AcqFflSrc: "FFL Source Required",
            FflWhse: "FFL Warehouse Required",
            FflState: "FFL State Required",
            FflName: "FFL Name Required",
            SerialNumber: "Serial Number Required",
            Manufacturer: "Manufacturer Required",
            BookModel: "Model Required",
            GunType: "Gun Type Required",
            Caliber: "Caliber Required"
      
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
        $("#t29").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Inventory/FflByName",
                    data: "{ search: '" + request.term + "', state: '" + $("#fs").val() + "' }",
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

                var flc = ui.item.value.FflFullLic;
                var exp = ui.item.value.FflExpires;
                var lof = ui.item.value.FflOnFile;
                var vld = ui.item.value.FflIsValid;
                var onf = lof ? "Yes" : "No";

                $("#fcd").val(fc);

                if (!vld) { exp = exp + " (EXPIRED)"; }

                //setFflTxt(n, a, c, fn);
                var txt = n + ' ' + a + ' ' + c + ' ' + fn;
                $("#t29").val(txt);

                setConfirmation(n, flc, exp, onf, id);
            }
        });
    });


    // Autocomplete Existing Gun Search
    $(function () {
        $("#t1").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Inventory/FindExistingGun",
                    data: "{ mfg: '" + $("#s3").val() + "', typ: '" + $("#s4").val() + "', cal: '" + $("#s5").val() + "', atn: '" + $("#s6").val() + "', cok: '" + $("#s24").val() + "', str: '" + $("#t1").val() + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        response($.map(data,
                            function (item) {

                                var lbl = "<img src=\"" + item.GunImgUrl + "\" class=\"exist-gun\" />" + item.Description;

                                return { label: lbl, value: item };
                            }));
                    },
                    complete: function () {
                        $("body").scrollTop($(document).height());
                        //$("#ui-id-2").append("<li class=\"ui-menu-item\"><a class=\"ui-menu-item-wrapper\">ABC 123</a></li>");
                    }
                });
            },
            open: function (event, ui) {
                var $input = $(event.target),
                    $results = $input.autocomplete("widget"),
                    top = $results.position().top,
                    height = $results.height(),
                    inputHeight = $input.height(),
                    newTop = top - height - inputHeight,
                    xTop = newTop < 432 ? newTop : 432;

                $(".ui-widget-content").css("max-height", "500px");

                $results.css("top", (xTop) + "px");

            },
            focus: function (event, ui) {
                this.value = "";
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();

                var desc = ui.item.value.Description;
                var txt = $(desc).text();
                $("#t1").val(txt);

                var mfg = ui.item.value.ManufId;
                var mod = ui.item.value.ModelName;
                var cal = ui.item.value.CaliberId;
                var typ = ui.item.value.GunTypeId;
                var mid = ui.item.value.MasterId;
                var isi = ui.item.value.InStockId;
                var iow = ui.item.value.IsOnWeb;

                showWebSearch();
                setBookEntry(mfg, mod, cal, typ);
                GetGunSpecsFromWeb(mid, isi, iow);
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

                            var t = "<div style=\"width:100%; text-align:center\"><div style=\"padding-bottom:3px\">No Matches Found</div><div style=\"border-top:solid 1px white; padding-top:3px\"><a class=\"ui-Link\" onclick=\"newCustomer()\">Create New Customer</a></div></div>";

                            var result = [{ label: t, value: response.term }];
                            return response(result);

                        }
                        else {
                            response($.map(data, function (item) {

                                var reg = item.IsReg ? "Registered User" : "Inquiry Only";
                                var iDiv = item.ProfilePic.length > 0 ? "<img src=\"" + item.ProfilePic + "\" class=\"pic-cust\" />" : "<div style='text-align:center'>Customer Photo Not Found</div>";

                                var d = "<div style=\"display:inline-block; vertical-align:top\">" + iDiv;
                                d += "<div style='vertical-align:top;display: inline-block; vertical-align: middle; padding-left: 10px; font-size:13px !important'>";
                                d += "<div class=\"cust-ui-name\">" + item.StrFullName + "</div>";
                                d += "<div><b>" + item.StrFullAddr + "</b></div>";
                                d += "<div>" + item.StrEmailPhn + "</div>";
                                d += "<div>" + item.StrCustType + " Customer</div>";
                                d += "<div><span>" + reg + "</span><span style=\"padding-left:15px\" class=\"link11Green\" href=\"#\" onclick=\"editCustomer('" + item.CustomerId + "')\">Update</span></div>";
                                d += "</div>";
                                d += "</div>";

                                return { label: d, value: item };
                            }));

                        }
                    },
                    complete: function () {
                        $("body").scrollTop($(document).height());
                        $("#ui-id-3").append("<li class=\"ui-menu-item\" style=\"text-align:center !important; height:30px\"></li>");
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


    // Autocomplete Filter Guns By Customer
    $(function () {
        $("#t48").autocomplete({
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
                $("#sci").val(id);
                $("#t48").val(fn);
                filterGuns();
            }
        });
    });




})(jQuery);




function valCus()
{
    var v = $("#t48").val();
    var vl = v.length;

    if (vl === 0)
    {
        $("#sci").val("");
        filterGuns();
    }
}


function setTransType(v) {

    $("#s1").prop("selectedIndex", 0);

    $("#ifs").val(v);
    resetGun(v);
    $("#dvDataSrc").show();   
}

function resetGun(v)
{
    restartCreate();
    $("#dvGunTtl").text("Create Gun Entry");

    $("#s32 > option").each(function () { this.style.display = "block"; });

    if (v === "true")
    {
        $("#s32 > option").each(function () { var i = parseInt(this.value); if (i > 102) { this.style.display = "none"; } });
    }
    else
    {
        $("#s32 > option").each(function () { var i = parseInt(this.value); if (i === 101 || i === 102) { this.style.display = "none"; } });
    }
}


function getGunById(v, b) {

    $("#cbid").val(v);

    var fd = new FormData();
    fd.append("Id", v);
    fd.append("Sa", b);

    $.ajax({
        cache: false,
        url: "/Inventory/GetGunById",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {

            var d0 = d.Images.PicId;
            var d1 = d.Images.ImgHse1;
            var d2 = d.Images.ImgHse2;
            var d3 = d.Images.ImgHse3;
            var d4 = d.Images.ImgHse4;
            var d5 = d.Images.ImgHse5;
            var d6 = d.Images.ImgHse6;

            var d7 = d.Images.Io1;
            var d8 = d.Images.Io2;
            var d9 = d.Images.Io3;
            var d10 = d.Images.Io4;
            var d11 = d.Images.Io5;
            var d12 = d.Images.Io6;

            var d13 = d.Images.DistCode;

            var loca = d.Accounting.LocationId;
            var msrp = d.Accounting.Msrp.toFixed(2);
            var fees = d.Accounting.ItemFees.toFixed(2);
            var cost = d.Accounting.ItemCost.toFixed(2);
            var frgt = d.Accounting.FreightCost.toFixed(2);
            var sTax = d.Accounting.SellerTaxAmount.toFixed(2);
            var aPrc = d.Accounting.AskingPrice.toFixed(2);
            var sPrc = d.Accounting.SalePrice.toFixed(2);
            var cPrc = d.Accounting.CustPricePaid.toFixed(2);
            var sCtx = d.Accounting.SellerCollectedTax;
            var iSal = d.Accounting.IsForSale;
            var trTp = d.Accounting.TransTypeId;
            var svNm = d.Accounting.SvcCustName;

            var lMak = d.Compliance.LockMakeId;
            var lMod = d.Compliance.LockModelId;
            var hcCt = d.Compliance.HiCapMagCount;
            var mCap = d.Compliance.HiCapCapacity;
            var cOky = d.Compliance.CaOkay;
            var cHid = d.Compliance.CaHide;
            var cHgn = d.Compliance.HoldGun;
            var cPpt = d.Compliance.CaPptOk;
            var cCur = d.Compliance.CaCurioOk;
            var cRos = d.Compliance.CaRosterOk;
            var cSga = d.Compliance.CaSglActnOk;
            var cSst = d.Compliance.CaSglShotOk;
            var cflc = d.Compliance.CflcInbound;
            var hExp = d.Compliance.StrHoldExp;
            var iPpt = d.Compliance.IsActualPpt;

            var isId = d.Gun.InStockId;
            var atId = d.Gun.ActionId;
            var fnId = d.Gun.FinishId;
            var cdId = d.Gun.ConditionId;
            var capa = d.Gun.CapacityInt;
            var wLbs = d.Gun.WeightLb;
            var gtId = d.Gun.GunTypeId;
            var wOzs = d.Gun.WeightOz;
            var bDec = d.Gun.BarrelDec;
            var cDec = d.Gun.ChamberDec;
            var oDec = d.Gun.OverallDec;
            var iHid = d.Gun.IsHidden;
            var iAtv = d.Gun.IsActive;
            var used = d.Gun.IsUsed;
            var oWeb = d.Gun.IsOnWeb;
            var iWbs = d.Gun.IsWebBased;
            var iVer = d.Gun.IsVerified;
            var oBox = d.Gun.OrigBox;
            var cMdl = d.Gun.IsCurModel;
            var oPpw = d.Gun.OrigPaperwork;
            var gUpc = d.Gun.UpcCode;
            var desc = d.Gun.Description;
            var wUpc = d.Gun.WebSearchUpc;
            var mfPn = d.Gun.MfgPartNumber;
            var lDsc = d.Gun.LongDescription;
            var trId = d.Gun.TransId;
            var tgMf = d.Gun.ManufName;
            var tgMd = d.Gun.ModelName;
            var tgCl = d.Gun.CaliberTitle;
            var tgSn = d.Gun.SerialNumber;
            var tgGt = d.Gun.GunType;
            var ioSk = d.Gun.IsOldSku;
            var oSku = d.Gun.OldSku;

            var cusId = d.CustomerId;
            var selId = d.SellerId;
            

            showUpdate(loca, iWbs);

            getLockModels(lMak, lMod);

            var b1Mn = oWeb ? "true" : "false";
            var b2Mn = sCtx ? "true" : "false";
            var b3Mn = ioSk ? "true" : "false";
            var b4Mn = iPpt ? "true" : "false";

            $("#s32 > option").each(function () { this.style.display = "block"; });

            if (iSal) {
                $("#dvAddWeb").show();
                $("#dvPrcInfo").show();
                $("#dvUnitCost").show();
                $("#dvCustPd").hide();
                $("#s32 > option").each(function() { var i = parseInt(this.value); if (i > 102) { this.style.display = "none"; }
                });

                switch (trTp)
                {
                    case 101:
                        $("#taxCustName").hide();
                        break;
                    case 102:
                        $("#taxCustName").show(); //CONSIGNMENT 
                        break;
                }

            } else {
                $("#dvAddWeb").hide();
                $("#dvPrcInfo").hide();
                $("#taxCustName").show();
                $("#s32 > option").each(function () { var i = parseInt(this.value); if (i === 101 || i === 102) { this.style.display = "none"; } });

                if (trTp === 103) {  //TRANSFER 
                    $("#dvCustPd").show();
                    $("#dvCaPpt").show();
                    $("#s42").prop("disabled", true);
                }
                else
                {
                    $("#dvCaPpt").hide();
                    $("#s42").prop("selectedIndex", 0);
                    $("#s42").prop("disabled", false);
                }

                switch (trTp) {
                    default:
                        $("#dvUnitCost").hide();
                        break;
                    case 4:
                    case 6:
                    case 7:
                        $("#dvUnitCost").show();
                        break;
                }


            }


 

            //old sku
            if (ioSk) { $("#dvOldSku").show(); } else { $("#dvOldSku").hide(); }
 
            $("#t35").val(svNm);

            $("#s2").val(loca);
            $("#s11").val(cdId);
            $("#s12").val(fnId);
            $("#s14").val(b1Mn);
            $("#s15").val(b2Mn);
            $("#s19").val(wLbs);
            $("#s20").val(wOzs);
            $("#s29").val(lMak);
            $("#s30").val(lMod);
            $("#s32").val(trTp);
            $("#s33").val(atId);
            $("#s41").val(b3Mn);
            $("#s42").val(b4Mn);

            $("#t5").val(capa);
            $("#t6").val(gUpc);
            $("#t7").val(mfPn);
            $("#t9").val(tgMd);
            $("#t10").val(desc);
            $("#t11").val(lDsc);

            $("#t12").val(cflc);
            $("#t13").val(hcCt);
            $("#t14").val(sTax);
            $("#t15").val(hExp);
            $("#t16").val(aPrc);
            $("#t17").val(msrp);
            $("#t18").val(sPrc);
            $("#t19").val(cost);
            $("#t20").val(frgt);
            $("#t21").val(fees);

            $("#t35").val(svNm);
            $("#t40").val(mCap);
            $("#t42").val(bDec);
            $("#t43").val(oDec);
            $("#t44").val(cDec);
            $("#t45").val(wUpc);
            $("#t46").val(oSku);
            $("#t47").val(cPrc);
         
            $("#c1").prop('checked', cMdl);
            $("#c2").prop('checked', used);
            $("#c3").prop('checked', iHid);
            $("#c4").prop('checked', iAtv);
            $("#c5").prop('checked', iVer);
            $("#c6").prop('checked', oBox);
            $("#c7").prop('checked', oPpw);
            $("#c8").prop('checked', cHid);
            $("#c9").prop('checked', cOky);

            $("#c10").prop('checked', cRos);
            $("#c11").prop('checked', cSga);
            $("#c12").prop('checked', cCur);
            $("#c13").prop('checked', cSst);
            $("#c14").prop('checked', cPpt);
            $("#c15").prop('checked', cHgn);

            $('[id^="ImgM_"]').empty();

            if (d1.length > 0) { $("#ImgM_1").append("<img src='" + d1 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_1").attr("orig-img", d7); } else { $("#delCol_1").hide(); }
            if (d2.length > 0) { $("#ImgM_2").append("<img src='" + d2 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_2").attr("orig-img", d8); } else { $("#delCol_2").hide(); }
            if (d3.length > 0) { $("#ImgM_3").append("<img src='" + d3 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_3").attr("orig-img", d9); } else { $("#delCol_3").hide(); }
            if (d4.length > 0) { $("#ImgM_4").append("<img src='" + d4 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_4").attr("orig-img", d10); } else { $("#delCol_4").hide(); }
            if (d5.length > 0) { $("#ImgM_5").append("<img src='" + d5 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_5").attr("orig-img", d11); } else { $("#delCol_5").hide(); }
            if (d6.length > 0) { $("#ImgM_6").append("<img src='" + d6 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_6").attr("orig-img", d12); } else { $("#delCol_6").hide(); }


            $("#s29").selectpicker("refresh");
            $("#s30").selectpicker("refresh");

            $("#locId").val(loca);
            $("#gtpId").val(gtId);
            $("#stkId").val(trId);
            $("#ttpId").val(trTp);
            $("#ifs").val(iSal);
            $("#isi").val(isId);
            $("#cus").val(cusId);
            $("#pts").val(selId);
            
            $("#tagMf").val(tgMf);
            $("#tagMd").val(tgMd);
            $("#tagCl").val(tgCl);
            $("#tagSn").val(tgSn);
            $("#tagGt").val(tgGt);
            
            if (oWeb) { showWebOpts(); } else { hideWebOpts(); }

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#rowBtnUpd").show();
            $("#addEdit").val("Update");
        }
    });
}



function runGunAtn(el, id, ifs) {

    restartPanels();
    var opt = parseInt(el.value);
    $("#gunId").val(id);


    $("span[id^='al']").show();
    //var al = "#al" + id;

    switch (opt) {
    case 0:
        var sp = $("#al" + id);
        var mu = $("#cb" + id);
        $(sp).show();
        $(mu).hide();
        return;
    case 1:
        getGunPurchases(id);
        $("#rowBtnUpd").show();
        break;
    case 2:
        gunRestock(id, ifs);
        $("select[id^='cb']").hide();
        break;
    case 3:
        //flushAmmo();
        break;
    case 4:
        $("#rowBtnAdd").show();
        break;
    }

    el.selectedIndex = 0;

    var cb = "#hit-" + id;
    $(".hide-it").show();
    $(".hide-it").not(cb).hide();
    $("select[id^='cb']").prop("selectedIndex", 0);
 
}


function getGunPurchases(v) {

    var sp = $("#al" + v);
    var mu = $("#cb" + v);

    $.ajax({
        data: "{ itemId: '" + v + "', catId: '100'}",
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

        }
    });
}


function showPanel() {
    restartPanels();
    $("#divSrchHdr").show();

   /*** 11/22/21: THIS IS FOR THE GUN SHOW ONLY - REMOVE AFTER!!!! ***/
    $("#dvInvGrp").show();
    $("#dvDataSrc").show();
    $("#s2").val("1");
    $("#s13").val("1");
}


function confirm() {

    var errCt = 0;

    var iv = $("#form-locTrans").valid();
    if (!iv) { errCt++; }

    iv = $("#form-gunBasics").valid();
    if (!iv) { errCt++; }

    iv = $("#form-gunSpecs").valid();
    if (!iv) { errCt++; }

    iv = $("#form-acct").valid();
    if (!iv) { errCt++; }

    iv = $("#form-book").valid();
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

    var id = $("#gunId").val();
    var lo = $("#s2").val();

    var ttp = $("#s32").val();
    var sv = parseInt(ttp);
    var isa = sv < 103;

    var fd = new FormData();
    fd.append("Id", id);
    fd.append("Lo", id);
    fd.append("Sa", isa);

    $.ajax({
        cache: false,
        url: "/Inventory/GunTagRestock",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            restockConfirm(data);

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


function restockConfirm(d) {
    
    $("#rowBtnAdd").hide();
    $("#rowBtnUpd").hide();
    $("#rowBtnRsk").show();

    var i3 = parseFloat($("#t13").val()); //hi cap unit ct
    var i4 = parseFloat($("#t19").val()); //cost
    var i5 = parseFloat($("#t20").val()); //frght
    var i6 = parseFloat($("#t21").val()); //fees
    var i7 = parseFloat($("#t14").val()); //tax coll

    var i10 = parseFloat($("#t16").val()); //ask prc
    var i11 = parseFloat($("#t17").val()); //msrp
    var i12 = parseFloat($("#t18").val()); //sale prc


    i3 = isNaN(i3) ? 0.0 : i3;
    i4 = isNaN(i4) ? 0.00 : i4;
    i5 = isNaN(i5) ? 0.00 : i5;
    i6 = isNaN(i6) ? 0.00 : i6;
    i7 = isNaN(i7) ? 0 : i7;

    i10 = isNaN(i10) ? 0.00 : i10;
    i11 = isNaN(i11) ? 0.00 : i11;
    i12 = isNaN(i12) ? 0.00 : i12;

    var i8 = isNaN(d.Capacity) ? 0 : i8;
    var i9 = isNaN(d.MagCount) ? 0 : i9;

    var loc = $("#s2").val(); //loc id
    var cus = $("#t35").val();
    var svc = $("#s32 :selected").text(); /* transfer type */
    var lmk = $("#s29").prop("selectedIndex") === 0 ? "" : $("#s29 :selected").text(); /* lock make */
    var lmd = $("#s30").prop("selectedIndex") === 0 ? "" : $("#s30 :selected").text(); /* lock model */

    var c1 = $("#c10").prop("checked"); //roster
    var c2 = $("#c11").prop("checked"); //sgl-atn
    var c3 = $("#c12").prop("checked"); //curio
    var c4 = $("#c13").prop("checked"); //sgl-shot
    var c5 = $("#c14").prop("checked"); //pvt-party


    var iow = (d.IsOnWeb) ? "YES" : "NO";
    var atv = (d.IsActive) ? "YES" : "NO";
    var ver = (d.IsVerified) ? "YES" : "NO";
    var hid = (d.IsHidden) ? "YES" : "NO";
    var hca = (d.IsHideCa) ? "YES" : "NO";
    var usd = (d.IsUsed) ? "YES" : "NO";
    var icr = (d.IsCurrent) ? "YES" : "NO";
    var cok = (d.IsCaOk ) ? "YES" : "NO";
    var rst = c1 ? "YES" : "NO";
    var san = c2 ? "YES" : "NO";
    var cur = c3 ? "YES" : "NO";
    var sst = c4 ? "YES" : "NO";
    var pvt = c5 ? "YES" : "NO";

    if (loc === "1") { $("#dvVerfCa").css("display", "inline-block"); } else { $("#dvVerfCa").css("display", "none"); }


    var aqs = $("#s21").val(); /* acq src */
    var cfl = $("#t12").val();
    var hxp = $("#t15").val();
    var ser = $("#t22").val();

    var supTxt = $("#t52").val();
    var supNam = "";

    if (supTxt != null) {
        var supIdx = supTxt.indexOf(":");
        supNam = supTxt.substring(0, supIdx);
        $("#sp41").text(supNam);

    }


    $("#sp1").text(i10).formatCurrency();
    $("#sp2").text(i11).formatCurrency();
    $("#sp3").text(i12).formatCurrency();
    $("#sp4").text(i4).formatCurrency();
    $("#sp5").text(i5).formatCurrency();
    $("#sp6").text(i6).formatCurrency();

    $("#sp7").text(svc);
    $("#sp8").text(iow);
    $("#sp9").text(cus);

    $("#sp10").text(d.Model);
    $("#sp11").text(d.Condition);
    $("#sp12").text(d.Finish);
    $("#sp13").text(d.Action);
    $("#sp14").text(i8);

    $("#sp15").text(icr);
    $("#sp16").text(usd);
    $("#sp17").text(hid);
    $("#sp18").text(atv);
    $("#sp19").text(ver);

    $("#sp20").text(cfl);
    $("#sp21").text(i9);
    $("#sp22").text(i7);
    $("#sp23").text(hxp);
    $("#sp24").text(lmk);
    $("#sp25").text(lmd);

    $("#sp26").text(hca);
    $("#sp27").text(cok);
    $("#sp28").text(rst);
    $("#sp29").text(san);
    $("#sp30").text(cur);
    $("#sp31").text(sst);
    $("#sp32").text(pvt);

    $("#sp42").text(d.MfgName);
    $("#sp43").text(d.Importer);
    $("#sp44").text(d.BookModel);
    $("#sp45").text(ser);
    $("#sp46").text(d.GunType);
    $("#sp47").text(d.Caliber);

    switch (aqs) {
    default: //COMM FFL
        $("#fromFfl").show();
        $("#fromPublic").hide();
        break;

    case "2": //03 C&R FFL
        $("#sp36").text(cfn);
        $("#sp37").text(cfb);
        $("#sp38").text(cex);
        $("#fromFfl").show();
        $("#fromPublic").hide();
        sel = "FFL";
        break;
    case "3":
        $("#fromFfl").hide();
        $("#fromPublic").show();
        sel = "PRIVATE PARTY";
        break;
    case "4":
        $("#fromFfl").hide();
        $("#fromPublic").show();
        sel = "POLICE";
        break;
    case "5":
        $("#fromFfl").hide();
        $("#fromPublic").show();
        sel = "OTHER ORGANIZATION";
        break;
    case "6":
        $("#sp28").text("Nick Nelson" + " - " + "FROM OWNER'S PERSONAL COLLECTION");
        $("#fromFfl").hide();
        $("#fromPublic").show();
        sel = "OWNER'S COLLECTION";
        break;
    }


}


/* MASTER DATA POST */
function writeData() {

    var v10 = $("#s7 :selected").text();   // gun mfg
    var v11 = $("#s8")[0].selectedIndex === 0 ? "" : $("#s8 :selected").text();   // gun importer
    var v12 = $("#t8").val();   // gun model
    var v13 = $("#t22").val();   // serial 
    var v14 = $("#s9 :selected").text(); // gun type 
    var v15 = $("#s10 :selected").text(); // caliber
    var v16 = $("#t12").val(); // cflc
    var v17 = $("#addEdit").val(); // addEdit
    var v18 = $("#t6").val();    // UPC Code
    var v19 = $("#t45").val();   // Web UPC
    var v20 = $("#t7").val();    // Mfg Part Number
    var v21 = $("#t10").val();   // Desc
    var v22 = $("#t11").val();   // Long Desc
    var v23 = $("#t9").val();    // Model
    var v24 = $("#t46").val();   // Old Sku
    var v25 = $("#t35").val();   // Tag Customer Name
    var v26 = $("#t51").val();   // Email Address

    var i10 = $("#mstId").val(); // masterid
    var i11 = $("#s2").val();    // locationId
    var i12 = $("#s9").val();    // gunTypeId
    var i13 = $("#s32").val();   // TransTypeId
    var i14 = $("#s21").val();   // AcqTypeId
    var i15 = $("#fcd").val();   // FFLCode
    var i16 = $("#stkId").val(); // InStockId
    var i17 = $("#s7").val();    // ManufId
    var i18 = $("#s8").val();    // ImporterId
    var i19 = $("#s10").val();   // CaliberId    
    var i20 = $("#s33").val();   // ActionId
    var i21 = $("#s12").val();   // FinishId
    var i22 = $("#s11").val();   // ConditionId
    var i23 = $("#s19").val();   // WeightLb
    var i24 = $("#t5").val();    // Mag Capacity
    var i25 = $("#s29").val();   // Lock Make
    var i26 = $("#s30").val();   // Lock Model
    var i27 = $("#t13").val();   // Hi Capacity Mag Count
    var i28 = $("#t40").val();   // Hi-Cap Capacity
    var i29 = $("#s24").val();   // FFL Source ID
    var i30 = $("#cus").val();   // CustomerID
    var i31 = $("#sup").val();   // SupplierID

    var d10 = $("#t19").val();  // cost
    var d11 = $("#t20").val();  // freight
    var d12 = $("#t21").val();  // fees
    var d13 = $("#t14").val();  // seller tax paid
    var d14 = $("#t42").val();  // barrel dec
    var d15 = $("#t43").val();  // overall dec
    var d16 = $("#t44").val();  // chamber dec
    var d17 = $("#s20").val();  // weight oz
    var d18 = $("#t16").val();  // price
    var d19 = $("#t17").val();  // msrp
    var d20 = $("#t18").val();  // sale price
    var d21 = $("#t47").val();  // customer paid seller cost

    var b10 = $("#s14").val();  // on web
    var b11 = $("#iwb").val();  // web based
    var b12 = $("#s15").val();  // seller collected tax
    var b13 = $("#c1").prop("checked");  // current model
    var b14 = $("#c2").prop("checked");    // used
    var b15 = $("#c3").prop("checked");    // hide
    var b16 = $("#c4").prop("checked");  // active
    var b17 = $("#c5").prop("checked");    // verified
    var b18 = $("#c6").prop("checked"); // orig box
    var b19 = $("#c7").prop("checked");  // has paperwork
    var b20 = $("#c8").prop("checked");  // hide CA  
    var b21 = $("#c9").prop("checked"); // CA legal
    var b22 = $("#c10").prop("checked");   // CA roster
    var b23 = $("#c12").prop("checked");   // curio relic
    var b24 = $("#c11").prop("checked");    // single atn revolver
    var b25 = $("#c13").prop("checked");    // single shot pistol
    var b26 = $("#c14").prop("checked");  // ca ppt
    var b27 = $("#s42").val(); // actual ppt
    var b28 = $("#s41").val();  // old sku
    var b29 = $("#c15").prop("checked"); // hold for 30 days

    var dt1 = $("#t15").val();  // hold expire date
    var dt2 = $("#t41").val();  // acq date

    var fd = new FormData();

    /* IMG BLOCK FROM INQUIRY - NOT UPLOADS */
    var i1 = "";
    var i2 = "";
    var i3 = "";
    var i4 = "";
    var i5 = "";
    var i6 = "";

    var ig1 = $("#ImgG_1").find("img").attr("src");
    var ig2 = $("#ImgG_2").find("img").attr("src");
    var ig3 = $("#ImgG_3").find("img").attr("src");
    var ig4 = $("#ImgG_4").find("img").attr("src");
    var ig5 = $("#ImgG_5").find("img").attr("src");
    var ig6 = $("#ImgG_6").find("img").attr("src");

    var ids = "HSE";

    /* IMG WITH 'SPIDER' IS DIST BASED */
    switch (v17) {
        case "Create":
        case "InStock":
            i1 = ig1 == null ? "" : getImgName(ig1);
            i2 = ig2 == null ? "" : getImgName(ig2);
            i3 = ig3 == null ? "" : getImgName(ig3);
            i4 = ig4 == null ? "" : getImgName(ig4);
            i5 = ig5 == null ? "" : getImgName(ig5);
            i6 = ig6 == null ? "" : getImgName(ig6);
            break;
        case "AddUnit":
            i1 = ig1 == null ? "" : getWebImg(ig1);
            break;
    }

    /** UPDATES FOR NEW IMAGE UPLOADS **/
    if (b11 === "true")
    {
        i1 = $("#igOnePic").attr("web-img");
        ids = $("#igOnePic").attr("dist");
    }

    fd.append("GunMfg", v10);
    fd.append("GunImp", v11);
    fd.append("GunMdl", v12);
    fd.append("Serial", v13);
    fd.append("GunTyp", v14);
    fd.append("Calibr", v15);
    fd.append("Cflchk", v16);
    fd.append("UpcCod", v18);
    fd.append("WebUpc", v19);
    fd.append("MfgNum", v20);
    fd.append("Descrp", v21);
    fd.append("LngDes", v22);
    fd.append("GModel", v23);
    fd.append("OldSku", v24);
    fd.append("TgCus", v25);
    fd.append("BkEmail", v26);
    fd.append("ImgGn1", i1);
    fd.append("ImgGn2", i2);
    fd.append("ImgGn3", i3);
    fd.append("ImgGn4", i4);
    fd.append("ImgGn5", i5);
    fd.append("ImgGn6", i6);
    fd.append("ImgDst", ids);

    fd.append("MstrId", i10);
    fd.append("LocaId", i11);
    fd.append("GnTpId", i12);
    fd.append("TrTpId", i13);
    fd.append("AcqTyp", i14);
    fd.append("FlCode", i15);
    fd.append("IstkId", i16);
    fd.append("ManfId", i17);
    fd.append("ImptId", i18);
    fd.append("ClbrId", i19);
    fd.append("ActnId", i20);
    fd.append("FinhId", i21);
    fd.append("CondId", i22);
    fd.append("WgtLbs", i23);
    fd.append("MagCap", i24);
    fd.append("LokMak", i25);
    fd.append("LokMod", i26);
    fd.append("HiCpCt", i27);
    fd.append("HiCpCp", i28);
    fd.append("FlScId", i29);
    fd.append("CustId", i30);
    fd.append("SuppId", i31);

    fd.append("GunCst", d10);
    fd.append("Freigt", d11);
    fd.append("GunFee", d12);
    fd.append("TaxAmt", d13);
    fd.append("BrlDec", d14);
    fd.append("OvrDec", d15);
    fd.append("ChbDec", d16);
    fd.append("WgtOzs", d17);
    fd.append("PrcAsk", d18);
    fd.append("PrcMsr", d19);
    fd.append("PrcSal", d20);
    fd.append("CusPrc", d21);

    fd.append("IsOnWb", b10);
    fd.append("IsWbsd", b11);
    fd.append("GotTax", b12);
    fd.append("CurMdl", b13);
    fd.append("IsUsed", b14);
    fd.append("HideGn", b15);
    fd.append("Active", b16);
    fd.append("Verifd", b17);
    fd.append("OrgBox", b18);
    fd.append("HasPpw", b19);
    fd.append("CaHide", b20);
    fd.append("CaLegl", b21);
    fd.append("CaRost", b22);
    fd.append("CaCuro", b23);
    fd.append("CaSaRv", b24);
    fd.append("CaSsPt", b25);
    fd.append("CaPptr", b26);
    fd.append("ActPpt", b27);
    fd.append("IsOlSk", b28);
    fd.append("Hold30", b29);

    fd.append("HldExp", dt1);
    fd.append("AcqDat", dt2);

    fd.append("AddEdt", addEdit);

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
        url: '/Inventory/CreateGunEntry',
        type: 'POST',
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fd,
        success: function (data) {
            writeGunTag(data);
        },
        error: function (data, error) {
            alert("Status : " + data.responseText);
        },
        complete: function () {
            $("#divShadow").hide();
            $("#divConfirmData").hide();
            $("#divDone").show();
        }

    });
}


function updateGun() {

    var isId = $("#isi").val();
    var cbId = $("#cbid").val();
    var tSku = $("#stkId").val();
    var iFos = $("#ifs").val();

    //var txEp = $("#s31 :selected").val();
    var cost = $("#t19").val();
    var frgt = $("#t20").val();
    var fees = $("#t21").val();
    //var sCtx = $("#c16").prop("checked");
    var sCtx = $("#s15 :selected").val();
    var sTax = $("#t14").val();
    var aPrc = $("#t16").val();
    var msrp = $("#t17").val();
    var sPrc = $("#t18").val();

    var lMak = $("#s29 :selected").val();
    var lMod = $("#s30 :selected").val();
    var hcCt = $("#t13").val();
    var mCap = $("#t40").val();
    var cOky = $("#c9").prop("checked");
    var cHid = $("#c8").prop("checked");
    var cHgn = $("#c15").prop("checked");
    var cPpt = $("#c14").prop("checked");
    var cCur = $("#c12").prop("checked");
    var cRos = $("#c10").prop("checked");
    var cSga = $("#c11").prop("checked");
    var cSst = $("#c13").prop("checked");
    var cflc = $("#t12").val();
    var hExp = $("#t15").val();

    var ttId = $("#s32 :selected").val();
    var atId = $("#s33 :selected").val();
    var fnId = $("#s12 :selected").val();
    var cdId = $("#s11 :selected").val();
    var capa = $("#t5").val();
    var wLbs = $("#s19 :selected").val();
    var wOzs = $("#s20 :selected").val();
    var bDec = $("#t42").val();
    var cDec = $("#t44").val();
    var oDec = $("#t43").val();
    var iHid = $("#c3").prop("checked");
    var iAtv = $("#c4").prop("checked");
    var used = $("#c2").prop("checked");
    var oWeb = $("#s14 :selected").val();
    var iVer = $("#c5").prop("checked");
    var oBox = $("#c6").prop("checked");
    var cMdl = $("#c1").prop("checked");
    var oPpw = $("#c7").prop("checked");
    var gUpc = $("#t6").val();
    var desc = $("#t10").val();
    var wUpc = $("#t45").val();
    var modl = $("#t9").val();
    var mfPn = $("#t7").val();
    var lDsc = $("#t11").val();
    var tCus = $("#t35").val();
    var iOsk = $("#s41").val();
    var oSku = $("#t46").val();
    var cPrc = $("#t47").val();

    var cuId = $("#cus").val();
    var slId = $("#pts").val();
    var iPpt = $("#s42").val();

    var tPrt = $("#chkTag").prop("checked");
    var tMfg = $("#tagMfg").text();
    var tCal = $("#tagCal").text();
    var tTyp = $("#tagAct").text();
    var tBrl = $("#tagBrl").text();
    var tCnd = $("#tagCnd").text();
    var tCap = $("#tagCap").text();

    var fd = new FormData();
    fd.append("IskId", isId);
    fd.append("CbsId", cbId); 
    fd.append("TtpId", ttId);
    fd.append("CusId", cuId);
    fd.append("SelId", slId);
    fd.append("ICost", cost);
    fd.append("Frght", frgt);
    fd.append("IFees", fees);
    fd.append("ScTax", sCtx);
    fd.append("TaxAm", sTax);
    fd.append("Price", aPrc);
    fd.append("PMsrp", msrp);
    fd.append("PSale", sPrc);
    fd.append("CuPrc", cPrc);
    fd.append("IfSal", iFos);
    fd.append("IsPpt", iPpt);

    fd.append("LMake", lMak);
    fd.append("LModl", lMod);
    fd.append("HcpCt", hcCt);
    fd.append("MCapa", mCap);
    fd.append("CaOky", cOky);
    fd.append("CaHid", cHid);
    fd.append("HoldG", cHgn);
    fd.append("CaPpt", cPpt);
    fd.append("CaCur", cCur);
    fd.append("CaRos", cRos);
    fd.append("CaSae", cSga);
    fd.append("CaSsp", cSst);
    fd.append("Caflc", cflc);
    fd.append("HldEx", hExp);

    fd.append("AtnId", atId);
    fd.append("FinId", fnId);
    fd.append("CndId", cdId);
    fd.append("Capty", capa);
    fd.append("WtLbs", wLbs);
    fd.append("WtOzs", wOzs);
    fd.append("BarDc", bDec);
    fd.append("ChmDc", cDec);
    fd.append("OvrDc", oDec);
    fd.append("HideG", iHid);
    fd.append("Activ", iAtv);
    fd.append("IUsed", used);
    fd.append("OnWeb", oWeb);
    fd.append("Verif", iVer);
    fd.append("OgBox", oBox);
    fd.append("CuMdl", cMdl);
    fd.append("OgPpw", oPpw);
    fd.append("UpcCd", gUpc);
    fd.append("Descr", desc);
    fd.append("WbUpc", wUpc);
    fd.append("Model", modl);
    fd.append("MfgPn", mfPn);
    fd.append("LgDes", lDsc);
    fd.append("IsOsk", iOsk);
    fd.append("OldSk", oSku);
    
    fd.append("Print", tPrt);
    fd.append("TgSku", tSku);
    fd.append("TgMfg", tMfg);
    fd.append("TgCal", tCal);
    fd.append("TgCat", tTyp);
    fd.append("TgBrl", tBrl);
    fd.append("TgCap", tCap);
    fd.append("TgCnd", tCnd);
    fd.append("TgCus", tCus);

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
        url: "/Inventory/UpdateGunCtl",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            writeGunTag(data);
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

function getFileFromUrl(url) {
    return url.split("?")[0];
}

function getImgName(url) {
    var fnx = url.lastIndexOf("/") + 1;
    var igo = url.substr(fnx);
    var fName = igo.length > 0 ? igo : "";
    var nf = fName.indexOf("?") > 0 ? getFileFromUrl(fName) : fName;
    return nf;
}

function getWebImg(url) {
    var l = url.indexOf("/Spider");
    var r = url.substring(l + 7, url.length);
    var s = r.replace("/L/", "\\L\\");
    s = s.replace("/", "\\");
    return s;
}


function cookTags() {

    var fd = new FormData();
    fd.append("Mfg", $("#tagMfg").text());
    fd.append("Mdl", $("#tagMdl").text());
    fd.append("Act", $("#tagAct").text());
    fd.append("Cal", $("#tagCal").text());
    fd.append("Cap", $("#tagCap").text());
    fd.append("Brl", $("#tagBrl").text());
    fd.append("Cnd", $("#tagCnd").text());
    fd.append("Mpn", $("#tagMpn").text());
    fd.append("Upc", $("#tagUpc").text());
    fd.append("Prc", $("#tagPrc").text());
    fd.append("Sku", $("#tagSku").text());
    fd.append("Cnt", $("#s40 option:selected").val());
    fd.append("Sal", $("#s32 option:selected").val());

    $.ajax({
        cache: false,
        url: "/Print/CookGunTag",
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


function GetGunSpecsFromWeb(mid, isi, iow) {

    $.ajax({
        data: "{ Mid: '" + mid + "', Isi: '" + isi + "', Iow: '" + iow + "'}",
        url: "/Inventory/GetGunSpecs",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        cache: false,
        success: function (d) {

            var cv = $("#s2").val();
            $('[id^="ImgG_"]').empty();
            $("input[id^=ImgGun_]").empty();
            //$("#dvPics").show();

            var mfg = d.ManufId;
            var isi = d.InStockId;
            var gtp = d.GunTypeId;
            var cal = d.CaliberId;
            var atn = d.ActionId;
            var fin = d.FinishId;
            var cnd = d.ConditionId;
            var cap = d.CapacityInt;
            var upc = d.UpcCode;
            var spc = d.WebSearchUpc;
            var mpn = d.MfgPartNumber;
            var dsc = d.Description;
            var lds = d.LongDescription;

            var mdl = d.ModelName;
            var img = d.GunImgUrl;
            var wlb = d.WeightLb;

            var bdc = d.BarrelDec;
            var cdc = d.ChamberDec;
            var odc = d.OverallDec;
            var woz = d.WeightOz;

            var cmd = d.InProduction;
            var usd = d.IsUsed;
            var hid = d.IsHidden;
            var act = d.IsActive;
            var ver = d.IsVerified;
            var obx = d.OrigBox;
            var opw = d.OrigPaperwork;
            var iwb = d.IsWebBased;

            var caHd = d.CaRestrict.CaHide;
            var caOk = d.CaRestrict.CaOkay;

            var d0 = d.Images.PicId;
            var d1 = d.Images.ImgHse1;
            var d2 = d.Images.ImgHse2;
            var d3 = d.Images.ImgHse3;
            var d4 = d.Images.ImgHse4;
            var d5 = d.Images.ImgHse5;
            var d6 = d.Images.ImgHse6;

            var d7 = d.Images.Io1;
            var d8 = d.Images.Io2;
            var d9 = d.Images.Io3;
            var d10 = d.Images.Io4;
            var d11 = d.Images.Io5;
            var d12 = d.Images.Io6;

            var d13 = d.Images.DistCode;



            $("#s7").val(mfg);
            $("#s9").val(gtp);
            $("#s10").val(cal);
            $("#s11").val(cnd);
            $("#s12").val(fin);
            $("#s19").val(wlb);
            $("#s20").val(woz);
            $("#s33").val(atn);

            $("#s7").selectpicker("refresh");
            $("#s10").selectpicker("refresh");
            

            $("#t5").val(cap);
            $("#t6").val(upc);
            $("#t7").val(mpn);
            $("#t9").val(mdl);
            $("#t10").val(dsc);
            $("#t11").val(lds);

            $("#t42").val(bdc);
            $("#t43").val(odc);
            $("#t44").val(cdc);
            $("#t45").val(spc);


            $("#c1").prop('checked', cmd);
            $("#c2").prop('checked', usd);
            $("#c3").prop('checked', hid);
            $("#c4").prop('checked', act);
            $("#c5").prop('checked', ver);
            $("#c6").prop('checked', obx);
            $("#c7").prop('checked', opw);
            $("#c8").prop('checked', caHd);
            $("#c9").prop('checked', caOk);


            $('[id^="ImgM_"]').empty();

            if (d1.length > 0) { $("#ImgM_1").append("<img src='" + d1 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_1").attr("orig-img", d7); } else { $("#delCol_1").hide(); }
            if (d2.length > 0) { $("#ImgM_2").append("<img src='" + d2 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_2").attr("orig-img", d8); } else { $("#delCol_2").hide(); }
            if (d3.length > 0) { $("#ImgM_3").append("<img src='" + d3 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_3").attr("orig-img", d9); } else { $("#delCol_3").hide(); }
            if (d4.length > 0) { $("#ImgM_4").append("<img src='" + d4 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_4").attr("orig-img", d10); } else { $("#delCol_4").hide(); }
            if (d5.length > 0) { $("#ImgM_5").append("<img src='" + d5 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_5").attr("orig-img", d11); } else { $("#delCol_5").hide(); }
            if (d6.length > 0) { $("#ImgM_6").append("<img src='" + d6 + "' alt='' data-id='" + d0 + "' />"); $("#ImgHse_6").attr("orig-img", d12); } else { $("#delCol_6").hide(); }


            var fs = $("#ifs").val(); //services, no add web option

            if (iwb) { disableAddWeb(d1, d7, d13); } else { enableAddWeb(); }
            if (fs === "true") { $("#dvAddWeb").show(); } else { $("#dvAddWeb").hide(); }

            $("#mstId").val(mid);
            $("#stkId").val(isi);

            $("#t6").prop("disabled", true);
            
        }
    });
}


function disableAddWeb(url, img, dst)
{
    $("#iwb").val('true');
    $("#addPhotoRow").hide();

    $("#igOnePic").attr("src", url);
    $("#igOnePic").attr("web-img", img);
    $("#igOnePic").attr("dist", dst);
    $("#spOnePic").show();
    
}

function enableAddWeb() {
    //$("#s14").prop("disabled", false);
    //$("#s14").css("color", "#000000");
    $("#iwb").val('false');
    $("#spOnePic").hide();
    $("#addPhotoRow").show();

}



$(function () {
        $("#t15").datepicker({ onSelect: function() {
            $("#c15").prop("checked", true);
            $("#c15").prop("disabled", true);
        }
    });
});

$(function () {
    $("#t39").datepicker({ onSelect: function () { $(this).valid(); } });
});


$(function () {
    $("#t41").datepicker({ onSelect: function () { $(this).valid(); } });
});

// CA DOJ REQUIRED 30-DAY HOLD
$(document).ready(function () {
    $("#t15").focusout(function () {
        var t = $("#t15").val();
        if (t === "") {
            $("#c15").prop("checked", false);
        }
    });
});

 
$(document).ready(function () {
    $("#t29").click(function () {
        var v = $("#s23").val();
        if (v === "") {
            Lobibox.alert('error', {
                title: 'FFL State Required',
                msg: 'Please select <b>FFL State</b> BEFORE entering the FFL Name',
                color: '#000000'
            });
        }
    });
});


// MENU: Search Method
$(document).ready(function () {
    $("#s1").change(function () {

        restartCreate();

        var t = $(this).val();
        var v = $("#s2").val();
        var tt = $("#s13").val();

        switch(t) {
            case "1": //PULL FROM CUSTOMER INQUIRY
                getServiceInquiries(v);
                showInquiry();
                break;
            case "2": //PULL FROM ALLGUNS WEBSITE
                $("#addEdit").val("AddUnit");
                $("#divExisting").show();
                //$("#dvPics").hide();
                break;
            case "3": //PULL FROM IN-STOCK (OFFLINE)
                $("#addEdit").val("InStock");
                $("#divExisting").show();
                //$("#dvPics").hide();
                break;
            case "4": //CREATE NEW ITEM
                $("div[id^=ImgG_]").empty();
                $("span[id^=dCol_]").hide();
                flushAll();
                $("#addEdit").val("Create");
                $("#iwb").val("false");
                $("#dvBtnReview").show();
                $("#t6").prop("disabled", false);
                $("#t45").prop("disabled", false);
                if (tt === "1") { $("#s14").prop("disabled", false); }
                if (tt === "2") { $("#s14").val("false"); }
                showStart(v);
                break;
        }
 
    });
});


function setInvGrp(v) {

    restartCreate();
    hideInquiry();
    $("#s1").prop("selectedIndex", 0);
    $("#s13").prop("selectedIndex", 0);
    $("#locId").val(v);

    var iv = parseInt(v);
    if (iv > 0) {
        $("#dvInvGrp").show();
    } else {
        $("#dvInvGrp").hide();
        $("#dvDataSrc").hide();
    }
}


/* FFL" SELECT BY WAREHOUSE */
$(document).ready(function () {
    $("#s22").change(function () {
        var id = $("#s22").val();
        $("#fcd").val(id);

        $.ajax({
            url: "/Inventory/FflById",
            data: "{ fcd: '" + id + "'}",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            success: function (response) {

                var n = response.TradeName;
                var eml = response.FflEmail;
                var flc = response.FflFullLic;
                var exp = response.FflExpires;
                var lof = response.FflOnFile;
                var onf = lof ? "Yes" : "No";
                setConfirmation(n, flc, exp, onf);

                $("#t51").val(eml);

            },
            error: function (err, data) {
                alert(err);
            },
            complete: function () {
                $("#sp48").text(id);
            }
        });


        

    });
});



$(document).ready(function () {
    $("#s23").change(function () {
        var t = $(this).val();
        $("#fs").val(t);
    });
});


$(document).ready(function () {
    $("#s4").change(function () {
        var t2 = $(this).val();
        $("#v2").val(t2);
    });
});

$(document).ready(function () {
    $("#s5").change(function () {
        var t3 = $(this).val();
        $("#v3").val(t3);
    });
});

$(document).ready(function () {
    $("#s6").change(function () {
        var t4 = $(this).val();
        $("#v4").val(t4);
    });
});

$(document).ready(function () {
    $("#s24").change(function () {
        var t5 = $(this).val();
        $("#v5").val(t5);
    });
});

function setAcqSource(v) {

    clearFflSelect();

    $("#fflSource").hide();
    $("#supSource").hide();
    $("#warehouse").hide();
    $("#findFfl").hide();
    $("#curioFfl").hide();
    $("#divOrg").hide();
    $("#dvEmail").hide();
    $("#dvPptCt").hide();
    
    var l = parseInt(v);
    switch (l) {
        default:
            $("#supSource").hide();
            break;
        case 1: //FFL COMM
            $("#supSource").hide();
            $("#fflSource").show();
            $("#dvEmail").show();
            break;
        case 2:
            $("#dvPptCt").show();
        case 3:
            $("#dvPptCt").show();
        case 4:
        case 5:
        case 6:
            $("#supSource").show();
            break;
    }

}

function setFflSelect(v) {
    var l = parseInt(v);

    if (l === 99) {
        $("#findFfl").show();
        $("#warehouse").hide();
    } else {
        $("#findFfl").hide();
        $("#warehouse").show();
        setWarehouse(v);
    }
    
}


$(document).ready(function () {
    $("#t6").focusout(function () {

        var v = $("#t6").val();

        if (v.length < 10) { return; }
        $.ajax({
            url: "/Inventory/CheckUpcCode",
            data: "{ upc: '" + v + "'}",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            success: function (response) {

                if (response) {
                    Lobibox.alert('error',
                        {
                            title: "Cannot Add Duplicate Upc Code",
                            msg: 'The Upc Code <b>' + v + '</b> already exists. Select another Upc Code.',
                            color: '#000000'
                        });

                    $("#t6").val('');
                } 
            },
            error: function (err, data) {
                alert(err);
            },
            complete: function () {

            }
        });
    });
});

var sData;

/* GUN PICS - VALIDATE */
$(document).ready(function () {
    $('.img-loop').on("change.bs.fileinput",
        function () { checkImages(); });
});

/* GUN PICS - DELETE */
function delImg(i) {

    Lobibox.confirm({
        title: "Delete Image!",
        msg: "Permanently delete this image? This action cannot be undone",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {
                var ig = $("#ImgG_" + i);
                var cl = $("#dCol_" + i);
                var row = $(ig).find("img").attr("data-id");
                var img = $(ig).find("img").attr("src");


                var fd = new FormData();
                fd.append("Id", row);
                fd.append("ImgId", i);
                fd.append("Sect", 4);
                fd.append("ImgNm", img);

                $.ajax({
                    cache: false,
                    url: "/Inventory/NixImage",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {

                    },
                    complete: function () {
                        $(ig).empty();
                        $(cl).hide();
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}




/* MENU - GET INQUIRY GUNS */
$(document).ready(function () {

    $("#s27").change(function() {

        var tt = $("#s2").val();
        var tv = "";
        var sh = "";
        var add = false;

        if (tt === "1" || tt === "3") {
            tv = "Create New Gun";
            sh = "inline-block";
            add = true;
        } else { tv = "Use Inquiry Data"; sh = "none"; add = false;
        }

        var iqn = $(this).val();
        if (iqn === "") { $("#addedGuns").hide(); return; }

        $.ajax({
            data: "{ inqNum: '" + iqn + "'}",
            url: "/Inventory/GetSvcInqGuns",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            cache: false,
            success: function (response) {

                var a = response.length;

                $("#addedGuns").show();

                if (a > 0) {

                    $("#divSvcGuns").empty();
                    $("#dvSvcEmpty").hide();
                    var c = "#3366CC";

                    $.each(response, function (i, item) {

                        $("#divSvcGuns").append("<div class=\"svc-row-item\" style=\"background-color:" + c + "\">" +
                            "<div style=\"width:70px; display:table-cell\"><img src=\"" + item.ImageName + "\" class=\"exist-gun\" /></div>" +
                            "<div class=\"svc-item\">" + item.Description + "<div><div class=\"svc-item\">" +
                            "<a class=\"lnk-add\"  href=\"#\" onclick=\"addGunExisting('" + item.Id + "', '" + item.ManufId + "', '" + item.GunTypeId + "', '" + item.CaliberId + "')\">Search Existing Guns</a> | " +
                            "<a class=\"lnk-add\" href=\"#\" onclick=\"getDataFromSvc('" + iqn + "', '" + item.Id + "')\">Create From Inquiry Data</a>" +
                            "</div></div></div></div>");

                        c === "#6699CC" ? c = "#3366CC" : c = "#6699CC";

                    });
                } else {
                    $("#divSvcGuns").hide();
                    $("#dvSvcEmpty").show();
                }


            },
            error: function (err, data) {
                alert(err);
            },
            complete: function () {

            }
        });

    });
});


function getDataFromSvc(iqn, id) {

    $.ajax({
        data: "{ inqNum: '" + iqn + "', gunId: '" + id + "'}",
        url: "/Inventory/GetBookDataFromSvc",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        cache: false,
        success: function (data) {

            flushAll();
            showAddNew();

            var mdl = data.Gun.ModelName;
            var ser = data.Gun.SerialNumber;
            var mfg = data.Gun.ManufId;
            var gTp = data.Gun.GunTypeId;
            var cal = data.Gun.CaliberId;
            var fin = data.Gun.FinishId;
            var cnd = data.Gun.ConditionId;
            var oBx = data.Gun.OrigBox;
            var oPw = data.Gun.OrigPaperwork;
            var bli = data.Gun.BarrelIn;
            var bld = data.Gun.BarrelDec.toFixed(3);
            var img1 = data.Gun.SvcImg1;
            var img2 = data.Gun.SvcImg2;
            var img3 = data.Gun.SvcImg3;
            var img4 = data.Gun.SvcImg4;
            var img5 = data.Gun.SvcImg5;
            var img6 = data.Gun.SvcImg6;

            $("#s7").val(mfg);
            $("#s9").val(gTp);
            $("#s10").val(cal);
            $("#s11").val(cnd);
            $("#s12").val(fin);

            $("#c6").prop("checked", oBx);
            $("#c7").prop("checked", oPw);

            $("#t8").val(mdl); //Book Model
            $("#t9").val(mdl); //Website Model
            $("#t22").val(ser);


            if (img1.length > 0) { $("#ImgG_1").append("<img src='" + img1 + "' />"); }
            if (img2.length > 0) { $("#ImgG_2").append("<img src='" + img2 + "' />"); }
            if (img3.length > 0) { $("#img_3").html("<img src='" + img3 + "' />"); }
            if (img4.length > 0) { $("#img_4").html("<img src='" + img4 + "' />"); }
            if (img5.length > 0) { $("#img_5").html("<img src='" + img5 + "' />"); }
            if (img6.length > 0) { $("#img_6").html("<img src='" + img6 + "' />"); }


            $("#s7").selectpicker("refresh");
            $("#s10").selectpicker("refresh");

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#addEdit").val("Create"); 
        }
    });
}


function writeGunTag(d) {

    var fSz = 0;
    var ml = d.MfgName.length;

    if (ml < 6) { fSz = "22px"; }
    if (ml > 5 && ml < 9) { fSz = "20px"; }
    if (ml > 8 && ml < 12) { fSz = "16px"; }
    if (ml > 11 && ml < 19) { fSz = "13px"; }
    if (ml > 18 && ml < 42) { fSz = "10px"; }
    if (ml > 41) { fSz = "9px"; }

    var typ = d.Caliber + " " + d.GunType;
    var brl = d.Barrel > 0 ? d.Barrel + '"' : "";

    $("#tagMfg").css("font-size", fSz);

    $("#tagMfg").text(d.MfgName);
    $("#tagMdl").text(d.Model);
    $("#tagAct").text(typ);
    $("#tagCap").text(d.Capacity);
    $("#tagBrl").text(brl);
    $("#tagCnd").text(d.Condition);
    $("#tagSer").text(d.SerNumber);
    $("#tagMpn").text(d.MfgPartNum);
    $("#tagSku").text(d.TagSku);
    $("#tagSvc").text(d.SvcType);
    $("#tagSvc").css("color", "blue");
    $("#brcTxt").text(d.TagSku);
    $("#tagPrc").removeAttr("style");

    if (d.IsSale) {
        $("#tagService").hide();
        $("#tagOurPrc").show();
        $("#tagConfCap").show();
        $("#tagConfBrl").show();
        $("#tagConfCond").show();
        $("#tagConfMfg").show();
        $("#tagPrc").text(d.TagPrice).formatCurrency();
    } else {
        $("#tagService").show();
        $("#tagSerial").show();
        $("#tagOurPrc").hide();
        $("#tagPrc").text(d.SvcName);
        $("#tagPrc").css("width", "200px");
        $("#tagPrc").css("font-weight", "800");
    }
}

 
function restockGun() {
    
    var loc = $("#s2").val(); //loc id
    var ttp = $("#s32").val(); //trans type id
    var hcc = $("#t13").val(); //hi-cap count
    var hcp = $("#t40").val(); //hi-cap capacity
    var lmk = $("#s29").val(); //lock make
    var lmd = $("#s30").val(); //lock model
    var asc = $("#s21").val(); //acq source
    var fsc = $("#s24").val(); //ffl source
    var ios = $("#s41").val(); //is old sku
    var ipt = $("#s42").val(); //Is PPT

    var gid = $("#gunId").val(); //gun id
 
    var prc = $("#t16").val(); //commission price
    var cst = $("#t19").val(); //cost
    var frt = $("#t20").val(); //freight
    var fee = $("#t21").val(); //fees
    var tax = $("#t14").val(); //tax amount
    var cpd = $("#t47").val(); //customer transfer price

    var ser = $("#t22").val(); //serial #
    var cus = $("#t35").val(); //svc customer
    var sku = $("#t46").val(); //old sku
    var eml = $("#t51").val(); //email
    var cfl = $("#t12").val(); //cflc

    var acq = $("#t41").val(); //acq date
    var exp = $("#t15").val(); //hold expires

    var ppt = $("#c14").prop("checked"); //PPT
    var hld = $("#c15").prop("checked");
    var itx = $("#s15").val();
 
    var cid = $("#cus").val(); //cust Id
    var sup = $("#sup").val(); //supp Id
    var fcd = $("#fcd").val(); //cust Id

    var isOwb = $("#sp8").text();
    var iow = isOwb === "YES" ? true : false;

    var fd = new FormData();

    fd.append("GunId", gid);
    fd.append("LocId", loc);
    fd.append("TtpId", ttp);
    fd.append("HcpCt", hcc);
    fd.append("HcpCp", hcp);
    fd.append("LokMk", lmk);
    fd.append("LokMd", lmd);
    fd.append("AcqSc", asc);
    fd.append("FflSc", fsc);
    fd.append("CusId", cid);
    fd.append("SupId", sup);
    fd.append("FflCd", fcd);

    fd.append("Ucost", cst);
    fd.append("Frght", frt);
    fd.append("Ufees", fee);
    fd.append("TaxAm", tax);
    fd.append("CusPd", cpd);
    fd.append("ComPr", prc);

    fd.append("Seria", ser);
    fd.append("OlSku", sku);
    fd.append("SvcCu", cus);
    fd.append("CflcN", cfl);
    fd.append("Email", eml);

    fd.append("AcqDt", acq);
    fd.append("HxpDt", exp);

    fd.append("IsHld", hld);
    fd.append("IsTax", itx);
    fd.append("IsOwb", iow);
    fd.append("IsOsk", ios);
    fd.append("IsPpt", ppt);
    fd.append("CaPpt", ipt);
    
    $.ajax({
        cache: false,
        url: "/Inventory/RestockGun",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            writeGunTag(data);
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


function verifyGun(v) {
    var errCt = 0;
    var iv = true;

    iv = $("#form-gunBasics").valid();
    if (!iv) { errCt++; }

    iv = $("#form-locTrans").valid();
    if (!iv) { errCt++; }

    iv = $("#form-gunSpecs").valid();
    if (!iv) { errCt++; }

    iv = $("#form-acct").valid();
    if (!iv) { errCt++; }

    iv = $("#form-calif").valid();
    if (!iv) { errCt++; }

    iv = $("#form-book").valid();
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
        $("#dvVerfBb").show();
        break;
    case 2:
        $("#rowBtnUpd").show();
        $("#dvVerfBb").hide();
        break;
    }

    var i1 = parseFloat($("#t16").val()); //price
    var i2 = parseFloat($("#t17").val()); //msrp
    var i3 = parseFloat($("#t18").val()); //sale price
    var i4 = parseFloat($("#t19").val()); //cost
    var i5 = parseFloat($("#t20").val()); //frt
    var i6 = parseFloat($("#t21").val()); //fees
    var i7 = parseFloat($("#t14").val()); //tax
    var i9 = parseFloat($("#t13").val()); //hi-caps

    i1 = isNaN(i1) ? 0.00 : i1;
    i2 = isNaN(i2) ? 0.00 : i2;
    i3 = isNaN(i3) ? 0.00 : i3;
    i4 = isNaN(i4) ? 0.00 : i4;
    i5 = isNaN(i5) ? 0.00 : i5;
    i6 = isNaN(i6) ? 0.00 : i6;
    i7 = isNaN(i7) ? 0.00 : i7;
    i9 = isNaN(i9) ? 0 : i9;

    var mfg = $("#s7").prop("selectedIndex") === 0 ? "" : $("#s7 :selected").text(); /* manufacturer */
    var imp = $("#s8").prop("selectedIndex") === 0 ? "" : $("#s8 :selected").text(); /* importer */
    var gtp = $("#s9").prop("selectedIndex") === 0 ? "" : $("#s9 :selected").text(); /* gun type */
    var clb = $("#s10").prop("selectedIndex") === 0 ? "" : $("#s10 :selected").text(); /* caliber */
    var cnd = $("#s11").prop("selectedIndex") === 0 ? "" : $("#s11 :selected").text(); /* condition */
    var fin = $("#s12").prop("selectedIndex") === 0 ? "" : $("#s12 :selected").text(); /* finish */
    var lmk = $("#s29").prop("selectedIndex") === 0 ? "" : $("#s29 :selected").text(); /* Lock make */
    var lmd = $("#s30").prop("selectedIndex") === 0 ? "" : $("#s30 :selected").text(); /* Lock model */
    var svc = $("#s32").prop("selectedIndex") === 0 ? "" : $("#s32 :selected").text(); /* trans type */
    var atn = $("#s33").prop("selectedIndex") === 0 ? "" : $("#s33 :selected").text(); /* action */

    var sct = $("#s15").val();


    var supTxt = $("#t52").val();
    var supNam = "";

    if (supTxt != null) {
        var supIdx = supTxt.indexOf(":");
        supNam = supTxt.substring(0, supIdx);
        $("#sp41").text(supNam);

    }


    var aqs = $("#s21").val(); /* acq src */
    var iow = $("#s14").val(); /* on web */
    var loc = $("#s2").val(); /* location */

    var cap = $("#t5").val(); /* capacity */
    var bmd = $("#t8").val(); /* book model */
    var mdl = $("#t9").val(); /* gun model */
    var cfl = $("#t12").val(); /* cflc */
    var hxp = $("#t15").val(); /* hold exp */
    var ser = $("#t22").val(); /* serial */
    var cfb = $("#t37").val(); /* cur ffl numb */
    var cfn = $("#t38").val(); /* cur ffl name */
    var cex = $("#t39").val(); /* cur ffl exp */
 
 
    var cus = $("#t35").val();

    var cur = $("#c1").is(":checked") ? "YES" : "NO";
    var usd = $("#c2").is(":checked") ? "YES" : "NO";
    var hid = $("#c3").is(":checked") ? "YES" : "NO";
    var atv = $("#c4").is(":checked") ? "YES" : "NO";
    var ver = $("#c5").is(":checked") ? "YES" : "NO";
    var hca = $("#c8").is(":checked") ? "YES" : "NO";
    var cal = $("#c9").is(":checked") ? "YES" : "NO";

    var rst = $("#c10").is(":checked") ? "YES" : "NO";
    var sar = $("#c11").is(":checked") ? "YES" : "NO";
    var cre = $("#c12").is(":checked") ? "YES" : "NO";
    var ssp = $("#c13").is(":checked") ? "YES" : "NO";
    var pvt = $("#c14").is(":checked") ? "YES" : "NO";
    var hld = $("#c15").is(":checked") ? "YES" : "NO";
    var fct = sct==="true" ? "YES" : "NO";
    
 

    var sel = "FFL";
    var web = iow === "true" ? "YES" : "NO";
 
 

    if (iow === "true") { $("#dvVerWb").css("display", "inline-block"); } else { $("#dvVerWb").css("display", "none"); }
    if (loc === "1") { $("#dvVerfCa").css("display", "inline-block"); } else { $("#dvVerfCa").css("display", "none"); }

    $("#sp1").text(i1).formatCurrency();
    $("#sp2").text(i2).formatCurrency();
    $("#sp3").text(i3).formatCurrency();
    $("#sp4").text(i4).formatCurrency();
    $("#sp5").text(i5).formatCurrency();
    $("#sp6").text(i6).formatCurrency();
    $("#sp7").text(svc);
    $("#sp8").text(web);
    $("#sp9").text(cus);
    $("#sp10").text(mdl);
    $("#sp11").text(cnd);
    $("#sp12").text(fin);
    $("#sp13").text(atn);
    $("#sp14").text(cap);
    $("#sp15").text(cur);
    $("#sp16").text(usd);
    $("#sp17").text(hid);
    $("#sp18").text(atv);
    $("#sp19").text(ver);
    $("#sp20").text(cfl);
    $("#sp21").text(i9);
    $("#sp22").text(i7).formatCurrency();
    $("#sp23").text(hxp);
    $("#sp24").text(lmk);
    $("#sp25").text(lmd);
    $("#sp26").text(hca);
    $("#sp27").text(cal);
    $("#sp28").text(rst);
    $("#sp29").text(sar);
    $("#sp30").text(cre);
    $("#sp31").text(ssp);
    $("#sp32").text(pvt);
    $("#sp33").text(hld);
    $("#sp34").text(fct);

    $("#sp42").text(mfg);
    $("#sp43").text(imp);
    $("#sp44").text(bmd);
    $("#sp45").text(ser);
    $("#sp46").text(gtp);
    $("#sp47").text(clb);
 

    switch (aqs) {
        default: //COMM FFL
            $("#fromFfl").show();
            $("#fromPublic").hide();
            break;

        case "2": //03 C&R FFL
            $("#sp36").text(cfn);
            $("#sp37").text(cfb);
            $("#sp38").text(cex);
            $("#fromFfl").show();
            $("#fromPublic").hide();
            sel = "FFL";
            break;
        case "3":
            $("#fromFfl").hide();
            $("#fromPublic").show();
            sel = "PRIVATE PARTY";
            break;
        case "4":
            $("#fromFfl").hide();
            $("#fromPublic").show();
            sel = "POLICE";
            break;
        case "5":
            $("#fromFfl").hide();
            $("#fromPublic").show();
            sel = "OTHER ORGANIZATION";
            break;
        case "6":
            $("#sp28").text("Nick Nelson" + " - " + "FROM OWNER'S PERSONAL COLLECTION");
            $("#fromFfl").hide();
            $("#fromPublic").show();
            sel = "OWNER'S COLLECTION";
            break;
    }

}


function addGunExisting(id, mfg, typ, cal) {

    $("#addEdit").val("AddUnit");

    $("#InqGunId").val(id);

    showExisting();

    $("#s3").val(mfg);
    $("#s3").selectpicker("refresh");

    $("#s4").val(typ);
    $("#s5").val(cal);

    $("#s5").selectpicker("refresh");

}



function addGunLockMfg() {

    var m = $("#t33").val();
    var s = $("#s29");
    var dv = $("#addLockMfg");

    $.ajax({
        data: "{ newmfg: '" + m + "'}",
        url: "/Inventory/AddGunLockMaker",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: "Lock Manufacturer Already Exists",
                        msg: '<b>' + m + '</b> cannot be added because that Gun Lock Manufacturer already exists',
                        color: '#000000'
                    });
            } else {


                var si = response.SelectedId;

                $(s).find("option").remove().end();
                s.append("<option>- SELECT -</option>");
                s.append("<option value=\"-1\">- ADD NEW LOCK MANUFACTURER -</option>");

                $.each(response.GunLockMfg, function (i, item) {
                    s.append("<option value=" + item.LockManufId + ">" + item.LockManuf + "</option>");
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

function getLockModels(id, sel) {

    var s = $("#s30");

    $.ajax({
        data: "{ lockMfgId: '" + id + "'}",
        url: "/Inventory/GetGunLockModels",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

                $(s).find("option").remove().end();
                s.append("<option>- SELECT -</option>");
                s.append("<option value=\"-1\">- ADD NEW LOCK MODEL -</option>");
            
                $.each(result, function (i, item) {
                    var x = parseInt(item.Value) === parseInt(sel) ? " selected" : "";
                    s.append("<option value=" + item.Value + " " + x + ">" + item.Text + "</option>");
                });

                s.selectpicker("refresh");
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}

function addGunLockModel() {

    var mdl = $("#t34").val();
    var lMfg = $("#s29").val();
    var s = $("#s30");
    var dv = $("#addLockModel");

    $.ajax({
        data: "{ lockMfgId: '" + lMfg + "', lockModel: '" + mdl + "'}",
        url: "/Inventory/AddGunLockModel",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: "Lock Model Already Exists",
                        msg: '<b>' + mdl + '</b> cannot be added because that Gun Lock Model already exists',
                        color: '#000000'
                    });
            } else {


                var si = response.SelectedId;

                $(s).find("option").remove().end();
                s.append("<option>- SELECT -</option>");
                s.append("<option value=\"-1\">- ADD NEW LOCK MANUFACTURER -</option>");

                $.each(response.GunLockModel, function (i, item) {
                    s.append("<option value=" + item.LockModelId + ">" + item.LockModel + "</option>");
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

function setBookEntry(make, mod, cal, typ) {

    showBook();
    $("#divAcct").show();

    $("#s7").val(make);
    $("#s7").selectpicker("refresh");
    $("#t8").val(mod);
    $("#s9").val(typ);
    $("#s10").val(cal);
    $("#s10").selectpicker("refresh");

}

function setFflTxt(fName, fAddr, fCity, fLic) {
    var txt = fLic + ' ' + fName + ' ' + fAddr + ' ' + fCity;
    $("#t29").val(txt);
}

function setConfirmation(fName, fNum, fExp, fSgn, fid) {

    $("#sp36").text(fName);
    $("#sp37").text(fNum);
    $("#sp38").text(fExp);
    $("#sp39").text(fSgn);
    $("#sp48").text(fid);
}

function clearForMfg() {
    $("#s4").prop('selectedIndex', 0);
    $("#s5").prop('selectedIndex', 0);
    $("#s5").selectpicker("refresh");
    $("#s6").prop('selectedIndex', 0);
    $("#s24").prop('selectedIndex', 0);
    $("#v2").val("");
    $("#v3").val("");
    $("#v4").val("");
    $("#v5").val("");
    $("#t1").val("");
}

function setWarehouse(id) {

    var d = $("#s22");

    $.ajax({
        cache: false,
        data: "{ fflId: '" + id + "'}",
        url: "/Inventory/GetWarehouses",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            $(d).find("option").remove().end();
            d.append("<option value=''>- SELECT -</option>");

            $.each(result, function (i, item) {
                d.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });

        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {

        }
    });
}

function getServiceInquiries(id) {

    var d = $("#s27");

    $.ajax({
        cache: false,
        url: "/Inventory/GetInquiries",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        data: { transTypeId: id },
        success: function (result) {

            $(d).find("option").remove().end();
            d.append("<option value='0'>- SELECT -</option>");

            $.each(result, function (i, item) {
                d.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });

        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {

        }
    });
}

/**** CLEAR FUNCTIONS ****/

function clearAllErrors() {
    clearGunSpecs();
    clearGunBasics();
    clearAcct();
    clearBoundBook();
}


function startOver() {
    $("#divShadow").show();
    $("#divSearch").show();
    clearAll();
    hideAll();
    $("div[id^=ImgG_]").empty();
    //$("span[id^=dCol_]").hide();
    $("input[id^=ImgGun_]").empty();
}


function AddAnother() {
    $("#divShadow").show();
    $("#divSearch").show();
}

function flushAll() {

    $("#s2").prop("disabled", false);
    $("#s42").prop("disabled", false);
    clearFormExisting();
    clearGunBasics();
    clearGunSpecs();
    clearAcct();
    clearFormCalif();
    clearBoundBook();
    clearFflSelect();
    clearStartSearch();
}



function showAddNew() {
    $("#divGunBasics").show();
    $("#divGunSpecs").show();
    $("#divAcct").show();
    $("#divBook").show();
    $("#dvRestHide").show();
}

function clearGunFilters() {
    clearFormExisting();
    clearGunBasics();
    clearGunSpecs();
    clearAcct();
    clearFormCalif();
    clearBoundBook();
    clearFflSelect();
    $("#divConfirmData").hide();
    $("#divGunBasics").hide();
    $("#divGunSpecs").hide();
    $("#divAcct").hide();
    $("#divBook").hide();
}


function clearAll() {
    $("#s1").prop('selectedIndex', 0);
    $("#s13").prop('selectedIndex', 0);
    $("#s1 option[value='3']").show();
    clearFormExisting();
    clearGunBasics();
    clearFormCalif();
    clearBoundBook();
    clearAcct();
    clearPics();
    $("#form-acct")[0].reset();
    $("#taxCustName").hide();
}

function clearBase() {
    clearFormExisting();
    clearFormCalif();
    clearBoundBook();
    clearAcct();
}

function clearGunSpecs() {
    var fv = $("#form-gunSpecs").validate();
    fv.resetForm();
    clearPics();
    $("#form-gunSpecs")[0].reset();
}

function clearGunBasics() {
    var fv = $("#form-gunBasics").validate();
    fv.resetForm();
    $("#CusImg_1").children("img").remove();
    $("#form-gunBasics")[0].reset();
    $("#s14").prop("disabled", false);
    $("#s14").css("color", "#000000");
}

function clearPics() {
    $("div[id^=ImgG_]").empty();
    $("input[id^=ImgGun_]").empty();
}

function clearFormExisting() {

    var fe = $("#form-existing").validate();
    fe.resetForm();

    $("#form-existing")[0].reset();
    $("#s3").selectpicker("refresh");
    $("#s5").selectpicker("refresh");
    $("#mstId").val("");
    $("#v1").val("");
    $("#v2").val("");
    $("#v3").val("");
    $("#v4").val("");
    $("#v5").val("");
}

function clearFormCalif() {
    $("#form-calif")[0].reset();
    $("#s29").prop('selectedIndex', 0);
    $("#s30").prop('selectedIndex', 0);
    $("#s29").selectpicker("refresh");
    $("#s30").selectpicker("refresh");
}

function clearBoundBook() {

    var fv = $("#form-book").validate();
    fv.resetForm();

    clearFflSelect();
    $("#form-book")[0].reset();
    $("#s7").selectpicker("refresh");
    $("#s8").selectpicker("refresh");
    $("#s10").selectpicker("refresh");
    hideBookAcq();
}

function clearAcct() {
    var fv = $("#form-acct").validate();
    fv.resetForm();
    $("#form-acct")[0].reset();
}

function clearFflSelect() {
    $("#s23").prop('selectedIndex', 0);
    $("#t29").val("");
    $("#fcd").val("0");
    $("#sup").val("0");
    $("#fs").val("");
}

function clearStartSearch() {
    var fe = $("#form-locTrans").validate();
    fe.resetForm();
    $("#form-locTrans")[0].reset();
}

function wipeGunFilters() {
    $("#locId").val("");
    $("#grpId").val("");
    $("#ttpId").val("");
    $("#mfgId").val("");
    $("#gtpId").val("");
}


/**** SHOW HIDE FUNCTIONS ****/

function showUpdate(l, wb) {
    //$("#dvPics").show();
    $("#dvGuns").show();
    $("#divGunBasics").show();
    $("#divGunSpecs").show();
    $("#divAcct").show();
    $("#divPrice").show();
    $("#dvBtnUpdate").css("display", "inline-block");
    if (!wb) { $("#dvBtnDelete").css("display", "inline-block"); }

    $("#s2").prop("disabled", true);

    if (l === 1) { $("#divCaStuff").show(); }
}

function showStart(l) {
    $("#divGunBasics").show();
    $("#divGunSpecs").show();
    $("#dvGuns").show();
    $("#divAcct").show();
    $("#divBook").show();
    $("#rowBtnAdd").show();
    $("#dvBtnUpdate").hide();
    $("#dvBtnDelete").hide();
    $("#dvRestHide").show();

    if (l === "1") {
        $("#divCaStuff").show();
    } else {
        $("#divCaStuff").hide();
    }
}

function restartPanels() {
    flushAll();
    $("#divExisting").hide();
    $("#divSrchHdr").hide();
    $("#divGunBasics").hide();
    $("#divGunSpecs").hide();
    $("#divAcct").hide();
    $("#divCaStuff").hide();
    $("#divBook").hide();
    $("#dvBtnUpdate").hide();
    $("#dvBtnDelete").hide();
    $("#dvBtnReview").hide();
    $("#dvBtnRestock").hide();
    $("#rowBtnAdd").hide();
    $("#rowBtnUpd").hide();

}

function restartCreate() {
    flushAll();
    $("#divExisting").hide();
    $("#divGunBasics").hide();
    $("#divGunSpecs").hide();
    $("#dvGuns").hide();
    $("#divAcct").hide();
    $("#divCaStuff").hide();
    $("#divBook").hide();
    $("#dvBtnUpdate").hide();
    $("#dvBtnDelete").hide();
    $("#dvBtnReview").hide();

}

function showWebSearch() {

    flushAll();
    var st = $("#s2").val();
    if (st === "1") { $("#divCaStuff").show(); }

    $("#divGunBasics").show();
    $("#divGunSpecs").show();
    $("#dvGuns").show();
    $("#divAcct").show();
    $("#divBook").show();
    $("#dvRestHide").show();
    $("#dvBtnReview").show();
    $("#dvBtnUpdate").hide();
    $("#dvBtnDelete").hide();
    $("#divExisting").hide();

}


function hideInquiry() {
    $("#s27").prop('selectedIndex', 0);
    $("#divSvcGuns").empty();
    $("#divExisting").hide();
    $("#addedGuns").hide();
    $("#svcInq").hide();

}

function hideBookAcq() {
    $("#s21").prop('selectedIndex', 0);
    $("#findFfl").hide();
    $("#warehouse").hide();
}


function hideToConfirm() {
    $("#divSearch").hide();
    $("#divExisting").hide();
    $("#divGunBasics").hide();
    $("#divGunSpecs").hide();
    $("#divAcct").hide();
    $("#divBook").hide();
}

function hideToEdit() {
    $("#divShadow").show();
    $("#divConfirmData").hide();

    var opt = $("#addEdit").val();
    var v = $("#s32").val();

    if (opt === "AddUnit") {
        $("#ttpId").val(v);
    }

}


function hideAll() {
    $("#divAcct").hide();
    $("#divBook").hide();
    $("#divExisting").hide();
    $("#divGunBasics").hide();
    $("#divGunSpecs").hide();
    $("#divDone").hide();
    
    hideInquiry();
}


function hideSelectGuns() {
    $("#divSvcGuns").empty();
    $("#addedGuns").hide();
}


function showInquiry() {
    $("#svcInq").show();

}

function showHideAcct() {
    var t = $("#s2").val();
    if (t === "1") {
        $("#divAcct").show();
    } else {
        $("#divAcct").hide();
    }
}


function showBook() {
    $("#divBook").show();
    //$("#s32").val($("#s2").val());
}


function gunRestock(gid, ifs) {

    flushAll();
    $("#gunId").val(gid);

    $("#s2").prop("disabled", false);

    $("#s32").prop("selectedIndex", 0);
    $("#s32 > option").each(function () { this.style.display = "block"; });

    if (ifs) {
        $("#taxCustName").hide();

        $("#s32 > option").each(function () {
            var i = parseInt(this.value);
            if (i > 102) { this.style.display = "none"; }
        });

    } else {
        $("#taxCustName").show();

        $("#s32 > option").each(function () {
            var i = parseInt(this.value);
            if (i === 101 || i === 102) { this.style.display = "none"; }
        });
    }

    $("#dvGuns").show();
    $("#divAcct").show();
    $("#divBook").show();
    $("#dvBtnRestock").show();
    $("#dvRestHide").hide();
    $("#dvPrcInfo").hide();
   
}


function setWebOpts(v) {
    if (v === "false") {
        hideWebOpts();
    } else {
        showWebOpts();
    }
}

function hideWebOpts() {
    
    $("#dvWebUpc").hide();
    $("#dvHide").hide();
    $("#dvActv").hide();
    $("#dvVerf").hide();
    $("#dvHdCa").hide();

    $("#igCol_2").hide();
    $("#igCol_3").hide();
    $("#igCol_4").hide();
    $("#igCol_5").hide();
    $("#igCol_6").hide();
}

function showWebOpts() {

    $("#dvWebUpc").show();
    $("#dvHide").show();
    $("#dvActv").show();
    $("#dvVerf").show();
    $("#dvHdCa").show();

    $("#dvRost").show();
    $("#dvSgAn").show();
    $("#dvCuro").show();
    $("#dvSgSt").show();
    $("#dvCppt").show();

    $("#igCol_2").show();
    $("#igCol_3").show();
    $("#igCol_4").show();
    $("#igCol_5").show();
    $("#igCol_6").show();
}

function showExisting() {
    $("#divExisting").show();
    $("#divGunBasics").hide();
    $("#divGunSpecs").hide();
    $("#divAcct").hide();
    $("#divBook").hide();
    $("#rowBtnAdd").show();
}


function hideCostInfo()
{
    $("#dvPrcInfo").hide();
    $("#dvUnitCost").hide();
}

function showCostInfo() {
    $("#dvPrcInfo").show();
    $("#dvUnitCost").show();
}


function filterGuns()
{
    var mfg = $("#s34 :selected").val(); //setMfg(v)
    var gtp = $("#s35 :selected").val();
    var loc = $("#s37 :selected").val();
    var sub = $("#s39 :selected").val(); //setTtp(o)
    var sci = $("#sci").val();

    getGunList(mfg, gtp, loc, sub, sci);
}

function getGunList(m, g, l, s, x) {

    var fd = new FormData();
    fd.append("Loc", l);
    fd.append("Ttp", s);
    fd.append("Mfg", m);
    fd.append("Gtp", g);
    fd.append("Cus", x);

    return $.ajax({
        cache: false,
        url: "/Inventory/GetGuns",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {

            $("#gunRows").empty();
            var rc = "#FFF7E5";

            var dl = data.length;            
            $("#dvNavCt").text(dl + " Guns Found");

            $.each(data, function (i, item) {

                var gid = item.Id;
                var uCa = item.UnitsCa;
                var uWy = item.UnitsWy;
                var rsk = item.Restocks;
                var img = item.ImageUrl;
                var mpn = item.MfgPartNumber;
                var upc = item.UpcCode;
                var mfg = item.ManufName;
                var dsc = item.ItemDesc;
                var ifs = item.IsForSale;
                var owb = item.IsOnWeb;

                var ow = "No";
                var fs = "No";
                if (owb) { ow = "Yes"; }
                if (ifs) { fs = "Yes"; }

                var au = upc.length > 0 ? ' UPC: ' + upc : '';
                var mp = mpn.length > 0 ? ' MFG# ' + mpn : '';
                var nds = mp + ' ' + au;

                var opt = ifs ? "Purchase(s)" : "Service(s)";

                var imgUl = img.length > 0 ? "<img src='" + img + "' class='gun-img' alt=''>" : "";

                var block = "<div data-id='" + gid + "' class='gun-row' style='color:black;background-color:" + rc + "'>";
                block += "<div class='ammo-bdr' style='padding-top:14px'><div class='gun-p1' style='text-align:center'><select id='atn" + gid + "' onchange='runGunAtn(this, " + gid + ", " + ifs + ")'  style='border-radius:4px; padding:3px'><option value='0'>-SELECT-</option><option value='1'>Update</option><option value='2'>Restock</option></select></div></div>";
                block += "<div class='ammo-bdr' style='padding-top:14px'><div class='gun-p1' style='text-align:center'><div class='hide-it' id='hit-" + gid + "'><select id='cb" + gid + "'  style='border-radius:4px; width:270px; padding:3px; display:none' onchange='getGunById(this.value," + ifs + ");'></select></div><span id='al" + gid + "'>" + rsk + " " + opt + "</span></div></div>";
                block += "<div class='ammo-bdr'><div class='ammo-img-row'>" + imgUl + "</div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1'><div><b style='color:blue'>" + mfg + "</b></div><div>" + dsc + "</div><div>" + nds + "</div></div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + fs + "</div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + ow + "</div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + uCa + "</div></div>";
                block += "<div class='ammo-bdr'><div class='gun-p1' style='text-align:center; padding-top:21px'>" + uWy + "</div></div>";
                block += "</div>";


                $('#gunRows').append(block);

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



function setLoc(v) {
    $("#s34").prop("selectedIndex", 0);
    $("#s35").prop("selectedIndex", 0);
    $("#s38").prop("selectedIndex", 0);
    $("#s39").prop("selectedIndex", 0);
    $("#csi").val("0");

    bindMfg(v);
    filterGuns();
}


function bindMfg(v) {

    var fd = new FormData();
    fd.append("Loc", v);
 
    return $.ajax({
        cache: false,
        url: "/Inventory/SetMfg",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (response) {
            var m = $("#s34");
            $(m).find("option").remove().end();
            m.append("<option>- SELECT -</option>");

            $.each(response, function (i, item) {
                m.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });
        },
        complete: function () {

        }

    });
}


//function filterTtp(o) {

//    $("#ttpId").val("0");

//    // show all
//    $("#s39").prop("selectedIndex", 0);
//    $("#s39 > option").each(function () { this.style.display = "block"; });

//    var v = parseInt(o);

//    // show sales
//    if (v === 101) { $("#s39 > option").each(function() {
//        var i = parseInt(this.value);
//        if (i > 102) { this.style.display = "none"; }
//    }); }

//    // show service
//    if (v === 102) { $("#s39 > option").each(function() {
//        var i = parseInt(this.value);
//        if (i === 101 || i === 102) { this.style.display = "none"; }
//    }); }
//}


function setTtp(o) {

    hideInquiry();
    $("#s1").prop("selectedIndex", 0);


    // show all
    $("#s32").prop("selectedIndex", 0);
    $("#s32 > option").each(function () { this.style.display = "block"; });
    $("#dvAddWeb").show();

    var v = parseInt(o);
    if (v > 0) { $("#dvDataSrc").show(); } else { $("#dvDataSrc").hide(); }

    var au = $("#addEdit").val();

    // show sales
    if (v === 1) {
        $("#dvAddWeb").show();
        $("#taxCustName").hide();
        if (au === "AddUnit") {
            $("#s14").val("true");
        }
        $("#s32 > option").each(function () { var i = parseInt(this.value); if (i > 102) { this.style.display = "none"; } });
        showWebOpts();
    }

    // show service
    if (v === 2) {
        $("#s14").val("false");
        $("#dvAddWeb").hide();
        $("#taxCustName").show();
        $("#s32 > option").each(function () { var i = parseInt(this.value); if (i === 101 || i === 102) { this.style.display = "none"; } });
        $("#t16").val("0.00");
        $("#t19").val("0.00");
        hideWebOpts();
    }
}

function setOldSku(v)
{
    if (v === "true") {
        $("#dvOldSku").show();
    } else {
        $("#dvOldSku").hide();
    }
}


function setCostInfo(v) {

    $("#ttpId").val("");
    $("#dvTax").hide();
    $("#dvGotTax").hide();
    $("#dvAddWeb").hide();
    $("#dvCaPpt").hide();

    $("#c14").prop('checked', false);
    set30Clear();

    switch (v) {
        default: //SALE
            showGunCost();
            $("#dvUnitCost").show();
            $("#taxCustName").hide();
            $("#dvAddWeb").show();
            break;
        case "102": //CONSIGNMENT
            showGunCost();
            set30Hold();
            $("#dvUnitCost").hide();
            $("#taxCustName").show();
            $("#dvAddWeb").show();
            $("#dvCustPd").hide();
            $("#c14").prop('checked', true);
            break;
        case "103":
            hideGunCost();
            $("#dvCaPpt").show();
            $("#dvCustPd").show();
            $("#dvGotTax").show();
            $("#dvUnitCost").hide();
            break;
        case "104":
            showGunCost();
            $("#dvUnitCost").hide();
            break;
        case "105":
            hideGunCost();
            $("#dvCustPd").hide();
            $("#dvUnitCost").hide();
            break;
        case "106":
            hideGunCost();
            $("#dvCustPd").hide();
            $("#dvUnitCost").hide();
            $("#dvFees").show();
            break;
        case "107":
            hideGunCost();
            $("#dvCustPd").hide();
            $("#dvUnitCost").show();
            $("#dvFees").show();
            break;
    }
}


function showGunCost() {
    $("#dvCustPd").hide();
    $("#dvPrcInfo").show();
    $("#dvFees").show();
}

function hideGunCost() {
    $("#taxCustName").show();
    $("#dvPrcInfo").hide();
    $("#dvFees").hide();
}

function showTax(v) {

    if (v === "true") {
        $("#dvTax").show();
    }
    else {
        $("#t14").val("0.00");
        $("#dvTax").hide();
    }
}

function setState(v) {
 
    if (v === "1") { $("#divCaStuff").show(); } else { $("#divCaStuff").hide(); }
}


function set30Hold()
{
    let currentDate = new Date();
    let cDay = currentDate.getDate();
    let cMonth = currentDate.getMonth() + 2;
    let cYear = currentDate.getFullYear();

    var dt = pad(cMonth, 2) + "/" + pad(cDay, 2) + "/" + cYear;
    $("#t15").val(dt);
    $("#c15").prop('checked', true);
}

function set30Clear()
{
    $("#t15").val("");
    $("#c15").prop('checked', false);
}


function pad(num, size) {
    var s = "0" + num;
    return s.substr(s.length - size);
}


function setPptSeller(v)
{
    if (v === "true")
    {
        $("#c14").prop('checked', true);
        $("#ppt").val("1");
    } else
    {
        $("#c14").prop('checked', false);
        $("#ppt").val("0");
    }
}


function gunCust() {
    var c = $("#cus").val();
    var x = parseInt(c);
    if (x === 0) { $("#t35").val(""); }
}


function gunSupp() {
    var c = $("#sup").val();
    var x = parseInt(c);
    if (x === 0) { $("#t52").val(""); }
}


function gunFfl() {
    var c = $("#fcd").val();
    var x = parseInt(c);
    if (x === 0) { $("#t29").val(""); }
}









