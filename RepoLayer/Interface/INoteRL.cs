using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity CreateNote(NoteModel noteModel, long userId);
        public IEnumerable<NoteEntity> Retrive(long userId, long noteId);
        public IEnumerable<NoteEntity> RetriveAll(long userId);
        public bool UpdateNote(NoteModel noteModel, long userId, long noteId);
       // public bool DeleteNotes(long noteId, long userId);
    }
}
