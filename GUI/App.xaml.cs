using Backup.Services;
using GUI.Models;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace GUI
{
    public partial class App : Application
    {
        public SensorParser _parser { get; set; }
        public List<PySensor> SensorsPy { get; set; }
        public List<LhtSensor> SensorsLht { get; set; }

        public App()
        {
            var attempts = 0;

            _parser = new SensorParser();
            SensorsPy = new List<PySensor>();
            SensorsLht = new List<LhtSensor>();

            // Setup connection with database
            var unparsedList = DatabaseConnection.Connect();
            attempts++;

            // Check if we get something, if not retry
            while (unparsedList.First.Count == 0 || unparsedList.Second.Count == 0)
            {
                unparsedList = DatabaseConnection.Connect();
                attempts++;
            }

            Debug.WriteLine("**********************************************");
            Debug.WriteLine($"Number of connection attempts: {attempts}");

            var parsedPySensor = _parser.Parse(unparsedList.First);
            var parsedLhtSensor = _parser.Parse(unparsedList.Second);

            SensorsPy = parsedPySensor.First;
            SensorsLht = parsedLhtSensor.Second;

            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
