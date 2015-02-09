using LibraryUniversity.Model;

namespace LibraryUniversity.ViewModel
{
    class NewReader
    {
        public bool isStudent { get; set; }
        public bool isTeacher { get; set; }
        public string NewName { get; set; }
        public string NewSurname { get; set; }
        public string NewPatronymic { get; set; }
        public int NewCourse { get; set; }
        public Faculty NewFaculty { get; set; }
        public Group NewGroup { get; set; }
    }
}
