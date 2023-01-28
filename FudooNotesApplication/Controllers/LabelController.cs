using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepoLayer.Context;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        ILabelBL labelbl;
        private readonly FundoContext fundoContext;
        private readonly IDistributedCache distributedCache;
        public LabelController(ILabelBL labelbl, FundoContext fundoContext, IDistributedCache distributedCache)
        {
            this.labelbl = labelbl;
            this.distributedCache = distributedCache;
            this.fundoContext = fundoContext;
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
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LableEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LableEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = await fundoContext.LableTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}
