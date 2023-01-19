using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class CollabRL:ICollabRL
    {
        FundoContext fundo;
        public CollabRL(FundoContext fundo)
        {
            this.fundo = fundo;
        }
        public CollabEntity CreateCollab(CollabModel collabModel,long noteId)
        {
            try
            {
                var noteIdResult=fundo.NoteTable.Where(e => e.NoteId == noteId).FirstOrDefault();
                var emailResult = fundo.UserTable.Where(e => e.Email == collabModel.Email).FirstOrDefault();
                
               
                if(emailResult!=null && noteIdResult!=null)
                {
                    CollabEntity collabEntity = new CollabEntity();
                    collabEntity.UserId = emailResult.UserId;
                    collabEntity.NoteId = noteIdResult.NoteId;
                    collabEntity.Email = collabModel.Email;
                    fundo.CollabTable.Add(collabEntity);
                    int result = fundo.SaveChanges();
                    return collabEntity;
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
        public IEnumerable<CollabEntity> RetriveCollab(long noteId)
        {
            try
            {
                var result = fundo.CollabTable.Where(e => e.NoteId == noteId).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
