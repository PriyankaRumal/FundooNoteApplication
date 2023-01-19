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
        [Authorize]
        [HttpGet]
        [Route("RetrieveLabel")]
        public IActionResult RetrieveLabel(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = labelbl.RetriveLabel(userId, labelId);
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Label Created SuccessFully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note not Created!" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateLabel")]
        public IActionResult UpdateLabel(UpdateLabel update)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = labelbl.UpdateLabel(userId,update);
                if (result ==true)
                {
                    return this.Ok(new { success = true, message = "Label Updated SuccessFully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Label not updated !" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteLabel")]
        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = labelbl.DeleteLabel(userId, labelId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Label deleted SuccessFully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "deletion unsuccesfull !" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
