using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningDiary
{
    public class LearningDiaryContextFactory : IDesignTimeDbContextFactory<LearningDiaryContext>
    {
        public LearningDiaryContext CreateDbContext(string[] args)
        {    
            return new LearningDiaryContext();
        }

        public static string getdbConfigStr() 
        {

            string dbConfigStr = @"Server=DESKTOP-7BQQ30N\MSSQLSERVER2;Database=LearningDiary;Trusted_Connection=True;MultipleActiveResultSets=true";
            return dbConfigStr;
        }

    }
}
