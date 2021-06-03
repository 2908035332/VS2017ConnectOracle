using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOracle
{
    using System.Data;
    using Oracle.ManagedDataAccess.Client;
    using System.Configuration;

    internal static class OracleDBHelper
    {
        /// <summary>
        /// 私有属性用于获取Oracle连接字符串
        /// </summary>
        private static string getOracle { get; set; }

        /// <summary>
        /// 获取Oracle连接字符串
        /// </summary>
        static OracleDBHelper()
        {
            getOracle = ConfigurationManager.ConnectionStrings["GetOracle"].ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OracleQuery">Oracle SQL语句</param>
        /// <param name="par">参数化数组(可为null)</param>
        /// <returns></returns>
        public static OracleDataReader OracleExecuteReader(string OracleQuery, params OracleParameter[] par)
        {
            OracleConnection conn = new OracleConnection(getOracle);
            conn.Open();
            OracleCommand cmd = new OracleCommand(OracleQuery, conn);
            if (par != null && par.Length > 0)
            {
                cmd.Parameters.AddRange(par);
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 非查询方法
        /// </summary>
        /// <param name="OracleQuery">Oracle SQL语句</param>
        /// <param name="par">参数化数组(可为null)</param>
        /// <returns></returns>
        public static int OracleExecuteNonQuery(string OracleQuery, params OracleParameter[] par)
        {
            using (OracleConnection conn = new OracleConnection(getOracle))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(OracleQuery, conn);
                if (par != null && par.Length > 0)
                {
                    cmd.Parameters.AddRange(par);
                }
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OracleQuery">Oracle SQL语句</param>
        /// <param name="par">参数化数组(可为null)</param>
        /// <returns></returns>
        public static object OracleExecuteScalar(string OracleQuery, params OracleParameter[] par)
        {
            using (OracleConnection conn = new OracleConnection(getOracle))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(OracleQuery, conn);
                if (par.Length > 0 && par != null)
                {
                    cmd.Parameters.AddRange(par);
                }
                return cmd.ExecuteScalar();
            }
        }
    }
}
