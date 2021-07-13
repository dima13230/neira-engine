using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

using System.Text.RegularExpressions;

namespace MultiPNG2Gif
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fullPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sprite_name.Text) && Directory.Exists(fullPath + sprite_name.Text))
            {
                GifBitmapEncoder encoder = new GifBitmapEncoder();

                if (!Directory.Exists(fullPath + sprite_name.Text + "/converted/"))
                    Directory.CreateDirectory(fullPath + sprite_name.Text + "/converted/");

                FileStream saveStream = new FileStream(fullPath + sprite_name.Text + "/converted/" + Path.GetDirectoryName(sprite_name.Text + "/") + ".gif", FileMode.Create);

                List<string> files = Directory.GetFiles(fullPath + sprite_name.Text).ToList().AlphabetSort().ToList();
                List<FileStream> streams = new List<FileStream>();
                if (files.Count > 0)
                {

                    for (int i = 0; i < files.Count; i++)
                    {
                        streams.Add(new FileStream(files[i], FileMode.Open));
                        encoder.Frames.Add(BitmapFrame.Create(streams[i]));
                    }

                    encoder.Save(saveStream);

                    foreach(FileStream stream in streams)
                    {
                        stream.Close();
                        stream.Dispose();
                    }

                    saveStream.Close();
                    saveStream.Dispose();
                    if((bool)ofap.IsChecked)
                    {
                        System.Diagnostics.Process.Start("explorer", $"\"{fullPath + sprite_name.Text + " /converted/"}\"".Replace("/","\\"));
                    }
                }
                else
                {
                    saveStream.Close();
                    saveStream.Dispose();
                    File.Delete(fullPath + sprite_name.Text + "/converted/" + Path.GetDirectoryName(sprite_name.Text) + ".gif");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fullPath = Path.GetFullPath("./sprites/");
            string[] dirs = Directory.GetDirectories(fullPath);
            foreach (string dir in dirs)
                sprite_name.Items.Add(Path.GetFileName(dir));
        }
    }
}
