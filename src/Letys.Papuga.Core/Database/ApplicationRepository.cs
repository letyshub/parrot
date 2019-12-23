using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace Letys.Parrot.Core
{
    public class ApplicationRepository : IApplicationRepository
    {
        public void Insert<T>(T item)
        {
            using (var db = new LiteDatabase(ApplicationConfiguration.ConnectionString))
            {
                var collection = db.GetCollection<T>(typeof(T).Name);
                collection.Insert(item);
            }
        }

        public void Update<T>(T item)
        {
            using (var db = new LiteDatabase(ApplicationConfiguration.ConnectionString))
            {
                var collection = db.GetCollection<T>(typeof(T).Name);
                collection.Update(item);
            }
        }

        public void Delete<T>(int id) where T : BaseObject
        {
            using (var db = new LiteDatabase(ApplicationConfiguration.ConnectionString))
            {
                var collection = db.GetCollection<T>(typeof(T).Name);
                collection.Delete(x => x.Id == id);
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            using (var db = new LiteDatabase(ApplicationConfiguration.ConnectionString))
            {
                var collection = db.GetCollection<T>(typeof(T).Name);
                return collection.FindAll();
            }
        }

        public T GetById<T>(int id) where T : BaseObject
        {
            using (var db = new LiteDatabase(ApplicationConfiguration.ConnectionString))
            {
                var collection = db.GetCollection<T>(typeof(T).Name);
                return collection.Find(x => x.Id == id).FirstOrDefault();
            }
        }
    }
}
