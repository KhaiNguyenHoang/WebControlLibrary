using System;
using System.Collections.Generic;

namespace WebControlLibrary.Models.Entites;

public partial class Returning
{
    public int ReturnId { get; set; }

    public int? BorrowId { get; set; }

    public DateOnly ReturnDate { get; set; }

    public decimal? LateFee { get; set; }

    public virtual Borrowing? Borrow { get; set; }
}
