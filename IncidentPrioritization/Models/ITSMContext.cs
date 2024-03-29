using IncidentPrioritization.Entities;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace IncidentPrioritization.Models
{
    public class ITSMContext : DbContext
    {
        public ITSMContext(DbContextOptions<ITSMContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Incidents> Incidents { get; set; }

    }
}

