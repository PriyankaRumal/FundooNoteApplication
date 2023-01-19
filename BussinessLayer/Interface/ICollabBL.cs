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
    }
}
