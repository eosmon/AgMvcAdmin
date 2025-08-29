function routeAd(v) {

    var id = $("#xFtrId" + v).val();

    if (id.length === 0) {
        var fid = "xFtrId" + v;
        document.getElementById(fid).focus();
        Lobibox.alert('error',
            {
                title: 'No Feature ID Found',
                msg: 'A valid Feature ID must be entered for this row to continue'
            });
    } else {

        var fileData = new FormData();
        fileData.append('Id', id);

        $.ajax({
            cache: false,
            url: '/Content/GetAdById',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (data) {

                $("#dvSmall").empty();
                $("#dvTall").empty();
                $("#dvBig").empty();
                

                if (data === null) {
                    //handle it
                } else {
                    var sz = data.AdSizeId;

                    switch(sz) {
                        case 1:
                            $("#dvSmall").append(adSmall(data));
                            break;
                        case 2:
                            $("#dvTall").append(adTall(data));
                            break;
                        case 3:
                            $("#dvBig").append(adBig(data));
                            break;
                    }
                }

                /* 1. See if data was returned */
                /* 2. Check the size */
                /* 3. Route the data   */
            },
            complete: function() {
 
            },
            error: function (err, data) {
                alert("Status : " + data.responseText);
            }
        });
    }

}


function adSmall(d) {

    var iImg = d.ItemImageUrl;
    var mImg = d.MfgImageUrl;
    var mBgc = d.MfgBgColor;
    var sale = d.OnSale;
    var isPr = d.IsPromo;
    var caOk = d.CaOkay;
    var save = "";
    var show = "none";
    var fsz = "17px";

    if (sale) {
        var sv = d.Savings;
        save = "Save $" + numberWithCommas(sv) + "!";
    }

    if (isPr) { show = "block"; }

    var pTxt = d.PromoTxt;
    var pBgc = d.PromoBgColor;
    var pTtl = d.HeaderTitle.length < 12 ? d.ManufName + " " + d.HeaderTitle : d.HeaderTitle;

    if (pTtl.length > 35) { fsz = "15px"; }
    if (pTtl.length > 42) { fsz = "14px"; }
    if (pTtl.length > 49) { fsz = "13px"; }


    var pc = parseInt(d.Price);
    var prc = "$" + numberWithCommas(pc) + ". ";

    var px = d.Price.toFixed(2);
    var ct = px.toString();
    var cts = ct.split(".")[1];
    var mpn = d.MfgPartNumber;

    var uts = d.Units < 6 ? "Only " + d.Units + " Left!" : "";
    var caTxt = "";

 

    if (caOk) {
        caTxt = d.CaText.length > 0 ? "** " + d.CaText + " **" : "";
    } else {
        caTxt = "** NO CA Public Sales - U.S./LEO Sales Only";
    }

        var b = "<div class='adSm'>";
        b += "<a href='" + d.NavUrl + "' style='text-decoration:none; color: white' title='VIEW OR BUY THIS ITEM'>";
        b += "<div class='adSm-body' style=\"background-image: url('" + iImg + "')\">";
        b += "<div class='adSm-header' style='background:" + mBgc + "'>";
        b += "<div class='adSm-header-align'><img src='" + mImg + "' class='adSm-header-img' /></div>";
        b += "<div class='adSm-savings'>" + save + "</div>";
        b += "</div>";
        b += "<div style='height:135px'>";
        b += "<div id='dvSmPro' class='adSm-promo-bar' style='background:" + pBgc + ";display:" + show + "'>" + pTxt + "</div>";
        b+= "<div>";
        b+= "<div class='adSm-bodyTxt'>";
        b += "<div class='adSm-model' style='font-size:" + fsz + "'>" + pTtl + "</div>";
        b+= "<div style='text-align: right; padding: 5px'>";
        b += "<div class='adSm-itemNum'>Item #: <span style='color: blue'>" + numberWithCommas(d.MasterId) + "</span></div>";
        b+= "<div class='adSm-descBody'>";
        b += "<div class='adSm-desc'>" + d.ShortDesc + "</div>";
        b+= "</div>";
        b += "<div class='adSm-price'>" + prc + "<span class='adSm-price-cents'>" + cts + "</span></div>";
        b += "<div class='adSm-units'>" + uts + "</div>";
        b+= "</div>";
        b+= "</div>";
        b += "</div>";
        b += "</div>";
        b+= "<div class='adSm-bottom'>";
        b += "<div style='float:left; padding-left:7px' id='dvCaMsg'>" + caTxt + "</div>";
        b += "<div style='float: right; padding-right: 9px'>MFG# " + mpn + "</div>";
        b+= "</div>";
        b += "</div>";
        b += "</a>";
        b+= "</div>";

    return b;

 
}

