using System;

namespace QrCodeWinClient.Common
{
    public class LoadRequestMessage
    {
        public LoadRequestMessage(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }
    }
    public class LoadResponseMessage
    {
        public object MyProperty { get; set; }
        public Type LoadType { get; set; }
    }

 

    public class SaveRequestMessage
    {
        public SaveRequestMessage(object objectToSave, Type type)
        {
            this.ObjectToSave = objectToSave;
            this.Type = type;
        }

        public Type Type { get; private set; }

        public object ObjectToSave { get; private set; }
    }

    public class LoadResponseMessage2<T>
    {
        public T MyProperty { get; set; }
    }
}