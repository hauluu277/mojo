using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.BaoCao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.Business
{
    public class md_ArticlesBusiness : BaseBusiness<md_Articles>
    {
        public md_ArticlesBusiness(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public List<md_Articles> GetDanhSachByIdDonVi(string IdClient, DateTime? TuNgay, DateTime? DenNgay)
        {
            var listArtileSearch = new List<md_Articles>();

            if (TuNgay != null && DenNgay != null)
            {
                listArtileSearch = this.context.md_Articles.Where(x => TuNgay <= x.CreatedDate && x.CreatedDate <= DenNgay).ToList();
            }
            else if (TuNgay != null && DenNgay == null)
            {
                listArtileSearch = this.context.md_Articles.Where(x => TuNgay <= x.CreatedDate && x.CreatedDate <= DateTime.Now).ToList();
            }
            else if (TuNgay == null && DenNgay != null)
            {
                listArtileSearch = this.context.md_Articles.Where(x => x.CreatedDate <= DenNgay).ToList();
            }
            else
            {
                listArtileSearch = this.context.md_Articles.ToList();
            }
            return listArtileSearch.Where(x => x.ClientId == IdClient).ToList();
        }

        public List<BaoCaoDanhMucResultBieuDoBO> getTinTucByDanhMuc(string CodeDanhMuc, DateTime? TuNgay, DateTime? DenNgay)
        {

            var listArtileSearch = new List<md_Articles>();



            if (TuNgay != null && DenNgay != null)
            {
                listArtileSearch = this.context.md_Articles.Where(x => TuNgay <= x.CreatedDate && x.CreatedDate <= DenNgay).ToList();
            }
            else if (TuNgay != null && DenNgay == null)
            {
                listArtileSearch = this.context.md_Articles.Where(x => TuNgay <= x.CreatedDate && x.CreatedDate <= DateTime.Now).ToList();
            }
            else if (TuNgay == null && DenNgay != null)
            {
                listArtileSearch = this.context.md_Articles.Where(x => x.CreatedDate <= DenNgay).ToList();
            }
            else
            {
                listArtileSearch = this.context.md_Articles.ToList();
            }


            var result = new List<md_Articles>();
            var danhMucGoc = this.context.core_Category.FirstOrDefault(x => x.Code == CodeDanhMuc);
            var listCap1 = new List<core_Category>();
            var listlonhonCap1 = new List<core_Category>();

            var listObj = new List<BaoCaoDanhMucBO>();

            if (danhMucGoc != null)
            {
                // list cấp 1
                listCap1 = this.context.core_Category.Where(x => x.ParentID == danhMucGoc.ItemID).ToList();
                if (listCap1 != null && listCap1.Any())
                {
                    foreach (var item in listCap1)
                    {
                        listObj.Add(new BaoCaoDanhMucBO()
                        {
                            core_Category = item,
                            core_Categories = LaySoLuongBaiViet(item.ItemID)
                        });
                    }
                }
            }

            var listDataResult = new List<BaoCaoDanhMucResultBieuDoBO>();
            if (listObj != null && listObj.Any())
            {

                foreach (var item in listObj)
                {
                    var listArticleCateGoc = this.context.md_ArticleCategory.Where(x => x.CategoryID == item.core_Category.ItemID).Select(x => x.ArticleID).ToList();
                    var listArticleGoc = listArtileSearch.Where(x => listArticleCateGoc.Contains(x.ItemID)).ToList();

                    var listCate = item.core_Categories.Select(x => x.ItemID).ToList();

                    var listAticleCate = this.context.md_ArticleCategory.Where(x => listCate.Contains((int)x.CategoryID)).Select(x => x.ArticleID).ToList();
                    var listArticle = listArtileSearch.Where(x => listAticleCate.Contains(x.ItemID)).ToList();

                    listDataResult.Add(new BaoCaoDanhMucResultBieuDoBO
                    {
                        NameCate = item.core_Category.Name,
                        //md_Articles = listArticle,
                        CountArticle = listArticle.Concat(listArticleGoc).ToList().Count,
                        ItemCateId = item.core_Category.ItemID
                    });
                }
            }

            return listDataResult;
        }

        public List<BaoCaoDanhMucResultBO> getTinTucByDanhMucKhongBieuDo(string CodeDanhMuc, DateTime? TuNgay, DateTime? DenNgay)
        {
            var listArtileSearch = new List<md_Articles>();



            if (TuNgay != null && DenNgay != null)
            {
                listArtileSearch = this.context.md_Articles.Where(x => TuNgay <= x.CreatedDate && x.CreatedDate <= DenNgay).ToList();
            }
            else if (TuNgay != null && DenNgay == null)
            {
                listArtileSearch = this.context.md_Articles.Where(x => TuNgay <= x.CreatedDate && x.CreatedDate <= DateTime.Now).ToList();
            }
            else if (TuNgay == null && DenNgay != null)
            {
                listArtileSearch = this.context.md_Articles.Where(x => x.CreatedDate <= DenNgay).ToList();
            }
            else
            {
                listArtileSearch = this.context.md_Articles.ToList();
            }

            var result = new List<md_Articles>();
            var danhMucGoc = this.context.core_Category.FirstOrDefault(x => x.Code == CodeDanhMuc);
            var listCap1 = new List<core_Category>();
            var listlonhonCap1 = new List<core_Category>();

            var listObj = new List<BaoCaoDanhMucBO>();

            if (danhMucGoc != null)
            {
                // list cấp 1
                listCap1 = this.context.core_Category.Where(x => x.ParentID == danhMucGoc.ItemID).ToList();
                if (listCap1 != null && listCap1.Any())
                {
                    foreach (var item in listCap1)
                    {
                        listObj.Add(new BaoCaoDanhMucBO()
                        {
                            core_Category = item,
                            core_Categories = LaySoLuongBaiViet(item.ItemID)
                        });
                    }
                }
            }

            var listDataResult = new List<BaoCaoDanhMucResultBO>();
            if (listObj != null && listObj.Any())
            {
                foreach (var item in listObj)
                {
                    var listArticleCateGoc = this.context.md_ArticleCategory.Where(x => x.CategoryID == item.core_Category.ItemID).Select(x => x.ArticleID).ToList();
                    var listArticleGoc = listArtileSearch.Where(x => listArticleCateGoc.Contains(x.ItemID)).ToList();

                    var listCate = item.core_Categories.Select(x => x.ItemID).ToList();

                    var listAticleCate = this.context.md_ArticleCategory.Where(x => listCate.Contains((int)x.CategoryID)).Select(x => x.ArticleID).ToList();
                    var listArticle = listArtileSearch.Where(x => listAticleCate.Contains(x.ItemID)).ToList();

                    listDataResult.Add(new BaoCaoDanhMucResultBO
                    {
                        core_Category = item.core_Category,
                        md_Articles = listArticle.Concat(listArticleGoc).ToList(),
                        CountArticle = listArticle.Count
                    });
                }
            }

            return listDataResult;
        }

        public List<core_Category> LaySoLuongBaiViet(int id)
        {
            var result = new List<core_Category>();
            var listChild = this.context.core_Category.Where(x => x.ParentID == id).ToList();
            result.AddRange(listChild);
            if (listChild != null && listChild.Any())
            {
                foreach (var item in listChild)
                {
                    result.AddRange(LaySoLuongBaiViet(item.ItemID));
                }
            }

            return result;
        }

        public List<md_Articles> GetByCategory(int categoryId, bool getChild = false)
        {
            var listCategory = new List<int>();
            listCategory.Add(categoryId);
            if (getChild)
            {
                var listCategoryChild = context.core_Category.Where(x => x.ParentID == categoryId).Select(x => x.ItemID).ToList();
                listCategory.AddRange(listCategoryChild);
            }

            var tableArticle = context.md_ArticleCategory.Where(x =>x.CategoryID.HasValue && listCategory.Contains(x.CategoryID.Value))
                .Select(x => x.ArticleID).Distinct();

            var listArticle = (from article in context.md_Articles
                               join tblArticle in tableArticle
                               on article.ItemID equals tblArticle
                               where article.IsPublished == true && article.IsApproved == true
                               orderby article.ItemID
                               select article
                             ).Take(20).ToList();
            return listArticle;


        }
    }
}
