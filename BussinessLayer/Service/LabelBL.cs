using BussinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entities;
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

        public IEnumerable<LableEntity> RetriveLabel(long userId, long LabelId)
        {
            try
            {
                return labelrl.RetriveLabel(userId,LabelId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateLabel(long userId, UpdateLabel update)
        {
            try
            {
                return labelrl.UpdateLabel(userId, update);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
