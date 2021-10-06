using System;
using System.Collections.Generic;

#nullable disable

namespace InteracTech.DataAccess
{
    public partial class User
    {
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public bool IsBlocked { get; set; }
        public string Username { get; set; }
        public string CountryCode { get; set; }
    }
}
