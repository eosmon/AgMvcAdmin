$(document).ready(function () {
    $("#to1").attr('readonly', false);
    shwGunBase();
    //loadTestFulfillment();
});


$(document).ready(function () {
    var t = $("#sb32 option[value='9']").text(); //like this: option[value = "' + i + '"]').text();
    //alert(t);
});


$(function () {
    $("#to3").datepicker({
        onSelect: function (dateTxt) {
            $(this).valid();
            $("#fl7").text(dateTxt);
        }
    });
});

$(function () {
    $("#to6").datepicker({
        onSelect: function () { }
    });
});


/* SEARCH CUSTOMER AUTOCOMPLETE  */
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
        $("#to1").autocomplete({
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

                                var reg = item.IsReg ? "Registered User" : "Inquiry Only";
                                var iDiv = item.ProfilePic.length > 0 ? "<img src=\"" + item.ProfilePic + "\" class=\"cust-ui-pic\" />" : "<div style='text-align:center'>Customer Photo Not Found</div>";

                                var d = "<div class=\"grid-hide-item\">";
                                d += "<div class=\"grid-hide-item-pic\">" + iDiv + "</div>";
                                d += "<div class=\"grid-hide-frame\">";
                                d += "<div class=\"cust-ui-name\">" + item.StrFullName + "</div>";
                                d += "<div><b>" + item.StrFullAddr + "</b></div>";
                                d += "<div>" + item.StrEmailPhn + "</div>";
                                d += "<div>" + item.StrCustType + " Customer</div>";
                                d += "<div><span>" + reg + "</span><span style=\"padding-left:15px\" class=\"link11Green\" href=\"#\" onclick=\"editCustomer('" + item.CustomerId +"')\">Update</span></div>";
                                d += "</div>";
                                d += "</div>";

                                return { label: d, value: item };
                            }));

                        }
                    },
                    complete: function () {
                        $("body").scrollTop($(document).height());
                        $("#ui-id-3").append("<li class=\"ui-menu-item\" style=\"text-align:center !important; height:30px; font-size:1.2em\"><a class=\"ui-Link\" onclick=\"newCustomer()\">Create New Customer</a></li>");
                        $("#ui-id-3").append("<li class=\"ui-menu-item\" style=\"text-align:center !important; height:30px; font-size:1.2em\"><a class=\"ui-Link\" onclick=\"readBySwipe()\">Create Customer From ID</a></li>");
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
                $("#custId").val(id);
                getCustomerInfo(id);
            }
        });
    });


    // Autocomplete Gun Search
    $(function () {
        $("#tb6").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Orders/SearchOrderGuns",
                    data: "{ mfg: '" + $("#sb3").val() + "', typ: '" + $("#sb14").val() + "', cal: '" + $("#sb4").val() + "', cok: '" + $("#sb15").val() + "', str: '" + $("#tb6").val() + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        response($.map(data,
                            function (item) { return { label: getStr(item), value: item }; }));
                    },
                    complete: function () { $("body").scrollTop($(document).height()); }
                });
            },
            focus: function (event, ui) {
                this.value = "";
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();
                setItemSch(ui);
            }
        });
    });


    // Autocomplete Ammo Search
    $(function () {
        $("#ta6").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Orders/SearchOrderAmmo",
                    data: "{ mfg: '" + $("#sb3").val() + "', cal: '" + $("#sb4").val() + "', atp: '" + $("#sb13").val() + "', btp: '" + $("#sb7").val() + "', str: '" + $("#ta6").val() + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        response($.map(data,
                            function (item) { return { label: getStr(item), value: item }; }));
                    },
                    complete: function () { $("body").scrollTop($(document).height()); }
                });
            },
            focus: function (event, ui) {
                this.value = "";
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();
                setItemSch(ui);
            }
        });
    });


    // Autocomplete Merchandise Search
    $(function () {
        $("#tm6").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Orders/SearchOrderMerchandise",
                    data: "{ mfg: '" + $("#sb3").val() + "', cat: '" + $("#sb8").val() + "', str: '" + $("#tm6").val() + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        response($.map(data,
                            function (item) { return { label: getStr(item), value: item }; }));
                    },
                    complete: function () { $("body").scrollTop($(document).height()); }
                });
            },
            focus: function (event, ui) {
                this.value = "";
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();
                setItemSch(ui);
            }
        });
    });


    // Autocomplete Service Gun Search
    $(function () {
        $("#tb28").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Orders/SearchOrderGuns",
                    data: "{ mfg: '" + $("#sb3").val() + "', str: '" + $("#tb28").val() + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        response($.map(data,
                            function (item) { return { label: getStr(item), value: item }; }));
                    },
                    complete: function () { $("body").scrollTop($(document).height()); }
                });
            },
            focus: function (event, ui) {
                this.value = "";
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();
                clearSpecs();
                SetGunSpecs(ui);

            }
        });
    });


    // Autocomplete FFL Name
    $(function () {
        $("#tb26").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Inventory/FflByName",
                    data: "{ search: '" + request.term + "', state: '" + $("#sb25").val() + "' }",
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
                $("#tb46").val(em);
                $("#tb26").val(txt);

            }
        });
    });


    // Autocomplete Pickup FFL
    $(function () {
        $("#tb48").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Inventory/FflByName",
                    data: "{ search: '" + request.term + "', state: '" + $("#sb33").val() + "' }",
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

                var fc = ui.item.value.FflCode;
                var n = ui.item.value.TradeName;
                var a = ui.item.value.FflAddress;
                var c = ui.item.value.FflCityStZip;
                var fn = ui.item.value.FflNumber;
                var em = ui.item.value.FflEmail;

                var exp = ui.item.value.FflExpires;
                var lof = ui.item.value.FflOnFile;
                var vld = ui.item.value.FflIsValid;
                var onf = lof ? "Yes" : "No";

                $("#pfc").val(fc);

                if (!vld) { exp = exp + " (EXPIRED)"; }

                var txt = n + ' ' + a + ' ' + c + ' ' + fn;
                $("#tb46").val(em);
                $("#tb48").val(txt);

            }
        });
    });





})(jQuery);

function flushExistingOrder()
{
    clearOrder();
    $("#dvPymtGrid").empty();
    $("#dvNoPymt").empty();
}

function getOrder(oid) {

    $("#oid").val(oid);

    var fd = new FormData();
    fd.append("oid", oid);

    flushExistingOrder();

    return $.ajax({
        cache: false,
        url: "/Orders/GetOrder",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {

            var sct = d.SectionCount.Transactions;
            var scc = d.SectionCount.Cart;
            var sca = d.SectionCount.Address;
            var scl = d.SectionCount.LockModel;
            var scd = d.SectionCount.DistributorUnits;

            var con = d.CustAddress.OrgName;
            var cfn = d.CustAddress.FirstName;
            var cln = d.CustAddress.LastName;
            var cad = d.CustAddress.Address;
            var csu = d.CustAddress.Suite;
            var cty = d.CustAddress.City;
            var cst = d.CustAddress.StateName;
            var czp = d.CustAddress.ZipCode;
            var cex = d.CustAddress.ZipExt;
            var cph = d.CustAddress.Phone;
            var cem = d.CustAddress.EmailAddress;

            var son = d.ShopAddress.OrgName;
            var sad = d.ShopAddress.Address;
            var sty = d.ShopAddress.City;
            var sst = d.ShopAddress.StateName;
            var szp = d.ShopAddress.ZipCode;
            var sph = d.ShopAddress.Phone;
            var sem = d.ShopAddress.EmailAddress;

            var odt = d.StrOrderDate;
            var pay = d.Payments;

            /* BASIC ORDER INFORMATION */

            var sea = sad + ", " + sty + ", " + sst + " " + szp;
            var oph = "P. " + sph;

            $("#fl1").text(son);
            $("#fl2").text(sea);
            $("#fl3").text(oph);

            var xfl = cfn + " " + cln;
            var xad = cad + ", " + cty + ", " + cst + " " + czp;
            var xpe = "P. " + cph + " E." + cem;

            $("#fl4").text(xfl);
            $("#fl5").text(xad);
            $("#fl6").text(xpe);
            $("#fl7").text(odt);
            $("#fl9").text("Nick Nelson");

            $("#dvt1").hide();
            $("#dvt2").show();

            /* NO TRANSACTIONS: DO NOT CONTINUE */
            if (sct === 0) { return; }

            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#dvFinal").show(); 
        }
    });
}





function testOrder() {

    $("#oid").val("1"); // Order Id
    $("#custId").val("1"); // Cust Id

    var fd = new FormData();
    fd.append("oid", "1");

    $('#dvPymtGrid').empty();
    $('#dvNoPymt').empty();

    return $.ajax({
        cache: false,
        url: "/Orders/GetOrder",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {

            var sct = d.SectionCount.Transactions;
            var scc = d.SectionCount.Cart;
            var sca = d.SectionCount.Address;
            var scl = d.SectionCount.LockModel;
            var scd = d.SectionCount.DistributorUnits;

            var con = d.CustAddress.OrgName;
            var cfn = d.CustAddress.FirstName;
            var cln = d.CustAddress.LastName;
            var cad = d.CustAddress.Address;
            var csu = d.CustAddress.Suite;
            var cty = d.CustAddress.City;
            var cst = d.CustAddress.StateName;
            var czp = d.CustAddress.ZipCode;
            var cex = d.CustAddress.ZipExt;
            var cph = d.CustAddress.Phone;
            var cem = d.CustAddress.EmailAddress;

            var son = d.ShopAddress.OrgName;
            var sad = d.ShopAddress.Address;
            var sty = d.ShopAddress.City;
            var sst = d.ShopAddress.StateName;
            var szp = d.ShopAddress.ZipCode;
            var sph = d.ShopAddress.Phone;
            var sem = d.ShopAddress.EmailAddress;

            var odt = d.StrOrderDate;
            var pay = d.Payments;

            /* BASIC ORDER INFORMATION */

            var sea = sad + ", " + sty + ", " + sst + " " + szp;
            var oph = "P. " + sph;

            $("#fl1").text(son);
            $("#fl2").text(sea);
            $("#fl3").text(oph);

            var xfl = cfn + " " + cln;
            var xad = cad + ", " + cty + ", " + cst + " " + czp;
            var xpe = "P. " + cph + " E." + cem;

            $("#fl4").text(xfl);
            $("#fl5").text(xad);
            $("#fl6").text(xpe);
            $("#fl7").text(odt);
            $("#fl9").text("Nick Nelson");

            $("#dvt1").hide();
            $("#dvt2").show();

            /* NO TRANSACTIONS: DO NOT CONTINUE */
            if (sct === 0) { return; }

            loadOrder(d);
            //setOrderTransactions(d.OrderTransactions);
            //setOrderTotals(d);
            //renderPayGrid(pay);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
        }
    });
}

