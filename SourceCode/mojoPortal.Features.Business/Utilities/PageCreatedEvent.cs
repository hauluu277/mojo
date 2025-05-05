using System;
using mojoPortal.Business;
using log4net;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Business.WebHelpers.PageEventHandlers;
using System.Data;

namespace Utilities
{
    public class CustomPageCreatedEventHandler : PageCreatedEventHandlerPovider
    {
        private static readonly ILog log
        = LogManager.GetLogger(typeof(CustomPageCreatedEventHandler));

        /* All Module Guid Here
         * HtmlContentFeatureName       881e4e00-93e4-444c-b7b0-6672fb55de10
         * ArticleLoaderFeatureName     026773f3-aeae-472c-a8f1-d556c7e68481
         * ArticleFeatureName           0102dfab-d04f-490d-a2da-d4fcb068eaf4
         * VideoFeatureName             5df19b37-f234-4141-a0e9-6cff5c6776cc
        */
        public override void PageCreatedHandler(object sender, PageCreatedEventArgs e)
        {
            if (sender == null) return;
            SiteSettings settings = UtilCacheHelper.GetCurrentSiteSettings();
            char[] param = {';'};
            string[] listModuleID = settings.AddThisDotComUsername.Split(param);
            PageSettings page = sender as PageSettings;
            //Publish must have modules
            foreach (string t in listModuleID)
            {
                int moduleId;
                if (!int.TryParse(t, out moduleId)) { return; }
                Module m = new Module(moduleId);
                DataTable modulesTable = Module.GetPageModulesTable(moduleId);
                if (modulesTable.Rows.Count <= 0) { return; }
                if (page != null)
                    Module.Publish(
                        page.ParentGuid,
                        m.ModuleGuid,
                        m.ModuleId,
                        page.PageId,
                        modulesTable.Rows[0]["PaneName"].ToString(),
                        int.Parse(modulesTable.Rows[0]["ModuleOrder"].ToString()),
                        DateTime.Parse(modulesTable.Rows[0]["PublishBeginDate"].ToString()),
                        DateTime.MinValue);
            }
            //Create custom modules
                //ModuleDefinition moduleDef = new ModuleDefinition(new Guid("0102dfab-d04f-490d-a2da-d4fcb068eaf4"));
                //Module module = new Module();
                //module.FeatureGuid = moduleDef.FeatureGuid;
                //module.SiteGuid = settings.SiteGuid;
                //module.SiteId = settings.SiteId;
                //module.PageId = page.PageId;
                //module.ModuleDefId = moduleDef.ModuleDefId;
                //module.ModuleOrder = 3;
                //module.PaneName = "contentpane";
                //module.ModuleTitle = "Content";
                //module.AuthorizedEditRoles = "Content Publishers;";
                //module.CreatedByUserId = SiteUtils.GetCurrentSiteUser().UserId;
                //module.CacheTime = moduleDef.DefaultCacheTime;
                //module.ShowTitle = WebConfigSettings.ShowModuleTitlesByDefault;
                //module.Save();
                //ModuleSettings.UpdateModuleSetting(module.ModuleGuid, module.ModuleId, "CustomCssClassSetting", "");
            //log
            if (page != null) log.Debug("CustomPageCreatedEventHandler handled PageCreated event for " + page.PageName);
        }
    }
}