﻿using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using LiveCharts.Defaults;
using SensorThings.Client;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using Newtonsoft.Json;
using SensorThings.Core;
using System.Linq;

namespace SensorThingsRealtimeLiveChartsSample
{
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> XFormatter { get; set; }

        private static string serverurl = "http://black-pearl:8080/v1.0";
        private static int datastreamid = 11;
        private static string server;
        private static string topic;
        private double minval;

        public MainWindow()
        {
            InitializeComponent();
            graph.LegendLocation = LegendLocation.Left;
            graph.AxisY[0].MinValue = 0;
            graph.Zoom = ZoomingOptions.Xy;
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<DateTimePoint>()
                } 
            };

            XFormatter = val => new DateTime((long)val).ToString("HH:mm");
            YFormatter = val => val.ToString("N") + "";
            LoadData();
            server = new Uri(serverurl).Host;
            topic = $"Datastreams({datastreamid})/Observations";

            ConnectToMqtt();


            DataContext = this;
        }

        private void ConnectToMqtt()
        {
            var mqttclient = new MqttClient(server);
            byte code = mqttclient.Connect(Guid.NewGuid().ToString());

            ushort msgId = mqttclient.Subscribe(new string[] { topic },
                    new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            mqttclient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }


        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var str = Encoding.Default.GetString(e.Message);
            var observation = JsonConvert.DeserializeObject<Observation>(str);
            var lPhenomenonTime = observation.PhenomenonTime.ToLocalTime();
            var newpoint = new DateTimePoint(lPhenomenonTime, (double)observation.Result);
            Dispatcher.Invoke(() =>
            {
                SeriesCollection[0].Values.Add(newpoint);
            });
        }

        private void LoadData()
        {
            var client = new SensorThingsClient(serverurl);
            var datastream = client.GetDatastream(datastreamid);
            var observations = datastream.GetObservations();
            var obs = observations.Items.OrderBy(m => m.PhenomenonTime);
            foreach (var observation in obs)
            {
                var lPhenomenonTime = observation.PhenomenonTime.ToLocalTime();
                var res = Convert.ToDouble(observation.Result);
                var newpoint = new DateTimePoint(lPhenomenonTime, res);
                SeriesCollection[0].Values.Add(newpoint);
            }

        }
    }
}
