using NormManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace NormManager.Services
{
    public class WindowService : IWindowService
    {
        private static Dictionary<System.Type, System.Type> _views = new();
        private static List<Window> _windows = new();

        /// <summary>
        /// Регистрация окна
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TView"></typeparam>
        public static void RegisterWindow<TViewModel, TView>()
        {
            _views.Add(typeof(TViewModel), typeof(TView));
        }

        /// <summary>
        /// Окрыть окно по VM
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        public void Show<TViewModel>()
        {
            var type = _views[typeof(TViewModel)];
            if (type != null) 
            {
                var window = Activator.CreateInstance(type) as Window;
                _windows.Add(window);
                window.ShowDialog();
            }
        }

        /// <summary>
        /// Закрыть окно по VM
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        public void Close<TViewModel>()
        {
            Window? tempWindow = null;
            var nameWindow = _views[typeof(TViewModel)].Name;
            if (_windows.Count > 0)
            {
                tempWindow = _windows.Find(x => x.GetType().Name == nameWindow);
            }

            if (tempWindow != null)
            {
                tempWindow.Close();
                _windows.Remove(tempWindow);
            }
        }
    }
}
