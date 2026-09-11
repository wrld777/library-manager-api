using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Models
{
    public class Loan
    {
        public Guid LoanId { get; set; }
        public Guid MemberId { get; set; }
        public Guid BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