function adTall(d) {

    var id = numberWithCommas(d.MasterId);
    var iCat = d.CategoryId;
    var tDsc = d.TallDesc;
    var iImg = d.ItemImageUrl;
    var mImg = d.MfgImageUrl;
    var mBgc = d.MfgBgColor;
    var sale = d.OnSale;
    var caOk = d.CaOkay;
    var caPt = d.CaPptOk;
    var isPr = d.IsPromo;
    var save = "";
    var show = "none";

    if (sale) {
        var sv = d.Savings;
        save = "Save $" + numberWithCommas(sv) + "!";
    }

    if (isPr) { show = "block"; }

    var pTxt = d.PromoTxt;
    var pBgc = d.PromoBgColor;

    var pc = parseInt(d.Price);
    var prc = "$" + numberWithCommas(pc) + ". ";

    var px = d.Price.toFixed(2);
    var ct = px.toString();
    var cts = ct.split(".")[1];
 
    var caTxt = "";
    var caMsg = "";

    if (!caOk) {
        caTxt = "U.S./LEO SALES ONLY **";
        caMsg = "** No California Public Sales. Must be shipped to a licensed FFL.";
    } else {

        caTxt = "WHILE SUPPLIES LAST"; //default messages
        caMsg = "Local CA Customers - purchase here in Orange County or have it shipped";

        /* GUNS */
        if (iCat === 100) {
            caTxt = d.CaText.length > 0 ? d.CaText + " **" : caTxt;
            if (caPt) {
                caMsg = "** Transfer must be completed here in Orange County. $35.00 PPT Fee Required";
            } else {
                caMsg = "** Local CA Customers - Transfer FREE here in Orange County or ship to your FFL. CA Sales Tax & DROS Apply";
            }
        }
        if (iCat === 200) {
            caMsg = "Local CA Customers - Transfer FREE here in Orange County or ship to your FFL. CA Sales Tax & Ammo DROS Applies";
        }
    }


    var b = "<div class='adTall'>";
        b += "<a href='" + d.NavUrl + "' style='text-decoration:none; color: white' title='VIEW OR BUY THIS ITEM'>";
        b += "<div class='adTall-header' style='background:" + mBgc + "'>";
        b += "<div><img src='" + mImg + "' class='adSm-header-img' /></div>";
        b += "</div>";
        b += "<div class='adTall-body'>";
        b += "<div id='dvTallPro' class='adSm-promo-bar ' style='background:" + pBgc + ";display:" + show + "'>" + pTxt + "</div>";
        b += "<div class='adTall-savings'>" + save + "</div>";
        b += "<div class='adTall-id'>Item # <span style='color: blue'>"+id+"</span></div>";
        b += "<div style='text-align: center'><img src='"+iImg+"' style='width: 160px; height: auto' /></div>";
        b += "<div class='adTall-desc'><b>"+d.ManufName+"</b> "+tDsc+"</div>";
        b += "<div class='adTall-smTxt' style='margin-bottom:-3px !important'>" + caTxt + "</div>";
        b += "<div class='adTall-price'>"+prc+"<span class='adSm-price-cents'>"+cts+"</span></div>";
        b += "<div class='adTall-smTxt adTall-padBtm'>" + caMsg + "</div>";
        b += "</div>";
        b += "</a>";
        b += "</div>";

    return b;


}


