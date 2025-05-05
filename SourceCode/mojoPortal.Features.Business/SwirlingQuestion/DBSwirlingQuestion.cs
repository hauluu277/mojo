using mojoPortal.Features.Business.SwirlingQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace SwirlingQuestionFeature.Business
{
    public class DBSwirlingQuestion
    {
        readonly SwirlingQuestionDC db = new SwirlingQuestionDC();

        public md_QA GetOne(Guid itemGuid)
        {
            var result = from p in db.md_QAs
                         where p.Guid.Equals(itemGuid)
                         select p;
            return result.FirstOrDefault();
        }
        public List<md_QA> GetItemGuid(Guid itemGuid)
        {
            var result = from p in db.md_QAs
                         where p.Guid == itemGuid
                         select p;
            return result.ToList();
        }
        public List<md_QA> GetAll(int siteID, int moduleID)
        {
            var result = from p in db.md_QAs
                         //where p.ModuleID.Equals(moduleID)
                         where p.SiteID == siteID
                         orderby p.LastModified descending
                         select p;
            return result.ToList();
        }

        public List<md_QA> GetAllUnpublised(int siteID, int moduleID)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.IsPublished==true
                         where p.Is_Active==true
                         where p.SiteID == siteID
                         orderby p.LastModified descending
                         select p;
            return result.ToList();
        }

        public List<md_QA> GetAllPublished(int siteID, int moduleID, int catId, int coQuanId, int order, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where p.IsPublished
                         where p.Is_Active==true
                         orderby p.LastModified descending
                         where p.SiteID==siteID
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            if (order == 1)
            {
                return result.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderByDescending(o => o.NgayXuatBanCauHoi).ToList();
            }
            else
            {
                return result.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(o => o.NgayXuatBanCauHoi).ToList();
            }
        }
        public List<md_QA> GetAllPublishedByUser(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int userId, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.IsPublished
                         where p.Is_Active == true
                         orderby p.LastModified descending
                         where p.SiteID == siteID
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         where userId>0? p.CreatQuestionByUser==userId:true
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAll(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, bool? tiepnhan, bool? duyetcauhoi, bool? xuatbancauhoi, bool? duthao, bool? duyetduthao, bool? xuatbanduthao, DateTime? begindate, DateTime? enddate, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.ViPhamQuyChe == false
                         where p.StatusSend==true
                         where p.Is_Active == true
                         where p.SiteID == siteID
                         orderby p.LastModified descending
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         where tiepnhan != null ? p.TiepNhanCauHoi == tiepnhan : true
                         where duyetcauhoi != null ? p.DuyetCauHoi == duyetcauhoi : true
                         where xuatbancauhoi != null ? p.IsPublished == xuatbancauhoi : true
                         where duthao != null ? p.DuThaoTraLoi == duthao : true
                         where duyetduthao != null ? p.DuyetDuThaoTraLoi == duyetduthao : true
                         where begindate!=null? p.LastModified >= begindate :true
                         where enddate!=null?p.LastModified<=enddate:true
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllExport(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, bool? tiepnhan, bool? duyetcauhoi, bool? xuatbancauhoi, bool? duthao, bool? duyetduthao, bool? xuatbanduthao, DateTime? begindate, DateTime? enddate)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.ViPhamQuyChe == false
                         where p.StatusSend == true
                         where p.Is_Active == true
                         orderby p.LastModified descending
                         where p.SiteID == siteID
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         where tiepnhan != null ? p.TiepNhanCauHoi == tiepnhan : true
                         where duyetcauhoi != null ? p.DuyetCauHoi == duyetcauhoi : true
                         where xuatbancauhoi != null ? p.IsPublished == xuatbancauhoi : true
                         where duthao != null ? p.DuThaoTraLoi == duthao : true
                         where duyetduthao != null ? p.DuyetDuThaoTraLoi == duyetduthao : true
                         where xuatbanduthao != null ? p.XuatBanDuThaoTraLoi == xuatbanduthao : true
                         where begindate != null ? p.LastModified >= begindate : true
                         where enddate != null ? p.LastModified <= enddate : true
                         select p;
            return result.ToList();
        }
        public List<md_QA> GetExPort(int siteID, int moduleID, bool? tiepnhan, bool? duyetcauhoi, bool? xuatbancauhoi, bool? duthao, bool? duyetduthao, bool? xuatbanduthao)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.ViPhamQuyChe == false
                         where p.StatusSend == true
                         where p.Is_Active == true
                         orderby p.LastModified descending
                         where p.SiteID == siteID
                         where tiepnhan != null ? p.TiepNhanCauHoi == tiepnhan : true
                         where duyetcauhoi != null ? p.DuyetCauHoi == duyetcauhoi : true
                         where xuatbancauhoi != null ? p.IsPublished == xuatbancauhoi : true
                         where duthao != null ? p.DuThaoTraLoi == duthao : true
                         where duyetduthao != null ? p.DuyetDuThaoTraLoi == duyetduthao : true
                         where xuatbanduthao != null ? p.XuatBanDuThaoTraLoi == xuatbanduthao : true
                         select p;
            return result.ToList();
        }
        public List<md_QA> GetAllDraftAnswer(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.IsPublished
                         where p.DuThaoTraLoi==true
                         where p.SiteID == siteID
                         where p.ChuyenDuThao == true
                         where p.KhongPheDuyetTraLoi.Value==false
                         where p.DuyetDuThaoTraLoi==false
                         where p.Is_Active == true
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllAproveAnswer(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.IsPublished
                         where p.DuThaoTraLoi == true
                         where p.DuyetDuThaoTraLoi == true
                         where p.XuatBanDuThaoTraLoi == false
                         where p.Is_Active == true
                         where p.SiteID == siteID
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllUnAproveAnswer(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.IsPublished
                         where p.DuThaoTraLoi == true
                         where p.DuyetDuThaoTraLoi==false
                         where p.KhongPheDuyetTraLoi.Value == true
                         where p.XuatBanDuThaoTraLoi == false
                         where p.SiteID == siteID
                         where p.Is_Active == true
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllIsPublishedAnswer(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.IsPublished
                         where p.DuThaoTraLoi == true
                         where p.DuyetDuThaoTraLoi == true
                         where p.XuatBanDuThaoTraLoi == true
                         where p.SiteID == siteID
                         where p.Is_Active == true
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllDeleted(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.Is_Active==false
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where p.SiteID == siteID
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> ExportViPhamQuyChe(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.ViPhamQuyChe == true
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.DuyetThongBao == false
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.XuatBanThongBao == false
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.Is_Active == true
                         where p.SiteID == siteID
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            return result.ToList();
        }
        public List<md_QA> GetAllViPhamQuyChe(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.ViPhamQuyChe == true
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.DuyetThongBao == false
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.XuatBanThongBao == false
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.Is_Active == true
                         where p.SiteID == siteID
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllViPhamQuyCheDaXoa(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.ViPhamQuyChe == true
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.Is_Active == false
                         where catId > 0 ? p.QACategoryID == catId : true
                         where p.SiteID == siteID
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllApproved(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.Is_Active == true
                         where p.DuyetCauHoi==true
                         where p.IsPublished == false
                         where p.ViPhamQuyChe == false
                         where p.SiteID == siteID
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<md_QA> GetAllReception(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.Is_Active == true
                         where p.TiepNhanCauHoi == true
                         where p.DuyetCauHoi == false
                         where p.IsPublished == false
                         where p.ViPhamQuyChe == false
                         where p.SiteID == siteID
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllUnReception(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ (p.Is_Active == true && p.ViPhamQuyChe==false && p.TiepNhanCauHoi==false && p.StatusSend==true)
                         where catId > 0 ? p.QACategoryID == catId : true
                         where p.SiteID == siteID
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllApproveViPham(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ (p.Is_Active == true && p.ViPhamQuyChe == true && (p.DuyetThongBao.HasValue && p.DuyetThongBao.Value == true) && (p.XuatBanThongBao.HasValue && p.XuatBanThongBao.Value == false))
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where p.SiteID == siteID
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetAllPublicViPham(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ (p.Is_Active == true && p.ViPhamQuyChe == true && (p.DuyetThongBao.HasValue && p.DuyetThongBao.Value == true) && (p.XuatBanThongBao.HasValue && p.XuatBanThongBao.Value == true))
                         where catId > 0 ? p.QACategoryID == catId : true
                         where p.SiteID == siteID
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        //public List<md_QA> GetAllUnReception(int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int pageSize, int currentPage, out int totalPages)
        //{
        //    var result = from p in db.md_QAs
        //                 where /*p.ModuleID.Equals(moduleID) &&*/ (p.Is_Active == true && p.ViPhamQuyChe == false && p.TiepNhanCauHoi == false)
        //                 where catId > 0 ? p.QACategoryID == catId : true
        //                 where coQuanId > 0 ? p.CoQuanID == coQuanId : true
        //                 where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
        //                 orderby p.LastModified descending
        //                 select p;
        //    totalPages = result.Count() / pageSize;
        //    if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
        //    {
        //        totalPages++;
        //    }
        //    return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        //}
        public List<md_QA> GetAllUnApproved(int siteID, int moduleID)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.Is_Active == true
                         where p.SiteID == siteID
                         where p.DuyetCauHoi == false
                         orderby p.LastModified descending
                         select p;
            return result.ToList();
        }

        public List<md_QA> GetAllOthers(int siteID, int moduleID, Guid itemGuid, int Top)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.Guid != itemGuid && p.IsPublished
                         where p.SiteID == siteID
                         orderby p.LastModified descending
                         select p;
            return result.Take(Top).ToList();
        }
        public List<md_QA> GetTopHot(int siteID, int Top, int moduleID)
        {
            var result = (from p in db.md_QAs
                          where p.SiteID == siteID
                          where p.IsHot == true && p.IsPublished
                          orderby p.LastModified descending
                          select p).Take(Top);
            return result.ToList();

        }
        public List<md_QA> GetPage(int siteID, int moduleID, int catId, int coQuanId, int order, int userId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.IsPublished
                         where userId > 0 ? p.AnswerUser != userId : true
                         where catId > 0 ? p.QACategoryID == catId : true
                         where p.SiteID == siteID
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            if (order == 1)// sap xep moi nhat
            {
                return result.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderByDescending(o=>o.NgayXuatBanCauHoi).ToList();
            }
            else
            {
                return result.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(o => o.NgayXuatBanCauHoi).ToList();
            }
        }
        public List<md_QA> GetPageViPham(int siteID, int moduleID, int catId, int coQuanId, int order, int userId, string keyword, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where p.ViPhamQuyChe==true
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.XuatBanThongBao==true
                         where userId > 0 ? p.AnswerUser != userId : true
                         where p.SiteID == siteID
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            if (order == 1)// sap xep moi nhat
            {
                return result.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderByDescending(o => o.NgayXuatBanCauHoi).ToList();
            }
            else
            {
                return result.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(o => o.NgayXuatBanCauHoi).ToList();
            }
        }
        public List<md_QA> GetPageByUser(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int userId, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where catId > 0 ? p.QACategoryID == catId : true
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where p.SiteID == siteID
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         where userId>0? p.AnswerUser==userId:true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetPageByUserNoSend(int siteID, int moduleID, int catId, int coQuanId, int phongBanId, string keyword, int userId, int pageSize, int currentPage, out int totalPages)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.StatusSend==false
                         where catId > 0 ? p.QACategoryID == catId : true
                         where p.SiteID == siteID
                         where coQuanId > 0 ? p.CoQuanID == coQuanId : true
                         where phongBanId > 0 ? p.PhongBanID == phongBanId : true
                         where !string.IsNullOrEmpty(keyword) ? p.FTS.Contains(keyword) : true
                         where userId > 0 ? p.AnswerUser == userId : true
                         orderby p.LastModified descending
                         select p;
            totalPages = result.Count() / pageSize;
            if ((result.Count() % pageSize) != 0 && result.Count() > pageSize)
            {
                totalPages++;
            }
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<md_QA> GetOthersPage(int siteID, int moduleID, int pageSize, int currentPage)
        {
            var result = from p in db.md_QAs
                         where /*p.ModuleID.Equals(moduleID) &&*/ p.IsPublished
                         orderby p.LastModified descending
                         select p;
            return result.Take((currentPage - 1) * pageSize).Skip(pageSize).ToList();
        }

        public bool Delete(Guid itemGuid)
        {
            bool flag = false;
            md_QA item = GetOne(itemGuid);
            db.md_QAs.DeleteOnSubmit(item);
            try
            {
                db.SubmitChanges();
                flag = true;
            }
            catch { }
            return flag;
        }

        public bool Create(int siteID, int moduleID, string title, string question, string answer, bool isPublished, string createdName, string createdEmail, string createdPhone, int answerUserID, string itemUrl, bool isHot, int catID, int coQuanID, out Guid itemGuid, bool is_active, bool tiepnhancauhoi, bool viphamquyche, bool duyetcauhoi, bool duthaotraloi, bool khongduyetduthao, bool duyetduthao, bool xuatbanduthao, string fts, int phongBanID, bool duyetThongBao, bool xuatBanThongBao, int userCreate, bool statusSend, bool chuyenDuThao )
        {
            return Create(siteID, moduleID, title, question, answer, isPublished, createdName, createdEmail, createdPhone,
                          answerUserID, itemUrl, isHot, out itemGuid, DateTime.UtcNow, catID, coQuanID, is_active, tiepnhancauhoi, viphamquyche, duyetcauhoi, duthaotraloi, khongduyetduthao, duyetduthao, xuatbanduthao, fts, phongBanID, duyetThongBao, xuatBanThongBao, userCreate, statusSend, chuyenDuThao);
        }

        public bool Create(int siteID, int moduleID, string title, string question, string answer, bool isPublished, string createdName, string createdEmail, string createdPhone, int answerUserID, string itemUrl, bool isHot, out Guid itemGuid, DateTime modifiedDate, int catID, int coQuanID, bool is_active, bool tiepnhancauhoi, bool viphamquyche, bool duyetcauhoi, bool duthaotraloi, bool khongduyetduthao, bool duyetduthao, bool xuatbanduthao, string fts, int phongBanID, bool duyetThongBao, bool xuatBanThongBao, int userCreate, bool statusSend, bool chuyenDuThao)
        {
            bool flag = false;

            md_QA item = new md_QA
            {
                SiteID = siteID,
                Title = title,
                Guid = Guid.NewGuid(),
                ModuleID = moduleID,
                Question = question,
                Answer = answer,
                IsPublished = isPublished,
                CreatedByName = createdName,
                CreatedByEmail = createdEmail,
                CreatedByPhone = createdPhone,
                AnswerUser = answerUserID,
                ItemUrl = itemUrl,
                IsHot = isHot,
                LastModified = modifiedDate,
                QACategoryID = catID,
                CoQuanID=coQuanID,
                Is_Active=is_active,
                TiepNhanCauHoi=tiepnhancauhoi,
                ViPhamQuyChe=viphamquyche,
                DuyetCauHoi=duyetcauhoi,
                DuThaoTraLoi=duthaotraloi,
                DuyetDuThaoTraLoi=duyetduthao,
                XuatBanDuThaoTraLoi=xuatbanduthao,
                KhongPheDuyetTraLoi=khongduyetduthao,
                FTS=fts,
                PhongBanID=phongBanID,
                DuyetThongBao=duyetThongBao,
                XuatBanThongBao=xuatBanThongBao,
                CreatQuestionByUser=userCreate,
                StatusSend=statusSend,
                ChuyenDuThao=chuyenDuThao,
            };
            itemGuid = item.Guid;
            db.md_QAs.InsertOnSubmit(item);
            try
            {
                db.SubmitChanges();
                flag = true;
            }
            catch { }
            return flag;
        }

        public bool Update(int siteID, Guid itemGuid, string title, string question, string answer, bool isPublished, string createdName, string createdEmail, string createdPhone, int answerUserID, string itemUrl, bool isHot, int catID, int coQuanID, bool isview, bool statusSend)
        {
            return Update(siteID, itemGuid, title, question, answer, isPublished, createdName, createdEmail, createdPhone,
                          answerUserID, itemUrl, isHot, DateTime.UtcNow, catID, coQuanID, isview, statusSend);
        }

        public bool Update(int siteID, Guid itemGuid, string title, string question, string answer, bool isPublished, string createdName, string createdEmail, string createdPhone, int answerUserID, string itemUrl, bool isHot, DateTime modifiedDate, int catID, int coQuanID, bool isview, bool statusSend)
        {
            bool flag = false;

            md_QA item = GetOne(itemGuid);
            if (item != null)
            {
                item.SiteID = siteID;
                item.Title = title;
                item.Question = question;
                item.Answer = answer;
                item.IsPublished = isPublished;
                item.CreatedByName = createdName;
                item.CreatedByEmail = createdEmail;
                item.CreatedByPhone = createdPhone;
                item.AnswerUser = answerUserID;
                item.ItemUrl = itemUrl;
                item.IsHot = isHot;
                item.HitCount++;
                if (isview == false)
                {
                    item.LastModified = modifiedDate;
                }
                item.StatusSend = statusSend;
                item.QACategoryID = catID;
                item.CoQuanID = coQuanID;
                try
                {
                    db.SubmitChanges();
                    flag = true;
                }
                catch { }
            }
            return flag;
        }

        public bool IncreaseCommentCount(Guid itemGuid)
        {
            bool flag = false;

            md_QA item = GetOne(itemGuid);
            if (item != null)
            {
                item.CommentCount++;
                try
                {
                    db.SubmitChanges();
                    flag = true;
                }
                catch { }
            }
            return flag;
        }

        public List<md_QA> Search(int siteID, int moduleID, string searchTerm)
        {
            List<md_QA> result = new List<md_QA>();
            searchTerm = searchTerm.ToLowerInvariant();

            var listModuleID = from p in db.mp_Modules
                               where p.SiteID == siteID
                               select p.ModuleID;
            List<md_QA> allQA = (from p in db.md_QAs
                                 where listModuleID.ToList().Contains(p.ModuleID)
                                 select p).ToList();
            foreach (md_QA qa in allQA)
            {
                if (qa.Title.ToLowerInvariant().Contains(searchTerm) ||
                    qa.Question.ToLowerInvariant().Contains(searchTerm) ||
                    qa.Answer.ToLowerInvariant().Contains(searchTerm) ||
                    FeatureUtilities.ConvertToUnsign(qa.Title).ToLowerInvariant().Contains(searchTerm) ||
                    FeatureUtilities.ConvertToUnsign(qa.Question).ToLowerInvariant().Contains(searchTerm) ||
                    FeatureUtilities.ConvertToUnsign(qa.Answer).ToLowerInvariant().Contains(searchTerm) ||
                    searchTerm == string.Empty)
                {
                    result.Add(qa);
                }
            }
            return result;
        }

        public void DeleteByModule(int moduleID)
        {
            var result = from p in db.md_QAs
                         where p.ModuleID.Equals(moduleID)
                         select p;
            db.md_QAs.DeleteAllOnSubmit(result);
            db.SubmitChanges();
        }
    }
}
