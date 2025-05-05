using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using mojoPortal.Data;

namespace mojoPortal.Business
{
    public class CoreCategoryIcon
    {
        #region Constructors

        public CoreCategoryIcon()
        { }


        public CoreCategoryIcon(
            int iconID)
        {
            GetIcon(
                iconID);
        }

        #endregion

        #region Private Properties

        private int iconID = -1;
        private string iconName = string.Empty;
        private string iconUrl = string.Empty;

        #endregion

        #region Public Properties

        public int IconID
        {
            get { return iconID; }
            set { iconID = value; }
        }
        public string IconName
        {
            get { return iconName; }
            set { iconName = value; }
        }
        public string IconUrl
        {
            get { return iconUrl; }
            set { iconUrl = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of Icon.
        /// </summary>
        /// <param name="iconID"> iconID </param>
        public void GetIcon(
            int iconID)
        {
            using (IDataReader reader = DBCoreCategoryIcon.GetOne(
                iconID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                if (reader["IconID"] != DBNull.Value)
                {
                    this.iconID = Convert.ToInt32(reader["IconID"]);
                }
                if (reader["IconName"] != DBNull.Value)
                {
                    this.iconName = reader["IconName"].ToString();
                }
                if (reader["IconUrl"] != DBNull.Value)
                {
                    this.iconUrl = reader["IconUrl"].ToString();
                }

            }

        }

        /// <summary>
        /// Persists a new instance of Icon. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCoreCategoryIcon.Create(
                this.iconName,
                this.iconUrl);

            this.iconID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of Icon. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCoreCategoryIcon.Update(
                this.iconID,
                this.iconName,
                this.iconUrl);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of Icon. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            if (this.iconID > 0)
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
        /// Deletes an instance of Icon. Returns true on success.
        /// </summary>
        /// <param name="iconID"> iconID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int iconID)
        {
            return DBCoreCategoryIcon.Delete(
                iconID);
        }


        /// <summary>
        /// Gets a count of Icon. 
        /// </summary>
        public static int GetCount()
        {
            return DBCoreCategoryIcon.GetCount();
        }

        private static List<CoreCategoryIcon> LoadListFromReader(IDataReader reader)
        {
            List<CoreCategoryIcon> md_IconModuleList = new List<CoreCategoryIcon>();
            try
            {
                while (reader.Read())
                {
                    CoreCategoryIcon md_IconModule = new CoreCategoryIcon();
                    if (reader["IconID"] != DBNull.Value)
                    {
                        md_IconModule.iconID = Convert.ToInt32(reader["IconID"]);
                    }
                    if (reader["IconName"] != DBNull.Value)
                    {

                        md_IconModule.iconName = reader["IconName"].ToString();
                    }
                    if (reader["IconUrl"] != DBNull.Value)
                    {

                        md_IconModule.iconUrl = reader["IconUrl"].ToString();
                    }
                    md_IconModuleList.Add(md_IconModule);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_IconModuleList;

        }

        /// <summary>
        /// Gets an IList with all instances of md_IconModule.
        /// </summary>
        public static List<CoreCategoryIcon> GetAll()
        {
            IDataReader reader = DBCoreCategoryIcon.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<CoreCategoryIcon> GetByName(string iconName)
        {
            IDataReader reader = DBCoreCategoryIcon.GetOneByName(iconName);
            return LoadListFromReader(reader);
        }
        /// <summary>
        /// Gets an IList with page of instances of Icon.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CoreCategoryIcon> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCoreCategoryIcon.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of Icon.
        /// </summary>
        public static int CompareByIconID(CoreCategoryIcon md_IconModule1, CoreCategoryIcon md_IconModule2)
        {
            return md_IconModule1.IconID.CompareTo(md_IconModule2.IconID);
        }
        /// <summary>
        /// Compares 2 instances of md_IconModule.
        /// </summary>
        public static int CompareByIconName(CoreCategoryIcon md_IconModule1, CoreCategoryIcon md_IconModule2)
        {
            return md_IconModule1.IconName.CompareTo(md_IconModule2.IconName);
        }
        /// <summary>
        /// Compares 2 instances of Icon.
        /// </summary>
        public static int CompareByIconUrl(CoreCategoryIcon md_IconModule1, CoreCategoryIcon md_IconModule2)
        {
            return md_IconModule1.IconUrl.CompareTo(md_IconModule2.IconUrl);
        }

        #endregion
    }
}
