using Microsoft.EntityFrameworkCore;
using System;

namespace AdminApp.Models
{
    public class AdminAppContext : DbContext
    {
        public AdminAppContext(DbContextOptions<AdminAppContext> options) : base(options) { }
    }
}
