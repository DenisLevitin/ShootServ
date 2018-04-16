namespace BO
{
    /// <summary>
    /// Результат выполнения метода, возвращающий тип значение
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultInfoStruct<T> where T : struct
    {
        public ResultInfoStruct()
        {
            Result = new ResultInfo();
            Data = default(T);
        }

        public ResultInfo Result;

        public T Data;
    }
}
