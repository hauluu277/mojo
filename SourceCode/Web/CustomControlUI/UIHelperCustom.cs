// Author:					Joe Audette
// Created:				    2007-05-14
// Last Modified:			2010-02-22
// 
// 07/05/2007  Alexander Yushchenko added confirmation dialog functions
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
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace mojoPortal.Web.Framework
{
    /// <summary>
    /// TinLT: "Use this to set header in Center Column same as Left and Right Column, combo with ModuleTitleControlCustom.cs in mojoPortal.Web/Controls"
    /// </summary>
    public static class UIHelperCustom
    {
        public const string CenterColumnId = "divCenter";
        public const string LeftColumnId = "divLeft";
        public const string RightColumnId = "divRight";


        public const string ArtisteerPostMetaHeader = "art-BlockHeader";
        public const string ArtPostHeader = "art-PostHeader";
        public const string ArtisteerBlockHeader = "art-BlockHeader";

        public const string ArtisteerPost = "art-Post";
        public const string ArtisteerPostContent = "art-BlockContent";
        public const string ArtisteerBlock = "art-Block";
        public const string ArtisteerBlockContent = "art-BlockContent";

        public const string ArtisteerPostMetaHeaderLower = "art-BlockHeader";
        public const string ArtPostHeaderLower = "art-PostHeader";
        public const string ArtisteerBlockHeaderLower = "art-BlockHeader";

        public const string ArtisteerPostLower = "art-post";
        public const string ArtisteerPostContentLower = "art-BlockContent";
        public const string ArtisteerBlockLower = "art-Block";
        public const string ArtisteerBlockContentLower = "art-BlockContent";

    }
}
