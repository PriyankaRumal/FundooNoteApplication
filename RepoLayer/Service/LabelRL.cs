using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class LabelRL:ILabelRL
    {
        FundoContext fundo;
        public LabelRL(FundoContext fundo)
        {
            this.fundo = fundo;
        }
        public bool CreateLable(LabelModel labelModel,long userId)
        {
            try
            {
                var result=fundo.UserTable.Where(e =>e.UserId==userId).FirstOrDefault();
                if(result!=null)
                {
                    LableEntity lableEntity = new LableEntity();
                    lableEntity.UserId = userId;
                    lableEntity.NoteID=labelModel.NoteId;
                    lableEntity.LabelName=labelModel.LabelName;
                    fundo.LableTable.Add(lableEntity);
                    int result1 = fundo.SaveChanges();
                    if(result1>0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
