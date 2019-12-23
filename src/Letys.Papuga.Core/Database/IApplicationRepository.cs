using System.Collections.Generic;

namespace Letys.Parrot.Core
{
    public interface IApplicationRepository
    {
        void Insert<T>(T item);
        void Update<T>(T item);
        void Delete<T>(int id) where T : BaseObject;
        IEnumerable<T> GetAll<T>();
        T GetById<T>(int id) where T : BaseObject;
    }
}
