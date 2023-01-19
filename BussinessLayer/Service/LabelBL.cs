using BussinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class LabelBL:ILabelBL
    {
        ILabelRL labelrl;
        public LabelBL(ILabelRL labelrl)
        {
            this.labelrl = labelrl;
        }

        public bool CreateLable(LabelModel labelModel, long userId)
        {
            return labelrl.CreateLable(labelModel, userId);
        }
    }
}
