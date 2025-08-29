using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models.Common;
using WebBase.Configuration;

namespace AgMvcAdmin.Models.Menus
{
    public class MenuModel : BaseModel
    {
        public int ManufId { get; set; }
        public int CaliberId { get; set; }
        public int ActionId { get; set; }
        public ContentMenu Categories { get; set; }
        public ContentMenu ContentPgs { get; set; }
        public ContentMenu OgTypes { get; set; }
        public MerchandiseMenu OtherCategories { get; set; }
        public MerchandiseMenu OtherSubCategories { get; set; }
        public BulletTypeMenu Bullets { get; set; }
        public GunManufMenu ManufOtherStuff { get; set; }
        public GunManufMenu ManufAmmo { get; set; }
        public GunManufMenu ManufAll { get; set; }
        public GunManufMenu ManufWeb { get; set; }
        public GunManufMenu Importers { get; set; }
        public GunManufMenu InStockMfg { get; set; }
        public MerchandiseMenu MerchOtherStuff { get; set; }
        public DistMenu FeedMfgDistCt { get; set; }
        public DistMenu FeedMfgDist { get; set; }
        public DistMenu AcqDist { get; set; }
        public DistMenu FflCodes { get; set; }
        public List<GunNavModel> FeedManuf { get; set; }
        public List<GunNavModel> FeedGunType { get; set; }
        public List<GunNavModel> FeedCaliber { get; set; }
        public List<GunNavModel> FeedAction { get; set; }
        public List<GunNavModel> FeedDaysPast { get; set; }
        public List<ContentModel> ToolTips { get; set; }
        public ColorMenu ItemColors { get; set; }
        public FscMenu CaFsc { get; set; }
        public FulfillmentMenu Fulfillment { get; set; }
        public GunLockMenu GunLockManuf { get; set; }
        public GunTypeMenu GunType { get; set; }
        public GunCaliberMenu AmmoCaliber { get; set; }
        public GunCaliberMenu WebCaliber { get; set; }
        public GunCaliberMenu AllCaliber { get; set; }
        public GunCaliberMenu InStockCal { get; set; }
        public GunActionMenu Action { get; set; }
        public GunConditionMenu Condition { get; set; }
        public GunFinishMenu Finish { get; set; }
        public GunSellerTypeMenu SellerType { get; set; }
        public BarrelInModel BrlInModel { get; set; }
        public BarrelDecModel BrlDecModel { get; set; }
        public ChamberInModel ChbInModel { get; set; }
        public ChamberDecModel ChbDecModel { get; set; }
        public OverallInModel OvlInModel { get; set; }
        public OverallDecModel OvlDecModel { get; set; }
        public WeightLbModel WeightLb { get; set; }
        public WeightOzModel WeightOz { get; set; }
        public UsStatesMenu UsStateNames { get; set; }
        public UsStatesMenu UsStateAbrv { get; set; }
        public CustAddressMenu CustAddr { get; set; }
        public TransTypesMenu TransTyp { get; set; }
        public CustomerTypesMenu CustTypes { get; set; }
        public SubCategoryMenu ItemSubCategories { get; set;  }
        public ShippingMenu Shipping { get; set; }
        public DocTypesMenu Docs { get; set; }
        public DocCategoriesMenu DocCats { get; set; }
        public DocTypesMenu DocTypes { get; set; }
        public SecurityQuestionModel SecQuest { get; set; }
        public List<SelectListItem> Industry { get; set; }
        public List<SelectListItem> RecoveryObjectives { get; set; }
        public List<SelectListItem> PaymentMethods { get; set; }

        public DayModel DdModel { get; set; }
        public MonthModel MoModel { get; set; }
        public YearModel YrModel { get; set; }
        public YearModel YrExpModel { get; set; }


        public AddCustomerMenu AddCustomer { get; set; }
        public ProfessionsMenu CustProfession { get; set; }
        public ReferralModel RefModel { get; set; }

        private List<SelectListItem> GetRecoveryObjectives()
        {
            return GetSelectMenu("ProcMenuRecoveryObjectives", "ID", "Objective", true);
        }

        private List<SelectListItem> GetCustomerIndusty()
        {
            return GetSelectMenu("ProcMenuCustomerIndustry", "ID", "IndustryName", true);
        }

        private List<SelectListItem> GetBulletTypes()
        {
            return GetSelectMenu("ProcMenuBulletTypes", "ID", "Bullet", false);
        }

        //private List<SelectListItem> GetAmmoUserTypes()
        //{
        //    return GetSelectMenu("ProcMenuAmmoUserTypes", "ID", "UserType", false);
        //}

        private List<SelectListItem> GetFflCodes()
        {
            return GetSelectMenu("ProcMenuFFLCodes", "ID", "FFLCode", false);
        }

        private List<SelectListItem> GetGunLockManufacturers()
        {
            return GetSelectMenu("ProcMenuGunLockMakers", "ID", "LockManuf", false);
        }

        public List<SelectListItem> GetGunLockModels(int id)
        {
            return GetSelectMenu("ProcMenuGunLockModels", "ID", "LockModel", id);
        }


        private List<SelectListItem> GetManufacturersAll()
        {
            return GetSelectMenu("ProcMenuManufAll", "ManufID", "ManufName", false);
        }

        private List<SelectListItem> GetMerchandiseAll()
        {
            return GetSelectMenu("ProcMenuMerchandise", "ID", "CategoryName", false);
        }

