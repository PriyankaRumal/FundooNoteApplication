using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ILabelRL
    {
        public bool CreateLable(LabelModel labelModel, long userId);
    }
}
