using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}