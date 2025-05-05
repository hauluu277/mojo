
// Author:					HiNet
// Created:					2014-9-3
// Last Modified:			2014-9-3
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

    public class LookupRespository
    {
        private const string featureGuid = "50e5119c-087f-4be3-a9d0-a1b8bac93998";
        public int LookUpPageId = -1;
        public int LookUpModuleId = -1;
        public static Guid FeatureGuid
        {
            get { return new Guid(featureGuid); }
        }

        public LookupRespository()
        { }

        /// <summary>
        /// Persists a new instance of Lookup. Returns true on success.
        /// </summary>
        /// <returns></returns>
        public void Save(Lookup lookup)
        {
            if (lookup.ItemID == -1)
            {
                int newId = DBLookup.Create(
                    lookup.ModuleID,
                    lookup.SiteID,
                    lookup.Name,
                    lookup.Description,
                    lookup.Interpretation,
                    lookup.Censorship,
                    lookup.IsPublic,
                    lookup.UserCreate,
                    lookup.DateCreate,
                    lookup.UserApprove,
                    lookup.DateApprove,
                    lookup.ItemUrl,
                    lookup.PageID);

                lookup.ItemID = newId;
            }
            else
            {
                DBLookup.Update(
                    lookup.ItemID,
                    lookup.ModuleID,
                    lookup.SiteID,
                    lookup.Name,
                    lookup.Description,
                    lookup.Interpretation,
                    lookup.Censorship,
                    lookup.IsPublic,
                    lookup.UserCreate,
                    lookup.DateCreate,
                    lookup.UserApprove,
                    lookup.DateApprove,
                    lookup.ItemUrl,
                    lookup.PageID);

            }

        }


        /// <param name="itemID"> itemID </param>
        public Lookup Fetch(
            int itemID)
        {
            using (IDataReader reader = DBLookup.GetOne(
                itemID))
            {
                if (reader.Read())
                {
                    Lookup lookup = new Lookup();
                    lookup.ItemID = Convert.ToInt32(reader["ItemID"]);
                    lookup.ModuleID = Convert.ToInt32(reader["ModuleID"]);
                    lookup.SiteID = Convert.ToInt32(reader["SiteID"]);
                    lookup.Name = reader["Name"].ToString();
                    lookup.Description = reader["Description"].ToString();
                    lookup.Interpretation = reader["Interpretation"].ToString();
                    lookup.Censorship = Convert.ToBoolean(reader["Censorship"]);
                    lookup.IsPublic = Convert.ToBoolean(reader["IsPublic"]);
                    lookup.UserCreate = Convert.ToInt32(reader["UserCreate"]);
                    lookup.DateCreate = Convert.ToDateTime(reader["DateCreate"]);
                    lookup.UserApprove = Convert.ToInt32(reader["UserApprove"]);
                    lookup.DateApprove = Convert.ToDateTime(reader["DateApprove"]);
                    lookup.ItemUrl = reader["ItemUrl"].ToString();
                    lookup.PageID = Convert.ToInt32(reader["PageID"]);
                    return lookup;

                }
            }

            return null;
        }


        /// <summary>
        /// Deletes an instance of Lookup. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public bool Delete(
            int itemID)
        {
            return DBLookup.Delete(
                itemID);
        }

        public List<Lookup> GetById(int itemID)
        {
            IDataReader reader = DBLookup.GetOne(itemID);
            return LoadListFromReader(reader, out LookUpPageId, out LookUpModuleId);
        }
        public List<Lookup> GetByTop(int Top)
        {
            IDataReader reader = DBLookup.GetByTop(Top);
            return LoadListFromReader(reader, out LookUpPageId, out LookUpModuleId);
        }

        /// <summary>
        /// Gets a count of Lookup. 
        /// </summary>
        public int GetCount(string keyword)
        {
            return DBLookup.GetCount(keyword);
        }


        /// <summary>
        /// Gets an IList with all instances of Lookup.
        /// </summary>
        public List<Lookup> GetAll()
        {
            IDataReader reader = DBLookup.GetAll();
            return LoadListFromReader(reader, out LookUpPageId, out LookUpModuleId);

        }

        /// <summary>
        /// Gets an IList with page of instances of Lookup.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public List<Lookup> GetOrther(int itemId, int top)
        {
            IDataReader reader = DBLookup.GetOrther(itemId, top);
            return LoadListFromReader(reader, out LookUpPageId, out LookUpModuleId);
        }

        public List<Lookup> GetPageByModule(int moduleId, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBLookup.GetPageByModule(moduleId, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader, out LookUpPageId, out LookUpModuleId);
        }

        public List<Lookup> GetPage(bool role, string keyword, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBLookup.GetPage(role, keyword, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader, out LookUpPageId, out LookUpModuleId);
        }


        private List<Lookup> LoadListFromReader(IDataReader reader, out int pageid, out int moduleid)
        {
            List<Lookup> lookupList = new List<Lookup>();
            int _pageid = -1;
            int _moduleid = -1;
            try
            {
                int i = 0;

                while (reader.Read())
                {
                    i++;
                    Lookup lookup = new Lookup();
                    lookup.ItemID = Convert.ToInt32(reader["ItemID"]);
                    lookup.ModuleID = Convert.ToInt32(reader["ModuleID"]);
                    lookup.SiteID = Convert.ToInt32(reader["SiteID"]);
                    lookup.Name = reader["Name"].ToString();
                    lookup.Description = reader["Description"].ToString();
                    lookup.Interpretation = reader["Interpretation"].ToString();
                    lookup.Censorship = Convert.ToBoolean(reader["Censorship"]);
                    lookup.IsPublic = Convert.ToBoolean(reader["IsPublic"]);
                    lookup.UserCreate = Convert.ToInt32(reader["UserCreate"]);
                    lookup.DateCreate = Convert.ToDateTime(reader["DateCreate"]);
                    lookup.UserApprove = Convert.ToInt32(reader["UserApprove"]);
                    lookup.DateApprove = Convert.ToDateTime(reader["DateApprove"]);
                    lookup.ItemUrl = reader["ItemUrl"].ToString();
                    lookup.PageID = Convert.ToInt32(reader["PageID"]);
                    lookupList.Add(lookup);
                    if (i <= 1)
                    {
                        _pageid = lookup.PageID;
                        _moduleid = lookup.ModuleID;
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            pageid = _pageid;
            moduleid = _moduleid;
            return lookupList;

        }


    }

}






