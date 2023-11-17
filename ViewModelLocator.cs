using Microsoft.Extensions.DependencyInjection;
using NormManager.Services;
using NormManager.Services.Interfaces;
using NormManager.ViewModels;
using NormManager.Views;

namespace NormManager
{
    public class ViewModelLocator
    {
        private static ServiceProvider? _provider;

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();

        public CreateNewXmlViewModel CreateNewXmlViewModel => _provider.GetRequiredService<CreateNewXmlViewModel>();

        public CreateNewFolderViewModel CreateNewFolderViewModel => _provider.GetRequiredService<CreateNewFolderViewModel>();

        public EditorTreeViewModel EditorTreeViewModel => _provider.GetRequiredService<EditorTreeViewModel>();

        public AddParamsViewModel AddParamsViewModel => _provider.GetRequiredService<AddParamsViewModel>();

        public CreateMeasureValueViewModel CreatieMeasureValueViewModel => _provider.GetRequiredService<CreateMeasureValueViewModel>();

        public EditMeasureValueViewModel EditMeasureValueViewModel => _provider.GetRequiredService<EditMeasureValueViewModel>();

        public TableColumnsViewModel TableColumnsViewModel => _provider.GetRequiredService<TableColumnsViewModel>();

        public EditTableColumnsViewModel EditTableColumnsViewModel => _provider.GetRequiredService<EditTableColumnsViewModel>();

        public static void Init()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IStructureService, StructureService>();
            services.AddSingleton<ITreeService, TreeService>();
            services.AddTransient<MainWindowViewModel>(); 
            services.AddTransient<CreateNewXmlViewModel>();
            services.AddTransient<EditorTreeViewModel>();
            services.AddTransient<CreateNewFolderViewModel>(); 
            services.AddTransient<AddParamsViewModel>(); 
            services.AddTransient<CreateMeasureValueViewModel>(); 
            services.AddTransient<TableColumnsViewModel>();
            services.AddTransient<EditMeasureValueViewModel>();
            services.AddTransient<EditTableColumnsViewModel>();

            WindowService.RegisterWindow<CreateNewXmlViewModel, CreateNewXmlWindow>();
            WindowService.RegisterWindow<EditorTreeViewModel, EditorTreeWindow>();
            WindowService.RegisterWindow<CreateNewFolderViewModel, CreateNewFolderWindow>();
            WindowService.RegisterWindow<AddParamsViewModel, AddParamsWindow>(); 
            WindowService.RegisterWindow<CreateMeasureValueViewModel, CreateMeasureValueWindow>(); 
            WindowService.RegisterWindow<TableColumnsViewModel, TableColumnsWindow>();
            WindowService.RegisterWindow<EditMeasureValueViewModel, EditMeasureValueWindow>();
            WindowService.RegisterWindow<EditTableColumnsViewModel, EditTableColumnsWindow>();

            _provider = services.BuildServiceProvider();
            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
    }
}
