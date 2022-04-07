using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using KatoAPI;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using system.net;
using System.Xml;
namespace StarForce
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileSystemWatcher watcher = new FileSystemWatcher();



        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += async delegate (object s, RoutedEventArgs e)
            {
                UpdateSyntax();

 			WebClient web = new WebClient();
                if (web.DownloadString("https://www.ch3rry.red/storage/starforce/check.html") != "enabled")
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("StarForce is currently disabled, please try agian later.", "Disabled", MessageBoxButton.OK, MessageBoxImage.Information); // Add custom Box
                    Environment.Exit(0);
                } else
		{
			data.close ();
			reader.close ();
		}
				
             


            };

        }



        private void UpdateSyntax()
        {
            Stream xshd_stream = File.OpenRead(Environment.CurrentDirectory + @"\bin\" + "Lua.xshd");
            XmlTextReader xshd_reader = new XmlTextReader(xshd_stream);
            avalon.SyntaxHighlighting = HighlightingLoader.Load(xshd_reader, HighlightingManager.Instance);

            xshd_reader.Close();
            xshd_stream.Close();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void inject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Kato.Inject();
        }

        private void execute_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Kato.IsInjected())
            {
                Kato.ExecuteScript(avalon.Text);
            }
            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Please inject before trying to execute a script.", "Not injected", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private void load_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua|All Files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (Kato.IsInjected())
            {
                var s = dialog.OpenFile();
                using (StreamReader sr = new StreamReader(s))
                {
                    avalon.Text = sr.ReadToEnd();
                }
            }
            else
            {
              MessageBoxResult result = System.Windows.MessageBox.Show("Please inject before trying to load a script.", "Not injected", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void save_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua|All Files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() != 0)
            {
                File.WriteAllText(dialog.FileName, avalon.Text);
            }
            else
            {
               return;
            }
        }

        private void clear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            avalon.Clear();
        }
    }
}
