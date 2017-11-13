using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.BaseModels;
using Shared.Interfaces;
using Shared.Models;
using CodingDojo3.DataSimulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CodingDojo3.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private Simulator sim;
        private List<ItemVm> Items = new List<ItemVm>();

        public ObservableCollection<ItemVm> Actors { get; set; }
        public ObservableCollection<ItemVm> Sensors { get; set; }

        private string curTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        private string curDate = DateTime.Now.ToLocalTime().ToShortDateString();

        DispatcherTimer timer = new DispatcherTimer();

        public string CurTime {
            get {return curTime; }
            set { curTime = value; RaisePropertyChanged(); }
        }

        public string CurDate
        {
            get { return curDate; }
            set { curDate = value; ; RaisePropertyChanged(); }
        }


        public MainViewModel()
        {
           
           
           Actors = new ObservableCollection<ItemVm>();
           Sensors = new ObservableCollection<ItemVm>();



            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += new EventHandler(LoadTime);

            timer.Start();



        }

        private void LoadTime(object sender, EventArgs ev)
        {
            CurTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurDate = DateTime.Now.ToLocalTime().ToShortDateString();
        }
            

        public void LoadSampleDataFromSimulator()
        {

            sim = new Simulator(Items);


        }

    }
}