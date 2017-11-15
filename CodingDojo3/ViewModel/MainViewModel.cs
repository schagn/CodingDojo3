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
        private List<ItemVm> AllItems;

        public ObservableCollection<ItemVm> Actors { get; set; }
        public ObservableCollection<ItemVm> Sensors { get; set; }

        public ObservableCollection<string> SensorModeList { get; private set; }
        public ObservableCollection<string> ActorModeList { get; private set; }

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

         AllItems = new List<ItemVm>();
           
         Actors = new ObservableCollection<ItemVm>();
         Sensors = new ObservableCollection<ItemVm>();
         SensorModeList = new ObservableCollection<string>();
         ActorModeList = new ObservableCollection<string>();



            //Mode Liste befüllen

            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                SensorModeList.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {
                ActorModeList.Add(item);

            }

            if (!IsInDesignMode)
            {
                LoadSampleDataFromSimulator();

            }

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

            //ModeList.Add("Enabled");
            //ModeList.Add("Disabled");

            Simulator sim = new Simulator(AllItems);

            foreach(var listitem in sim.Items)
            {
                if(listitem.ItemType.Equals(typeof(IActuator)))
                {
                    Actors.Add(listitem);
                }

                if (listitem.ItemType.Equals(typeof(ISensor)))
                {
                    Sensors.Add(listitem);
                }
            }


        }

    }
}