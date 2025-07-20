using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using ThreatModelingApp.Data.DbContext;
using ThreatModelingApp.Data.Repositories;
using ThreatModelingApp.Core.Services;
using ThreatModelingApp.Views;

namespace ThreatModelingApp
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Инициализация конфигурации
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // Настройка DI-контейнера
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            // Инициализация базы данных
            InitializeDatabase();

            // Проверка обновлений
            CheckForUpdates();

            // Показать главное окно
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Конфигурация базы данных
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Репозитории
            services.AddScoped<IThreatRepository, ThreatRepository>();
            services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
            services.AddScoped<IDataRepository, LocalDataRepository>();

            // Сервисы
            services.AddSingleton<IDocumentGenerator, WordDocumentGenerator>();
            services.AddSingleton<IUpdateService, UpdateService>();
            services.AddSingleton<IConfiguration>(Configuration);

            // Окна
            services.AddSingleton<MainWindow>();
            services.AddTransient<QuestionnaireView>();
            services.AddTransient<ThreatsListView>();
        }

        private void InitializeDatabase()
        {
            using var scope = ServiceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();
            
            // Применение миграций (если есть)
            // dbContext.Database.Migrate();
        }

        private async void CheckForUpdates()
        {
            try
            {
                var updateService = ServiceProvider.GetRequiredService<IUpdateService>();
                if (await updateService.CheckForUpdatesAsync())
                {
                    if (MessageBox.Show("Доступно обновление. Загрузить сейчас?", 
                        "Обновление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        await updateService.DownloadUpdatesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке обновлений: {ex.Message}");
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // Очистка ресурсов
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}