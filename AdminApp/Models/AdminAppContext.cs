using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection.Metadata;

namespace AdminApp.Models
{
    public class AdminAppContext : DbContext
    {
        public AdminAppContext(DbContextOptions<AdminAppContext> options) : base(options) { }

        public DbSet<Persona> persona { get; set; }
        public DbSet<Factura> factura { get; set; }
    }
}
