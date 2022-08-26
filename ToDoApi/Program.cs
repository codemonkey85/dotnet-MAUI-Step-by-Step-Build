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

// get all
app.MapGet(ToDoEndpointUrl, async (AppDbContext appDbContext) =>
{
    var toDos = await appDbContext.ToDos.ToListAsync();
    return Results.Ok(toDos);
});

// get one
app.MapGet($"{ToDoEndpointUrl}/{{id}}", async (AppDbContext appDbContext, int id) =>
{
    var foundToDo = await appDbContext.ToDos.FindAsync(id);
    return Results.Ok(foundToDo);
});

// create
app.MapPost(ToDoEndpointUrl, async (AppDbContext appDbContext, ToDo newToDo) =>
{
    await appDbContext.ToDos.AddAsync(newToDo);
    await appDbContext.SaveChangesAsync();
    return Results.Created($"{ToDoEndpointUrl}/{newToDo.Id}", newToDo);
});

// update
app.MapPut($"{ToDoEndpointUrl}/{{id}}", async (AppDbContext appDbContext, int id, ToDo toDo) =>
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
app.MapDelete($"{ToDoEndpointUrl}/{{id}}", async (AppDbContext appDbContext, int id) =>
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
