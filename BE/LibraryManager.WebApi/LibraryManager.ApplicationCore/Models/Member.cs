using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Models
{
    public class Member
    {
        public Guid MemberId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
