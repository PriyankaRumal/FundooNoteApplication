using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FudooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        ILabelBL labelbl;
        public LabelController(ILabelBL labelbl)
        {
            this.labelbl = labelbl;
        }
        [Authorize]
        [HttpPost]
        [Route("LabelCreate")]
        public IActionResult CreateLabel(LabelModel labelModel)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
            var result=labelbl.CreateLable(labelModel,userId);
            if(result==true)
            {
                return this.Ok(new { success = true, message = "Label Created SuccessFully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note not Created!" });
            }
        }
    }
}