function adBig(d) {
    
    var iImg = d.ItemImageUrl;
    var mImg = d.MfgImageUrl;
    var mBgc = d.MfgBgColor;
    var sale = d.OnSale;
    var isPr = d.IsPromo;
    var caOk = d.CaOkay;
    var save = "";
    var show = "none";
    var fsz = "22px";
    var caTxt = "";
    var cndId = d.ConditionId;
    var cndUts = "";

    if (cndId === 1) { //NEW 
        cndUts = d.Units < 6 ? "Only " + d.Units + " Left!" : "Brand New";
    } else {
        cndUts = d.ConditionId > 1 ? d.ItemCond + " Condition " : d.ItemCond;
    }

    if (caOk) {
        caTxt = d.CaText.length > 0 ? "** " + d.CaText + " **" : "";
    } else {
        caTxt = "** NO CA PUBLIC SALES **";
    }

    if (sale) {
        var sv = d.Savings;
        save = "Save $" + numberWithCommas(sv) + "!";
    }

    if (isPr) { show = "block"; }

    var pTxt = d.PromoTxt;
    var pBgc = d.PromoBgColor;
    var pTtl = d.HeaderTitle.length < 12 ? d.ManufName + " " + d.HeaderTitle : d.HeaderTitle;

    if (pTtl.length > 38) { fsz = "20px"; }
    if (pTtl.length > 42) { fsz = "18px"; }
    if (pTtl.length > 45) { fsz = "17px"; }
    if (pTtl.length > 49) { fsz = "15px"; }

    var pc = parseInt(d.Price);
    var prc = "$" + numberWithCommas(pc) + ". ";

    var px = d.Price.toFixed(2);
    var ct = px.toString();
    var cts = ct.split(".")[1];

    var b = "<div class='adBig' >";
    b += "<a href='" + d.NavUrl + "' style='text-decoration:none; color: white' title='VIEW OR BUY THIS ITEM'>";
    b += "<div class='adBig-body' style=\"background-image: url('" + iImg + "')\">";
    b += "<div class='adSm-header' style='background:" + mBgc + "'>";
    b += "<div style='width:200px; display:inline-block'></div>";
    b += "<div style='width:180px; display:inline-block; text-align: center'><img src='"+mImg+"' class='adSm-header-img' /></div>";
    b += "<div style='width:193px; display:inline-block; text-align:right' class='adBig-savings'>"+save+"</div>";
    b += "</div>";
    b += "<div style='height:100px'>";
    b += "<div id='dvBigPro' class='adSm-promo-bar' style='background:" + pBgc + ";display:" + show + "'>" + pTxt + "</div>";
    b += "<div class='adBig-left'>";
    b += "<div class='adBig-model' style='font-size:" + fsz + "'>" + pTtl + "</div>";
    b += "<div class='adBig-desc'>" + d.BigDesc + "</div>";
    b += "</div>";
    b += "</div>";
    b += "<div class='adBig-content'>";
    b += "<div style='width:53%; display:inline-block'>";
    b += "<div class='adBig-txt'>" + caTxt + "</div>";
    b += "<div class='adBig-cond'>" + cndUts + "</div>";
    b += "<div class='adBig-txt' style='padding-left:11px; padding-top:3px; font-style: italic'>ITEM # <span style='color: blue'>" + numberWithCommas(d.MasterId) + "</span></div>";
    b += "</div>";
    b += "<div class='adBig-priceRow'>";
    b += "<div class='adBig-price'>" + prc + "<span class='adBig-price-cents'>" + cts + "</span></div>";
    b += "</div>";
    b += "</div>";
    b += "</div>";
    b += "</a>";
    b += "</div>";

    return b;
}