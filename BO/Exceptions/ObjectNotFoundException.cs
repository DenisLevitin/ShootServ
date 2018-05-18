using System;

namespace BO.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public string ObjectName { get; }

        public ObjectNotFoundException() : base()
        {

        }

        public ObjectNotFoundException(string objectName) : base(objectName + " doesn't found")
        {
            ObjectName = objectName;
        }
    }
}
