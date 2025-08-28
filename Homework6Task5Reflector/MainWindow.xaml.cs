using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;

namespace Homework6Task5Reflector
{
    public partial class MainWindow : Window
    {
        private Assembly assembly;
        public MainWindow()
        {
            InitializeComponent();
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

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"СПИСОК ВСІХ ТИПІВ В ЗБІРЦІ: {assembly.FullName}");
                sb.AppendLine();

                foreach (Type type in assembly.GetTypes())
                {
                    sb.AppendLine($"Тип: {type.FullName}");

                    foreach (var attr in type.GetCustomAttributes())
                    {
                        sb.AppendLine($"   Атрибут типу: [{attr.GetType().Name}]");
                    }

                    if (chkMethods.IsChecked == true)
                    {
                        foreach (var method in type.GetMethods())
                        {
                            sb.AppendLine($"   Метод: {method.Name}");
                            foreach (var attr in method.GetCustomAttributes())
                            {
                                sb.AppendLine($"      Атрибут: [{attr.GetType().Name}]");
                            }
                        }
                    }

                    if (chkProperties.IsChecked == true)
                    {
                        foreach (var prop in type.GetProperties())
                        {
                            sb.AppendLine($"   Властивість: {prop.Name}");
                            foreach (var attr in prop.GetCustomAttributes())
                            {
                                sb.AppendLine($"      Атрибут: [{attr.GetType().Name}]");
                            }
                        }
                    }

                    if (chkFields.IsChecked == true)
                    {
                        foreach (var field in type.GetFields())
                        {
                            sb.AppendLine($"   Поле: {field.Name}");
                            foreach (var attr in field.GetCustomAttributes())
                            {
                                sb.AppendLine($"      Атрибут: [{attr.GetType().Name}]");
                            }
                        }
                    }

                    if (chkEvents.IsChecked == true)
                    {
                        foreach (var ev in type.GetEvents())
                        {
                            sb.AppendLine($"   Подія: {ev.Name}");
                            foreach (var attr in ev.GetCustomAttributes())
                            {
                                sb.AppendLine($"      Атрибут: [{attr.GetType().Name}]");
                            }
                        }
                    }

                    sb.AppendLine(new string('-', 40));
                }

                textBlock.Text = sb.ToString();
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
