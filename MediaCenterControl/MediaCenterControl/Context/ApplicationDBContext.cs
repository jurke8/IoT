using MediaCenterControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MediaCenterControl.Context
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}