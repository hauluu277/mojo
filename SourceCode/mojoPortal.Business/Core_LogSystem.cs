
// Author:					Mr Hậu
// Created:					2021-1-20
// Last Modified:			2021-1-20
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

    public class core_LogSystemMojo
    {

        #region Constructors

        public core_LogSystemMojo()
        { }


        public core_LogSystemMojo(
            long itemID)
        {
            Getcore_LogSystem(
                itemID);
        }

        #endregion

        #region Private Properties

        private long itemID;
        private int userID = -1;
        private DateTime startLogin = DateTime.Now;
        private DateTime endLogin = DateTime.Now;
        private int countLogin = -1;
        private DateTime createdDate = DateTime.Now;
        private string fullName = string.Empty;
        private string loginName = string.Empty;

        #endregion

        #region Public Properties
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public long ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public DateTime StartLogin
        {
            get { return startLogin; }
            set { startLogin = value; }
        }
        public DateTime EndLogin
        {
            get { return endLogin; }
            set { endLogin = value; }
        }
        public int CountLogin
        {
            get { return countLogin; }
            set { countLogin = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of core_LogSystem.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getcore_LogSystem(
            long itemID)
        {
            using (IDataReader reader = DBcore_LogSystem.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt64(reader["ItemID"]);
                this.userID = Convert.ToInt32(reader["UserID"]);
                this.startLogin = Convert.ToDateTime(reader["StartLogin"]);
                this.endLogin = Convert.ToDateTime(reader["EndLogin"]);
                this.countLogin = Convert.ToInt32(reader["CountLogin"]);
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);

            }

        }

        /// <summary>
        /// Persists a new instance of core_LogSystem. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBcore_LogSystem.Create(
                this.userID,
                this.startLogin,
                this.endLogin,
                this.countLogin,
                this.createdDate);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of core_LogSystem. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBcore_LogSystem.Update(
                this.itemID,
                this.userID,
                this.startLogin,
                this.endLogin,
                this.countLogin,
                this.createdDate);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of core_LogSystem. Returns true on success.
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
        /// Deletes an instance of core_LogSystem. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            return DBcore_LogSystem.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of core_LogSystem. 
        /// </summary>


        private static List<core_LogSystemMojo> LoadListFromReader(IDataReader reader)
        {
            List<core_LogSystemMojo> core_LogSystemList = new List<core_LogSystemMojo>();
            try
            {
                while (reader.Read())
                {
                    core_LogSystemMojo core_LogSystem = new core_LogSystemMojo();
                    core_LogSystem.itemID = Convert.ToInt64(reader["ItemID"]);
                    core_LogSystem.userID = Convert.ToInt32(reader["UserID"]);
                    core_LogSystem.startLogin = Convert.ToDateTime(reader["StartLogin"]);
                    core_LogSystem.endLogin = Convert.ToDateTime(reader["EndLogin"]);
                    core_LogSystem.countLogin = Convert.ToInt32(reader["CountLogin"]);
                    core_LogSystem.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    core_LogSystem.fullName = reader["FullName"].ToString();
                    core_LogSystem.loginName = reader["LoginName"].ToString();
                    core_LogSystemList.Add(core_LogSystem);

                }
            }
            finally
            {
                reader.Close();
            }

            return core_LogSystemList;

        }

        /// <summary>
        /// Gets an IList with all instances of core_LogSystem.
        /// </summary>
        public static List<core_LogSystemMojo> GetAll()
        {
            IDataReader reader = DBcore_LogSystem.GetAll();
            return LoadListFromReader(reader);

        }


        public static core_LogSystemMojo GetByUser(int userId)
        {
            using (IDataReader reader = DBcore_LogSystem.GetByUser(userId))
            {
                if (reader.Read())
                {
                    core_LogSystemMojo coreLogSystem = new core_LogSystemMojo();
                    coreLogSystem.itemID = Convert.ToInt64(reader["ItemID"]);
                    coreLogSystem.userID = Convert.ToInt32(reader["UserID"]);
                    coreLogSystem.startLogin = Convert.ToDateTime(reader["StartLogin"]);
                    coreLogSystem.endLogin = Convert.ToDateTime(reader["EndLogin"]);
                    coreLogSystem.countLogin = Convert.ToInt32(reader["CountLogin"]);
                    coreLogSystem.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    return coreLogSystem;
                }
            }

            return null;

        }

        /// <summary>
        /// Gets an IList with page of instances of core_LogSystem.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<core_LogSystemMojo> GetPage(string loginName,
            string fullName,
            DateTime? dateFrom,
            DateTime? dateTo,
            DateTime? startLoginFrom,
            DateTime? startLoginTo,
            DateTime? endLoginFrom,
            DateTime? endLoginTo,
            int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBcore_LogSystem.GetPage(loginName, fullName,
                dateFrom, dateTo, startLoginFrom, startLoginTo,
                endLoginFrom, endLoginTo,
                pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of core_LogSystem.
        /// </summary>
        public static int CompareByUserID(core_LogSystemMojo core_LogSystem1, core_LogSystemMojo core_LogSystem2)
        {
            return core_LogSystem1.UserID.CompareTo(core_LogSystem2.UserID);
        }
        /// <summary>
        /// Compares 2 instances of core_LogSystem.
        /// </summary>
        public static int CompareByStartLogin(core_LogSystemMojo core_LogSystem1, core_LogSystemMojo core_LogSystem2)
        {
            return core_LogSystem1.StartLogin.CompareTo(core_LogSystem2.StartLogin);
        }
        /// <summary>
        /// Compares 2 instances of core_LogSystem.
        /// </summary>
        public static int CompareByEndLogin(core_LogSystemMojo core_LogSystem1, core_LogSystemMojo core_LogSystem2)
        {
            return core_LogSystem1.EndLogin.CompareTo(core_LogSystem2.EndLogin);
        }
        /// <summary>
        /// Compares 2 instances of core_LogSystem.
        /// </summary>
        public static int CompareByCountLogin(core_LogSystemMojo core_LogSystem1, core_LogSystemMojo core_LogSystem2)
        {
            return core_LogSystem1.CountLogin.CompareTo(core_LogSystem2.CountLogin);
        }
        /// <summary>
        /// Compares 2 instances of core_LogSystem.
        /// </summary>
        public static int CompareByCreatedDate(core_LogSystemMojo core_LogSystem1, core_LogSystemMojo core_LogSystem2)
        {
            return core_LogSystem1.CreatedDate.CompareTo(core_LogSystem2.CreatedDate);
        }

        #endregion


    }

}