function setPrintInvoice()
{
    var v = $("#oid").val();

    var fd = new FormData();
    fd.append("oid", v);

    return $.ajax({
        cache: false,
        url: "/Orders/SaveInvoice",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {

            var oid = d.OrderId;
            var iqt = d.IsQuote;
            var sub = toBucks(d.SubTotal);
            var tax = toBucks(d.SalesTax);
            var ttl = toBucks(d.OrderTotal);
            var bpd = toBucks(d.BalancePaid);
            var due = toBucks(d.BalanceDue);
            var ocd = d.OrderCode;
            var hdr = d.Header;
            var rep = d.SalesRep;
            var pay = d.PayMethods;
            var ful = d.FulfillTypes;
            var ffl = d.FflCode;
            var tcd = d.TermsCond;
            var lib = d.LiabilityTxt;
            var dat = d.StrOrderDate;
            var url = d.InvoiceUrl;
 
            var con = d.CustAddress.OrgName;
            var cfn = d.CustAddress.FirstName;
            var cln = d.CustAddress.LastName;
            var cad = d.CustAddress.Address;
            var cty = d.CustAddress.City;
            var cst = d.CustAddress.StateName;
            var czp = d.CustAddress.ZipCode;
            var cph = d.CustAddress.Phone;
            var cem = d.CustAddress.EmailAddress;

            var son = d.ShopAddress.OrgName;
            var sad = d.ShopAddress.Address;
            var sty = d.ShopAddress.City;
            var sst = d.ShopAddress.StateName;
            var szp = d.ShopAddress.ZipCode;
            var sph = d.ShopAddress.Phone;
            var sem = d.ShopAddress.EmailAddress;

            var nsa = sty + ", " + sst + " " + szp;

            $("#spOrdNum").text(ocd);
            $("#dvInvTtl").text(hdr);
            $("#spOrdDat").text(dat);
            $("#dvBzAddr").text(sad);
            $("#dvBzCszp").text(nsa);
            $("#spBzPhon").text(sph);
            $("#spBzFfl").text(ffl);
            $("#dvBzEmal").text(sem);

            var cxn = cfn + " " + cln;
            var ccz = cty + ", " + cst + " " + czp;

            $("#dvCsNam").text(cxn);
            $("#dvCsAdr").text(cad);
            $("#dvCsCsz").text(ccz);
            $("#spCsPhn").text(cph);
            $("#spCsEml").text(cem);

            $("#dvSalesRp").text(rep);
            $("#dvFulfill").text(ful);
            $("#dvPayMthd").text(pay);

            $("#dvOrdSub").text(sub);
            $("#dvOrdTax").text(tax);
            $("#dvOrdTtl").text(ttl);
            $("#dvOrdApd").text(bpd);
            $("#dvOrdAdu").text(due);

            $("#dvTrmCond").text(tcd);
            $("#spLiabTxt").text(lib);
            

            var t = d.OrderTransactions[0].OrderCartItems;

            //var bgc = "#FFFFFF";

            $.each(t, function (i, item) {

                var cTrw = item.IsTaxRow;
                var cRid = item.RowId;
                var cUnt = cTrw ? "" : item.Units;
                var cPrc = cTrw ? "" : toBucks(item.Price);
                var cExt = cTrw ? "" : toBucks(item.Extension);
                var cCat = item.Category;
                var cSid = item.SrcInvDesc;
                var cIds = item.ItemDesc;
                var cTax = item.Taxable;
                var cIsr = item.IsSellerRow;
                var cIsh = item.IsShipRow;
                var cIpu = item.IsPickupRow;
                var cIdl = item.IsDeliverRow;


                var c = "<div class=\"ord-print-row\">"
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cRid + ".</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cCat + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cSid + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black; text-align:left; padding-left:10px;\">" + cIds + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cUnt + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black; text-align:right; padding-right:10px;\">" + cPrc + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black; text-align:right; padding-right:10px;\">" + cExt + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black; border-right:solid 1px black;\">" + cTax + "</div>";
                c += "</div>";

                $("#dvPrintRows").append(c);

                //bgc = bgc === "#E3E5E7" ? "#FFFFFF" : "#E3E5E7";
               
            });

            window.open(url);

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}

function setOrderList() {

    $("#dvOrdList").empty();

    var fd = new FormData();
    var rc = "#DCDCDC";

    return $.ajax({
        cache: false,
        url: "/Orders/AllOrders",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {

            $.each(d, function (i, item) {

                var oid = item.OrderId;
                var ttl = toBucks(item.OrderTotal);
                var bpd = toBucks(item.BalancePaid);
                var due = toBucks(item.BalanceDue);
                var ocd = item.OrderCode;
                var dat = item.StrOrderDate;
                var loc = item.StrLocation;
                var cus = item.StrCustName;
                var phn = item.StrCustPhone;

                var c = "<div class=\"ord-list-row\" style=\"background-color:" + rc + "\">"
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\"><a class=\"link12Blue\" href=../orders/cookorder?edit="+oid+">edit</a></div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + ocd + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + dat + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + loc + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cus + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + phn + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + ttl + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + bpd + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + due + "</div>";
                c += "<div class=\"ord-row\" style=\"border-left:solid 1px black; border-right:solid 1px black;\"><a class=\"link12Blue\"href=\"#\" onclick=\"nixOrderFromList('" + oid + "');\">delete</a></div>";
                c += "</div>";

                rc = rc === "#FFFFFF" ? "#DCDCDC" : "#FFFFFF";

                $("#dvOrdList").append(c);
            });




        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}

//function editOrder(v) {
//    window.location.href = "../orders/cookorder";
//    getOrder(v);
//}



//function testPrint()
//{
//    var v = $("#oid").val();

//    var fd = new FormData();
//    fd.append("oid", v);

//    return $.ajax({
//        cache: false,
//        url: "/Orders/RenderPrintInvoice",
//        type: "POST",
//        contentType: false,
//        processData: false,
//        data: fd,
//        success: function (d) {

//            streamToPdf(d);

//        },
//        error: function (err, data) {
//            alert(err);
//        },
//        complete: function () {

//        }
//    });
//}


function streamToPdf(d)
{

    var sub = toBucks(d.SubTotal);
    var tax = toBucks(d.SalesTax);
    var ttl = toBucks(d.OrderTotal);
    var bpd = toBucks(d.BalancePaid);
    var due = toBucks(d.BalanceDue);
    var ocd = d.OrderCode;
    var hdr = d.Header;
    var rep = d.SalesRep;
    var pay = d.PayMethods;
    var ful = d.FulfillTypes;
    var ffl = d.FflCode;
    var tcd = d.TermsCond;
    var lib = d.LiabilityTxt;
    var dat = d.StrOrderDate;

    var con = d.CustAddress.OrgName;
    var cfn = d.CustAddress.FirstName;
    var cln = d.CustAddress.LastName;
    var cad = d.CustAddress.Address;
    var cty = d.CustAddress.City;
    var cst = d.CustAddress.StateName;
    var czp = d.CustAddress.ZipCode;
    var cph = d.CustAddress.Phone;
    var cem = d.CustAddress.EmailAddress;

    var son = d.ShopAddress.OrgName;
    var sad = d.ShopAddress.Address;
    var sty = d.ShopAddress.City;
    var sst = d.ShopAddress.StateName;
    var szp = d.ShopAddress.ZipCode;
    var sph = d.ShopAddress.Phone;
    var sem = d.ShopAddress.EmailAddress;

    var nsa = sty + ", " + sst + " " + szp;
    var cxn = cfn + " " + cln;
    var ccz = cty + ", " + cst + " " + czp;
    var bgc = "#FFFFFF";
 
    var t = d.OrderTransactions[0].OrderCartItems;

    var b = "";

    b += "<div id=\"dvPrint\" style=\"padding - bottom: 15px; \">";
    b += "<div class=\"ord-print-top\">";
    b += "<div style=\"width:100%; border:solid 1px black; height:155px;\">";
    b += "<div style=\"width:100%; background-color:black; height:40px; color:white;\">";
    b += "<div class=\"ord-print-ord-num\">Order #: <span id=\"spOrdNum\" style=\"font-weight:bold;\">"+ocd+"</span></div>";
    b += "<div class=\"ord-print-heading\" id=\"dvInvTtl\">"+hdr+"</div>";
    b += "</div>";
    b += "<div class=\"ord-print-biz-info\">";
    b += "<div style=\"position:relative; top:-35px; left:5px;\"><img src=\"~/Common/Images/Print_Logo.png\" style=\"width:165px; height:auto;\" /></div>";
    b += "<div class=\"ord-print-biz-blk\">";
    b += "<div id=\"dvBzAddr\" class=\"ord-print-addr\">"+sad+"</div>";
    b += "<div id=\"dvBzCszp\" class=\"ord-print-addr\">"+nsa+"</div>";
    b += "<div class=\"ord-print-addr\">Phone: <span id=\"spBzPhon\">"+sph+"</span></div>";
    b += "<div style=\"display:inline-block; width:100%; font-size:.8em;\">";
    b += "<div style=\"display:inline-block; width:45%; text-align:right; padding-right:8px;\">www.hcpawn.com</div>";
    b += "<div style=\"display:inline-block; width:45%; text-align:left; padding-left:10px;\">FFL# <span id=\"spBzFfl\">"+ffl+"</span></div>";
    b += "</div>";
    b += "<div style=\"padding-top:5px; font-weight:bold;\">** WE BUY USED GUNS **</div>";
    b += "<div id=\"dvBzEmal\" style=\"padding-top:3px; font-size:.8em;\">"+sem+"</div>";
    b += "</div>";
    b += "</div>";
    b += "<div class=\"ord-print-cus-blk\">";
    b += "<div style=\"font-weight:bold;\">Customer:</div>";
    b += "<div id=\"dvCsNam\">"+cxn+"</div>";
    b += "<div id=\"dvCsAdr\">"+cad+"</div>";
    b += "<div id=\"dvCsCsz\">"+ccz+"</div>";
    b += "<div>Phone: <span id=\"spCsPhn\">"+cph+"</span> Email: <span id=\"spCsEml\">"+cem+"</span></div>";
    b += "</div>";
    b += "<div class=\"ord-print-date\">Date: <span style=\"font-weight:bold;\" id=\"spOrdDat\">"+dat+"</span></div>";
    b += "</div>";
    b += "<div style=\"padding-top:25px; padding-bottom:25px;\">";
    b += "<div class=\"ord-print-tdt\">";
    b += "<div class=\"ord-print-ttl\" style=\"border-left: 0\">Sold By:</div>";
    b += "<div class=\"ord-print-txt\" id=\"dvSalesRp\">"+rep+"</div>";
    b += "<div class=\"ord-print-ttl\">Order Fulfillment:</div>";
    b += "<div class=\"ord-print-txt\" id=\"dvFulfill\">"+ful+"</div>";
    b += "<div class=\"ord-print-ttl\">Payment Method:</div>";
    b += "<div class=\"ord-print-txt\" id=\"dvPayMthd\">"+pay+"</div>";
    b += "</div>";
    b += "</div>";
    b += "<div id=\"dvPrintGrid\" style=\"font-size:13px;\">";
    b += "<div class=\"ord-print-hdr\" id=\"dvPrtGrdHdr\">";
    b += "<div class=\"ord-print\">Item</div>";
    b += "<div class=\"ord-print\">Category</div>";
    b += "<div class=\"ord-print\">Inventory #</div>";
    b += "<div class=\"ord-print\" style=\"text-align:left; padding-left:10px;\">Description</div>";
    b += "<div class=\"ord-print\">Units</div>";
    b += "<div class=\"ord-print\">Price</div>";
    b += "<div class=\"ord-print\">Amount</div>";
    b += "<div class=\"ord-print\" style=\"border-right:solid 1px black;\">Tax</div>";
    b += "</div>";
    b += "<div id=\"dvPrintRows\">";

    $.each(t, function (i, item) {

        var cTrw = item.IsTaxRow;
        var cRid = item.RowId;
        var cUnt = cTrw ? "" : item.Units;
        var cPrc = cTrw ? "" : toBucks(item.Price);
        var cExt = cTrw ? "" : toBucks(item.Extension);
        var cCat = item.Category;
        var cSid = item.SrcInvDesc;
        var cIds = item.ItemDesc;
        var cTax = item.Taxable;
        var cIsr = item.IsSellerRow;
        var cIsh = item.IsShipRow;
        var cIpu = item.IsPickupRow;
        var cIdl = item.IsDeliverRow;


        b += "<div class=\"ord-print-row\" style=\"background-color:" + bgc + "\">"
        b += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cRid + ".</div>";
        b += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cCat + "</div>";
        b += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cSid + "</div>";
        b += "<div class=\"ord-row\" style=\"border-left:solid 1px black; text-align:left; padding-left:10px;\">" + cIds + "</div>";
        b += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cUnt + "</div>";
        b += "<div class=\"ord-row\" style=\"border-left:solid 1px black; text-align:right; padding-right:10px;\">" + cPrc + "</div>";
        b += "<div class=\"ord-row\" style=\"border-left:solid 1px black; text-align:right; padding-right:10px;\">" + cExt + "</div>";
        b += "<div class=\"ord-row\" style=\"border-left:solid 1px black; border-right:solid 1px black;\">" + cTax + "</div>";
        b += "</div>";

        bgc = bgc === "#E3E5E7" ? "#FFFFFF" : "#E3E5E7";

    });

    b += "</div>";
    b += "<div id=\"dvPrintBass\">";
    b += "<div class=\"ord-print-bass\">";
    b += "<div style=\"border-left:solid 1px black;\">&nbsp;</div>";
    b += "<div style=\"border-left:solid 1px black;\">&nbsp;</div>";
    b += "<div style=\"border-left:solid 1px black;\">&nbsp;</div>";
    b += "<div style=\"border-left:solid 1px black;\">&nbsp;</div>";
    b += "<div style=\"border-left:solid 1px black;\">&nbsp;</div>";
    b += "<div style=\"border-left:solid 1px black;\">&nbsp;</div>";
    b += "<div style=\"border-left:solid 1px black;\">&nbsp;</div>";
    b += "<div style=\"border-left:solid 1px black; border-right:solid 1px black;\">&nbsp;</div>";
    b += "</div>";
    b += "<div class=\"ord-print-bass\">";
    b += "<div class=\"ord-print-tc-ttl\">CUSTOMER TERMS & CONDITIONS</div>";
    b += "<div class=\"ord-print-bdr-lb\"></div>";
    b += "<div class=\"ord-print-bdr-lb\"></div>";
    b += "<div class=\"ord-print-bdr-lb\"></div>";
    b += "<div class=\"ord-print-bdr-lb\" style=\"border-right:solid 1px black;\"></div>";
    b += "</div>";
    b += "<div class=\"ord-print-bass\">";
    b += "<div class=\"ord-print-tc-txt\" id=\"dvTrmCond\">"+tcd+"</div>";
    b += "<div style=\"grid-column-start: 5; grid-column-end: 7; border-left:solid 1px black;\">";
    b += "<div class=\"ord-print-totals-txt\">Sub-Total: </div>";
    b += "<div class=\"ord-print-totals-txt\">Sales Tax: </div>";
    b += "<div class=\"ord-print-totals-txt\">Order Total: </div>";
    b += "<div class=\"ord-print-totals-txt\">Amount Paid: </div>";
    b += "<div class=\"ord-print-totals-txt\">Balance Due: </div>";
    b += "</div>";
    b += "<div style=\"border-left:solid 1px black;\">";
    b += "<div class=\"ord-print-totals-amt\" id=\"dvOrdSub\">" + sub + "</div>";
    b += "<div class=\"ord-print-totals-amt\" id=\"dvOrdTax\">" + tax + "</div>";
    b += "<div class=\"ord-print-totals-amt\" id=\"dvOrdTtl\">" + ttl + "</div>";
    b += "<div class=\"ord-print-totals-amt\" id=\"dvOrdApd\">" + bpd + "</div>";
    b += "<div class=\"ord-print-totals-amt\" id=\"dvOrdAdu\">" + due + "</div>";
    b += "</div>";
    b += "<div style=\"border-right:solid 1px black;\">";
    b += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
    b += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
    b += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
    b += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
    b += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
    b += "</div>";
    b += "</div>";
    b += "</div>";
    b += "<div style=\"width:100%\">";
    b += "<div style=\"padding-left:5px; padding-top:10px; font-size:13px;\"><span style=\"font-weight:bold\">LIABILITY: </span> <span id=\"spLiabTxt\">"+lib+"</span></div>";
    b += "</div>";
    b += "<div class=\"ord-sig-line\" style=\"padding-top:25px; padding-left:5px;\">";
    b += "<div style=\"height:25px;\">I have read and agree to the these conditions:</div>";
    b += "<div style=\"border-bottom:solid 1px black; border-right:solid 1px black\">&nbsp;</div>";
    b += "<div style=\"border-bottom:solid 1px black\">&nbsp;</div>";
    b += "</div>";
    b += "<div class=\"ord-sig-line\" style=\"padding-top:3px; padding-left:5px;\">";
    b += "<div style=\"height:25px;\"></div>";
    b += "<div style=\"font-weight:bold;\">Customer Signature</div>";
    b += "<div style=\"font-weight:bold;\">Date</div>";
    b += "</div>";
    b += "</div>";
    b += "</div>";
    b += "</div>";


}



function loadOrder(d)
{
    $('#dvCartHolder').empty();
    $('#dvPymtGrid').empty();

    setOrderBasic(d);
    setOrderTransactions(d.OrderId, d.OrderTransactions);
    setOrderTotals(d);
    renderPayGrid(d.Payments);
    $("#dvFinal").show();
}


function setPymtOptions(v)
{
    $("#dvCard4").hide();
    $("#dvAprCd").hide();
    $("#dvChkNm").hide();

    switch (v)
    {
        case "2":
        case "3":
        case "4":
        case "5":
            $("#dvCard4").show();
            $("#dvAprCd").show();
            break;
        case "6":
        case "7":
        case "8":
        case "9":
        case "10":
            $("#dvChkNm").show();
            break;
    }
}

function setOrderBasic(d)
{
    clearOrder();

    //OrderId = oid;
    var cid = d.CustomerId;
    var rid = d.SalesRepId;
    var lid = d.LocationId;
    var otp = d.OrderTypeId;
    var odt = d.StrOrderDate;

    var onm = d.CustAddress.OrgName;
    var fnm = d.CustAddress.FirstName;
    var lnm = d.CustAddress.LastName;
    var adr = d.CustAddress.Address;
    var ste = d.CustAddress.Suite;
    var cty = d.CustAddress.City;
    var sta = d.CustAddress.StateName;
    var zip = d.CustAddress.ZipCode;
    var ext = d.CustAddress.ZipExt;
    var phn = d.CustAddress.Phone;
    var eml = d.CustAddress.EmailAddress;

    $("#to3").val(odt);

    $("#sn1").val(lid);
    $("#sn2").val(rid);
    $("#sn3").val(otp);
 

    setCustInfo(cid, fnm, lnm, ste, adr, cty, sta, zip, ext, phn, eml);
}


function setOrderTotals(t)
{
    var sub = toBucks(t.SubTotal);
    var tax = toBucks(t.SalesTax);
    var exc = toBucks(t.ExciseTax);
    var otl = toBucks(t.OrderTotal);
    var bpd = toBucks(t.BalancePaid);
    var bdu = toBucks(t.BalanceDue);
    var loc = t.LocationId;

    $("#ordSub").text(sub);
    $("#ordTax").text(tax);
    $("#ordTot").text(otl);
    $("#ordDep").text(bpd);
    $("#ordBal").text(bdu);

    if (loc === 1 && t.ExciseTax > 0) {
        $("#ordCxt").text(exc);
        $("#dvCxt").show();

    }
    else {
        $("#ordCxt").text('$0.00');
        $("#dvCxt").hide();
    }

}

function setOrderTransactions(o, t)
{
    var oid = o;

    $.each(t, function (i, item) {

        var tid = item.TransactionId;
        var ftp = item.FulfillmentTypeId;
        var row = item.RowNumber;
        var hdr = item.OrderHeading;
        var nts = item.Notes;

        var sub = item.SubTotal;
        var tax = item.SalesTax;
        var exc = item.ExciseTax;
        var ttl = item.TransTotal;
        var ilc = item.IsCaDojCflc;
        var lid = item.LocationId;

        var cfl = item.CflcNumber;


        var b = "<div style=\"display:inline-block; width:100%;\" class=\"ord-grp\">";
        b += "<div style=\"display:inline-block; width:73%;\">" + hdr + "</div>"; 
        b += "<div class=\"ord-nav-left\"><a class=\"update-lnk\" onclick=\"editFulfillment('" + tid + "', '" + ftp + "')\">Edit Fulfillment</a></div>";
        b += "<div class=\"ord-nav-right\"><a class=\"update-lnk\" onclick=\"nixInvoiceTrans('" + oid + "', '" + tid + "')\">Delete Transaction</a></div>";
        b += "</div>";
        b += setOrderCartHeader();
        b += setOrderCartRows(item.OrderCartItems);
        b += "<div style=\"border-top:solid 2px black; width:100%; min-height:40px; background-color:#CCCCCC; border-bottom-right-radius: 4px; border-bottom-left-radius: 4px;\">";
        b += "<div class=\"cart-grid-ftr\" style=\"padding-bottom:20px;\">";
        b += "<div><b>Notes: </b>" + nts + "</div>";
        b += "<div style=\"padding-top:10px;\">";
        b += "<div style=\"text-align:right; height:20px;\">Sub-Total:</div>";
        b += "<div style=\"text-align:right; height:20px;\">Sales Tax:</div>";
        if (lid === 1 && exc > 0)
        {
            b += "<div style=\"text-align:right; height:20px;\">CA Excise Tax:</div>";
        }
        b += "<div style=\"text-align:right; height:20px;font-weight:bold;\">Transaction Total:</div>";
        b += "</div>"
        b += "<div style=\"padding-top:10px;\">";
        b += "<div style=\"text-align:left; padding-left:11px; height:20px;\">" + toBucks(sub) + "</div>";
        b += "<div style=\"text-align:left; padding-left:11px; height:20px;\">" + toBucks(tax) + "</div>";
        if (lid === 1 && exc > 0) {
            b += "<div style=\"text-align:left; padding-left:11px; height:20px;\">" + toBucks(exc) + "</div>";
        }
        b += "<div style=\"text-align:left; padding-left:11px; height:20px; font-weight:bold;\">" + toBucks(ttl) + "</div>";
        b += "</div>"
        b += "</div>"

        // insert CA DOJ Shipping here
        if (ilc)
        {
            b += "<div style=\"width:100%; padding-left:10px; padding-bottom:3px; color:black;\">";
            b += "<div style=\"display:inline-block; font-weight:bold; min-width:122px;text-align:right;\">CA DOJ Shipping #:</div>";
            b += "<div style=\"display:inline-block; padding-top:5px; padding-left:5px;\"><input class=\"ag-control input-sm\" type=\"text\" style=\"min-width:287px;width:100%;\" name='trDojCflc" + tid + "' id='trDojCflc" + tid + "' value='" + cfl + "' ></div>";
            b += "<div style=\"display:inline-block; padding-left:5px;\"><button type=\"button\" class=\"btn btn-blue2 btn-xs\" id='trUpdCflcBtn" + tid + "' name='trUpdCflcBtn" + tid + "' style=\"width: 110px; margin-top:-2px;\">Update CFLC</button></div>"
            b += "</div>"
        }

        b += "<div style=\"width:100%; padding-left:10px; padding-bottom:10px; color:black;\">";
        b += "<div style=\"display:inline-block; font-weight:bold;\">Add Transaction Fee: </div>";
        b += "<div style=\"display:inline-block; padding-left:5px;\"><select class=\"ag-control input-sm\" id='trAddFee" + tid + "' name = 'trAddFee" + tid + "' onchange=\"setAddFeeTxt(this.value,'"+tid+"')\" >" + setMenuOptions(item.MenuOrderFees, 0) + "</select></div>";
        b += "<div style=\"display:inline-block; padding-top:5px; padding-left:5px; max-width:35px;\"><input class=\"ag-control input-sm\" type=\"text\" style=\"width:100%; text-align:center\" name='trAddFeeUnt" + tid + "' id='trAddFeeUnt" + tid + "' value=\"1\"  onkeypress=\"return isNumber(event)\"></div>";
        b += "<div style=\"display:inline-block; padding-top:5px; padding-left:5px; max-width:80px;\"><input class=\"ag-control input-sm\" type=\"text\" style=\"width:100%; text-align:center\" name='trAddFeePrc" + tid + "' id='trAddFeePrc" + tid + "' value=\"$0.00\" onkeypress=\"return isDecimal(event)\"></div>";
        b += "<div style=\"display:inline-block; padding-top:5px; padding-left:5px; width:250px;\"><input class=\"ag-control input-sm\" type=\"text\" style=\"width:100%\" name='trAddFeeDsc"+tid+"' id='trAddFeeDsc"+tid+"'></div>";
        b += "<div style=\"display:inline-block; padding-left:5px;\"><button type=\"button\" class=\"btn btn-blue2 btn-xs\" id='trAddBtn" + tid + "' name='trAddBtn" + tid + "' style=\"width: 60px; margin-top:-2px;\" onclick=\"addCartFee('"+tid+"')\">Add Fee</button></div>"
        b += "</div>"

        b+= "</div>"
        $('#dvCartHolder').append(b);
    });
}

function setOrderCartHeader()
{
    var b = "<div class=\"cart-grid-hdr\" id=\"dvCartGrid\">";
        b += "<div></div>";
    b += "<div></div>";
    b += "<div class=\"srch-hdr\" style=\"text-align: center\">Group</div>";
    b += "<div class=\"srch-hdr\" style=\"text-align: center\">Inventory</div>";
    b += "<div class=\"srch-hdr\" style=\"text-align: center\">Units</div>";
    b += "<div class=\"srch-hdr\" style=\"text-align: center\">Description</div>";
    b += "<div class=\"srch-hdr\" style=\"text-align: center\">Price</div>";
    b += "<div class=\"srch-hdr\" style=\"text-align: center\">Tax Rate/Amt</div>";
    b += "<div class=\"srch-hdr\" style=\"text-align: center\">Extension</div>";
    b += "</div>";
    return b;
}

function setOrderCartRows(t)
{
    var b = "";
 
    $.each(t, function (i, item) {

        var cid = item.CartItemId;
        var cag = item.CategoryId;
        var tid = item.TransactionId;
        var rid = item.RowId;
        var cat = item.Category;
        var itx = item.IsTaxable;
        var tax = itx ? "Y" : "N";
        var trw = item.IsTaxRow;
        var prc = item.Price.toFixed(2); //toBucks(item.Price);
        var ext = toBucks(item.Extension);
        var unt = item.Units;
        var ttp = item.TransTypeId;
        var ibi = item.ItemBasisID;
        var did = item.DistributorId;
        var tsi = item.TaxStatusId;
        var trt = item.TaxRate.toFixed(4);
        var tdu = item.TaxDue.toFixed(2);
        var ftp = item.FullfillmentTypeID; 
        var iff = ftp === 1;

        var lmk = item.LockMakeID;
        var lmd = item.LockModelID;

        var gun = item.IsGunRow;
        var amo = item.IsAmmoRow;
        var mch = item.IsMrchRow;
        var fee = item.IsFee;
        var grw = item.IsInvMenuRow;
        var lok = item.IsLock;

        var ttl = item.ItemTitle;
        var dsc = item.ItemDesc;
        var ser = item.SerialNumber;
        var svc = item.SrcInvDesc;
        var fid = item.FeeID;

        var ash = item.AddressShipping;
        var adl = item.AddressDelivery;
        var apu = item.AddressPickup;
        var asl = item.AddressSeller;




        var fsd = item.FsdOptionID;
        let f1 = [2, 3, 5];
        let cfd = f1.includes(fsd);

        var stl = ttl;


        if (gun || amo || mch)
        {
            //var nds = gun ? dsc + " S/N: <b>" + ser + "</b>" : dsc;
            stl = "<div><a class=\"link12Blue\" id='ordTtl" + cid + "'>" + ttl + "</a></div><div>" + dsc + "</div>" 
        }


        b += "<div class=\"cart-grid-row\">"; 
        b += "<div class=\"cart-row\" style=\"font-weight:bold; padding-top:10px;\">" + rid + ".</div>";
        if (fee || trw) {
            b += "<div class=\"cart-row\" style=\"padding-top:10px;\"><a class=\"uploadTxt\" href=\"#\" onclick=\"nixCartRow('" + cid + "')\">delete</a></div>";
        }
        else
        {
            b += "<div class=\"cart-row\" style=\"padding-top:10px;\"><a class=\"uploadTxt\" href=\"#\" onclick=\"showEdit('" + tid + "', '" + ttp + "')\">edit</a></div>";
        }

        b += "<div class=\"cart-row\" style=\"padding-top:10px;\">" + cat + "</div>";
        b += "<div class=\"cart-row\" style=\"padding-top:10px; padding-left:10px; padding-right:10px; text-align:left;\">";
        if (fee) { b += svc; }
        if (!fee && ttp==103) { b += svc; }
 
        if (!fee && grw) {
            b += "<div>";
            b += "<select id='mSup" + cid + "' name='mSup" + cid + "' onchange=\"setSupplier('"+cid+"', this.value)\">";
            b += setMenuOptions(item.MenuSupplier, did)
            b += "</select>";
            b += "</div>";
            if (ibi > 0) {
                b += "<div>";
                b += "<select id='mItm" + cid + "' name='mItm" + cid + "'  onchange=\"setInventoryItem('" + cid +"', this.value)\">";
                b += setMenuOptions(item.MenuInventoryItem, ibi)
                b += "</select>";
                b += "</div>";
            }
        }
        if (cag>0) {
            b += "<div style=\"padding-bottom:5px;\">";
            b += "<select id='mSup" + cid + "' name='mSup" + cid + "' onchange=\"setTaxStatus('" + cid + "', this.value)\">";
            b += setTaxMenuOptions(item.MenuTaxOptions, tsi, ttp)
            b += "</select>";
            b += "</div>";
        }
 
        b += "</div>";
        if (fee) {
            b += "<div class=\"cart-row\" style=\"padding-top:5px; padding-left:10px; padding-right:10px;\"><input type=\"text\" style=\"width:100%; text-align:center\" name=\"Units\" id=\"tb33\" value='" + unt + "' onkeypress=\"return isNumber(event)\" onchange=\"setUnits('"+cid+"', this.value)\"></div>";
        }
        else
        {
            b += "<div class=\"cart-row\" style=\"padding-top:10px; font-weight:bold\">" + unt + "</div>";
        }
        b += "<div class=\"cart-row\" style=\"padding-top:10px; padding-left:5px; text-align:left;\">";
        b += "<div>" + stl + "</div>";
        if (fee) // show address: shipping, pickup, delivery
        {
            switch (fid) {
                case 11:
                    b += setCartAddress(ash);
                    break;
            }
        }
        if (cag > 0 && !fee && ttp === 103) // transfers only: no fees, category required
        {
            b += setCartAddress(asl);
        }
        if (gun && iff) {
            b += "<div style=\"padding-bottom:5px; padding-right:10px;\">";
            b += "<select id='mFsd" + cid + "' name='mFsd" + cid + "' onchange=\"setFsdOption('" + cid + "', this.value)\" style=\"width:100%;\">";
            b += "<option value=''>FSD COMPLIANCE</option>";
            b += setFsdMenuOptions(item.MenuFsdOptions, fsd)
            b += "</select>";
            b += "</div>";
            if (cfd)
            {
                b += "<div style=\"padding-bottom:5px; padding-right:10px;\">";
                b += "<select id='mLmk" + cid + "' name='mLmk" + cid + "' onchange=\"setFsdLockMake('" + cid + "', this.value)\" style=\"width:100%;\">";
                b += "<option value='0'>LOCK MAKE</option>";
                b += setFsdMenuOptions(item.MenuLockMakes, lmk)
                b += "</select>";
                b += "</div>";

                b += "<div style=\"padding-bottom:5px; padding-right:10px;\">";
                b += "<select id='mLmd" + cid + "' name='mLmd" + cid + "' onchange=\"setFsdLockModel('" + cid + "', this.value)\" style=\"width:100%;\">";
                b += "<option value='0'>LOCK MODEL</option>";
                b += setFsdMenuOptions(item.MenuLockModels, lmd)
                b += "</select>";
                b += "</div>";
            }
        }
        b += "</div>";
        if (!trw) {
            b += "<div class=\"cart-row\" style=\"padding-top:5px; padding-left:10px; padding-right:10px\"><input type=\"text\" style=\"width:100 %\" name=\"SchPrice\" id=\"tb34\" value='" + prc + "'  onkeypress=\"return isDecimal(event)\"  onchange=\"setPrice('" + cid + "', this.value)\"></div>";
            if (itx) {
                b += "<div class=\"cart-row\" style=\"padding-top:5px; padding-left:10px; padding-right:10px\">";
                b += "<div><input type=\"text\" style=\"width:100%\" name=\"TaxRate\" id=\"tb35\" value='" + trt + "'  onkeypress=\"return isDecimal(event)\"  onchange=\"setTaxRate('" + cid + "', this.value)\"></div>";
                b += "<div><input type=\"text\" style=\"width:100%; background:#CCCCCC\" name=\"TaxAmt\" id=\"tb36\" value='" + tdu + "' readonly></div>";
                b += "</div>";
            }
            else { b += "<div class=\"cart-row\"></div>"; }
            b += "<div class=\"cart-row-end\" style=\"padding-top:10px; text-align:left; padding-left:15px;\">" + ext + "</div>";
        }
        else
        {
            b += "<div class=\"cart-row\"></div>";
            b += "<div class=\"cart-row\"></div>";
            b += "<div class=\"cart-row\"></div>";
        }


        b += "</div>";
    });

    return b;

}

function setCartAddress(d)
{
    var a = "";

    var dsc = d.ItemDesc;
    var ful = d.FullName;
    var org = d.OrgName;
    var fnm = d.FirstName;
    var lnm = d.LastName;
    var adr = d.Address;
    var cty = d.City;
    var sta = d.StateName;
    var zip = d.ZipCode;
    var ext = d.ZipExt;
    var eml = d.EmailAddress;
    var phn = d.Phone;
    var ffl = d.StrFFL;

    var fzp = ext.length > 0 ? zip + "-" + ext : zip;
    var csz = cty + ", " + sta + " " + fzp;
    var nem = eml.length > 0 ? " E. " + eml : "";

    a += "<div style=\"font-weight:bold; font size:12px; padding-top:10px;\">" + dsc + "</div>";
    a += "<div style=\"font-size: .9em; padding-bottom:10px;\">";
    a += "<div>" + fnm + " " + lnm + "</div>";
    a += "<div>" + org + "</div>";
    a += "<div>" + adr + "</div>";
    a += "<div>" + csz + "</div>";
    a += "<div>P. " + phn + nem + "</div>";
    if (ffl.length > 0)
    {
        a += "<div>FFL: " + ffl + "</div>";
    }
    a += "</div>";

    return a;
}


function setFsdOption(c, f) {
    var fd = new FormData();
    fd.append("cid", c);
    fd.append("fsd", f);

    return $.ajax({
        cache: false,
        url: "/Orders/SetFsdOption",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}

function setFsdLockMake(c, m) {
    var fd = new FormData();
    fd.append("cid", c);
    fd.append("lmk", m);

    return $.ajax({
        cache: false,
        url: "/Orders/SetLockMake",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}

function setFsdLockModel(c, m) {
    var fd = new FormData();
    fd.append("cid", c);
    fd.append("lmd", m);

    return $.ajax({
        cache: false,
        url: "/Orders/SetLockModel",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}


function setSupplier(c, s)
{
    var fd = new FormData();
    fd.append("cid", c);
    fd.append("sid", s);

    return $.ajax({
        cache: false,
        url: "/Orders/SetCartSupplier",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}

function setInventoryItem(c, m) {
    var fd = new FormData();
    fd.append("cid", c);
    fd.append("mid", m);

    return $.ajax({
        cache: false,
        url: "/Orders/SetInventoryMenu",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}


function setUnits(c, u) {

    var fd = new FormData();
    fd.append("cid", c);
    fd.append("unt", u);

    return $.ajax({
        cache: false,
        url: "/Orders/SetCartUnits",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}

function setPrice(c, p) {

    var fd = new FormData();
    fd.append("cid", c);
    fd.append("prc", p);

    return $.ajax({
        cache: false,
        url: "/Orders/SetCartPrice",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}


function setTaxRate(c, r) {

    var fd = new FormData();
    fd.append("cid", c);
    fd.append("rat", r);

    return $.ajax({
        cache: false,
        url: "/Orders/SetTaxRate",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}



function showEdit(tid, ttp)
{
    //$("#oid").val(o);
    $("#tid").val(tid);
    //$("#ttp").val(p);

    setModalEdit();

    $("#TransModal").show();
    getTransEdit(ttp);
    viewCart();
}

function editFulfillment(tid, ftp)
{
    //$("#TransModal").show();
    //$("#dvOrdFulfill").show();
    //$("#dvCartBody").show();

    //$("#dvSchCat").hide();
    $("#TransModal").show();
    $("#dvCartBody").show();

    showFulfillPanel();

    $("#dvBtnOrdBk").hide();
    $("#dvBtnOrdNw").hide();
    $("#dvBtnOrdCn").hide();
    $("#dvBtnOrdGo").css("visibility", "visible");
    setModalEdit();

    viewCart(tid);

    setGoOpt(ftp);
}

function hideFulfillExtras()
{
    $("#dvOrdNav").hide();
    //$("#dvBtnOrdBk").hide();
    //$("#dvBtnOrdNw").hide();
}




function setModalEdit()
{
    $("#dvTransCan").hide();
    $("#dvEditCan").show();

    $("#dvBtnEdit").css("display", "inline-block");
    $("#dvBtnFulfill").css("display", "none");
    
}

function returnEdit()
{
    $("#TransModal").hide();
/*    location.reload();*/
}


function renderPayGrid(d)
{
    var apd = toBucks(d.AmountPaid);
    var due = toBucks(d.EndingBalance);

    $("#ordDep").text(apd);
    $("#ordBal").text(due);


    $("#dvPymtGrid").empty();
    $("#dvNoPymt").empty();

    var b = "";
    var rc = "#CCCCCC";

    if (d.OrderPayments.length > 0) {
        $.each(d.OrderPayments, function (i, item) {

            var pid = item.Id;
            var bbl = item.BeginBalance;
            var apd = item.AmountPaid;
            var ebl = item.EndingBalance;
            var des = item.PaymentDesc;
            var dat = item.StrDate;

            b += "<div class=\"pymt-grid-row\" style=\"background-color:" + rc + "\" id='payRw" + pid + "'>";
            b += "<div class=\"pymt-row\" style=\"padding-top:10px\" id='payEdt" + pid + "'><a class=\"uploadTxt\" href=\"#\">edit</a></div>";
            b += "<div class=\"pymt-row\" style=\"padding-top:10px\" id='payDat" + pid + "'>" + dat + "</div>";
            b += "<div class=\"pymt-row\" style=\"padding-top:10px\" id='payBbl" + pid + "'>" + toBucks(bbl) + "</div>";
            b += "<div class=\"pymt-row\" style=\"padding-top:10px\" id='payDes" + pid + "'>" + des + "</div>";
            b += "<div class=\"pymt-row\" style=\"padding-top:10px\" id='payApd" + pid + "'>" + toBucks(apd) + "</div>";
            b += "<div class=\"pymt-row\" style=\"padding-top:10px\" id='payEbl" + pid + "'>" + toBucks(ebl) + "</div>";
            b += "<div class=\"pymt-row\" style=\"padding-top:10px\" id='payDel" + pid + "'><a class=\"uploadTxt\" href=\"#\" onclick=\"deletePayment('" + pid +"');\">delete</a></div>";
            b += "</div>";

            rc = rc === "#E2BABA" ? "#CCCCCC" : "#E2BABA";
        });

        $('#dvPymtGrid').append(b);
    }
    else
    {
        b += "<div class=\"pymt-none\" id='payNone'>No payments have been applied for this order</div>";
        $('#dvNoPymt').append(b);
    }


}


function setAddFeeTxt(v, r)
{
    var mnu = "trAddFee" + r;
    var cst = "trAddFeePrc" + r;
    var des = "trAddFeeDsc" + r;
    var a = $("#trAddFee" + r);
    var b = $("#trAddFeePrc" + r);
    var c = $("#trAddFeeDsc" + r);
    var txt = $("#" + mnu + " option:selected").text();

    var x = "";

    $.ajax({
        cache: false,
        data: "{ Id: '" + v + "'}",
        url: "/Orders/GetMenuPrice",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (d) {
            x = toBucks(d);
        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            c.val(txt);
            b.val(x);
        }
    });
}

function nixCartRow(id)
{

    var fd = new FormData();
    fd.append("cid", id);

    return $.ajax({
        cache: false,
        url: "/Orders/NixFeeRow",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}

function setTaxStatus(cid, tsi) {

    var fd = new FormData();
    fd.append("cid", cid);
    fd.append("tsi", tsi);

    return $.ajax({
        cache: false,
        url: "/Orders/SetTaxMenuItem",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}



function addCartFee(r)
{
    var a = $("#trAddFee" + r);
    var b = $("#trAddFeePrc" + r);
    var c = $("#trAddFeeDsc" + r);
    var d = $("#trAddFeeUnt" + r);

    var fid = a.val();
    var cst = b.val();
    var des = c.val();
    var unt = d.val();

    xst = fromBucks(cst);

    var fd = new FormData();
    fd.append("tid", r);
    fd.append("fid", fid);
    fd.append("unt", unt);
    fd.append("cst", xst);
    fd.append("des", des);

    return $.ajax({
        cache: false,
        url: "/Orders/AddFee",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}


function showPayments()
{
    oid = $("#oid").val(); // Order Id
    var b = "";

    var fd = new FormData();
    fd.append("oid", oid);

    return $.ajax({
        cache: false,
        url: "/Orders/ViewPayments",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            renderPayGrid(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}

function deletePayment(id) {

    oid = $("#oid").val(); // Order Id

    var fd = new FormData();
    fd.append("Id", id);
    fd.append("oid", oid);

    return $.ajax({
        cache: false,
        url: "/Orders/NixPayment",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            renderPayGrid(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}


function setMenuOptions(t, s)
{
    var d = "";

    d += "<option value=''>- SELECT -</option>";
    $.each(t, function (i, item) {
        var x = item.Value === s.toString() ? "selected" : "";
        d += "<option value=" + item.Value + " " +x+">" + item.Text + "</option>";
    });

    return d;
}

function setFsdMenuOptions(t, s) {
    var d = "";

    $.each(t, function (i, item) {
        var x = item.Value === s.toString() ? "selected" : "";
        d += "<option value=" + item.Value + " " + x + ">" + item.Text + "</option>";
    });

    return d;
}


function setTaxMenuOptions(t, s, p) {
    var d = "";
    d += "<option value=''>- SELECT -</option>";
    let a1 = [1, 4, 6];

    switch(p)
    {
        case 101:
            $.each(t, function (i, item) {
                let chk1 = a1.includes(parseInt(item.Value));
                if (chk1) {
                    var x = item.Value === s.toString() ? "selected" : "";
                    d += "<option value=" + item.Value + " " + x + ">" + item.Text + "</option>";
                }
            });
            break;

        default:
            $.each(t, function (i, item) {
                var x = item.Value === s.toString() ? "selected" : "";
                d += "<option value=" + item.Value + " " + x + ">" + item.Text + "</option>";
            });
            break;
    }

    return d;
}






function addOrderPayment() {

    oid = $("#oid").val(); // Order Id
    apd = $("#to4").val(); // Amount Paid
    clf = $("#to5").val(); // Card Last Four
    pdt = $("#to6").val(); // Payment Date
    ath = $("#to7").val(); // Auth Code
    chk = $("#to8").val();
    ptp = $("#sn4").val(); // Payment Method
    cid = $("#custId").val(); // Cust Id
    bbl = $("#ordBal").text();

    xbl = fromBucks(bbl);

    var fd = new FormData();
    fd.append("oid", oid);
    fd.append("cid", cid);
    fd.append("clf", clf);
    fd.append("ptp", ptp);
    fd.append("ath", ath);
    fd.append("chk", chk);
    fd.append("bbl", xbl);
    fd.append("apd", apd);
    fd.append("pdt", pdt);
 
    return $.ajax({
        cache: false,
        url: "/Orders/PostOrderPayment",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            renderPayGrid(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#form-pymt")[0].reset();
        }
    });
}


function getStr(item)
{
    var l = item.Sources.length;

    var lbl = "<div style=\"display:inline-block; vertical-align:top\">";
    lbl += "<img src =\"" + item.ImageUrl + "\" class=\"exist-gun\" /></div><div style=\"display:inline-block; vertical-align:top; width:160px\">" + item.ItemDesc + "</div>";
    lbl += "<div style=\"display:inline-block; text-align:center; width:60px;  vertical-align:top\">";
    lbl += "<div style=\"font-weight:bold\">Price</div>";
    lbl += "<div style=\"position:relative; top:13px; font-weight:bold; color:yellow\">" + item.StrPrice + "</div>";
    lbl += "</div>";

    if (l > 0) {
        $.each(item.Sources, function () {
            lbl += "<div style=\"display:inline-block; text-align:center; width:65px\">";
            lbl += "<div style=\"font-weight:bold\">" + this.DistCode + "</div>";
            lbl += "<div>" + this.Units + "</div>";
            lbl += "<div>" + this.StrCost + "</div>";
            lbl += "<div>" + this.Margin + "</div>";
            lbl += "<div style=\"font-weight:bold; color:#00FF00\">" + this.StrGross + "</div>";
            lbl += "</div>";
        });
    }
    else {
        lbl += "<div style=\"display:inline-block; position:relative; top:25px; left:50px; font-weight:bold; color:yellow\">ALL DISTRIBUTOR SOURCES *** OUT OF STOCK ***</div>";

    }

    return lbl;
}


function SetGunSpecs(ui) {

    var isi = ui.item.value.InStockId;
    var mid = ui.item.value.MasterId;
    var iow = ui.item.value.IsOnWeb;

    $.ajax({
        data: "{ Mid: '" + mid + "', Isi: '" + isi + "', Iow: '" + iow + "'}",
        url: "/Inventory/GetGunSpecs",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        cache: false,
        success: function (d) {

            $('[id^="ImgM_"]').empty();
 
            var mfg = d.ManufId;
            var cal = d.CaliberId;
            var atn = d.ActionId;
            var fin = d.FinishId;
            var gtp = d.GunTypeId;
            var upc = d.UpcCode;
            var mdl = d.ModelName;
            var wlb = d.WeightLb;
            var woz = d.WeightOz;

            //var isi = d.InStockId;
            var cap = d.CapacityInt;
            var mpn = d.MfgPartNumber;
            var bdc = d.BarrelDec;
            var cdc = d.ChamberDec;
 
            var d0 = d.Images.PicId;
            var d1 = d.Images.ImgHse1;
            var d7 = d.Images.Io1;
 
            $("#sb3").val(mfg);
            $("#sb4").val(cal);
            $("#sb5").val(atn);
            $("#sb6").val(fin);
            $("#sb14").val(gtp);
            $("#sb18").val(woz);

            $("#tb2").val(mdl);
            $("#tb3").val(upc);
            $("#tb9").val(bdc);
            $("#tb17").val(wlb);
            $("#tb22").val(mpn);
            $("#tb39").val(cap);
          
            $("#sb3").selectpicker("refresh");
            $("#sb4").selectpicker("refresh");


            $('[id^="ImgM_"]').empty();
            if (d1.length > 0) {
                $("#ImgM_1").append("<img src='" + d1 + "' alt='' data-id='" + d0 + "' />");
            } else
            {
                $("#delCol_1").hide();
            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#mid").val(mid);
        }
    });
}



function setItemSch(ui) {

    var isi = ui.item.value.InStockId;
    var mid = ui.item.value.MasterId;
    var dsc = ui.item.value.ItemDesc;
    var img = ui.item.value.ImageUrl;
    var inv = ui.item.value.Sources;
    var prc = ui.item.value.StrPrice;
    var iow = ui.item.value.IsOnWeb;

    flushItmSch();
    setDistOptions(inv, isi, mid);

    $("#imgSchRes").attr("src", img);
    $("#dvSchDesc").append(dsc);
    $("#dvSellGrs").text(prc);
    $("#tb24").val(prc);
    $("#tb25").val(1);

    $("#opc").val(prc);
    $("#mid").val(mid);
    $("#isi").val(isi);
    $("#iow").val(iow);

    setExt();

    $("#dvItmSchRslt").show();
}


$(document).ready(function () {

    $.validator.addMethod("validsupcount", function (value, element) {
        alert('Supplier ID Required');

        var x = $("#sup").val();
        var s = parseInt(x);

        return s > 0;
    });


    $("form[name='form-start']").validate({
        ignore: [],
        submitHandler: function (form) { form.submit(); },
        rules: {
            Location: { required: true },
            SearchCustomer: { required: true },
            OrderDate: { required: true },
            SalesRep: { required: true },
            OrderType: { required: true },
            //AddItem: { required: true }
        },
        messages: {
            Location: "Location Required",
            SearchCustomer: "Customer Required",
            OrderDate: "Order Date Required",
            SalesRep: "Sales Rep Required",
            OrderType: "Order Type Required"
            //AddItem: "Add Item Required"
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

    $("form[name='form-search-params']").validate({
        rules: {
            WarehouseSrc: { required: true },
            SchPrice: { required: true },
            SchQty: { required: true }
        },
        messages: {
            WarehouseSrc: "Fulfillment Source Required",
            SchPrice: "Price Required",
            SchQty: "Number!"
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


    $("form[name='form-generic']").validate({
        rules: {
            GenTax: { required: true },
            GenPrice: { required: true },
            GenDesc: { required: true }
        },
        messages: {
            GenTax: "Item Taxable Required",
            GenPrice: "Item Price Required",
            GenDesc: "Item Description Required"
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



    $("form[name='form-item-search']").validate({
        rules: {
            AcqFflType: { required: true },
            AcqFflWhs: { required: true },
                Condition: { required: true },
                SrchManuf: { required: true },
                SrchCaliber: { required: true },
                GunAction: { required: true },
                GunFinish: { required: true },
                GunType: { required: true },
                FflName: { required: true },
                FflState: { required: true },
            Fulfill: { required: true },
                ModelName: { required: true },
            FixedPrice: { required: true },
                OrigBox: { required: true },
                Paperwork: { required: true },
                Barrel: { required: true },
            BulletWeight: { required: true },
                Capacity: { required: true },
            SerialNum: { required: true },
            Valuation: { required: true },
            AmmoType: { required: true },
                Units: { required: true },
            MerchDesc: { required: true },
            Rent: { required: true },
            RoundsPerBox: { required: true },
                AcqSellerType: { required: true },
            PickupFirstName: { required: true },
            PickupLastName: { required: true },
            PickupAddress: { required: true },
            PickupCity: { required: true },
            PickupState: { required: true },
            PickupZipCode: { required: true },
            PickupPhone: { required: true },
            IdAddrMatch: { required: true },
            IsRealId: { required: true },
            SupplierName: { required: true }

        }, 
        messages: {
            Condition: "Condition Required",
            SrchManuf: "Manufacturer Required",
            SrchCaliber: "Caliber Required",
            GunAction: "Action Required",
            GunFinish: "Finish Required",
            GunFinish: "Finish Required",
            GunType: "Gun Type Required",
            FflName: "FFL Name Required",
            FflState: "FFL State Required",
            Fulfill: "Fulfillment Source Required",
            ModelName: "Model Name Required",
            FixedPrice: "Fixed Price Required",
            OrigBox: "Original Box Required",
            Paperwork: "Paperwork Required",
            Barrel: "Barrel Length Required",
            BulletWeight: "Bullet Weight Required",
            Capacity: "Capacity Required",
            SerialNum: "Serial Number Required",
            Valuation: "Valuation Required",
            AmmoType: "Ammo Type Required",
            Units: "Units Required",
            MerchDesc: "Description Required",
            Rent: "Monthly Rent Required",
            RoundsPerBox: "Rounds Per Box Required",
            AcqSellerType: "Recipient Type Required",
            AcqFflType: "FFL Source Required",
            AcqFflWhs: "FFL Warehouse Required",
            PickupFirstName: "First Name Required",
            PickupLastName: "Last Name Required",
            PickupAddress: "Address Required",
            PickupCity: "City Required",
            PickupState: "State Required",
            PickupZipCode: "Zip Code Required",
            PickupPhone: "Phone Number Required",
            IdAddrMatch: "ID Address Current Required",
            IsRealId: "Has Real ID Required",
            SupplierName: "Supplier Name Required"

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

function flushItmSch()
{
    $("#dvSchDesc").empty();
    $("#tb24").val("$0.00");
    $("#tb25").val("0");
    $("#dvSchMgn").empty();
    $("#dvSchPft").empty();
    $("#dvCostUnt").text("$0.00");
    $("#dvCostExt").empty();
    $("#dvSellGrs").empty();

    $("#mid").val("0");
    $("#isi").val("0");
    $("#iow").val("");
    $("#sci").val("");
}

function resetSch() {

    var p = $("#opc").val();

    $("#tb24").val(p);
    $("#tb25").val("1");
    $("#dvSchMgn").empty();
    $("#dvSchPft").empty();
    $("#dvCostUnt").text("$0.00");
    $("#dvCostExt").empty();
    $("#dvSellGrs").text(p);

   
}

function setOrderNav()
{
    var ttp = $("#ttp").val();
    var nav = $("#nav").val(); // current step
    var mns = $("#mns").val(); // maximum step
    var t = parseInt(ttp);
    var c = parseInt(nav);
    var m = parseInt(mns);

    /** INITIALIZE **/
    $("#dvOrdNav").css("display", "inline-block");

    $("#dvHovAit").removeClass("ord-nav-active");
    $("#dvHovFul").removeClass("ord-nav-active");
    $("#dvHovInv").removeClass("ord-nav-active");

    $("#dvHovAit").removeClass("ord-nav-select");
    $("#dvHovFul").removeClass("ord-nav-select");
    $("#dvHovInv").removeClass("ord-nav-select");

    $("#dvHovTtp").css("color", "");
    $("#dvHovAit").css("color", "");
    $("#dvHovFul").css("color", "");
    $("#dvHovInv").css("color", "");

    $("#dvNavTtp").css("color", "#FFFFFF");
    $("#dvNavFul").css("color", "#666666");
    $("#dvNavInv").css("color", "#666666");

    var it = t === 103 ? true : false;

    if (c >= m) { $("#mns").val(c); m = c; }


    switch (m)
    {
        case 1:
            if (it) {
                $("#dvNavTtp").css("display", "inline-block");
                $("#dvNavTtp").addClass("ord-nav-active");
                $("#dvArwTtp").css("display", "none");
            }
            else
            {
                $("#dvNavTtp").css("display", "none");
            }
            break;
        case 2:
            navTransferLink(it);
            $("#dvHovAit").addClass("ord-nav-active");
            break;
        case 3:
            navTransferLink(it);
            $("#dvHovAit").addClass("ord-nav-active");
            $("#dvHovFul").addClass("ord-nav-active");
            break;
        case 4:
            navTransferLink(it);
            $("#dvHovAit").addClass("ord-nav-active");
            $("#dvHovFul").addClass("ord-nav-active");
            $("#dvHovInv").addClass("ord-nav-active");

            break;
    }

    switch (c) {
        case 1:
            $("#dvHovTtp").removeClass("ord-nav-active");
            $("#dvHovTtp").addClass("ord-nav-select");
            break;
        case 2:
            $("#dvHovAit").removeClass("ord-nav-active");
            $("#dvHovAit").addClass("ord-nav-select");
            break;
        case 3:
            $("#dvHovFul").removeClass("ord-nav-active");
            $("#dvHovFul").addClass("ord-nav-select");
            break;
        case 4:
            $("#dvHovInv").removeClass("ord-nav-active");
            $("#dvHovInv").addClass("ord-nav-select");
            break;
    }


}

function navTransferLink(b)
{
    if (b) {
        $("#dvNavTtp").css("display", "inline-block");
        $("#dvHovTtp").addClass("ord-nav-active");
        $("#dvArwTtp").css("display", "inline-block");
        $("#dvArwTtp").css("color", "#FFFFFF");
    }
    else { $("#dvNavTtp").css("display", "none"); }
}


function setDistOptions(inv, isi, mid) {

    $("#sb22").find("option").remove().end();
    $("#sb22").append("<option value=\"\">- SELECT -</option>");

    var ds = "";

    $.each(inv, function () {

        var i = this.SupplierId;
        var c = this.DistCode;
        var u = this.Units;
        var t = this.StrCost;

        switch (c)
        {
            case "HSE":
                ds = "IN-STOCK: CA";
                break;
            case "WYO":
                ds = "IN-STOCK: WY";
                break;
            case "SSI":
                ds = "SPORTS SOUTH, LLC";
                break;
            case "CSS":
                ds = "CHATTANOOGA SHOOTING";
                break;
            case "LIP":
                ds = "LIPSEYS";
                break;
            case "MGE":
                ds = "MGE WHOLESALE";
                break;
            case "DAV":
                ds = "DAVIDSONS";
                break;
            case "RSR":
                ds = "RSR GROUP";
                break;
            case "AMR":
                ds = "AMCHAR WHOLESALE";
                break;
            case "BHC":
                ds = "BILL HICKS INC";
                break;
            case "ZAN":
                ds = "ZANDERS";
                break;
            case "KRL":
                ds = "KROLL CORP";
                break;
        }

        var txt = ds + ":  COST " + t + " (" + u + " UNITS)";
        var so = i + "|" + t + "|" + isi + "|" + mid + "|" + u;
        $("#sb22").append("<option value='" + so + "'>" + txt + "</option>");
    });
}

function showSchCost(v)
{
    var tv = v.value;
    //var av = tv.split("|"); //[1];
    //if (av.length < 2) { return; }

    //var i = av[0]; //dist
    //var t = av[1]; //cost

    var av = tv.split("|"); //[1];
    if (av.length < 4) { return; }

    var i = av[0]; //supId
    var t = av[1]; //cost
    var s = av[2]; //isi
    var m = av[3]; //mid

    var x = $(v).prop("selectedIndex");

    if (i!="0") {
        var f = fromBucks(t);
        var u = getSchUnits();
        var xt = f * u;
        var tb = toBucks(xt);
        $("#dvCostUnt").text(t);
        $("#dvCostExt").text(tb);
        $("#mid").val(m);
        $("#isi").val(s);
        $("#did").val(i);
    }
    else
    {
        resetSch();
    }

    $("#sup").val(i); //FFL CODE
    //$("#fco").val(i); //FFL CODE
    calcSch();
}

function getSchUnits()
{
    var u = $("#tb25").val(); //units
    var ut = parseInt(u);
    return ut;
}

function calcSch()
{
    setCost();
    setExt();
}

function setCost()
{
    //var c = $("#sb22").val(); //cost
    var c = $("#dvCostUnt").text(); //cost
    var f = fromBucks(c);
    var tb = "$0.00";

    if (f > 0) {
        var u = getSchUnits();
        var xt = f * u;
        tb = toBucks(xt);
    }

    $("#dvCostExt").text(tb);
    calcPftMgn();
}


function setExt() 
{
    var c = $("#tb24").val(); //price
    var r = fromBucks(c);
    var tb = "$0.00";

    if (r > 0)
    {
        var ip = setDec(r) //parseFloat(r).toFixed(2);
        var ut = getSchUnits();
        var xt = ut * ip;
        var tb = toBucks(xt)
    }

    $("#dvSellGrs").text(tb);
    calcPftMgn();
}

function calcPftMgn()
{
    var c = $("#dvCostExt").text(); //cost
    var p = $("#dvSellGrs").text(); //price

    var u = getSchUnits();
    var dc = fromBucks(c);
    var dp = fromBucks(p);
    var mb = "$0.00";
    var mp = "0%";

    if (dc > 0 && dp > 0)
    {
        var gm = (dp - dc);
        var m = gm * u;
        mb = toBucks(m);

        var np = ((gm / dc) * 100).toFixed(0);
        mp = np + "%";

    }

    $("#dvSchMgn").text(mb);
    $("#dvSchPft").text(mp);
} 


function fromBucks(s) { return s.replace("$", "").replace(",", ""); }

function setDec(s) { return parseFloat(s).toFixed(2); }

function toBucks(s)
{
    var uso = { style: 'currency', currency: 'USD' };
    return  new Intl.NumberFormat('en-US', uso).format(s);
}



function makeNewOrder()
{
    var c = $("#custId").val();
    var l = $("#lid").val();
    var s = $("#sid").val();
    var t = $("#otp").val();
    var odt = $("#to3").val();

    var fd = new FormData();
    fd.append("cid", c);
    fd.append("lid", l);
    fd.append("sid", s);
    fd.append("otp", t);
    fd.append("odt", odt);

    return $.ajax({
        cache: false,
        url: "/Orders/StartOrder",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {

            var ot = $("#otp").val();
            var qt = ot === "2" ? "QUOTE" : "ORDER";
            var ot = qt + " #: " + d.OrderNumber;

            $("#oid").val(d.OrderId);
            $("#onb").val(d.OrderNumber);
            $("#dvt3").text(ot);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#fl7").text(odt);
            $("#dvg1").show();
        }
    });
}

function beginTrans()
{
    $("#nav").val("0");
    $("#mns").val("0");
    $("#dvOrdNav").hide();
    
    $("#dvg3").show();
    $("#dvg11").hide();
    $("#so1").prop("selectedIndex", 0);
    $("#so8").prop("selectedIndex", 0);

    showTrans();
    setNavFul("4");
    $("#dvCartBody").hide();
    $("#dvOrdFulfill").hide();
    $("#dvBtnRowFulfill").hide();
    $("#tid").val("0");

    flushCart();
}

function flushCart()
{
    $('#cartItems').empty();
    $('#spItemCt').hide();
    $('#spZeroCt').show();
    $("#spTtl").text("$0.00");
    hideTheCart();
    showItemGrpOpt();
}




function dropOrder(o) {
 
    Lobibox.confirm({
        title: "Delete Order?",
        msg: "You are about to permanently delete this order and all transactions associated with it. This action cannot be undone - continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {
                var fd = new FormData();
                fd.append("oid", o); //just pass the masterOrderID
                //fd.append("otp", t);

                $.ajax({
                    cache: false,
                    url: "/Orders/NixOrder",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {
                        location.reload();
                    },
                    complete: function () {
                        //loadAll();
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}

function dropTransNoUi()
{
    var fd = new FormData();
    fd.append("tid", t);

    $.ajax({
        cache: false,
        url: "/Orders/NixTransaction",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            resetTrans();
            clearTrans();
        },
        complete: function () {
            $("#nav").val("2");
            $("#mns").val("2");
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}


function dropTrans() {
    var t = $("#tid").val();

    Lobibox.confirm({
        title: "Delete Transaction?",
        msg: "You are about to permanently delete this transaction and all cart items associated with it. This action cannot be undone - continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {
 
                var fd = new FormData();
                fd.append("tid", t);

                $.ajax({
                    cache: false,
                    url: "/Orders/NixTransaction",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (data) {
                        resetTrans();
                        clearTrans();
                    },
                    complete: function () {
                        $("#TransModal").hide();
                        $("#nav").val("0");
                        $("#mns").val("0");
                        $("#dvOrdNav").hide();
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}

function nixInvoiceTrans(o, t)
{
    var fd = new FormData();
    fd.append("oid", o);
    fd.append("tid", t);

    $.ajax({
        cache: false,
        url: "/Orders/NixInvoiceTrans",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        complete: function () {
            $("#tid").val(t);
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}



function nixTransCart() {
    var t = $("#tid").val();
    var fd = new FormData();
    fd.append("tid", t);

    $.ajax({
        cache: false,
        url: "/Orders/NixTransactionCart",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
            resetTrans();
            clearTrans();
        },
        complete: function () {
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function resetFflTransType(v) {

    var b = "false";

    switch(v) {
        case "1":
            $("#tt1").text("NEW TRANSACTION - FFL TRANSFER");
            break;
        case "2":
            b = "true";
            $("#tt1").text("NEW TRANSACTION - PRIVATE PARTY TRANSFER");
            break;
    }

    $("#ppt").val(b);

    var t = $("#tid").val();
    var fd = new FormData();
    fd.append("tid", t);
    fd.append("ppt", b);

    $.ajax({
        cache: false,
        url: "/Orders/ResetTransType",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (data) {
        },
        complete: function () {
            $("#nav").val("2");
            $("#mns").val("2");
            $("#dvg5").show();
            $("#dvSchCat").show();

            $("#dvg6").hide();
            $("#dvg9").hide();
            $("#dvg12").hide();
            $("#dvPptSup").hide();
            $("#dvPptSup").empty();
            $("#dvBtnCart").hide();
            $("#dvOrdFulfill").hide();
            $("#dvBtnRowFulfill").hide();
            $("#so2").prop("selectedIndex", 0);

            flushCart();
            showCartBody();
            setOrderNav();
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}




function clearTrans()
{
    $("#cat").val("0");
    $("#tid").val("0");
    $("#sup").val("0");
    $("#fcd").val("0");
    $("#pup").val("0");
    $("#pfc").val("0");
    $("#ttp").val("");
}

function clearSpecs()
{
    $("#form-item-search")[0].reset();
    var v = $("#form-item-search").validate();
    v.resetForm();
    $("#ImgM_1").empty();

    $("#dvx37").hide(); 
    $("#dvx61").hide(); 

    $("#sb3").selectpicker("refresh");
    $("#sb4").selectpicker("refresh");
    $("#sb11").selectpicker("refresh");

}

function cancelCustomAdd()
{
    clearSpecs();
    $("#dvOrdFulfill").hide();
    $("#so3").prop("selectedIndex", 0);
}



function clearSearchParams()
{
    $("#so1").prop("selectedIndex", 0);
    $("#so2").prop("selectedIndex", 0);
    $("#so3").prop("selectedIndex", 0);
    $("#sb3").prop("selectedIndex", 0);
    $("#sb4").prop("selectedIndex", 0);
    $("#sb14").prop("selectedIndex", 0);
    $("#sb15").prop("selectedIndex", 0);

    $("#sb3").selectpicker("refresh");
    $("#sb4").selectpicker("refresh");
    $("#tb6").val("");

    $("#imgSchRes").attr("src", "");
    $("#dvSchDesc").empty();
    $("#sb22").prop("selectedIndex", 0);
    $("#tb24").val("$0.00");
    $("#tb25").val("1");

    $("#dvCostUnt").text("$0.00");
    $("#dvCostExt").text("$0.00");
    $("#dvSellGrs").text("$0.00");
    $("#dvSchMgn").text("$0.00");
    $("#dvSchPft").text("0%");
}

function flushCustomSearch()
{
    $("#sid").val("");
    $("#fidAcq").val("");
    $("#aqFnm").val("");
    $("#aqFlc").val("");
    $("#ImgItm").empty();
    $("#ImgM_1").empty();
    $("#so2").prop("selectedIndex", 0);
    $("#so3").prop("selectedIndex", 0);

    $("[id^=sb]").prop("selectedIndex", 0);
    $("[id^=tb]").val("");

    $("#sb3").selectpicker("refresh");
    $("#sb4").selectpicker("refresh");

    $("#dvg6").hide();
    $("#dvx47").hide();
    $("#acqOrg").hide();

    $("#dvOrdFulfill").hide();
}


function resetTrans()
{
    $("#dvg1").show();

    $("#so1").prop("selectedIndex", 0);
    $("#so2").prop("selectedIndex", 0);
    $("#so3").prop("selectedIndex", 0);
    $("#sb3").prop("selectedIndex", 0);
    $("#sb4").prop("selectedIndex", 0);
    $("#sb14").prop("selectedIndex", 0);
    $("#sb15").prop("selectedIndex", 0);

    $("#sb3").selectpicker("refresh");
    $("#sb4").selectpicker("refresh");

    $("#tb6").val("");
    $(".poptext").css("visibility", "hidden");

    /** CART **/
    $("#imgSchRes").attr("src", "");
    $("#cartItems").empty();
    $("#dvSchDesc").empty();
    $("#sb22").prop("selectedIndex", 0);
    $("#tb24").val("$0.00");
    $("#tb25").val("1");
    $(".cartpop-subTotal").text("$0.00");
    $("#dvCostUnt").text("$0.00");
    $("#dvCostExt").text("$0.00");
    $("#dvSellGrs").text("$0.00");
    $("#dvSchMgn").text("$0.00");
    $("#dvSchPft").text("0%");
}

function getSupplierInfo(id) {

    $.ajax({
        data: "{ id: '" + id + "'}",
        url: "/Customer/GetSupplierById",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (d) {

            $("#dvCust").empty();

            var email = d.Email;
            var fName = d.FirstName;
            var lName = d.LastName;
            var addrs = d.Address;
            //var suite = d.CustomerBase.Suite;
            var cCity = d.City;
            var phone = d.Phone;
            var state = State;
            var zipCd = ZipCode;
            var zipEx = ZipExt;

            var supId = d.Id;
            var supTp = d.SupplerTypeId;

            $("#sup").val(supId);
            $("#stp").val(supTp);

            var fln = fName + " " + lName;
            var zip = zipEx > 0 ? zipCd + "-" + zipEx : zipCd;


            var lnk = "<a class=\"uploadTxt\" href=\"#\" onclick=\"dropCustomer()\">Remove</a> | <a class=\"uploadTxt\" href=\"#\" onclick=\"editCustomer('" + id + "')\">Update</a>";

            var ci = "<div style=\"width:100%\"><div style=\"float:left; width:75%\"><div style=\"margin-bottom:-4px\"><b style=\"color:blue; font-size:1.2em; margin-bottom:-5px\">" + fln + "</b></div>";
            ci += "<div>" + addrs + "</div>";
            ci += "<div>" + cCity + ", " + state + "  " + zip + "</div>";
            ci += "<div>" + "P. " + phone + "&nbsp;&nbsp;&nbsp; " + " E. " + email + "</div></div>";
            ci += "<div style=\"float:right; width:25%\">" + lnk + "</div></div>";

            $("#dvSupp").append(ci);
            $("#dvSun").hide();
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#dvSuppInfo").show();
            $("#to1").css("color", "#0000FF");
        }
    });

}

function getCustomerInfo(id) {

    $.ajax({
        data: "{ id: '" + id + "'}",
        url: "/Customer/GetOrderCustomer",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (d) {

            $("#dvCust").empty();

            var email = d.CustomerBase.EmailAddress;
            var fName = d.CustomerBase.FirstName;
            var lName = d.CustomerBase.LastName;
            var addrs = d.CustomerBase.Address;
            var suite = d.CustomerBase.Suite;
            var cCity = d.CustomerBase.City;
            var phone = d.CustomerBase.Phone;
            var state = d.CustomerBase.StateName;
            var zipCd = d.CustomerBase.ZipCode;
            var zipEx = d.CustomerBase.ZipExt;

            var cusId = d.CustomerId;
            var cusTp = d.CustomerTypeId;

            $("#custId").val(cusId);
            $("#ctp").val(cusTp);

            var fln = fName + " " + lName;
            var adr = suite.length > 0 ? addrs + " #" + suite : addrs;
            var zip = zipEx > 0 ? zipCd + "-" + zipEx : zipCd;


            var lnk = "<a class=\"uploadTxt\" href=\"#\" onclick=\"dropCustomer()\">Remove</a> | <a class=\"uploadTxt\" href=\"#\" onclick=\"editCustomer('"+id+"')\">Update</a>";

            var ci = "<div style=\"width:100%\"><div style=\"float:left; width:75%\"><div style=\"margin-bottom:-4px\"><b style=\"color:blue; font-size:1.2em; margin-bottom:-5px\">" + fln + "</b></div>"; 
            ci += "<div>" + adr + "</div>";
            ci += "<div>" + cCity + ", " + state + "  " + zip + "</div>";
            ci += "<div>" + "P. " + phone + "&nbsp;&nbsp;&nbsp; " + " E. " + email + "</div></div>";
            ci += "<div style=\"float:right; width:25%\">" + lnk +"</div></div>";

            var oad = adr + ', ' + cCity + ", " + state + "  " + zip;
            var oem = "P. " + phone  + " E. " + email;

            $("#fl4").text(fln);
            $("#fl5").text(oad);
            $("#fl6").text(oem);

            $("#to1").val(fln);
            $("#to1").attr('readonly', 'readonly');

            $("#dvCust").append(ci);

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#dvCustInfo").show();
            $("#to1").css("color", "#0000FF");
        }
    });

}


function setCustInfo(cid, fnm, lnm, ste, adr, cty, sta, zip, ext, phn, eml)
{
    var fln = fnm + " " + lnm;
    var fad = ste.length > 0 ? adr + " #" + ste : adr;
    var fzp = ext > 0 ? zip + "-" + ext : zip;


    var lnk = "<a class=\"uploadTxt\" href=\"#\" onclick=\"dropCustomer()\">Remove</a> | <a class=\"uploadTxt\" href=\"#\" onclick=\"editCustomer('" + cid + "')\">Update</a>";

    var ci = "<div style=\"width:100%\"><div style=\"float:left; width:75%\"><div style=\"margin-bottom:-4px\"><b style=\"color:blue; font-size:1.2em; margin-bottom:-5px\">" + fln + "</b></div>";
    ci += "<div>" + fad + "</div>";
    ci += "<div>" + cty + ", " + sta + "  " + fzp + "</div>";
    ci += "<div>" + "P. " + phn + "&nbsp;&nbsp;&nbsp; " + " E. " + eml + "</div></div>";
    ci += "<div style=\"float:right; width:25%\">" + lnk + "</div></div>";

    var oad = fad + ', ' + cty + ", " + sta + "  " + fzp;
    var oem = "P. " + phn + " E. " + eml;

    $("#fl4").text(fln);
    $("#fl5").text(oad);
    $("#fl6").text(oem);

    $("#to1").val(fln);
    $("#to1").attr('readonly', 'readonly');

    $("#dvCust").append(ci);

    $("#dvCustInfo").show();
    $("#to1").css("color", "#0000FF");
}

function dropCustomer() {

    $("#custId").val("0");
    $("#dvCust").empty();
    $("#dvCustInfo").hide();
    $("#to1").val("");
    $("#to1").attr('readonly', false);
    $("#to1").css("color", "#000000");

}




function showCatPanel() {

    var itmClass = $("#so2").val();
    var invSrc = $("#so3").val();

    setSearchPnl(itmClass, invSrc);
}

function startOrder()
{
    //var av = $("#form-start").valid();
    //$("#form-start").validate();
    //if (!av) { return; }

    var oid = $("#oid").val();
    var id = parseInt(oid);

    if (id === 0) { makeNewOrder(); }

    $("#dvt1").hide();
    $("#dvt2").show();   

    $("#dvAddTrans").show();
}

function clearOrder()
{
    $("#form-start")[0].reset();
    $("#to3").val("");
    $("#dvCust").empty();
    $("#dvCustInfo").hide();
    $('#to1').attr("readonly", false);
}

function nixOrder()
{
    var oid = $("#oid").val();
    var id = parseInt(oid);
    if (id > 0) { dropOrder(oid); }
}


function nixOrderFromList(v) {
    var id = parseInt(v);
    if (id > 0) { dropOrder(v); }
}




function setSearchPnl(ic, vs) {

    hidePanels();

    $("#dvOrdFulfill").css("width", "100%");
    $("#dvItmSchLf").css("width", "50%");
    $("#dvOrdFulfill").show();

    switch (ic) {
        case "100":   
            switch (vs) {
                case "102": //CONSIGNMENTS
                    $("#dvOrdSchTtl").text("Consignment Guns");
                    shwGunConsign();
                    break;
                case "104": //SHIPPING
                    $("#dvOrdSchTtl").text("Gun Shipping");
                    shwGunShipping();
                    break;
                case "105": //STORAGE
                    $("#dvOrdSchTtl").text("Gun Storage");
                    shwGunStorage();
                    break;
                case "106": //REPAIR
                    $("#dvOrdSchTtl").text("Gun Repair");
                    shwGunRepair();
                    break;
                case "107": //ACQUISITIONS
                    $("#dvOrdSchTtl").text("Acquisition Guns");
                    shwGunLiquid();
                    break;
                case "108": //TRANSPORT
                    $("#dvOrdSchTtl").text("Gun Transport");
                    shwGunTransport();
                    break;
                case "109": //RECOVERY
                    $("#dvOrdSchTtl").text("Police Gun Recovery");
                    shwGunRecovery();
                    break;
                case "150": //IN STOCK 
                    $("#dvOrdFulfill").show();
                    $("#dvOrdSchTtl").text("Search Stock Guns");
                    $("label[for = tb6]").text("Gun Search:");
                    shwGunBase();
                    break;
                case "151": //SPEC ORDER
                    $("#dvOrdSchTtl").text("Special Order Guns");
                    shwGunCust();
                    break;
                default:
                    $("#dvOrdFulfill").hide();
                    break;
            }
            break;
        case "200":
            switch (vs) {
                case "102": //CONSIGNMENTS
                    $("#dvOrdSchTtl").text("Consignment Ammo");
                    shwAmmoConsign();
                    break;
                case "104": //SHIPPING
                    $("#dvOrdSchTtl").text("Ammo Shipping");
                    shwAmmoShipping();
                    break;
                case "105": //STORAGE
                    $("#dvOrdSchTtl").text("Ammo Storage");
                    shwAmmoStorage();
                    break;
                case "107": //ACQUISITIONS
                    $("#dvOrdSchTtl").text("Acquisition Ammo");
                    shwAmmoLiquid();
                    break;
                case "108": //TRANSPORT
                    $("#dvOrdSchTtl").text("Ammo Transport");
                    shwAmmoTransport();
                    break;
                case "109": //RECOVERY
                    $("#dvOrdSchTtl").text("Police Ammo Recovery");
                    shwAmmoRecovery();
                    break;
                case "150": //IN-STOCK
                    $("#dvOrdSchTtl").text("Search Stock Ammo");
                    $("label[for = tb6]").text("Ammo Search:");
                    shwAmmoBase();
                    break;
                case "151": //SPEC ORDER
                    $("#dvOrdSchTtl").text("Special Order Ammo");
                    shwAmmoCust();
                    break;
                default:
                    $("#dvOrdFulfill").hide();
                    break;
            }
            break;
        case "300":
            switch (vs) {
                case "102": //CONSIGNMENTS
                    $("#dvOrdSchTtl").text("Consignment Merchandise");
                    shwMerchConsign();
                    break;
                case "104": //SHIPPING
                    $("#dvOrdSchTtl").text("Merchandise Shipping");
                    shwMerchShipping();
                    break;
                case "105": //STORAGE
                    $("#dvOrdSchTtl").text("Merchandise Storage");
                    shwMerchStorage();
                    break;
                case "107": //ACQUISITIONS
                    $("#dvOrdSchTtl").text("Acquisition Merchandise");
                    shwMerchLiquid();
                    break;
                case "108": //TRANSPORT
                    $("#dvOrdSchTtl").text("Merchandise Transport");
                    shwMerchTransport();
                    break;
                case "109": //RECOVERY
                    $("#dvOrdSchTtl").text("Police Merchandise Recovery");
                    shwMerchRecovery();
                    break;
                case "150": //IN-STOCK
                    $("#dvOrdSchTtl").text("Search Stock Merchandise");
                    $("label[for = tb6]").text("Item Search:");
                    shwMerchBase();
                    break;
                case "151": //SPEC ORDER
                    $("#dvOrdSchTtl").text("Special Order Merchandise");
                    shwMerchCust();
                    break;
                default:
                    $("#dvOrdFulfill").hide();
                    break;
            }
            break;
        case "500":
        case "600":
        case "700":
        case "800":
        case "900":
            shwSvcBase();
            break;
        case "1000":
            shwGunsmith();
            break;
        case "1100":
            shwShipping();
            break;
        case "1200":
            shwGeneric();
            break;
    }
}


function setCommType(v) {
    switch (v) {
        case "1":
            $("#dvx61").show();
            $("#dvx37").hide();
            break;
        case "2":
            $("#dvx61").hide();
            $("#dvx37").show();
            break;
        default:
            $("#dvx61").hide();
            $("#dvx37").hide();
            break;

    }
 
}


/*** SALES: IN-STOCK ***/

function shwGunBase()
{
    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx17").show(); //SCH
    $("#dvx19").show(); //GTP
    $("#dvx23").show(); //COK
    
    //$("#dvBtnTopOrdSch").css("display", "inline-block");
}

function shwAmmoBase()
{
    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx7").show();  //BTP
    $("#dvx18").show(); //ATP
    $("#dva17").show(); //SCH

    //$("#dvBtnTopOrdSch").css("display", "inline-block");
}

function shwMerchBase()
{
    $("#dvx2").show();  //MFG
    $("#dvx8").show();  //CAT
    $("#dvm17").show(); //SCH

    //$("#dvBtnTopOrdSch").css("display", "inline-block");
}

/*** SALES: SPECIAL ORDER ***/

function shwGunCust()
{
    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx4").show();  //ATN
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    $("#dvx10").show(); //UPC
    $("#dvx11").show(); //PRC
    $("#dvx12").show(); //CND
    $("#dvx13").show(); //BOX
    $("#dvx19").show(); //GTP
    $("#dvx22").show(); //BRL
    $("#dvx27").show(); //CST
    $("#dvx28").show(); //FRT
    $("#dvx29").show(); //FEE
    $("#dvx38").show(); //PPW
    $("#dvx41").show(); //MPN
    $("#dvx43").show(); //CAP
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //WGT
    $("#dvx34").show(); //IPB
    $("#dvx44").show(); //FFL
    $("#dvx51").show(); //PIC
    $("#dvx55").show(); //QTY
    $("#dvx60").show(); //DAT
   
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
    $("#dvBtnTopOrdCan").css("display", "inline-block");
}

function shwAmmoCust()
{
    $("#dvx2").show(); //MFG
    $("#dvx3").show(); //CAL
    $("#dvx9").show(); //MDL
    $("#dvx7").show(); //BTP
    $("#dvx10").show(); //UPC
    $("#dvx11").show(); //PRC
    $("#dvx12").show(); //CND
    $("#dvx41").show(); //MPN
    $("#dvx18").show(); //ATP
    $("#dvx20").show(); //RPB
    $("#dvx21").show(); //WGT
    $("#dvx27").show(); //CST
    $("#dvx28").show(); //FRT
    $("#dvx29").show(); //FEE
    $("#dvx44").show(); //FFL
    $("#dvx51").show(); //PIC
    $("#dvx55").show(); //QTY
    $("#dvx60").show(); //DAT

    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwMerchCust()
{
    $("#dvx2").show();  //MFG
    $("#dvx8").show();  //CAT
    $("#dvx9").show();  //MDL
    $("#dvx10").show(); //UPC
    $("#dvx11").show(); //PRC
    $("#dvx12").show(); //CND
    $("#dvx41").show(); //MPN
    $("#dvx27").show(); //CST
    $("#dvx28").show(); //FRT
    $("#dvx29").show(); //FEE
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //WGT
    $("#dvx34").show(); //IPB
    $("#dvx44").show(); //FFL
    $("#dvx51").show(); //PIC
    $("#dvx53").show(); //DES
    $("#dvx55").show(); //QTY
    $("#dvx60").show(); //DAT
    

    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

/*** SHIPPING ***/

function shwGunShipping()
{
    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx4").show();  //ATN
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    //$("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx13").show(); //BOX
    //$("#dvx41").show(); //MPN
    $("#dvx19").show(); //GTP
    $("#dvx22").show(); //BRL
    $("#dvx24").show(); //SER
    $("#dvx33").show(); //WGT
    $("#dvx36").show(); //INS
    $("#dvx38").show(); //PPW
    $("#dvx42").show(); //NOT
    $("#dvx43").show(); //CAP
    $("#dvx51").show(); //PIC
    $("#dvx62").show(); //SCH

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwAmmoShipping() {
    $("#dvx2").show(); //MFG
    $("#dvx3").show(); //CAL
    $("#dvx9").show(); //MDL
    $("#dvx7").show(); //BTP
    $("#dvx9").show();  //MDL
    $("#dvx12").show(); //CON
    $("#dvx18").show(); //ATP
    $("#dvx20").show(); //RPB
    $("#dvx21").show(); //BWT
    $("#dvx36").show(); //INS
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx55").show(); //UNT

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwMerchShipping() {
    $("#dvx2").show();  //MFG
    $("#dvx8").show();  //CAT
    $("#dvx9").show();  //MDL
    $("#dvx12").show(); //CON
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //SWT
    $("#dvx34").show(); //IPB
    $("#dvx36").show(); //INS
    $("#dvx37").show(); //BOT
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx53").show(); //DES
    $("#dvx55").show(); //UNT

    $("#sb12").prop("selectedIndex", 0);
    $("#dvx37").hide();
    $("#dvx61").hide();

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

/*** GUNSMITHING ***/

function shwGunRepair() {
    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx4").show();  //ATN
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    $("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx41").show(); //MPN
    $("#dvx19").show(); //GTP
    $("#dvx22").show(); //BRL
    $("#dvx24").show(); //SER
    $("#dvx30").show(); //REP
    $("#dvx31").show(); //PAR
    $("#dvx42").show(); //NOT
    $("#dvx43").show(); //CAP
    $("#dvx51").show(); //PIC
    $("#dvx62").show(); //SCH

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}



/*** CONSIGNMENT ***/

function shwGunConsign()
{
    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx4").show();  //ATN
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    $("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx13").show(); //BOX
    $("#dvx14").show(); //LOK
    $("#dvx15").show(); //VAL
    $("#dvx41").show(); //MPN
    $("#dvx19").show(); //GTP
    $("#dvx22").show(); //BRL
    $("#dvx24").show(); //SER
    $("#dvx30").show(); //REP
    $("#dvx31").show(); //PAR
    $("#dvx38").show(); //PPW
    $("#dvx39").show(); //LMD
    $("#dvx42").show(); //NOT
    $("#dvx43").show(); //CAP
    $("#dvx51").show(); //PIC
    $("#dvx62").show(); //SCH

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwAmmoConsign()
{
    $("#dvx2").show(); //MFG
    $("#dvx3").show(); //CAL
    $("#dvx9").show(); //MDL
    $("#dvx7").show(); //BTP
    $("#dvx9").show();  //MDL
    $("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx15").show(); //VAL
    $("#dvx41").show(); //MPN
    $("#dvx18").show(); //ATP
    $("#dvx20").show(); //RPB
    $("#dvx21").show(); //BWT
    $("#dvx37").show(); //BOT
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx55").show(); //UNT
    
    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwMerchConsign()
{
    $("#dvx2").show();  //MFG
    $("#dvx8").show();  //CAT
    $("#dvx9").show();  //MDL
    $("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx15").show(); //VAL
    $("#dvx41").show(); //MPN
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //SWT
    $("#dvx34").show(); //IPB
    $("#dvx37").show(); //BOT
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx53").show(); //DES
    $("#dvx55").show(); //UNT

    $("#sb12").prop("selectedIndex", 0);
    $("#dvx37").hide(); 
    $("#dvx61").hide();  

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

/*** LIQUIDATION ***/

function shwGunLiquid() {

    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx4").show();  //ATN
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    $("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx13").show(); //BOX
    $("#dvx14").show(); //LOK
    $("#dvx41").show(); //MPN
    $("#dvx19").show(); //GTP
    $("#dvx22").show(); //BRL
    $("#dvx24").show(); //SER
    $("#dvx26").show(); //OFA
    $("#dvx38").show(); //PPW
    $("#dvx39").show(); //LMD
    $("#dvx42").show(); //NOT
    $("#dvx43").show(); //CAP
    $("#dvx51").show(); //PIC
    $("#dvx62").show(); //SCH

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");


}

function shwAmmoLiquid()
{
    $("#dvx2").show(); //MFG
    $("#dvx3").show(); //CAL
    $("#dvx9").show(); //MDL
    $("#dvx7").show(); //BTP
    $("#dvx9").show();  //MDL
    $("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx41").show(); //MPN
    $("#dvx18").show(); //ATP
    $("#dvx20").show(); //RPB
    $("#dvx21").show(); //BWT
    $("#dvx26").show(); //OFA
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx55").show(); //UNT

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwMerchLiquid()
{
    $("#dvx2").show();  //MFG
    $("#dvx8").show();  //CAT
    $("#dvx9").show();  //MDL
    $("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx26").show(); //OFA
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //SWT
    $("#dvx34").show(); //IPB
    $("#dvx37").show(); //BOT
    $("#dvx41").show(); //MPN
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx53").show(); //DES
    $("#dvx55").show(); //UNT

    $("#sb12").prop("selectedIndex", 0);
    $("#dvx37").hide();
    $("#dvx61").hide();

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

/*** STORAGE ***/

function shwGunStorage() {
    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx4").show();  //ATN
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    //$("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx13").show(); //BOX
    //$("#dvx41").show(); //MPN
    $("#dvx19").show(); //GTP
    $("#dvx22").show(); //BRL
    $("#dvx24").show(); //SER
    $("#dvx65").show(); //RNT
    $("#dvx38").show(); //PPW
    $("#dvx42").show(); //NOT
    $("#dvx43").show(); //CAP
    $("#dvx51").show(); //PIC
    $("#dvx62").show(); //SCH

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwAmmoStorage()
{
    $("#dvx2").show(); //MFG
    $("#dvx3").show(); //CAL
    $("#dvx7").show(); //BTP
    //$("#dvx41").show(); //MPN
    $("#dvx18").show(); //ATP
    $("#dvx20").show(); //RPB
    $("#dvx21").show(); //BWT
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx55").show(); //UNT
    $("#dvx65").show(); //RNT

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwMerchStorage()
{
    $("#dvx2").show();  //MFG
    $("#dvx8").show();  //CAT
    $("#dvx9").show();  //MDL
    //$("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    //$("#dvx41").show(); //MPN
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //SWT
    $("#dvx34").show(); //IPB
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx65").show(); //RNT

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

/*** TRANSPORT ***/

function shwGunTransport()
{
    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx4").show();  //ATN
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    //$("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    $("#dvx13").show(); //BOX
    //$("#dvx41").show(); //MPN
    $("#dvx19").show(); //GTP
    $("#dvx22").show(); //BRL
    $("#dvx24").show(); //SER
    $("#dvx36").show(); //INS
    $("#dvx38").show(); //PPW
    $("#dvx42").show(); //NOT
    $("#dvx43").show(); //CAP
    $("#dvx51").show(); //PIC
    $("#dvx62").show(); //SCH

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwAmmoTransport()
{
    $("#dvx2").show(); //MFG
    $("#dvx3").show(); //CAL
    $("#dvx7").show(); //BTP
    $("#dvx9").show(); //MDL
    $("#dvx12").show();//CON
    $("#dvx20").show(); //RPB
    //$("#dvx41").show(); //MPN
    $("#dvx18").show(); //ATP
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //SWT
    $("#dvx34").show(); //IPB
    $("#dvx36").show(); //INS
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx55").show(); //UNT

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwMerchTransport()
{
    $("#dvx2").show();  //MFG
    $("#dvx8").show();  //CAT
    $("#dvx9").show();  //MDL
    //$("#dvx10").show(); //UPC
    $("#dvx12").show(); //CON
    //$("#dvx41").show(); //MPN
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //SWT
    $("#dvx34").show(); //IPB
    $("#dvx36").show(); //INS
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

/*** L.E. RECOVERY ***/

function shwGunRecovery()
{
    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    $("#dvx12").show(); //CON
    $("#dvx13").show(); //BOX
    $("#dvx41").show(); //MPN
    $("#dvx19").show(); //GTP
    $("#dvx22").show(); //BRL
    $("#dvx24").show(); //SER
    $("#dvx38").show(); //PPW
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx62").show(); //SCH

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwAmmoRecovery()
{
    $("#dvx2").show(); //MFG
    $("#dvx3").show(); //CAL
    $("#dvx7").show(); //BTP
    $("#dvx18").show(); //ATP
    $("#dvx20").show(); //RPB
    $("#dvx21").show(); //BWT
    $("#dvx41").show(); //MPN
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC
    $("#dvx55").show(); //UNT

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");
}

function shwMerchRecovery()
{
    $("#dvx2").show();  //MFG
    $("#dvx8").show();  //CAT
    $("#dvx9").show();  //MDL
    $("#dvx12").show(); //CON
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //SWT
    $("#dvx34").show(); //IPB
    $("#dvx41").show(); //MPN
    $("#dvx42").show(); //NOT
    $("#dvx51").show(); //PIC

    $("#dvBtnTopOrdCan").css("display", "inline-block");
    $("#dvBtnTopOrdAdd").css("display", "inline-block");

}

/*** REPAIRS-GUNSMITHING ***/

function shwGunsmith() {

    $("#dvv1").hide();  

    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    $("#dvx12").show(); //CON

    $("#dvx41").show(); //MPN
    $("#dvx19").show(); //GTP
    $("#dvx24").show(); //SER
    $("#dvx30").show(); //REP
    $("#dvx31").show(); //PAR

    $("#dvx42").show(); //NOT
}

/*** SHIPPING ***/

function shwShipping()
{

    $("#dvv1").hide();  

    $("#dvx2").show();  //MFG
    $("#dvx3").show();  //CAL
    $("#dvx5").show();  //FIN
    $("#dvx9").show();  //MDL
    $("#dvx12").show(); //CON
    $("#dvx16").show(); //HFE
    $("#dvx19").show(); //GTP

    $("#dvx24").show(); //SER
    $("#dvx32").show(); //SSZ
    $("#dvx33").show(); //SWT
    $("#dvx34").show(); //IPB
    $("#dvx36").show(); //INS

    $("#dvx42").show(); //NOT
}


/*** GENERIC ***/

function shwGeneric()
{
    $("#dvv1").hide(); 

    $("#dvx11").show(); //PRC
    $("#dvx40").show(); //TAX
    $("#dvx42").show(); //NOT
}

function hidePanels() {

    $("[id^=dvx]").hide();
    $("[id^=dva]").hide();
    $("[id^=dvm]").hide();
    $("[id^=dvBtnTopOrd]").hide();
    $("#dvRecovery").hide();
}

function shwSvcBase()
{
    $("#dvv1").show(); //TOP
}

function resetInv() {
    $("#sb2").prop("selectedIndex", 0);
}


function setLocation(v) {

    $.ajax({
        data: "{ Id: '" + v + "'}",
        url: "/Orders/GetLocationById",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (d) {
            var c = d.length;
            if (c === 0) { return; }
            else {

                var com = d.Company;
                var adr = d.Address
                var cty = d.City
                var sta = d.StateName
                var zip = d.ZipCode
                var phn = d.Phone

                var lad = adr + ', ' + cty + ', ' + sta + ' ' + zip
                var lpn = 'P. ' + phn;

                $("#fl1").text(com);
                $("#fl2").text(lad);
                $("#fl3").text(lpn);

            }
        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            $("#lid").val(v);
            getSalesReps(v);
        }
    });
}

function setSalesRep(v)
{
    var x = v.options[v.selectedIndex].text;
    $("#fl9").text(x);
    $("#sid").val(v.value);
}
 
function setOrderType(v) {
    var x = v.options[v.selectedIndex].text;
    $("#fl8").text(x);
    $("#otp").val(v.value);

}

function editBasicOrder()
{
    var t = $("#onb").val();
    var ot = $("#otp").val();
    var qt = ot === "2" ? "QUOTE" : "ORDER";
    var nt = "EDIT " + qt + "#: " + t;
    $("#dvt3").text(nt);

    $("#dvt1").show();
    $("#dvt2").hide();

    $("#dvOrdEdit").css("display", "inline-block");
    $("#dvOrdCanc").css("display", "inline-block");

    $("#dvBtnCont").hide();
    $("#dvOrdClear").hide();
    $("#dvOrdTyp").hide();
    

}



function updBasOrd() {
    var av = $("#form-start").valid();
    if (!av) { return; }

    var oid = $("#oid").val();
    var id = parseInt(oid);

    if (id > 0) {
        var o = $("#oid").val();
        var c = $("#custId").val();
        var l = $("#lid").val();
        var s = $("#sid").val();
        var t = $("#otp").val();
        var odt = $("#to3").val();

        var fd = new FormData();
        fd.append("oid", o);
        fd.append("cid", c);
        fd.append("lid", l);
        fd.append("sid", s);
        fd.append("otp", t);
        fd.append("odt", odt);

        return $.ajax({
            cache: false,
            url: "/Orders/EditBaseOrder",
            type: "POST",
            contentType: false,
            processData: false,
            data: fd,
            success: function (d) {
                $("#dvt1").hide();
                $("#dvt2").show();
            },
            error: function (err, data) {
                alert(err);
            },
            complete: function () {
                var qt = ot === "2" ? "QUOTE" : "ORDER";
                var nt = qt + "#: " + t;
                $("#dvt3").text(nt);
            }
        });
    }
}


function canBasOrder()
{
    $("#dvt1").hide();
    $("#dvt2").show();
}

function resetTrans()
{
    $("#sup").val("0");
}


//** REGION TRANSACTION **/

function getTransEdit(v) {

    resetTrans();
    showFulfillOpt();
    showItemGrpOpt();

    $("#dvCartMain").show();
    $("#dvSchCat").show();
    $("#dvBtnCart").hide();
    $("#dvPptSup").hide();
    $("#dvGenCat").hide();
    $("#spItmCat").hide();
    $("#dvg5").show();
    $("#dvg6").hide();
    $("#dvg9").hide();
    $("#dvg11").hide();
    $("#dvg12").hide();
    $("#so2").show();
    //$("#so9").show();

    $("#so2").prop("selectedIndex", 0);
    $("#so3").prop("selectedIndex", 0);


    var x = parseInt(v);
    $("#ttp").val(v);
    $("#nav").val("2"); //default nav step

    showCartBody();


    var t = $("#so1 option:selected").text().toUpperCase();
    var nt = "NEW TRANSACTION - " + t;
    $("#tt1").text(nt);

    switch (x) {
        case 0:
            break;
        case 101: //SALES: GUNS, AMMO, ACCESSORIES
            $("#so3 option[value='3']").hide();
            break;
        case 102: //CONSIGNMENTS
        case 104: //SHIPPING
        case 105: //STORAGE
            break;
        case 106: //REPAIR
            $("#so2 option[value='200']").hide();
            $("#so2 option[value='300']").hide();
            break;
        case 107: //ACQUISITIONS
        case 108: //TRANSPORT
        case 109: //GUN RECOVERY
            break;
        case 103: //SERVICE: FFL: TRANSFERS
            $("#so2 option[value='300']").hide();
            $("#so3 option[value='3']").hide();
            $("#nav").val("1");
            showTrnBody();
            break;
        case 200: //GENERIC TRANSACTION
            $("#dvCartBody").show();
            $("#dvGenCat").show();
            $("#dvSchCat").hide();
            $("#dvCartMain").hide();
            break;
        default:
            $("#so3 option[value='1']").show();
            $("#so3 option[value='2']").show();
            break;
    }

    //startTrans();
    setFulfillMenuOpts(x);
    setOrderNav();
}

function setTransType(v) {

    resetTrans();
    showFulfillOpt();
    showItemGrpOpt();

    $("#dvCartMain").show();
    $("#dvSchCat").show();
    $("#dvBtnCart").hide();
    $("#dvPptSup").hide();
    $("#dvGenCat").hide();
    $("#spItmCat").hide();
    $("#dvg5").show();
    $("#dvg6").hide();
    $("#dvg9").hide();
    $("#dvg11").hide();
    $("#dvg12").hide();
    $("#so2").show();
    //$("#so9").show();

    $("#so2").prop("selectedIndex", 0);
    $("#so3").prop("selectedIndex", 0);
 
    
    var x = parseInt(v);
    $("#ttp").val(v);
    $("#nav").val("2"); //default nav step

    showCartBody();


    var t = $("#so1 option:selected").text().toUpperCase();
    var nt = "NEW TRANSACTION - " + t;
    $("#tt1").text(nt);

    switch (x) {
        case 0:
            break;
        case 101: //SALES: GUNS, AMMO, ACCESSORIES
            $("#so3 option[value='3']").hide();
            break;
        case 102: //CONSIGNMENTS
        case 104: //SHIPPING
        case 105: //STORAGE
            break;
        case 106: //REPAIR
            $("#so2 option[value='200']").hide();
            $("#so2 option[value='300']").hide();
            break;
        case 107: //ACQUISITIONS
        case 108: //TRANSPORT
        case 109: //GUN RECOVERY
            break;
        case 103: //SERVICE: FFL: TRANSFERS
            $("#so2 option[value='300']").hide();
            $("#so3 option[value='3']").hide();
            $("#nav").val("1");
            showTrnBody();
            break;
        case 200: //GENERIC TRANSACTION
            $("#dvCartBody").show();
            $("#dvGenCat").show();
            $("#dvSchCat").hide();
            $("#dvCartMain").hide();
            break;
        default:
            $("#so3 option[value='1']").show();
            $("#so3 option[value='2']").show();
            break;
    }

    startTrans();
    setFulfillMenuOpts(x);
    setOrderNav();
}

function NavPanel(v)
{
    var n = parseInt(v);

    switch (n)
    {
        case 1:
            $("#nav").val("1");
            showTrnBody();
            break;
        case 2:
            $("#nav").val("2");
            $("#dvSchCat").show();
            $("#dvOrdFulfill").hide();
            $("#dvBtnRowFulfill").hide();
            $("#dvg12").hide();
            $("#so2").prop("selectedIndex", 0);
            break;
        case 3:
            $("#nav").val("3");
            setFulfill();
            break;
    }

    setOrderNav();
}


function setItemTransfer(v)
{
    $("#nav").val("2");
    $("#dvg6").hide();
    $("#dvg9").hide();
    $("#dvSchCat").show();
    $("#dvg5").show();
    
    switch (v) {
        case "1":
            $("#ppt").val('false');
            break;
        case "2":
            $("#ppt").val('true');
            $("#tt1").text("NEW TRANSACTION - PRIVATE PARTY TRANSFER");
            break;
    }

    showCartBody();
    startTrans();
    setOrderNav();
}



function startTrans() {

    var s = $("#tid").val();
    var si = parseInt(s);

    var o = $("#oid").val();
    var t = $("#so1").val();

    var fd = new FormData();
    fd.append("oid", o);
    fd.append("ttp", t);

    return $.ajax({
        cache: false,
        url: "/Orders/StartOrderTrans",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            $("#tid").val(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#dvBtnRowFulfill").hide();
        }
    });
}


function showCartBody()
{
    $("#dvCartBody").show();
    $("#dvg7").show();
    $("#dvg1").hide();
}

function showTrnBody() {
    $("#dvCartBody").hide();
    $("#dvg11").show();
    $("#dvg1").show();
    $("#dvg3").hide();
}


//** REGION: ITEM SEARCH CATEGORY


function addItemGrp(v) {


    $("#cat").val(v);

    hidePanels();
    $("#dvOrdFulfill").hide();
    $("#dvPptSup").hide();
    $("#spItmCat").hide();
    $("#so2").show();
    $("#dvg9").hide();
    
    $("#so3").prop("selectedIndex", 0);

    var tt = $("#ttp").val();
    var p = $("#ppt").val(); // CA PPT

    var x = parseInt(v);

    switch (tt)
    {
        case "101": // SALES
            switch (x) {
                case 0:
                    $("#dvx17").show();
                    $("#dva17").hide();
                    $("#dvm17").hide();
                    $("#dvg6").hide();
                    showHideNext();
                    break;
                case 100:
                    $("#dvx17").show();
                    $("#dva17").hide();
                    $("#dvm17").hide();
                    $("#dvg6").show();
                    break;
                case 200:
                    $("#dvx17").hide();
                    $("#dva17").show();
                    $("#dvm17").hide();
                    $("#dvg6").show();
                    break;
                case 300:
                    $("#dvx17").hide();
                    $("#dva17").hide();
                    $("#dvm17").show();
                    $("#dvg6").show();
                    break;
            }
            break;
        case "102": // CONSIGNMENTS
            setSearchPnl(v, "102");
            break;
        case "103": // FFL TRANSFER
            switch (x) {
                case 0:
                    $("#dvg6").hide();
                    $("#dvg12").hide();
                    showHideNext();
                    break;
                case 100: // GUNS
                case 200: // AMMO
                    menuTransferItems(x, p);
                    break;


            }
            break;
        case "104": // SHIPPING
            setSearchPnl(v, "104");
            break;
        case "105": // STORAGE
            setSearchPnl(v, "105");
            break;
        case "106": // REPAIR
            setSearchPnl(v, "106");
            break;
        case "107": // ACQUISITIONS
            setSearchPnl(v, "107");
            break;
        case "108": // TRANSPORT
            setSearchPnl(v, "108");
            break;
        case "109": // RECOVERY
            setSearchPnl(v, "109");
            break;
    }
}

function setPptOpt(cat)
{
    var s = $("#sup").val();
    var sv = parseInt(s);

    if (cat === "100") { $("#spItmCat").text("Guns"); } else { $("#spItmCat").text("Ammo"); }

    $("#spItmCat").show();
    $("#so2").hide();

    $("#dvg9").show();
    $("#dvg12").hide();

}


function setFulfillMenuOpts(v)
{
    switch (v)  
    {
        case 101:
            trimSaleOpt();
            break;
        case 102:
        case 107:
            trimConsignOpt();
            break;
        case 103:
            trimTransferOpt();
            break;
        case 104:
            trimShippingOpt();
            $('#sb30 option:contains("Pickup")').text('Pickup & Shipping');
            break;
    }
}

function trimSaleOpt() {
    $("#sb30 > option").each(function () { var i = parseInt(this.value); if (i > 3) { this.style.display = "none"; } });
}

function trimTransferOpt()
{
    $("#sb30 > option").each(function () { var i = parseInt(this.value); if (i > 1) { this.style.display = "none"; } });
}

function trimConsignOpt() {
    $("#sb30 > option").each(function () { var i = parseInt(this.value); if (i === 2 || i === 3 || i === 5) { this.style.display = "none"; } });
}

function trimShippingOpt() {
    $("#sb30 > option").each(function () { var i = parseInt(this.value); if (i === 3 || i === 4 || i === 6) { this.style.display = "none"; } });
}

function showFulfillOpt() {
    $("#sb30 > option").each(function () { this.style.display = "block";  });
}

function showItemGrpOpt() {
    $("#so2 option[value='100']").show();
    $("#so2 option[value='200']").show();
    $("#so2 option[value='300']").show();
}
 


function menuTransferItems(a, p)
{
    var b = $("#custId").val();
    var m = $("#so6");

    $.ajax({
        cache: false,
        data: "{ cat: '" + a + "', cid: '" + b + "', ppt: '" + p + "'}",
        url: "/Orders/GetTransferItems",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            $(m).find("option").remove().end();
            m.append("<option value=''>- SELECT -</option>");

            $.each(result, function (i, item) {
                m.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });

        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            $("#dvg9").show();
        }
    });
}


function menuPptItems(c) {

    var a = $("#cat").val(); // $("#so2").val();
    var b = $("#custId").val();
    var m = $("#so6");

    $.ajax({
        cache: false,
        data: "{ cat: '" + a + "', cid: '" + b + "', sup: '" + c + "'}",
        url: "/Orders/GetPptItems",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            $(m).find("option").remove().end();
            m.append("<option value=''>- SELECT -</option>");

            $.each(result, function (i, item) {
                m.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });

        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            $("#dvg9").show();
        }
    });
}


//function menuPptSuppliers(a) {
//    var b = $("#custId").val();
//    var m = $("#so9");

//    $.ajax({
//        cache: false,
//        data: "{ cat: '" + a + "', cid: '" + b + "'}",
//        url: "/Orders/GetCaPptSellers",
//        type: "POST",
//        contentType: 'application/json; charset=utf-8',
//        success: function (result) {

//            $(m).find("option").remove().end();
//            m.append("<option value=''>- SELECT -</option>");

//            $.each(result, function (i, item) {
//                m.append("<option value=" + item.Value + ">" + item.Text + "</option>");
//            });

//        },
//        error: function (err, result) {
//            alert(err);
//        },
//        complete: function () {
//            $("#dvg12").show();
//        }
//    });
//}

 

function getPptSellerInfo(v)
{
    if (v === "") { $("#dvPptSup").hide(); return; }

    $.ajax({
        data: "{ isi: '" + v + "'}",
        url: "/Customer/GetPptSellerInfo",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            $("#dvPptSup").empty();

            $("#dvSupBtnCnl").css("display", "inline-block");
            $("#dvSupBtnEdt").css("display", "inline-block");
            $("#dvSupBtnAdd").css("display", "none");

            var sid = data.Id;
            var pct = data.PptCtYtd;
            var fnm = data.FirstName;
            var lnm = data.LastName;
            var adr = data.Address;
            var cty = data.City;
            var sta = data.State;
            var zip = data.ZipCode;
            var phn = data.Phone;
            var idn = data.IdNumber;
            var dxp = data.LineIdExp;

            var fln = fnm + ' ' + lnm;
            var csz = cty + ', ' + sta + ' ' + zip
            var net = pct + 1;
            var tax = net > 2 ? "Yes" : "No";

            var x = $("#sb23").val();
            var d = "<div style='display: inline-block; color: black'>";
            d += "<div><b>" + fln + "</b></div>";
            d += "<div>" + adr + "</div>";
            d += "<div>" + csz + "</div>";
            d += "<div>P. " + phn + "</div>";
            d += "<div><span style=\"padding-right:5px\">" + sta + " ID # " + idn + "</span> EXP: " + dxp + "</div>";
            d += "<div><span style=\"padding-right:5px\">YTD PPT Count:" + net + "</span> Taxable: " + tax + "</div>";
            d += "<div><span class=\"link11Blue\" href=\"#\" onclick=\"readSupplier('1', '" + sid + "', '3')\">Update</span></div>"; 
            d += "</div>";

            $("#dvPptSup").append(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#isi").val(v);
            $("#dvg12").show();
            $("#dvg13").show();
            $("#dvPptSup").css("display", "inline-flex");
        }
    //}).done(menuPptItems(v));
    });
}

function addPptItem()
{
    var v = $("#isi").val();
    addCartTransfer(v);
    $("#dvg9").hide();
    $("#dvg13").hide();
}


function getPptSupplier(v) {

    if (v === "") {
        $("#dvPptSup").hide();
        return;
    } 

    $.ajax({
        data: "{ id: '" + v + "'}",
        url: "/Customer/GetSupplier",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            $("#dvPptSup").empty();

            $("#dvSupBtnCnl").css("display", "inline-block");
            $("#dvSupBtnEdt").css("display", "inline-block");
            $("#dvSupBtnAdd").css("display", "none");

            var sid = data.Id;
            var sta = data.StateId;
            var stp = data.SupplerTypeId;

            var pct = data.PptCtYtd;
 

            var idt = data.IdType;
            var ids = data.IdState;
            var fnm = data.FirstName;
            var lnm = data.LastName;
            var org = data.OrgName;
            var adr = data.Address;
            var cty = data.City;
            var sta = data.State;
            var zip = data.ZipCode;
            var ext = data.ZipExt;
            var phn = data.Phone;
            var eml = data.Email;
            var ffl = data.CurFfl;
            var idn = data.IdNumber;
            var exp = data.LineCurExp;
            var dob = data.LineIdDob;
            var dxp = data.LineIdExp;

            var fln = fnm + ' ' + lnm;
            var csz = cty + ', ' + sta + ' ' + zip
            var net = pct + 1;
            var tax = net > 2 ? "Yes" : "No";

            var d = "<div style='display: inline-block; color: black'>";
            d += "<div><b>" + fln + "</b></div>";
            d += "<div>" + adr + "</div>";
            d += "<div>" + csz + "</div>";
            d += "<div>P. " + phn + "</div>";
            d += "<div><span style=\"padding-right:5px\">" + sta + " ID # " + idn + "</span> EXP: " + dxp + "</div>";
            d += "<div><span style=\"padding-right:5px\">YTD PPT Count:" + net + "</span> Taxable: " + tax + "</div>";
            d += "<div><span class=\"link11Blue\" href=\"#\" onclick=\"readSupplier('"+sid+"', '1')\">Update</span></div>";
            d += "</div>";

            $("#dvPptSup").append(d);
 

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#sup").val(v);
            $("#so9").hide();
            $("#dvPptSup").css("display", "inline-flex");
        }
    });
}


//function resetPptGrp()
//{
//    var a = $("#so2").val();
//    menuPptSuppliers(a);
//    $("#dvPptSup").hide();
//}

function setTransAction(v) {
    if (v === '') { return; }
    var p = $("#ppt").val();

    if (p === "true") //PPT
    {
        getPptSellerInfo(v);
    }
    else
    {
        addCartTransfer(v);

    }
}
//** REGION CART **/

function addCartTransfer(v) {

    var tid = $("#tid").val(); //transaction Id

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("bid", v);

    var method = "/Orders/AddItemTransfer";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight);

    showContinue();
}


function addGenericItem()
{
    var av = $("#form-generic").valid();
    if (!av) { return; }

    var tid = $("#tid").val();  // transaction Id
    var tax = $("#so10").val(); // taxable
    var prc = $("#tb29").val(); // price
    var des = $("#tb30").val(); // desc

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("tax", tax);
    fd.append("prc", prc);
    fd.append("des", des);

    return $.ajax({
        cache: false,
        url: "/Orders/GenericAddItem",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            //$("#dvBtnOrdBk").hide();
            clearGeneric();
            $("#dvGenCat").hide();
            setFulfill();

        }

    });
    

}

function clearGeneric()
{
    $("#form-generic")[0].reset();
    var v = $("#form-generic").validate();
    v.resetForm();
}


function addCartItem()
{
    var av = $("#form-search-params").valid();
    if (!av) { return; }

    var ttp = $("#ttp").val(); //trans type
    var tid = $("#tid").val(); //transaction Id
    var sup = $("#sup").val(); //supplier id
    var isi = $("#isi").val(); //in-stock id
    var mid = $("#mid").val(); //master id
    var ask = $("#tb24").val(); //asking price
    var unt = $("#tb25").val(); //units
    var did = $("#did").val();  //dist id

    var mnu = "";

    $("#sb22 option").each(function () {
        // Add $(this).val() to your list
        mnu += $(this).val() + "^";
    });

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("sup", sup);
    fd.append("mid", mid);
    fd.append("isi", isi);
    fd.append("unt", unt);
    fd.append("ttp", ttp);
    fd.append("did", did);
    fd.append("ask", fromBucks(ask));
    fd.append("mnu", mnu);

    var method = "/Orders/AddCartSaleItem";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight);    
    canAddItem();

    showContinue();
}

function canAddItem()
{
    clearSearchParams();
    $("#dvOrdFulfill").hide();
    $("#dvItmSchRslt").hide();
    $("#dvg6").hide();

    showHideNext();
}

function showHideNext()
{
    var cct = $("#cnt").val();
    var y = parseInt(cct);
    if (y > 0) { showNavCancel(); } else { $("#dvBtnRowFulfill").hide(); }
}



function deleteCartItem(cartId) {

    var fileData = new FormData();
    fileData.append('CartId', cartId);

    var method = '/Orders/NixCartItem';
    showTheCart(fileData, method, true);

    $("#so2").prop("selectedIndex", 0);
    $("#dvg9").hide();

}

function adjCartCount(cartId, addItem) {

    var fileData = new FormData();
    fileData.append('CartId', cartId);
    fileData.append('AddItem', addItem);

    var method = '/Orders/AdjustCartQuantity';
    showTheCart(fileData, method, false);
}


function setCartHeight(u)
{
    var ht = "";

    switch (u) {
        case 1:
            ht = "112px";
            break;
        default:
            ht = "225px";
            break;
        //case 3:
        //    ht = "338px";
        //    break;
        //default:
        //    ht = "450px";
        //    break;

    }

    $('#cartItems').css("max-height", ht);
}


function viewCart(tid) {

    var s = $("#cartIcon").is(":visible");
    if (s) { return; }

    var id = $("#tid").val();

    var fd = new FormData();
    fd.append('Id', id);

    var method = "/Orders/ShowCart";
    showTheCart(fd, method, false);

}


function showTheCart(fileData, method, nxtBtn) {

    return $.ajax({
        cache: false,
        url: method,
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (data) {

            $('#cartItems').empty();

            var dl = data.ItemCount;
            setCartHeight(dl);

            var itl = "0.00";
            $("#cnt").val(dl);

            if (dl > 0) {
                $('#spItemCt').text(dl + " Items");
                $('#spItemCt').show();
                $('#spZeroCt').hide();
                itl = data.CartItems[0].CartTotal.toFixed(2);
            }
            else {
                $('#spItemCt').hide();
                $('#spZeroCt').show();
            }

            var sub = toBucks(itl);
            $("#spTtl").text(sub);

            $('.cartpop-subTotal').text(sub);

            $.each(data.CartItems, function (i, item) {

                var si = item.ImgName.length > 0;

                var add = "onClick = 'adjCartCount(" + item.Id + ", true)'";
                var remove = "onClick = 'adjCartCount(" + item.Id + ", false)'";
                var clAdd = 'cartpop-addBtn';
                var clRem = 'cartpop-addBtn';

                var cat = item.CategoryId;
                var title = "";
                var prc = toBucks(item.Price);
                var ext = toBucks(item.Extension);
                var ins = toBucks(item.Insurance);
                var fee = toBucks(item.Rent);
                var pts = toBucks(item.Parts);
                var lbr = toBucks(item.Labor);
                var tt = item.TransTypeId;
                var othFee = "";

                switch (tt)
                {
                    case 4:
                    case 8:
                        othFee = ", <b> Insurance: </b> " + ins + "";
                        break;
                    case 6:
                        othFee = ", <b>Notes:</b> " + item.Notes + ", <b>Est Parts:</b> " + pts + ", <b>Est Labor:</b> " + lbr + "";
                        break;
                    case 5:
                        othFee = ", <b>Monthly Rent:</b> " + fee + "";
                        break;
                }


                title = item.Manuf + " " + item.Model + " " + item.Mpn;

                if (parseInt(item.Units) < 2) { remove = ''; clRem = 'cartpop-disable'; }

                var block = "<div class=\"cart-item-row\">";
                block += "<div class=\"cart-item-num\">" + item.RowId + ". </div>";
                block += "<div class=\"cart-item-img\">";
                if (si) { block += "<img class=\"cartImg\" src = '" + item.ImgName + "' alt =\"\">"; } else { block += "<div class=\"cartNoImg\">Image Not Available</div>"; }
                block += "</div>";
                block += "<div class=\"cart-item-desc\">";
                block += "<div><b style=\"color:blue\">" + title + "</b> " + item.ItemDesc + " <b> Condition: </b> " + item.Cond + othFee + "</div>";
                block += "<div><b>Category: </b><span style=\"color:slategrey;font-weight:bold\"> " + item.CatName + "</span></div>";
                block += "</div>";
                block += "</div>";
                block += "<div class=\"cart-calc-row\">";
                block += "<div class=\"cart-qty-block\">";

                //block += "<div class=\"cartpop-disable\" id=\"btnPopAdd\" onclick=\"adjCartCount('" + item.Id + "', true)\">+</div><div class=\"cart-qty-count\">" + item.Units + "</div><div class=\"cartpop-disable\" id=\"btnPopRemove\">-</div>";

                block += "<div class=" + clAdd + " id='btnPopAdd' " + add + ">+</div>";
                block += "<div style='color:green; font-weight:bold; display: table-cell; padding:3px; font-size:14px'>" + item.Units + "</div>";
                block += "<div class=" + clRem + " id='btnPopRemove' " + remove + ")'>-</div>";

                block += "</div>";
                block += "<div class=\"cart-qty-x\">x</div>";
                block += "<div class=\"cart-qty-price\">" + prc + "</div>";
                block += "<div class=\"cartpop-lineTtl\">" + ext + "</div>";
                block += "<div class=\"cart-qty-delete \">";
                block += "<a href=\"#\" onclick=\"deleteCartItem('" + item.Id + "')\"><span class=\"glyphicon glyphicon-remove-circle cartpop-delete-item\"></span></a>";
                block += "</div>";
                block += "</div>";
                block += "<hr class=\"cart-hr-top\">";
                $('#cartItems').append(block);

            });

        },
        complete: function () {

            var s = $("#spItemCt").is(":visible");
            if (s) { $("#cartIcon").show(); } else { $("#cartIcon").hide(); }

            //if (nxtBtn) { showHideNext(); }
            //testOrder();
           
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}



function hideTheCart() {
    $("#cartIcon").hide();
}

function showTrans()
{
    $("#TransModal").show();
    $("#dvg1").show();
}


function setFflSource(v) {
    var l = parseInt(v);

    if (l === 99) {
        $("#dvx47").hide();
    } else {
        $("#dvx47").show();
        setFflWarehouse(v, "sb26");
    }
}

function setFflWarehouse(id, c) {

    var d = $("#" + c);

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

function getMenuPrice(id) {

    var b = "";

    $.ajax({
        cache: false,
        data: "{ Id: '" + id + "'}",
        url: "/Orders/GetMenuPrice",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (d) {
            b = toBucks(d);
        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            return b;
        }
    });
}



function setSource(v) {

    $("#stp").val(v);
    $("#sup").val("0");
    $("#fcd").val("0");

    $("#dvx59").hide();
    $("#dvx46").hide();


    var l = parseInt(v);
    switch (l) {
        default:
            $("#dvx59").hide();
            break;
        case 1: //FFL COMM
            $("#dvx59").hide();
            $("#dvx46").show();
            break;
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
            $("#dvx59").show();
            break;

    }
}


function setPickup(v) {

    $("#pup").val("0");
    $("#pfc").val("0");

    $("#dvx50").hide();
    $("#dvx57").hide();
 
    var l = parseInt(v);
    switch (l) {
        default:
            $("#dvx50").hide();
            break;
        case 1: //FFL COMM
            $("#dvx50").hide();
            $("#dvx57").show();
            break;
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
            $("#dvx50").show();
            break;
    }
}


function fflState(v) { $("#fsAcq").val(v); }


function getFflData(id) {

    $.ajax({
        data: "{ Id: '" + id + "'}",
        url: "/Orders/GetFfl",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            $("#aqFnm").val(result.TradeName);
            $("#aqFlc").val(result.FflFullLic);
        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            $("#fidAcq").val(id);
        }
    });
}

function allOthSvcAddAmmo() {

    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var ttp = $("#ttp").val();  // transType
    var tid = $("#tid").val();  // transId
    var mid = $("#sb3").val();   // mfgId
    var cid = $("#sb4").val();   // calId
    var btp = $("#sb7").val();   // bulletTypeId
    var cnd = $("#sb9").val();   // condId
    var val = $("#sb12").val();  // valuation
    var atp = $("#sb13").val();  // ammoTypeId

    var mdl = $("#tb2").val();   // model
    var upc = $("#tb3").val();   // upc
    var rpb = $("#tb7").val();   // roundsPerBox
    var bwt = $("#tb8").val();   // bullet weight
    var ofr = $("#tb11").val();  // offer
    var ins = $("#tb20").val();  // ins
    var flt = $("#tb21").val();  // flatRate
    var mpn = $("#tb22").val();  // mpn
    var not = $("#tb23").val();  // notes
    var com = $("#tb27").val();  // comm
    var fee = $("#tb32").val();  // rent
    var unt = $("#tb44").val();  // units


    var fd = new FormData();
    fd.append("ttp", ttp);
    fd.append("tid", tid);
    fd.append("mid", mid);
    fd.append("atp", atp);
    fd.append("val", val);
    fd.append("cid", cid);
    fd.append("btp", btp);
    fd.append("cnd", cnd);
    fd.append("rpb", rpb);
    fd.append("bwt", bwt);
    fd.append("unt", unt);

    fd.append("mdl", mdl);
    fd.append("mpn", mpn);
    fd.append("upc", upc);
    fd.append("not", not);

    fd.append("flt", fromBucks(flt));
    fd.append("com", fromBucks(com));
    fd.append("ofr", fromBucks(ofr));
    fd.append("ins", fromBucks(ins));
    fd.append("fee", fromBucks(fee));

    var f = document.getElementById("ImgItm").files;
    if (f.length > 0) {
        fd.append('ImgItm', f[0]);
        fd.append('GroupId', 'ImgItm');
    }

    var method = "/Orders/OtherSvcAddAmmo";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight);

    flushCustomSearch();

    showContinue();

}

function allOthSvcAddGun() {

    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var ttp = $("#ttp").val();  // transTypeId
    var tid = $("#tid").val();  // transId
    var mst = $("#mid").val();  // masterId
    var mid = $("#sb3").val();   // mfgId
    var lmk = $("#sb11").val();  // lockMake
    var val = $("#sb12").val();  // valuation
    var lmd = $("#sb20").val();  // lockModel
    var gtp = $("#sb14").val();  // gunTypeId
    var cid = $("#sb4").val();   // calId
    var aid = $("#sb5").val();   // atnId
    var fid = $("#sb6").val();   // finId
    var cap = $("#tb39").val();  // capacity
    var cnd = $("#sb9").val();   // condId
    var box = $("#sb10").val();  // box
    var woz = $("#sb18").val();  // woz
    var ppw = $("#sb19").val();  // ppw


    var mdl = $("#tb2").val();   // model
    var ser = $("#tb10").val();  // serial
    var ofr = $("#tb11").val();  // offer
    var rep = $("#tb15").val();  // labor
    var pts = $("#tb16").val();  // parts
    var wlb = $("#tb17").val();   // wgt pounds
    var ins = $("#tb20").val();  // ins
    var flt = $("#tb21").val();  // flatRate
    var mpn = $("#tb22").val();  // mpn
    var com = $("#tb27").val();  // comm
    var upc = $("#tb3").val();   // upc
    var brl = $("#tb9").val();   // barDec
    var not = $("#tb23").val();  // notes
    var fee = $("#tb32").val();  // rent


    var fd = new FormData();
    fd.append("ttp", ttp);
    fd.append("tid", tid);
    fd.append("mst", mst);
    fd.append("mid", mid);
    fd.append("gtp", gtp);
    fd.append("cid", cid);
    fd.append("aid", aid);
    fd.append("fid", fid);
    fd.append("cap", cap);
    fd.append("cnd", cnd);
    fd.append("lmk", lmk);
    fd.append("lmd", lmd);
    fd.append("val", val);
    fd.append("box", box);
    fd.append("ppw", ppw);
    fd.append("wlb", wlb);
    fd.append("woz", woz);
 
    fd.append("mdl", mdl);
    fd.append("mpn", mpn);
    fd.append("upc", upc);
    fd.append("ser", ser);
    fd.append("not", not);

    fd.append("rep", fromBucks(rep));
    fd.append("pts", fromBucks(pts));
    fd.append("ins", fromBucks(ins));
    fd.append("flt", fromBucks(flt));
    fd.append("com", fromBucks(com));
    fd.append("fee", fromBucks(fee));
    fd.append("ofr", fromBucks(ofr));
    fd.append("brl", brl);

    var f = document.getElementById("ImgItm").files;
    if (f.length > 0) {
        fd.append('ImgItm', f[0]);
        fd.append('GroupId', 'ImgItm');
    }

    var method = "/Orders/OtherSvcAddGun";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight);

    flushCustomSearch();

    showContinue();

}

function allOthSvcAddMerch(ttp) {

    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var tid = $("#tid").val();  // transId
    var mid = $("#sb3").val();   // mfgId
    var sct = $("#sb8").val();   // subCat
    var cnd = $("#sb9").val();   // condId
    var ssz = $("#sb17").val();  // shipSizeId
    var ofr = $("#tb11").val();  // offer
    var slb = $("#tb17").val();   // shipLbs
    var ipb = $("#tb18").val();   // itemsPerBox
    var ins = $("#tb20").val();   // insValue
    var mpn = $("#tb22").val();   // mpn
    var fee = $("#tb32").val();  // rent
    var unt = $("#tb44").val();   // units

    var mdl = $("#tb2").val();   // model
    var not = $("#tb23").val();  // notes
    var des = $("#tb43").val();   // desc
    var soz = $("#sb18").val();  // shipOz

    var fd = new FormData();
    fd.append("ttp", ttp);
    fd.append("tid", tid);
    fd.append("mid", mid);
    fd.append("sct", sct);
    fd.append("cnd", cnd);
    fd.append("ssz", ssz);
    fd.append("slb", slb);
    fd.append("ipb", ipb);
    fd.append("unt", unt);

    fd.append("mpn", mpn);
    fd.append("mdl", mdl);
    fd.append("not", not);
    fd.append("des", des);

    fd.append("ofr", fromBucks(ofr));
    fd.append("fee", fromBucks(fee));
    fd.append("ins", fromBucks(ins));
    fd.append("soz", soz);

    var f = document.getElementById("ImgItm").files;
    if (f.length > 0) {
        fd.append('ImgItm', f[0]);
        fd.append('GroupId', 'ImgItm');
    }

    var method = "/Orders/OtherSvcAddMerch";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight);

    flushCustomSearch();

    showContinue();

}

function consignAddMerch() {

    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var tid = $("#tid").val();  // transId
    var mid = $("#sb3").val();   // mfgId
    var sct = $("#sb8").val();   // subCat
    var cnd = $("#sb9").val();   // condId
    var ssz = $("#sb17").val();  // shipSizeId
    var slb = $("#tb17").val();   // shipLbs
    var ipb = $("#tb18").val();   // itemsPerBox
    var unt = $("#tb44").val();   // units
    var val = $("#sb12").val();  // valuation

    var mdl = $("#tb2").val();   // model
    var mpn = $("#tb22").val();  // mpn
    var upc = $("#tb3").val();   // upc
    var not = $("#tb23").val();  // notes
    var des = $("#tb43").val();   // units

    var com = $("#tb27").val();  // comm
    var flt = $("#tb21").val();  // flatRate
    var soz = $("#sb18").val();  // shipOz

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("mid", mid);
    fd.append("sct", sct);
    fd.append("cnd", cnd);
    fd.append("ssz", ssz);
    fd.append("slb", slb);
    fd.append("ipb", ipb);
    fd.append("val", val);
    fd.append("unt", unt);

    fd.append("mdl", mdl);
    fd.append("mpn", mpn);
    fd.append("upc", upc);
    fd.append("not", not);
    fd.append("des", des);

    fd.append("flt", fromBucks(flt));
    fd.append("com", fromBucks(com));
    fd.append("soz", soz);

    var f = document.getElementById("ImgItm").files;
    if (f.length > 0) {
        fd.append('ImgItm', f[0]);
        fd.append('GroupId', 'ImgItm');
    }

    var method = "/Orders/ConsignmentAddMerch";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight);

    flushCustomSearch();

    showContinue();

}

function consignAddAmmo() {

    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var tid = $("#tid").val();  // transId
    var mid = $("#sb3").val();   // mfgId
    var atp = $("#sb13").val();  // ammoTypeId
    var lmk = $("#sb11").val();  // lockMake
    var lmd = $("#sb20").val();  // lockModel
    var val = $("#sb12").val();  // valuation
    var cid = $("#sb4").val();   // calId
    var btp = $("#sb7").val();   // bulletTypeId
    var cnd = $("#sb9").val();   // condId
    var rpb = $("#tb7").val();   // roundsPerBox
    var bwt = $("#tb8").val();   // bullet weight
    var unt = $("#tb44").val();   // units

    var mdl = $("#tb2").val();   // model
    var mpn = $("#tb22").val();  // mpn
    var upc = $("#tb3").val();   // upc
    var not = $("#tb23").val();  // notes

    var com = $("#tb27").val();  // comm
    var flt = $("#tb21").val();  // flatRate

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("mid", mid);
    fd.append("atp", atp);
    fd.append("lmk", lmk);
    fd.append("lmd", lmd);
    fd.append("val", val);
    fd.append("cid", cid);
    fd.append("btp", btp);
    fd.append("cnd", cnd);
    fd.append("rpb", rpb);
    fd.append("bwt", bwt);
    fd.append("unt", unt);

    fd.append("mdl", mdl);
    fd.append("mpn", mpn);
    fd.append("upc", upc);
    fd.append("not", not);

    fd.append("flt", fromBucks(flt));
    fd.append("com", fromBucks(com));

    var f = document.getElementById("ImgItm").files;
    if (f.length > 0) {
        fd.append('ImgItm', f[0]);
        fd.append('GroupId', 'ImgItm');
    }

    var method = "/Orders/ConsignmentAddAmmo";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight);

    flushCustomSearch();

    showContinue();

}

function custAddGun()
{

    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var tid = $("#tid").val();  // transId
    var mid = $("#sb3").val();  // mfgId
    var sct = $("#sb14").val(); // gunTypeId
    var cid = $("#sb4").val();  // calId
    var aid = $("#sb5").val();  // atnId
    var fid = $("#sb6").val();  // finId
    var cap = $("#tb39").val(); // capacity
    var cnd = $("#sb9").val();  // condId
    var ssz = $("#sb17").val(); // shipSize
    var ipb = $("#tb18").val(); // itemsPerBox
    var wlb = $("#tb17").val(); // wtLbs
    var qty = $("#tb44").val(); // units
    var box = $("#sb10").val(); // box
    var ppw = $("#sb19").val(); // ppw
    var mdl = $("#tb2").val();  // model
    var mpn = $("#tb22").val(); // mpn
    var upc = $("#tb3").val();  // upc
    var prc = $("#tb4").val();  // price
    var cst = $("#tb12").val(); // cost
    var frt = $("#tb13").val(); // freight
    var fee = $("#tb14").val(); // fees
    var brl = $("#tb9").val();  // barDec
    var eml = $("#tb46").val(); // email
    var woz = $("#sb18").val(); // wtOz
    var adt = $("#tb56").val(); // Acq Date
    var acq = $("#sb23").val(); // AcqSourceID
    var sup = $("#sup").val();  // SupplierID
    var fcd = $("#fcd").val();  // FFLCode

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("mid", mid);
    fd.append("sct", sct);
    fd.append("cid", cid);
    fd.append("aid", aid);
    fd.append("fid", fid);
    fd.append("cap", cap);
    fd.append("cnd", cnd);
    fd.append("acq", acq);
    fd.append("fcd", fcd);
    fd.append("ipb", ipb);
    fd.append("wlb", wlb);
    fd.append("ssz", ssz);
    fd.append("qty", qty);
    fd.append("sup", sup);
    fd.append("box", box);
    fd.append("ppw", ppw);
    fd.append("mdl", mdl);
    fd.append("mpn", mpn);
    fd.append("upc", upc);
    fd.append("eml", eml);
    fd.append("adt", adt);

    fd.append("prc", fromBucks(prc));
    fd.append("cst", fromBucks(cst));
    fd.append("frt", fromBucks(frt));
    fd.append("fee", fromBucks(fee));
    fd.append("brl", brl);
    fd.append("woz", woz);

    var f = document.getElementById("ImgItm").files;
    if (f.length > 0) {
        fd.append('ImgItm', f[0]);
        fd.append('GroupId', 'ImgItm');
    }
 
    var method = "/Orders/CustomAddGun";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight); 

    flushCustomSearch();

    showContinue();

}


function custAddAmmo()
{
    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var tid = $("#tid").val();  // transId
    var atp = $("#sb13").val(); // ammoTypeId
    var mid = $("#sb3").val();  // mfgId
    var cid = $("#sb4").val();  // calId
    var bid = $("#sb7").val();  // bulletTypeId
    var gwt = $("#tb8").val();  // bulletWeight
    var rpb = $("#tb7").val();  // roundsPerBox
    var cnd = $("#sb9").val();  // condId
    var sup = $("#sup").val();  // sup id
    var fcd = $("#fcd").val();  // fflCode
    var acq = $("#sb23").val(); // acq type id
    var qty = $("#tb44").val(); // Units
    var slg = $("#sb28").val(); // is slug
    var eml = $("#tb46").val(); // email
    var mpn = $("#tb22").val(); // mpn
    var upc = $("#tb3").val();  // upc
    var mdl = $("#tb2").val();  // model
    var prc = $("#tb4").val();  // price
    var cst = $("#tb12").val(); // cost
    var frt = $("#tb13").val(); // freight
    var fee = $("#tb14").val(); // fees
    var chb = $("#tb41").val(); // chamberDec
    var ssz = $("#tb42").val(); // shotSzWt
    var adt = $("#tb56").val(); //acq date

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("atp", atp);
    fd.append("mid", mid);
    fd.append("cid", cid);
    fd.append("bid", bid);
    fd.append("gwt", gwt);
    fd.append("rpb", rpb);
    fd.append("cnd", cnd);
    fd.append("sup", sup);
    fd.append("fcd", fcd);
    fd.append("acq", acq);
    fd.append("qty", qty);
    fd.append("slg", slg);
    fd.append("eml", eml);
    fd.append("mpn", mpn);
    fd.append("upc", upc);
    fd.append("mdl", mdl);
    fd.append("prc", fromBucks(prc));
    fd.append("cst", fromBucks(cst));
    fd.append("frt", fromBucks(frt));
    fd.append("fee", fromBucks(fee));
    fd.append("chb", chb);
    fd.append("ssz", ssz);
    fd.append("adt", adt);

    var f = document.getElementById("ImgItm").files;
    if (f.length > 0) {
        fd.append('ImgItm', f[0]);
        fd.append('GroupId', 'ImgItm');
    }

    var method = "/Orders/CustomAddAmmo";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight);

    flushCustomSearch();
    showContinue();

}

function custAddMerch()
{
    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var tid = $("#tid").val();  // transId
    var mid = $("#sb3").val();  // mfgId
    var sct = $("#sb8").val();  // subCatId
    var ssz = $("#sb17").val(); // shipSize
    var ipb = $("#tb18").val(); // itemsPerBox
    var wlb = $("#tb17").val(); // wtLbs
    var qty = $("#tb44").val(); // Units
    var cnd = $("#sb9").val();  // condId
    var sup = $("#sup").val();  // sup id
    var fcd = $("#fcd").val();  // fflCode
    var acq = $("#sb23").val(); // acq type id

    var prc = $("#tb4").val();  // price
    var cst = $("#tb12").val(); // cost
    var frt = $("#tb13").val(); // freight
    var fee = $("#tb14").val(); // fees
    var woz = $("#sb18").val(); // wtOz

    var eml = $("#tb46").val(); // email
    var mdl = $("#tb2").val();  // model
    var mpn = $("#tb22").val(); // mpn
    var upc = $("#tb3").val();  // upc
    var dsc = $("#tb43").val(); // descr

    var adt = $("#tb56").val(); //acq date

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("mid", mid);
    fd.append("sct", sct);
    fd.append("ssz", ssz);
    fd.append("ipb", ipb);
    fd.append("wlb", wlb);
    fd.append("qty", qty);
    fd.append("cnd", cnd);
    fd.append("sup", sup);
    fd.append("fcd", fcd);
    fd.append("acq", acq);

    fd.append("prc", fromBucks(prc));
    fd.append("cst", fromBucks(cst));
    fd.append("frt", fromBucks(frt));
    fd.append("fee", fromBucks(fee));
    fd.append("woz", woz);

    fd.append("eml", eml);
    fd.append("mdl", mdl);
    fd.append("mpn", mpn);
    fd.append("upc", upc);
    fd.append("dsc", dsc);

    fd.append("adt", adt);

    var f = document.getElementById("ImgItm").files;
    if (f.length > 0) {
        fd.append('ImgItm', f[0]);
        fd.append('GroupId', 'ImgItm');
    }

    var method = "/Orders/CustomAddMerch";
    showTheCart(fd, method, true);
    $("#cartItems").scrollTop($("#cartItems")[0].scrollHeight);

    flushCustomSearch();

}



function routeCustom()
{
    var t = $("#ttp").val(); // trans type
    var c = $("#cat").val(); // category

    var rt = parseInt(t);
    var rc = parseInt(c);


    switch (rt)
    {
        case 101: // SALES
            switch (rc) {
                case 100:
                    custAddGun();
                    break;
                case 200:
                    custAddAmmo();
                    break;
                case 300:
                    custAddMerch();
                    break;
            }
            break;
        case 102: // CONSIGNMENT
        case 104: // SHIPPING
        case 105: // STORAGE
        case 106: // REPAIR
        case 107: // ACQUISITION
        case 108: // TRANSPORT
        case 109: // RECOVERY
            switch (rc) {
                case 100:
                    allOthSvcAddGun();
                    break;
                case 200:
                    allOthSvcAddAmmo();
                    break;
                case 300:
                    allOthSvcAddMerch(rt);
                    break;
            }
            break;
 
    }


}


function shotOrShell(v) {

    if (v === "9") {
        $("#dvx52").show();
        $("#dvx7").hide();
        $("#dvx21").hide();
    } else {
        $("#dvx52").hide();
        $("#dvx7").show();
        $("#dvx21").show();
    }
}


function setFulfill()
{
    $("#nav").val("3");
    clearFulfill();
    showFulfillPanel();
    showBaseNav();
    setOrderNav();
}

function showContinue()
{
    $("#dvBtnCart").show();
    $("#dvSchCat").hide();
    $("#dvFulSpace").hide();
    $("#dvBtnRowFulfill").hide();
    $("#dvOrdFulfill").hide();
    $("#dvg9").hide();
}


function addMoreItems() {

    $("#dvSchCat").show();
    $("#spItmCat").hide();
    $("#dvg12").hide();
    $("#so2").prop("selectedIndex", 0);
    $("#so2").show();

    $("#dvFulSpace").hide();
    $("#dvBtnCart").hide();  
    $("#dvOrdFulfill").hide();
    //location.reload();
}


function showFulfillPanel()
{
    $("#dvOrdSchTtl").text("Order Fulfillment");

    hidePanels();

    $("#dvBtnCart").hide();
    $("#dvSchCat").hide();   //hide base search params
    $("#dvFulSpace").hide(); //hide spacer bar
    $("#dvOrdFulfill").css("width", "50%");
    $("#dvItmSchLf").css("width", "100%");
    $("#dvOrdFulfill").show();

    var ttp = $("#ttp").val();
    if (ttp === "109")
    {
        $("#dvx66").show();
        $("#dvx56").hide();
    }
    else
    {
        $("#dvx66").hide();
        $("#dvx56").show();
    }
}

function showRecoveryFulfill(v)
{
    if (v != "") { $("#dvx56").show(); } else { $("#dvx56").hide(); }
}


 

 

function showBaseNav()
{
    $("#dvBtnOrdBk").css("visibility", "visible");
    $("#dvBtnOrdNw").show();
    $("#dvBtnOrdCn").hide();
    $("#dvBtnOrdGo").css("visibility", "visible");
}

function showNavCancel()
{
    $("#dvBtnRowFulfill").show();
    $("#dvBtnOrdCn").css("visibility", "visible");
    $("#dvBtnOrdGo").css("visibility", "hidden");
}

 


function setGoOpt(z) {

    setNavFul("1");
    $("#lblSb23").text("Recipient Type:");
    $("#lbltb31").text("Recipient Name:");

    $("#dvFulWrap").hide();
    $("#dvx42").hide(); // Notes
    $("#dvx44").hide(); // Order From
    $("#dvx48").hide(); // Pickup Name
    $("#dvx58").hide(); // Pickup Group
    $("#dvx63").hide(); // Photo ID Opts


    switch (v)
    {
        case "1": // F2F
            $("#dvx42").show(); // Notes
            $("#dvx63").show();
            break;
        case "2": // SHIPPING
        case "3": // DELIVERY
            $("#dvFulWrap").show();
            $("#dvx44").show();
            $("#dvx42").show();
            break;
        case "4": // PICKUP
            $("#dvFulWrap").show();
            $("#dvx42").show();
            $("#dvx48").show();
            $("#dvx58").show();
            break;
        case "5":  // PICKUP & DELIVERY
            $("#dvFulWrap").show();
            $("#dvx42").show();
            $("#dvx44").show();
            $("#dvx48").show();
            $("#dvx58").show();
            break;
        default:
            $("#dvx42").hide();
            $("#dvx48").hide();
            $("#dvx58").hide();
            $("#dvx63").hide();
            break;
    }

    // Gun Recovery
    var x = $("#ttp").val();
    if (x === "109") { $("#dvRecovery").show(); } else { $("#dvRecovery").hide(); }
}

function setNavFul(v)
{
    switch (v)
    {
        case "1":
            $("#dvBtnRowFulfill").show();
            $("#dvBtnOrdGo").css("visibility", "visible");
            $("#dvBtnOrdCn").css("visibility", "hidden");
            $("#dvBtnOrdBk").css("visibility", "visible");
            break;
        case "2":
            //$("#dvBtnRowFulfill").hide();
            break;
        case "3":
            $("#dvBtnRowFulfill").show();
            $("#dvBtnOrdBk").css("visibility", "visible");
            $("#dvBtnOrdGo").css("visibility", "hidden");
            break;
        case "4":
            $("#dvBtnRowFulfill").hide();
            $("#dvBtnOrdGo").css("visibility", "hidden");
            $("#dvBtnOrdCn").css("visibility", "hidden");
            //$("#dvBtnOrdBk").css("visibility", "hidden");
            break;
    }
}

function routeFulfill()
{
    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var o = $("#sb30").val();
    var v = parseInt(o);

    switch (v)
    {
        case 1:
            setF2F();
            break;
        default:
            shipOrder();
            break;
    }

    $("#TransModal").hide();
}


//function goToInvoice()
//{
//    routeFulfill();

//    var o = $("#oid").val();
//    getOrder(o);
//    $("#dvFinal").show();
//}

function setF2F()
{
    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var tid = $("#tid").val(); //transId  		 
    var fsc = $("#sb30").val(); //fulfillSrcId 
    var nts = $("#tb23").val(); //notes

    /* Gun Recovery */
    var aty = $("#tb50").val(); //attorney
    var aph = $("#tb51").val(); //attorney phone
    var ofc = $("#tb52").val(); //officer
    var oph = $("#tb53").val(); //officer phone
    var aem = $("#tb54").val(); //attorney email
    var oem = $("#tb55").val(); //officer email
    var rec = $("#sb36").val(); //recovery type id

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("fsc", fsc);
    fd.append("rec", rec);
    fd.append("nts", nts);
    fd.append("aty", aty);
    fd.append("aph", aph);
    fd.append("ofc", ofc);
    fd.append("oph", oph);
    fd.append("aem", aem);
    fd.append("oem", oem);

    return $.ajax({
        cache: false,
        url: "/Orders/AddF2F",
        type: "POST",
        contentType: false, 
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
        }
    });
}

function shipOrder() {
    var av = $("#form-item-search").valid();
    if (!av) { return; }

    var tid = $("#tid").val();  // transId  
    var ful = $("#sb30").val(); // fulfillSrcId 
    var sup = $("#sup").val();  // dest supplier id
    var daq = $("#sb23").val(); // dest acq type id
    var fcd = $("#fcd").val();  // dest fflCode
    var dem = $("#tb46").val(); // dest email
    var pup = $("#pup").val();  // pickup supplier id
    var paq = $("#sb31").val(); // pickup acq type id
    var pfc = $("#pfc").val();  // pickup fflCode
    var pem = $("#tb49").val(); // pickup email
    var not = $("#tb23").val(); // notes
    var loc = $("#lid").val();  

    /* Gun Recovery */
    var aty = $("#tb50").val(); //attorney
    var aph = $("#tb51").val(); //attorney phone
    var ofc = $("#tb52").val(); //officer
    var oph = $("#tb53").val(); //officer phone
    var aem = $("#tb54").val(); //attorney email
    var oem = $("#tb55").val(); //officer email
    var rec = $("#sb36").val(); //recovery type id

    var fd = new FormData();
    fd.append("tid", tid);
    fd.append("ful", ful);
    fd.append("sup", sup);
    fd.append("daq", daq);
    fd.append("fcd", fcd);
    fd.append("dem", dem);
    fd.append("pup", pup);
    fd.append("paq", paq);
    fd.append("pfc", pfc);
    fd.append("pem", pem);
    fd.append("not", not);
    fd.append("loc", loc);
    fd.append("aty", aty);
    fd.append("aph", aph);
    fd.append("ofc", ofc);
    fd.append("oph", oph);
    fd.append("aem", aem);
    fd.append("oem", oem);
    fd.append("rec", rec);

    return $.ajax({
        cache: false,
        url: "/Orders/AddFulfillment",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (d) {
            loadOrder(d);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });


}

function getSalesReps(v) {

    var s = $("#sn2");

    $.ajax({
        data: "{ locId: '" + v + "'}",
        url: "/Orders/GetSalesReps",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            $(s).find("option").remove().end();
            s.append("<option>- SELECT -</option>");

            $.each(result, function (i, item) {
                s.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}


function clearFulfill()
{
    $("#sb23").prop("selectedIndex", 0);
    $("#sb26").prop("selectedIndex", 0);
    $("#sb30").prop("selectedIndex", 0);

    $("#tb23").val("");
    $("#tb27").val("");
    $("#tb40").val("");
    $("#tb46").val("");

    $("#dvRecovery").hide();
}


function setLocks()
{
    var iw = $("#sb11").val();
    getLockModels(iw, 0);
}


function getLockModels(id, sel) {

    var s = $("#sb20");

    $.ajax({
        data: "{ lockMfgId: '" + id + "'}",
        url: "/Inventory/GetGunLockModels",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            $(s).find("option").remove().end();
            s.append("<option>- SELECT -</option>");
            $.each(result, function (i, item) {
                var x = parseInt(item.Value) === parseInt(sel) ? " selected" : "";
                s.append("<option value=" + item.Value + " " + x + ">" + item.Text + "</option>");
            });

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {

        }
    });
}


function formSupplier() {

    $("#sup").val("0");

    lightSupModal();
    $("#dvSupBtnCnl").css("display", "inline-block");
    $("#dvSupBtnAdd").css("display", "inline-block");

    var ttl = "Add Supplier: ";

    var v = $("#sb23").val();
    $("#stp").val(v);

    /* SET DEFAULT VISIBILITY */
    $("#dvCfl").hide();
    $("#dvOrg").hide();
    $("#dvSid").hide();

    switch (v) {
        case "2":
            ttl += "Curio-Relic FFL";
            $("#dvCfl").show();
            $("#dvSid").show();
            break;
        case "3":
            ttl += "Private Party";
            $("#dvSid").show();
            break;
        case "4":
            ttl += "Police Department";
            $("#dvOrg").show();
            break;
        case "5":
            ttl += "Other Organization";
            $("#dvOrg").show();
            break;
        case "6":
            ttl += "From Owner's Collection";
            break;
    }

    $("#dvAddTtl").text(ttl);
}


function hasCust()
{
    var c = $("#custId").val();
    var x = parseInt(c);
    if (x === 0) { $("#to1").val(""); }
}


function hasSupp() {
    var c = $("#sup").val();
    var x = parseInt(c);
    if (x === 0) { $("#tb31").val(""); }
}


function hasFfl() {
    var c = $("#fcd").val();
    var x = parseInt(c);
    if (x === 0) { $("#tb26").val(""); }
}

 
 
 


 

