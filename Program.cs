var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку Razor Pages (это нужно для твоего интерфейса)
builder.Services.AddRazorPages();

var app = builder.Build();

// Настройки обработки ошибок и безопасности
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Указываем, что главная страница — это наши Razor Pages
app.MapRazorPages();

app.Run();