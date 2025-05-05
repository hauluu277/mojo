
// Author:					Manh Dtr
// Created:					2020-1-6
// Last Modified:			2020-1-6
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
	
	public class md_ImportantDate
	{

#region Constructors

	    public md_ImportantDate()
		{}
	    
	
	    public md_ImportantDate(
			int itemID) 
		{
	        Getmd_ImportantDate(
				itemID); 
	    }

#endregion

#region Private Properties

		private int itemID = -1;
		private int siteID = -1;
		private int pageID = -1;
		private int moduleID = -1;
		private string titleImportant = string.Empty;
		private DateTime? dateImportant1 = null;
		private DateTime? dateImportant2 = null;
		private DateTime? dateImportant3 = null;
		private DateTime? dateImportant4 = null;
		
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
		public int PageID 
		{
			get { return pageID; }
			set { pageID = value; }
		}
		public int ModuleID 
		{
			get { return moduleID; }
			set { moduleID = value; }
		}
		public string TitleImportant 
		{
			get { return titleImportant; }
			set { titleImportant = value; }
		}
		public DateTime? DateImportant1 
		{
			get { return dateImportant1; }
			set { dateImportant1 = value; }
		}
		public DateTime? DateImportant2 
		{
			get { return dateImportant2; }
			set { dateImportant2 = value; }
		}
		public DateTime? DateImportant3 
		{
			get { return dateImportant3; }
			set { dateImportant3 = value; }
		}
		public DateTime? DateImportant4 
		{
			get { return dateImportant4; }
			set { dateImportant4 = value; }
		}

#endregion

#region Private Methods

		/// <summary>
		/// Gets an instance of md_ImportantDate.
		/// </summary>
		/// <param name="itemID"> itemID </param>
		private void Getmd_ImportantDate(
			int itemID) 
		{
			using(IDataReader reader = DBmd_ImportantDate.GetOne(
				itemID)) 
			{
			PopulateFromReader(reader);
			}
			
		}
		
		
		private void PopulateFromReader(IDataReader reader) 
		{
			if(reader.Read())
			{
				this.itemID = Convert.ToInt32(reader["ItemID"]);
				this.siteID = Convert.ToInt32(reader["SiteID"]);
				this.pageID = Convert.ToInt32(reader["PageID"]);
				this.moduleID = Convert.ToInt32(reader["ModuleID"]);
				this.titleImportant = reader["TitleImportant"].ToString();
                if (!string.IsNullOrEmpty(reader["DateImportant1"].ToString()))
                {
                    this.dateImportant1 = Convert.ToDateTime(reader["DateImportant1"]);
                }
                if (!string.IsNullOrEmpty(reader["DateImportant2"].ToString()))
                {
                    this.dateImportant2 = Convert.ToDateTime(reader["DateImportant2"]);
                }
                if (!string.IsNullOrEmpty(reader["DateImportant3"].ToString()))
                {
                    this.dateImportant3 = Convert.ToDateTime(reader["DateImportant3"]);
                }
                if (!string.IsNullOrEmpty(reader["DateImportant4"].ToString()))
                {
                    this.dateImportant4 = Convert.ToDateTime(reader["DateImportant4"]);
                }
               
			
			}
			
		}
		
		/// <summary>
        /// Persists a new instance of md_ImportantDate. Returns true on success.
        /// </summary>
        /// <returns></returns>
		private bool Create()
		{ 
			int newID = 0;
			
			newID = DBmd_ImportantDate.Create(
				this.siteID, 
				this.pageID, 
				this.moduleID, 
				this.titleImportant, 
				this.dateImportant1, 
				this.dateImportant2, 
				this.dateImportant3, 
				this.dateImportant4); 
				
			this.itemID = newID;
					
			return (newID > 0);

		}

		
		/// <summary>
        /// Updates this instance of md_ImportantDate. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
		private bool Update()
		{

			return DBmd_ImportantDate.Update(
				this.itemID, 
				this.siteID, 
				this.pageID, 
				this.moduleID, 
				this.titleImportant, 
				this.dateImportant1, 
				this.dateImportant2, 
				this.dateImportant3, 
				this.dateImportant4); 
				
		}
		
		
		


#endregion

#region Public Methods

		/// <summary>
        /// Saves this instance of md_ImportantDate. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
		public bool Save()
		{
			if( this.itemID > 0)
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
		/// Deletes an instance of md_ImportantDate. Returns true on success.
		/// </summary>
		/// <param name="itemID"> itemID </param>
		/// <returns>bool</returns>
		public static bool Delete(
			int  itemID) 
		{
			return DBmd_ImportantDate.Delete(
				itemID); 
		}
		
		
		/// <summary>
		/// Gets a count of md_ImportantDate. 
		/// </summary>
		public static int GetCount()
		{
			return DBmd_ImportantDate.GetCount();
		}
	
		private static List<md_ImportantDate> LoadListFromReader(IDataReader reader)
		{
			List<md_ImportantDate> md_ImportantDateList = new List<md_ImportantDate>();
			try
			{
				while (reader.Read())
				{
					md_ImportantDate md_ImportantDate = new md_ImportantDate();
					md_ImportantDate.itemID = Convert.ToInt32(reader["ItemID"]);
					md_ImportantDate.siteID = Convert.ToInt32(reader["SiteID"]);
					md_ImportantDate.pageID = Convert.ToInt32(reader["PageID"]);
					md_ImportantDate.moduleID = Convert.ToInt32(reader["ModuleID"]);
					md_ImportantDate.titleImportant = reader["TitleImportant"].ToString();
                    if (!string.IsNullOrEmpty(reader["DateImportant1"].ToString()))
                    {
                        md_ImportantDate.dateImportant1 = Convert.ToDateTime(reader["DateImportant1"]);
                    }
                    if (!string.IsNullOrEmpty(reader["DateImportant2"].ToString()))
                    {
                        md_ImportantDate.dateImportant2 = Convert.ToDateTime(reader["DateImportant2"]);
                    }
                    if (!string.IsNullOrEmpty(reader["DateImportant3"].ToString()))
                    {
                        md_ImportantDate.dateImportant3 = Convert.ToDateTime(reader["DateImportant3"]);
                    }
                    if (!string.IsNullOrEmpty(reader["DateImportant4"].ToString()))
                    {
                        md_ImportantDate.dateImportant4 = Convert.ToDateTime(reader["DateImportant4"]);
                    }
                    md_ImportantDateList.Add(md_ImportantDate);
					
				}
			}
			finally
			{
				reader.Close();
			}
	
			return md_ImportantDateList;
		
		}
	
		/// <summary>
		/// Gets an IList with all instances of md_ImportantDate.
		/// </summary>
		public static List<md_ImportantDate> GetAll()
		{
			IDataReader reader = DBmd_ImportantDate.GetAll();
			return LoadListFromReader(reader);
	
		}
        public static List<md_ImportantDate> GetTopHot(int top, int siteId)
        {
            IDataReader reader = DBmd_ImportantDate.GetTopHot(top, siteId);
            return LoadListFromReader(reader);
        }
        /// <summary>
        /// Gets an IList with page of instances of md_ImportantDate.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<md_ImportantDate> GetPage(int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			IDataReader reader = DBmd_ImportantDate.GetPage(pageNumber, pageSize, out totalPages);	
			return LoadListFromReader(reader);
		}


        public static List<md_ImportantDate> GetPage(DateTime? dateImportant1, DateTime? dateImportant2, DateTime? dateImportant3, DateTime? dateImportant4, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_ImportantDate.GetPage(dateImportant1, dateImportant2, dateImportant3, dateImportant4, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }


        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_ImportantDate.
        /// </summary>
        public static int CompareByItemID(md_ImportantDate md_ImportantDate1, md_ImportantDate md_ImportantDate2)
		{
			return md_ImportantDate1.ItemID.CompareTo(md_ImportantDate2.ItemID);
		}	
		/// <summary>
		/// Compares 2 instances of md_ImportantDate.
		/// </summary>
		public static int CompareBySiteID(md_ImportantDate md_ImportantDate1, md_ImportantDate md_ImportantDate2)
		{
			return md_ImportantDate1.SiteID.CompareTo(md_ImportantDate2.SiteID);
		}	
		/// <summary>
		/// Compares 2 instances of md_ImportantDate.
		/// </summary>
		public static int CompareByPageID(md_ImportantDate md_ImportantDate1, md_ImportantDate md_ImportantDate2)
		{
			return md_ImportantDate1.PageID.CompareTo(md_ImportantDate2.PageID);
		}	
		/// <summary>
		/// Compares 2 instances of md_ImportantDate.
		/// </summary>
		public static int CompareByModuleID(md_ImportantDate md_ImportantDate1, md_ImportantDate md_ImportantDate2)
		{
			return md_ImportantDate1.ModuleID.CompareTo(md_ImportantDate2.ModuleID);
		}	
		/// <summary>
		/// Compares 2 instances of md_ImportantDate.
		/// </summary>
		public static int CompareByTitleImportant(md_ImportantDate md_ImportantDate1, md_ImportantDate md_ImportantDate2)
		{
			return md_ImportantDate1.TitleImportant.CompareTo(md_ImportantDate2.TitleImportant);
		}	
		/// <summary>
		/// Compares 2 instances of md_ImportantDate.
		/// </summary>
		public static int CompareByDateImportant1(md_ImportantDate md_ImportantDate1, md_ImportantDate md_ImportantDate2)
		{
			return md_ImportantDate1.DateImportant1.Value.CompareTo(md_ImportantDate2.DateImportant1);
		}	
		/// <summary>
		/// Compares 2 instances of md_ImportantDate.
		/// </summary>
		public static int CompareByDateImportant2(md_ImportantDate md_ImportantDate1, md_ImportantDate md_ImportantDate2)
		{
			return md_ImportantDate1.DateImportant2.Value.CompareTo(md_ImportantDate2.DateImportant2);
		}	
		/// <summary>
		/// Compares 2 instances of md_ImportantDate.
		/// </summary>
		public static int CompareByDateImportant3(md_ImportantDate md_ImportantDate1, md_ImportantDate md_ImportantDate2)
		{
			return md_ImportantDate1.DateImportant3.Value.CompareTo(md_ImportantDate2.DateImportant3);
		}	
		/// <summary>
		/// Compares 2 instances of md_ImportantDate.
		/// </summary>
		public static int CompareByDateImportant4(md_ImportantDate md_ImportantDate1, md_ImportantDate md_ImportantDate2)
		{
			return md_ImportantDate1.DateImportant4.Value.CompareTo(md_ImportantDate2.DateImportant4);
		}	

#endregion


	}
	
}





