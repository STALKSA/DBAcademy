using Microsoft.EntityFrameworkCore;
using StudentsManager.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentsManager.ValueObjects;



namespace StudentsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private AppDbContext _db = new AppDbContext();
        private ObservableCollection<Student> _students;
        //private ObservableCollection<Group> _groups;
        private Student _student;
        public MainWindow()
        {
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            CultureInfo.CurrentUICulture = new CultureInfo("ru-RU");
            Language = XmlLanguage.GetLanguage("ru-RU");

            InitializeComponent();
        }

        ~MainWindow() { _db.Dispose(); }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            await LoadDefaultData();

            //students.First(it => it.Name == "Inna")
            //    .Group = groups.First(it => it.Name == "PV112");
            //students.First(it => it.Name == "Mark")
            //    .Group = groups.First(it => it.Name == "PV113");

            //students[1].Group = groups[1];
            //students[2].Group = groups[1];
            //await _db.SaveChangesAsync();

        }


        private async void studentsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            //await using var db = new AppDbContext();
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Birthday = DateTime.Now,
                Email = ""
            };

            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
            e.NewItem = student;
        }

        private async void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            //await using var db = new AppDbContext();
            await _db.SaveChangesAsync();
        }

        private async void visitsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            if (studentsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Выберите студента", "Внимание", MessageBoxButton.OK);
                return;
            }
            var visit = new Visit()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Student = (Student)studentsDataGrid.SelectedItem,
                Subject = (Subject)subjectsDataGrid.SelectedItem
            };

            await _db.Visits.AddAsync(visit);
            await _db.SaveChangesAsync();
            e.NewItem = visit;
        }

        private async void visitsDataGrid_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                var selectedItem = visitsDataGrid.SelectedItem as Visit;
                _db.Remove(selectedItem!);
                await _db.SaveChangesAsync();
                e.CanExecute = true;
            }
        }

        private async void subjectsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var subject = new Subject()
            {
                Id = Guid.NewGuid(),
                Name = ""

            };

            await _db.Subjects.AddAsync(subject);
            await _db.SaveChangesAsync();
            e.NewItem = subject;
        }

        private async void groupsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var group = new Group()
            {
                Id = Guid.NewGuid(),
                Name = ""

            };

            await _db.Groups.AddAsync(group);
            await _db.SaveChangesAsync();
            e.NewItem = group;
        }

        private async void groupsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (groupsDataGrid.SelectedItems is not null) return;
            Group selectedGroup = (Group)groupsDataGrid.SelectedItem;
            await _db.Entry(selectedGroup)
                 .Collection(it => it.Students).LoadAsync();
            studentsDataGrid.ItemsSource = selectedGroup.Students;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Student student;
            try
            {
                student = new Student
                {
                    Id = Guid.NewGuid(),
                    Name = studentNameTextBox.Text,
                    Birthday = studentBirthdayDatePicker.SelectedDate.GetValueOrDefault(),
                    Email = studentEmailTextBox.Text,
                    Group = (Group)studentGroupComboBox.SelectedItem,
                    PassportNumber = new PassportNumber(studentPassportTextBox.Text),

                };

            } 
            
            catch (ArgumentException exc)
            {
                MessageBox.Show(exc.Message);
                return;
            }

            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
            _students.Add(student);
            studentsDataGrid.SelectedItem = student;
        }

        private async void studentsSaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (studentsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Выберите студента");
            }
            else
            {
                _student = (Student)studentsDataGrid.SelectedItem;
                studentNameTextBox.Text = _student.Name;
                studentBirthdayDatePicker.SelectedDate = _student.Birthday;
                studentEmailTextBox.Text = _student.Email;
                studentGroupComboBox.SelectedItem = _student.Group;
                await _db.SaveChangesAsync();

            }

        }

        //int _textVersion = 0;

        bool _isDefaultData = false;
        private async void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchTextBox.Text == "Поиск...") return;
            //_textVersion++;
            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                await LoadDefaultData();
                return;
            }

            await DebounceDispatcher.Debounce(1000, () => Search(searchTextBox.Text));
           

            //Debouncing(1000, () => Search(searchTextBox.Text));

            //_isDefaultData = false;


            //Debouncing
            //var text = _textVersion;
            //await Task.Delay(TimeSpan.FromMilliseconds(1000));

            //if (text == _textVersion)
            //{
            //    await Search(searchTextBox.Text);
            //}
        }
        
        private async Task LoadDefaultData()
        {
            if (_isDefaultData) return;
            _isDefaultData = true;

            List<Student> students = await _db.Students.ToListAsync();
            _students = new ObservableCollection<Student>(students);
            studentsDataGrid.ItemsSource = _students;
            subjectsDataGrid.ItemsSource = await _db.Subjects.ToListAsync();
            visitsDataGrid.ItemsSource = await _db.Visits
                .Include(visit => visit.Student)
                //.Include(visit => visit.Subject)
                .ToListAsync();

            List<Group> groups = await _db.Groups.ToListAsync();
            groupsDataGrid.ItemsSource = groups;
            //studentGroupComboBox.Items.Clear();
            studentGroupComboBox.ItemsSource = groups;
        }

        private async Task Search(string text)
        {
            _isDefaultData = false;

            var studentsMatches = await _db.Students
                            .Where(s => EF.Functions.Like(s.Name, $"%{text}%"))
                            .ToListAsync();
            studentsDataGrid.ItemsSource = studentsMatches;

            var groupsMatches = await _db.Groups
                .Where(g => g.Name.Contains(text) || g.Students!.Any(it => it.Name.Contains(text)))
                .ToListAsync();
            groupsDataGrid.ItemsSource = groupsMatches;

            var visitsMatches = await _db.Visits
                .Where(v => v.Student!.Name.Contains(text))
                .ToListAsync();
            visitsDataGrid.ItemsSource = visitsMatches;
        }

        private void SelectAllSearchTextBoxText()
        {
            if (searchTextBox.Text == "Поиск...")
            {
                searchTextBox.SelectAll();
                searchTextBox.SelectionStart = 0;
                searchTextBox.SelectionLength = searchTextBox.Text.Length;
            }
        }

        private void searchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectAllSearchTextBoxText();
        }

        private void searchTextBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SelectAllSearchTextBoxText();
        }
    }
}