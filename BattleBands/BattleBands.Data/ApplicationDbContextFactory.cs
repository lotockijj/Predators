﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBands.Data
{
    class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create(DbContextFactoryOptions options)
        {

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("workstation id=BattleBandsDB.mssql.somee.com;packet size=4096;user id=K_Adams_SQLLogin_1;pwd=xgoxt4nuwg;data source=BattleBandsDB.mssql.somee.com;persist security info=False;initial catalog=BattleBandsDB; MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer("Server=PAYNE-PC\\SQLEXPRESS;Database=TestBattleBandsDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
