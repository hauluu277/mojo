using mojoPortal.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Features.Data.QLLog
{
    public static class DBCoreQLLog
    {
        /// <summary>
        /// Inserts a row in the core_ThuTuc table. Returns new integer id.
        /// </summary>
        /// <param name="diaChiIP"> diaChiIP </param>
        /// <param name="type"> type </param>
        /// <param name="urlThaoTac"> urlThaoTac </param>
        /// <param name="hanhDongThaoTac"> hanhDongThaoTac </param>
        /// <param name="noiDung"> noiDung </param>
        /// <param name="duongDanThaoTac"> duongDanThaoTac </param>
        /// <param name="createdBy"> createdBy </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="editDate"> editDate </param>
        /// <param name="editByUser"> editByUser </param>
        /// <returns>int</returns>
        public static int Create(
            string diaChiIP,
            string type,
            string hanhDongThaoTac,
            string noiDung,
            string duongDanThaoTac,
            string createdBy,
            int createdByUser,
            DateTime createdDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_QLLog_Insert", 8);
            sph.DefineSqlParameter("@DiaChiIP", SqlDbType.VarChar, 250, ParameterDirection.Input, diaChiIP);
            sph.DefineSqlParameter("@Type", SqlDbType.VarChar, 250, ParameterDirection.Input, type);
            sph.DefineSqlParameter("@HanhDongThaoTac", SqlDbType.NVarChar, 250, ParameterDirection.Input, hanhDongThaoTac);
            sph.DefineSqlParameter("@NoiDung", SqlDbType.NVarChar, 250, ParameterDirection.Input, noiDung);
            sph.DefineSqlParameter("@DuongDanThaoTac", SqlDbType.VarChar, 250, ParameterDirection.Input, duongDanThaoTac);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.NVarChar, 255, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.Int, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the core_ThuTuc table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_QLLog_SelectAll",
                null);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the core_ThuTuc table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_QLLog_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }
    }
}
