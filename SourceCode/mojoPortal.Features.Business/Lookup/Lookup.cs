
// Author:					HiNet
// Created:					2014-9-22
// Last Modified:			2014-9-22
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

    public class Lookup
    {

        public Lookup()
        { }


        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private string name = string.Empty;
        private string description = string.Empty;
        private string interpretation = string.Empty;
        private bool censorship = false;
        private bool isPublic = false;
        private int userCreate = -1;
        private DateTime dateCreate = DateTime.UtcNow;
        private int userApprove = -1;
        private DateTime dateApprove = DateTime.UtcNow;
        private string itemUrl = string.Empty;
        private int pageID = -1;
        private Guid pageGuid = Guid.Empty;

        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Interpretation
        {
            get { return interpretation; }
            set { interpretation = value; }
        }
        public bool Censorship
        {
            get { return censorship; }
            set { censorship = value; }
        }
        public bool IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }
        public int UserCreate
        {
            get { return userCreate; }
            set { userCreate = value; }
        }
        public DateTime DateCreate
        {
            get { return dateCreate; }
            set { dateCreate = value; }
        }
        public int UserApprove
        {
            get { return userApprove; }
            set { userApprove = value; }
        }
        public DateTime DateApprove
        {
            get { return dateApprove; }
            set { dateApprove = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public int PageID
        {
            get { return pageID; }
            set { pageID = value; }
        }
        public Guid PageGuid
        {
            get { return pageGuid; }
            set { pageGuid = value; }
        }

        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByItemID(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.ItemID.CompareTo(lookup2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByModuleID(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.ModuleID.CompareTo(lookup2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareBySiteID(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.SiteID.CompareTo(lookup2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByName(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.Name.CompareTo(lookup2.Name);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByDescription(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.Description.CompareTo(lookup2.Description);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByInterpretation(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.Interpretation.CompareTo(lookup2.Interpretation);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByUserCreate(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.UserCreate.CompareTo(lookup2.UserCreate);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByDateCreate(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.DateCreate.CompareTo(lookup2.DateCreate);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByUserApprove(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.UserApprove.CompareTo(lookup2.UserApprove);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByDateApprove(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.DateApprove.CompareTo(lookup2.DateApprove);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByItemUrl(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.ItemUrl.CompareTo(lookup2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByPageID(Lookup lookup1, Lookup lookup2)
        {
            return lookup1.PageID.CompareTo(lookup2.PageID);
        }

        #endregion


    }

}





