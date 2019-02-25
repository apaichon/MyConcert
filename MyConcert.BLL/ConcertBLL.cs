using System;
using Puppy.BLL;
using Puppy.Model;

namespace MyConcert.BLL
{
    public class ConcertBLL:MyConcertBase
    {
        public ConcertBLL()
        {
            this.DbConfig.TableName ="concert";
        }
        
    }
}
