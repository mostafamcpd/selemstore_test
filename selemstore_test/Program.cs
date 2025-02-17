using Microsoft.EntityFrameworkCore;
using selemstore_test.Data;

var builder = WebApplication.CreateBuilder(args);

// إضافة خدمات MVC مع معالجة الدورة المتكررة
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// إضافة Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // مدة صلاحية الجلسة
    options.Cookie.HttpOnly = true; // أمان الكوكيز
    options.Cookie.IsEssential = true; // تأكيد استخدام الكوكيز
});

// إضافة الاتصال بقاعدة البيانات
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// إعداد الـ Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// استخدام الـ Session
app.UseSession();

app.UseAuthorization();

// إعداد الـ Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();