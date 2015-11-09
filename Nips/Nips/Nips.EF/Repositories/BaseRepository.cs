using System;

namespace Nips.DAL.Repositories
{
    public class BaseRepository : IDisposable
    {
        protected DataService Db;

        public BaseRepository()
        {
            Db = new DataService();
        }

        public void Dispose()
        {

        }
    }
}
