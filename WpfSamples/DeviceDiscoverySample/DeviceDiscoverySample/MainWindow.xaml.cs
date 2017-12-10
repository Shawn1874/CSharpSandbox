using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Threading;
using System.Windows;

namespace DeviceDiscoverySample
{
    class ThreadIdEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "ThreadId", Thread.CurrentThread.ManagedThreadId));
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string logTemplate = "{Timestamp:yyyy-MMM-dd HH:mm:ss.fff zzz} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}";
        public MainWindow()
        {
            InitializeComponent();

            ILogger logger = new LoggerConfiguration()
               .WriteTo.RollingFile("logfile.txt", LogEventLevel.Information, outputTemplate: logTemplate, retainedFileCountLimit: 2)
               .Enrich.With(new ThreadIdEnricher())
               .WriteTo.Console(LogEventLevel.Information, outputTemplate: logTemplate)
               .Enrich.With(new ThreadIdEnricher())
                .CreateLogger();

            logger.Information("Application started");

            Log.Logger = logger;
        }

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            Log.Logger.Information("Button Clicked");
        }
    }
}
