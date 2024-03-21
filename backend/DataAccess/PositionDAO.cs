using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PositionDAO
    {
        public static Position FindPositionById(int positionId)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    return context.Positions.Include(s => s.Contracts)
                        .SingleOrDefault(c => c.Id == positionId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
