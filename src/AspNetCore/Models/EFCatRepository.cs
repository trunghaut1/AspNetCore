using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class EFCatRepository : ICatRepository
    {
        private DatabaseContext db;
        public EFCatRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public IEnumerable<Cat> Cats => db.Cat;
        public Cat GetById(int id)
        {
            return db.Cat.Find(id);
        }
    }
}
