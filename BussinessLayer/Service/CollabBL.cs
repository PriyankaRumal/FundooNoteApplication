using BussinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
		ICollabRL collabrl;
		public CollabBL(ICollabRL collabrl)
		{
			this.collabrl = collabrl;
		}
        public CollabEntity CreateCollab(CollabModel collabModel, long noteId)
        {
			try
			{
				return collabrl.CreateCollab(collabModel, noteId);
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
				return collabrl.RetriveCollab(noteId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public bool RemoveCollab(long collabId)
        {
			try
			{
				return collabrl.RemoveCollab(collabId);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
