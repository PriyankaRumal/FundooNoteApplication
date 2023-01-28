using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepoLayer.Context;
using RepoLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace FudooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        ICollabBL collabbl;
        private readonly FundoContext fundoContext;
        private readonly IDistributedCache distributedCache;
        public CollabController(ICollabBL collabbl, FundoContext fundoContext, IDistributedCache distributedCache)
        {
            this.collabbl = collabbl;
            this.distributedCache = distributedCache;
            this.fundoContext = fundoContext;
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
        [HttpGet]
        [Route("RetriveCollab")]
        public IActionResult RetriveCollab(long noteId)
        {
            try
            {
                var result = collabbl.RetriveCollab(noteId);
                if (result != null)
                {
                    return this.Ok(new { succes = true, message = "Retrived Successfull!", data = result });
                }
                else
                {
                    return this.BadRequest(new { succes = false, message = " Retrive Failed!" });
                }


            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("RemoveCollab")]
        public IActionResult RemoveCollab(long collabId)
        {
            try
            {
                var result = collabbl.RemoveCollab(collabId);
                if(result==true)
                {
                    return this.Ok(new { succes = true, message = "Removed Successfull!", data = result });
                }
                else
                {
                    return this.BadRequest(new { succes = false, message = " Removed Failed!" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
            var cacheKey = "CollabList";
            string serializedCollabList;
            var CollabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                CollabList = await fundoContext.CollabTable.ToListAsync();
                serializedCollabList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollabList);
        }
    }
}
