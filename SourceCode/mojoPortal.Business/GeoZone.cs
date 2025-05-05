// Author:					Joe Audette
// Created:				    2007-02-16
// Last Modified:			2009-02-01
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using System.Data;
using mojoPortal.Data;
using System.Collections.Generic;

namespace mojoPortal.Business
{
    /// <summary>
    /// Represents a State within a Country
    /// </summary>
    public class GeoZone
    {

        #region Constructors

        public GeoZone()
        { }

        public GeoZone(Guid guid)
        {
            GetGeoZone(guid);
        }

        #endregion

        #region Private Properties

        private Guid guid = Guid.Empty;
        private Guid countryGuid = Guid.Empty;
        private string name;
        private string code;
        private bool is_Weather = false;
        private int orderBy = 0;

        #endregion

        #region Public Properties

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        public Guid CountryGuid
        {
            get { return countryGuid; }
            set { countryGuid = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public bool Is_Weather
        {
            get { return is_Weather; }
            set { is_Weather = value; }
        }
        public int OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }
        #endregion

        #region Private Methods

        private void GetGeoZone(Guid guid)
        {
            using (IDataReader reader = DBGeoZone.GetOne(guid))
            {
                LoadFromReader(this, reader);
            }

        }

        private static void LoadFromReader(GeoZone geoZone, IDataReader reader)
        {
            if (reader.Read())
            {
                geoZone.guid = new Guid(reader["Guid"].ToString());
                geoZone.countryGuid = new Guid(reader["CountryGuid"].ToString());
                geoZone.name = reader["Name"].ToString();
                geoZone.code = reader["Code"].ToString();
                if (reader["Is_Weather"] != DBNull.Value)
                {
                    geoZone.is_Weather = bool.Parse(reader["Is_Weather"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["OrderBy"].ToString()))
                {
                    geoZone.orderBy = Convert.ToInt32(reader["OrderBy"].ToString());
                }
            }

        }
        private bool Create()
        {

            this.guid = Guid.NewGuid();

            int rowsAffected = DBGeoZone.Create(
                this.guid,
                this.countryGuid,
                this.name,
                this.code,
                this.is_Weather,
                this.orderBy);

            return (rowsAffected > 0);

        }

        private bool Update()
        {

            return DBGeoZone.Update(
                this.guid,
                this.countryGuid,
                this.name,
                this.code,
                this.is_Weather,
                this.orderBy);

        }


        #endregion

        #region Public Methods


        public bool Save()
        {
            if ((this.guid != null) && (this.guid != Guid.Empty))
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

        public static bool Delete(Guid guid)
        {
            return DBGeoZone.Delete(guid);
        }


        public static IDataReader GetByCountry(Guid countryGuid)
        {
            return DBGeoZone.GetByCountry(countryGuid);
        }
        public static List<GeoZone> GetByCountryIsWeather(Guid countryGuid)
        {
            List<GeoZone> GeoZoneyCollection
              = new List<GeoZone>();

            using (IDataReader reader = DBGeoZone.GetByCountry(countryGuid))
            {
                while (reader.Read())
                {
                    GeoZone geoZone = new GeoZone();
                    geoZone.guid = new Guid(reader["Guid"].ToString());
                    geoZone.countryGuid = new Guid(reader["CountryGuid"].ToString());
                    geoZone.name = reader["Name"].ToString();
                    geoZone.code = reader["Code"].ToString();
                    if (reader["Is_Weather"] != DBNull.Value)
                    {
                        geoZone.is_Weather = bool.Parse(reader["Is_Weather"].ToString());
                    }
                    else
                    {
                        geoZone.Is_Weather = false;
                    }
                    if (!string.IsNullOrEmpty(reader["OrderBy"].ToString()))
                    {
                        geoZone.orderBy = Convert.ToInt32(reader["OrderBy"].ToString());
                    }
                    GeoZoneyCollection.Add(geoZone);
                }
            }
            return GeoZoneyCollection;
        }
        public static GeoZone GetByCode(string code)
        {
            GeoZone geoZone = new GeoZone();
            using (IDataReader reader = DBGeoZone.GetByCode2(code))
            {
                LoadFromReader(geoZone, reader);
            }
            return geoZone;
        }
        public static GeoZone GetByCode(Guid countryGuid, string code)
        {
            GeoZone geoZone = new GeoZone();
            using (IDataReader reader = DBGeoZone.GetByCode(countryGuid, code))
            {
                LoadFromReader(geoZone, reader);
            }

            if (geoZone.Guid == Guid.Empty)
            {
                return null;
            }
            return geoZone;


        }

        //public static DataTable GetPage(int pageNumber, int pageSize)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("Guid",typeof(Guid));
        //    dataTable.Columns.Add("CountryGuid",typeof(Guid));
        //    dataTable.Columns.Add("CountryName", typeof(string));
        //    dataTable.Columns.Add("Name",typeof(string));
        //    dataTable.Columns.Add("Code",typeof(string));
        //    dataTable.Columns.Add("TotalPages", typeof(int));

        //    IDataReader reader = DBGeoZone.GetGeoZonePage(pageNumber, pageSize);	
        //    while (reader.Read())
        //    {
        //        DataRow row = dataTable.NewRow();
        //        row["Guid"] = reader["Guid"];
        //        row["CountryGuid"] = reader["CountryGuid"];
        //        row["CountryName"] = reader["CountryName"];
        //        row["Name"] = reader["Name"];
        //        row["Code"] = reader["Code"];
        //        row["TotalPages"] = Convert.ToInt32(reader["TotalPages"]);
        //        dataTable.Rows.Add(row);
        //    }

        //    reader.Close();

        //    return dataTable;

        //}

        public static DataTable GetPage(
            Guid countryGuid,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Guid", typeof(Guid));
            dataTable.Columns.Add("CountryGuid", typeof(Guid));
            dataTable.Columns.Add("CountryName", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Code", typeof(string));
            dataTable.Columns.Add("Is_Weather", typeof(bool));
            dataTable.Columns.Add("OrderBy", typeof(int));
            //dataTable.Columns.Add("TotalPages", typeof(int));

            using (IDataReader reader = DBGeoZone.GetPage(countryGuid, pageNumber, pageSize, out totalPages))
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["Guid"] = new Guid(reader["Guid"].ToString());
                    row["CountryGuid"] = reader["CountryGuid"];
                    row["CountryName"] = reader["CountryName"];
                    row["Name"] = reader["Name"];
                    row["Code"] = reader["Code"];
                    if (reader["Is_Weather"] != DBNull.Value)
                    {
                        row["Is_Weather"] = bool.Parse(reader["Is_Weather"].ToString());
                    }
                    else
                    {
                        row["Is_Weather"] = false;
                    }
                    if (!string.IsNullOrEmpty(reader["OrderBy"].ToString()))
                    {
                        row["OrderBy"] = Convert.ToInt32(reader["OrderBy"].ToString());
                    }
                    //row["TotalPages"] = Convert.ToInt32(reader["TotalPages"]);
                    dataTable.Rows.Add(row);
                }

            }

            return dataTable;

        }



        #endregion


    }

}
