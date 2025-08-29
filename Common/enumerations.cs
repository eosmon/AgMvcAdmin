using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Common
{

    public enum CustomerTypes
    {
        Unknown = 0,
        CommercialFFL = 1,
        CurioRelic = 2,
        PrivateParty = 3,
        LawEnforcement = 4,
        OtherBiz = 5,
    }

    public enum AcquisitionSources
    {
        Unknown = 0,
        CommercialFfl = 1,
        CurioFfl = 2,
        PrivateParty = 3,
        Police = 4,
        OtherOrg = 5,
        OwnersColl = 6
    }


    public enum Pages
    {
        Unknown = 0,
        InventoryGuns = 1,
        InventoryAmmo = 2,
        InventoryMerchandise = 3,
        BoundBook = 4,
        OrdersCreate = 5,
        OrdersExisting = 6,
        OrdersCompleted = 7,
        DataFeedGuns = 8,
        DataFeedDuplicates = 9,
        DataFeedImages = 10,
        DataFeedManuf = 11,
        ContentManager = 12,
        ContentCampaigns = 13,
        SalesCampaigns = 14,
        ContentHomeScroll = 15,
        CustomerAdd = 16,
        Cflc = 17,
        CaHiCap = 18,
        OrdersTest = 19,
    }

    public enum SiteImageSections
    {
        Unknown = 0,
        Banner = 1,
        Header = 2,
        HomeScroll = 3,
        Guns = 4,
        Ammunition = 5,
        Merchandise = 6
    }

    public enum ConfigurationNames
    {
        Application = 1,
        Crypto = 2,
        Exception = 3
    }

    public enum ActiveConnections
    {
        LocalSql = 1,
        ProdSql = 2
    }

    public enum CryptoProperties
    {
        SecurityKey = 1,
        SecurityVector = 2,
        SecurityProvider = 3
    }

    public enum ConnectionNames
    {
        AdminSql = 1,
        ComplSql = 2,
        EcsmsSql = 3,
        BotDbSql = 4,
        GunDbSql = 5,
        WebDbSql = 6
    }

    public enum HostUrl
    {
        Unknown = 0,
        Local = 1,
        Beta = 2,
        Production = 3,
        LocalWeb = 4
    }

    public enum PhotoUpload
    {
        Svc1 = 1,
        Svc2 = 2,
        Svc3 = 3,
        Svc4 = 4,
        Svc5 = 5,
        Svc6 = 6,
        Svc7 = 7,
        Svc8 = 8,
        Svc9 = 9,
        Svc10 = 10
    }

    public enum MissingSpecs
    {
        Unknown = 0,
        All = 1,
        GunType = 2,
        Caliber = 3,
        Capacity = 4,
        Action = 5,
        Finish = 6,
        Model = 7,
        Description = 8,
        LongDesc = 9,
        Barrel = 10,
        Overall = 11,
        Weight = 12
    }

    public enum DupMenus
    {
        Unknown = 0,
        Mfg = 1,
        Gtp = 2,
        Cal = 3
    }

    public enum ImgMenus
    {
        Unknown = 0,
        Mfg = 1,
        Gtp = 2,
        Cal = 3
    }

    public enum ItemCategories
    {
        Unknown = 0,
        Guns = 100,
        Ammo = 200,
        Merchandise = 300,
        Generic = 400
    }

    public enum DocTypes
    {
        AlienI9 = 1,
        AlienPermRes = 2,
        BirthCertForgn = 3,
        BirthCertUs = 4,
        CertOfElig = 5,
        CaFscCard = 6,
        CaFscInstr = 7,
        CaDojHiCap = 8,
        SpecWeaPermt = 9,
        ConcWpnPermit = 10,
        DmvChgOfAddr = 11,
        DriversLicense = 12,
        FflCurioRelic = 13,
        FflCommercial = 14,
        ExpFirearmsCard = 15,
        HuntLicense = 16,
        LeoId = 17,
        MilitaryId = 18,
        PassportForgn = 19,
        PassportUs = 20,
        PostCert = 21,
        PropertyDeed = 22,
        PropertyLease = 23,
        ResalePermit = 24,
        StateId = 25,
        DmvRegistration = 26

        //OtherGovtDoc = 11,
        //UtilBillMuni = 16,
        //CflcLetter = 18,
        //GunLockReceipt = 23,
        //CaGunSafeAffid = 24,
        //LeoAmmoLetter = 26,
        //PptSellerId = 27,
        //SafeHandlAff = 29,
        //UtilityBill = 31,
        //AtfMultiPistol = 34,
        //AtfMultiRifle = 35,
        //LeoDutyLetter = 37,

    }

    public enum DistCodes
    {
        UNK = 0,
        SSI = 1,
        CSS = 2,
        LIP = 3,
        MGE = 4,
        DAV = 5,
        RSR = 6,
        AMR = 7,
        BHC = 8,
        ZAN = 9,
        KRL = 10,
        HSE = 11,
        WYO = 12

    }

    public enum FedExPkgTypes
    {
        Unknown = 0,
        FEDEX_SMALL_BOX = 1,
        FEDEX_MEDIUM_BOX = 2,
        FEDEX_LARGE_BOX = 3,
        FEDEX_EXTRA_LARGE_BOX = 4,
        FEDEX_PAK = 5,
        FEDEX_TUBE = 6,
        FEDEX_ENVELOPE = 7,
        YOUR_PACKAGING = 8
    }

    public enum ShipTimes
    {
        Unknown = 0,
        Overnight = 1,
        TwoDayAir = 2,
        ExpressSaver = 3,
        Ground = 4,
        Pickup = 5
    }

    public enum ShipQuoteTypes
    {
        Unknown = 0,
        Items = 1,
        Ammo = 2,
        OneRate = 3
    }

    public enum FreightCarriers
    {
        Unknown = 0,
        FedEx = 1
    }

    /* 12/22 TRANS TYPES UPDATED TO COINCIDE WITH PIC FOLDERS */
    public enum PicFolders
    {
        Unknown = 0,
        Sales = 101,
        Consignment = 102,
        Transfers = 103,
        Shipping = 104,
        Storage = 105,
        Repair = 106,
        Acquisition = 107,
        Transport = 108,
        Recovery = 109,
        Spider = 149,
        InStock = 150,
        Custom = 151,
        Generic = 200
    }


    public enum GunRecoveryTypes
    {
        Unknown = 0,
        STORAGE = 1,
        CONSIGNMENT = 2,
        LIQUIDATION = 3,
        SHIPPING = 4,
        TRANSFER = 5
    }


    public enum OrderFees
    {
        Unknown = 0,
        CA_DROS = 1,
        CANCEL_ORDER = 2,
        CA_DROS_RETURN = 3,
        CA_AMMO_DROS = 4,
        CA_FSC_EXAM = 5,
        SHIPPING = 11,
        PICKUP = 12,
        DELIVERY = 13,
        DOC_PROCESSING = 15,
        PARTS_LABOR = 16,
        RESTOCKING = 17,
        APPRAISAL = 18,
        TRADE_CREDIT = 19,
        GUN_LOCK = 20,
        TRAVEL = 21,
        OTHER = 22
    }


    public enum TransTypes
    {
        Unknown = 0,
        SALE = 101,
        CONSIGNMENT = 102,
        TRANSFER = 103,
        SHIPPING = 104,
        STORAGE = 105,
        REPAIR = 106,
        ACQUISITION = 107,
        TRANSPORT = 108,
        RECOVERY = 109,
        GENERIC = 5

    }

    public enum FulfillmentTypes
    {
        Unknown = 0,
        FaceToFace = 1,
        Shipping = 2,
        Delivery = 3,
        Pickup  = 4,
        PickupDelivery = 5,
        CustomerToShip = 6
    }



}