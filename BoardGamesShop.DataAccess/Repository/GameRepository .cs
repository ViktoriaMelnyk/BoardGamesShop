using BoardGames.DataAccess.Repository.IRepository;
using BoardGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.DataAccess.Repository
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private  ApplicationDbContext _db;
        public GameRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public void Update(Game obj)
        {
            var objFromDb = _db.Games.FirstOrDefault(u=>u.Id==obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.MinPlayers = obj.MinPlayers;
                objFromDb.MaxPlayers=obj.MaxPlayers;
                objFromDb.Price = obj.Price;
                objFromDb.Price3 = obj.Price3;
                objFromDb.Price10 = obj.Price10;
                objFromDb.Authors=obj.Authors;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
             




            }
        }
    }
}
