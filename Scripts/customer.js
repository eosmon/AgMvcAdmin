$(function () { $("#ts13").datepicker({ onSelect: function () { $(this).valid(); } }); });
$(function () { $("#ts14").datepicker({ onSelect: function () { $(this).valid(); } }); });



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
        $("#t40").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Customer/FindCustomers",
                    data: "{ search: '" + request.term + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {

                        if (!data.length) {

                            var t = "<div style=\"width:100%; text-align:center\">No matches found</div>";

                            var result = [{ label: t, value: response.term }];
 

                            return  response(result);

                        }
                        else
                        {
                            response($.map(data, function (item) {

                                    var reg = item.IsReg ? "Registered User" : "Inquiry Only";
                                    var iDiv = item.ProfilePic.length > 0 ? "<img src=\"" + item.ProfilePic + "\" class=\"cust-ui-pic\" />" : "<div style='text-align:center'>Customer Photo Not Found</div>";

                                    var d = "<div style='border:solid 1px black; border-radius:4px; max-width:100%; height:102px; margin-top: -15px !important; margin-bottom: -15px !important;'>";
                                    d += "<div style='text-align: center;width: 5%;display: table-cell;vertical-align: middle;background: black;min-width: 80px; height:100px;'>" + iDiv + "</div>";
                                    d += "<div style='width: 95%;display: table-cell;vertical-align: middle; height:100px;padding-left: 10px'>";
                                    d += "<div class=\"cust-ui-name\">" + item.StrFullName + "</div>";
                                    d += "<div><b>" + item.StrFullAddr + "</b></div>";
                                    d += "<div>" + item.StrEmailPhn + "</div>";
                                    d += "<div>" + item.StrCustType + " Customer</div>";
                                    d += "<div>" + reg + "</div>";
                                    d += "</div>";
                                    d += "</div>";

                                    return { label: d, value: item };
                                }));
                            
                        }  


                    },
                    complete: function () {
                        $("body").scrollTop($(document).height());
                        $("#ui-id-1").append("<li class=\"ui-menu-item\" style=\"text-align:center !important; height:30px; font-weight:14px\"><a class=\"ui-Link\" onclick=\"startNew()\">Create New Customer</a></li>");
                        $("#ui-id-1").append("<li class=\"ui-menu-item\" style=\"text-align:center !important; height:30px; font-weight:14px\"><a class=\"ui-Link\" onclick=\"readBySwipe()\">Create Customer From ID</a></li>");
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
                flushForm();
                $("#dvCustBasic").show();
                $("#dvCustDocs").show();
                getCustomer(id);
            }
        });
    });


    // Autocomplete Supplier Search
    $(function () {

        $("#t52").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Customer/FindSuppliers",
                    data: "{ sid: '" + $("#s21").val() + "', search: '" + request.term + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {

                        var z = $("#s21").val();

                        if (!data.length) {
                            var t = "<div style=\"width:100%; text-align:center\"><div style=\"padding-bottom:3px\">No Matches Found</div><div style=\"border-top:solid 1px white; padding-top:3px\"><a class=\"ui-Link\" onclick=\"newSupplier('" + z +"', 'false')\">Create New Supplier</a></div></div>";

                            var result = [{ label: t, value: response.term }];
                            return response(result);

                        }
                        else {
                            response($.map(data, function (item) {

                                var d = "<div style='vertical-align:top;display: inline-block; vertical-align: middle; padding-left: 10px; font-size:13px !important'>";
                                d += "<div><b>" + item.LineName + "</b></div>";
                                d += "<div>" + item.LineAddress + "</div>";
                                d += "<div><span>P. " + item.Phone + "</span><span style=\"padding-left:10px\">E. " + item.Email + "</span></div>";
                                if (item.LineCurFfl.length > 0) {
                                    d += "<div><span>" + item.LineCurFfl + "</span><span style=\"padding-left:10px\">EXP: " + item.LineCurExp + "</span></div>";
                                }
                                d += "<div><span class=\"link11Green\" href=\"#\" onclick=\"readSupplier('1', '" + item.Id + "', '" + z + "')\">Update</span></div>";
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

                var id = ui.item.value.Id;
                var nm = ui.item.value.LineName;
                var ad = ui.item.value.LineAddress;
                var cu = ui.item.value.LineCurFfl;
                var lo = ui.item.value.PptCtLocal;
                var ot = ui.item.value.PptCtOther;
                var txt = nm + ' : ' + ad;

                if (cu.length > 0) { txt = nm + ' : ' + cu; }

                var p = $("#ppt").val();

                if (p==="1")
                {
                    $("#dvPptCt").show();
                }
                else
                {
                    $("#dvPptCt").hide();
                }

                $("#t49").val(ot);
                $("#t50").val(lo);

                $("#t52").val(txt);
                $("#sup").val(id);


            }
        });
    });


    // Autocomplete Supplier Search
    $(function () {

        $("#tb31").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Customer/FindSuppliers",
                    data: "{ sid: '" + $("#sb23").val() + "', search: '" + request.term + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {

                        var z = $("#sb23").val();

                        if (!data.length) {

                            var t = "<div style=\"width:100%; text-align:center\"><div style=\"padding-bottom:3px\">No Matches Found</div><div style=\"border-top:solid 1px white; padding-top:3px\"><a class=\"ui-Link\" onclick=\"newSupplier('" + z + "', 'false')\">Create New Supplier</a></div></div>";

                            var result = [{ label: t, value: response.term }];
                            return response(result);

                        }
                        else {
                            response($.map(data, function (item) {
                                var d = "<div style='vertical-align:top;display: inline-block; vertical-align: middle; padding-left: 10px; font-size:13px !important'>";
                                d += "<div><b>" + item.LineName + "</b></div>";
                                d += "<div>" + item.LineAddress + "</div>";
                                d += "<div><span>P. " + item.Phone + "</span><span style=\"padding-left:10px\">E. " + item.Email + "</span></div>";
                                if (item.LineCurFfl.length > 0) {
                                    d += "<div><span>" + item.LineCurFfl + "</span><span style=\"padding-left:10px\">EXP: " + item.LineCurExp + "</span></div>";
                                }
                                d += "<div><span class=\"link11Green\" href=\"#\" onclick=\"readSupplier('1', '" + item.Id + "', '" + z + "')\">Update</span></div>";
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

                var id = ui.item.value.Id;
                var nm = ui.item.value.LineName;
                var ad = ui.item.value.LineAddress;
                var cu = ui.item.value.LineCurFfl;
                var lo = ui.item.value.PptCtLocal;
                var ot = ui.item.value.PptCtOther;
                var txt = nm + ' : ' + ad;

                if (cu.length > 0) { txt = nm + ' : ' + cu; }

                var p = $("#ppt").val();

                if (p === "1") {
                    $("#dvPptCt").show();
                }
                else {
                    $("#dvPptCt").hide();
                }

                $("#t49").val(ot);
                $("#t50").val(lo);

                $("#tb31").val(txt);
                $("#sup").val(id);
                $("#ipu").val("false");
            }
        });
    });



    // Autocomplete Pickup Search
    $(function () {
        $("#tb47").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Customer/FindSuppliers",
                    data: "{ sid: '" + $("#sb31").val() + "', search: '" + request.term + "' }",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {

                        var z = $("#sb31").val();

                        if (!data.length) {
                            var t = "<div style=\"width:100%; text-align:center\"><div style=\"padding-bottom:3px\">No Matches Found</div><div style=\"border-top:solid 1px white; padding-top:3px\"><a class=\"ui-Link\" onclick=\"newSupplier('"+z+"', 'true')\">Create New Supplier</a></div></div>";

                            var result = [{ label: t, value: response.term }];
                            return response(result);

                        }
                        else {
                            response($.map(data, function (item) {
                                var d = "<div style='vertical-align:top;display: inline-block; vertical-align: middle; padding-left: 10px; font-size:13px !important'>";
                                d += "<div><b>" + item.LineName + "</b></div>";
                                d += "<div>" + item.LineAddress + "</div>";
                                d += "<div><span>P. " + item.Phone + "</span><span style=\"padding-left:10px\">E. " + item.Email + "</span></div>";
                                if (item.LineCurFfl.length > 0) {
                                    d += "<div><span>" + item.LineCurFfl + "</span><span style=\"padding-left:10px\">EXP: " + item.LineCurExp + "</span></div>";
                                }
                                d += "<div><span class=\"link11Green\" href=\"#\" onclick=\"readSupplier('1', '" + item.Id + "', '" + z + "')\">Update</span></div>";
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

                var id = ui.item.value.Id;
                var nm = ui.item.value.LineName;
                var ad = ui.item.value.LineAddress;
                var cu = ui.item.value.LineCurFfl;
                var txt = nm + ' : ' + ad;

                if (cu.length > 0) { txt = nm + ' : ' + cu; }

                $("#tb47").val(txt);
                $("#pup").val(id);
                $("#ipu").val("true");


            }
        });
    });


})(jQuery);


function updatePptCount()
{
    var s = $("#sup").val();
    var v = $("#t49").val();
    var xs = parseInt(s);
    if (xs < 1) { return; }

    $.ajax({
        data: "{ id: '" + s + "', ct: '" + v + "'}",
        url: "/Customer/SetSupPptOtherCt",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
 
        }
    });

}


function startNew() {
    flushForm();
    $("#ui-id-1").hide();
    $("#dvCustBasic").show();
    $("#dvCustBtnAdd").css("display", "inline-block");

    $("#pnlCustPvtBiz").hide();
    $("#pnlCustLeo").hide();
    $("#pnlCustFfl").hide();
    $("#dvCustCaCmpy").hide();
    $("#dvCustCmpy").hide();
    $("#dvCustBtnEdit").hide();
    $("#dvCustDocs").hide();
    $("#dvCustBtnClr").css("display", "inline-block");
    $("#dvCustBtnCurDoc").hide();
    $("#dvCustBtnEditDoc").hide();
    $("#dvCustSrch").hide();
    $("#dvBtnAdd").css("display", "inline-block");
}

/* PHONE NUMBER FORMATTING */
$(document).ready(function () {
    $("#ct10").usPhoneFormat({ format: '(xxx) xxx-xxxx' });
    $("#ct27").usPhoneFormat({ format: '(xxx) xxx-xxxx' });
    $("#ct28").usPhoneFormat({ format: '(xxx) xxx-xxxx' });

});


/* DATE PICKER FUNCTIONS */
$(function () {
    $("#ct38").datepicker({ onSelect: function () { $(this).valid(); } });
});

$(function () {
    $("#ct39").datepicker({ onSelect: function () { $(this).valid(); } });
});




/* VALIDATE FORM SECTIONS HERE */
$(document).ready(function () {
    $("form[data-form-validate='true']").each(function () {

        $(this).validate({
            rules: {
                EmailAddress: {
                    require_from_group: [1, ".req-group"]
                },
                FirstName: { required: true },
                LastName: { required: true },
                Company: { required: true },
                Phone: {
                    require_from_group: [1, ".req-group"]
                },
                IsCitizen: { required: true },
                LicName: { required: true },
                FflAddress: { required: true },
                FflCity: { required: true },
                FflStateId: { required: true },
                FflZipCode: { required: true },
                BuyForResale: { required: true },
                FflExpMo: { required: true },
                FflExpDay: { required: true },
                FflExpYear: { required: true },
                CaCfdNumber: { required: true },
                CaHasHiCap: { required: true },
                ResaleNumber: { required: true },
                JurisdictionId: { required: true },
                CurAddress: { required: true },
                DivisionId: { required: true },
                RegionName: { required: true },
                DocGroup: { required: true },
                DocType: { required: true },
                DocIdNumber: { required: true },
                DocStateId: { required: true },
                DocExpDate: { required: true },
                DocDob: { required: true },
                DobMonth: { required: true },
                DobDay: { required: true },
                DobYear: { required: true },
                RealID: { required: true },
                SupCurFfl: { required: true },
                SupCurExp: { required: true },
                SupOrg: { required: true },
                SupFirst: { required: true },
                SupLast: { required: true },
                SupAddr: { required: true },
                SupCity: { required: true },
                SupState: { required: true },
                SupZipCode: { required: true },
                SupPhone: { required: true }
 

            },
            messages: {
                EmailAddress: "Valid Email Address Required",
                FirstName: "First Name Required",
                LastName: "Last Name Required",
                Company: "Company Name Required",
                Phone: "Valid Phone Required",
                IsCitizen: "US Citizen Status Required",
                LicName: "FFL: License Name Required",
                FflAddress: "FFL: Address Required",
                FflCity: "FFL: City Required",
                FflStateId: "FFL: State Required",
                FflZipCode: "FFL: Zip Code Required",
                BuyForResale: "Buying for Resale Required",
                FflExpMo: "FFL: Expiration Month Required",
                FflExpDay: "FFL: Expiration Day Required",
                FflExpYear: "FFL: Expiration Year Required",
                CaCfdNumber: "CA FFL: Centralized List Number Required",
                CaHasHiCap: "CA FFL: Hi-Cap Permit Info Required",
                CurAddress: "Verify Current Address Required",
                ResaleNumber: "CA FFL: BOE Resale Number Required",
                JurisdictionId: "Law Enforcement Jurisdiction Type Required",
                DivisionId: "Law Enforcement Division Required",
                RegionName: "City, Co, or State Region Required",
                DocGroup: "Document Group Required",
                DocType: "Document Type Required",
                DocIdNumber: "Document ID Number Required",
                DocStateId: "Document State ID Required",
                DocExpDate: "Document Expiration Date Required",
                DocDob: "Document Date of Birth Required",
                DobMonth: "Birth Month Required",
                DobDay: "Birth Day Required",
                DobYear: "Birth Year Required",
                RealID: "Verify Real ID Required",
                SupCurFfl: "Curio FFL # Required",
                SupCurExp: "Curio Exp Date Required",
                SupOrg: "Organization Name Required",
                SupFirst: "First Name Required",
                SupLast: "Last Name Required",
                SupAddr: "Address Required",
                SupCity: "City Required",
                SupState: "State Required",
                SupZipCode: "Zip Code Required",
                SupPhone: "Phone Required"

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




//ADVANCE CURSOR ON FFL
$(function () {
    $('#ct16,#ct17,#ct18,#ct19,#ct20,#ct21').keyup(function (e) {


        var id = $(this).attr('id');
        var m = '';
        var k = ['ct16', 'ct17', 'ct18', 'ct19', 'ct20', 'ct21', 'btnverifyffl'];

        $.each(['#ct16', '#ct17', '#ct18', '#ct19', '#ct20', '#ct21', '#btnverifyffl'],
            function (index, value) {
                if (id == k[index]) {
                    m = '#' + k[index + 1];
                }
            });

        if ($(this).val().length == $(this).attr('maxlength')) {
            $(m).focus();
        }
    });
});


function closeCustModal() { $("#CustModal").hide(); }

function closeSupModal() { $("#SupModal").hide(); }




function readData(el) {

    var d = el.value;
    flushForm();
    $("#dvCustBasic").show();
    $("#dvCustType").show();

    $("#dvCustBtnAdd").css("display", "inline-block");
    $("#dvCustBtnEdit").hide();

    var g1 = d.split("^");
    var gCityLine = g1[0];
    var gNameLine = g1[1];
    var gAddr = g1[2];

    var gStr = gNameLine.split("$");
    var gFirst = gStr[0];
    var gLast = gStr[1];

    var gState = gCityLine.substring(1, 3);
    var gCity = gCityLine.substring(3, gCityLine.length);

    $("#ct1").val(gLast);
    $("#ct2").val(gFirst);

    $("#ct4").val(gAddr); //Address
    $("#ct6").val(gCity); //City
    $("#cs3 option:contains(" + gState + ")").attr('selected', 'selected'); //State

}

function readBySwipe() {
    $("#ui-id-1").hide();
    $("#dvSwipe").show();

}


function flushForm() {

    $("#custId").val("0");
    $("#cs1").val("0"); //CUSTOMER TYPE
    $("#CusImg_1").empty(); //PROFILE IMG

    $("#cust-basic-info")[0].reset();
    $("#cust-form-profile")[0].reset();
    $("#cust-form-security")[0].reset();
    $("#cust-ppt-biz")[0].reset();
    $("#cust-form-leo")[0].reset();
    $("#cust-form-ffl")[0].reset();
    $("#cust-form-docs")[0].reset();

    $("#dvCustDocRows").empty();
    $("#dvCustDocsGrid").hide();

}


function setCustAddr(v)
{
    switch (v)
    {
        case "":
            $("#dvAddr").hide();
            clearAddress();
            break;
        case "-1":
            $("#dvAddr").show();
            clearAddress();
            break;
        default:
            $("#dvAddr").show();
            getCustAddr(v);
            break;

    }


    //if (v === "-1" || v === "") {
    //    clearAddress();
    //}
    //else
    //{
    //    getCustAddr(v);
    //}
}

function clearAddress()
{
    $("#ct4").val("");
    $("#ct5").val("");
    $("#ct6").val("");
    $("#ct7").val("");
    $("#ct8").val("");

    $("#cs3").prop("selectedIndex", 0);
}



function getCustAddr(id)
{
 
    $.ajax({
        data: "{ id: '" + id + "'}",
        url: "/Customer/GetCustAddress",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (d)
        {
            var v1 = d.Address;
            var v2 = d.Suite;
            var v3 = d.City;
            var v4 = d.StateId;
            var v5 = d.ZipCode;
            var v6 = d.ZipExt;

            $("#ct4").val(v1);
            $("#ct5").val(v2);
            $("#ct6").val(v3);
            $("#cs3").val(v4);
            $("#ct7").val(v5);
            $("#ct8").val(v6);

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
        }
    });
}

//function getSupplier(v, id)
//{
//    $.ajax({
//        data: "{ id: '" + id + "'}",
//        url: "/Customer/GetItemSupplier",
//        type: "POST",
//        contentType: 'application/json; charset=utf-8',
//        success: function (d) {

//            var sid = d.StateId;
//            var stp = d.SupplerTypeId;
//            var idt = d.IdType;
//            var ids = d.IdState;
//            var pcl = d.PptCtLocal;
//            var pco = d.PptCtOther;
//            var pcy = d.PptCtYtd;
//            var fnm = d.FirstName;
//            var lnm = d.LastName;
//            var org = d.OrgName;

//            var adr = d.Address;
//            var cty = d.City;
//            var sta = d.State;
//            var zip = d.ZipCode;
//            var ext = d.ZipExt;
//            var phn = d.Phone;
//            var eml = d.Email;
//            var idn = d.IdNumber;
//            var ffl = d.CurFfl;
//            var dob = d.LineIdDob;
//            var exp = d.LineIdExp;
//            var cxp = d.LineCurExp;


//            $("#ts1").val(ffl);
//            $("#ts2").val(cxp);
//            $("#ts3").val(org);
//            $("#ts4").val(fnm);
//            $("#ts5").val(lnm);
//            $("#ts6").val(adr);
//            $("#ts7").val(cty);

//            $("#ts8").val(zip);
//            $("#ts9").val(ext);
//            $("#ts10").val(phn);
//            $("#ts11").val(eml);
//            $("#ts12").val(idn);
//            $("#ts13").val(dob);
//            $("#ts14").val(exp);

//            $("#ss1").val(sid);
//            $("#ss2").val(idt);
//            $("#ss3").val(ids);

//            lightSupModal();

//            var ttl = "Add Supplier: ";

//            /* SET DEFAULT VISIBILITY */
//            $("#dvSupBtnCnl").css("display", "hidden");
//            $("#dvSupBtnAdd").css("display", "inline-block");
//            $("#dvSupBtnAdd").css("display", "inline-block");
//            $("#dvCfl").hide();
//            $("#dvOrg").hide();
//            $("#dvSid").hide();

//            switch (v) {
//                case "2":
//                    ttl += "Curio-Relic FFL";
//                    $("#dvCfl").show();
//                    $("#dvSid").show();
//                    break;
//                case "3":
//                    ttl += "Private Party";
//                    $("#dvSid").show();
//                    break;
//                case "4":
//                    ttl += "Police Department";
//                    $("#dvOrg").show();
//                    break;
//                case "5":
//                    ttl += "Other Organization";
//                    $("#dvOrg").show();
//                    break;
//                case "6":
//                    ttl += "From Owner's Collection";
//                    break;
//            }

//            $("#dvAddTtl").text(ttl);
            
//        },
//        error: function (err, data) {
//            alert(err);
//        },
//        complete: function () {

//        }
//    });
//}


function readSupplier(s, v, z) {
    $.ajax({
        data: "{ id: '" + v + "'}",
        url: "/Customer/GetItemSupplier",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            lightSupModal();
            $("#dvSupBtnCnl").css("display", "none");
            $("#dvSupBtnEdt").css("display", "none");
            $("#dvSupBtnAdd").css("display", "none");

            var sid = data.StateId;
            var stp = data.SupplerTypeId;

            var ppl = data.PptCtLocal;
            var ppo = data.PptCtOther;

            var idt = data.IdType;
            var ids = data.IdState;
            var fnm = data.FirstName;
            var lnm = data.LastName;
            var org = data.OrgName;
            var adr = data.Address;
            var cty = data.City;
            var zip = data.ZipCode;
            var ext = data.ZipExt;
            var phn = data.Phone;
            var eml = data.Email;
            var ffl = data.CurFfl;
            var idn = data.IdNumber;
            var exp = data.LineCurExp;
            var dob = data.LineIdDob;
            var dxp = data.LineIdExp;

            $("#ts1").val(ffl);
            $("#ts2").val(exp);
            $("#ts3").val(org);
            $("#ts4").val(fnm);
            $("#ts5").val(lnm);
            $("#ts6").val(adr);
            $("#ts7").val(cty);
            $("#ts8").val(zip);
            $("#ts9").val(ext);
            $("#ts10").val(phn);
            $("#ts11").val(eml);
            $("#ts12").val(idn);
            $("#ts13").val(dob);
            $("#ts14").val(dxp);
            $("#ss1").val(sid);
            $("#ss2").val(idt);
            $("#ss3").val(ids);

            var ttl = "Edit Supplier: ";

            switch (z) {
                case "2":
                    ttl += "Curio-Relic FFL";
                    $("#dvCfl").show();
                    $("#dvOrg").hide();
                    $("#dvSid").show();
                    break;
                case "3":
                    ttl += "Private Party";
                    $("#dvCfl").hide();
                    $("#dvOrg").hide();
                    $("#dvSid").show();
                    break;
                case "4":
                    ttl += "Police Department";
                    $("#dvCfl").hide();
                    $("#dvOrg").show();
                    $("#dvSid").hide();
                    break;
                case "5":
                    ttl += "Other Organization";
                    $("#dvCfl").hide();
                    $("#dvOrg").show();
                    $("#dvSid").show();
                    break;
                case "6":
                    ttl += "From Owner's Collection";
                    $("#dvCfl").hide();
                    $("#dvOrg").hide();
                    $("#dvSid").hide();
                    break;
            }

            $("#dvAddTtl").text(ttl);
            $("#dvSupBtnEdt").css("display", "inline-block");

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#sup").val(v);
        }
    });
}


function getCustomer(id) {

    $.ajax({
        data: "{ id: '" + id + "'}",
        url: "/Customer/GetCustomer",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (d) {


            $("#dvCustBtnAdd").hide();
            $("#dvCustBtnAddDoc").hide();

            $("#dvCustBtnEdit").css("display", "inline-block");

            $("#pnlCustPvtBiz").hide();
            $("#pnlCustLeo").hide();
            $("#pnlCustFfl").hide();
            $("#dvCustCaCmpy").hide();
            $("#dvCustCmpy").hide();

            var docs = d.CustomerDocs;
            var pdoc = d.PresentationDocs;

 
            var email = d.CustomerBase.EmailAddress;
            var fName = d.CustomerBase.FirstName;
            var lName = d.CustomerBase.LastName;
            var addrs = d.CustomerBase.Address;
            var suite = d.CustomerBase.Suite;
            var city = d.CustomerBase.City;
            var phone = d.CustomerBase.Phone;
            var state = d.CustomerBase.StateId;
            var zipCd = d.CustomerBase.ZipCode;
            var zipEx = d.CustomerBase.ZipExt;

            var cusId = d.CustomerId;
            var cusTp = d.CustomerTypeId;
            var srcId = d.SourceId;
            var indId = d.IndustryId;
            var prfId = d.ProfessionId;
            var faxNm = d.Fax;
            var notes = d.CustomerNotes;
            var image = d.ProfilePic;
            var isVip = d.IsVip;
            var isWeb = d.IsOnWeb;
            var isCit = d.IsCitizen;
            var isPrs = d.IsPermResident;
            var gunSf = d.CaHasGunSafe;
            var fscId = d.CaFscStatus;
            var resal = d.BuyForResale;
            var boeNm = d.ResaleNumber;

            var allDc = d.DocCtAll;
            var curDc = d.DocCtCurrent;
            var arcDc = d.DocCtArchived;
 
            var uk = d.SecurityBase.UserKey;
            var usrNm = d.SecurityBase.UserName;
            var pswd = d.SecurityBase.Password;
            var resPw = d.SecurityBase.ResetPassword;
            var secQ1 = d.SecurityBase.SecurityQuestion1;
            var secQ2 = d.SecurityBase.SecurityQuestion2;
            var secQ3 = d.SecurityBase.SecurityQuestion3;
            var secA1 = d.SecurityBase.SecurityAnswer1;
            var secA2 = d.SecurityBase.SecurityAnswer2;
            var secA3 = d.SecurityBase.SecurityAnswer3;

            var b1 = isVip ? "true" : "false";
            var b2 = resPw ? "true" : "false";
            var b3 = isCit ? "true" : "false";
            var b4 = gunSf ? "true" : "false";
            var b5 = resal ? "true" : "false";
            var b6 = isWeb ? "true" : "false";
            var b7 = isPrs ? "true" : "false";
            var fx = faxNm.length < 9 ? "" : faxNm;

            var fb3 = hc ? "true" : "false";

            if (!isCit) { $("#dvResType").show(); } else { $("#dvResType").hide(); }

            $("#custUk").val(uk);

            $("#ct28").val(fx);
            $("#ct33").val(usrNm);
            $("#ct34").val(pswd);
            $("#ct35").val(secA1);
            $("#ct36").val(secA2);
            $("#ct37").val(secA3);


            $("#cs2").val(srcId);
            $("#cs3").val(state);
            $("#cs4").val(indId);
            $("#cs9").val(b3);
            $("#cs26").val(b7);
            $("#cs11").val(fscId);
            $("#cs16").val(b1);
            $("#cs17").val(b6);
            $("#cs19").val(secQ1);
            $("#cs20").val(secQ2);
            $("#cs32").val(secQ3);
            $("#cs33").val(b2);


            $("#cta1").val(notes);

            $("#ct1").val(fName);
            $("#ct2").val(lName);
            $("#ct4").val(addrs);
            $("#ct5").val(suite);
            $("#ct6").val(city);
            $("#ct10").val(phone);
            $("#ct7").val(zipCd);
            $("#ct8").val(zipEx);
            $("#ct9").val(email);

            custWebAccess(b6);

            if (image.length > 0) {
                $("#CusImg_1").empty();
                $("#CusImg_1").append("<img src=\"" + image + "\" style='max-width:120px; max-height:92px'>");
            }


            if (indId > 0) {
                $("#dvCustProfession").show();
                getProfessions(prfId);
            } else {
                $("#dvCustProfession").hide();
            }


            //POPULATE ADDRESSES
            var am = $("#cs13");

            $(am).find("option").remove().end();
            am.append("<option value=\"0\">- SELECT -</option>");

            $.each(d.CustAddresses, function (i, item) {
                am.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });
            am.append("<option value=\"-1\">- ADD NEW ADDRESS -</option>");
            $(am).prop("selectedIndex", 1); //1st pos is always current addr


            if (cusTp > 0) {
                $("#dvCustType").show();
                $("#cs1").val(cusTp);


                switch (cusTp) {
                    case 3:
                        $("#pnlCustPvtBiz").show();
                        $("#dvDob").show();

                        $("#cs35").val(d.DobMonth);
                        $("#cs36").val(d.DobDay);
                        $("#cs37").val(d.DobYear);

                        // CALIFORNIA
                        if (state === 5) {
                            $("#dvCaCust").show();
                            $("#cs10").val(b4);
                            //$("#cs11").val(fscId);
                        } else { $("#dvCaCust").hide(); }
                        

                        break;
                    case 1:
                        $("#pnlCustFfl").show();
                        $("#dvCustFflLt").show();
                        $("#dvCustFflRt").show();
                        $("#dvBtnFflSch").show();

                        var f1 = d.FedFireLicBase.LicRegion;
                        var f2 = d.FedFireLicBase.LicDistrict;
                        var f3 = d.FedFireLicBase.LicCounty;
                        var f4 = d.FedFireLicBase.LicType;
                        var f5 = d.FedFireLicBase.LicExpCode;
                        var f6 = d.FedFireLicBase.LicSequence;

                        var fc = d.FedFireLicBase.FflCode;
                        var ln = d.FedFireLicBase.LicName;
                        var tn = d.FedFireLicBase.TradeName;
                        var ad = d.FedFireLicBase.FflAddress;
                        var ct = d.FedFireLicBase.FflCity;
                        var st = d.FedFireLicBase.FflStateId;
                        var zp = d.FedFireLicBase.FflZipCode;

                        var ed = d.FedFireLicBase.FflExpDay;
                        var em = d.FedFireLicBase.FflExpMo;
                        var ey = d.FedFireLicBase.FflExpYear;

                        var ph = d.FedFireLicBase.FflPhone;
                        var cf = d.FedFireLicBase.CaCfdNumber;
                        var hc = d.FedFireLicBase.CaHasHiCap;
                        var ie = d.FedFireLicBase.IsExpired;


                        // CALIFORNIA
                        if (st === 5) {
                            $("#dvCustCaFfl").show();
                            $("#dvCustCaCmpy").show();
                            $("#cs18").val(b5);
                            if (resal) {
                                $("#dvCustResNum").show();
                                $("#ct14").val(boeNm);
                            } else {
                                $("#dvCustResNum").hide();
                            }

                        } else
                        {
                            $("#dvCustCaFfl").hide();
                            $("#dvCustCaCmpy").hide();
                        }

                        $("#fcd").val(fc);
 
                        $("#cs12").val(st);
                        $("#cs15").val(f4);
                        $("#cs21").val(ed);
                        $("#cs22").val(em);
                        $("#cs23").val(ey);
                        $("#cs24").val(fb3);


                        $("#ct16").val(f1);
                        $("#ct17").val(f2);
                        $("#ct18").val(f3);
                        $("#ct19").val(f4);
                        $("#ct20").val(f5);
                        $("#ct21").val(f6);

                        $("#ct22").val(ln);
                        $("#ct23").val(tn);
                        $("#ct24").val(ad);
                        $("#ct25").val(ct);
                        $("#ct26").val(zp);
                        $("#ct27").val(ph);
                        $("#ct29").val(cf);

                        showEnabled(false);

                        if (ie) {
                            $("#dvCustFflExp").css("display", "inline-block");
                        } else {
                            $("#dvCustFflExp").css("display", "none");
                        }

                        break;

                    case 2:

                        $("#pnlCustFfl").show();
                        $("#dvCustFflLt").show();
                        $("#dvCustFflRt").show();
                        $("#dvCustTradeName").hide();
                        $("#dvCustFflPhone").hide();
                        $("#dvCustCaFfl").hide();
                        $("#dvBtnFflSch").hide();
                        
                        var cr1 = d.FedFireLicBase.LicRegion;
                        var cr2 = d.FedFireLicBase.LicDistrict;
                        var cr3 = d.FedFireLicBase.LicCounty;
                        var cr4 = d.FedFireLicBase.LicType;
                        var cr5 = d.FedFireLicBase.LicExpCode;
                        var cr6 = d.FedFireLicBase.LicSequence;

                        var cln = d.FedFireLicBase.LicName;
                        var cad = d.FedFireLicBase.FflAddress;
                        var cct = d.FedFireLicBase.FflCity;
                        var cst = d.FedFireLicBase.FflStateId;
                        var czp = d.FedFireLicBase.FflZipCode;

                        var ced = d.FedFireLicBase.FflExpDay;
                        var cem = d.FedFireLicBase.FflExpMo;
                        var cey = d.FedFireLicBase.FflExpYear;

                        $("#ct16").val(cr1);
                        $("#ct17").val(cr2);
                        $("#ct18").val(cr3);
                        $("#ct19").val(cr4);
                        $("#ct20").val(cr5);
                        $("#ct21").val(cr6);


                        $("#ct22").val(cln);
                        $("#ct24").val(cad);
                        $("#ct25").val(cct);
                        $("#ct26").val(czp);
                        $("#cs12").val(cst);
                        $("#cs15").val("03");
                        $("#cs21").val(cem);
                        $("#cs22").val(ced);
                        $("#cs23").val(cey);

                        setCurio(true);

                        break;

                    case 4:

                        var lj = d.LeoBase.JurisdictionId;
                        var ld = d.LeoBase.DivisionId;
                        var lr = d.LeoBase.RegionName;

                        $("#pnlCustLeo").show();
                        $("#dvDob").show();
                        $("#cs7").val(lj);
                        $("#cs8").val(ld);
                        $("#ct15").val(lr);
                        break;

                    case 5:

                        $("#pnlCustPvtBiz").show();
                        $("#dvCustCmpy").show();
                        $("#dvDob").show();

                        var cmp = d.CompanyName;
                        $("#ct13").val(cmp);

                        // CALIFORNIA
                        if (state === 5) {
                            $("#dvCustCaCmpy").show();
                            $("#dvCaCust").show();
                            $("#cs10").val(b4);
                            //$("#cs11").val(fscId);
                            $("#cs18").val(b5);

                            if (resal) {
                                $("#dvCustResNum").show();
                                $("#ct14").val(boeNm);
                            } else {
                                $("#dvCustResNum").hide();
                            }

                        }
                        else
                        {
                            $("#dvCustCaCmpy").hide();
                            $("#dvCaCust").hide();
                        }

                        break;
                }

            }

            var dtt = "Customer Docs " + "(" + allDc + ")";

            $("#dvDocTtl").text(dtt);
            $('select[name=DocCatId] option:eq(1)').text("Current Documents (" + curDc + ")");
            $('select[name=DocCatId] option:eq(2)').text("Archived Documents (" + arcDc + ")");

            if (docs.length > 0) {
                $("#cs6").val("1");
                cookDocs(docs, cusId);
                loadDocGrid(pdoc);
            }

            $("#dvCustDocs").hide();
            $("#dvCustDocsGrid").hide();

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#custId").val(id);
            $("#dvCustBasic").show();
            $("#dvCustBtnClr").hide();
        }
    });
    
}

function getFfl() {

    var a = validateFflFields();
    if (!a) {
        return;
    }
    //debugger;

    var lic1 = $("#ct16").val();
    var lic2 = $("#ct17").val();
    var lic3 = $("#ct18").val();
    var lic4 = $("#ct19").val();
    var lic5 = $("#ct20").val();
    var lic6 = $("#ct21").val();


    var fileData = new FormData();
    fileData.append("ffl1", lic1);
    fileData.append("ffl2", lic2);
    fileData.append("ffl3", lic3);
    fileData.append("ffl4", lic4);
    fileData.append("ffl5", lic5);
    fileData.append("ffl6", lic6);

    $.ajax({
        cache: false,
        url: '/Customer/GetFfl',
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (data) {

            var ye = data.fflData.FflExists;
            var id = data.fflData.FflId;
            var ie = data.fflData.IsExpired;
            var fc = data.fflData.FflCode;

            $("#fcd").val(fc);
            $("#ch1").val(ye);

            $("#dvCustFflInfo").show();

            if (ye) {
                disableCustFfl(true);
                $("#ch2").val(id);
                $("#ct22").val(data.fflData.LicName);
                $("#ct23").val(data.fflData.TradeName);
                $("#ct24").val(data.fflData.FflAddress);
                $("#ct25").val(data.fflData.FflCity);
                $("#ct26").val(data.fflData.FflZipCode);
                $("#ct27").val(data.fflData.FflPhone);
                $("#cs12").val(data.fflData.FflStateId);
                $("#cs15").val(data.fflData.LicType);

                $("#cs21").val(data.fflData.FflExpMo);
                $("#cs22").val(data.fflData.FflExpDay);
                $("#cs23").val(data.fflData.FflExpYear);


                if (ie) {
                    $("#dvCustFflExp").css("display", "inline-block");
                } else {
                    $("#dvCustFflExp").css("display", "none");
                }

                var caFfl = $("#dvCustCaFfl");
                var caCmpy = $("#dvCustCaCmpy");
                if (data.fflData.FflStateId === 5) {
                    $(caFfl).show();
                    $(caCmpy).show();
                } else {
                    $(caFfl).hide();
                    $(caCmpy).hide();
                }

                $("#dvCustFflLt").show();
                $("#dvCustFflRt").show();
                showEnabled(false);

            } else {
                Lobibox.alert('error',
                    {
                        title: 'FFL Not Found',
                        msg:
                            'Please check the FFL number entered and try again. If you believe the license number is correct and continue to get this error, please call (916) 602-5041 during business hours or email sales@hcpawn.com'
                    });
            }
        },
        error: function (data, error) {
            alert("Status : " + data.responseText);
        }
    });
}

function disableCustFfl(b) {
    var l = ['ct22', 'ct23', 'ct24', 'ct25', 'ct26', 'ct27', 'cs12', 'cs15', 'cs21', 'cs22', 'cs23'];

    for (var i = 0; i < 7; i++) {
        var v = document.getElementById(l[i]);
        v.disabled = b;

        //if (!b) {
        //    v.className = l[i] === "s19" ? "ag-control-short input-sm" : "ag-control input-sm ctrl-width";
        //} else {
            switch (l[i]) {
                case "cs12":
                    v.className = b ? "ag-control-short input-sm ffl-disable" : "ag-control-short input-sm";
                    break;
                case "ct26":
                    v.className = b ? "ag-control input-sm ffl-disable" : "ag-control input-sm";
                    break;
                default:
                    v.className = b ? "ag-control input-sm ctrl-width ffl-disable" : "ag-control input-sm ctrl-width";
                    break;
            }
        //}

    }
}


function clearFflRecip()
{
    $("#sb25").prop('selectedIndex', 0);
    $("#tb26").val("");
    $("#tb46").val("");
    $("#sup").val("0");
    $("#fcd").val("0");
}

function clearFflPickup() {
    $("#sb33").prop('selectedIndex', 0);
    $("#tb48").val("");
    $("#tb49").val("");
    $("#pup").val("0");
    $("#pfc").val("0");
}



function clearFfl() {
    $("#ct16").val('');
    $("#ct17").val('');
    $("#ct18").val('');
    $("#ct19").val('');
    $("#ct20").val('');
    $("#ct21").val('');
}

//function clearFfl() {

//    var v = $("#cs1").val();
//    $("#cust-form-ffl").find("input[type=text], textarea, select").val("");
//    $("#dvCustFflExp").hide();
//    $("#dvCustCaCmpy").hide();
//    $("#errorct16").hide();
//    var validator = $("#cust-form-ffl").validate();
//    validator.resetForm();
//    showEnabled(true);
//    setCurio(true);

    

//    if (v === "2") {
//        $("#dvCustFflInfo").hide();
//        $("#dvCustFflLt").hide();
//        $("#dvCustFflRt").hide();
//    } else {
//        $("#dvCustFflInfo").show();
//        $("#dvCustFflLt").show();
//        $("#dvCustFflRt").show();
//        $("#cs15").val("03");
//    }
 
//}

 
function clearFflNumber() {

    var l = [6];
    var x = 0;
    var eDiv = document.getElementById('errorct16');
    eDiv.style.display = "none";

    for (var i = 16; i < 22; i++) {
        var v = 't' + i;
        l[x] = document.getElementById(v);
        l[x].style.backgroundColor = "";
    }
}

function showCt(v) {
    var vs = parseInt(v);
    var b = $("#dvCustCaCmpy");
    $(b).hide();

    if (vs > 0) {
        $("#dvCustType").show();
        $("#dvCustCt").show();
        setCa();
    } else {
        $("#dvCustType").hide();
        $("#dvCustCt").hide();
    }
}

function showProfession(v) {
    var vs = parseInt(v);
 
}

function setCurio(isC) {
    if (isC) {
        $("#ct22").prop("disabled", false);
        $("#ct24").prop("disabled", false);
        $("#ct25").prop("disabled", false);
        $("#ct26").prop("disabled", false);
        $("#cs12").prop("disabled", false);
        $("#cs21").prop("disabled", false);
        $("#cs22").prop("disabled", false);
        $("#cs23").prop("disabled", false);
    } else {
        $("#ct22").prop("disabled", true);
        $("#ct24").prop("disabled", true);
        $("#ct25").prop("disabled", true);
        $("#ct26").prop("disabled", true);
        $("#cs12").prop("disabled", true);
        $("#cs21").prop("disabled", true);
        $("#cs22").prop("disabled", true);
        $("#cs23").prop("disabled", true);
    }
    
}


function showResale(isFfl, v) {
    if (isFfl) {
        if (v === "true") {
            $("#divFflResNum").show();
        } else {
            $("#divFflResNum").hide();
        }
    } else {
        if (v === "true") {
            $("#dvCustResNum").show();
        } else {
            $("#dvCustResNum").hide();
        }
    }
}

function setCa() {
    var v = $("#cs1").val();
    var caPp = $("#dvCaCust");
    var caFfl = $("#dvCustCaFfl");
    var a = isCa();
    var b = $("#dvCustCaCmpy");

    if (a) {
        $(caPp).show();
        if (v === "1") { $(caFfl).show(); } else { $(caFfl).hide(); } // COMM FFL
        if (v === "5") { $(b).show(); } else { $(b).hide(); } // OTHER BIZ
    } else {
        $(caPp).hide();
        $(caFfl).hide();
    }
}

function isCa() {
    var st = $("#cs3").val();
    var si = parseInt(st);
    var a = (si === 5);
    return a;
}

function setPanel(v) {

    $("#errorct16").hide();
    $("#dvCustFflExp").hide();
    var st = $("#cs3").val();

    $("form").each(function () {
        var a = $(this).attr("id");
        var validator = $(this).validate();

        switch(a) {
            case "cust-basic-info":
            case "cust-form-profile":
                break;
            default:
                $(this)[0].reset();
                validator.resetForm();
                break;
        }
    });


    var vs = parseInt(v);
    
    $("#pnlCustPvtBiz").hide();
    $("#pnlCustFfl").hide();
    $("#pnlCustLeo").hide();
    $("#dvCustCaCmpy").hide();
    $("#dvCustCmpy").hide();
    $("#dvCustCaFfl").hide();
    $("#dvDob").hide();

    switch(vs) {
        case 1: //FFL
            $("#pnlCustFfl").show();
            $("#dvCustTradeName").show();
            $("#dvCustFflPhone").show();
            $("#dvBtnFflSch").show();
            $("#s14").val("0");

            setCurio(false);
            $("#dvCustFflLt").hide();
            $("#dvCustFflRt").hide();
            break;
        case 2: //C&R
            showEnabled(true);
            disableCustFfl(false);
            $("#dvCustFflInfo").show();
            $("#pnlCustFfl").show();
            $("#dvCustTradeName").hide();
            $("#dvCustFflPhone").hide();
            $("#dvBtnFflSch").hide();

            setCurio(true);
            $("#dvCustFflLt").show();
            $("#dvCustFflRt").show();
            $("#cs21").prop("disabled", false);
            $("#cs22").prop("disabled", false);
            $("#cs23").prop("disabled", false);
            $("#cs15").val("03");
            break;
        case 3: //PUB
            $("#pnlCustPvtBiz").show();
            $("#dvDob").show();
            break;
        case 4: //LEO
            $("#pnlCustLeo").show();
            $("#dvDob").show();
            break;
        case 5: //BIZ
            $("#pnlCustPvtBiz").show();
            $("#dvCustCmpy").show();
            $("#dvDob").show();
            if (isCa()) { $("#dvCustCaCmpy").show(); } else { $("#dvCustCaCmpy").hide(); }
            break;
    }

    setCa();
}


function showAddMenuItem(item) {

    $("#addIndustry").hide();
    $("#addProfession").hide();

    var v1 = $("#cs4").val();
    var vs = parseInt(v1);

    if (vs > 0) {
        $("#dvCustProfession").show();
    } else {
        $("#dvCustProfession").hide();
    }

    switch (item) {
    case 1:
        if (vs===-1) {
            $("#ct11").val("");
            $("#addIndustry").show();
        }
        else if (vs > 0) {
            getProfessions(0);
        }
        break;
    case 2:
        var v2 = $("#cs5").val();
        if (v2 === "-1") {
            $("#ct12").val("");
            $("#addProfession").show();
        }
        break;
    }
}

function addIndustry() {

    var n = $("#ct11").val();
    var s = $("#cs4");

    $.ajax({
        data: "{ industry: '" + n + "'}",
        url: "/Customer/AddIndustry",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: "Industry Already Exists",
                        msg: '<b>' + n + '</b> cannot be added because that industry already exists',
                        color: '#000000'
                    });
            } else {

                var si = response.SelectedId;

                $(s).find("option").remove().end();
                s.append("<option value=\"0\">- SELECT -</option>");
                s.append("<option value=\"-1\">ADD NEW INDUSTRY</option>");

                $.each(response.CustIndustry, function (i, item) {
                    s.append("<option value=" + item.Value + ">" + item.Text + "</option>");
                });
                s.append("<option value=\"-2\">DECLINE TO SAY</option>");
                
                s.val(si);

            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#addIndustry").hide();
            $("#dvCustProfession").show();
        }
    });

}


function addProfession() {

    var n = $("#ct12").val();
    var s = $("#cs4").val(); // INDUSTRY
    var p = $("#cs5"); // PROFESSION

    var vs = parseInt(s);

    if (vs < 1) {a
        Lobibox.alert('error',
            {
                title: "Industry Required",
                msg: 'Please select a valid Industry before proceeding',
                color: '#000000'
            });
    }


    $.ajax({
        data: "{ indId: '" + s + "', prof: '" + n + "'}",
        url: "/Customer/AddProfession",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var d = response.IsDuplicate;
            if (d) {
                Lobibox.alert('error',
                    {
                        title: "Profession Already Exists",
                        msg: '<b>' + n + '</b> cannot be added because that profession already exists',
                        color: '#000000'
                    });
            } else {

                var si = response.SelectedId;

                $(p).find("option").remove().end();
                p.append("<option>- SELECT -</option>");
                p.append("<option value=\"-1\">ADD NEW PROFESSION</option>");

                $.each(response.CustProfession, function (i, item) {
                    p.append("<option value=" + item.Value + ">" + item.Text + "</option>");
                });
                p.append("<option value=\"-2\">DECLINE TO SAY</option>");

                p.val(si);

            }
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#addProfession").hide();
        }
    });

}

function flushProfession() {

    var n = $("#cs4").val(); // INDUSTRY
    var p = $("#cs5"); // PROFESSION

}

function getProfessions(v) {

    var n = $("#cs4").val(); // INDUSTRY
    var p = $("#cs5"); 

    $.ajax({
        data: "{ indId: '" + n + "'}",
        url: "/Customer/GetProfessions",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            $(p).find("option").remove().end();
            p.append("<option>- SELECT -</option>");
            p.append("<option value=\"-1\">ADD NEW PROFESSION</option>");

            $.each(response, function (i, item) {
                p.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });
            p.append("<option value=\"-2\">DECLINE TO SAY</option>");

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            if (v > 0) {
                $("#cs5").val(v);
            }
        }
    });
}


function addEditCustomer(io) {

    var a = false;
    var trip = false;

    var ct = $("#cs1").val();
    var br = $("#cs18").val();
    var rn = $("#ct14").val();
    var cid = $("#custId").val();
    var fcd = $("#fcd").val();

    var dmo = $("#cs35").val();
    var ddy = $("#cs36").val();
    var dyr = $("#cs37").val();

    var av = $("#cust-basic-info").valid();
    if (!av) { trip = true; }

    var bv = $("#cust-form-profile").valid();
    if (!bv) { trip = true; }

    var cv = $("#cust-ppt-biz").valid();
    if (!cv) { trip = true; }

    var dv = $("#cust-form-leo").valid();
    if (!dv) { trip = true; }

    var fv = $("#cust-form-security").valid();
    if (!fv) { trip = true; }

    if (ct === "1" || ct === "2") {

        var rv = $("#cust-form-ffl").valid();
        if (!rv) { trip = true; }

        a = validateFflFields();
        if (a === false) { trip = true; }
    }

    if (trip === true) { return false; }

    var fileData = new FormData();

    $(":input", "#cust-basic-info").each(function () {
        fileData.append(this.name, this.value);
    });

    $(":input", "#cust-form-profile").each(function () {
        fileData.append(this.name, this.value);
    });

    $(":input", "#cust-ppt-biz").each(function () {
        fileData.append(this.name, this.value);
    });

    $(":input", "#cust-form-leo").each(function () {
        fileData.append(this.name, this.value);
    });

    $(":input", "#cust-form-ffl").each(function () {
        fileData.append(this.name, this.value);
    });

    $(":input", "#cust-form-security").each(function () {
        fileData.append(this.name, this.value);
    });

    fileData.append("CustomerTypeId", ct);
    fileData.append("BuyForResale", br);
    fileData.append("ResaleNumber", rn);

    fileData.append("DobMonth", dmo);
    fileData.append("DobDay", ddy);
    fileData.append("DobYear", dyr);
    
    var ev = $(event.target).attr("id");

    if (ev === "btnAddNewCust") {
        fileData.append("Action", "Create"); //CREATE
        fileData.append("CustomerId", "0");
        fileData.append("FflCode", fcd);
    } else {
        fileData.append("Action", "Edit"); //UPDATE
        fileData.append("CustomerId", cid);
        fileData.append("FflCode", fcd);

        if (cid === "0") {
            Lobibox.alert('error',
                {
                    title: 'Error: Customer ID Missing',
                    msg: 'Reselect the customer and update to continue'
                });

            return false;
        }
    }

    var file = document.getElementById("ProfileImg").files;
    if (file.length > 0) {
        fileData.append('Files', file[0]);
    }

    $.ajax({
        cache: false,
        url: '/Customer/Create',
        type: 'POST',
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (data) {

            var c = data.iCm;
            var s = data.iSm;

            $("#custId").val(c.CustomerId);
            $("#custUk").val(s.StrUserKey);


            $("#dvCustBasic").hide();
            var an = ev === "btnAddNewCust" ? true : false;
            var nm = "Updated";
            var adc = "add/edit";

            if (an) {
                nm = "Added";
                adc = "add";
                $("#s6").val("3");
                window.scrollTo(0, document.body.scrollHeight);

                $("#dvCustBtnClr").css("display", "inline-block");
            }

            Lobibox.confirm({
                title: "Customer Successfully " + nm + "!",
                msg: "Do you wish to " + adc +" customer documents?",
                modal: true,
                callback: function (lobibox, type) {
                    if (type === 'no') {
                        $("#dvCustDocs").hide();
                        $("#dvCustDocsGrid").hide();
                        if (io)
                        {
                            $("#CustModal").hide();
                            getCustomerInfo(c.CustomerId);
                        }
                        return;
                    } else {
                        if (io) { getCustomerInfo(c.CustomerId); }
                        $("#dvCustDocs").show();
                        $("#dvCustDocsGrid").show();
                        $("#dvCustDocImg").hide();
                        $("#dvCustAddDocs").hide();

                    }
                }
            });


        },
        complete: function () {
            $("#dvCustBtnAdd").hide();
            $("#dvCustBtnClr").hide();
            $("#dvCustBtnEdit").hide();
        },
        error: function (data, error) {
            alert("Status : " + data.responseText);
        }
    });
}


function validateFflFields() {
    var res = true;
    var errCt = 0;
    var l = [6];
    var x = 0;
    var eDiv = document.getElementById("errorct16");

    for (var i = 16; i < 22; i++) {
        var v = 'ct' + i;
        l[x] = document.getElementById(v);

        if (l[x].value.length === 0) {
            l[x].style.backgroundColor = "yellow";
            errCt++;
        }
    }

    if (errCt > 0) {
        eDiv.style.display = "block";
        eDiv.innerHTML = 'Complete FFL License Number is Required';
        eDiv.style.color = "red";
        eDiv.style.fontWeight = "bold";
        res = false;
    } else {
        eDiv.style.display = "none";
    }
    return res;
}

function showEnabled(v) {

    var bgc = "#e5e5e5";
    if (v) { bgc = "#ffffff"; }

    $("#cs12").css("background", bgc);
    $("#cs15").css("background", bgc);
    $("#cs21").css("background", bgc);
    $("#cs22").css("background", bgc);
    $("#cs23").css("background", bgc);
    $("#ct22").css("background", bgc);
    $("#ct23").css("background", bgc);
    $("#ct24").css("background", bgc);
    $("#ct25").css("background", bgc);
    $("#ct26").css("background", bgc);
    $("#ct27").css("background", bgc);
}


function wipeCustForm() {

    $("form[data-form-validate='true']").each(function () {
        $(this).validate().resetForm();
        $(this)[0].reset();
    });

    $("#pnlCustPvtBiz").hide();
    $("#pnlCustLeo").hide();
    $("#pnlCustFfl").hide();

    $("#cs1").val("0");
}


function setDocPanel(v) {

    var di = parseInt(v);
    var id = $("#custId").val();

    flushAddDoc();
    $("#dvCustAddDocs").hide();
    $("#dvCustDocImg").hide();

    switch (di) {
        case 1:
            loadDocs(true, id);
            $("#dvCustBtnAddDoc").hide();
            break;
        case 2:
            loadDocs(false, id);
            $("#dvCustBtnAddDoc").hide();
            break;
        case 3: 
            $("#dvCustAddDocs").show();
            $("#dvCustDocImg").show();
            $("#dvCustBtnAddDoc").show();
            $("#dvCustBtnEditDoc").hide();
            $("#dvCustBtnCurDoc").hide();
            $("#dvCustDocsGrid").hide();
            break;
    }
}
    
function getMenuDocs(v) {

    $("#dvIdn").hide();
    $("#dvExd").hide();
    $("#dvDst").hide();
    $("#dvDob").hide();
    

    //var n = $("#cs14").val(); // CAT
    var p = $("#cs25");

    $.ajax({
        data: "{ catId: '" + v + "'}",
        url: "/Customer/GetDocumentTypes",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            $(p).find("option").remove().end();
            p.append("<option value=\"\">- SELECT -</option>");

            $.each(response, function (i, item) {
                p.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
        }
    });
}

function setProf(c, r) {
    var p = $("#cs25");

    $.ajax({
        data: "{ catId: '" + c + "'}",
        url: "/Customer/GetDocumentTypes",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            $(p).find("option").remove().end();
            p.append("<option>- SELECT -</option>");

            $.each(response, function (i, item) {
                p.append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });

        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $(p).val(r);
        }
    });
}



function setDocType(v) {

    $("#dvIdn").hide(); 
    $("#dvDst").hide(); 
    $("#dvExd").hide(); 
    $("#dvDob").hide(); 

    $.ajax({
        data: "{ docId: '" + v + "'}",
        url: "/Customer/GetSubMenus",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            var id = data.Id;
            var idf = data.IsIdField;
            var sta = data.IsStateField;
            var exp = data.IsExpField;
            var dob = data.IsDobField;
            var adr = data.IsAddrCurrent;
            var rid = data.IsRealId;

            if (idf) { $("#dvIdn").show(); } else { $("#dvIdn").hide(); }
            if (sta) { $("#dvDst").show(); } else { $("#dvDst").hide(); }
            if (exp) { $("#dvExd").show(); } else { $("#dvExd").hide(); }
            if (dob) { $("#dvDob").show(); } else { $("#dvDob").hide(); }
            if (adr) { $("#dvAdc").show(); } else { $("#dvAdc").hide(); }
            if (rid) { $("#dvRid").show(); } else { $("#dvRid").hide(); }

        },
        complete: function () {

        },
        error: function (data, error) {
            alert("Status : " + data.responseText);
        }
    });
}


function clearPic() {
    $("#CusImgDoc").removeAttr("style");
    $("#CusImgDoc").text("");
}

function resetPic() {
    clearPic();
    $("#CusImgDoc").empty();
}


function addDoc() {

    var iv = $("#cust-form-docs").valid();

    var idoc = document.getElementById("AddCustDocImg");
    if (idoc === null) { return; }
    var fl = idoc.files;

    if (fl.length === 0) {
        iv = false;
        $("#CusImgDoc").text("Document Required");
        $("#CusImgDoc").css('background-color', 'yellow');
        $("#CusImgDoc").css("color", "red");
        $("#CusImgDoc").css("font-weight", "bold");
    }

    if(iv) {
        var uid = $("#custId").val();
        var uky = $("#custUk").val();

        var cat = $("#cs14").val(); //document group
        var typ = $("#cs25").val(); //document type
        var sid = $("#cs28").val(); //state id
        var did = $("#ct31").val();
        var exp = $("#ct38").val();
        var dob = $("#ct39").val();
        var adr = $("#cs41").val();
        var rid = $("#cs42").val();


        var fileData = new FormData();
        fileData.append("Uky", uky);
        fileData.append("Uid", uid);
        fileData.append("Cat", cat);
        fileData.append("Typ", typ);
        fileData.append("Sta", sid);
        fileData.append("Did", did);
        fileData.append("Exp", exp);
        fileData.append("Dob", dob);
        fileData.append("Adr", adr);
        fileData.append("Rid", rid);

        var file = document.getElementById("AddCustDocImg").files;
        if (file.length > 0) {
            fileData.append('Files', file[0]);
        }

        $.ajax({
            cache: false,
            url: '/Customer/AddDocument',
            type: 'POST',
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (d) {

                var curDc = d.CustModel.DocCtCurrent;
                var arcDc = d.CustModel.DocCtArchived;
                var allDc = d.CustModel.DocCtAll;

                var dtt = "Customer Docs " + "(" + allDc + ")";

                $("#dvDocTtl").text(dtt);
                $('select[name=DocCatId] option:eq(1)').text("Current Documents (" + curDc + ")");
                $('select[name=DocCatId] option:eq(2)').text("Archived Documents (" + arcDc + ")");


                Lobibox.alert('success',
                    {
                        title: 'Complete',
                        msg: 'Document Added Successfully'
                    });
            },
            complete: function () {
                $("#cs6").prop('selectedIndex', 0);
                $("#dvCustAddDocs").hide();
                $("#dvCustDocImg").hide();
                $("#dvCustBtnAddDoc").hide();
                
                loadDocs(true, uid);
            },
            error: function (data, error) {
                alert("Status : " + data.responseText);
            }
        });
    }
}


function getDoc(v, uid) {
    
    $.ajax({
        data: "{ id: '" + v + "'}",
        url: "/Customer/GetDocById",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            var id = data.Id;
            var catId = data.DocCatId;
            var dType = data.DocTypeId;
            var staId = data.StateId;
            var expDt = data.StrExp;
            var dobDt = data.StrDob;
            var docNm = data.DocNumber;
            var imgUl = data.DocImg;
            var isCur = data.IsCurrent;
            var isRid = data.IsRealId;
            var isAdc = data.IsAddrCurrent;

            var rid = isRid ? "true" : "false";
            var adr = isAdc ? "true" : "false";

            $("#cs14").val(catId);
            $("#ct31").val(docNm);
            $("#cs28").val(staId);
            $("#ct38").val(expDt);
            $("#ct39").val(dobDt);
            $("#cs14").val(catId);
            $("#cs41").val(adr);
            $("#cs42").val(rid);
            
            $("#did").val(id);
            $("#custId").val(uid);
            setProf(catId, dType);
            setDocType(dType);

            if (!isCur) { $("#dvCustBtnCurDoc").show(); } else { $("#dvCustBtnCurDoc").hide(); }

            if (imgUl.length > 0) {
                $("#CusImgDoc").empty();
                $("#CusImgDoc").append("<img src=\"" + imgUl + "\" style='max-width:130px; max-height:130px'>");
            }


        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
            $("#dvCustAddDocs").show();
            $("#dvCustDocImg").show();
            $("#dvCustBtnAddDoc").hide();
            $("#dvCustDocsGrid").hide();
            $("#dvCustBtnEditDoc").css("display", "inline-block");
            
        }
    });

}


function cookDocs(data, uid) {

    $("#dvCustDocRows").empty();

    var rc = "#3366CC";

    if (data != null) {

        $("#dvCustDocsGrid").show();
        $("#dvCustNoDocs").hide();

        var l = data.length;

        if (l > 0) {

            var msg = l + " Documents Found";
            $("#dvCustDocHdr").text(msg);

            $.each(data, function (i, item) {

                var id = item.Id;
                var ver = item.Version;
                var img = item.DocImg;
                var grp = item.DocGroup;
                var typ = item.DocType;
                var num = item.DocNumber;
                var exp = item.StrExp;
                var dob = item.StrDob;

                var block = "<div style='padding:5px; border-bottom:solid 1px black; background-color: " + rc + "'>";
                block += "<div style='text-align: center;width: 5%;display: table-cell;vertical-align: middle; min-width: 60px'><div class='doc-Lnk' href='#' onclick=\"archiveDoc('" + id + "', '" + uid + "')\">archive</div><div class='doc-Lnk' href='#' style='padding-top:8px' onclick=\"getDoc('" + id + "', '" + uid + "')\">update</div></div>";
                block += "<div style='text-align: center;width: 5%;display: table-cell;vertical-align: middle; min-width: 60px;'><span>Ver. " + ver + "</span></div>";

                block += "<div style='text-align: center;width: 10%;display: table-cell;vertical-align: middle; min-width: 80px;'><img id='img" + id + "' alt=\"" + typ + "\" src='" + img + "' class=\"myImg\" style='max-width:80px; height:auto;max-height:54px;' onclick=\"bigImg('img" + id + "')\" /></div>";
                block += "<div style='width: 15%;display: table-cell;vertical-align: middle;'><div style='color:yellow; font-weight:bold'>" + grp + "</div><div>" + typ + "</div><div>ID# " + num + "</div></div>";
                block += "<div style='width: 65%;display: table-cell;vertical-align: middle;'><div>EXP: " + exp + "</div>";
                block += "</div>";

                $("#dvCustDocRows").append(block);

                rc = rc === "#3366CC" ? "#6699CC" : "#3366CC";

            });
        }
        else {
            $("#dvCustDocsGrid").hide();
            //$("#dvCustNoDocs").show();
        }




    }
    else {
        $("#dvCustDocsGrid").hide();
        $("#dvCustNoDocs").show();
    }
}

function setCurrent() {

    var v = $("#did").val();
    var cid = $("#custId").val();

    var fileData = new FormData();
    fileData.append("Id", v);

    $.ajax({
        cache: false,
        url: '/Customer/SetDocCur',
        type: 'POST',
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (d) {

            var curDc = d.DocCtCurrent;
            var arcDc = d.DocCtArchived;

            $('select[name=DocCatId] option:eq(1)').text("Current Documents (" + curDc + ")");
            $('select[name=DocCatId] option:eq(2)').text("Archived Documents (" + arcDc + ")");

        },
        complete: function () {
            loadDocs(true, cid);
            $("#cs6").prop("selectedIndex", 1);
            $("#dvCustBtnEditDoc").hide();
            $("#dvCustBtnCurDoc").hide();
            $("#dvCustAddDocs").hide();
            $("#dvCustDocImg").hide();
        },
        error: function (data, error) {
            alert("Status : " + data.responseText);
        }
    });
}

function editDoc() {

    var v = $("#did").val();
    var uid = $("#uid").val();
    var cid = $("#custId").val();

    var cat = $("#cs14").val();
    var typ = $("#cs25").val();
    var sid = $("#cs28").val();
    var num = $("#ct31").val();
    var exp = $("#ct38").val();
    var dob = $("#ct39").val();
    var adr = $("#cs41").val();
    var rid = $("#cs42").val();


    var fileData = new FormData();
    fileData.append("Id", v);
    fileData.append("Uid", cid);
    fileData.append("Cat", cat);
    fileData.append("Typ", typ);
    fileData.append("Sta", sid);
    fileData.append("Num", num);
    fileData.append("Exp", exp);
    fileData.append("Dob", dob);
    fileData.append("Adr", adr);
    fileData.append("Rid", rid);

    var file = document.getElementById("AddCustDocImg").files;
    if (file.length > 0) {
        fileData.append('Files', file[0]);
    }

    $.ajax({
        cache: false,
        url: '/Customer/EditDocument',
        type: 'POST',
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (data) {

            Lobibox.alert('success',
                {
                    title: 'Complete',
                    msg: 'Document Updated Successfully'
                });
        },
        complete: function () {
            loadDocs(true, cid);
        },
        error: function (data, error) {
            alert("Status : " + data.responseText);
        }
    });

}

function flushAddDoc() {

    $("#cs14").prop("selectedIndex", 0);
    $("#cs25").prop("selectedIndex", 0);
    $("#cs28").prop("selectedIndex", 0);

    $("#ct31").val("");
    $("#ct38").val("");
    $("#ct39").val("");

    $("#dvIdn").hide();
    $("#dvDst").hide();
    $("#dvExd").hide();
    $("#dvDob").hide();


    $("#CusImgDoc").empty();

}


function bigImg(el) {

    var modal = document.getElementById("DocModal");

    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = document.getElementById(el);
    var modalImg = document.getElementById("img02");
    var captionText = document.getElementById("docCaption");
    img.onclick = function () {
        modal.style.display = "block";
        modalImg.src = img.src;
        //img.src = el.src;
        //captionText.innerHTML = el.alt;
    }

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }
}

function closeImgPreview() {
    var modal = document.getElementById("DocModal");
    modal.style.display = "none";
}

function closeDocPreview() {
    var modal = document.getElementById("CustModal");
    modal.style.display = "none";
}

function closeSupPreview() {
    var modal = document.getElementById("SupModal");
    modal.style.display = "none";
}


function loadDocs(b, id) {

    var fileData = new FormData();
    fileData.append("Id", id);
    fileData.append("Cur", b);

    $.ajax({
        cache: false,
        url: "/Customer/GetAllDocs",
        type: "POST",
        contentType: false,
        processData: false,
        data: fileData,
        success: function (data) {
            cookDocs(data, id);
        },
        complete: function () {
            return false;

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function archiveDoc(id, uid) {
    
    Lobibox.confirm({
        title: "Archive Document?",
        msg: "A new document will need to be added once this is archived. Do you wish to continue?",
        modal: true,
        callback: function (lobibox, type) {
            if (type === 'no') {
                return;
            } else {
                var fd = new FormData();
                fd.append("Id", id);

                $.ajax({
                    cache: false,
                    url: "/Customer/ArchiveDoc",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fd,
                    success: function (d) {

                        var curDc = d.DocCtCurrent;
                        var arcDc = d.DocCtArchived;

                        $('select[name=DocCatId] option:eq(1)').text("Current Documents (" + curDc + ")");
                        $('select[name=DocCatId] option:eq(2)').text("Archived Documents (" + arcDc + ")");

                    },
                    complete: function () {
                        loadDocs(true, uid);
                        $("#s6").prop("selectedIndex", 1);
                        $("#dvCustAddDocs").hide();
                        $("#dvCustDocImg").hide();
                    },
                    error: function (err, data) {
                        alert("Status : " + data.responseText);
                    }
                });
            }
        }
    });
}

function loadDocGrid(data) {

    $("#dvDocSummaryRows").empty();

    var rc = "#3366CC";

    if (data != null) {

        $("#dvGridDocs").show();

        var l = data.length;

        if (l > 0) {

            var id = 1;

            var msg = l + " Customer Docs";
            $("#docSumTtl").text(msg);

            $.each(data, function (i, item) {

                var v1 = item.Version;
                var v3 = item.DocType;
                var v2 = item.DocGroup;
                var v4 = item.UsState;
                var v5 = item.DocNumber;
                var v9 = item.DocImg;
                var v6 = item.StrExp;
                var v7 = item.DocStatus;

                var block = "<div class=\"docGrid-row\" style='min-height:47px; background-color: " + rc + "'>";
                block += "<div class=\"docRow-item\">" + v1 + "</div>";
                block += "<div style=\"padding-top:2px\"><img id='img" + id + "' alt=\"" + v3 + "\" src='" + v9 + "' class=\"myImg\" style='max-width:55px; height:auto;max-height:54px;' onclick=\"bigImg('img" + id + "')\" /></div>";
                block += "<div class=\"docRow-item\">" + v2 + "</div>";
                block += "<div class=\"docRow-item\">" + v3 + "</div>";
                block += "<div class=\"docRow-item\">" + v4 + "</div>";
                block += "<div class=\"docRow-item\">" + v5 + "</div>";
                block += "<div class=\"docRow-item\">" + v6 + "</div>";
                block += "<div class=\"docRow-item\">" + v7 + "</div>";
                block += "</div>";

                $("#dvDocSummaryRows").append(block);

                rc = rc === "#3366CC" ? "#6699CC" : "#3366CC";
                id++;

            });
        }
        else {
            $("#dvCustDocsGrid").hide();
        }
    }
}



function custWebAccess(v)
{
    if (v==="true") { $("#dvWebAccess").show(); } else { $("#dvWebAccess").hide(); }
}

//function custFscPanel(v) {
//    if (v === "1") { $("#dvFscCust").show(); } else { $("#dvFscCust").hide(); }
//}

function editCustomer(id) {

    getCustomer(id);

    lightModal();
    $("#dvCustBtnEdit").css("display", "inline-block");
    $("#dvCustBtnClr").css("display", "inline-block");
}

function lightModal() {

    resetCustomer();

    $("#dvCustBtnClr").hide();
    $("#dvCustBtnAdd").hide();
    $("#dvCustBtnEdit").hide();
    $("#dvCustBtnEditDoc").hide();
    $("#dvCustBtnCurDoc").hide();
    $("#dvCustBtnClrDoc").hide();
    $("#dvCustBtnAddDoc").hide();


    $("#ui-id-1").hide();
    $("#dvCustBasic").show();

    $("#pnlCustPvtBiz").hide();
    $("#pnlCustLeo").hide();
    $("#pnlCustFfl").hide();
    $("#dvCustCaCmpy").hide();
    $("#dvCustCmpy").hide();
    $("#dvCustDocs").hide();

    $("#CustModal").show();
}

function lightSupModal()
{
    $("#SupModal").show();
}



function newSupplier(v, b) {

    // args:
    // v = supplier type

    $("#sup").val("0");
    clearSupplier();
    lightSupModal();
    $("#dvSupBtnCnl").css("display", "inline-block");
    $("#dvSupBtnAdd").css("display", "inline-block");
    $("#dvSupBtnEdt").css("display", "none");
    $("#stp").val(v);
    $("#ipu").val(b);

    /* SET DEFAULT VISIBILITY */
    $("#dvCfl").hide();
    $("#dvOrg").hide();
    $("#dvSid").hide();

    var ttl = "Add Supplier: ";

    switch(v)
    {
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

function addSupplier(v)
{
    var iv = $("#form-supplier").valid();
    if (!iv) { return false; }

    var stp = $("#stp").val();
    var sta = $("#ss1").val();
    var idt = $("#ss2").val();
    var ids = $("#ss3").val();
    var cfl = $("#ts1").val();
    var cxp = $("#ts2").val();
    var org = $("#ts3").val();
    var fnm = $("#ts4").val();
    var lnm = $("#ts5").val();
    var adr = $("#ts6").val();
    var cty = $("#ts7").val();
    var zip = $("#ts8").val();
    var ext = $("#ts9").val();
    var phn = $("#ts10").val();
    var eml = $("#ts11").val();
    var idn = $("#ts12").val();
    var dob = $("#ts13").val();
    var exp = $("#ts14").val();

    var fileData = new FormData();
    fileData.append("Stp", stp);
    fileData.append("Sta", sta);
    fileData.append("Idt", idt);
    fileData.append("Ids", ids);
    fileData.append("Cfl", cfl);
    fileData.append("Cxp", cxp);
    fileData.append("Org", org);
    fileData.append("Fir", fnm);
    fileData.append("Las", lnm);
    fileData.append("Adr", adr);
    fileData.append("Cty", cty);
    fileData.append("Zip", zip);
    fileData.append("Ext", ext);
    fileData.append("Phn", phn);
    fileData.append("Eml", eml);
    fileData.append("Idn", idn);
    fileData.append("Dob", dob);
    fileData.append("Exp", exp);

    $.ajax({
        cache: false,
        url: '/Customer/NewSupplier',
        type: 'POST',
        contentType: false,  
        processData: false,  
        data: fileData,
        success: function (data) {

            var nam = fnm + ' ' + lnm;
            var sta = $("#ss1 option:selected").text();
            var ads = adr + ' ' + cty + ', ' + sta + ' ' + zip;
            var txt = nam + ' : ' + ads;

            if (cfl.length > 0) { txt = nam + ' : ' + cfl; }

            switch (v)
            {
                case "1":
                    var pu = $("#ipu").val();
                    if (pu === "true") { $("#tb47").val(txt); } else { $("#tb31").val(txt); }
                    break;
                case "2":
                case "3":
                case "4":
                    $("#t52").val(txt);
                    break;

            }

            $("#sup").val(data);

            Lobibox.alert('success',
                {
                    title: 'Complete',
                    msg: 'Supplier Added'
                });
        },
        complete: function () {
            $("#SupModal").hide();
        },
        error: function (data, error) {
            alert("Status : " + data.responseText);
        }
    });
}

function editSupplier(v) {

    var iv = $("#form-supplier").valid();
    if (!iv) { return false; }

    var sid = $("#sup").val();

    var sta = $("#ss1").val();
    var idt = $("#ss2").val();
    var ids = $("#ss3").val();
    var cfl = $("#ts1").val();
    var cxp = $("#ts2").val();
    var org = $("#ts3").val();
    var fnm = $("#ts4").val();
    var lnm = $("#ts5").val();
    var adr = $("#ts6").val();
    var cty = $("#ts7").val();
    var zip = $("#ts8").val();
    var ext = $("#ts9").val();
    var phn = $("#ts10").val();
    var eml = $("#ts11").val();
    var idn = $("#ts12").val();
    var dob = $("#ts13").val();
    var dxp = $("#ts14").val();

    var fileData = new FormData();
    fileData.append("Sid", sid);
    fileData.append("Sta", sta);
    fileData.append("Idt", idt);
    fileData.append("Ids", ids);
    fileData.append("Cfl", cfl);
    fileData.append("Cxp", cxp);
    fileData.append("Org", org);
    fileData.append("Fir", fnm);
    fileData.append("Las", lnm);
    fileData.append("Adr", adr);
    fileData.append("Cty", cty);
    fileData.append("Zip", zip);
    fileData.append("Ext", ext);
    fileData.append("Phn", phn);
    fileData.append("Eml", eml);
    fileData.append("Idn", idn);
    fileData.append("Dob", dob);
    fileData.append("Exp", dxp);

    $.ajax({
        cache: false,
        url: '/Customer/ModSupplier',
        type: 'POST',
        contentType: false,
        processData: false,
        data: fileData,
        success: function (data) {
            $("#SupModal").hide();

            var nam = fnm + ' ' + lnm;
            var sta = $("#ss1 option:selected").text();
            var ads = adr + ' ' + cty + ', ' + sta + ' ' + zip;
            var txt = nam + ' : ' + ads;

            if (cfl.length > 0) { txt = nam + ' : ' + cfl; }

            switch (v) {
                case "1":
                    var pu = $("#ipu").val();
                    if (pu === "true") { $("#tb47").val(txt); } else { $("#tb31").val(txt); }
                    break;
                case "2":
                case "3":
                case "4":
                    $("#t52").val(txt);
                    break;

            }

            //$("#sup").val(data);

        },
        complete: function () {

            var p = $("#ppt").val();
            if (p === "true") {

                var stn = $("#ss1 option:selected").text();

                $("#dvPptSup").empty();
                var csz = cty + ', ' + stn + ' ' + zip
                //var net = pct + 1;
                //var tax = net > 2 ? "Yes" : "No";


                var d = "<div style='display: inline-block; color: black'>";
                d += "<div><b>" + fnm + ' ' + lnm + "</b></div>";
                d += "<div>" + adr + "</div>";
                d += "<div>" + csz + "</div>";
                d += "<div>P. " + phn + "</div>";
                d += "<div><span style=\"padding-right:5px\">" + sta + " ID # " + idn + "</span> EXP: " + dxp + "</div>";
                //d += "<div><span style=\"padding-right:5px\">YTD PPT Count:" + net + "</span> Taxable: " + tax + "</div>";
                d += "<div><span class=\"link11Blue\" href=\"#\" onclick=\"readSupplier('" + sid + "', '1')\">Update</span></div>";
                d += "</div>";

                $("#dvPptSup").append(d);

            }


            Lobibox.alert('success',
                {
                    title: 'Complete',
                    msg: 'Supplier Updated'
                });
        },
        error: function (data, error) {
            alert("Status : " + data.responseText);
        }
    });
}


function clearSupplier()
{
    $("#sup").val("0");
    $("#form-supplier")[0].reset();
    $("#dvSupBtnAdd").css("display", "none");
    $("#dvSupBtnEdt").css("display", "none");
}




function newCustomer() {

    lightModal();
    $("#dvCustBtnAdd").css("display", "inline-block");
    $("#dvCustBtnClr").css("display", "inline-block");

    $.ajax({
        data: "{}",
        url: "/Customer/TempLogin",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (d) {

            var u = d.UserName;
            var p = d.Password;

            $("#ct33").val(u);
            $("#ct34").val(p);
        },
        error: function (err, data) {
            alert(err);
        },
        complete: function () {
        }
    });
}



function resetCustomer() {

    //clear address menu
    var am = $("#cs13");

    $(am).find("option").remove().end();
    am.append("<option value=\"0\">- SELECT -</option>");
    am.append("<option value=\"-1\">- ADD NEW ADDRESS -</option>");

    //$("#so1").val("0"); //CUSTOMER TYPE
    $("#CusImg_1").empty(); //PROFILE IMG

    $("#cust-basic-info")[0].reset();
    $("#cust-form-profile")[0].reset();
    $("#cust-form-security")[0].reset();
    $("#cust-ppt-biz")[0].reset();
    $("#cust-form-leo")[0].reset();
    $("#cust-form-ffl")[0].reset();
    $("#cust-form-docs")[0].reset();

    $("#dvCustDocRows").empty();
    $("#dvCustDocsGrid").hide();

}


function setAlienType(v)
{
    if (v === "false") { $("#dvResType").show(); } else { $("#dvResType").hide(); }

    
}


 