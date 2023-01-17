using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepoLayer.Entities
{
    public class NoteEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string color { get; set; }
        public string ImagePath { get; set; }
        public bool ArchiveNote { get; set; }
        public bool PinNote { get; set; }
        public bool DeleteNote { get; set; }
        public bool Trash { get; set; }
        public DateTime CretedTime { get; set; }
        public DateTime EditedTime { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
