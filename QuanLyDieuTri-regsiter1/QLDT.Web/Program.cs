// File: Program.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore; // Cần thiết cho AddDbContext
using QLDT_DAL.Models; // Cho Test6Context
using QLDT_DAL;       // Cho RegisterRep
using QLDT.BLL;       // Cho RegisterSvc
using QLDT.BLL.Services; // Cho IEmailService và EmailService


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ********************************************************************************
// <<< THÊM CÁC DÒNG ĐĂNG KÝ DỊCH VỤ CỦA BẠN VÀO ĐÂY >>>

// 1. Đăng ký Test6Context (từ appsettings.json)
builder.Services.AddDbContext<Test6Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Đăng ký RegisterRep (nhận Test6Context qua DI)
builder.Services.AddScoped<RegisterRep>();

// 3. Đăng ký Email Service
builder.Services.AddTransient<IEmailService, EmailService>();

// 4. Đăng ký Business Service (nhận RegisterRep và IEmailService qua DI)
builder.Services.AddScoped<RegisterSvc>();

builder.Services.AddScoped<UserRep>();
builder.Services.AddScoped<UserSvc>();

// ********************************************************************************
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // frontend chạy ở đây
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Thêm UseCors nếu bạn có cấu hình CORS (ví dụ từ Startup.cs cũ)
// app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); // Chỉ thêm nếu bạn cần CORS policy

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
