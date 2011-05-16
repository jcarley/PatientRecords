using System;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using Infrastructure;
using PatientRecords.ViewModels;
using StructureMap;

namespace PatientRecords
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // configure StructureMap
            BootStrapper.Run();

            //Console.WriteLine(ObjectFactory.WhatDoIHave());

            // Get the MainViewModel
            var locator = new ViewModels.ViewModelLocator();
            var main = locator.Main;
            main.ActivateItem<PatientSearchViewModel>();
        }
    }
}
