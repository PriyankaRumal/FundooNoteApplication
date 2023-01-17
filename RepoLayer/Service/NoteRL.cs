using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class NoteRL :INoteRL
    {
        FundoContext fundo;
        //IConfiguration configuration;
        public NoteRL(FundoContext fundo)
        {
            this.fundo = fundo;
        }
        public NoteEntity CreateNote(NoteModel noteModel, long userId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = noteModel.Title;
                noteEntity.Description = noteModel.Description;
                noteEntity.Reminder = noteModel.Reminder;
                noteEntity.color = noteModel.color;
                noteEntity.ImagePath = noteModel.ImagePath;
                noteEntity.ArchiveNote = noteModel.ArchiveNote;
                noteEntity.PinNote = noteModel.PinNote;
                noteEntity.DeleteNote = noteModel.DeleteNote;
               noteEntity.CretedTime = noteModel.CretedTime;
                noteEntity.EditedTime = noteModel.EditedTime;
                noteEntity.UserId = userId;
                fundo.NoteTable.Add(noteEntity);
                int result= fundo.SaveChanges();
                

                if (result>0)
                {
                    return noteEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<NoteEntity> Retrive(long userId,long noteId)
        {
            try
            {
                var result = fundo.NoteTable.Where(e => e.NoteId==noteId && e.UserId == userId);
                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public IEnumerable<NoteEntity> RetriveAll(long userId)
        {
            try
            {
                var result = fundo.NoteTable.Where(x => x.UserId == userId);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateNote(NoteModel noteModel, long userId,long noteId)
        {
            try
            {
                var result = fundo.NoteTable.FirstOrDefault(x => x.UserId == userId && x.NoteId==noteId);
                if(result!=null)
                {
                    if(noteModel.Title!= null)
                    {
                        result.Title = noteModel.Title;
                    }
                    if(noteModel.Description!=null)
                    {
                        result.Description=noteModel.Description;
                    }
                    result.EditedTime = DateTime.Now;
                    fundo.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
