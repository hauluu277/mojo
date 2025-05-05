
// Author:					Manhnd
// Created:					2020-4-11
// Last Modified:			2020-4-11
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

    public class md_Officer
    {

        #region Constructors

        public md_Officer()
        { }


        public md_Officer(
            int itemID)
        {
            Getmd_Officer(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private string name = string.Empty;
        private string position = string.Empty;
        private string email = string.Empty;
        private string phone = string.Empty;
        private string missionOfficer = string.Empty;
        private string urlImage = string.Empty;
        private int officerID = -1;
        private int count = -1;
        private int isTop = -1;
        private int orderByOfficer = 1;

        #endregion

        #region Public Properties
        public int OrderByOfficer
        {
            get { return orderByOfficer; }
            set { orderByOfficer = value; }
        }

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Position
        {
            get { return position; }
            set { position = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string MissionOfficer
        {
            get { return missionOfficer; }
            set { missionOfficer = value; }
        }
        public string UrlImage
        {
            get { return urlImage; }
            set { urlImage = value; }
        }
        public int OfficerID
        {
            get { return officerID; }
            set { officerID = value; }
        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public int IsTop
        {
            get { return isTop; }
            set { isTop = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of md_Officer.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_Officer(
            int itemID)
        {
            using (IDataReader reader = DBmd_Officer.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt32(reader["ItemID"]);
                this.name = reader["Name"].ToString();
                this.position = reader["Position"].ToString();
                this.email = reader["Email"].ToString();
                if (!string.IsNullOrEmpty(reader["Phone"].ToString()))
                {
                    this.phone = reader["Phone"].ToString();
                }
                this.missionOfficer = reader["MissionOfficer"].ToString();
                this.urlImage = reader["UrlImage"].ToString();
                this.officerID = Convert.ToInt32(reader["OfficerID"]);
                this.count = Convert.ToInt32(reader["Count"]);
                this.isTop = Convert.ToInt32(reader["IsTop"]);
                if (!string.IsNullOrEmpty(reader["OrderByOfficer"].ToString()))
                {
                    this.orderByOfficer = Convert.ToInt32(reader["OrderByOfficer"]);
                }
            }

        }

        /// <summary>
        /// Persists a new instance of md_Officer. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBmd_Officer.Create(
                this.name,
                this.position,
                this.email,
                this.phone,
                this.missionOfficer,
                this.urlImage,
                this.officerID,
                this.count,
                this.isTop,
                this.orderByOfficer);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_Officer. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBmd_Officer.Update(
                this.itemID,
                this.name,
                this.position,
                this.email,
                this.phone,
                this.missionOfficer,
                this.urlImage,
                this.officerID,
                this.count,
                this.isTop,
                this.orderByOfficer);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_Officer. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            if (this.itemID > 0)
            {
                return Update();
            }
            else
            {
                return Create();
            }
        }




        #endregion

        #region Static Methods

        /// <summary>
        /// Deletes an instance of md_Officer. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBmd_Officer.Delete(
                itemID);
        }

        public static void Delete(List<md_Officer> listOfficer)
        {
            if (listOfficer != null && listOfficer.Count > 0)
            {
                foreach (var item in listOfficer)
                {
                    Delete(item.itemID);
                }
            }
        }


        /// <summary>
        /// Gets a count of md_Officer. 
        /// </summary>
        public static int GetCount()
        {
            return DBmd_Officer.GetCount();
        }

        private static List<md_Officer> LoadListFromReader(IDataReader reader)
        {
            List<md_Officer> md_OfficerList = new List<md_Officer>();
            try
            {
                while (reader.Read())
                {
                    md_Officer md_Officer = new md_Officer();
                    md_Officer.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_Officer.name = reader["Name"].ToString();
                    md_Officer.position = reader["Position"].ToString();
                    md_Officer.email = reader["Email"].ToString();
                    if (!string.IsNullOrEmpty(reader["Phone"].ToString()))
                    {
                        md_Officer.phone = reader["Phone"].ToString();
                    }

                    md_Officer.missionOfficer = reader["MissionOfficer"].ToString();
                    md_Officer.urlImage = reader["UrlImage"].ToString();
                    md_Officer.officerID = Convert.ToInt32(reader["OfficerID"]);
                    md_Officer.count = Convert.ToInt32(reader["Count"]);
                    md_Officer.isTop = Convert.ToInt32(reader["IsTop"]);
                    if (!string.IsNullOrEmpty(reader["OrderByOfficer"].ToString()))
                    {
                        md_Officer.orderByOfficer = Convert.ToInt32(reader["OrderByOfficer"]);
                    }
                    md_OfficerList.Add(md_Officer);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_OfficerList;

        }

        /// <summary>
        /// Gets an IList with all instances of md_Officer.
        /// </summary>
        public static List<md_Officer> GetAll()
        {
            IDataReader reader = DBmd_Officer.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of md_Officer.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<md_Officer> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_Officer.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }
        public static List<md_Officer> GetList(int officerID)
        {
            IDataReader reader = DBmd_Officer.GetList(officerID);
            return LoadListFromReader(reader);
        }

        public static List<md_Officer> GetList_ld(int officerID)
        {
            IDataReader reader = DBmd_Officer.GetList_ld(officerID);
            return LoadListFromReader(reader);
        }

        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_Officer.
        /// </summary>
        public static int CompareByItemID(md_Officer md_Officer1, md_Officer md_Officer2)
        {
            return md_Officer1.ItemID.CompareTo(md_Officer2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of md_Officer.
        /// </summary>
        public static int CompareByName(md_Officer md_Officer1, md_Officer md_Officer2)
        {
            return md_Officer1.Name.CompareTo(md_Officer2.Name);
        }
        /// <summary>
        /// Compares 2 instances of md_Officer.
        /// </summary>
        public static int CompareByPosition(md_Officer md_Officer1, md_Officer md_Officer2)
        {
            return md_Officer1.Position.CompareTo(md_Officer2.Position);
        }
        /// <summary>
        /// Compares 2 instances of md_Officer.
        /// </summary>
        public static int CompareByEmail(md_Officer md_Officer1, md_Officer md_Officer2)
        {
            return md_Officer1.Email.CompareTo(md_Officer2.Email);
        }

        /// <summary>
        /// Compares 2 instances of md_Officer.
        /// </summary>
        public static int CompareByMissionOfficer(md_Officer md_Officer1, md_Officer md_Officer2)
        {
            return md_Officer1.MissionOfficer.CompareTo(md_Officer2.MissionOfficer);
        }
        /// <summary>
        /// Compares 2 instances of md_Officer.
        /// </summary>
        public static int CompareByUrlImage(md_Officer md_Officer1, md_Officer md_Officer2)
        {
            return md_Officer1.UrlImage.CompareTo(md_Officer2.UrlImage);
        }
        /// <summary>
        /// Compares 2 instances of md_Officer.
        /// </summary>
        public static int CompareByOfficerID(md_Officer md_Officer1, md_Officer md_Officer2)
        {
            return md_Officer1.OfficerID.CompareTo(md_Officer2.OfficerID);
        }

        #endregion


    }

}





