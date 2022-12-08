using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfBKStudio1.Models;
using WpfBKStudio1.ViewModels;

namespace WpfBKStudio1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ReversePN _reversePNModel;

        public App()
        {
            _reversePNModel = new ReversePN();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(new ReversePolishNotationViewModel(_reversePNModel))
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
