using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IAccountRepository
    {
        Task<List<AppUser>> GetAllAsync ();
    }
}