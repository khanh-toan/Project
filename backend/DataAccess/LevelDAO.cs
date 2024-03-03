using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LevelDAO
    {
        public static List<Level> GetLevels()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    return context.Levels.Include(s => s.Contracts).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
