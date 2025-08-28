using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Homework6Task5Reflector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Assembly assembly;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "DLL files (*.dll)|*.dll|Executable files (*.exe)|*.exe|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;

                try
                {
                    assembly = Assembly.LoadFile(path);
                    textBlock.Text = $"Збірка {path} - УСПІШНО ЗАГРУЖЕНА"
                                   + Environment.NewLine + Environment.NewLine;
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                textBlock.Text += $"СПИСОК ВСІХ ТИПІВ В ЗБІРЦІ: {assembly.FullName}"
                                + Environment.NewLine + Environment.NewLine;

                foreach (Type type in assembly.GetTypes())
                {
                    textBlock.Text += $"Тип: {type}" + Environment.NewLine;

                    foreach (var method in type.GetMethods())
                    {
                        string methStr = $"   Метод: {method.Name}" + Environment.NewLine;
                        var methodBody = method.GetMethodBody();
                        if (methodBody != null)
                        {
                            methStr += "   IL-код: ";
                            foreach (var b in methodBody.GetILAsByteArray())
                                methStr += b + ":";
                        }
                        textBlock.Text += methStr + Environment.NewLine;
                    }
                }
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void textBlock_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}