using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FudooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        ICollabBL collabbl;
        public CollabController(ICollabBL collabbl)
        {
            this.collabbl = collabbl;
        }
        [HttpPost]
        [Route("CreateCollab")]
        public IActionResult CreateCollab(CollabModel collab,long noteId)
        {
            try
            {
                var result=collabbl.CreateCollab(collab,noteId);
                if(result!=null)
                {
                    return this.Ok(new { succes = true, message = "Collabration Created successfully !", data = result });
                }
                else
                {
                    return this.BadRequest(new { succes = false, message = " Collabration Failed !"});
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
