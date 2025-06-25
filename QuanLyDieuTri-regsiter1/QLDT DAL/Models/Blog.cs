using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class Blog
{
    public int IdBlog { get; set; }

    public string? Type { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
