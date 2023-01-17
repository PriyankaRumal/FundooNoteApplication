using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FudooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBL notebl;
        public NoteController(INoteBL notebl)
        {
            this.notebl = notebl;
        }
       [Authorize]
        [HttpPost]
        [Route("NoteModel")]
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
        [HttpGet]
        [Route("GetAll")]
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
        [HttpDelete]
        [Route("DeleteApi")]
        public IActionResult DeleteApi(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = notebl.DeleteNote(userId, noteId);
                if(result==true)
                {
                    return this.Ok(new { succes = true, message = "Deleted successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { succes = false, message = "Not Deleted"});
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
