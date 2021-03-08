using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using DataLib.Models;

namespace ASP.Data
{
    public class SQLUnitRepository : IRepository<Unit>
    {
        private DataContext db;

        public SQLUnitRepository()
        {
            this.db = new DataContext();
        }

        public IEnumerable<Unit> GetItemList()
        {
            return db.Unit;
        }

        public Unit GetItem(int id)
        {
            return db.Unit.Find(id);
        }

        public void Create(Unit book)
        {
            db.Unit.Add(book);
        }

        public void Update(Unit book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Unit book = db.Unit.Find(id);
            if (book != null)
                db.Unit.Remove(book);
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
