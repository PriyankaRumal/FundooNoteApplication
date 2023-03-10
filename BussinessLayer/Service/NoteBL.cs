using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32.SafeHandles;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class NoteBL:INoteBL
    {
        INoteRL noterl;
        public NoteBL(INoteRL noterl)
        {
            this.noterl = noterl;
        }

        public NoteEntity CreateNote(NoteModel noteModel, long userId)
        {
            try
            {
                return noterl.CreateNote(noteModel, userId);

            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<NoteEntity> Retrive(long userId, long noteId)
        {
            try
            {
                return noterl.Retrive(userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<NoteEntity> RetriveAll(long userId)
        {
            try
            {
                return noterl.RetriveAll(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateNote(NoteModel noteModel, long userId, long noteId)
        {
            try
            {
                return noterl.UpdateNote(noteModel, userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                return noterl.DeleteNote(userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool PinNote(long userId, long noteId)
        {
            try
            {
                return noterl.PinNote(userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ArchieveNote(long userId, long noteId)
        {
            try
            {
                return noterl.ArchieveNote(userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Trash(long userId, long noteId)
        {
            try
            {
                return noterl.Trash(userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NoteEntity color(ColorModel model, long userId)
        {
            try
            {
                return noterl.color(model,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UploadImage(IFormFile image, long noteId, long userId)
        {
            try
            {
                return noterl.UploadImage(image, noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
