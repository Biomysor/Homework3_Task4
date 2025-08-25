using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;

namespace Homework3._4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FileName = "user-data.txt";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile userStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            IsolatedStorageFileStream userStream = new IsolatedStorageFileStream(FileName, FileMode.Create, userStorage);
            StreamWriter userWriter = new StreamWriter(userStream);
            userWriter.WriteLine(InputBox.Text);
            userWriter.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile userStorage = IsolatedStorageFile.GetUserStoreForAssembly();

            if (userStorage.FileExists(FileName))
            {
                userStorage.DeleteFile(FileName);
                MessageBox.Show("Файл видалено з ізольованого сховища.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Немає файлу для видалення.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Clear();
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForAssembly();

            if (!store.FileExists(FileName))
            {
                MessageBox.Show("Файл ще не створено.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                IsolatedStorageFileStream s = new IsolatedStorageFileStream(FileName, FileMode.Open, store);

                StreamReader userReader = new StreamReader(s);
                string contents = userReader.ReadToEnd();
                MessageBox.Show(contents, "Вміст файлу", MessageBoxButton.OK, MessageBoxImage.Information);
                s.Close();
                userReader.Close();
            }
        }
    }
}
