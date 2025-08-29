$(document).ready(function () {
    $("#t12").usPhoneFormat({ format: '(xxx) xxx-xxxx' });
    $("#t13").usPhoneFormat({ format: '(xxx) xxx-xxxx' });
    $("#t28").usPhoneFormat({ format: '(xxx) xxx-xxxx' });
    $("#t29").usPhoneFormat({ format: '(xxx) xxx-xxxx' });
});

$(document).ready(function () {
    $("#t1").keyup(function () {
        $("#t2").val($(this).val());
    });
});

/* VALIDATE FORM SECTIONS HERE */
$(document).ready(function () {
    $("form[data-form-validate='true']").each(function () {

        $(this).validate({
            rules: {
                EmailAddress: { required: true },
                Username: { required: true },
                Password: { required: true, minlength: 5 },
                FirstName: { required: true },
                LastName: { required: true },
                Address: { required: true },
                City: { required: true },
                StateId: { required: true },
                ZipCode: { required: true, number: true },
                Phone: { required: true },
                SourceId: { required: true },
                CustomerTypeId: { required: true },
                GotStateId: { required: true },
                StateIssued: { required: true },
                DobMonth: { required: true },
                DobDay: { required: true },
                DobYear: { required: true },
                IsCitizen: { required: true },
                CaHasGunSafe: { required: true },
                CaFscStatus: { required: true },
                CaFscNumber: { required: true },
                CaFscExpMo: { required: true },
                CaFscExpDay: { required: true },
                CaFscExpYear: { required: true },
                LicName: { required: true },
                FflAddress: { required: true },
                FflCity: { required: true },
                FflStateId: { required: true },
                FflZipCode: { required: true },
                BuyForResale: { required: true },
                FflExpMo: { required: true },
                FflExpDay: { required: true },
                FflExpYear: { required: true },
                FflPhone: { required: true },
                CaCfdNumber: { required: true },
                CaHasHiCap: { required: true },
                ResaleNumber: { required: true },
                JurisdictionId: { required: true },
                DivisionId: { required: true },
                RegionName: { required: true },
                OfficerNumber: { required: true }

            },
            messages: {
                EmailAddress: "Valid Email Address Required",
                UserName: "User Name Required",
                Password: {
                    required: "Password Required",
                    minlength: "Minimum Length: 5 Characters"
                },
                FirstName: "First Name Required",
                LastName: "Last Name Required",
                Address: "Address Required",
                City: "City Required",
                StateId: "State Required",
                ZipCode: {
                    required: "Zip Code Required",
                    number: "Zip Code must be numeric"
                },
                ZipExt: "Zip Extension must be 4 digits numeric",
                Phone: "Phone Required",
                SourceId: "Referral Source Required",
                CustomerTypeId: "Customer Type Required",
                GotStateId: "Got Valid State ID - Selection Required",
                StateIssued: "Which US State Issued your ID? - Selection Required",
                DobMonth: "Date of Birth: Month Required",
                DobDay: "Date of Birth: Day Required",
                DobYear: "Date of Birth: Year Required",
                IsCitizen: "US Citizen Status Required",
                CaHasGunSafe: "CA Customers: Gun Safe Ownership Status Required",
                CaFscStatus: "CA Firearm Safety Certificate: STATUS Required",
                CaFscNumber: "CA Firearm Safety Certificate: Number Required",
                CaFscExpMo: "CA FSC Certificate: Month Required",
                CaFscExpDay: "CA FSC Certificate: Day Required",
                CaFscExpYear: "CA FSC Certificate: Year Required",
                LicName: "FFL: License Name Required",
                FflAddress: "FFL: Address Required",
                FflCity: "FFL: City Required",
                FflStateId: "FFL: State Required",
                FflZipCode: "FFL: Zip Code Required",
                BuyForResale: "Buying for Resale Required",
                FflExpMo: "FFL: Expiration Month Required",
                FflExpDay: "FFL: Expiration Day Required",
                FflExpYear: "FFL: Expiration Year Required",
                FflPhone: "FFL: Phone Number Required",
                CaCfdNumber: "CA FFL: Centralized List Number Required",
                CaHasHiCap: "CA FFL: Hi-Cap Permit Info Required",
                ResaleNumber: "CA FFL: BOE Resale Number Required",
                JurisdictionId: "Law Enforcement Jurisdiction Type Required",
                DivisionId: "Law Enforcement Division Required",
                RegionName: "City, Co, or State Region Required",
                OfficerNumber: "Officer Number Required"
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


function addNewCustomer() {

    var a = false;
    var trip = false;

    var ct = $("#s5").val();

    $("form").each(function () {
        a = $(this).valid();
        if (a === false) { trip = true; }
    });

    if (ct === "2" || ct === "3") {
        a = validateFflFields();
        if (a === false) { trip = true; }
    }

    if (trip===true) { return false; }

    //alert('trip is' + trip);


    var fileData = new FormData();

    $(":input", "#form-add-customer").each(function () {
        fileData.append(this.name, this.value);
    });

    $(":input", "#form-select-type").each(function () {
        fileData.append(this.name, this.value);
    });

    $(":input", "#form-customer-type").each(function () {
        fileData.append(this.name, this.value);
    });

    var ev = $(event.target).attr("id");
    if (ev === "btnContinue") { fileData.append("Action", "Create"); } else { fileData.append("Action", "Create"); }


    $("input[id^=Svc]").each(function () {
        var fileId = $(this).attr("id");
        var f = document.getElementById(fileId).files;
        if (f.length > 0) {
            fileData.append(f[0].name, f[0]);
            fileData.append("GroupId", fileId);
        }
    });

    $.ajax({
        cache: false,
        url: '/Customer/Create',
        type: 'POST',
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (data) {

        },
        complete: function () {

        },
        error: function (data, error) {
            alert("Status : " + data.responseText);
        }
    });


}


function setCustomerType() {

    hideAllCustomerPanels();
    clearRequiredDocs();
    clearCustomerType();

    var v = $('#s5').val();

    switch (v) {
        case "1":
            $("#pnlPptBiz").show();
            break;
        case "2":
            setCommFfl();
            break;
        case "3":
            setCurioFfl();
            break;
        case "4":
            setLeo();
            break;
        case "5":
            setOtherBiz();
            break;
    }
}


function hideAllCustomerPanels() {
    $("#pnlPptBiz").hide();
    $("#pnlFfl").hide();
    $("#pnlLeo").hide();
    $("#otherBiz").hide();
    $("#divCaPvt").hide();
    $("#divStateId").hide();
    $("#divFsc").hide();
    $('#divFflLeft').hide();
    $('#divFflRight').hide();
}

function clearRequiredDocs() {
    $("#divImg1").hide();
    $("#divImg2").hide();
    $("#divImg3").hide();
    $("#divImg4").hide();
    $("#divImg5").hide();
    $("#divImg6").hide();
    $("#divImg7").hide();
    $("#divImg8").hide();
}

function reqCaDocs() {

    var st = $("#s2").val();
    var ct = $("#s5").val();

    $("#divImg1").hide();
    $("#divImg6").hide();

    if (ct === "4" && st === "5") {
        $("#divImg1").show();
        $("#divImg6").show();
    }
}

function setI9() {
    var res = $("#s12").val();

    if (res === "false") { $("#divImg2").show(); } else { $("#divImg2").hide(); }
}

function setBoe() {
    var res = $("#s20").val();
    if (res === "true") { $("#divImg3").show(); } else { $("#divImg3").hide(); }
}

function setCoe() {
    var res = $("#s19").val();
    if (res === "5") { $("#divImg7").show(); } else { $("#divImg7").hide(); }
}


function setCommFfl() {

    $("#pnlFfl").show();
    $("#divFflExp").hide();

    $("#divGetFfl").show();
    $("#divFflName").show();
    $("#divTradeName").show();
    
    $("#divFflComm").show();
    $("#divBuyResale").show();

    //$('#divImg4').show();
}


function setCurioFfl() {

    $("#pnlFfl").show();
    $("#divFflExp").show();
    $('#divFflLeft').show();
    $('#divFflRight').show();
    $("#divFflName").show();
    $('#divImg4').show();

    $("#divGetFfl").hide();
    $("#divTradeName").hide();
    $("#divFflComm").hide();
    $("#divBuyResale").hide();

    $("#pnlFfl input[type=text]").prop("disabled", false);
    $("#pnlFfl select").prop("disabled", false);

    $("#s21").val("03");
    $("#s21").prop("disabled", true);
    $("#s21").addClass("ffl-disable");
}


function setLeo() {

    $("#pnlLeo").show();

    var st = $('#s2').val();

    if (st === "5") {
        $("#divImg1").show();
        $("#divImg6").show();
    } else {
        $("#divImg1").hide();
        $("#divImg6").hide();
    }
}


function setCaPvt() {
    $("#divCaPvt").find("input[type=text], textarea, select").val("");
    $("#divFsc").hide();
}


function clearCustomerType() {

    $("#form-customer-type")[0].reset();
    $("#pnlFfl input[type=text]").removeClass("ffl-disable");
    $("#pnlFfl select").removeClass("ffl-disable");
    $('#errort17').hide();

}

function setOtherBiz() {
    
    $("#pnlPptBiz").show();
    $("#otherBiz").show();
}


function showFsc() {

    var v = $("#s15").val();
    if (v === "1") { $("#divFsc").show(); $("#divImg8").show(); } else {
        $("#divFsc").hide(); $("#divImg8").hide();
    }
}

function showResale() {

    var v = $("#s20").val();
    if (v === "true") { $("#divResNum").show(); $("#divImg3").show(); } else {
        $("#divResNum").hide(); $("#divImg3").hide();
    }
}


function showStateId() {

    var v = $("#s6").val();
    if (v==="true") { $("#divStateId").show(); } else { $("#divStateId").hide();
    }
}

function showCalifAdds() {

    setCaPvt();

    var v = $("#s8").val();
    if (v === "5") {
        $("#divCaPvt").show();
        $("#divImg1").show();
        setI9();
    } else {
        $("#divCaPvt").hide();
        $("#divImg1").hide();
        $("#divImg2").hide();
        $("#divImg8").hide();
    }
}



//CHECK FFL LICENSE
function checkFfl() {
    var a = validateFflFields();
    if (!a) { return; }

    var lic1 = $("#t17").val();
    var lic2 = $("#t18").val();
    var lic3 = $("#t19").val();
    var lic4 = $("#t20").val();
    var lic5 = $("#t21").val();
    var lic6 = $("#t22").val();


    var fileData = new FormData();
    fileData.append('ffl1', lic1);
    fileData.append('ffl2', lic2);
    fileData.append('ffl3', lic3);
    fileData.append('ffl4', lic4);
    fileData.append('ffl5', lic5);
    fileData.append('ffl6', lic6);

    $.ajax({
        cache: false,
        url: '/Customer/GetFFL',
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (data) {

            var ye = data.fflData.FflExists;
            var id = data.fflData.FflId;
            $("#h1").val(ye);

            if (ye) {
                disableFflFields(true);
                $("#h2").val(id);
                $("#t23").val(data.fflData.LicName);
                $("#t24").val(data.fflData.TradeName);
                $("#t25").val(data.fflData.FflAddress);
                $("#t26").val(data.fflData.FflCity);
                $("#t27").val(data.fflData.FflZipCode);
                $("#t28").val(data.fflData.FflPhone);
                $("#s19").val(data.fflData.FflStateId);
                $("#s21").val(data.fflData.LicType);

                $('#divFflLeft').show();
                $('#divFflRight').show();
                $('#divDocs').show();
                $('#divDocHeading').show();
                $("#divImg4").show();


                if ($("#s19").val() ==="5") {
                    $('#divCaFfl').show();
                    $('#divBuyResale').show();
                    //$('#rcFflCa').show();
                } else {
                    $('#divCaFfl').hide();
                    $('#divBuyResale').hide();
                    //$('#rcFflCa').hide();
                }
            } else {
                Lobibox.alert('error',
                    {
                        title: 'FFL Not Found',
                        msg:
                            'Please check the FFL number entered and try again. If you believe the license number is correct and continue to get this error, please call (916) 504-9642 during business hours or email sales@hcpawn.com'
                    });
            }
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
    var eDiv = document.getElementById('errort17');

    for (var i = 17; i < 23; i++) {
        var v = 't' + i;
        l[x] = document.getElementById(v);

        if (l[x].value.length == 0) {
            l[x].style.backgroundColor = "yellow";
            errCt++;
        }
    }

    if (errCt > 0) {
        eDiv.style.display = "block";
        eDiv.innerHTML = 'Complete FFL License Number is Required';
        eDiv.style.color = "yellow";
        eDiv.style.fontWeight = "bold";
        res = false;
    }
    return res;
}


function clearFflNumber() {

    var v = $('#s5').val();


    $("#pnlFfl").find("input[type=text], textarea, select").val("");
    $("#pnlFfl input[type=text]").removeClass("ffl-disable");
    $("#pnlFfl select").removeClass("ffl-disable");
    disableFflFields(false);
    $('#errort17').hide();

    if (v === "2") {
        $('#divFflLeft').hide();
        $('#divFflRight').hide();
        $("#divImg4").hide();
    } else {
        $("#s21").val("03");
        $("#s21").prop("disabled", true);
        $("#s21").addClass("ffl-disable");
    }


    var l = [6];
    var x = 0;
    //var eDiv = document.getElementById('errort17');
    //eDiv.style.display = "none";

    for (var i = 17; i < 23; i++) {
        var z = "t" + i;
        l[x] = document.getElementById(z);
        l[x].style.backgroundColor = "#FFFFFF";
    }
}


function disableFflFields(b) {
    var l = ['t23', 't24', 't25', 't26', 't27', 's19', 's21'];

    for (var i = 0; i < 7; i++) {
        var v = document.getElementById(l[i]);
        v.disabled = b;

        if (!b) {
            v.className = l[i] === "s19" ? "ag-control-short input-sm" : "ag-control input-sm ctrl-width";
        } else {
            switch(l[i])
            {
                case "s19":
                    v.className = "ag-control-short input-sm ffl-disable";
                    break;
                case "t27":
                    v.className = "ag-control input-sm ffl-disable";
                    break;
                default:
                    v.className = "ag-control input-sm ctrl-width ffl-disable";
                    break;
            }
        }

    }
}


//ADVANCE CURSOR ON FFL
$(function () {
    $('#t17,#t18,#t19,#t20,#t21,#t22').keyup(function (e) {

        var id = $(this).attr('id');
        var m = '';
        var k = ['t17', 't18', 't19', 't20', 't21', 't22', 'btnVerifyFfl'];

        $.each(['#t17', '#t18', '#t19', '#t20', '#t21', '#t22', '#btnVerifyFfl'],
            function (index, value) {
                if (id == k[index]) {
                    m = '#' + k[index + 1];
                }
            });

        if ($(this).val().length == $(this).attr("maxlength")) {
            $(m).focus();
        }
    });
});
