using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using DataLib.Models;

namespace ASP.Data
{
    public class SQLFactoryRepository : IRepository<Factory>
    {
        private BookContext db;

        public SQLFactoryRepository()
        {
            this.db = new BookContext();
        }

        public IEnumerable<Factory> GetItemList()
        {
            return db.Factory;
        }

        public Factory GetBook(int id)
        {
            return db.Factory.Find(id);
        }

        public void Create(Factory book)
        {
            db.Factory.Add(book);
        }

        public void Update(Factory book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Factory book = db.Factory.Find(id);
            if (book != null)
                db.Factory.Remove(book);
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
