using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using DataLib.Models;

namespace ASP.Data
{
    public class SQLTankRepository : IRepository<Tank>
    {
        private DataContext db;

        public SQLTankRepository()
        {
            this.db = new DataContext();
        }

        public IEnumerable<Tank> GetItemList()
        {
            return db.Tank;
        }

        public Tank GetItem(int id)
        {
            return db.Tank.Find(id);
        }

        public void Create(Tank book)
        {
            db.Tank.Add(book);
        }

        public void Update(Tank book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Tank book = db.Tank.Find(id);
            if (book != null)
                db.Tank.Remove(book);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
