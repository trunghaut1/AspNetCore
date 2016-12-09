using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public interface ICatRepository
    {
        IEnumerable<Cat> Cats { get; }
        Cat GetById(int id);
    }
}
