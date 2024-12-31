using System;
using System.Collections.Generic;

namespace WebControlLibrary.Models.Entites;

public partial class Reader
{
    public int ReaderId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
