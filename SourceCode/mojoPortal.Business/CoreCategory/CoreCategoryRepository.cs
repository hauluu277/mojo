
// Author:					HiNet JSC
// Created:					2014-6-27
// Last Modified:			2014-6-27
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using mojoPortal.Data;

namespace mojoPortal.Business
{

    public class CoreCoreCategoryRespository
    {

        public CoreCoreCategoryRespository()
        { }

        /// <summary>
        /// Persists a new instance of CoreCategory. Returns true on success.
        /// </summary>
        /// <returns></returns>
        public void Save(CoreCategory category)
        {
            if (category.ItemID == -1)
            {
                int newId = DBCoreCategory.Create1(
                    category.ParentID,
                    category.SiteID,
                    category.Name,
                    category.NameEN,
                    category.Description,
                    category.ItemCount,
                    category.CreatedUtc,
                    category.CreatedBy,
                    category.ModifiedUtc,
                    category.ModifiedBy,
                    category.Priority,
                    category.IconID,
                    category.Automatic,
                    category.CoreSkinID,
                    category.CoreSkinDefault,
                    category.IsPhongBan,
                    category.ShowMenuLeft,
                    category.PathIMG,
                    category.PathFile,
                    category.SubName,
                    category.IsLinhVucDieuTra,
                    category.IsTinTuc);

                category.ItemID = newId;
            }
            else
            {
                DBCoreCategory.Update(
                    category.ItemID,
                    category.ParentID,
                    category.SiteID,
                    category.Name,
                    category.NameEN,
                    category.Description,
                    category.ItemCount,
                    category.CreatedUtc,
                    category.CreatedBy,
                    category.ModifiedUtc,
                    category.ModifiedBy,
                    category.Priority,
                    category.IconID,
                    category.Automatic,
                    category.CoreSkinID,
                    category.CoreSkinDefault,
                    category.IsPhongBan,
                    category.ShowMenuLeft,
                    category.PathIMG,
                    category.PathFile,
                    category.SubName,
                    category.IsLinhVucDieuTra,
                    category.IsTinTuc
                    );

            }

        }


        /// <param name="itemID"> itemID </param>
        public CoreCategory Fetch(
            int itemID)
        {
            using (IDataReader reader = DBCoreCategory.GetOne(
                itemID))
            {
                if (reader.Read())
                {
                    CoreCategory category = new CoreCategory();
                    category.ItemID = Convert.ToInt32(reader["ItemID"]);
                    category.ParentID = Convert.ToInt32(reader["ParentID"]);
                    category.SiteID = Convert.ToInt32(reader["SiteID"]);
                    category.Name = reader["Name"].ToString();
                    category.NameEN = reader["NameEN"].ToString();
                    category.Description = reader["Description"].ToString();
                    category.ItemCount = Convert.ToInt32(reader["ItemCount"]);
                    category.CreatedUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    category.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                    category.ModifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                    category.ModifiedBy = new Guid(reader["ModifiedBy"].ToString());
                    category.Priority = Convert.ToInt32(reader["Priority"]);
                    return category;

                }
            }

            return null;
        }


        /// <summary>
        /// Deletes an instance of CoreCategory. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public bool Delete(
            int itemID)
        {
            return DBCoreCategory.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of CoreCategory. 
        /// </summary>
        //public int GetCount(int siteId, string keyword, int parentid)
        //{
        //    return DBCoreCategory.GetCount(siteId, keyword, parentid);
        //}


        /// <summary>
        /// Gets an IList with all instances of CoreCategory.
        /// </summary>
        public List<CoreCategory> GetAll()
        {
            IDataReader reader = DBCoreCategory.GetAll();
            return LoadListFromReader(reader);

        }


        private List<CoreCategory> LoadListFromReader1(IDataReader reader)
        {
            List<CoreCategory> categoryList = new List<CoreCategory>();
            try
            {
                while (reader.Read())
                {
                    CoreCategory category = new CoreCategory();
                    category.ItemID = Convert.ToInt32(reader["ItemID"]);
                    category.ParentID = Convert.ToInt32(reader["ParentID"]);
                    category.SiteID = Convert.ToInt32(reader["SiteID"]);
                    category.Name = reader["Name"].ToString();
                    category.NameEN = reader["NameEN"].ToString();
                    category.ParentName = reader["ParentName"].ToString();
                    category.Description = reader["Description"].ToString();
                    category.ItemCount = Convert.ToInt32(reader["ItemCount"]);
                    category.CreatedUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    category.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                    category.ModifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                    category.ModifiedBy = new Guid(reader["ModifiedBy"].ToString());
                    category.Priority = Convert.ToInt32(reader["Priority"]);
                    category.IconID = Convert.ToInt32(reader["IconID"]);
                    categoryList.Add(category);

                }
            }
            finally
            {
                reader.Close();
            }
            return categoryList;
        }
        private List<CoreCategory> LoadListFromReader(IDataReader reader)
        {
            List<CoreCategory> categoryList = new List<CoreCategory>();

            try
            {
                while (reader.Read())
                {
                    CoreCategory category = new CoreCategory();
                    category.ItemID = Convert.ToInt32(reader["ItemID"]);
                    category.ParentID = Convert.ToInt32(reader["ParentID"]);
                    category.SiteID = Convert.ToInt32(reader["SiteID"]);
                    category.Name = reader["Name"].ToString();
                    category.NameEN = reader["NameEN"].ToString();
                    category.ParentName = reader["ParentName"].ToString();
                    category.Description = reader["Description"].ToString();
                    category.ItemCount = Convert.ToInt32(reader["ItemCount"]);
                    category.CreatedUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    category.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                    category.ModifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                    category.ModifiedBy = new Guid(reader["ModifiedBy"].ToString());
                    category.Priority = Convert.ToInt32(reader["Priority"]);
                    category.IconID = Convert.ToInt32(reader["IconID"]);
                    categoryList.Add(category);

                }
            }
            finally
            {
                reader.Close();
            }

            return categoryList;

        }


    }

}





