using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AccountDtos;

namespace Backend.Interface
{
    public interface IAccountService
    {
        Task<List<GetUserDto>> GetAll();
    }
}