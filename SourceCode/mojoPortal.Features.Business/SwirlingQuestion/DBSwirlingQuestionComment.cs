using mojoPortal.Features.Business.SwirlingQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwirlingQuestionFeature.Business
{
    public class DBSwirlingQuestionComment
    {
        SwirlingQuestionDC db = new SwirlingQuestionDC();

        public ud_QAComment GetOne(Guid itemGuid)
        {
            var result = from p in db.ud_QAComments
                         where p.Guid.Equals(itemGuid)
                         select p;
            return result.FirstOrDefault();
        }

        public List<ud_QAComment> GetAll(int moduleID)
        {
            var result = from p in db.ud_QAComments
                         where p.ModuleID.Equals(moduleID)
                         orderby p.CreatedDate descending
                         select p;
            return result.ToList<ud_QAComment>();
        }

        public List<ud_QAComment> GetAllByQAGuid(Guid qaGuid)
        {
            var result = from p in db.ud_QAComments
                         where p.QAGuid.Equals(qaGuid)
                         orderby p.CreatedDate descending
                         select p;
            return result.ToList<ud_QAComment>();
        }

        public bool Delete(Guid itemGuid)
        {
            bool flag = false;
            ud_QAComment item = GetOne(itemGuid);
            db.ud_QAComments.DeleteOnSubmit(item);
            try
            {
                db.SubmitChanges();
                flag = true;
            }
            catch { }
            return flag;
        }

        public bool Create(int moduleID, Guid qaGuid, string title, string name, string email, string comment)
        {
            bool flag = false;

            ud_QAComment item = new ud_QAComment();
            item.Guid = Guid.NewGuid();
            item.ModuleID = moduleID;
            item.QAGuid = qaGuid;
            item.Title = title;
            item.Name = name;
            item.Email = email;
            item.Comment = comment;
            item.CreatedDate = DateTime.UtcNow;
            db.ud_QAComments.InsertOnSubmit(item);
            try
            {
                db.SubmitChanges();
                flag = true;
            }
            catch { }
            return flag;
        }

        public bool Update(Guid itemGuid, Guid qaGuid, string title, string name, string email, string comment)
        {
            bool flag = false;

            ud_QAComment item = GetOne(itemGuid);
            if (item != null)
            {
                item.QAGuid = qaGuid;
                item.Title = title;
                item.Name = name;
                item.Email = email;
                item.Comment = comment;
                item.CreatedDate = DateTime.UtcNow;
                try
                {
                    db.SubmitChanges();
                    flag = true;
                }
                catch { }
            }
            return flag;
        }

        public void DeleteByModule(int moduleID)
        {
            var result = from p in db.ud_QAComments
                         where p.ModuleID.Equals(moduleID)
                         select p;
            db.ud_QAComments.DeleteAllOnSubmit(result);
            db.SubmitChanges();
        }
    }
}
