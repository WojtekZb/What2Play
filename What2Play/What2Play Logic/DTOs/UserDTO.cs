using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Play_Logic.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string email {  get; set; }
        public string hashedPassword { get; set; }
    }
}
