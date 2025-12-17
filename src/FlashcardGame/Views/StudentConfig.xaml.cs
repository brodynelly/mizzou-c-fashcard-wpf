using FlashcardGame.ViewModels;
using FlashcardGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace FlashcardGame.Views
{
    /// <summary>
    /// Interaction logic for StudentConfig.xaml
    /// </summary>
    public partial class StudentConfig : Page
    {
        public StudentConfig()
        {
            InitializeComponent();
            this.DataContext = new StudentConfigViewModel();
        }

        // when save button is clicked check if the name of the student already exists in the folder if not create a new student and move on to classes page
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // make sure the name is valid
            if (string.IsNullOrWhiteSpace(StudentNameTextBox.Text))
            {
                MessageBox.Show("Please enter a valid student name.");
                return;
            }

            // move to saved folder and create if not created already
            string folderPath = @"Saved";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // set the file name of the current student that may be new or not and create a path with this name
            string normalizedStudentName = StudentNameTextBox.Text.Trim().ToLower();
            string fileName = $"{normalizedStudentName}.json";
            string filePath = System.IO.Path.Combine(folderPath, fileName);
            string[] files = Directory.GetFiles(folderPath);
            var options = new JsonSerializerOptions { IncludeFields = true };

            // try to find a file with the same name as the current student
            foreach (string file in files)
            {
                // if found read out the json file and copy it into a Student object and move on to the classes page while passing that student for later
                if (System.IO.Path.GetFileName(file) == $"{normalizedStudentName}.json")
                {
                    filePath = System.IO.Path.Combine(folderPath, System.IO.Path.GetFileName(file));
                    string foundFile = File.ReadAllText(filePath);
                    
                    Student studen = JsonSerializer.Deserialize<Student>(foundFile, options);
                    var mainWindo = Application.Current.MainWindow as MainWindow;
                    if (mainWindo?.MainFrame != null)
                    {
                        var classesPage = new ClassesPage(studen);
                        classesPage.DataContext = new ClassesPageViewModel { CurrentStudent = studen };
                        mainWindo.MainFrame.Navigate(classesPage);
                    }
                    
                    return;
                }
            }

            // if student not found then create a new student and create a json file for that student and move on to the classes page while passing that student for later
            Student student = new Student(StudentNameTextBox.Text.Trim());
            string json = JsonSerializer.Serialize(student, options);
            File.WriteAllText(filePath, json);

            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow?.MainFrame != null)
            {
                var classesPage = new ClassesPage(student);
                classesPage.DataContext = new ClassesPageViewModel { CurrentStudent = student };
                mainWindow.MainFrame.Navigate(classesPage);
            }
        }
    }
}