        private List<SelectListItem> GetManufacturersWeb()
        {
            return GetSelectMenu("ProcMenuManufWeb", "ManufID", "ManufName", false);
        }

        //private List<SelectListItem> GetManufacturersInStockGuns()
        //{
        //    return GetSelectMenu("ProcMenuManufInStockGuns", "ManufID", "ManufName", false);
        //}

        private List<SelectListItem> GetManufacturersInStockAmmo()
        {
            return GetSelectMenu("ProcMenuManufInStockAmmo", "ManufID", "ManufName", false);
        }

        private List<SelectListItem> GetManufacturersInStockMerch()
        {
            return GetSelectMenu("ProcMenuManufInStockMerch", "ManufID", "ManufName", false);
        }

        private List<SelectListItem> GetCalibersInStockAmmo()
        {
            return GetSelectMenu("ProcMenuCaliberInStockAmmo", "ID", "CaliberName", false);
        }

        
        private List<SelectListItem> GetManufacturersAmmo()
        {
            return GetSelectMenu("ProcMenuManufAmmo", "ID", "ManufName", false);
        }

        private List<SelectListItem> GetManufacturersOtherStuff()
        {
            return GetSelectMenu("ProcMenuManufOtherStuff", "ID", "ManufName", false);
        }

        private List<SelectListItem> GetImporters()
        {
            return GetSelectMenu("ProcMenuImporters", "ManufID", "ManufName", false);
        }
        
        private List<SelectListItem> GetGunTypes()
        {
            return GetSelectMenu("ProcMenuGetGunTypes", "ID", "GunType", false);
        }

        private List<SelectListItem> GetWebCalibers()
        {
            return GetSelectMenu("ProcMenuCalibersWeb", "ID", "CaliberName", true);
        }

        private List<SelectListItem> GetAllCalibers()
        {
            return GetSelectMenu("ProcMenuCalibersAll", "ID", "CaliberName", false);
        }

        private List<SelectListItem> GetColors()
        {
            return GetSelectMenu("ProcMenuGetColors", "ID", "ColorName", false);
        }

        private List<SelectListItem> GetCaFscTypes()
        {
            return GetSelectMenu("ProcMenuCaFsc", "ID", "FscType", false);
        }

        private List<SelectListItem> GetFulfillmentTypes()
        {
            return GetSelectMenu("ProcMenuFulfillmentTypes", "ID", "FulfillType", false);
        }


        private List<SelectListItem> GetGunActions()
        {
            return GetSelectMenu("ProcMenuGetGunActions", "ID", "ActionName", false);
        }

        private List<SelectListItem> GetGunConditions()
        {
            return GetSelectMenu("ProcMenuGetGunConditions", "ID", "ConditionName", false);
        }

        private List<SelectListItem> GetGunFinishes()
        {
            return GetSelectMenu("ProcMenuGetGunFinishes", "ID", "FinishName", false);
        }


        private List<SelectListItem> GetGunSellers()
        {
            return GetSelectMenu("ProcMenuGetGunSellers", "ID", "SellerName", true);
        }

        private List<SelectListItem> GetAcqDistributors() //MAY BE REDUNDENT AFTER REFACTORING METHOD BELOW
        {
            return GetSelectMenu("ProcMenuAcqDistributors", "ID", "Distributor", true);
        }

        private List<SelectListItem> GetFflSuppliers()
        {
            return GetSelectMenu("ProcMenuFflSuppliers", "ID", "Distributor", true);
        }


        private List<SelectListItem> GetAbvStates()
        {
            return GetSelectMenu("ProcMenuUsStates", "StateID", "StateAbbrev", false);
        }

        private List<SelectListItem> GetUsStateNames()
        {
            return GetSelectMenu("ProcMenuGetUsStatesByName", "StateID", "StateName", false);
        }

        private List<SelectListItem> GetCustAddresses(int id)
        {
            return GetSelectMenu("ProcCustomerGetAllAddresses", "ID", "CustAddress", false);
        }


        public List<SelectListItem> GetWarehouse(int id)
        {
            return GetSelectMenu("ProcMenuGetDistWarehouses", "FFLID", "Warehouse", id);
        }

        public List<SelectListItem> GetSvcInqHistory()
        {
            return GetSelectMenu("ProcMenuGetSvcInquiries", "ID", "Desc", false);
        }

        public List<SelectListItem> GetTransTypes()
        {
            return GetSelectMenu("ProcMenuGetTransTypes", "ID", "TransType", false);
        }

        private List<SelectListItem> GetOtherCategories()
        {
            return GetSelectMenu("ProcMenuMerchandiseCategories", "ID", "CategoryName", false);
        }

        public List<SelectListItem> GetSubCategories(int id)
        {
            return GetSelectMenu("ProcMenuMerchandiseSubCategories", "ID", "SubCatName", id);
        }

        public List<SelectListItem> GetSubCatByGroup(int id)
        {
            return GetSelectMenu("ProcMenuSubCatByGroup", "ID", "PageName", id);
        }
        

        public MenuCollection GetManufByCategory(int id)
        {
            return GetMenuCollection("ProcMenuManufByGroup", "ID", "Name", id);
        }

        public List<SelectListItem> GetAllCustomers()
        {
            return GetSelectMenu("ProcAddCustomerMenu", "ID", "Customer", false);
        }

        private List<SelectListItem> GetReferralSources()
        {
            return GetSelectMenu("ProcMenuReferralTypes", "ID", "ReferralType", true);
        }

