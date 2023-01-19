using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity CreateCollab(CollabModel collabModel, long noteId);
        public IEnumerable<CollabEntity> RetriveCollab(long noteId);
        public bool RemoveCollab(long collabId);
    }
}
