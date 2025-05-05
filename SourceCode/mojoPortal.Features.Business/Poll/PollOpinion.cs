
// Author:					HAULD
// Created:					2015-10-21
// Last Modified:			2015-10-21
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
using PollFeature.Data;

namespace PollFeature.Business
{

    public class PollOpinion
    {

        #region Constructors

        public PollOpinion()
        { }


        public PollOpinion(
            int itemID)
        {
            GetPollOpinion(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private string opinion = string.Empty;
        private int createByUser = -1;
        private Guid pollGuid = Guid.Empty;
        private string nameUser = string.Empty;
        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        public string Opinion
        {
            get { return opinion; }
            set { opinion = value; }
        }
        public int CreateByUser
        {
            get { return createByUser; }
            set { createByUser = value; }
        }
        public Guid PollGuid
        {
            get { return pollGuid; }
            set { pollGuid = value; }
        }
        public string NameUser
        {
            get { return nameUser; }
            set { nameUser = value; }
        }

        #endregion
        #region Private Methods

        /// <summary>
        /// Gets an instance of PollOpinion.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetPollOpinion(
            int itemID)
        {
            using (IDataReader reader = DBPollOpinion.GetOne(
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
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.opinion = reader["Opinion"].ToString();
                this.createByUser = Convert.ToInt32(reader["CreateByUser"]);
                this.pollGuid = new Guid(reader["PollGuid"].ToString());

            }

        }
        /// <summary>
        /// Persists a new instance of PollOpinion. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBPollOpinion.Create(
                this.siteID,
                this.opinion,
                this.createByUser,
                this.pollGuid);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of PollOpinion. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBPollOpinion.Update(
                this.itemID,
                this.siteID,
                this.opinion,
                this.createByUser,
                this.pollGuid);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of PollOpinion. Returns true on success.
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
        /// Deletes an instance of PollOpinion. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBPollOpinion.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of PollOpinion. 
        /// </summary>
        public static int GetCount()
        {
            return DBPollOpinion.GetCount();
        }

        private static List<PollOpinion> LoadListFromReader(IDataReader reader)
        {
            List<PollOpinion> pollOpinionList = new List<PollOpinion>();
            try
            {
                while (reader.Read())
                {
                    PollOpinion pollOpinion = new PollOpinion();
                    pollOpinion.itemID = Convert.ToInt32(reader["ItemID"]);
                    pollOpinion.siteID = Convert.ToInt32(reader["SiteID"]);
                    pollOpinion.opinion = reader["Opinion"].ToString();
                    pollOpinion.createByUser = Convert.ToInt32(reader["CreateByUser"]);
                    pollOpinion.pollGuid = new Guid(reader["PollGuid"].ToString());
                    pollOpinionList.Add(pollOpinion);

                }
            }
            finally
            {
                reader.Close();
            }

            return pollOpinionList;

        }
        private static List<PollOpinion> LoadListFromReader2(IDataReader reader)
        {
            List<PollOpinion> pollOpinionList = new List<PollOpinion>();
            try
            {
                while (reader.Read())
                {
                    PollOpinion pollOpinion = new PollOpinion();
                    pollOpinion.itemID = Convert.ToInt32(reader["ItemID"]);
                    pollOpinion.siteID = Convert.ToInt32(reader["SiteID"]);
                    pollOpinion.opinion = reader["Opinion"].ToString();
                    pollOpinion.createByUser = Convert.ToInt32(reader["CreateByUser"]);
                    pollOpinion.pollGuid = new Guid(reader["PollGuid"].ToString());
                    pollOpinion.nameUser = reader["NameUser"].ToString();
                    pollOpinionList.Add(pollOpinion);

                }
            }
            finally
            {
                reader.Close();
            }

            return pollOpinionList;

        }
        public static List<PollOpinion> GetByPoll(Guid pollGuid)
        {
            IDataReader reader = DBPollOpinion.GetByPollGuid(pollGuid);
            return LoadListFromReader2(reader);
        }
        /// <summary>
        /// Gets an IList with all instances of PollOpinion.
        /// </summary>
        public static List<PollOpinion> GetAll()
        {
            IDataReader reader = DBPollOpinion.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of PollOpinion.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<PollOpinion> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBPollOpinion.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of PollOpinion.
        /// </summary>
        public static int CompareByItemID(PollOpinion pollOpinion1, PollOpinion pollOpinion2)
        {
            return pollOpinion1.ItemID.CompareTo(pollOpinion2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of PollOpinion.
        /// </summary>
        public static int CompareBySiteID(PollOpinion pollOpinion1, PollOpinion pollOpinion2)
        {
            return pollOpinion1.SiteID.CompareTo(pollOpinion2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of PollOpinion.
        /// </summary>
        public static int CompareByOpinion(PollOpinion pollOpinion1, PollOpinion pollOpinion2)
        {
            return pollOpinion1.Opinion.CompareTo(pollOpinion2.Opinion);
        }
        /// <summary>
        /// Compares 2 instances of PollOpinion.
        /// </summary>
        public static int CompareByCreateByUser(PollOpinion pollOpinion1, PollOpinion pollOpinion2)
        {
            return pollOpinion1.CreateByUser.CompareTo(pollOpinion2.CreateByUser);
        }

        #endregion


    }

}





