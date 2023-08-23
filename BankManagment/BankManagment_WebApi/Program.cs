using BankManagment_Domain.Entity;
using BankManagment_Infrastructure.Context;
using BankManagment_Infrastructure.Repository;
using BankManagment_Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

builder.Services.AddScoped<IRepository<BankAccount>, Repository<BankAccount>>();
builder.Services.AddScoped<IRepository<BankTransaction>, Repository<BankTransaction>>();
builder.Services.AddScoped<IRepository<AccountType>, Repository<AccountType>>();
builder.Services.AddScoped<IRepository<PaymentMethod>, Repository<PaymentMethod>>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
