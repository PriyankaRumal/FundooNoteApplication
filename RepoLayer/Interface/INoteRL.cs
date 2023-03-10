using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public bool DeleteNote(long userId, long noteId);
        public bool PinNote(long userId, long noteId);
        public bool ArchieveNote(long userId, long noteId);
        public bool Trash(long userId, long noteId);
        public NoteEntity color(ColorModel model, long userId);
        public string UploadImage(IFormFile image, long noteId, long userId);
    }
}
