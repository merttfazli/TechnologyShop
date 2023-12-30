using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IModuleService,ModuleManager>();
builder.Services.AddSingleton<IModuleDal,EfModuleDal>();
builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IUserDal, EfUserDal>();
builder.Services.AddSingleton<IUserGroupService, UserGroupManager>();
builder.Services.AddSingleton<IUserGroupDal, EfUserGroupDal>();
builder.Services.AddSingleton<IProductService, ProductManager>();
builder.Services.AddSingleton<IProductDal, EfProductDal>();
builder.Services.AddSingleton<IColorService,ColorManager>();
builder.Services.AddSingleton<IColorDal, EfColorDal>();
builder.Services.AddSingleton<ICategoryService, CategoryManager>();
builder.Services.AddSingleton<ICategoryDal, EfCategoryDal>();
builder.Services.AddSingleton<IBrandService, BrandManager>();
builder.Services.AddSingleton<IBrandDal, EfBrandDal>();
builder.Services.AddSingleton<IAuthService, AuthManager>();
builder.Services.AddSingleton<IFeatureDal, EfFeatureDal>();
builder.Services.AddSingleton<IFeatureService, FeatureManager>();
builder.Services.AddSingleton<IFeatureRelationDal, EfFeatureRelationDal>();
builder.Services.AddSingleton<IFeatureRelationService, FeatureRelationManager>();
//Add services to the container.

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
config.Filters.Add(new AuthorizeFilter(policy));
}).AddRazorRuntimeCompilation();

builder.Services.AddSession();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ElevatedRights", policy =>
          policy.RequireRole("Admin", "User"));
});
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
    {
        option.LoginPath = "/login";
        option.Cookie.HttpOnly = true;
        option.Cookie.SameSite = SameSiteMode.Strict;
        option.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        option.ExpireTimeSpan = TimeSpan.FromDays(30);
        option.SlidingExpiration = true;
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
