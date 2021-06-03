using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceOracle
{
    using ModelOracle;
    public interface IMyTest
    {

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        List<MyTest> SelectMyTest();

        /// <summary>
        /// 向表添加数据
        /// </summary>
        /// <param name="myTest"></param>
        /// <returns></returns>
        bool Insert(MyTest myTest);

        /// <summary>
        /// 编号查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MyTest SelectByID(int id = -1);

        /// <summary>
        /// 根据编号修改 
        /// </summary>
        /// <param name="myTest"></param>
        /// <returns></returns>
        bool Updata(MyTest myTest);

        /// <summary>
        /// 根据编号删除 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id = -1);

        /// <summary>
        /// 条件查询所有信息
        /// </summary>
        /// <returns></returns>
        List<MyTest> SelectWhereMyTest(string name,DateTime? StartTime,DateTime? EndTime);
    }
}
