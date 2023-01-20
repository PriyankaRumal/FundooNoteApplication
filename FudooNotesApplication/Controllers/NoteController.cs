using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Channels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepoLayer.Context;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepoLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FudooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBL notebl;
       // private readonly IMemoryCache memoryCache;
        private readonly FundoContext fundoContext;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBL notebl,  FundoContext fundoContext, IDistributedCache distributedCache)
        {
            this.notebl = notebl;
           // this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundoContext = fundoContext;
        }
       [Authorize]
        [HttpPost]
        [Route("CreateNote")]
        public IActionResult CreateNote(NoteModel noteModel)
        {
            try
            {
               long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = notebl.CreateNote(noteModel, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Created SuccessFully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note not Created!" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Retrive")]
        public IActionResult RetriveNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = notebl.Retrive(userId, noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Get Notes Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to get Note." });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("RetriveAll")]
        public IActionResult RetriveAll()
        {
            try
            {
               long userId= Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = notebl.RetriveAll(userId);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Retrived all notes", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Data Not Found" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateApi")]
        public IActionResult UpdateNote(NoteModel notemodel,long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = notebl.UpdateNote(notemodel, userId, noteId);
                if(result == true)
                {
                    return this.Ok(new {succes=true, message="Updated successfully",data=result});
                }
                else
                {
                    return this.BadRequest(new { succes = false, message = "Updation failed" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteApi")]
        public IActionResult DeleteApi(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = notebl.DeleteNote(userId, noteId);
                if (result == true)
                {
                    return this.Ok(new { succes = true, message = "Deleted successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { succes = false, message = "Not Deleted" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public IActionResult Pinned(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result=notebl.PinNote(userId, noteId);
                if(result==true)
                {
                    return this.Ok(new { succes = true, message = "Pinned a Message", data = result });
                }
                else
                {
                    return this.BadRequest(new { succes = false, message = "UnPinned Message!" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ArchieveNote")]
        public IActionResult ArchieveNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result=notebl.ArchieveNote(userId, noteId);
                if(result==true)
                {
                    return this.Ok(new { succes = true, message = "Archived !", data = result });
                }
                else
                {
                    return this.Ok(new { succes = true, message = "UnArchived" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("TrashApi")]
        public IActionResult Trash(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = notebl.Trash(userId, noteId);
                if (result == true)
                {
                    return this.Ok(new { succes = true, message = "Moved To Trash !", data = result });
                }
                else
                {
                    return this.Ok(new { succes = true, message = "Get a data" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ColorNote")]
        public IActionResult Color(ColorModel model)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = notebl.color(model,userId);
                if(result!=null)
                {
                    return this.Ok(new { succes = true, message = "Color change successfully !", data = result });
                }
                else
                {
                    return this.BadRequest(new { succes = true, message = "Color not changes!", data = result });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage(IFormFile image, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = notebl.UploadImage(image,noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { succes = true, message = "Uploaded image successfully !", data = result });
                }
                else
                {
                    return this.BadRequest(new { succes = true, message = "Upload Image Unsuccessfull!", data = result });
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNoteUsingRedisCache()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
            var cacheKey = "NoteList";
            string serializedNoteList;
            var NoteList = new List<NoteEntity>();
            var redisNoteList = await distributedCache.GetAsync(cacheKey);
            if (redisNoteList != null)
            {
                serializedNoteList = Encoding.UTF8.GetString(redisNoteList);
                NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNoteList);
            }
            else
            {
                NoteList = await fundoContext.NoteTable.ToListAsync();
                serializedNoteList = JsonConvert.SerializeObject(NoteList);
                redisNoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNoteList, options);
            }
            return Ok(NoteList);
        }
    } 
}
