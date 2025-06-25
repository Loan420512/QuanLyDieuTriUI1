using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? NewEmail { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public string? EmailConfirmationToken { get; set; }

    public DateTime? TokenExpiration { get; set; }

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();

    public virtual InfoDoctor? InfoDoctor { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Blog> IdBlogs { get; set; } = new List<Blog>();
}
