using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos.AccountDtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}