using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
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
using System.Windows.Shapes;

namespace Text_editor
{
    public static class CopiedText
    {
        public static string? CopiedString { get; set; } = null;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string FilePath = JsonPath.Text;
                string readString = File.ReadAllText(FilePath);
                MainTextBox.Text = readString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; // Set the file filters
            saveDialog.DefaultExt = "txt"; // Set the default file extension
            saveDialog.Title = "Save File"; // Set the dialog title
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            bool? result = saveDialog.ShowDialog();
            if (result == true)
            {
                string fileName = saveDialog.FileName;
                // Save the file using the fileName
                File.WriteAllText(fileName, MainTextBox.Text);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {


                var CheckBox = sender as CheckBox;
                if (CheckBox?.IsChecked == true)
                {
                    Save_Button.IsEnabled = false;


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            var CheckBox = sender as CheckBox;
            if (CheckBox?.IsChecked == false) { Save_Button.IsEnabled = true; }
        }

        private void AutoCheck_Checked(object sender, KeyEventArgs e)
        {
            if (AutoSave.IsChecked == true)
            {
                string path = @"C:\Users\Crash\Desktop\NewAutoSave.txt";
                File.WriteAllText(path, MainTextBox.Text);
            }
        }

        private void CopyText_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox.SelectionLength > 0)
            {
                MainTextBox.Select(MainTextBox.SelectionStart, MainTextBox.SelectionLength);
                CopiedText.CopiedString = MainTextBox.SelectedText;
            }
            else
            {
                MainTextBox.SelectAll();
                CopiedText.CopiedString = MainTextBox.SelectedText;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CopiedText.CopiedString != null)
            {
                StringBuilder temp = new StringBuilder(MainTextBox.Text);
                temp.Insert(MainTextBox.CaretIndex, CopiedText.CopiedString);
                MainTextBox.Text = temp.ToString();
            }
        }

        private void Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            JsonPath.SelectAll();
        }
    }
}
