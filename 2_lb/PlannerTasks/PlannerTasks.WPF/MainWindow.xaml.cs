using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ninject;
using Ninject.Modules;
using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.Interfaces;
using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.WPF.Util;

namespace PlannerTasks.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Specify path to dataDirectory
            string path = ConfigurationSettings.AppSettings["DataDirectory"];
            //Configurating data_directory path
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            var modules = new INinjectModule[] { new ServiceModule("TestInjection") };
            var kernel = new StandardKernel(modules);
            kernel.Bind<ITaskService>().To<TaskService>();
            ITaskService taskService = kernel.Get<ITaskService>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /*
       private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
       {

       }*/
    }
}
