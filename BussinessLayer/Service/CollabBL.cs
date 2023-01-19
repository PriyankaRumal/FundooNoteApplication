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
    }
}
