using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class AccountRepository(ApplicationDataContext dataContext, UserManager<AppUser> userManager) : IAccountRepository
    {
        private readonly ApplicationDataContext _dataContext = dataContext;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<List<AppUser>> GetAllAsync()
        {
            
            return await  _dataContext.Users.ToListAsync(); 
        }
        
    }
}