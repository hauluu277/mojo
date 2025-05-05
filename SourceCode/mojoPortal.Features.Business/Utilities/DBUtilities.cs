using mojoPortal.Features.Business.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Utilities
{
    public class DBUtilities
    {
        readonly mojoPortalLinq db;
        public DBUtilities()
        {
            db = new mojoPortalLinq();
        }

        public DBUtilities(string connectionString)
        {
            db = new mojoPortalLinq(connectionString);
        }

        public mp_User GetUser(Guid userGuid)
        {
            var result = (from p in db.mp_Users
                          where p.UserGuid == userGuid
                          select p).FirstOrDefault();
            return result;
        }

        public List<mp_ModuleSettings_SelectResult> GetCustomSettingValues(int moduleId)
        {
            return db.mp_ModuleSettings_Select(moduleId).ToList();
        }

        public mp_Modules_SelectOneResult GetModule(int moduleId)
        {
            return db.mp_Modules_SelectOne(moduleId).FirstOrDefault();
        }
        public List<ModuleBO> GetModuleBO(int siteId, Guid moduleGuid)
        {

            var listModulePage = db.mp_PageModules.ToList();
            listModulePage = listModulePage ?? new List<mp_PageModule>();
            var result = (from p in db.mp_Modules
                          where p.SiteID == siteId
                           && p.FeatureGuid.Equals(moduleGuid)
                          select new ModuleBO
                          {
                              ModuleDefID = p.ModuleDefID,
                              ModuleID = p.ModuleID,
                              ModuleTitle = p.ModuleTitle,
                              SiteID = p.SiteID

                          }).ToList();
            foreach (var item in result)
            {
                var firstModulePage = listModulePage.Where(x => x.ModuleID.Equals(item.ModuleID)).FirstOrDefault();
                if (firstModulePage != null)
                {
                    item.PageID = firstModulePage.PageID;
                }
                else
                {
                    item.PageID = -1;
                }
            }
            return result.Where(x => x.PageID > 0).ToList();
        }
        public mp_User GetUserByUserID(int userID)
        {
            var result = (from p in db.mp_Users
                          where p.UserID == userID
                          select p).FirstOrDefault();
            return result;
        }

        public List<mp_Module> GetModule()
        {
            var result = from p in db.mp_Modules
                         select p;
            return result.ToList();
        }

        public List<mp_Module> GetModuleById(int moduleId)
        {
            var result = from p in db.mp_Modules
                         where p.ModuleID.Equals(moduleId)
                         select p;
            return result.ToList();
        }

        public List<mp_Module> GetModuleByModuleGuid(Guid moduleGuid)
        {
            var result = from p in db.mp_Modules
                         where p.FeatureGuid.Equals(moduleGuid)
                         select p;
            return result.ToList();
        }

        public List<mp_Module> GetModuleByModuleGuidAndSite(int siteId, Guid moduleGuid)
        {
            var result = from p in db.mp_Modules
                         where p.SiteID==siteId
                         where p.FeatureGuid.Equals(moduleGuid)
                         select p;
            return result.ToList();
        }
        public List<mp_Module> GetModuleByModuleGuid(Guid moduleGuid, int siteID)
        {
            var result = from p in db.mp_Modules
                         where p.FeatureGuid.Equals(moduleGuid) && p.SiteID.Equals(siteID)
                         select p;
            return result.ToList();
        }

        public mp_PageModule GetFirstPageByModuleID(int moduleID)
        {
            var result = from p in db.mp_PageModules
                         where p.ModuleID == moduleID
                         select p;
            return result.FirstOrDefault();
        }

        
        public string GetCounter()
        {
            var result = (from p in db.ud_Counters
                          select p).FirstOrDefault();
            if (result != null)
            {
                string counter = "000000000" + result.Num;
                counter = counter.Substring(counter.Length - 9, 9);
                return counter;
            }
            return string.Empty;
        }

        public string GetCounter(int number, bool useImage, string imageSiteRoot)
        {
            var result = (from p in db.ud_Counters
                          select p).FirstOrDefault();
            if (result != null)
            {
                string counter = string.Empty;
                for (int i = 0; i < number; i++)
                {
                    counter += "0";
                }
                counter += result.Num.ToString();
                counter = counter.Substring(counter.Length - number, number);
                if (useImage)
                {
                    int length = counter.Length;

                    for (int i = 1; i <= length; i++)
                    {
                        counter += "<img src='" + imageSiteRoot + "/Data/Images/Counter/" + counter.Substring(0, 1) + ".png'>";
                        counter = counter.Remove(0, 1);
                    }
                }
                return counter;
            }
            return string.Empty;
        }
        
        public void UpdateCounter()
        {
            var result = (from p in db.ud_Counters
                          select p).FirstOrDefault();
            if (result != null)
            {
                result.Num++;
                result.LastModified = DateTime.UtcNow;
                try { db.SubmitChanges(); }
                catch { }
            }

            else { InsertCounter(); }
        }

        private void InsertCounter()
        {
            ud_Counter counter = new ud_Counter {Num = 1, LastModified = DateTime.UtcNow};
            db.ud_Counters.InsertOnSubmit(counter);
            db.SubmitChanges();
        }

        static public DataTable LINQToDataTable(System.Data.Linq.DataContext ctx, object query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            IDbCommand cmd = ctx.GetCommand(query as IQueryable);
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter
                                                               {
                                                                   SelectCommand =
                                                                       (System.Data.SqlClient.SqlCommand) cmd
                                                               };
            DataTable dt = new DataTable("ListAssign");

            try
            {
                cmd.Connection.Open();
                adapter.FillSchema(dt, SchemaType.Source);
                adapter.Fill(dt);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return dt;
        }

        #region USERS
        public List<mp_User> Users_GetListWaiting()
        {
            var result = from p in db.mp_Users
                         join q in db.mp_UserRoles on p.UserID equals q.UserID
                         where q.RoleID == 21
                         select p;
            return result.ToList();
        }
        public mp_UserRole Users_CheckWaiting(int userId)
        {
            var result = from p in db.mp_UserRoles
                         where p.RoleID == 21 && p.UserID == userId
                         select p;
            return result.FirstOrDefault();
        }
        #endregion USERS

        #region Country
        public mp_GeoCountry CountryGetOne(Guid countryGuid)
        {
            return db.mp_GeoCountries.Where(p => p.Guid == countryGuid).FirstOrDefault();
        }

        public List<mp_GeoCountry> CountryGetAll()
        {
            return (db.mp_GeoCountries.OrderBy(p => p.Name)).ToList();
        }

        public mp_GeoCountry CountryGetOneByGeoZone(Guid geoZoneGuid)
        {
            return db.mp_GeoCountries.Where(q => q.Guid == db.mp_GeoZones.Where(p => p.Guid == geoZoneGuid).FirstOrDefault().CountryGuid).FirstOrDefault();
        }
        #endregion

        #region GeoZone
        public mp_GeoZone GeoZoneGetOne(Guid geoZoneGuid)
        {
            return db.mp_GeoZones.Where(p => p.Guid == geoZoneGuid).FirstOrDefault();
        }

        public List<mp_GeoZone> GeoZoneGetAll()
        {
            return db.mp_GeoZones.OrderBy(p => p.Name).ToList();
        }

        public List<mp_GeoZone> GeoZoneGetAllByCountry(Guid countryGuid)
        {
            return db.mp_GeoZones.Where(p => p.CountryGuid == countryGuid).ToList();
        }

        #endregion
    }
    public class ModuleBO
    {
        public int ModuleID { get; set; }

        public int? SiteID { get; set; }

        public int ModuleDefID { get; set; }

        public string ModuleTitle { get; set; }
        public int PageID { get; set; }
        public string PageName { get; set; }

        //public string AuthorizedEditRoles;

        //public int CacheTime;

        //public bool? ShowTitle;

        //public int EditUserID;

        //public bool AvailableForMyPage;

        //public bool AllowMultipleInstancesOnMyPage;

        //public string Icon;

        //public int CreatedByUserID;

        //public DateTime? CreatedDate;

        //public int CountOfUseOnMyPage;

        //public Guid? Guid;

        //public Guid? FeatureGuid;

        //public Guid? SiteGuid;

        //public Guid? EditUserGuid;

        //public bool HideFromUnAuth;

        //public bool HideFromAuth;

        //public string ViewRoles;

        //public string DraftEditRoles;
    }
}
