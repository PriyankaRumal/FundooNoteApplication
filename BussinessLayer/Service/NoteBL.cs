using BussinessLayer.Interface;
using CommonLayer.Model;
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
    }
}
