using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity CreateCollab(CollabModel collabModel, long noteId);
        public IEnumerable<CollabEntity> RetriveCollab(long noteId);
    }
}
