using Board.Dto;
using Board.enties;
using Board.Enties;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyBoardsContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyBoardConectionString")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<MyBoardsContext>();

var PendingMigraions = dbContext.Database.GetPendingMigrations();
if (PendingMigraions.Any())
{
    dbContext.Database.Migrate();
}

var users = dbContext.Users.ToList();

if(!users.Any())
{
    var user1 = new User()
    {
        Email = "user1@test.com",
        FullName = "user One",
        Address = new Address()
        {
            City = "Cracow",
            Street = "Mickievicza"
        }
    };

    var user2 = new User()
    {
        Email = "user2@test.com",
        FullName = "user Two",
        Address = new Address()
        {
            City = "kracow",
            Street = "Szeroka"
        }
    };

    dbContext.Users.AddRange(user1,user2);

    dbContext.SaveChanges();    
}

app.MapPost("pagination", async (MyBoardsContext db) =>
{
    //user input 
    var filter = "a";
    string sortBy = "FullName";//FullName Email null
    bool SortByDesceding = false;
    int pageNumber = 1;
    int pageSize = 10;
    //

    var query = db.Users
    .Where(u => filter == null || 
    (u.Email.ToLower().Contains(filter.ToLower()) || u.FullName.ToLower().Contains(filter.ToLower())));

    var totalCount = query.Count();
    if (sortBy != null)
    {
        //
        var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>
        {
            {nameof(User.Email), user =>  user.Email},
            {nameof(User.FullName), user =>  user.FullName}
        };

        var SortByExpression = columnsSelector[sortBy];
        query = SortByDesceding ? query.OrderByDescending(SortByExpression) 
        :query.OrderBy(SortByExpression);
    }

    var result  = query.Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToList();

    var pagedResult = new PagedResult<User>(result,totalCount,pageSize,pageNumber);

    return pagedResult;
});


app.Run();


