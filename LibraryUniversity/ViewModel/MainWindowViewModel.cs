using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using LibraryUniversity.Model;
using System;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Group = LibraryUniversity.Model.Group;
using Actions = LibraryUniversity.Model.Actions;
using Excel = Microsoft.Office.Interop.Excel;


namespace LibraryUniversity.ViewModel
{
    class MainWindowViewModel: INotifyPropertyChanged
    {
        #region Fields
        private object _selected;
        private object _selectedReader;
        private int _count;
        private string _newFaculty;
        private Faculty _selectedFaculty;
        private Faculty _fSelected;
        private Faculty _tSelected;
        private Faculty _fAdd;
        private Group _gAdd;
        private Group _selectedGroup;
        private Group _sGroup;
        private List<Employee> _employee;
        private List<Teachers> _teacherNames;
        private List<Teachers> _teachers;
        private List<Student> _studentNames;
        private List<Student> _students;
        private List<Books> _books;
        private List<Actions> _givingStudent;
        private List<Actions> _givingTeacher;
        private List<Faculty> _faculty;
        private List<Group> _someGroup;
        private List<Group> _group;
        private List<Group> _groupAdd;
        private List<Group> _groupSt;
        private List<Shelving> _shelvings;
        private List<Publication> _publications;
        private List<Writer> _writers;
        private List<Books> _booksEdit;
        private bool _isSelectedTabTeacher;
        private bool _isSelectedTabStudent;
        private FileInfo _file;
        private string _source;
        private string _destination;
        private int _countAllBook;
        private int _countGiveBook;
        private string _foundText;
        private int percent;
        private int _max;
        private string _emp;
        private readonly DBUniversityLibraryEntities db = new DBUniversityLibraryEntities();
        private readonly BackgroundWorker worker;
        #endregion Fields
        #region Command
        public ICommand ClickCommandSelectedCombo { get; set; }
        public ICommand ClickCommandSelectedComboGroup { get; set; }
        public ICommand ClickCommandGiving { get; set; }
        public ICommand ClickStudentGiving { get; set; }
        public ICommand ClickTeacherGiving { get; set; }
        public ICommand ClickForGetCountGivingBook { get; set; }
        public ICommand ClickReturnBook { get; set; }
        public ICommand ClickAddNewReader { get; set; }
        public ICommand ClickAddNewFaculty { get; set; }
        public ICommand ClickDeleteFaculty { get; set; }
        public ICommand ClickEditFaculty { get; set; }
        public ICommand ClickAddNewGroup { get; set; }
        public ICommand ClickDeleteGroup { get; set; }
        public ICommand ClickEditGroup { get; set; }
        public ICommand ClickAddNewPublication { get; set; }
        public ICommand ClickDeletePublication { get; set; }
        public ICommand ClickEditPublication { get; set; }
        public ICommand ClickAddNewShelving { get; set; }
        public ICommand ClickDeleteShelving { get; set; }
        public ICommand ClickEditShelving { get; set; }
        public ICommand ClickAddNewWriter { get; set; }
        public ICommand ClickDeleteWriter { get; set; }
        public ICommand ClickEditWriter { get; set; }
        public ICommand ClickAddNewBooks { get; set; }
        public ICommand ClickDeleteBooks { get; set; }
        public ICommand ClickEditBooks { get; set; }
        public ICommand ClickLoadFile { get; set; }
        public ICommand ClickAddNewEmployee { get; set; }
        public ICommand ClickDeleteEmployee { get; set; }
        public ICommand ClickEditEmployee { get; set; }
        public ICommand ClickDeleteFile { get; set; }
        public ICommand ClickDeleteStudent { get; set; }
        public ICommand ClickDeleteTeacher { get; set; }
        public ICommand ClickGetSourceFile { get; set; }
        public ICommand ClickSetDestinationFile { get; set; }
        public ICommand ClickConvertToPdf { get; set; }
        public ICommand ClickFoundBook { get; set; }
        public ICommand ClickRefreshBook { get; set; }
        public ICommand ClickSelectedFacultiesStudent { get; set; }
        public ICommand ClickSelectedGroupsStudent { get; set; }
        public ICommand ClickSelectedFacultiesTeacher { get; set; }
        public ICommand ClickGetReport { get; set; }
        public ICommand ClickRefreshTeacher { get; set; }
        public ICommand ClickRefreshStudent { get; set; }
        public ICommand ClickCommandSelectedComboAdd { get; set; }

