using CloudinaryDotNet;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
    public class NoteRL : INoteRL
    {
        FundoContext fundo;
        IConfiguration configuration;
        public NoteRL(FundoContext fundo, IConfiguration configuration)
        {
            this.fundo = fundo;
            this.configuration = configuration;
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
                int result = fundo.SaveChanges();


                if (result > 0)
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
        public IEnumerable<NoteEntity> Retrive(long userId, long noteId)
        {
            try
            {
                var result = fundo.NoteTable.Where(e => e.NoteId == noteId && e.UserId == userId);
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

        public bool UpdateNote(NoteModel noteModel, long userId, long noteId)
        {
            try
            {
                var result = fundo.NoteTable.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                if (result != null)
                {
                    if (noteModel.Title != null)
                    {
                        result.Title = noteModel.Title;
                    }
                    if (noteModel.Description != null)
                    {
                        result.Description = noteModel.Description;
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
        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                var result = fundo.NoteTable.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                //fundo.NoteTable.Remove(result);
                if (result != null)
                {
                    fundo.NoteTable.Remove(result);
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
        public bool PinNote(long userId, long noteId)
        {
            try
            {
                var result = fundo.NoteTable.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                if (result.PinNote == true)
                {
                    result.PinNote = false;
                    fundo.SaveChanges();
                    return false;
                }
                else
                {
                    result.PinNote = true;
                    fundo.SaveChanges();
                    return true;
                }

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
                var result = fundo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (result.ArchiveNote == true)
                {
                    result.ArchiveNote = false;
                    fundo.SaveChanges();
                    return false;
                }
                else
                {
                    result.ArchiveNote = true;
                    fundo.SaveChanges();
                    return true;
                }
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
                var result = fundo.NoteTable.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);

                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundo.SaveChanges();

                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundo.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity color(ColorModel model ,long userId)
        {
            try
            {
                var result = fundo.NoteTable.Where(x => x.UserId == userId && x.NoteId == model.NoteId).FirstOrDefault();
                if(result!=null)
                {
                    result.color =model.color;
                    fundo.SaveChanges();
                    return result;

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
        public string UploadImage(IFormFile image, long noteId, long userId)
        {
            try
            {
                var result = fundo.NoteTable.FirstOrDefault(e => e.NoteId == noteId && e.UserId == userId);
                if (result != null)
                {
                    Account accounnt = new Account(
                        this.configuration["CloudinarySettings:CloudName"],
                       this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]
                        );
                    Cloudinary cloudinary = new Cloudinary(accounnt);
                    var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.ImagePath = imagePath;
                    fundo.SaveChanges();

                    return "Image uploaded successfully";
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


    }
}
