﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookMenagement.DAL.DBContext;
using Microsoft.EntityFrameworkCore.Migrations;
using BookMenagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookMenagement.DAL.Repository
{
    public class Repository<T> : IRepository<T>,IDisposable where T : class
    {
        private BookMenagementContext db;
        private DbSet<T> dbSet;

        public Repository()
        {
            db = new BookMenagementContext();
            dbSet = db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T GetById(object id)
        {
            return dbSet.Find(id);
        }
        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }
        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T getObjById = dbSet.Find(id);
            dbSet.Remove(getObjById);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (this.db != null)
        //        {
        //            this.db.Dispose();
        //            this.db = null;
        //        }
        //    }
        //}
        public void Dispose()
        {
            if (this.db != null)
            {
                this.db.Dispose();
                this.db = null;
            }
        }

        public IQueryable<T> where(Expression<Func<T,bool>> predicate)
        {
            return this.dbSet.Where(predicate);
        }
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Any(predicate);
        }

    }
}
