using OfficeOpenXml;
using SmartApi.Commons;
using SmartApi.Commons.EncodeDecode;
using SmartApi.Homes.ChangePasswords._01Models;
using SmartApi.Homes.ChangePasswords._02Repositorys;
using SmartApi.Homes.ClientDetails._01Models;
using SmartApi.Homes.ClientDetails._02Repositorys;
using SmartApi.Homes.Employees._01Models;
using SmartApi.Homes.Employees._02Repositorys;
using SmartApi.Homes.ExcelFileSaves._01Models;
using SmartApi.Homes.ExcelFileSaves._02Repositorys;
using SmartApi.Homes.LoginApi._01Models;
using SmartApi.Homes.LoginApi._02Repositorys;
using SmartApi.Homes.RegistrationForms._01Models;
using SmartApi.Homes.RegistrationForms._02Repositorys;
using SmartApi.Homes.SingUps._01Models;
using SmartApi.Homes.SingUps._02Repositorys;
using SmartApi.Homes.UpdateEmployees._01Models;
using SmartApi.Homes.UpdateEmployees._02Repositorys;


var builder = WebApplication.CreateBuilder(args);
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IEncodeDecode, EncodeDecode>();
builder.Services.AddSingleton<IConnStringServices, ConJMMSORS>();
builder.Services.AddSingleton<IEmployeServices, EmployeRepository>();
builder.Services.AddSingleton<IRegistrationFormsServices, RegistrationFormRepository>();
builder.Services.AddSingleton<IUpdateServices, UpdateEmployeeRepository>();
builder.Services.AddSingleton<ILoginServices, LoginRepository>();
builder.Services.AddSingleton<ISignupServices, SignupRepository>();
builder.Services.AddSingleton<IChangePasswordServices, ChangePasswordRepository>();
builder.Services.AddSingleton<IClientDetailsServices, ClientsDetailsRepository>();
builder.Services.AddSingleton<IExecelSaveServices, ExcelSaveRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://newsmartui.netlify.app", "http://localhost:3000") // Replace with the actual origin you want to allow
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

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
