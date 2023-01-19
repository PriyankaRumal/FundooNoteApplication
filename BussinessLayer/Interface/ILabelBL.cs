using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ILabelBL
    {
        public bool CreateLable(LabelModel labelModel, long userId);
        public IEnumerable<LableEntity> RetriveLabel(long userId, long LabelId);
        public bool UpdateLabel(long userId, UpdateLabel update);
        public bool DeleteLabel(long userId, long LabelId);
    }
}
