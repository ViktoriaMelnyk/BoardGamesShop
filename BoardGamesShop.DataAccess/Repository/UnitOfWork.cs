using BoardGames.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork

    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Game = new GameRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }

        public IGameRepository Game { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
