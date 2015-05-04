using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GalaSoft.MvvmLight.Messaging;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.Persistence
{
    // Todo change bad naming -Manager
    public class PersistenceManager
    {
        private static volatile PersistenceManager instance;
        private static object syncRoot = new Object();

        public static PersistenceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new PersistenceManager();
                    }
                }

                return instance;
            }
        }
        private PersistenceManager()
        {
            Messenger.Default.Register<LoadRequestMessage>(this, this.Load);
            Messenger.Default.Register<SaveRequestMessage>(this, this.Save);
        }

        private void Save(SaveRequestMessage obj)
        {
              Persistence.Save(obj.ObjectToSave, obj.Type);
        }

        private void Load(LoadRequestMessage obj)
        {
            var loadedObject = Persistence.Load(obj.Type);
           
            Messenger.Default.Send<LoadResponseMessage>(new LoadResponseMessage()
            {
                LoadType = obj.Type,
                MyProperty = loadedObject
            });
        }

        public void Init()
        {
             
        }
    }



    public class Persistence
    {
        public static bool TrySave<T>(T toSave)
        {
            try
            {
                Save(toSave);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static void Save<T>(T toSave)
        {
            var path = GetPath(typeof (T));
            var dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var serialzer = new XmlSerializer(typeof (T));
            using (var fs = new FileStream(path, FileMode.Create))
            {
                serialzer.Serialize(fs, toSave);
            }
        }

        public static void Save(object toSave, Type type)
        {
            var path = GetPath(type);
            var dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var serialzer = new XmlSerializer(type);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                serialzer.Serialize(fs, toSave);
            }
        }

        public static bool TryLoad<T>(out T result)
        {
            try
            {
                result = Load<T>();
            }
            catch (Exception e)
            {
                result = default(T);
                return false;
            }
            return !Equals(result, default(T));
        }

        public static T Load<T>()
        {
            var path = GetPath(typeof (T));

            if (!File.Exists(path))
                return default(T);

            var serialzer = new XmlSerializer(typeof (T));

            T result;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                result = (T) serialzer.Deserialize(fs);
            }

            return result;
        }

        public static object Load(Type type)
        {
            var path = GetPath(type);

            if (!File.Exists(path))
                return null;

            var serialzer = new XmlSerializer(type);

            object result;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                result = serialzer.Deserialize(fs);
            }

            return result;
        }

        private static string GetPath(Type type)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            var typeName = type.Name + ".xml";

            var result = Path.Combine(appDataPath, name, typeName);
            return result;
        }
    }
}