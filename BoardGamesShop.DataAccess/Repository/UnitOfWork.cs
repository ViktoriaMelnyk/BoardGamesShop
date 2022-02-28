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
            Company = new CompanyRepository(_db);
            ShoppingCart= new ShoppingCartRepository(_db);
            ApplicationUser=new ApplicationUserRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }

        public IGameRepository Game { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
