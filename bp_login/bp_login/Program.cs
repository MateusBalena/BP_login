using bp_login.data.Context;
using bp_login.data.Repo;
using bp_login.Filtros;

var builder = WebApplication.CreateBuilder(args);

//Adiciona serviços para serem utilizados dentro do aplicativo do ASP.NET CORE
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IBDContext, BDContext>();
builder.Services.AddTransient<ILoginRepo, LoginRepo>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSession();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(FiltroGlobal));
    options.EnableEndpointRouting = false;
});

//=======================================================================================

//Constrói o aplicativo que será usado e executado do ASP.NET CORE
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMvc();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
