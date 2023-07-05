using CinemaTicketsDomain.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CinemaTicketsDomain.Identity;
using CinemaTicketServices.Implementation;
using CinemaTicketServices.Interface;
using CinemaTicketsRepository;
using CinemaTicketsRepository.Implementation;
using CinemaTicketsRepository.Interface;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IMovieProjectionRepository), typeof(MovieProjectionRepository));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IShoppingCartRepository), typeof(ShoppingCartRepository));

var emailSettings = new EmailSettings();
builder.Configuration.GetSection("EmailSettings").Bind(emailSettings);
builder.Services.AddScoped<EmailSettings>(es => emailSettings);

builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<IMovieProjectionService, MovieProjectionService>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();

// builder.Services.AddScoped<IMailService, MailService>(email => new MailService(emailSettings));

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
}
else {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();