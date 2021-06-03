using InterfaceOracle;
using ModelOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceOracle
{

    public class SMyTest : IMyTest
    {
        private int id => GetID();

        /// <summary>
        /// 向表添加数据
        /// </summary>
        /// <param name="myTest"></param>
        /// <returns></returns>
        public bool Insert(MyTest myTest)
        {
            string queryOracleSql = "insert into MyTest (id,name,time)values(:id,:name,:time)";
            OracleParameter[] Opar = new OracleParameter[] {
                new OracleParameter(":id",id),
                new OracleParameter(":name",myTest.name),
                new OracleParameter(":time",myTest.time),
            };
            return OracleDBHelper.OracleExecuteNonQuery(queryOracleSql, Opar) > 0;
        }

        private int GetID()
        {
            Random random = new Random();
            int id = random.Next();
            if (GetByID(id) == false)
            {
                GetByID();
            }
            return id;
        }

        private bool GetByID(int id = -1)
        {
            StringBuilder queryOracleSql = new StringBuilder(" SELECT COUNT(id) FROM MyTest WHERE 1=1 ");
            if (id > 0)
            {
                queryOracleSql.Append(" AND id=:id ");
                return Convert.ToInt32(OracleDBHelper.OracleExecuteScalar(queryOracleSql.ToString(), new OracleParameter(":id", id))) > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询表数据
        /// </summary>
        /// <returns></returns>
        public List<MyTest> SelectMyTest()
        {
            List<MyTest> myTests = new List<MyTest>();
            string queryOracleSql = "SELECT * FROM MyTest ORDER BY Time ASC";
            using (OracleDataReader odr = OracleDBHelper.OracleExecuteReader(queryOracleSql))
            {
                while (odr.Read())
                {
                    myTests.Add(new MyTest()
                    {
                        id = odr["id"] == DBNull.Value ? (int?)null : Convert.ToInt32(odr["id"]),
                        name = odr["name"] == DBNull.Value ? null : odr["name"].ToString(),
                        time = odr["time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(odr["time"])
                    });
                }
            }
            return myTests;
        }

        /// <summary>
        /// 根据编号查询表数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MyTest SelectByID(int id)
        {
            MyTest myTest = null;
            StringBuilder queryOracleSql = new StringBuilder("SELECT * FROM MyTest WHERE 1=1");
            if (id > 0)
            {
                queryOracleSql.Append(" AND id=:id");
                OracleParameter[] opar =
                {
                    new OracleParameter(":id",id)
                };
                using (OracleDataReader odr = OracleDBHelper.OracleExecuteReader(queryOracleSql.ToString(), opar))
                {
                    while (odr.Read())
                    {
                        myTest = new MyTest()
                        {
                            id = odr["id"] == DBNull.Value ? (int?)null : Convert.ToInt32(odr["id"]),
                            name = odr["name"] == DBNull.Value ? null : odr["name"].ToString(),
                            time = odr["time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(odr["time"])
                        };
                    }
                }
                return myTest;
            }
            else
            {
                return myTest;
            }
        }

        /// <summary>
        /// 根据编号进行修改
        /// </summary>
        /// <param name="myTest"></param>
        /// <returns></returns>
        public bool Updata(MyTest myTest)
        {
            string queryOracleSql = "update mytest set name=:name,time=:time where id=:id";
            OracleParameter[] opar = {
                new OracleParameter(":name",myTest.name),
                new OracleParameter(":time",myTest.time),
                new OracleParameter(":id",myTest.id),
            };
            return OracleDBHelper.OracleExecuteNonQuery(queryOracleSql, opar) > 0;
        }

        /// <summary>
        /// 根据编号删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id = -1)
        {
            if (id > 0)
            {
                string queryOracleSql = "delete mytest where id=:id";
                return OracleDBHelper.OracleExecuteNonQuery(queryOracleSql, new OracleParameter(":id", id)) > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 条件查询信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="StratTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        public List<MyTest> SelectWhereMyTest(string name, DateTime? StartTime, DateTime? EndTime)
        {
            List<MyTest> myTests = new List<MyTest>();
            StringBuilder queryOracleSql = new StringBuilder(" select * from MyTest where 1=1 ");
            List<OracleParameter> pars = new List<OracleParameter>();
            if (!string.IsNullOrEmpty(name))
            {
                queryOracleSql.Append(" and name like :name ");
                pars.Add(new OracleParameter(":name", '%' + name + "%"));
            }
            if (StartTime != null)
            {
                queryOracleSql.Append(" and time >= :StratTime ");
                pars.Add(new OracleParameter(":StratTime", StartTime));
            }
            if (EndTime != null)
            {
                queryOracleSql.Append(" and time <= :EndTime ");
                pars.Add(new OracleParameter(":StratTime", EndTime));
            }
            using (OracleDataReader odr = OracleDBHelper.OracleExecuteReader(queryOracleSql.ToString(), pars.ToArray()))
            {
                while (odr.Read())
                {
                    myTests.Add(new MyTest()
                    {
                        id = odr["id"] == DBNull.Value ? (int?)null : Convert.ToInt32(odr["id"]),
                        name = odr["name"] == DBNull.Value ? null : odr["name"].ToString(),
                        time = odr["time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(odr["time"])
                    });
                }
            }
            return myTests;
        }
    }
}