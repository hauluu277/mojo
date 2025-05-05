using mojoPortal.Features.Data.QLLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Features.Business.QLLog
{
    public class QLLog
    {
        #region Constructors

        public QLLog()
        { }


        public QLLog(
            long itemID)
        {
            GetCoreQLLog(itemID);
        }

        #endregion

        #region Private Properties
        private long itemID;
        public string diaChiIP = string.Empty;
        public string type = string.Empty;
        public string duongDanThaoTac = string.Empty;
        public string hanhDongThaoTac = string.Empty;
        public string noiDung = string.Empty;
        private string createdBy = string.Empty;
        private int createdByUser = -1;
        private DateTime createdDate = DateTime.UtcNow;
        #endregion

        #region Public Properties

        public long ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string DiaChiIP { get { return diaChiIP; } set { diaChiIP = value; } }
        public string Type { get { return type; } set { type = value; } }
        public string DuongDanThaoTac { get { return duongDanThaoTac; } set { duongDanThaoTac = value; } }
        public string HanhDongThaoTac { get { return hanhDongThaoTac; } set { hanhDongThaoTac = value; } }
        public string NoiDung { get { return noiDung; } set { noiDung = value; } }

        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public int CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of core_ThuTuc.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetCoreQLLog(long itemID)
        {
            using (IDataReader reader = DBCoreQLLog.GetOne(itemID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt64(reader["ItemID"]);
                this.hanhDongThaoTac = reader["HanhDongThaoTac"].ToString();
                this.diaChiIP = reader["DiaChiIP"].ToString();
                this.type = reader["Type"].ToString();
                this.duongDanThaoTac = reader["DuongDanThaoTac"].ToString();
                this.noiDung = reader["NoiDung"].ToString();
                this.createdBy = reader["CreatedBy"].ToString();
                this.createdByUser = Convert.ToInt32(reader["CreatedByUser"]);
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
            }

        }

        /// <summary>
        /// Persists a new instance of core_ThuTuc. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCoreQLLog.Create(
                this.diaChiIP,
                this.type,
                this.hanhDongThaoTac,
                this.noiDung,
                this.duongDanThaoTac,
                this.createdBy,
                this.createdByUser,
                this.createdDate);

            this.itemID = newID;

            return (newID > 0);

        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of core_ThuTuc. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            return Create();
        }

        #endregion
    }
}
