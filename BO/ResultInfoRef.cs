using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Результат выполнения метода, возвращающий ссылочный тип
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultInfoRef<T> where T : new()
    {
        public ResultInfoRef()
        {
            Result = new ResultInfo();
            Data = new T();
        }

        public ResultInfo Result;

        public T Data;
    }
}
