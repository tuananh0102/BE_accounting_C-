using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.WebUtilities;
using MISA.Web04.Api.Middlewares;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Interfaces.UnitOfWork;
using MISA.Web04.Core.Interfaces.Validations;
using MISA.Web04.Core.Services;
using MISA.Web04.Core.Validations;
using MISA.Web04.Infrastructure.Excels;
using MISA.Web04.Infrastructure.Repository;
using MISA.Web04.Infrastructure.UnitOfWork;
using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
                    options.JsonSerializerOptions.PropertyNamingPolicy
                     = null); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

// add DI
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeSevice>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeExcel, EmployeeExcel>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IProviderGroupRepository, ProviderGroupRepository>();
builder.Services.AddScoped<IProviderValidation, ProviderValidation>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IAddressShipRepository, AddressShipRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<IAccountantRepository, AccountantRepository>();
builder.Services.AddScoped<IReceiptValidation, ReceiptValidation>();
builder.Services.AddScoped<IAccountValidation,AccountValidation>();
builder.Services.AddScoped<IReceiptExcel, ReceiptExcel>();
builder.Services.AddScoped<IProviderExcel, ProviderExcel>();
builder.Services.AddScoped<IAccountExcel, AccountExcel>();


// add cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddLocalization(o => { o.ResourcesPath = "Resources"; });
builder.Services.AddControllersWithViews()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();

// Custom error bad request
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var modelState = actionContext.ModelState;
        var keys = actionContext.ModelState.Keys;
        

        var dictionary = modelState.Keys.ToDictionary(
            key => key,
            key => modelState[key].Errors.Select(error => error.ErrorMessage).ToList()
        );

        if (dictionary.ContainsKey("id") && dictionary["id"] != null)
        {

            dictionary.Remove("id");
        }


        return new BadRequestObjectResult(new BaseException
        {
            ErrorCode = (int)HttpStatusCode.BadRequest,
            DevMsg = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
            UserMsg = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
            TraceId = "",
            MoreInfo = "",
            ErrorMsgs = dictionary

        }) ;
    };
});

// add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
