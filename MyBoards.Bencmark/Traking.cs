using BenchmarkDotNet.Attributes;
using Board.enties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Bencmark
{
    [MemoryDiagnoser]
    public class Traking
    {
        [Benchmark]
        public int WithTraking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyBoardsContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyBoardsDb;Trusted_Connection=True;");
             var dbContext = new MyBoardsContext(optionsBuilder.Options);

            var comments = dbContext.Comments.ToList();

            return comments.Count;
        }
        [Benchmark]
        public int WithNoTraking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyBoardsContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyBoardsDb;Trusted_Connection=True;");
            var dbContext = new MyBoardsContext(optionsBuilder.Options);

            var comments = dbContext.Comments
                .AsNoTracking()
                .ToList();

            return comments.Count;
        }
    }
}
