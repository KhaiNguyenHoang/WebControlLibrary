using System;
using System.Collections.Generic;

namespace WebControlLibrary.Models.Entites;

public partial class Borrowing
{
    public int BorrowId { get; set; }

    public int? ReaderId { get; set; }

    public int? BookId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Reader? Reader { get; set; }

    public virtual ICollection<Returning> Returnings { get; set; } = new List<Returning>();
}
