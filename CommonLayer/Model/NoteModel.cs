using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string color { get; set; }
        public string ImagePath { get; set; }
        public bool ArchiveNote { get; set; }
        public bool PinNote { get; set; }
        public bool DeleteNote { get; set; }
       public DateTime CretedTime { get; set; }
       public DateTime EditedTime { get; set; }
    }
}
