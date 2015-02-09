using System;
using LibraryUniversity.Model;

namespace LibraryUniversity.ViewModel
{
    class NewBook
    {
        public string Name { get; set; }
        public Writer Writer { get; set; }
        public string ISBN { get; set; }
        public DateTime Year { get; set; }
        public int Pages { get; set; }
        public int Count { get; set; }
        public Publication Publication { get; set; }
        public Shelving Shelving { get; set; }
    }
}
