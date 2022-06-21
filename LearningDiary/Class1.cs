using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningDiary
{
    public class LearningDiaryContext : DbContext 
    {

        public LearningDiaryContext(DbContextOptions<LearningDiaryContext> options) : base(options)
        { 
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Task> Tasks { get; set; }
        

    }
    
   
}