        private List<SelectListItem> GetCustomerTypes()
        {
            return GetSelectMenu("ProcMenuGetCustomerTypes", "ID", "CustomerType", true);
        }

        private List<SelectListItem> GetDistributors()
        {
            return GetSelectMenu(GunDbSqlConnection, "ProcEditManufDistibutors", "ID", "DistCode");
        }

        private List<SelectListItem> GetShippingSizes()
        {
            return GetSelectMenu(AdminSqlConnection, "ProcMenuShippingSizes", "ID", "Desc");
        }

        private List<SelectListItem> GetDistributorsCount()
        {
            return GetSelectMenu(GunDbSqlConnection, "ProcEditManufDistibutors", "ID", "DistCode", "ItemCount");
        }

        private List<SelectListItem> GetContentSections()
        {
            return GetSelectMenu(WebSqlConnection, "ProcContentMenuCategories", "ID", "CategoryName");
        }

        public List<SelectListItem> GetContentPages(int id)
        {
            return GetSelectMenu(WebSqlConnection, "ProcContentMenuPages", "ID", "PageName", id);
        }

        public List<SelectListItem> GetHomeAdOptions()
        {
            return GetSelectMenu(WebSqlConnection, "ProcContentMenuHomeOptions", "ID", "OptionName");
        }

        public List<SelectListItem> GetHomeBgColors()
        {
            return GetSelectMenu(WebSqlConnection, "ProcContentMenuHomeBgColors", "ID", "ColorName");
        }

        public List<SelectListItem> GetHomeFtrSizes()
        {
            return GetSelectMenu(WebSqlConnection, "ProcContentMenuHomeFtrSizes", "ID", "SizeName");
        }

        public List<SelectListItem> GetPaymentMethods()
        {
            return GetSelectMenu(AdminSqlConnection, "ProcMenuPaymentMethods", "ID", "PaymentMethod");
        }


        public List<SelectListItem> GetDocCategories()
        {
            return GetSelectMenu(AdminSqlConnection, "ProcMenuDocCategories", "ID", "DocGroup");
        }

        public List<SelectListItem> GetOgTypes(int id)
        {
            return GetSelectMenu(WebSqlConnection, "ProcContentMenuOgType", "ID", "SeoOgType", id);
        }

        private List<SelectListItem> GetItemSubCategories(int id)
        {
            return GetSelectMenu(AdminSqlConnection, "ProcMenuSubCategories", "ID", "SubCategoryName", id);
        }

        public List<SelectListItem> GetProfessions(int id)
        {
            return GetSelectMenu(AdminSqlConnection, "ProcMenuCustomerProfession", "ID", "ProfessionName", id);
        }

        public List<SelectListItem> GetDocTypes(int id)
        {
            return GetSelectMenu(AdminSqlConnection, "ProcMenuDocsByCategory", "ID", "DocType", id);
        }

        private List<SelectListItem> GetSecurityQuestions()
        {
            return GetSelectMenu(WebSqlConnection, "ProcMenuSecurityQuestions", "ID", "Question");
        }

        public List<SelectListItem> GetSalesRepsByLocation(int id)
        {
            return GetSelectMenu("ProcSalesRepsByLocation", "ID", "SalesRep", id);
        }



        private List<SqlParameter> SetBaseMenuParams(GunModel g)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@IsGunType", SqlDbType.Bit) { Value = g.Filters.IsMissingGunType });
            parameters.Add(new SqlParameter("@IsCaliber", SqlDbType.Bit) { Value = g.Filters.IsMissingCaliber });
            parameters.Add(new SqlParameter("@IsCapacity", SqlDbType.Bit) { Value = g.Filters.IsMissingCapacity });
            parameters.Add(new SqlParameter("@IsAction", SqlDbType.Bit) { Value = g.Filters.IsMissingAction });
            parameters.Add(new SqlParameter("@IsFinish", SqlDbType.Bit) { Value = g.Filters.IsMissingFinish });
            parameters.Add(new SqlParameter("@IsModel", SqlDbType.Bit) { Value = g.Filters.IsMissingModel });
            parameters.Add(new SqlParameter("@IsDesc", SqlDbType.Bit) { Value = g.Filters.IsMissingDesc });
            parameters.Add(new SqlParameter("@IsLongDesc", SqlDbType.Bit) { Value = g.Filters.IsMissingLongDesc });
            parameters.Add(new SqlParameter("@IsBarLen", SqlDbType.Bit) { Value = g.Filters.IsMissingBrlLen });
            parameters.Add(new SqlParameter("@IsOvrLen", SqlDbType.Bit) { Value = g.Filters.IsMissingOvrLen });
            parameters.Add(new SqlParameter("@IsWeight", SqlDbType.Bit) { Value = g.Filters.IsMissingWeight });
            parameters.Add(new SqlParameter("@IsImage", SqlDbType.Bit) { Value = g.Filters.IsMissingImage });
            parameters.Add(new SqlParameter("@IsCaAwRest", SqlDbType.Bit) { Value = g.Filters.IsCaAwRestricted });
            parameters.Add(new SqlParameter("@IsHidden", SqlDbType.Bit) { Value = g.IsHidden });
            parameters.Add(new SqlParameter("@IsCurModel", SqlDbType.Bit) { Value = g.IsCurModel });
            parameters.Add(new SqlParameter("@IsMissOnly", SqlDbType.Bit) { Value = g.Filters.IsMissingAll });
            parameters.Add(new SqlParameter("@IsOnDataFeed", SqlDbType.Bit) { Value = g.Filters.IsMissingGunType });
            parameters.Add(new SqlParameter("@IsLEO", SqlDbType.Bit) { Value = g.Filters.IsLeo });
            parameters.Add(new SqlParameter("@IsCaLegal", SqlDbType.Bit) { Value = g.CaRestrict.CaOkay });
            parameters.Add(new SqlParameter("@IsCaRoster", SqlDbType.Bit) { Value = g.CaRestrict.CaRosterOk });
            parameters.Add(new SqlParameter("@IsCaSaRev", SqlDbType.Bit) { Value = g.CaRestrict.CaSglActnOk });
            parameters.Add(new SqlParameter("@IsCaSsPst", SqlDbType.Bit) { Value = g.CaRestrict.CaSglShotOk });
            parameters.Add(new SqlParameter("@IsCaCurRel", SqlDbType.Bit) { Value = g.CaRestrict.CaCurioOk });
            parameters.Add(new SqlParameter("@IsCaPpt", SqlDbType.Bit) { Value = g.CaRestrict.CaPptOk });

