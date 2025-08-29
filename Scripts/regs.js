$(document).ready(function () {
    $("form[data-form-validate='true']").each(function () {

        $(".selectpicker").selectpicker().change(function () {
            $(this).valid();
        });


        $(this).validate({
            rules: {
                Manufacturer: { required: true },
                BookModel: { required: true },
                SerialNumber: { required: true },
                GunType: { required: true },
                Caliber: { required: true },
                AcqDate: { required: true },
                AcqSellerType: { required: true },
                AcqFflState: { required: true },
                AcqFflType: { required: true },
                AcqFflWhs: { required: true },
                AcqFflName: { required: true },
                AcqCurioName: { required: true },
                AcqCurioFfl: { required: true },
                AcqCurioExp: { required: true },
                AcqOrgName: { required: true },
                AcqFirstName: { required: true },
                AcqLastName: { required: true },
                AcqAddress: { required: true },
                AcqCity: { required: true },
                AcqState: { required: true },
                AcqZipCode: { required: true },
                DspDate: { required: true },
                DspSellerType: { required: true },
                DspFflType: { required: true },
                DspFflState: { required: true },
                DspFflWhs: { required: true },
                DspCurioName: { required: true },
                DspCurioFfl: { required: true },
                DspCurioExp: { required: true },
                DspOrgName: { required: true },
                DspFirstName: { required: true },
                DspLastName: { required: true },
                DspAddress: { required: true },
                DspCity: { required: true },
                DspState: { required: true },
                DspZipCode: { required: true }
                
            },
            messages: {
                Manufacturer: "Manufacturer Required",
                BookModel: "Model Required",
                SerialNumber: "Serial Number Required",
                GunType: "Serial Number Required",
                Caliber: "Caliber Required",
                AcqDate: "Acquisition Date Required",
                AcqSellerType: "Acquisition Seller Type Required",
                AcqFflState: "Acquisition FFL State Required",
                AcqFflType: "Acquisition FFL Source Required",
                AcqFflWhs: "Acquisition FFL Warehouse Required",
                AcqFflName: "Acquisition FFL Name Required",
                AcqCurioName: "Acquisition Curio FFL Name Required",
                AcqCurioFfl: "Acquisition Curio FFL License Required",
                AcqCurioExp: "Acquisition Curio FFL Exp Date Required",
                AcqOrgName: "Acquisition Organization Name Required",
                AcqFirstName: "Acquisition First Name Required",
                AcqLastName: "Acquisition Last Name Required",
                AcqAddress: "Acquisition Address Required",
                AcqCity: "Acquisition City Required",
                AcqState: "Acquisition State Required",
                AcqZipCode: "Acquisition Zip Required",
                DspDate: "Disposition Zip Required",
                DspSellerType: "Disposition To Required",
                DspFflType: "Disposition FFL Source Required",
                DspFflState: "Disposition FFL State Required",
                DspFflWhs: "Disposition FFL Warehouse Required",
                DspCurioName: "Disposition Curio Name Required",
                DspCurioFfl: "Disposition Curio FFL License Required",
                DspCurioExp: "Disposition Curio FFL Exp Date Required",
                DspOrgName: "Disposition Organization Name Required",
                DspFirstName: "Disposition First Name Required",
                DspLastName: "Disposition Last Name Required",
                DspAddress: "Disposition Address Required",
                DspCity: "Disposition City Required",
                DspState: "Disposition State Required",
                DspZipCode: "Disposition Zip Required"
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

    // Autocomplete Acquisition FFL
    $(function () {
        $("#t17").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Inventory/FflByName",
                    data: "{ search: '" + request.term + "', state: '" + $("#fsAcq").val() + "' }",
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
                $("#fidAcq").val(id);
                var n = ui.item.value.TradeName;
                var a = ui.item.value.FflAddress;
                var c = ui.item.value.FflCityStZip;
                var fn = ui.item.value.FflNumber;

                var txt = n + ' ' + a + ' ' + c + ' ' + fn;
                $("#t17").val(txt);

                getFflData(id, true);

            }
        });
    });

    // Autocomplete Disposition FFL
    $(function () {
        $("#t3").autocomplete({
            delay: 20,
            source: function (request, response) {
                $.ajax({
                    url: "/Inventory/FflByName",
                    data: "{ search: '" + request.term + "', state: '" + $("#fsDsp").val() + "' }",
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
                $("#fidDsp").val(id);

                var n = ui.item.value.TradeName;
                var a = ui.item.value.FflAddress;
                var c = ui.item.value.FflCityStZip;
                var fn = ui.item.value.FflNumber;

                var txt = n + ' ' + a + ' ' + c + ' ' + fn;
                $("#t3").val(txt);

                getFflData(id, false);
            }
        });
    });






})(jQuery);

function showAcqDate(v)
{
    if (v === "1") { $("#dvAcqDateRange").css("visibility", "visible"); }
    else { $("#dvAcqDateRange").css("visibility", "hidden"); }
}

function showDspDate(v) {
    if (v === "1") { $("#dvDspDateRange").css("visibility", "visible"); }
    else { $("#dvDspDateRange").css("visibility", "hidden"); }
}

function getInput() {

    //var s1 = window.sessionStorage.getItem("s1");
    //if (s1 === null) { s1 = "2"; } //DEFAULT TO WY
    //$("#s1").val(s1);

    var s1 = "1";

    var gunsPerPg = $("#gunsPerPg").val();
    var curPage = $("#curPg").val();
    var startRow = 1;

    if (gunsPerPg === "0") { gunsPerPg = 25; } //default to 25
    if (curPage === "0") { curPage = 1; }

    var pgTtl = ((curPage * gunsPerPg) - gunsPerPg) + 1;
    if (curPage > 1) { startRow = pgTtl; }

    $("#ttlPgs").val(startRow); //set total pages

    var isBkSale = $("#c1").prop("checked");
    var isBkCons = $("#c2").prop("checked");
    var isBkTran = $("#c3").prop("checked");
    var isBkShip = $("#c4").prop("checked");
    var isBkStor = $("#c5").prop("checked");
    var isBkRepr = $("#c6").prop("checked");
    var isBkAcqn = $("#c7").prop("checked");

    var isBkPst = $("#c8").prop("checked");
    var isBkRev = $("#c9").prop("checked");
    var isBkRif = $("#c10").prop("checked");
    var isBkSht = $("#c11").prop("checked");
    var isBkRec = $("#c12").prop("checked");

    var locId = s1; //$("#s1").val();
    var dspId = $("#s2").val();
    var adtId = $("#s3").val();
    var corId = $("#s4").val();
    var srtId = $("#s25").val();
    var ddtId = $("#s3").val();

    var aDateMin = $("#acqSlider").dateRangeSlider("min");
    var aMinTime = Date.parse(aDateMin);
    var aMinDms = new Date(aMinTime);
    var aMinDy = aMinDms.getDate();
    var aMinMo = aMinDms.getMonth() + 1;
    var aMinYr = aMinDms.getFullYear();
    var aFDate = new Date(aMinYr + '-' + aMinMo + '-' + aMinDy);
    var aDateFrom = (aFDate.getMonth() + 1) + '/' + aFDate.getDate() + '/' + aFDate.getFullYear(); 

    var aDateMax = $("#acqSlider").dateRangeSlider("max");
    var aMaxTime = Date.parse(aDateMax);
    var aMaxDms = new Date(aMaxTime);
    var aMaxDy = aMaxDms.getDate();
    var aMaxMo = aMaxDms.getMonth() + 1;
    var aMaxYr = aMaxDms.getFullYear();
    var aTDate = new Date(aMaxYr + '-' + aMaxMo + '-' + aMaxDy);
    var aDateTo = (aTDate.getMonth() + 1) + '/' + aTDate.getDate() + '/' + aTDate.getFullYear();

    var dDateMin = $("#acqSlider").dateRangeSlider("min");
    var dMinTime = Date.parse(dDateMin);
    var dMinDms = new Date(dMinTime);
    var dMinDy = dMinDms.getDate();
    var dMinMo = dMinDms.getMonth() + 1;
    var dMinYr = dMinDms.getFullYear();
    var dFDate = new Date(dMinYr + '-' + dMinMo + '-' + dMinDy);
    var dDateFrom = (dFDate.getMonth() + 1) + '/' + dFDate.getDate() + '/' + dFDate.getFullYear();

    var dDateMax = $("#acqSlider").dateRangeSlider("max");
    var dMaxTime = Date.parse(dDateMax);
    var dMaxDms = new Date(dMaxTime);
    var dMaxDy = dMaxDms.getDate();
    var dMaxMo = dMaxDms.getMonth() + 1;
    var dMaxYr = dMaxDms.getFullYear();
    var dTDate = new Date(dMaxYr + '-' + dMaxMo + '-' + dMaxDy);
    var dDateTo = (dTDate.getMonth() + 1) + '/' + dTDate.getDate() + '/' + dTDate.getFullYear();


 
    var schTxt = $("#t1").val();

    var fileData = new FormData();
    fileData.append("IsSal", isBkSale);
    fileData.append("IsTrn", isBkTran);
    fileData.append("IsCon", isBkCons);
    fileData.append("IsShp", isBkShip);
    fileData.append("IsStr", isBkStor);
    fileData.append("IsRep", isBkRepr);
    fileData.append("IsAqn", isBkAcqn);
    fileData.append("IsPst", isBkPst);
    fileData.append("IsRev", isBkRev);
    fileData.append("IsRif", isBkRif);
    fileData.append("IsSht", isBkSht);
    fileData.append("IsRec", isBkRec);
    fileData.append("AqDateFr", aDateFrom);
    fileData.append("AqDateTo", aDateTo);
    fileData.append("DpDateFr", dDateFrom);
    fileData.append("DpDateTo", dDateTo);
    fileData.append("LocId", locId);
    fileData.append("DspId", dspId);
    fileData.append("CorId", corId);
    fileData.append("SrtId", srtId);
    fileData.append("AdtId", adtId);
    fileData.append("DdtId", ddtId);
    fileData.append("Search", schTxt);
    fileData.append("IPrPg", gunsPerPg);
    fileData.append("StaRw", startRow);

    cookGrid(fileData);

}

function cookGrid(fileData) {

    var beg = "";

    return $.ajax({
        cache: false,
        url: "/Compliance/FillGrid",
        type: "POST",
        contentType: false, 
        processData: false,  
        data: fileData,
        success: function (b) {

            $("#dvBkGrid").empty();

            var x = 1;
            var bgc = "palegoldenrod";
            var lcr = "palegoldenrod";
            var ct = b.length;

            if (ct > 0) {
                var trc = b[0].TotalRowCount;
                $("#ttlRowCt").val(trc);
                $("#gunCountHeader").text(trc + " Records Found");

                $(".prt-ico").show();
                $(".gun-count").show();
                $(".row-Ct").show();
                $(".row-Ct").text(ct + " Records Found");
                $(".testTop").show();
                $(".noResults").hide();
            } else {
                $(".prt-ico").hide();
                $(".row-Ct").hide();
                $(".testTop").hide();
                $(".gun-count").hide();
                $(".noResults").show();
            }

            $.each(b, function(i, item) {

                var id = item.Id;
                var bkc = item.BookCode;
                var tid = item.TransId;
                var mfg = item.GunMfg;
                var imp = item.GunImpt;
                var mdl = item.GunModelName;
                var srl = item.GunSerial;
                var cal = item.GunCaliber;
                var typ = item.GunType;
                var anm = item.AcqName;
                var aaf = item.AcqAddrOrFfl;
                var dnm = item.DispFullName;
                var daf = item.DispAddrOrFfl;
                var dsp = item.IsDisposed;
                var cor = item.IsCorrected;
                var org = item.IsOriginal;

                var adt = item.StrDateAcq;
                var ddt = item.StrDateDsp;
                var mdt = item.StrDateMod;

                mfg = imp.length > 0 ? mfg + " / " + imp : mfg;

                var mcg = mdl + "<br/>" + cal;
                var snt = srl + "<br/>" + typ;

                var anf = anm + "<br/>" + aaf;
 

                var p = 1;
                var dsp1 = "none";
                var dsp2 = "block";
                var bb = "";

 

                if (!cor) {
                    lcr = lcr === "palegoldenrod" ? "lightyellow" : "palegoldenrod";
                }

                //if (tid != beg) {
                //    beg = tid; //set hard line
                //    p = 2;
                //    dsp1 = "none";
                //    dsp2 = "block";
                //} else {
                //    p = 1;
                //    dsp1 = "block";
                //    dsp2 = "none";
                //}

                if (cor) { bgc = "yellow"; } else { bgc = lcr; }
                //if (org) { bgc = lcr; } else { bgc = "yellow"; }

                if (x === ct) {
                    bb = "border-bottom: solid 1px black;";
                }

                if (!dsp && !cor) {
                    ddt = "<a onclick=\"editBook('" + id + "', true)\" class='tLink'>Dispose</a>";
                }

                var ndt = !cor ? mdt : "";

                var b = "<div class=\"bk-grid\" style=\"" + bb + "border-bottom:solid " + p + "px black; background-color: " + bgc + "\">";
                b += "<div class=\"rowData-ctr\">" + bkc + "</div>";
                b += "<div class=\"rowData-ctr\"><a onclick=\"editBook('" + id + "', false)\" class=\"tLink\">" + tid + "</a></div>";
                b += "<div class=\"rowData-ctr\">" + mfg + "</div>";
                b += "<div class=\"rowData-ctr\">" + mcg + "</div>";
                b += "<div class=\"rowData-ctr\">" + snt + "</div>";
                b += "<div class=\"rowData\">" + adt + "</div>";
                b += "<div class=\"rowData-sm\" style=\"border-right: solid 2px black\">" + anf + "</div>";
                b += "<div class=\"rowData-brk\">" + ddt + "</div>";
                b += "<div class=\"rowData-sm\">" + dnm + "</div>";
                b += "<div class=\"rowData-sm\" style=\"border-right: solid 2px black\">" + daf + "</div>";
                b += "<div class=\"rowData-brk\" style=\"border-right: solid 1px black\">" + mdt + "</div>";
                b += "</div>";

                x++;
                $("#dvBkGrid").append(b);

            });

        },
        complete: function () {
            var c = $("#ttlRowCt").val();
            var ci = parseInt(c);
            
            if (ci > 0) {
                $("#divPage").show();
                setBookPaging("#pagerDt1", 1);
                showBookPgRange();
            }
        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function clearEditor() {

    //$("#form-editor")[0].reset();

    $("#s7 > option").each(function () { $(this).removeAttr("selected"); });
    $("#s8 > option").each(function () { $(this).removeAttr("selected"); });
    $("#s9 > option").each(function () { $(this).removeAttr("selected"); });
    $("#s10 > option").each(function () { $(this).removeAttr("selected"); });
    $("#s12 > option").each(function () { $(this).removeAttr("selected"); });
    $("#s13 > option").each(function () { $(this).removeAttr("selected"); });

    $("#s20").prop('disabled', false);


    $("#acqFindFfl").hide();
    $("#acqCurFfl").hide();
    $("#dvNameAcq").hide();
    $("#acqWhs").hide();

    $("#dvFflDsp").hide();
    $("#dvCurDsp").hide();
    $("#dvNameDsp").hide();
    $("#dvWhsDsp").hide();

}

function flushEditor() {
 
    //$("#form-editor")[0].reset();

    $("#s11").selectpicker("refresh");
    $("#s12").selectpicker("refresh");
    $("#s18").selectpicker("refresh");

    //$("#dvTypeCal").show();

    $("#acqFflSrc").hide();
    $("#acqCurFfl").hide();
    $("#acqPvtPty").hide();
    $("#acqOrg").hide();

    $("#dspFflSrc").hide();
    $("#dspCurFfl").hide();
    $("#dspPvtPty").hide();
    $("#dspOrg").hide();
    
    $("#addEdit").val("");
    $("#fsAcq").val("");
    $("#fsDsp").val("");
    $("#fidAcq").val("");
    $("#fidDsp").val("");
    $("#tid").val("");
    $("#rid").val("");
    $("#loc").val("");
    $("#aqFnm").val("");
    $("#aqFlc").val("");
    $("#dpFnm").val("");
    $("#dpFlc").val("");

}


function showCflc() {

    var loc = $("#s1").val();
    var aqt = $("#s13").val(); //acqSrcId
    var dpt = $("#s22").val(); //dspSrcId
    var dsi = $("#s8").val(); //dspStateId

    if (loc === "1") { //CA
        if (aqt === "1") {
            $("#dvCflcIn").show();
        } else {
            $("#dvCflcIn").hide();
        }
    }

    if (dsi === "5" && dpt === "1") {
        $("#dvCflcOut").show();
    } else {
        $("#dvCflcOut").hide();
    }

}

function getBookItemById(loc, id, dpo) {

    flushEditor();

    $.ajax({
        url: "/Compliance/GetBookItem",
        data: "{ locId: '" + loc + "', itemId: '" + id + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (d) {

            //var idp = d.IsDisposed ? "true" : "false";

            var bokId = d.Id;
            var aFlCd = d.AcqFflCode;
            var aFlSt = d.AcqFflStateId;
            var aFlSc = d.AcqFflSrcId;
            var aScId = d.AcqSourceId;
            var aStId = d.AcqStateId;
            var dFlCd = d.DspFflCode;
            var dFlSt = d.DspFflStateId;
            var dFlSc = d.DspFflSrcId;
            var dScId = d.DspSourceId;
            var dStId = d.DspStateId;
            var dFtId = d.DspFulfillTypeId;
            var mfgId = d.ManufId;
            var impId = d.ImporterId;
            var calId = d.CaliberId;
            var gtpId = d.GunTypeId;
            var trnId = d.TransId;
            var serNm = d.GunSerial;
            var aOgNm = d.AcqOrgName;
            var aFNam = d.AcqFirstName;
            var aLNam = d.AcqLastName;
            var aAddr = d.AcqAddress;
            var aCity = d.AcqCity;
            var aZipC = d.AcqZipCode;
            var aZipX = d.AcqZipExt;
            var aCuFl = d.AcqCurioFfl;
            var aCuNm = d.AcqCurioName;
            var dOgNm = d.DispOrgName;
            var dFNam = d.DispFirstName;
            var dMNam = d.DispMiddleName;
            var dLNam = d.DispLastName;
            var dAddr = d.DispAddress;
            var dCity = d.DispCity;
            var dZipC = d.DispZipCode;
            var dZipE = d.DispZipExt;
            var dCuFl = d.DspCurioFfl;
            var dCuNm = d.DspCurioName;
            var cflIb = d.CflcInbound;
            var cflOb = d.CflcOutbound;
            var dtAcq = d.StrDateAcq;
            var dtDsp = d.StrDateDsp;
            var dtAce = d.StrDateAcqCurExp;
            var dtDce = d.StrDateDspCurExp;
            var isDsp = d.IsDisposed;
            var isHcp = d.IsHiCaps;

            $("#s11").val(mfgId); //mfgId
            $("#s12").val(impId); //impId
            $("#s17").val(gtpId); //gtpId
            $("#s18").val(calId); //calId
                $("#t15").val(); //Model
            $("#t16").val(serNm); //Serial

            if (dpo) {
                $("#dvAcqGroup").hide(); // acq block: master show/hide
            }
            else
            {
                var acqFfl = d.AcqFflName + " " + d.AcqFflNumber;

                $("#dvAcqGroup").show(); 
                $("#s13").val(aScId); //acqSrcId
                $("#s14").val(aFlSt); //acqFflSt
                $("#s21").val(aFlSc); //acqFflSrc
                    $("#t17").val(acqFfl); //AcqFFLName

                    $("#t18").val(d.AcqFflName); //AcqFFLName
                    $("#t19").val(d.AcqFflNumber); //AcqFFLNumber
                $("#t20").val(dtAce); //AcqCurExp

                $("#t23").val(aOgNm); //AcqOrg
                $("#t24").val(aFNam); //AcqFirst
                $("#t25").val(aLNam); //AcqLast
                $("#t26").val(aAddr); //AcqAddr
                $("#t27").val(aCity); //AcqCity
                $("#t28").val(aZipC); //AcqZip
                $("#t29").val(aZipX); //AcqZipExt

                if (isDsp) {
                    $("#dvDspEdit").show();
                    $("#dvAcqEdit").css("width", "50%");
                    $("#dvBkSpan").css("width", "100%");

                    $("#t2").val(dtDsp); //Disp Date
                    $("#s22").val(dScId); //dspSrcId
                    $("#s8").val(dFlSt); //dspFFLState

                        $("#t17").val(acqFfl); //DspFFLName

                    $("#t18").val(d.AcqFflName); //DspFFLName
                    $("#t19").val(d.AcqFflNumber); //DspFFLNumber

                }
                else
                {
                    $("#dvDspEdit").hide();
                    $("#dvAcqEdit").css("width", "100%");
                    $("#dvBkSpan").css("width", "50%");
                    
                    
                    
                }

            }


            $("#s7").val(d.DspFflSrc); //dspFflSrc

            $("#s9").val(d.DispFflId); //dspWhs







            $("#s20").val(idp); //isDisp








            $("#t31").val(d.StrDateAcq); //AcqDate


            $("#t33").val(d.CflcInbound); //CFLC IN
            $("#t34").val(d.CflcOutbound); //CFLC OUT

            $("#aqFnm").val(d.AcqName); // acq ffl name
            $("#aqFlc").val(d.AcqAddrOrFfl); // acq address ffl
            $("#dpFnm").val(d.DispName); // dsp name
            $("#dpFlc").val(d.DispAddrOrFfl); // dsp address ffl


            $("#editTtl").text("Edit Entry: " + d.TransId);
            $("#editTtl").show();

            $("#s11").selectpicker("refresh");
            $("#s12").selectpicker("refresh");
            $("#s18").selectpicker("refresh");


            if (d.IsDisposed) {
                $("#s20").prop("disabled", false);
            } else {
                $("#s20").prop("disabled", true);
            }

            if (!dpo) {

                /** ACQUISITION **/
                var l = parseInt(d.AcqTypeId);

                $("#dvCflcIn").hide();
                $("#dvAcqEdit").show();
                $("#dvDspEdit").css("width", "50%");
                $("#dvTypeCal").show();
                $("#dvBtnEdit").hide();
                $("#dvBtnDisp").show();
                $("#dvDspHiCaps").hide();

                switch (l) {
                case 1: //COMMERCIAL FFL
                    $("#acqFflSrc").show();
                    
                    var fsc = parseInt(d.AcqFflSrc);
                    if (fsc > 0) {
                        if (fsc === 99) {

                            $("#acqWhs").hide();
                            $("#acqFindFfl").show();
                            $("#s14").val(d.AcqFflStateId); //AcqFFLState

                        } else {
                            $("#acqWhs").show();
                            $("#acqFindFfl").hide();
                            getWhs(d.AcqFflSrc, "s15", d.AcqFflId);
                        }
                        $("#fidAcq").val(d.AcqFflId);
                    }

                    break;
                case 2: //03 C&R FFL
                    $("#acqWhs").hide();
                    $("#acqFindFfl").hide();
                    $("#acqCurFfl").show();

                    break;
                case 3: //PRIVATE PARTY
                    $("#acqWhs").hide();
                    $("#acqFindFfl").hide();
                    $("#acqPvtPty").hide();
                    $("#acqFname").show();
                    $("#acqLname").show();
                    $("#acqPvtPty").show();
                    $("#t24").val(d.AcqFirstName); //AcqFirst
                    $("#t25").val(d.AcqLastName); //AcqLast
                    $("#t26").val(d.AcqAddress); //AcqAddr
                    $("#t27").val(d.AcqCity); //AcqCity
                    $("#s16").val(d.AcqStateId); //acqFFLState
                    $("#t28").val(d.AcqZipCode); //AcqZip
                    $("#t29").val(d.AcqZipExt); //AcqZipExt
                    break;
                case 4: //POLICE 
                    $("#acqFindFfl").hide();
                    $("#acqFflSrc").hide();
                case 5: //OTHER ORG
                    $("#acqWhs").hide();
                    $("#acqFindFfl").hide();
                    $("#acqOrg").show();
                    $("#acqFname").hide();
                    $("#acqLname").hide();
                    $("#acqPvtPty").show();
                    $("#t23").val(d.AcqOrgName); //AcqOrg
                    $("#t26").val(d.AcqAddress); //AcqAddr
                    $("#t27").val(d.AcqCity); //AcqCity
                    $("#s16").val(d.AcqStateId); //acqFFLState
                    $("#t28").val(d.AcqZipCode); //AcqZip
                    $("#t29").val(d.AcqZipExt); //AcqZipExt
                    break;
                case 6: //OWNER'S COLLECTION
                    $("#acqWhs").hide();
                    $("#acqFindFfl").hide();
                    break;
                }

                if (d.IsDisposed) {
                    $("#dvDispGuts").show();


                    setDispose(d);
                } else {
                    $("#dvDispGuts").hide();
                }


                showCflc();
            }

            else {

                if(d.IsHiCaps) { $("#dvDspHiCaps").show(); } else { $("#dvDspHiCaps").hide(); }

                $("#dvBtnEdit").show();
                $("#dvBtnDisp").hide();

                $("#dvDspEdit").css("width", "100%");
                $("#dvTypeCal").hide();
                $("#dvAcqEdit").hide();
                $("#dvDispGuts").show();
                $("#s20").prop("disabled", false);
                $("#s20").val("true");

                $("#s22").prop('selectedIndex', 0);
                $("#dspFflSrc").hide();
                $("#dspCurFfl").hide();
                $("#dspPvtPty").hide();
                $("#dspFindFfl").hide();

                var dt = new Date();
                var month = dt.getMonth() + 1;
                var day = dt.getDate();
                //var dtFmt = dt.getFullYear() + '/' + (month < 10 ? '0' : '') + month + '/' + (day < 10 ? '0' : '') + day;
                var dtFmt = (month < 10 ? '0' : '') + month + '/' + (day < 10 ? '0' : '') + day + '/' + dt.getFullYear();
                $("#t2").val(dtFmt);

                setDispose(d);
            }

        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            var modal = document.getElementById("dvBbk");
            modal.style.display = "block";
            $("#tid").val(id);
        }
    });
}

function setDispose(d) {
    
    /** DISPOSITION **/
    var m = parseInt(d.DspTypeId);

    switch (m) {
    case 1: //COMMERCIAL FFL
        $("#dspFflSrc").show();
        var dfs = parseInt(d.DspFflSrc);
        if (dfs > 0) {
            if (dfs === 99) {
                var dspFfl = d.DispFflName + " " + d.DispFflNumber;
                $("#dspFindFfl").show();
                $("#s8").val(d.DspFflStateId); //DspFFLState
                $("#t3").val(dspFfl); //DspFFLName
            } else {
                $("#dspWhs").show();
                getWhs(d.DspFflSrc, "s9", d.DispFflId);
            }
            $("#fidDsp").val(d.DispFflId);
        }
        break;
    case 2: //03 C&R FFL
        $("#dspCurFfl").show();
        $("#t4").val(d.DispFflName); //DspFFLName
        $("#t5").val(d.DispFflNumber); //DspFFLNumber
        $("#t6").val(d.StrDspCurExp); //DspCurExp
        break;
    case 3: //PRIVATE PARTY
        $("#dspPvtPty").show();
        $("#dspOrg").hide();
        $("#dspFname").show();
        $("#dspLname").show();
        $("#t8").val(d.DispFirstName); //DspFirst
        $("#t9").val(d.DispLastName); //DspLast
        $("#t10").val(d.DispAddress); //DspAddr
        $("#t11").val(d.DispCity); //DspCity
        $("#s10").val(d.DspStateId); //DspFFLState
        $("#t12").val(d.DispZipCode); //DspZip
        $("#t32").val(d.DispZipExt); //DspZipExt
        break;
    case 4: //POLICE 
    case 5: //OTHER ORG
        $("#dspOrg").show();
        $("#dspFname").hide();
        $("#dspLname").hide();
        $("#dspPvtPty").show();
        $("#t7").val(d.DispOrgName); //DspOrg
        $("#t10").val(d.DispAddress); //DspAddr
        $("#t11").val(d.DispCity); //DspCity
        $("#s10").val(d.DspStateId); //DspFFLState
        $("#t12").val(d.DispZipCode); //DspZip
        $("#t32").val(d.DispZipExt); //DspZipExt
        break;
    case 6: //OWNER'S COLLECTION
        break;
    }

}


//function editBookItem(id, tid) {

//    Lobibox.confirm({
//        title: "Make Corrections To Record " + tid + " ?",
//        msg: "Making changes to this bound book item are permanent and cannot be undone. Do you wish to continue?",
//        modal: true,
//        callback: function (lobibox, type) {
//            if (type === 'no') {
//                return;
//            } else {

//                var dp = $("#s20").val();

//                var acqDate = $("#t7").val();
//                var isDisp = dp === "1" ? true : false;

//                var isImpV = parseInt($("#s8").val());

//                var mfg = $("#s7 option:selected").text();
//                var imp = isImpV > 0 ? $("#s8 option:selected").text() : '';
//                var mdl = $("#t4").val();
//                var ser = $("#t5").val();
//                var typ = $("#s9 option:selected").text();
//                var typId = $("#s9").val();
//                var cal = $("#s10 option:selected").text();

//                var acqTypId = $("#s12").val();
//                var acqNam = "";
//                var acqAdr = "";
//                var acqCurExp = "";
//                var acqFlId = 0;

//                var l = parseInt($("#s12").val());

//                switch (l) {
//                case 100: //COMMERCIAL FFL
//                    var v1 = $("#t9").val();
//                    var v2 = v1.indexOf("FFL:");
//                    acqNam = v1.substring(0, v2);
//                    acqAdr = v1.substring(v2, v1.length);
//                    acqFlId = $("#fidAcq").val();
//                    break;
//                case 150: //03 C&R FFL
//                    acqNam = $("#t10").val();
//                    acqAdr = "FFL: " + $("#t11").val();
//                    acqCurExp = $("#t12").val();
//                    break;
//                case 200: //PRIVATE PARTY
//                case 250: //BUSINESS
//                case 300: //POLICE
//                    acqNam = $("#t33").val();
//                    acqAdr = $("#t34").val();
//                    break;
//                default:
//                    acqFlId = $("#s14").val();
//                    break;
//                }

//                var dspTypId = "";
//                var dspNam = "";
//                var dspAdr = "";
//                var dspCurExp = "";
//                var dspDate = "";
//                var dspFlId = 0;

//                if (isDisp) {

//                    dspTypId = $("#s16").val();
//                    dspDate = $("#t8").val();
//                    var m = parseInt(dspTypId);

//                    switch (m) {
//                    case 100: //COMMERCIAL FFL
//                        var v3 = $("#t20").val();
//                        var v4 = v3.indexOf("FFL:");
//                        dspNam = v3.substring(0, v4);
//                        dspAdr = v3.substring(v4, v3.length);
//                        break;
//                    case 150: //03 C&R FFL
//                        dspNam = $("#t21").val();
//                        dspAdr = "FFL: " + $("#t22").val();
//                        dspCurExp = $("#t23").val();
//                        break;
//                    case 200: //PRIVATE PARTY
//                    case 250: //BUSINESS
//                    case 300: //POLICE
//                        dspNam = $("#t35").val();
//                        dspAdr = $("#t36").val();
//                        break;
//                    default:
//                        dspFlId = $("#s18").val();
//                        break;

//                    }
//                }

//                var fileData = new FormData();
//                fileData.append("BkTrId", tid);
//                fileData.append("BkMfg", mfg);
//                fileData.append("BkImp", imp);
//                fileData.append("BkMdl", mdl);
//                fileData.append("BkSer", ser);
//                fileData.append("BkTyp", typ);
//                fileData.append("BkCal", cal);
//                fileData.append("BkGunTypId", typId);
//                fileData.append("BkAcqTypId", acqTypId);
//                fileData.append("BkAcqDate", acqDate);
//                fileData.append("BkAcqName", acqNam);
//                fileData.append("BkAcqAddr", acqAdr);
//                fileData.append("BkAcqCurExp", acqCurExp);
//                fileData.append("BkAcqFflId", acqFlId);
//                fileData.append("BkIsDisp", isDisp);
//                fileData.append("BkDspTypId", dspTypId);
//                fileData.append("BkDspDate", dspDate);
//                fileData.append("BkDspName", dspNam);
//                fileData.append("BkDspAddr", dspAdr);
//                fileData.append("BkDspCurExp", dspCurExp);
//                fileData.append("BkDspFflId", dspFlId);


//                $.ajax({
//                    cache: false,
//                    url: "/Compliance/EditBook",
//                    type: "POST",
//                    contentType: false,
//                    processData: false,
//                    data: fileData,
//                    success: function () {

//                    },
//                    error: function (err, result) {
//                        alert('there was an error');
//                    },
//                    complete: function () {
//                        location.reload();
//                    }
//                });
//            }
//        }
//    });
//}


function updateItem() {

    var loc = $("#s1").val();
    var iid = $("#tid").val();

    var errCt = 0;

    var iv = $("#form-dispose").valid();
    if (!iv) { errCt++; }

    iv = $("#form-editor").valid();
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

    var aSrc = $("#s13").val();
    var dSrc = $("#s22").val();
    var aFid = "0";
    var dFid = "0";

    if (aSrc === "1") //ACQUISTION COMM FFL 
    {
        var aFsc = $("#s21").val();
        aFid = aFsc === "99" ? $("#fidAcq").val() : $("#s15").val();
    }

    if (dSrc === "1") //DISP COMM FFL 
    {
        var dFsc = $("#s7").val();
        dFid = dFsc === "99" ? $("#fidDsp").val() : $("#s9").val();
    }

 

    var mfdId = $("#s11").val(); // mfg id
    var impId = $("#s12").val(); // imp id
    var calId = $("#s18").val(); // cal id
    var gtpId = $("#s17").val(); // gtp id
    var model = $("#t15").val(); // model
    var seria = $("#t16").val(); // serial #
    var manuf = $("#s11 :selected").text(); // manuf
    var imptr = $("#s12 :selected").text(); // importer
    //var trnId = $("#tid").val(); // trans Id
    var gunTp = $("#s17 :selected").text(); // gun type
    var calib = $("#s18 :selected").text(); // caliber
    var aqDat = $("#t31").val(); // acq date
    var aqFfn = $("#aqFnm").val(); // acq ffl name
    var aqAdf = $("#aqFlc").val(); // acq address ffl
    var aqTyp = $("#s13").val(); // acq source id
    var aqFsc = $("#s21").val(); // acq ffl source id
    var aqFid = aFid; // acq ffl id
    var aqOrg = $("#t23").val(); // acq org name
    var aqFnm = $("#t24").val(); // acq first name
    var aqLnm = $("#t25").val(); // acq last name
    var aqAdr = $("#t26").val(); // acq address
    var aqCty = $("#t27").val(); // acq city
    var aqSid = $("#s16").val(); // acq state id
    var aqSta = $("#s16 :selected").text(); // acq state name
    var aqZip = $("#t28").val(); // acq zip
    var aqExt = $("#t29").val(); // acq zip ext
    var aqCur = $("#t18").val(); // acq cur name
    var aqCuf = $("#t19").val(); // acq cur ffl
    var aqCfe = $("#t20").val(); // acq cur ffl exp
    var aqFsi = $("#s14").val(); // acq ffl state id

    var isDsp = $("#s20").val(); // is disposed
    var dpDat = $("#t2").val(); // is disposed
    var dpFfn = $("#dpFnm").val(); // dsp name
    var dpAdf = $("#dpFlc").val(); // dsp address ffl
    //var dpTyp = $("#s6").val(); // dsp source id
    var dpFsc = $("#s7").val(); // dsp ffl source id
    var dpFid = dFid; // dsp ffl id
    var dpOrg = $("#t7").val(); // dsp org name
    var dpFnm = $("#t8").val(); // dsp first name
    var dpLnm = $("#t9").val(); // dsp last name
    var dpAdr = $("#t10").val(); // dsp address
    var dpCty = $("#t11").val(); // dsp city
    var dpSid = $("#s10").val(); // dsp state id
    var dpSta = $("#s10 :selected").text(); // dsp state name $("#s10 :selected").text();
    var dpZip = $("#t12").val(); // dsp zip
    var dpExt = $("#t32").val(); // dsp zip ext
    var dpCur = $("#t4").val(); // dsp cur name
    var dpCuf = $("#t5").val(); // dsp cur ffl
    var dpCfe = $("#t6").val(); // dsp cur ffl exp
    var dpFsi = $("#s8").val(); // dsp ffl state id


    var aqCfl = $("#t33").val(); //CFLC IN
    var dpCfl = $("#t34").val(); //CFLC OUT

    var fileData = new FormData();
    fileData.append("LocId", loc);
    fileData.append("ItmId", iid);
    fileData.append("MfgId", mfdId);
    fileData.append("ImpId", impId);
    fileData.append("CalId", calId);
    fileData.append("GtpId", gtpId);
    fileData.append("Model", model);
    fileData.append("SerNm", seria);
    fileData.append("Manuf", manuf);
    fileData.append("Imptr", imptr);
    fileData.append("GunTp", gunTp);
    fileData.append("Calib", calib);
    fileData.append("AqCfl", aqCfl);
    fileData.append("AqDat", aqDat);
    fileData.append("AqNam", aqFfn);
    fileData.append("AqAdf", aqAdf);
    fileData.append("AqTyp", aqTyp);
    fileData.append("AqFsc", aqFsc);
    fileData.append("AqFfl", aqFid);
    fileData.append("AqOrg", aqOrg);
    fileData.append("AqFnm", aqFnm);
    fileData.append("AqLnm", aqLnm);
    fileData.append("AqAdr", aqAdr);
    fileData.append("AqCty", aqCty);
    fileData.append("AqSid", aqSid);
    fileData.append("AqSta", aqSta);
    fileData.append("AqZip", aqZip);
    fileData.append("AqExt", aqExt);
    fileData.append("AqCur", aqCur);
    fileData.append("AqCuf", aqCuf);
    fileData.append("AqCue", aqCfe);
    fileData.append("IsDsp", isDsp);
    fileData.append("DpCfl", dpCfl);
    fileData.append("DpDat", dpDat);
    fileData.append("DpTyp", dSrc);
    fileData.append("DpFsc", dpFsc);
    fileData.append("DpFfl", dpFid);
    fileData.append("DpNam", dpFfn);
    fileData.append("DpAdf", dpAdf);
    fileData.append("DpOrg", dpOrg);
    fileData.append("DpFnm", dpFnm);
    fileData.append("DpLnm", dpLnm);
    fileData.append("DpAdr", dpAdr);
    fileData.append("DpCty", dpCty);
    fileData.append("DpSid", dpSid);
    fileData.append("DpSta", dpSta);
    fileData.append("DpZip", dpZip);
    fileData.append("DpExt", dpExt);
    fileData.append("DpCur", dpCur);
    fileData.append("DpCuf", dpCuf);
    fileData.append("DpCue", dpCfe);

    fileData.append("AqFsi", aqFsi);
    fileData.append("DpFsi", dpFsi);

    $.ajax({
        cache: false,
        url: "/Compliance/UpdateBookEntry",
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

function setLocPref(v)
{
    window.sessionStorage.setItem("s1", v);
}

function disposeItem() {

    var errCt = 0;

    var iv =  $("#form-editor").valid();
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

    var loc = $("#s1").val();
    var tid = $("#tid").val();


    var dSrc = $("#s22").val();
    var dFid = "0";

    if (dSrc === "1") //DISP COMM FFL 
    {
        var dFsc = $("#s7").val();
        dFid = dFsc === "99" ? $("#fidDsp").val() : $("#s9").val();
    }

    //var isDsp = $("#s20").val(); // is disposed

    var isDsp = true; // *** ALWAYS TRUE
    var isMag = $("#s23").val(); // dsp hi-cap mags
    var dpDat = $("#t2").val(); // dsp date
    //var dpTyp = $("#s22").val(); // dsp source id
    var dpFsc = $("#s7").val(); // dsp ffl source id
    var dpFid = dFid; // dsp ffl id
    var dpCur = $("#t4").val(); // dsp cur name
    var dpCuf = $("#t5").val(); // dsp cur ffl
    var dpCfe = $("#t6").val(); // dsp cur ffl exp
    var dpOrg = $("#t7").val(); // dsp org name
    var dpFnm = $("#t8").val(); // dsp first name
    var dpLnm = $("#t9").val(); // dsp last name
    var dpAdr = $("#t10").val(); // dsp address
    var dpCty = $("#t11").val(); // dsp city
    var dpSid = $("#s10").val(); // dsp state id
    var dpSta = $("#s10 :selected").text(); // dsp state name $("#s10 :selected").text();
    var dpZip = $("#t12").val(); // dsp zip
    var dpExt = $("#t32").val(); // dsp zip ext
    var dpCfl = $("#t34").val(); // dsp zip ext


    var dpFfn = $("#dpFnm").val(); // dsp name
    var dpAdf = $("#dpFlc").val(); // dsp address ffl

    var fileData = new FormData();
    fileData.append("LocId", loc);
    fileData.append("ItmId", tid);
    //fileData.append("TrnId", tid);
    fileData.append("IsDsp", isDsp);
    fileData.append("IsMag", isMag);
    fileData.append("DpDat", dpDat);
    fileData.append("DpTyp", dSrc);
    fileData.append("DpFsc", dpFsc);
    fileData.append("DpFfl", dpFid);
    fileData.append("DpNam", dpFfn);
    fileData.append("DpAdf", dpAdf);
    fileData.append("DpOrg", dpOrg);
    fileData.append("DpFnm", dpFnm);
    fileData.append("DpLnm", dpLnm);
    fileData.append("DpAdr", dpAdr);
    fileData.append("DpCty", dpCty);
    fileData.append("DpSid", dpSid);
    fileData.append("DpSta", dpSta);
    fileData.append("DpZip", dpZip);
    fileData.append("DpExt", dpExt);
    fileData.append("DpCur", dpCur);
    fileData.append("DpCuf", dpCuf);
    fileData.append("DpCue", dpCfe);
    fileData.append("DpCfl", dpCfl);

    $.ajax({
        cache: false,
        url: "/Compliance/DisposeGunEntry",
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


function setSellerType(isAcq) {

    var a = $("#dvFflDsp");
    var b = $("#dvWhsDsp");
    var c = $("#dvCurDsp");
    var d = $("#dvNameDsp");

    var v = $("#s16").val();


    if (isAcq) {
        a = $("#acqFindFfl");
        b = $("#acqWhs");
        c = $("#acqCurFfl");
        d = $("#dvNameAcq");

        v = $("#s12").val();
    }

    clearFfl(isAcq);
    b.hide();
    a.hide();



    var l = parseInt(v);

    switch (l) {
    case 1: //DISTIBUTOR
    case 2:
    case 3:
    case 5:
    case 6:
    case 7:
    case 8:
    case 9:
    case 10:
    case 11:
    case 12:
    case 13:
        a.hide();
        b.show();
        c.hide();
        d.hide();
        setWhs(l, isAcq);
        break;
    case 100: //COMMERCIAL FFL
        a.show();
        c.hide();
        d.hide();
        break;
    case 150: //03 C&R FFL
        a.hide();
        c.show();
        d.hide();
        break;
    case 200: //PRIVATE PARTY
    case 250: //BUSINESS
    case 300: //POLICE
        a.hide();
        c.hide();
        d.show();
        break;
    default:
        a.hide();
        c.hide();
        d.hide();
        break;

    }
}


function clearFfl(isAcq) {
    if (isAcq) {
        $("#s13").prop('selectedIndex', 0);
        $("#t9").val("");
        $("#fidAcq").val("");
        $("#fsAcq").val("");
    } else {
        $("#s17").prop('selectedIndex', 0);
        $("#t20").val("");
        $("#fidDsp").val("");
        $("#fsDsp").val("");
    }
}

function printFormatMain() {
    $("#dvHd").css("text-align", "left");
    $("#dvHd").css("margin-left", ".5px");
    $("#dvPrt").css("position", "absolute");
    $("#dvPrt").css("left", "10px");
}


function printFormatMags() {

    $(".grid-head-mags-4").css("display", "none");
    $(".grid-mags").css("grid-template-columns", "40px 120px 50px 250px 70px 240px 70px 240px");
    $(".grid-mags").css("float", "left");
    $(".grid-mags-del").css("display", "none");
}


function printAll() {
    window.print();
}


function printIt(p) {

    pcf(p);
}


async function pcf(p) {

    var v = $("#ttlRowCt").val();
    $("#gunsPerPg").val(v);

    //let response = await buildCflc();


    switch(p) {
        case "1":
        {
            await getInput();
            break;
        }
        case "2":
        {
            await buildCflc(); 
            break;
        }
        case "3":
        {
            await buildHiCaps();
            printFormatMags();
            break;
        }
    }

    printFormatMain();

    await new Promise((resolve, reject) => window.print());

}

var afterPrint = function() {
    $("#dvHd").css("text-align", "center");
    $("#dvHd").css("margin-left", "0");
    $("#dvPrt").css("position", "");
    $("#dvPrt").css("left", "");
}

window.onafterprint = afterPrint;





function getWhs(id, c, v) {

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
            d.val(v);
        }
    });
}


function setWhs(id, isAcq, optId) {

    var d = $("#s18");

    if (isAcq) {
        d = $("#s14");
    }

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
            d.val(optId);
        }
    });
}


function setBooks(c) {
    if (c) {
        $("#c1").prop("checked", true);
        $("#c2").prop("checked", true);
        $("#c3").prop("checked", true);
        $("#c4").prop("checked", true);
        $("#c5").prop("checked", true);
        $("#c6").prop("checked", true);
        $("#c7").prop("checked", true);

        $("#c1").prop("disabled", true);
        $("#c2").prop("disabled", true);
        $("#c3").prop("disabled", true);
        $("#c4").prop("disabled", true);
        $("#c5").prop("disabled", true);
        $("#c6").prop("disabled", true);
        $("#c7").prop("disabled", true);
    }
    else
    {
        $("#c1").prop("checked", false);
        $("#c2").prop("checked", false);
        $("#c3").prop("checked", false);
        $("#c4").prop("checked", false);
        $("#c5").prop("checked", false);
        $("#c6").prop("checked", false);
        $("#c7").prop("checked", false);

        $("#c1").prop("disabled", false);
        $("#c2").prop("disabled", false);
        $("#c3").prop("disabled", false);
        $("#c4").prop("disabled", false);
        $("#c5").prop("disabled", false);
        $("#c6").prop("disabled", false);
        $("#c7").prop("disabled", false);
    }
}

function setGunTypes(c) {
    if (c) {
        $("#c8").prop("checked", true);
        $("#c9").prop("checked", true);
        $("#c10").prop("checked", true);
        $("#c11").prop("checked", true);
        $("#c12").prop("checked", true);

        $("#c8").prop("disabled", true);
        $("#c9").prop("disabled", true);
        $("#c10").prop("disabled", true);
        $("#c11").prop("disabled", true);
        $("#c12").prop("disabled", true);

    }
    else {
        $("#c8").prop("checked", false);
        $("#c9").prop("checked", false);
        $("#c10").prop("checked", false);
        $("#c11").prop("checked", false);
        $("#c12").prop("checked", false);

        $("#c8").prop("disabled", false);
        $("#c9").prop("disabled", false);
        $("#c10").prop("disabled", false);
        $("#c11").prop("disabled", false);
        $("#c12").prop("disabled", false);
    }
}

function setState(v, isAcq) {

    if (isAcq) {
        $("#fsAcq").val(v);
    } else {
        $("#fsDsp").val(v);
    }

    if (v === "5" && !isAcq) { $("#dvCflcOut").show(); } else { $("#dvCflcOut").hide(); }
}


function filterAcqDsp() {
    var t = $("#s3").val();

    $("#dvAcqDateRange").css("visibility", "hidden");

    switch (t) {
        case "0":
            $("#s2 option[value=\"1\"]").hide();
            $("#s2 option[value=\"2\"]").show();
            break;
        case "1":
            $("#s2 option[value=\"1\"]").show();
            $("#s2 option[value=\"2\"]").hide();
            break;
        default:
            $("#s2 option[value=\"1\"]").show();
            $("#s2 option[value=\"2\"]").show();
            break;
    }
}


$(document).ready(function () {
    $("#s23").change(function () {
        var t = $(this).val();
        $("#fs").val(t);
    });
});

function setAcqSrc(v) {

    //clearFflSelect();

    $("#acqFflSrc").hide();
    $("#acqPvtPty").hide();
    $("#acqWhs").hide();
    $("#acqFindFfl").hide();
    $("#acqCurFfl").hide();
    $("#acqFname").hide();
    $("#acqLname").hide();
    $("#acqOrg").hide();

    var l = parseInt(v);
    switch (l) {
    case 1: //FFL COMM
        $("#acqFflSrc").show();
        $("#dvCflcIn").show();
        break;
    case 2: //CURIO
        $("#acqCurFfl").show();
        $("#dvCflcIn").hide();
        break;
    case 3: //PUBLIC
        $("#acqPvtPty").show();
        $("#acqFname").show();
        $("#acqLname").show();
        $("#dvCflcIn").hide();
        break;
    case 4:
    case 5: //POLICE - ORG
        $("#acqPvtPty").show();
        $("#acqOrg").show();
        $("#dvCflcIn").hide();
        break;
    case 6:
        $("#dvCflcIn").hide();
        break;

    }

}

function setDspSrc(v) {

    //clearDispFflSelect();

    $("#dspFflSrc").hide();
    $("#dspPvtPty").hide();
    $("#dspWhs").hide();
    $("#dspFindFfl").hide();
    $("#dspCurFfl").hide();
    $("#dspFname").hide();
    $("#dspLname").hide();
    $("#dspOrg").hide();

    dspFlushCommFfl();
    dspFlushCurRel();
    dspFlushPvtPty();
    dspFlushOrg();

    var l = parseInt(v);
    switch (l) {
    case 1: //FFL COMM
        $("#dspFflSrc").show();
        break;
    case 2: //CURIO
        $("#dspCurFfl").show();
        break;
    case 3: //PUBLIC
        $("#dspPvtPty").show();
        $("#dspFname").show();
        $("#dspLname").show();
        break;
    case 4:
    case 5: //POLICE - ORG
        $("#dspPvtPty").show();
        $("#dspOrg").show();
        break;
    }

}

function dspFlushCommFfl() {
    $("#s7").prop('selectedIndex', 0);
    $("#s8").prop('selectedIndex', 0);
    $("#s9").prop('selectedIndex', 0);
    $("#t3").val("");
    $("#fidDsp").val("");
}

function dspFlushCurRel() {
    $("#t4").val("");
    $("#t5").val("");
    $("#t6").val("");
}

function dspFlushPvtPty() {
    $("#t8").val("");
    $("#t9").val("");
    $("#t10").val("");
    $("#t11").val("");
    $("#t12").val("");
    $("#t32").val("");
    $("#s10").prop('selectedIndex', 0);
}

function dspFlushOrg() {
    $("#t7").val("");
    $("#t10").val("");
    $("#t11").val("");
    $("#t12").val("");
    $("#t32").val("");
    $("#s10").prop('selectedIndex', 0);
}

//function clearDispFflSelect() {
//    $("#s8").prop('selectedIndex', 0);
//    $("#t3").val("");
//    $("#dfid").val("");
//    $("#dfs").val("");
//}

function acqFflSelect(v) {
    var l = parseInt(v);

    if (l === 99) {
        $("#acqFindFfl").show();
        $("#acqWhs").hide();
    } else {
        $("#acqFindFfl").hide();
        $("#acqWhs").show();
        setWarehouse(v, "s15");
    }
}


function dspFflSelect(v) {
    var l = parseInt(v);

    if (l === 99) {
        $("#dspFindFfl").show();
        $("#dspWhs").hide();
    } else {
        $("#dspFindFfl").hide();
        $("#dspWhs").show();
        setWarehouse(v, "s9");
    }
}

function setWarehouse(id, c) {

    var d = $("#"+c);

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


function getFflData(id, ia) {

    $.ajax({
        data: "{ fflId: '" + id + "'}",
        url: "/Compliance/GetFflBookData",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            if (ia) {
                $("#aqFnm").val(result.TradeName);
                $("#aqFlc").val(result.FflFullLic);
            } else {
                $("#dpFnm").val(result.TradeName);
                $("#dpFlc").val(result.FflFullLic);
            }


        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {

        }
    });
}


window.onclick = function (event) {
    var modal = document.getElementById("dvBbk");
    if (event.target === modal) {
        modal.style.display = "none";
    }
}



function editBook(id, dpo) {

    var lc = $("#s1").val();
    getBookItemById(lc, id, dpo);
}




function closeDiv(d) {
    var modal = document.getElementById(d);
    modal.style.display = "none";
}


 


function buildCflc() {

    var gunsPerPg = $("#gunsPerPg").val();
    var curPage = $("#curPg").val();
    var startRow = 1;

    if (gunsPerPg == null) { gunsPerPg = 100; } //default to 100
    var pgTtl = ((curPage * gunsPerPg) - gunsPerPg) + 1;
    if (curPage > 1) { startRow = pgTtl; }

    $("#ttlPgs").val(startRow); //set total pages

    var locId = $("#s1").val();
    var mfgId = $("#s2").val();
    var gtpId = $("#s3").val();
    var calId = $("#s4").val();
    var dirId = $("#s5").val();
    var serch = $("#t1").val();
    var dateF = $("#t2").val();
    var dateT = $("#t3").val();

    var fd = new FormData();
    fd.append("LocId", locId);
    fd.append("MfgId", mfgId);
    fd.append("GtpId", gtpId);
    fd.append("CalId", calId);
    fd.append("BtpId", dirId);
    fd.append("Search", serch);
    fd.append("DateFr", dateF);
    fd.append("DateTo", dateT);
    fd.append("IPrPg", gunsPerPg);
    fd.append("StaRw", startRow);

    var beg = "";

    return $.ajax({
        cache: false,
        url: "/Compliance/FillCflc",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (b) {

            $("#cflcGrid").empty();
            $("#dvHdCa").hide();
            $("#dvHdWy").hide();

            var x = 1;
            var bgc = "palegoldenrod";
            var ct = b.length;
            
            if (ct > 0) {
                var trc = b[0].TotalRowCount;
                $("#ttlRowCt").val(trc);
                $(".prt-ico").show();
                $(".gun-count").show();
                $(".gun-count").text(trc + " Records Found");
                //$(".testTop").show();
                $(".noResults").hide();
            } else {
                $(".prt-ico").hide();
                $(".gun-count").hide();
                //$(".testTop").hide();
                $(".noResults").show();
            }

            $.each(b, function (i, item) {

                var loc = item.LocationId;
                var tid = item.TransId;
                var des = item.GunDesc;
                var aqn = item.AcqName;
                var aaf = item.AcqAddrOrFfl;
                var cfi = item.CflcInbound;
                var dpn = item.DispName;
                var daf = item.DispAddrOrFfl;
                var cot = item.CflcOutbound;
                var adt = item.StrDateAcq;
                var ddt = item.StrDateDsp;

                var an = aqn + "<br/>" + aaf;
                var dn = dpn + "<br/>" + daf;

                //var cd = cor ? createDate : "";
                var p = 1;
                var bb = "";

                bgc = bgc === "palegoldenrod" ? "lightyellow" : "palegoldenrod";
               

                if (tid != beg) {
                    beg = tid; //set hard line
                    p = 2;
                } else {
                    p = 1;
                }

 
                
                if (x === ct) {
                    bb = "border-bottom: solid 2px black;";
                }

                //var cl = loc === "1" ? $("#dvSt").addClass("grid-ca") : $("#dvSt").addClass("grid-wy");

                var cl = "";

                if (loc === 1) {
                    $("#dvHdCa").show();
                    $("#dvAdd").addClass("grid-ca");
                    cl = "grid-ca";
                } else {
                    $("#dvHdWy").show();
                    $("#dvAdd").addClass("grid-wy");
                    cl = "grid-wy";
                }


                var block = "<div class='" + cl + "' style=\"border-top:solid " + p + "px black; background-color: " + bgc + ";" + bb + "\">";
                block += "<div class='rowData grid-rt' style=\"border-left: solid 1px black\">" + tid + "</div>";
                block += "<div class='rowData grid-rt' style='border-right: solid 2px black'>" + des + "</div>";
                if (loc === 1) {
                    block += "<div class='rowData grid-rt' style='text-align:center !important'>" + adt + "</div>";
                    block += "<div class='rowData grid-rt'>" + an + "</div>";
                    block += "<div class='rowData grid-rt' style='border-right: solid 2px black; text-align:center !important; font-weight:bold; color:red'>" + cfi + "</div>";
                }
                block += "<div class='rowData grid-rt' style='text-align:center !important'>" + ddt + "</div>";
                block += "<div class='rowData grid-rt'>" + dn + "</div>";
                block += "<div class='rowData grid-rt' style='border-right: solid 2px black; text-align:center !important; font-weight:bold; color:red'>" + cot + "</div>";
                block += "</div>";

                x++;
                $("#cflcGrid").append(block);

            });

        },
        complete: function () {
            var c = $("#ttlRowCt").val();
            var ci = parseInt(c);
            
            if (ci > 0) {
                $("#divPage").show();
                setPaging("#pagerDt1", 2);
                showPgRange();
            }

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function setPageCount(count, p) {

    $("#gunsPerPg").val(count);
    $("#curPg").val(1);
    if (p === "1") { getInput(); } else { buildCflc(); }
}

function navToPg(sender, p) {

    if ($(sender).hasClass("active")) { return; }
    else {
        var item = $(sender).index();
        $("#curPg").val(item);
        if (p === "1") { getInput(); } else { buildCflc(); }
    }
}



function setBookPaging(pg, p) {

    $(pg).empty();

    var trc = $("#ttlRowCt").val();
    var gpp = $("#gunsPerPg").val();
    var cp = $("#curPg").val();

    var icp = parseInt(cp);
    var iGpp = parseInt(gpp);
    var iTrc = parseInt(trc);

    //set defaults
    if (icp === 0) { icp = 1; }
    if (iGpp == null) { iGpp = 25; } //default to 25
    var ttlPages = Math.ceil(iTrc / iGpp);

    $(pg + " a").removeClass("active").removeClass("nav-disable");
    $(pg).append("<a id='pp' href='#' onclick='navBookBack(this)'>&laquo;</a>");

    for (var z = 1; z < ttlPages + 1; z++) { $(pg).append("<a class='nav-bluff' href='#' onclick=\"navToPg(this, '"+p+"')\">" + z + "</a>"); }

    if (icp < 10) { $(pg + " a.nav-bluff:gt(9)").hide(); }
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

        $(pg + " a.nav-bluff:gt(0)").show();
        $(pg + " a.nav-bluff:lt(" + (l) + ")").hide();
        $(pg + " a.nav-bluff:gt(" + (u) + ")").hide();
    }

    $(pg + " a.nav-bluff:eq(" + (icp - 1) + ")").addClass("nav-disable").addClass("active");
    $(pg).append("<a id='np' href='#' onclick='navBookNext(this)'>&raquo;</a>");
}

function navBookBack(sender) {
    var id = $(sender).closest("div").prop("id");
    var item = $("#" + id + " a.active").index();

    if (item === 1) { return; } else {
        item--;
        $("#curPg").val(item);
        $("#" + id + " a").removeClass("active");

        /* SHOW PREV PAGE GROUP AT MAX TAB*/
        if (item % 10 === 0) {
            $("#" + id + " a.nav-bluff").show();
            $("#" + id + " a.nav-bluff:gt(" + (item - 1) + ")").hide();
            if (item > 10) { $(sender + " a.nav-bluff:lt(" + (item - 10) + ")").hide(); }
        }
        $("#" + id + " a.nav-bluff:eq(" + (item - 1) + ")").addClass("active");
        //buildCflc();
        getInput();
    }
}


function navBookNext(sender) {
    var id = $(sender).closest("div").prop("id");

    var item = $("#" + id + " a.active").index();
    var trc = parseInt($("#ttlRowCt").val());
    var gpp = parseInt($("#gunsPerPg").val());
    var ttlPages = Math.ceil(trc / gpp);

    if (item === ttlPages) { return; }
    else {

        item++;
        $("#curPg").val(item);
        //buildCflc();
        getInput();
    }
}


function showBookPgRange() {

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




function buildHiCaps() {

    var gunsPerPg = $("#gunsPerPg").val();
    var curPage = $("#curPg").val();
    var startRow = 1;

    if (gunsPerPg == null) { gunsPerPg = 100; } //default to 100
    var pgTtl = ((curPage * gunsPerPg) - gunsPerPg) + 1;
    if (curPage > 1) { startRow = pgTtl; }

    $("#ttlPgs").val(startRow); //set total pages


    var mfgId = $("#s1").val();
    var gtpId = $("#s2").val();
    var calId = $("#s3").val();
    var capId = $("#s4").val();
    var dateF = $("#t1").val();
    var dateT = $("#t2").val();

    var fd = new FormData();
    fd.append("MfgId", mfgId);
    fd.append("GtpId", gtpId);
    fd.append("CalId", calId);
    fd.append("CapId", capId);
    fd.append("DateFr", dateF);
    fd.append("DateTo", dateT);
    fd.append("IPrPg", gunsPerPg);
    fd.append("StaRw", startRow);

    var beg = "";

    return $.ajax({
        cache: false,
        url: "/Compliance/FillHiCaps",
        type: "POST",
        contentType: false,
        processData: false,
        data: fd,
        success: function (b) {

            $("#magGrid").empty(); 

            var x = 1;
            var bgc = "palegoldenrod";
            var ct = b.length;
            
            if (ct > 0) {
                var trc = b[0].TotalRowCount;
                $("#ttlRowCt").val(trc);
                $(".prt-ico").show();
                $(".gun-count").show();
                $(".gun-count").text(trc + " Records Found");
                $(".noResults").hide();
            } else {
                $(".prt-ico").hide();
                $(".gun-count").hide();
                $(".noResults").show();
            }

            $.each(b, function (i, item) {

                var id = item.Id;
                var cap = item.CapacityId;
                var tid = item.TransId;
                var des = item.GunDesc;
                var anm = item.AcqName;
                var aaf = item.AcqAddrOrFfl;
                var dnm = item.DispName;
                var daf = item.DispAddrOrFfl;
                var adt = item.StrDateAcq;
                var ddt = item.StrDateDsp;

                var an = anm + "<br/>" + aaf;
                var dn = dnm + "<br/>" + daf;

                var p = 1;
                var bb = "";

                bgc = bgc === "palegoldenrod" ? "lightyellow" : "palegoldenrod";
               

                if (tid != beg) {
                    beg = tid; //set hard line
                    p = 2;
                } else {
                    p = 1;
                }

                if (x === ct) {
                    bb = "border-bottom: solid 2px black;";
                }


                var block = "<div class='grid-mags' style=\"border-top:solid " + p + "px black; background-color: " + bgc + ";" + bb + "\">";
                block += "<div class='rowData grid-rt' style='text-align:center !important; border-left: solid 1px black'><a class='tLink' onclick=\"getMag('"+id+"')\">" + id + "</a></div>";
                block += "<div class='rowData grid-rt' style='text-align:center !important'>" + tid + "</div>";
                block += "<div class='rowData grid-rt' style='text-align:center !important'>" + cap + "</div>";
                block += "<div class='rowData grid-rt' style='border-right: solid 2px black'>" + des + "</div>";
                block += "<div class='rowData grid-rt' style='text-align:center !important'>" + adt + "</div>";
                block += "<div class='rowData grid-rt' style='border-right: solid 2px black; text-align:center !important'>" + an + "</div>";
                block += "<div class='rowData grid-rt' style='text-align:center !important'>" + ddt + "</div>";
                block += "<div class='rowData grid-rt' style='border-right: solid 2px black; text-align:center !important'>" + dn + "</div>";
                block += "<div class='rowData grid-rt grid-mags-del'><a class='tLink' onclick=\"nixMag('"+id+"')\">delete</a></div>";
                block += "</div>";

                x++;
                $("#magGrid").append(block);

            });

        },
        complete: function () {
            var c = $("#ttlRowCt").val();
            var ci = parseInt(c);
            
            if (ci > 0) {
                $("#divPage").show();
                setPaging("#pagerDt1", 2);
                showPgRange();
            }

        },
        error: function (err, data) {
            alert("Status : " + data.responseText);
        }
    });
}

function getMag(id) {
    
    $("#dvBtnEdit").show();
    $("#dvBtnAdd").hide();

    $.ajax({
        url: "/Compliance/GetHiCapById",
        data: "{ itemId: '" + id + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function(d) {
            
            var idp = d.IsDisposed ? "true" : "false";

            $("#s6").val(d.ManufId);
            $("#s6").selectpicker("refresh");
            $("#s7").val(d.CaliberId);
            $("#s7").selectpicker("refresh");
            $("#s8").val(d.GunTypeId);
            $("#s9").val(idp);
            $("#t4").val(d.CapacityId);
            $("#t5").val(d.GunModelName);
            $("#t6").val(d.StrDateAcq);
            $("#t7").val(d.AcqName);
            $("#t8").val(d.AcqAddrOrFfl);
            $("#t9").val(d.StrDateDsp);
            $("#t10").val(d.DispName);
            $("#t11").val(d.DispAddrOrFfl);

        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            var modal = document.getElementById("dvMag");
            modal.style.display = "block";
            $("#magId").val(id);
        }
    });

}


function editMag() {

    var magId = $("#magId").val();

    var mfgId = $("#s6").val();
    var calId = $("#s7").val();
    var gtpId = $("#s8").val();
    var isDsp = $("#s9").val();
    var capId = $("#t4").val();
    var model = $("#t5").val();
    var aqDat = $("#t6").val();
    var aqNam = $("#t7").val();
    var aqAdf = $("#t8").val();
    var dpDat = $("#t9").val();
    var dpNam = $("#t10").val();
    var dpAdf = $("#t11").val();

    var fd = new FormData();
    fd.append("ItmId", magId);
    fd.append("MfgId", mfgId);
    fd.append("CalId", calId);
    fd.append("GtpId", gtpId);
    fd.append("IsDsp", isDsp);
    fd.append("CapId", capId);
    fd.append("Model", model);
    fd.append("AcqDt", aqDat);
    fd.append("AcqNm", aqNam);
    fd.append("AcqAf", aqAdf);
    fd.append("DspDt", dpDat);
    fd.append("DspNm", dpNam);
    fd.append("DspAf", dpAdf);

    $.ajax({
        cache: false,
        url: "/Compliance/EditHiCap",
        type: "POST",
        contentType: false, 
        processData: false,  
        data: fd,
        success: function(b) {
            
        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            closeDiv("dvMag");
            buildHiCaps();
        }
    });
}


function addMag() {

    var mfgId = $("#s6").val();
    var calId = $("#s7").val();
    var gtpId = $("#s8").val();
    var isDsp = $("#s9").val();
    var capId = $("#t4").val();
    var model = $("#t5").val();
    var aqDat = $("#t6").val();
    var aqNam = $("#t7").val();
    var aqAdf = $("#t8").val();
    var dpDat = $("#t9").val();
    var dpNam = $("#t10").val();
    var dpAdf = $("#t11").val();

    var fd = new FormData();
    fd.append("MfgId", mfgId);
    fd.append("CalId", calId);
    fd.append("GtpId", gtpId);
    fd.append("IsDsp", isDsp);
    fd.append("CapId", capId);
    fd.append("Model", model);
    fd.append("AcqDt", aqDat);
    fd.append("AcqNm", aqNam);
    fd.append("AcqAf", aqAdf);
    fd.append("DspDt", dpDat);
    fd.append("DspNm", dpNam);
    fd.append("DspAf", dpAdf);

    $.ajax({
        cache: false,
        url: "/Compliance/AddHiCap",
        type: "POST",
        contentType: false, 
        processData: false,  
        data: fd,
        success: function(b) {
            
        },
        error: function (err, result) {
            alert(err);
        },
        complete: function () {
            closeDiv("dvMag");
            buildHiCaps();
        }
    });
}


function openModal() {

    $("#dvBtnAdd").show();
    $("#dvBtnEdit").hide();

    $("#form-mags")[0].reset();
    $("#s6").selectpicker("refresh");
    $("#s7").selectpicker("refresh");
    var modal = document.getElementById("dvMag");
    modal.style.display = "block";    
}

function nixMag(id) {
    
    Lobibox.confirm({
        title: "Delete Magazine " + id + " ?",
        msg: "Making changes to this bound book item are permanent and cannot be undone. Do you wish to continue?",
        modal: true,
        callback: function(lobibox, type) {
            if (type === 'no') {
                return;
            } else {
                $.ajax({
                    url: "/Compliance/DeleteMag",
                    data: "{ itemId: '" + id + "'}",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(d) {
           
                    },
                    error: function (err, result) {
                        alert(err);
                    },
                    complete: function () {
                        closeDiv("dvMag");
                        buildHiCaps();
                    }
                });
            }
        }
    });
}