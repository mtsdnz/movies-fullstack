using FluentValidation;
using MoviesAPI.Application;
using MoviesAPI.DependencyInjection;
using MoviesAPI.Infra;
using MoviesAPI.Mapping;
using MoviesAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddAndConfigureSwagger();
    builder.Services.AddDbContextForSqlite();
    builder.Services.AddControllersWithViews();
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblyContaining<ApplicationDbContext>();
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });
    builder.Services.AddAutoMapper(typeof(EntityToViewModelProfile).Assembly);
    builder.Services.AddTokenAuthorization();
    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    builder.Services.AddTransient<ExceptionHandleMiddleware>();

    builder.Services.AddCors(options =>
    {
        // Just to avoid CORS issues with the front-end (wouldn't use this in a real-world scenario)
        options.AddPolicy("allow-all",
            policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseRouting();
    app.UseCors("allow-all");

    app.UseHttpsRedirection();

    app.EnsureDatabaseSetup();

    app.UseAuthorization();
    app.UseMiddleware<ExceptionHandleMiddleware>();
    app.MapControllers();
    app.Run();
}