        #endregion Command
        #region Properties
        public List<Employee> Employees
        {
            get { return _employee; }
            set
            {
                if (_employee != value)
                {
                    _employee = value;
                    OnPropertyChanged("Employees");
                }
            }
        }
        public List<Student> StudentNames
        {
            get { return _studentNames; }
            set
            {
                if (_studentNames != value)
                {
                    _studentNames = value;
                    OnPropertyChanged("StudentNames");
                }
            }
        }
        public List<Student> Students
        {
            get { return _students; }
            set
            {
                if (_students != value)
                {
                    _students = value;
                    OnPropertyChanged("Students");
                }
            }
        }
        public List<Teachers> TeacherNames
        {
            get { return _teacherNames; }
            set
            {
                if (_teacherNames != value)
                {
                    _teacherNames = value;
                    OnPropertyChanged("TeacherNames");
                }
            }
        }
        public List<Teachers> Teachers
        {
            get { return _teachers; }
            set
            {
                if (_teachers != value)
                {
                    _teachers = value;
                    OnPropertyChanged("Teachers");
                }
            }
        }
        public List<Actions> StudentGiving
        {
            get { return _givingStudent; }
            set
            {
                if (_givingStudent != value)
                {
                    _givingStudent = value;
                    OnPropertyChanged("StudentGiving");
                }
            }
        }
        public List<Actions> TeacherGiving
        {
            get { return _givingTeacher; }
            set
            {
                if (_givingTeacher != value)
                {
                    _givingTeacher = value;
                    OnPropertyChanged("TeacherGiving");
                }
            }
        }
        public List<Faculty> Faculties
        {
            get { return _faculty; }
            set
            {
                if (_faculty != value)
                {
                    _faculty = value;
                    OnPropertyChanged("Faculties");
                }
            }
        }
        public List<Group> SomeGroups
        {
            get { return _someGroup; }
            set
            {
                if (_someGroup != value)
                {
                    _someGroup = value;
                    OnPropertyChanged("SomeGroups");
                }
            }
        }
        public List<Group> SomeGroupsSt
        {
            get { return _groupSt; }
            set
            {
                if (_groupSt != value)
                {
                    _groupSt = value;
                    OnPropertyChanged("SomeGroupsSt");
                }
            }
        }
        public List<Group> Groups
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    OnPropertyChanged("Groups");
                }
            }
        }
        public List<Books> Books
        {
            get { return _books; }
            set
            {
                if (_books != value)
                {
                    _books = value;
                    OnPropertyChanged("Books");
                }
            }
        }
        public List<Publication> Publications
        {
            get { return _publications; }
            set
            {
                if (_publications != value)
                {
                    _publications = value;
                    OnPropertyChanged("Publications");
                }
            }
        }
        public List<Shelving> Shelvings
        {
            get { return _shelvings; }
            set
            {
                if (_shelvings != value)
                {
                    _shelvings = value;
                    OnPropertyChanged("Shelvings");
                }
            }
        }
        public List<Writer> Writers
        {
            get { return _writers; }
            set
            {
                if (_writers != value)
                {
                    _writers = value;
                    OnPropertyChanged("Writers");
                }
            }
        }
        public List<Books> BooksEdit
        {
            get { return _booksEdit; }
            set
            {
                if (_booksEdit != value)
                {
                    _booksEdit = value;
                    OnPropertyChanged("BooksEdit");
                }
            }
        }
        public string SourceFile
        {
            get { return _source; }
            set
            {
                if(_source != value)
                {
                    _source = value;
                    OnPropertyChanged("SourceFile");
                }
            }
        }
        public string DestinationFile
        {
            get { return _destination; }
            set
            {
                if (_destination != value)
                {
                    _destination = value;
                    OnPropertyChanged("DestinationFile");
                }
            }
        }
        public object SelectedItem
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }
        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (_selectedGroup != value)
                {
                    _selectedGroup = value;
                    OnPropertyChanged("SelectedGroup");
                }
            }
        }
        public object SelectedReader
        {
            get { return _selectedReader; }
            set
            {
                if (_selectedReader != value)
                {
                    _selectedReader = value;
                    OnPropertyChanged("SelectedReader");
                }
            }
        }
        public Faculty SelectedFaculty
        {
            get { return _selectedFaculty; }
            set
            {
                if (_selectedFaculty != value)
                {
                    _selectedFaculty = value;
                    OnPropertyChanged("SelectedFaculty");
                }
            }
        }
        public int BookGivingCount
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged("BookGivingCount");
                }
            }
        }
        public bool IsSelectedTeacher
        {
            get { return _isSelectedTabTeacher; }
            set
            {
                if (_isSelectedTabTeacher != value)
                {
                    _isSelectedTabTeacher = value;
                    OnPropertyChanged("IsSelectedTeacher");
                }
            }
        }
        public bool IsSelectedStudent
        {
            get { return _isSelectedTabStudent; }
            set
            {
                if (_isSelectedTabStudent != value)
                {
                    _isSelectedTabStudent = value;
                    OnPropertyChanged("IsSelectedStudent");
                }
            }
        }
        public string NewFaculty
        {
            get { return _newFaculty; }
            set
            {
                if (_newFaculty != value)
                {
                    _newFaculty = value;
                    OnPropertyChanged("NewFaculty");
                }
            }
        }
        public int CountAllBook
        {
            get { return _countAllBook; }
            set
            {
                if (_countAllBook != value)
                {
                    _countAllBook = value;
                    OnPropertyChanged("CountAllBook");
                }
            }
        }
        public int CountGiveBook
        {
            get { return _countGiveBook; }
            set
            {
                if (_countGiveBook != value)
                {
                    _countGiveBook = value;
                    OnPropertyChanged("CountGiveBook");
                }
            }
        }
        public string TextFound
        {
            get { return _foundText; }
            set
            {
                if (_foundText != value)
                {
                    _foundText = value;
                    OnPropertyChanged("TextFound");
                }
            }
        }
        public FileInfo File
        {
            get { return _file; }
            set
            {
                if (_file != value)
                {
                    _file = value;
                    OnPropertyChanged("File");
                }
            }
        }
        public int Max
        {
            get { return _max; }
            set
            {
                if (_max != value)
                {
                    _max = value;
                    OnPropertyChanged("Max");
                }
            }
        }
        public Faculty SelectedfacultyStudent
        {
            get { return _fSelected; }
            set
            {
                if (_fSelected != value)
                {
                    _fSelected = value;
                    OnPropertyChanged("SelectedfacultyStudent");
                }
            }
        }
        public Group SelectedgroupStudent
        {
            get { return _sGroup; }
            set
            {
                if (_sGroup != value)
                {
                    _sGroup = value;
                    OnPropertyChanged("SelectedgroupStudent");
                }
            }
        }
        public Faculty SelectedfacultyTeacher
        {
            get { return _tSelected; }
            set
            {
                if (_tSelected != value)
                {
                    _tSelected = value;
                    OnPropertyChanged("SelectedfacultyTeacher");
                }
            }
        }
        public int CurrentProgress
        {
            get { return percent; }
            set
            {
                if (percent != value)
                {
                    percent = value;
                    OnPropertyChanged("CurrentProgress");
                }
            }
        }
        public Faculty SelectedFacultyAddNew
        {
            get { return _fAdd; }
            set
            {
                if (_fAdd != value)
                {
                    _fAdd = value;
                    OnPropertyChanged("SelectedFacultyAddNew");
                }
            }
        }
        public Group SelectedGroupAddNew
        {
            get { return _gAdd; }
            set
            {
                if (_gAdd != value)
                {
                    _gAdd = value;
                    OnPropertyChanged("SelectedGroupAddNew");
                }
            }
        }
        public List<Group> SomeGroupsAdd
        {
            get { return _groupAdd; }
            set
            {
                if (_groupAdd != value)
                {
                    _groupAdd = value;
                    OnPropertyChanged("SomeGroupsAdd");
                }
            }
        }
        public string EmployeeCurrent
        {
            get { return _emp; }
            set
            {
                if (_emp != value)
                {
                    _emp = value;
                    OnPropertyChanged("EmployeeCurrent");
                }
            }
        }
        #endregion Properties

        public MainWindowViewModel()
        {          
            Employees = db.Employee.Select(emp => emp).ToList();
            Books = db.Books.Select(book => book).OrderBy(book => book.name).ToList();
            Faculties = db.Faculty.Select(faculty => faculty).OrderBy(f => f.name).ToList();
            Students = db.Student.Select(student => student).OrderBy(s=> s.surname).ToList();
            Teachers = db.Teachers.Select(teacher => teacher).OrderBy(t => t.surname).ToList();
            Groups = db.Group.Select(group => group).OrderBy(g => g.name).ToList();
            Publications = db.Publication.Select(publication => publication).OrderBy(p=> p.name).ToList();
            Shelvings = db.Shelving.Select(shelving => shelving).OrderBy(shelving => shelving.subject).ToList();
            Writers = db.Writer.Select(writer => writer).OrderBy(w => w.surname).ToList();
            BooksEdit = db.Books.Select(book => book).OrderBy(b=>b.name).ToList();
            CountAllBook = db.Books.Count();
            CountGiveBook = db.Actions.Count(a => a.id_status == 1);

            ClickCommandSelectedCombo = new Command(args => SelectedFromCombo());
            ClickCommandSelectedComboGroup = new Command(args => SelectedFromComboGroup());
            ClickCommandGiving = new Command(args => GivingBook());
            ClickStudentGiving = new Command(StudentGivingBook);
            ClickTeacherGiving = new Command(TeacherGivingBook);
            ClickForGetCountGivingBook = new Command(GetCountGivingBook);
            ClickReturnBook = new Command(ReturnBook);
            ClickAddNewReader = new Command(AddNewReader);
            ClickAddNewFaculty = new Command(AddNewFaculty);
            ClickDeleteFaculty = new Command(DeleteFaculty);
            ClickEditFaculty = new Command(EditFaculty);
            ClickAddNewGroup = new Command(AddNewGroup);
            ClickDeleteGroup = new Command(DeleteGroup);
            ClickEditGroup = new Command(EditGroup);
            ClickAddNewPublication = new Command(AddNewPublication);
            ClickDeletePublication = new Command(DeletePublication);
            ClickEditPublication = new Command(EditPublication);
            ClickAddNewShelving = new Command(AddNewShelving);
            ClickDeleteShelving = new Command(DeleteShelving);
            ClickEditShelving = new Command(EditShelving);
            ClickAddNewWriter = new Command(AddNewWriter);
            ClickDeleteWriter = new Command(DeleteWriter);
            ClickEditWriter = new Command(EditWriter);
            ClickAddNewBooks= new Command(AddNewBook);
            ClickDeleteBooks = new Command(DeleteBook);
            ClickEditBooks = new Command(EditBook);
            ClickLoadFile = new Command(args => LoadFile());
            ClickDeleteFile = new Command(DeleteFile);
            ClickAddNewEmployee = new Command(AddNewEmpl);
            ClickDeleteEmployee = new Command(DeleteEmpl);
            ClickEditEmployee = new Command(EditEmpl);
            ClickDeleteStudent = new Command(DeleteStudent);
            ClickDeleteTeacher = new Command(DeleteTeacher);
            ClickGetSourceFile = new Command(args => GetSourceFile());
            ClickSetDestinationFile = new Command(args => SetDestinationFile());
            ClickConvertToPdf = new Command(args => ConvertToPdf());
            ClickFoundBook = new Command(args => FoundBook());
            ClickRefreshBook = new Command(args => RefreshBook());
            ClickSelectedFacultiesStudent = new Command(SelectedFacultyStudent);
            ClickSelectedGroupsStudent = new Command(SelectedGroupStudent);
            ClickSelectedFacultiesTeacher = new Command(SelectedFacultyTeacher);
            ClickGetReport = new Command(args => GetReport());
            ClickRefreshStudent = new Command(args => GetRefreshStudent());
            ClickRefreshTeacher = new Command(args => GetRefreshTeacher());
            ClickCommandSelectedComboAdd = new Command(args => CommandSelectedComboAdd());
            worker = new BackgroundWorker();
            Max = 100;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        /// <summary>
        /// Обробник події коли завершує роботу BackgroundWorker
        /// </summary>
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                MessageBox.Show("Конвертування завершено успішно!");
                CurrentProgress = 0;
            }
        }

        /// <summary>
        /// Обробник події для зміни значення progressbar
        /// </summary>
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = e.ProgressPercentage;
        }

        /// <summary>
        /// Після вибору факультет відібрати відповідні групи на гооловній вкладці 
        /// </summary>
        private void SelectedFromCombo()
        {
            if (SelectedFaculty != null)
            {
                SomeGroups = db.Group.Where(group => group.id_faculty == SelectedFaculty.id).ToList();
            }
            if(SelectedGroup == null)
            {
                TeacherNames = db.Teachers.Where(teacher => teacher.id_faculty == SelectedFaculty.id).ToList();
            }
        }

        /// <summary>
        /// Після вибору факультету відібрати відповідні групи на вкладці додавання читача
        /// </summary>
        private void CommandSelectedComboAdd()
        {
            if (SelectedFacultyAddNew != null)
            {
                SomeGroupsAdd = db.Group.Where(group => group.id_faculty == SelectedFacultyAddNew.id).ToList();
            }
        }

        /// <summary>
        /// Після вибору групи вибрати відповідних студентів
        /// </summary>
        private void SelectedFromComboGroup()
        {
            if (SelectedFaculty != null && SelectedGroup != null)
            {
                StudentNames =
                    db.Student.Where(
                        student => student.id_group == SelectedGroup.id).
                        ToList();
            }
        }

        /// <summary>
        /// Фільтр по факультетах для вкладки студенти
        /// </summary>
        /// <param name="param">вибраний факультет</param>
        private void SelectedFacultyStudent(object param)
        {
            var selected = param as Faculty;
            if(selected != null)
            {
                SomeGroupsSt = db.Group.Where(group => group.id_faculty == selected.id).ToList();
                Students = db.Student.Where(s => s.Group.Faculty.name == selected.name).ToList();
            }
        }

        /// <summary>
        /// Фільтр по факультетах для вкладки викладачі
        /// </summary>
        /// <param name="param">вибраний факультет</param>
        private void SelectedFacultyTeacher(object param)
        {
            var selected = param as Faculty;
            if (selected != null)
            {
                Teachers = db.Teachers.Where(t => t.Faculty.name == selected.name).ToList();
            }
        }

        /// <summary>
        /// Фільтр по групах для вкладки студенти
        /// </summary>
        /// <param name="param">вибрана група</param>
        private void SelectedGroupStudent(object param)
        {
            var selected = param as Group;
            if (selected != null)
            {
                Students = db.Student.Where(s => s.Group.name == selected.name).ToList();
            }
        }

        /// <summary>
        /// Видати книгу читачу
        /// </summary>
        private void GivingBook()
        {
            var student = SelectedReader as Student;
            var teacher = SelectedReader as Teachers;
            var book = SelectedItem as Books;
            var emp = db.Employee.First(e => e.surname == EmployeeCurrent);
            Actions giving;
            if (book == null)
            {
                MessageBox.Show("Виберіть книгу!");
                return;
            }
            if(student != null)
            {
                giving = new Actions
                {
                    date = DateTime.Now,
                    id_book = book.id,
                    id_student = student.id,
                    id_teacher = null,
                    id_employee = emp.id,
                    id_status = 1
                };
            }

            else if(teacher != null)
            {
                giving = new Actions
                {
                    date = DateTime.Now,
                    id_book = book.id,
                    id_student = null,
                    id_teacher = teacher.id,
                    id_employee = emp.id,
                    id_status = 1
                };
            }
            else
            {
                MessageBox.Show("Виберіть читача!");
                return;
            }

            if (book.count == BookGivingCount)
            {
                MessageBox.Show("Всі примірники книги видані!");
                return;
            }
            
            db.AddToActions(giving);
            db.SaveChanges();
            GetCountGivingBook(book);
            MessageBox.Show("Книга успішно видана!");
        }

        /// <summary>
        /// Отримати книжки які брав студент
        /// </summary>
        /// <param name="param">студент</param>
        private void StudentGivingBook(object param)
        {
            var student = param as Student;
            if (student == null) return;
            var query = db.Actions.Where(r => r.id_student == student.id && r.id_status == 1).ToList();
            StudentGiving = query.Count != 0 ? query.ToList() : null;
        }

        /// <summary>
        /// Отримати книжки які брав викладач
        /// </summary>
        /// <param name="param">викладач</param>
        private void TeacherGivingBook(object param)
        {
            var teacher = param as Teachers;
            if (teacher == null) return;
            var query = db.Actions.Where(r => r.id_teacher == teacher.id && r.id_status == 1).ToList();
            TeacherGiving = query.Count != 0 ? query.ToList() : null;
        }

        /// <summary>
        /// Підрахунок кількості виданих примірників
        /// </summary>
        /// <param name="param">книжка</param>
        private void GetCountGivingBook(object param)
        {
            var book = param as Books;
            if(book != null)
            {
                BookGivingCount = db.Actions.Count(g => g.id_book == book.id & g.id_status == 1);
                CountGiveBook = db.Actions.Count(a => a.id_status == 1);
            }
        }

        /// <summary>
        /// Повернення книги
        /// </summary>
        /// <param name="param">книга</param>
        private void ReturnBook(object param)
        {
            var give = param as Actions;
            if(give != null)
            {
                give.id_status = 2;
                db.SaveChanges();
                CountGiveBook = db.Actions.Count(a => a.id_status == 1);
                if (IsSelectedStudent) StudentGiving = db.Actions.Where(g => g.id_student == give.id_student && g.id_status == 1).ToList();
                if (IsSelectedTeacher) TeacherGiving = db.Actions.Where(g => g.id_teacher == give.id_teacher && g.id_status == 1).ToList();
                MessageBox.Show("Повернення книги успішно здійснено!");
            }
        }
       
        /// <summary>
        /// Додати нового читача
        /// </summary>
        /// <param name="param">нові дані</param>
        private void AddNewReader(object param)
        {
            var newReader = param as NewReader;
            if (newReader == null) return;
            if (newReader.NewSurname == string.Empty)
            {
                MessageBox.Show("Вкажіть призвіще читата!");
                return;
            }
            if (newReader.NewName == string.Empty)
            {
                MessageBox.Show("Вкажіть ім'я читата!");
                return;
            }
            if (newReader.NewFaculty == null)
            {
                MessageBox.Show("Вкажіть факультет читата!");
                return;
            }
            if(newReader.isStudent)
            {
                var newStudent = new Student
                                     {
                                         surname = newReader.NewSurname,
                                         name = newReader.NewName,
                                         patronymic = newReader.NewPatronymic,
                                         course = newReader.NewCourse,
                                         id_faculty = newReader.NewFaculty.id,
                                         id_group = newReader.NewGroup.id
                                     };
                db.AddToStudent(newStudent);
                db.SaveChanges();
                MessageBox.Show("Новий читач успішно додано!");
            }
            if(newReader.isTeacher)
            {
                var newTeacher = new Teachers
                                     {
                                         surname = newReader.NewSurname,
                                         name = newReader.NewName,
                                         patronymic = newReader.NewPatronymic,
                                         id_faculty = newReader.NewFaculty.id
                                     };
                db.AddToTeachers(newTeacher);
                db.SaveChanges();
                MessageBox.Show("Новий читач успішно додано!");
            }
            Students = db.Student.Select(student => student).ToList();
            Teachers = db.Teachers.Select(teacher => teacher).ToList();
        }

        /// <summary>
        /// Додати новий факультет
        /// </summary>
        /// <param name="param">нові дані</param>
        private void AddNewFaculty(object param)
        {
            var faculty = param.ToString();
            if(faculty != string.Empty)
            {
                var existed = db.Faculty.Where(f => f.name == faculty).ToList();
                if (existed.Count > 0)
                {
                    var result =
                        MessageBox.Show("Item with this name already exist. Do you add item with the same name?",
                                        "Existed", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        var newFaculty = new Faculty {name = faculty};
                        db.AddToFaculty(newFaculty);
                        db.SaveChanges();
                        Faculties = db.Faculty.Select(f => f).ToList();
                    }
                }
                else
                {
                    var newFaculty = new Faculty { name = faculty };
                    db.AddToFaculty(newFaculty);
                    db.SaveChanges();
                    Faculties = db.Faculty.Select(f => f).ToList();
                }
            } 
        }

        /// <summary>
        /// Видалити факультет
        /// </summary>
        /// <param name="param">факультет</param>
        private void DeleteFaculty(object param)
        {
            var faculty = param as Faculty;
            if (faculty != null)
            {
                db.DeleteObject(faculty);
                db.SaveChanges();
                Faculties = db.Faculty.Select(f => f).ToList();
            }
        }

        /// <summary>
        /// Редагувати факультет
        /// </summary>
        /// <param name="param">факультет</param>
        private void EditFaculty(object param)
        {
            var faculty = param.ToString();
            var selectedfaculty = SelectedItem as Faculty;
            if (selectedfaculty != null && faculty != string.Empty)
            {
                selectedfaculty.name = faculty;
                db.SaveChanges();
                Faculties = db.Faculty.Select(f => f).ToList();
            }
        }

        /// <summary>
        /// Додати нову групу
        /// </summary>
        /// <param name="param">нові дані</param>
        private void AddNewGroup(object param)
        {
            var group = param as NewGroup;
            if (group != null)
            {
                var existed = db.Group.Where(f => f.name == group.Name).ToList();
                if (existed.Count > 0)
                {
                    var result =
                        MessageBox.Show("Item with this name already exist. Do you add item with the same name?",
                                        "Existed", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        var newGroup = new Group { name = group.Name, id_faculty = group.Faculty.id};
                        db.AddToGroup(newGroup);
                        db.SaveChanges();
                        Groups = db.Group.Select(g => g).ToList();
                    }
                }
                else
                {
                    var newGroup = new Group { name = group.Name, id_faculty = group.Faculty.id };
                    db.AddToGroup(newGroup);
                    db.SaveChanges();
                    Groups = db.Group.Select(g => g).ToList();
                }
            }
        }

        /// <summary>
        /// Видалити групу
        /// </summary>
        /// <param name="param">група</param>
        private void DeleteGroup(object param)
        {
            var group = param as Group;
            if (group != null)
            {
                db.DeleteObject(group);
                db.SaveChanges();
                Groups = db.Group.Select(g => g).ToList();
            }
        }

        /// <summary>
        /// Редагувати групу
        /// </summary>
        /// <param name="param">група</param>
        private void EditGroup(object param)
        {
            var group = param as NewGroup;
            var selected = SelectedItem as Group;            
            if (selected != null && group != null)
            {
                selected.name = group.Name;
                selected.id_faculty = group.Faculty.id;
                db.SaveChanges();
                Groups = db.Group.Select(g => g).ToList();
            }
        }

        /// <summary>
        /// Додати нове видавництво
        /// </summary>
        /// <param name="param">нові дані</param>
        private void AddNewPublication(object param)
        {
            var publication = param.ToString();
            if (publication != string.Empty)
            {
                var existed = db.Publication.Where(f => f.name == publication).ToList();
                if (existed.Count > 0)
                {
                    var result =
                        MessageBox.Show("Item with this name already exist. Do you add item with the same name?",
                                        "Existed", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        var newPublication = new Publication { name = publication };
                        db.AddToPublication(newPublication);
                        db.SaveChanges();
                        Publications = db.Publication.Select(p => p).ToList();
                    }
                }
                else
                {
                    var newPublication = new Publication { name = publication };
                    db.AddToPublication(newPublication);
                    db.SaveChanges();
                    Publications = db.Publication.Select(p => p).ToList();
                }
            }
        }

        /// <summary>
        /// Видалити видавництво
        /// </summary>
        /// <param name="param">видавництво</param>
        private void DeletePublication(object param)
        {
            var publication = param as Publication;
            if (publication != null)
            {
                db.DeleteObject(publication);
                db.SaveChanges();
                Publications = db.Publication.Select(p => p).ToList();
            }
        }

        /// <summary>
        /// Редагувати факультет
        /// </summary>
        /// <param name="param">видавництво</param>
        private void EditPublication(object param)
        {
            var publication = param.ToString();
            var selected = SelectedItem as Publication;
            if (selected != null && publication != string.Empty)
            {
                selected.name = publication;
                db.SaveChanges();
                Publications = db.Publication.Select(p => p).ToList();
            }
        }

        /// <summary>
        /// Додати нову полицю
        /// </summary>
        /// <param name="param">нові дані</param>
        private void AddNewShelving(object param)
        {
            var shelving = param.ToString();
            if (shelving != string.Empty)
            {
                var existed = db.Shelving.Where(f => f.subject == shelving).ToList();
                if (existed.Count > 0)
                {
                    var result =
                        MessageBox.Show("Item with this subject already exist. Do you add item with the same subject?",
                                        "Existed", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        var newShelving = new Shelving { subject = shelving };
                        db.AddToShelving(newShelving);
                        db.SaveChanges();
                        Shelvings = db.Shelving.Select(sh => sh).ToList();
                    }
                }
                else
                {
                    var newShelving = new Shelving { subject = shelving };
                    db.AddToShelving(newShelving);
                    db.SaveChanges();
                    Shelvings = db.Shelving.Select(sh => sh).ToList();
                }
            }
        }

        /// <summary>
        /// Видалити полицю
        /// </summary>
        /// <param name="param">полиця</param>
        private void DeleteShelving(object param)
        {
            var shelving = param as Shelving;
            if (shelving != null)
            {
                db.DeleteObject(shelving);
                db.SaveChanges();
                Shelvings = db.Shelving.Select(sh => sh).ToList();
            }
        }

        /// <summary>
        /// Редагувати полицю
        /// </summary>
        /// <param name="param">полиця</param>
        private void EditShelving(object param)
        {
            var shelving = param.ToString();
            var selected = SelectedItem as Shelving;
            if (selected != null && shelving != string.Empty)
            {
                selected.subject = shelving;
                db.SaveChanges();
                Shelvings = db.Shelving.Select(sh => sh).ToList();
            }
        }

        /// <summary>
        /// Додати нового автора
        /// </summary>
        /// <param name="param">нові дані</param>
        private void AddNewWriter(object param)
        {
            var writer = param as NewWriter;
            if (writer != null)
            {
                var existed = db.Writer.Where(wr => wr.surname == writer.Surname && wr.name == writer.Name && wr.patronymic == writer.Patronymic).ToList();
                if (existed.Count > 0)
                {
                    var result =
                        MessageBox.Show("Item with this subject already exist. Do you add item with the same subject?",
                                        "Existed", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        var newWriter = new Writer { surname = writer.Surname, name = writer.Name, patronymic = writer.Patronymic};
                        db.AddToWriter(newWriter);
                        db.SaveChanges();
                        Writers = db.Writer.Select(w => w).ToList();
                    }
                }
                else
                {
                    var newWriter = new Writer { surname = writer.Surname, name = writer.Name, patronymic = writer.Patronymic };
                    db.AddToWriter(newWriter);
                    db.SaveChanges();
                    Writers = db.Writer.Select(w => w).ToList();
                }
            }
        }

        /// <summary>
        /// Видалити автора
        /// </summary>
        /// <param name="param">автор</param>
        private void DeleteWriter(object param)
        {
            var writer = param as Writer;
            if (writer != null)
            {
                db.DeleteObject(writer);
                db.SaveChanges();
                Writers = db.Writer.Select(w => w).ToList();
            }
        }

        /// <summary>
        /// Редагувати автора
        /// </summary>
        /// <param name="param">автор</param>
        private void EditWriter(object param)
        {
            var writer = param as NewWriter;
            var selected = SelectedItem as Writer;
            if (selected != null && writer != null)
            {
                selected.surname = writer.Surname;
                selected.name = writer.Name;
                selected.patronymic = writer.Patronymic;
                db.SaveChanges();
                Writers = db.Writer.Select(w => w).ToList();
            }
        }

        /// <summary>
        /// Додати нову книгу
        /// </summary>
        /// <param name="param">нові дані</param>
        private void AddNewBook(object param)
        {
            var book = param as NewBook;
            var ebook = new EBook();
            var newBook = new Books();
            if (book != null)
            {
                if (File != null)
                {
                    ebook.size = File.Length;
                    ebook.type = File.Extension;
                    ebook.adress = File.FullName;
                    db.AddToEBook(ebook);
                    db.SaveChanges();
                    newBook.id_ebook = ebook.id;
                }
                else
                {
                    newBook.id_ebook = null;
                }
                
                newBook.name = book.Name;
                newBook.id_writer = book.Writer.id;
                newBook.id_publication = book.Publication.id;
                newBook.ISBN = book.ISBN;
                newBook.pages = book.Pages;
                newBook.year = book.Year;
                newBook.count = book.Count;

                if(book.Shelving != null) newBook.id_shelving = book.Shelving.id;
                
                db.AddToBooks(newBook);
                db.SaveChanges();
                CountAllBook = db.Books.Count();
                BooksEdit = db.Books.Select(b => b).ToList();
                Books = BooksEdit;
            }         
        }

        /// <summary>
        /// Видалити книгу
        /// </summary>
        /// <param name="param">книга</param>
        private void DeleteBook(object param)
        {
            var book = param as Books;
            if (book != null)
            {
                var giving = db.Actions.Where(a => a.id_book == book.id && a.id_status == 1).ToList();
                var returns = db.Actions.Where(a => a.id_book == book.id && a.id_status == 2).ToList();
                if (giving.Count > 0)
                {
                    MessageBox.Show("Дана книга знаходиться у використанні!");
                    return;    
                }
                if(returns.Count > 0)
                {
                    foreach (var action in returns)
                    {
                        db.DeleteObject(action);
                        db.SaveChanges();
                    }
                }
                db.DeleteObject(book);
                db.SaveChanges();
                CountAllBook = db.Books.Count();
                BooksEdit = db.Books.Select(b => b).ToList();
                Books = BooksEdit;
            }
        }

        /// <summary>
        /// Редагувати книгу
        /// </summary>
        /// <param name="param">книга</param>
        private void EditBook(object param)
        {
            var ebook = new EBook();
            var book = param as NewBook;
            var selected = SelectedItem as Books;
            if (selected != null && book != null)
            {
                selected.name = book.Name;
                selected.id_writer = book.Writer.id;
                selected.id_publication = book.Publication.id;
                selected.ISBN = book.ISBN;
                selected.pages = book.Pages;
                selected.count = book.Count;
                selected.year = book.Year;
                if (book.Shelving != null) selected.id_shelving = book.Shelving.id;
                if (File != null)
                {
                    if(selected.EBook == null)
                    {
                        ebook.size = File.Length;
                        ebook.type = File.Extension;
                        ebook.adress = File.FullName;
                        db.AddToEBook(ebook);
                        db.SaveChanges();
                        selected.EBook = ebook;
                    }
                    if (selected.EBook != null)
                    {
                        selected.EBook.size = File.Length;
                        selected.EBook.type = File.Extension;
                        selected.EBook.adress = File.FullName;
                    }
                }
                db.SaveChanges();
                BooksEdit = db.Books.Select(b => b).ToList();
                Books = BooksEdit;
            }
        }

        /// <summary>
        /// Завантажити файл для книги
        /// </summary>
        private void LoadFile()
        {
            var openFileDialog = new OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();

            if (result != true) return;
            File = new FileInfo(openFileDialog.FileName);
        }

        /// <summary>
        /// Додати нового співробітника
        /// </summary>
        /// <param name="param">нові дані</param>
        private void AddNewEmpl(object param)
        {
            var employee = param as NewEmployee;
            if (employee != null)
            {
                var existed = db.Employee.Where(e => e.surname == employee.Surname && e.name == employee.Name && e.patronymic == employee.Patronymic).ToList();
                if (existed.Count > 0)
                {
                    var result =
                        MessageBox.Show("Item with this subject already exist. Do you add item with the same subject?",
                                        "Existed", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        var newEmployee = new Employee {surname = employee.Surname, name = employee.Name, patronymic = employee.Patronymic, permission = employee.Permission, password = employee.Password};
                        db.AddToEmployee(newEmployee);
                        db.SaveChanges();
                        Employees = db.Employee.Select(e => e).ToList();
                    }
                }
                else
                {
                    var newEmployee = new Employee { surname = employee.Surname, name = employee.Name, patronymic = employee.Patronymic, permission = employee.Permission, password = employee.Password };
                    db.AddToEmployee(newEmployee);
                    db.SaveChanges();
                    Employees = db.Employee.Select(e => e).ToList();
                }
            }
        }

        /// <summary>
        /// Видалити співробітника
        /// </summary>
        /// <param name="param">співробітника</param>
        private void DeleteEmpl(object param)
        {
            var employee = param as Employee;
            if (employee != null)
            {
                db.DeleteObject(employee);
                db.SaveChanges();
                Employees = db.Employee.Select(e => e).ToList();
            }
        }

        /// <summary>
        /// Редагувати співробітника
        /// </summary>
        /// <param name="param">співробітника</param>
        private void EditEmpl(object param)
        {
            var employee = param as NewEmployee;
            var selected = SelectedItem as Employee;
            if (selected != null && employee != null)
            {
                selected.surname = employee.Surname;
                selected.name = employee.Name;
                selected.patronymic = employee.Patronymic;
                selected.permission = employee.Permission;
                selected.password = employee.Password;
                db.SaveChanges();
                Writers = db.Writer.Select(w => w).ToList();
            }
        }

        /// <summary>
        /// Видалити файл книги
        /// </summary>
        /// <param name="param">файл</param>
        private void DeleteFile(object param)
        {
            var file = param as Books;
            if(file != null)
            {
                var fileinfo = new FileInfo(file.EBook.adress);
                if (fileinfo.Exists)
                {
                    var result = MessageBox.Show("Файл буде видалено без можливості відновлення. Ви бажаєте продовжити?", "Попередження", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        db.DeleteObject(file.EBook);
                        db.SaveChanges();
                        fileinfo.Delete();
                        File = null;
                        BooksEdit = db.Books.Select(b => b).ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Файл не знайдено.");
                }
            }
            
        }

        /// <summary>
        /// Видалити студента
        /// </summary>
        /// <param name="param">студента</param>
        private void DeleteStudent(object param)
        {
            var student = param as Student;
            if (student != null)
            {
                db.DeleteObject(student);
                db.SaveChanges();
                Students = db.Student.Select(s => s).ToList();
            }
        }

        /// <summary>
        /// Видалити викладача
        /// </summary>
        /// <param name="param">викладач</param>
        private void DeleteTeacher(object param)
        {
            var teacher = param as Teachers;
            if (teacher != null)
            {
                db.DeleteObject(teacher);
                db.SaveChanges();
                Teachers = db.Teachers.Select(t => t).ToList();
            }
        }

        /// <summary>
        /// Папка вихідна з картинками
        /// </summary>
        private void GetSourceFile()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SourceFile = dialog.SelectedPath;
                DestinationFile = SourceFile + "\\Book.pdf";
            }
        }

        /// <summary>
        /// Папка з результуючим файлом
        /// </summary>
        private void SetDestinationFile()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DestinationFile = dialog.SelectedPath+"\\Book.pdf";
            }
        }

        /// <summary>
        /// Конвертувати
        /// </summary>
        private void ConvertToPdf()
        {
            if(!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Конвертація в фоновому потоці
        /// </summary>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SourceFile == string.Empty)
            {
                MessageBox.Show("Папка з файлами не вибрана!", "Помилка");
                return;
            }

            if (DestinationFile == string.Empty)
            {
                MessageBox.Show("Папка для збереження файлу не вибрана!", "Помилка");
                return;
            }

            var searchPattern = new Regex(
                @"$(?<=\.(jpg|gif|png|bmp|jpe|jpeg))",
                RegexOptions.IgnoreCase);
            var _sourceFiles = Directory.GetFiles(SourceFile).Where(f => searchPattern.IsMatch(f)).ToList();
            if(_sourceFiles.Count == 0)
            {
                MessageBox.Show("У вибраній папці немає картинок/фотографій!","Помилка");
                return;
            }
            var doc = new PdfDocument();
            Max = _sourceFiles.Count;
            try
            {
                foreach (var sourceFile in _sourceFiles)
                {
                    XImage xImage = XImage.FromFile(sourceFile);
                    var page = doc.AddPage();
                    page.Height = xImage.Height;
                    page.Width = xImage.Width;
                    var gfx = XGraphics.FromPdfPage(page);
                    gfx.DrawImage(xImage, 0, 0, xImage.Width, xImage.Height);
                    CurrentProgress++;
                }
                doc.Save(DestinationFile);
                doc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка");
            }
        }

        /// <summary>
        /// Пошук в книгах
        /// </summary>
        private void FoundBook()
        {
            if (TextFound != "")
            {
                Books = db.Books.Where(b => b.name.Contains(TextFound) || b.Writer.surname.Contains(TextFound)).ToList();
            }
        }

        private void RefreshBook()
        {
            TextFound = "";
            Books = db.Books.Select(book => book).ToList();
        }

        /// <summary>
        /// Отримати звіт про найпопулярніші книги
        /// </summary>
        private void GetReport()
        {
            var application = new Excel.Application {Visible = true};
            application.Workbooks.Add(Type.Missing);
            var excelsheets = application.ActiveWorkbook.Worksheets;
            var excelworksheet = (Worksheet)excelsheets.Item[1];
            var report = (db.Actions.Join(db.Books, a => a.id_book, b => b.id, (a, b) => new {a, b}).Where(
                @t => @t.a.date.Value.Month == DateTime.Today.Month).GroupBy(
                    @t => new {Name = @t.b.name, WriterName = @t.b.Writer.name, WriterSurname = @t.b.Writer.surname},
                    @t => @t.b).OrderByDescending(gr => gr.Count()).Select(
                        gr => new {gr.Key.Name, gr.Key.WriterSurname, gr.Key.WriterName, Num = gr.Count()})).ToList();
            excelworksheet.Cells[1, 1].Value2 = "Назва книги:";
            excelworksheet.Cells[1, 2].Value2 = "Призвіще автора";
            excelworksheet.Cells[1, 3].Value2 = "Ім'я автора";
            excelworksheet.Cells[1, 4].Value2 = "Кількість";
            excelworksheet.Rows[1].Font.Bold = true;
            int i = 2;
            foreach (var item in report)
            {
                excelworksheet.Cells[i, 1].Value2 = item.Name;
                excelworksheet.Cells[i, 2].Value2 = item.WriterSurname;
                excelworksheet.Cells[i, 3].Value2 = item.WriterName;
                excelworksheet.Cells[i, 4].Value2 = item.Num;
                i++;
            }
        }

        private void GetRefreshStudent()
        {
            SelectedfacultyStudent = null;
            SelectedgroupStudent = null;
            Students = db.Student.Select(student => student).OrderBy(s => s.surname).ToList();
        }

        private void GetRefreshTeacher()
        {
            SelectedfacultyTeacher = null;
            Teachers = db.Teachers.Select(teacher => teacher).OrderBy(t => t.surname).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