            return parameters;
        }

        public List<SelectListItem> GetManufacturersInStockGuns(int locId)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                string proc = ConfigurationHelper.GetPropertyValue("application", "ProcMenuManufInStockGuns");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var prm = new SqlParameter("@LocID", SqlDbType.Int) { Value = locId };
                cmd.Parameters.Add(prm); 

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows) return list;
                while (dr.Read())
                {
                    var id = dr["ManufID"].ToString();
                    var n = dr["ManufName"].ToString();

                    list.Add(new SelectListItem { Value = id, Text = n });
                }
            }

            return list;
        }


        public List<GunNavModel> CalibersByCategory(int catId, int mfgId)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuCalibersByGroup");

            var prm = new List<SqlParameter>();
            prm.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = catId });
            prm.Add(new SqlParameter("@ManufID", SqlDbType.Int) { Value = mfgId });
            menu = GenericMenuList(AdminSqlConnection, p, prm);
            return menu;
        }


        public List<GunNavModel> MfgMapCodes(int distId)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcEditManufGetCodes");

            var prm = new List<SqlParameter>();
            prm.Add(new SqlParameter("@DistID", SqlDbType.Int) { Value = distId });
            menu = GenericMenuList(GunDbSqlConnection, p, prm);
            return menu;
        }

        private List<GunNavModel> ImagesManuf(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuImgManuf");

            var prm = new List<SqlParameter>();
            prm.Add(new SqlParameter("@IsMissing", SqlDbType.Bit) { Value = g.ItemMissing });
            menu = GenericMenuList(GunDbSqlConnection, p, prm, g);
            return menu;
        }

        private List<GunNavModel> ImagesGunType(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuImgGunType");

            var prm = new List<SqlParameter>();
            prm.Add(new SqlParameter("@IsMissing", SqlDbType.Bit) { Value = g.ItemMissing });
            prm.Add(new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId });
            menu = GenericMenuList(GunDbSqlConnection, p, prm, g);
            return menu;
        }

        private List<GunNavModel> ImagesCaliber(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuImgCaliber");

            var prm = new List<SqlParameter>();
            prm.Add(new SqlParameter("@IsMissing", SqlDbType.Bit) { Value = g.ItemMissing });
            prm.Add(new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId });
            prm.Add(new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = g.GunTypeId });
            menu = GenericMenuList(GunDbSqlConnection, p, prm, g);
            return menu;
        }


        private List<GunNavModel> DupesManuf(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuDupesManuf");

            var prm = new List<SqlParameter>();
            prm.Add(new SqlParameter("@SrchID", SqlDbType.Int) { Value = g.ValueId });
            menu = GenericMenuList(GunDbSqlConnection, p, prm, g);
            return menu;
        }

        private List<GunNavModel> DupesGunType(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuDupesGunType");

            var prm = new List<SqlParameter>();
            prm.Add(new SqlParameter("@SrchID", SqlDbType.Int) { Value = g.ValueId });
            prm.Add(new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId });
            menu = GenericMenuList(GunDbSqlConnection, p, prm, g);
            return menu;
        }

        private List<GunNavModel> DupesCaliber(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuDupesCaliber");

            var prm = new List<SqlParameter>();
            prm.Add(new SqlParameter("@SrchID", SqlDbType.Int) { Value = g.ValueId });
            prm.Add(new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId });
            prm.Add(new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = g.GunTypeId });
            menu = GenericMenuList(GunDbSqlConnection, p, prm, g);
            return menu;
        }

        private List<GunNavModel> GunFeedManuf(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuManufFeedCount");

            var bp = SetBaseMenuParams(g);
            menu = GenericMenuList(GunDbSqlConnection, p, bp, g);
            return menu;
        }

        private List<GunNavModel> GunFeedGunTypes(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuGunTypeFeedCount");

            var bp = SetBaseMenuParams(g);
            bp.Add(new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId });

            menu = GenericMenuList(GunDbSqlConnection, p, bp, g);
            return menu;
        }

        private List<GunNavModel> GunFeedCalibers(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuCaliberFeedCount");

            var bp = SetBaseMenuParams(g);
            bp.Add(new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId });
            bp.Add(new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = g.GunTypeId });

            menu = GenericMenuList(GunDbSqlConnection, p, bp, g);
            return menu;
        }

        private List<GunNavModel> GunFeedActions(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuActionFeedCount");

            var bp = SetBaseMenuParams(g);
            bp.Add(new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId });
            bp.Add(new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = g.GunTypeId });
            bp.Add(new SqlParameter("@CaliberID", SqlDbType.Int) { Value = g.CaliberId });

            menu = GenericMenuList(GunDbSqlConnection, p, bp, g);
            return menu;
        }

        private List<GunNavModel> GunFeedDaysPast(GunModel g)
        {
            var menu = new List<GunNavModel>();
            string p = ConfigurationHelper.GetPropertyValue("application", "ProcMenuDaysPastFeedCount");

            var bp = SetBaseMenuParams(g);
            bp.Add(new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId });
            bp.Add(new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = g.GunTypeId });
            bp.Add(new SqlParameter("@CaliberID", SqlDbType.Int) { Value = g.CaliberId });
            bp.Add(new SqlParameter("@ActionID", SqlDbType.Int) { Value = g.ActionId });

            menu = GenericMenuList(GunDbSqlConnection, p, bp, g);
            return menu;
        }

        private MenuCollection GetMenuCollection(string p, string cboId, string cboTxt, int id)
        {
            var mc = new MenuCollection();
            
            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                string proc = ConfigurationHelper.GetPropertyValue("application", p);
                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };

                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                cmd.Parameters.Add(param);
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows) return mc;
                var l1 = new List<SelectListItem>();
                while (dr.Read())
                {
                    var val = dr[cboId].ToString();
                    var n = dr[cboTxt].ToString();
                    l1.Add(new SelectListItem { Value = val, Text = n });
                }

                mc.List1 = l1;

                dr.NextResult();
                if (!dr.HasRows) return mc;
                var l2 = new List<SelectListItem>();

                while (dr.Read())
                {
                    var val = dr[cboId].ToString();
                    var n = dr[cboTxt].ToString();
                    l2.Add(new SelectListItem { Value = val, Text = n });
                }

                mc.List2 = l2;
            }

            return mc;
        }



        public List<SelectListItem> GetSelectMenuBool(string p, string cboId, string cboTxt, bool bit)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                string proc = ConfigurationHelper.GetPropertyValue("application", p);
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Bit) { Value = bit };
                cmd.Parameters.Add(param);

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows) return list;
                while (dr.Read())
                {
                    var id = dr[cboId].ToString();
                    var n = dr[cboTxt].ToString();

                    list.Add(new SelectListItem { Value = id, Text = n });
                }
            }

            return list;
        }


        private List<SelectListItem> GetSelectMenu(string p, string cboId, string cboTxt, int id)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                string proc = ConfigurationHelper.GetPropertyValue("application", p);
                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };

                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                cmd.Parameters.Add(param);
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows) return list;
                while (dr.Read())
                {
                    var val = dr[cboId].ToString();
                    var n = dr[cboTxt].ToString();

                    list.Add(new SelectListItem { Value = val, Text = n });
                }
            }

            return list;
        }


        private List<SelectListItem> GetSelectMenu(string sqlConn, string p, string cboId, string cboTxt, int id)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(sqlConn))
            {
                string proc = ConfigurationHelper.GetPropertyValue("application", p);
                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };

                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                cmd.Parameters.Add(param);
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows) return list;
                while (dr.Read())
                {
                    var val = dr[cboId].ToString();
                    var n = dr[cboTxt].ToString();

                    list.Add(new SelectListItem { Value = val, Text = n });
                }
            }

            return list;
        }


        private List<SelectListItem> GetSelectMenu(string p, string cboId, string cboTxt, bool isCap)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                string proc = ConfigurationHelper.GetPropertyValue("application", p);
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows) return list;
                while (dr.Read())
                {
                    var id = dr[cboId].ToString();
                    var n = dr[cboTxt].ToString();
                    if (isCap) { n = n.ToUpper(); }

                    list.Add(new SelectListItem { Value = id, Text = n });
                }
            }

            return list;
        }

        private List<SelectListItem> GetSelectMenu(string connection, string p, string cboId, string cboTxt)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(connection))
            {
                string proc = ConfigurationHelper.GetPropertyValue("application", p);
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows) return list;
                while (dr.Read())
                {
                    var id = dr[cboId].ToString();
                    var txt = dr[cboTxt].ToString();

                    list.Add(new SelectListItem { Value = id, Text = txt });
                }
            }

            return list;
        }


        private List<SelectListItem> GetSelectMenu(string connection, string p, string cboId, string cboTxt, string cboCt)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(connection))
            {
                string proc = ConfigurationHelper.GetPropertyValue("application", p);
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows) return list;
                while (dr.Read())
                {
                    var id = dr[cboId].ToString();
                    var t = dr[cboTxt].ToString();
                    var c = dr[cboCt].ToString();
                    var txt = string.Format("{0}: ({1})", t, c);

                    list.Add(new SelectListItem { Value = id, Text = txt });
                }
            }

            return list;
        }


        private List<GunNavModel> GenericMenuList(string connection, string procName, List<SqlParameter> parameters)
        {
            SqlDataReader sdr;
            var menu = new List<GunNavModel>();

            using (var conn = new SqlConnection(connection))
            {
                var cmd = new SqlCommand(procName, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                if (parameters != null)
                {
                    foreach (var parameter in parameters) { try { cmd.Parameters.Add(parameter); } catch (Exception e) { } }
                }

                try
                {
                    sdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                

                if (!sdr.HasRows) return menu;

                while (sdr.Read())
                {
                    var id = sdr[0].ToString();
                    var name = sdr[1].ToString();

                    var nv = new GunNavModel();
                    nv.Link = id;
                    nv.MenuText = name;
                    menu.Add(nv);
                }
            }
            return menu;
        }


        private List<GunNavModel> GenericMenuList(string connection, string procName, List<SqlParameter> parameters, GunModel g)
        {
            SqlDataReader sdr;
            var menu = new List<GunNavModel>();

            using (var conn = new SqlConnection(connection))
            {
                var cmd = new SqlCommand(procName, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                if (parameters != null)
                {
                    foreach (var parameter in parameters) { try { cmd.Parameters.Add(parameter); } catch (Exception exc) { } }
                }

                try
                {
                    sdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                if (!sdr.HasRows) return menu;

                if (g.IsLink)
                {
                    while (sdr.Read())
                    {
                        var n1 = 0;
                        var gunType = sdr[0].ToString();
                        var count = Int32.TryParse(sdr[1].ToString(), out n1) ? Convert.ToInt32(sdr[1]) : 0;
                        var link = sdr[2].ToString();

                        var nv = new GunNavModel();
                        nv.GunType = gunType;
                        nv.ItemCount = count;
                        nv.Link = link;
                        menu.Add(nv);
                    }
                }
                else
                {
                    while (sdr.Read())
                    {
                        var id = Convert.ToInt32(sdr[0]);
                        var name = sdr[1].ToString();
                        var count = Convert.ToInt32(sdr[2]);

                        var nv = new GunNavModel();
                        nv.IntValue = id;
                        nv.ItemCount = count;
                        nv.MenuText = name;
                        menu.Add(nv);
                    }
                }


            }
            return menu;
        }



        public MenuModel() {}

        public MenuModel(string mName)
        {
            
        }

        /* ITEM DUPLICATES */
        public MenuModel(GunModel g, DupMenus d)
        {
            switch (d)
            {
                default:
                    FeedManuf = DupesManuf(g);
                    FeedGunType = DupesGunType(g);
                    FeedCaliber = DupesCaliber(g);
                    break;
                case DupMenus.Mfg:
                    FeedGunType = DupesGunType(g);
                    FeedCaliber = DupesCaliber(g);
                    break;
                case DupMenus.Gtp:
                    FeedCaliber = DupesCaliber(g);
                    break;

            }

        }


        /* Images */
        public MenuModel(GunModel g, ImgMenus d)
        {
            switch (d)
            {
                default:
                    FeedManuf = ImagesManuf(g);
                    FeedGunType = ImagesGunType(g);
                    FeedCaliber = ImagesCaliber(g);
                    break;
                case ImgMenus.Mfg:
                    FeedGunType = ImagesGunType(g);
                    FeedCaliber = ImagesCaliber(g);
                    break;
                case ImgMenus.Gtp:
                    FeedCaliber = ImagesCaliber(g);
                    break;

            }

        }


        /* USE FOR DATA FEED ADMIN */
        public MenuModel(GunModel g, int s)
        {
            switch (s)
            {
                case 1:
                    FeedManuf = GunFeedManuf(g);
                    FeedGunType = GunFeedGunTypes(g);
                    FeedCaliber = GunFeedCalibers(g);
                    FeedAction = GunFeedActions(g);
                    FeedDaysPast = GunFeedDaysPast(g);
                    break;
                case 2:
                    FeedGunType = GunFeedGunTypes(g);
                    FeedCaliber = GunFeedCalibers(g);
                    FeedAction = GunFeedActions(g);
                    FeedDaysPast = GunFeedDaysPast(g);
                    break;
                case 3:
                    FeedCaliber = GunFeedCalibers(g);
                    FeedAction = GunFeedActions(g);
                    FeedDaysPast = GunFeedDaysPast(g);
                    break;
                case 4:
                    FeedAction = GunFeedActions(g);
                    FeedDaysPast = GunFeedDaysPast(g);
                    break;
                case 5:
                    FeedDaysPast = GunFeedDaysPast(g);
                    break;
                case 6:
                    FeedManuf = GunFeedManuf(g);
                    FeedGunType = GunFeedGunTypes(g);
                    FeedCaliber = GunFeedCalibers(g);
                    break;
            }

            ManufAll = new GunManufMenu { Manufacturers = GetManufacturersAll() };
            GunType = new GunTypeMenu { GunTypes = GetGunTypes() };
            AllCaliber = new GunCaliberMenu { AllCalibers = GetAllCalibers() };
            Action = new GunActionMenu { Actions = GetGunActions() };
            Finish = new GunFinishMenu { GunFinishes = GetGunFinishes() };
            Condition = new GunConditionMenu { GunConditions = GetGunConditions() };
            //FeedManuf = GunFeedManuf(g);
            //FeedGunType = GunFeedGunTypes(g);
            //FeedCaliber = GunFeedCalibers(g);
            //FeedAction = GunFeedActions(g);
            //FeedDaysPast = GunFeedDaysPast(g);
        }

        public MenuModel(Pages p)
        {
            switch (p)
            {
                case Pages.InventoryGuns:
                    Action = new GunActionMenu { Actions = GetGunActions() };
                    AcqDist = new DistMenu { Distributors = GetAcqDistributors() };
                    AllCaliber = new GunCaliberMenu { AllCalibers = GetAllCalibers() };
                    BrlInModel = new BarrelInModel { BarrelInches = MeasureInches(97) };
                    BrlDecModel = new BarrelDecModel { BarrelDecimal = MeasureDecimal() };
                    CaFsc = new FscMenu { FscList = GetCaFscTypes() };
                    ChbInModel = new ChamberInModel { ChamberInches = MeasureInches(10) };
                    ChbDecModel = new ChamberDecModel { ChamberDecimal = MeasureDecimal() };
                    Condition = new GunConditionMenu { GunConditions = GetGunConditions() };
                    CustTypes = new CustomerTypesMenu { CustomerTypes = GetCustomerTypes() };
                    DdModel = new DayModel { Day = GetDays() };
                    DocCats = new DocCategoriesMenu { DocumentCategories = GetDocCategories() };
                    Finish = new GunFinishMenu { GunFinishes = GetGunFinishes() };
                    GunType = new GunTypeMenu { GunTypes = GetGunTypes() };
                    GunLockManuf = new GunLockMenu { GunLockMfgs = GetGunLockManufacturers() };
                    Industry = GetCustomerIndusty();
                    Importers = new GunManufMenu { Manufacturers = GetImporters() };
                    InStockMfg = new GunManufMenu { Manufacturers = GetManufacturersInStockGuns(-1) };
                    ManufAll = new GunManufMenu { Manufacturers = GetManufacturersAll() };
                    ManufWeb = new GunManufMenu { Manufacturers = GetManufacturersWeb() };
                    MoModel = new MonthModel { Month = GetMonths() };
                    OvlInModel = new OverallInModel { OverallInches = MeasureInches(115) };
                    OvlDecModel = new OverallDecModel { OverallDecimal = MeasureDecimal() };
                    RefModel = new ReferralModel { ReferralSources = GetReferralSources() };
                    SecQuest = new SecurityQuestionModel { SecurityQuestions = GetSecurityQuestions() };
                    SellerType = new GunSellerTypeMenu { GunSellerTypes = GetGunSellers() };
                    TransTyp = new TransTypesMenu { TransactionTypes = GetTransTypes() };
                    UsStateAbrv = new UsStatesMenu { UsStates = GetAbvStates() };
                    UsStateNames = new UsStatesMenu { UsStates = GetUsStateNames() };
                    YrExpModel = new YearModel { Year = GetYears(-10, +10) };
                    YrModel = new YearModel { Year = GetYears(-105, -15) };
                    WebCaliber = new GunCaliberMenu { WebCalibers = GetWebCalibers() };
                    WeightLb = new WeightLbModel { WeightPounds = MeasureInches(75) };
                    WeightOz = new WeightOzModel { WeightOunces = WeightDecimal(.00, .99) };
                    break;
                case Pages.InventoryAmmo:
                    SellerType = new GunSellerTypeMenu { GunSellerTypes = GetGunSellers() };
                    TransTyp = new TransTypesMenu { TransactionTypes = GetTransTypes() };
                    ManufAmmo = new GunManufMenu { Manufacturers = GetManufacturersAll() };
                    InStockMfg = new GunManufMenu { Manufacturers = GetManufacturersInStockAmmo() };
                    InStockCal = new GunCaliberMenu { AllCalibers = GetCalibersInStockAmmo() };
                    AmmoCaliber = new GunCaliberMenu { AllCalibers = GetAllCalibers() };
                    Bullets = new BulletTypeMenu { BulletTypes = GetBulletTypes() };
                    Condition = new GunConditionMenu { GunConditions = GetGunConditions() };
                    ItemSubCategories = new SubCategoryMenu { SubCategories = GetItemSubCategories(200) };
                    UsStateAbrv = new UsStatesMenu { UsStates = GetAbvStates() };
                    UsStateNames = new UsStatesMenu { UsStates = GetUsStateNames() };
                    break;
                case Pages.InventoryMerchandise:
                    SellerType = new GunSellerTypeMenu { GunSellerTypes = GetGunSellers() };
                    TransTyp = new TransTypesMenu { TransactionTypes = GetTransTypes() };
                    ManufAll = new GunManufMenu { Manufacturers = GetManufacturersAll() };
                    InStockMfg = new GunManufMenu { Manufacturers = GetManufacturersInStockMerch() };
                    MerchOtherStuff = new MerchandiseMenu { Categories = GetMerchandiseAll() };
                    Condition = new GunConditionMenu { GunConditions = GetGunConditions() };
                    ItemColors = new ColorMenu { Colors = GetColors() };
                    Shipping = new ShippingMenu { ShippingBoxes = GetShippingSizes() };
                    WeightOz = new WeightOzModel { WeightOunces = WeightDecimal(.00, .99) };
                    UsStateAbrv = new UsStatesMenu { UsStates = GetAbvStates() };
                    UsStateNames = new UsStatesMenu { UsStates = GetUsStateNames() };
                    break;
                case Pages.BoundBook:
                    AcqDist = new DistMenu { Distributors = GetAcqDistributors() };
                    ManufAll = new GunManufMenu { Manufacturers = GetManufacturersAll() };
                    Importers = new GunManufMenu { Manufacturers = GetImporters() };
                    AllCaliber = new GunCaliberMenu { AllCalibers = GetAllCalibers() };
                    GunType = new GunTypeMenu { GunTypes = GetGunTypes() };
                    UsStateNames = new UsStatesMenu { UsStates = GetUsStateNames() };
                    UsStateAbrv = new UsStatesMenu { UsStates = GetAbvStates() };
                    SellerType = new GunSellerTypeMenu { GunSellerTypes = GetGunSellers() };
                    break;
                case Pages.Cflc:
                    ManufAll = new GunManufMenu { Manufacturers = GetManufacturersAll() };
                    AllCaliber = new GunCaliberMenu { AllCalibers = GetAllCalibers() };
                    GunType = new GunTypeMenu { GunTypes = GetGunTypes() };
                    break;
                case Pages.CaHiCap:
                    ManufAll = new GunManufMenu { Manufacturers = GetManufacturersAll() };
                    AllCaliber = new GunCaliberMenu { AllCalibers = GetAllCalibers() };
                    GunType = new GunTypeMenu { GunTypes = GetGunTypes() };
                    break;
                case Pages.OrdersCreate:
                    AcqDist = new DistMenu { Distributors = GetAcqDistributors() };
                    FflCodes = new DistMenu { Distributors = GetFflCodes() };
                    Action = new GunActionMenu { Actions = GetGunActions() };
                    AddCustomer = new AddCustomerMenu { Customers = GetAllCustomers() };
                    AllCaliber = new GunCaliberMenu { AllCalibers = GetAllCalibers() };
                    Bullets = new BulletTypeMenu { BulletTypes = GetBulletTypes() };
                    CaFsc = new FscMenu { FscList = GetCaFscTypes() };
                    Condition = new GunConditionMenu { GunConditions = GetGunConditions() };
                    CustProfession = new ProfessionsMenu { Professions = GetProfessions(0) };
                    CustTypes = new CustomerTypesMenu { CustomerTypes = GetCustomerTypes() };
                    DocCats = new DocCategoriesMenu { DocumentCategories = GetDocCategories() };
                    DdModel = new DayModel { Day = GetDays() };
                    Finish = new GunFinishMenu { GunFinishes = GetGunFinishes() };
                    Fulfillment = new FulfillmentMenu { Fulfillments = GetFulfillmentTypes() };
                    GunType = new GunTypeMenu { GunTypes = GetGunTypes() };
                    GunLockManuf = new GunLockMenu { GunLockMfgs = GetGunLockManufacturers() };
                    Industry = GetCustomerIndusty();
                    RecoveryObjectives = GetRecoveryObjectives();
                    ManufAll = new GunManufMenu { Manufacturers = GetManufacturersAll() };
                    ItemSubCategories = new SubCategoryMenu { SubCategories = GetItemSubCategories(200) };
                    MerchOtherStuff = new MerchandiseMenu { Categories = GetMerchandiseAll() };
                    MoModel = new MonthModel { Month = GetMonths() };
                    PaymentMethods = GetPaymentMethods();
                    RefModel = new ReferralModel { ReferralSources = GetReferralSources() };
                    SecQuest = new SecurityQuestionModel { SecurityQuestions = GetSecurityQuestions() };
                    SellerType = new GunSellerTypeMenu { GunSellerTypes = GetGunSellers() };
                    Shipping = new ShippingMenu { ShippingBoxes = GetShippingSizes() };
                    UsStateAbrv = new UsStatesMenu { UsStates = GetAbvStates() };
                    UsStateNames = new UsStatesMenu { UsStates = GetUsStateNames() };
                    YrModel = new YearModel { Year = GetYears(-105, -15) };
                    YrExpModel = new YearModel { Year = GetYears(-10, +10) };
                    WeightOz = new WeightOzModel { WeightOunces = WeightDecimal(.00, .99) };
                    break;
                case Pages.DataFeedManuf:
                    FeedMfgDistCt = new DistMenu { Distributors = GetDistributorsCount() };
                    ManufAll = new GunManufMenu { Manufacturers = GetManufacturersAll() };
                    FeedMfgDist = new DistMenu { Distributors = GetDistributors() };
                    break;
                case Pages.ContentManager:
                    Categories = new ContentMenu { ContentList = GetContentSections() };
                    ContentPgs = new ContentMenu { ContentList = GetContentPages(0) };
                    break;
                case Pages.ContentHomeScroll:
                case Pages.ContentCampaigns:
                    break;
                case Pages.CustomerAdd:
                    Industry = GetCustomerIndusty();
                    UsStateAbrv = new UsStatesMenu { UsStates = GetAbvStates() };
                    UsStateNames = new UsStatesMenu { UsStates = GetUsStateNames() };
                    CustTypes = new CustomerTypesMenu { CustomerTypes = GetCustomerTypes() };
                    RefModel = new ReferralModel { ReferralSources = GetReferralSources() };
                    CustProfession = new ProfessionsMenu { Professions = GetProfessions(0) };
                    DocCats = new DocCategoriesMenu { DocumentCategories = GetDocCategories() };  
                    DdModel = new DayModel { Day = GetDays() };
                    MoModel = new MonthModel { Month = GetMonths() };
                    YrModel = new YearModel { Year = GetYears(-105, -15) };
                    YrExpModel = new YearModel { Year = GetYears(-10, +10) };
                    SecQuest = new SecurityQuestionModel { SecurityQuestions = GetSecurityQuestions() };
                    CaFsc = new FscMenu { FscList = GetCaFscTypes() };
                    break;
            }
        }
    }
}