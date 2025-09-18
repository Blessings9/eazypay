using EazyPay.Application.Services.Banks;
using EazyPay.Application.Services.Payments;
using EazyPay.Core.Entities;
using EazyPay.Core.Repositories;
using EazyPay.Infrastructure.Persistence.Contexts;
using EazyPay.Infrastructure.Persistence.Repositories;
using EazyPay.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("EazyPay"));
});

builder.Services.AddTransient<IBaseRepository<BaseEntity>, BaseRepository<BaseEntity>>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<IBankService, BankService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // Replace with your React app's URL
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("MyAllowSpecificOrigins");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();


app.Run();