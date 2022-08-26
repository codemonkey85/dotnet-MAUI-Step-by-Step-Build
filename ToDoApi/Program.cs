var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContextFactory<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

const string toDoEndpointUrl = $"api/todo";

// get all
app.MapGet(toDoEndpointUrl, async (AppDbContext appDbContext) =>
{
    var toDos = await appDbContext.ToDos.ToListAsync();
    return Results.Ok(toDos);
});

// get one
app.MapGet($"{toDoEndpointUrl}/{{id}}", async (AppDbContext appDbContext, int id) =>
{
    var foundToDo = await appDbContext.ToDos.FindAsync(id);
    return Results.Ok(foundToDo);
});

// create
app.MapPost(toDoEndpointUrl, async (AppDbContext appDbContext, ToDo newToDo) =>
{
    await appDbContext.ToDos.AddAsync(newToDo);
    await appDbContext.SaveChangesAsync();
    return Results.Created($"{toDoEndpointUrl}/{newToDo.Id}", newToDo);
});

// update
app.MapPut($"{toDoEndpointUrl}/{{id}}", async (AppDbContext appDbContext, int id, ToDo toDo) =>
{
    var foundToDo = await appDbContext.ToDos.FindAsync(id);
    if (foundToDo is null)
    {
        return Results.NotFound();
    }
    foundToDo.Title = toDo.Title;
    await appDbContext.SaveChangesAsync();
    return Results.NoContent();
});

// delete
app.MapDelete($"{toDoEndpointUrl}/{{id}}", async (AppDbContext appDbContext, int id) =>
{
    var foundToDo = await appDbContext.ToDos.FindAsync(id);
    if (foundToDo is null)
    {
        return Results.NotFound();
    }
    appDbContext.ToDos.Remove(foundToDo);
    await appDbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
