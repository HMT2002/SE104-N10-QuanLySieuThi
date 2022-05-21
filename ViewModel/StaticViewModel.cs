using SE104_N10_QuanLySieuThi.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class StaticViewModel:BaseViewModel
    {
        public SeriesCollection SeriesMostProduct { get; set; }

        public StaticViewModel()
        {
            SeriesMostProduct = new SeriesCollection()
            {
                new PieSeries
                {
                Title="1",
                Values=new ChartValues<ObservableValue>{new ObservableValue(9)},
                DataLabels=true
                },

                new PieSeries
                {
                Title="2",
                Values=new ChartValues<ObservableValue>{new ObservableValue(21)},
                DataLabels=true
                },
                                                new PieSeries
                {
                Title="3",
                Values=new ChartValues<ObservableValue>{new ObservableValue(33)},
                DataLabels=true
                },
                                                                new PieSeries
                {
                Title="4",
                Values=new ChartValues<ObservableValue>{new ObservableValue(37)},
                DataLabels=true
                }

            };
        }

    }
}
