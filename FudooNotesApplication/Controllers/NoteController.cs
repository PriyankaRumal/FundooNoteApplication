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
        INoteBL userbl;
        public NoteController(INoteBL userbl)
        {
            this.userbl = userbl;
        }
       [Authorize]
        [HttpPost]
        [Route("NoteModel")]
        public IActionResult CreateNote(NoteModel noteModel)
        {
            try
            {
               long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = userbl.CreateNote(noteModel, userId);
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
    }
}
