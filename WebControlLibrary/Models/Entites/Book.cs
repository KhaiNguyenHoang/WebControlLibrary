using System;
using System.Collections.Generic;

namespace WebControlLibrary.Models.Entites;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string? Genre { get; set; }

    public int? PublishedYear { get; set; }

    public string? Isbn { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
