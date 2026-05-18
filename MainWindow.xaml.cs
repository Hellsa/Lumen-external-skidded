using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace SkiddingApp
{
    public partial class MainWindow : Window
    {
        private Storyboard? slideInLeft;
        private DateTime startTime;
        private bool isVisible = true;
        private MemoryManager memory = new MemoryManager();
        private SkiddingApp.Functions skiddingFunctions;
        private SkiddingApp.Features.ESPSettings espSettings = new SkiddingApp.Features.ESPSettings();
        private SkiddingApp.Features.OverlayWindow overlayWindow;
        
        // ObservableCollection for WPF DataBinding
        public ObservableCollection<SkiddingApp.Features.PlayerModel> PlayerList { get; set; } = new ObservableCollection<SkiddingApp.Features.PlayerModel>();

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private const int VK_LCONTROL = 0xA2;
        private bool lastKeyState = false;

        // ESP Settings (Synced with UI)
        public bool ESPMasterEnabled { get => espSettings.Enabled; set => espSettings.Enabled = value; }
        public bool TeamCheckEnabled { get => espSettings.TeamCheck; set => espSettings.TeamCheck = value; }
        public bool BoxesEnabled { get => espSettings.Boxes; set => espSettings.Boxes = value; }
        public bool HealthBarEnabled { get => espSettings.HealthBar; set => espSettings.HealthBar = value; }
        public bool NamesEnabled { get => espSettings.Names; set => espSettings.Names = value; }
        public bool DistanceEnabled { get => espSettings.Distance; set => espSettings.Distance = value; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            startTime = DateTime.Now;
            skiddingFunctions = new SkiddingApp.Functions(memory);
            
            if (Application.Current.TryFindResource("SlideInLeft") is Storyboard sb) slideInLeft = sb;

            // Initialize and show the Overlay
            overlayWindow = new SkiddingApp.Features.OverlayWindow(espSettings, memory);
            overlayWindow.Show();

            // Input Handling Timer
            var keyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
            keyTimer.Tick += (s, e) => {
                bool isKeyDown = (GetAsyncKeyState(VK_LCONTROL) & 0x8000) != 0;
                if (isKeyDown && !lastKeyState) ToggleVisibility();
                lastKeyState = isKeyDown;
            };
            keyTimer.Start();

            // Uptime Timer
            var uptimeTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            uptimeTimer.Tick += (s, e) => {
                TimeSpan uptime = DateTime.Now - startTime;
                UptimeText.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", uptime.Hours, uptime.Minutes, uptime.Seconds);
            };
            uptimeTimer.Start();

            // Start Advanced Background Loop (Tutorial Style)
            StartAdvancedLoop();
        }

        private async void StartAdvancedLoop()
        {
            while (true)
            {
                try
                {
                    if (!memory.IsConnected)
                    {
                        if (memory.Attach("RobloxPlayerBeta"))
                        {
                            Application.Current.Dispatcher.Invoke(() => {
                                ConnectionStatus.Text = "Connected";
                                ConnectionStatus.Foreground = System.Windows.Media.Brushes.LightGreen;
                            });
                        }
                    }

                    if (memory.IsConnected)
                    {
                        long dataModel = memory.GetDataModel();
                        if (dataModel != 0)
                        {
                            // Update PlaceID on UI thread
                            var serverInfo = skiddingFunctions.ServerMonitor.GetInfo();
                            Application.Current.Dispatcher.Invoke(() => PlaceIDText.Text = serverInfo.PlaceId.ToString());

                            // Perform Heavy Memory Scanning on Background Thread
                            var scannedPlayers = await Task.Run(() => skiddingFunctions.PlayerManager.UpdatePlayers());

                            // Sync back to UI thread (Communication with WPF)
                            Application.Current.Dispatcher.Invoke(() => {
                                // Send updated player list to the OverlayWindow
                                overlayWindow.UpdatePlayers(scannedPlayers);

                                // Sync Collections
                                PlayerListItems.ItemsSource = scannedPlayers.OrderBy(x => x.Name).ToList();
                                if (NearbyPlayersList != null)
                                    NearbyPlayersList.ItemsSource = scannedPlayers.Where(x => x.RawDistance >= 0 && x.RawDistance < 2000).OrderBy(x => x.RawDistance).Take(8).ToList();
                                
                                PlayerCountText.Text = scannedPlayers.Count.ToString();
                            });
                        }
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(() => {
                            ConnectionStatus.Text = "Disconnected";
                            ConnectionStatus.Foreground = System.Windows.Media.Brushes.Red;
                        });
                    }
                }
                catch { }

                await Task.Delay(1000); // 1 second refresh rate as per tutorial suggestion
            }
        }



        private void ToggleVisibility()
        {
            if (isVisible)
            {
                DoubleAnimation anim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.2));
                anim.Completed += (s, e) => this.Hide();
                this.BeginAnimation(Window.OpacityProperty, anim);
            }
            else
            {
                this.Show();
                this.Activate();
                this.BeginAnimation(Window.OpacityProperty, new DoubleAnimation(1, TimeSpan.FromSeconds(0.2)));
            }
            isVisible = !isVisible;
        }

        private void Spectate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button b && b.Tag is long target)
            {
                skiddingFunctions.Spectate(target);
            }
        }

        private void Teleport_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button b && b.Tag is long target)
            {
                skiddingFunctions.TeleportTo(target);
            }
        }

        private void ProcessVisuals(List<SkiddingApp.Features.PlayerModel> scannedPlayers)
        {
            // Logic moved to OverlayWindow for 60FPS rendering
        }

        private void Tab_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton btn)
            {
                slideInLeft?.Begin(ContentHost);
                string tab = btn.Content.ToString() ?? "";
                if (HomeView != null) HomeView.Visibility = tab == "Home" ? Visibility.Visible : Visibility.Collapsed;
                if (PlayersView != null) PlayersView.Visibility = tab == "Players" ? Visibility.Visible : Visibility.Collapsed;
                if (VisualsView != null) VisualsView.Visibility = tab == "Visuals" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { if (e.LeftButton == MouseButtonState.Pressed) DragMove(); }
        private void Close_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}