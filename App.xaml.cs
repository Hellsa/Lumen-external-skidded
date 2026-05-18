using System.IO;
using System.Windows;

namespace SkiddingApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crash_log.txt"), $"[{DateTime.Now}] Application Starting (Manual)...\n");
            
            AppDomain.CurrentDomain.UnhandledException += (s, ev) => 
                LogException(ev.ExceptionObject as Exception);
            
            this.DispatcherUnhandledException += (s, ev) => {
                LogException(ev.Exception);
                ev.Handled = true;
            };

            try 
            {
                MainWindow main = new MainWindow();
                main.Show();
                File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crash_log.txt"), $"[{DateTime.Now}] Window Shown.\n");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            base.OnStartup(e);
        }

        private void LogException(Exception? ex)
        {
            if (ex == null) return;
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crash_log.txt");
            File.AppendAllText(logPath, $"[{DateTime.Now}] ERROR: {ex.Message}\n{ex.StackTrace}\n\n");
            MessageBox.Show($"Application Error: {ex.Message}\nCheck crash_log.txt for details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

