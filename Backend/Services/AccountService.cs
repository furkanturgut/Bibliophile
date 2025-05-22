using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Data;
using Backend.Dtos.AccountDtos;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDataContext dataContext;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, UserManager<AppUser> userManager, ApplicationDataContext dataContext)
        {
            this._accountRepository = accountRepository;
            this._mapper = mapper;
            this._userManager = userManager;
            this.dataContext = dataContext;
        }

        public async Task<List<GetUserDto>> GetAll()
        {
            try
            {
                var users= _mapper.Map<List<GetUserDto>>( await _accountRepository.GetAllAsync()); 
                foreach (GetUserDto user in users)
                {
                    var role = await dataContext.UserRoles.Where(w=> w.UserId== user.Id).Select(w=> w.RoleId).FirstOrDefaultAsync();
                    user.role = await dataContext.Roles.Where(r=> r.Id==role).Select(rn=> rn.Name).FirstOrDefaultAsync();
                }
                return users;
                
            }
            catch (Exception )
            { 
                throw ;
            }
        }
    }
}