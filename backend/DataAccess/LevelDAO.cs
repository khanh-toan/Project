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

        public static Level FindLevelById(int id)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    //return context.Levels.Include(s => s.Contracts)
                    //    .SingleOrDefault(c => c.Id == id);
                    return context.Levels.SingleOrDefault(c => c.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SaveLevel(Level level)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Levels.Add(level);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Level FindLevelByName(string name)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    //return context.Levels.Include(s => s.Contracts)
                    //    .SingleOrDefault(c => c.LevelName == name);
                    return context.Levels.SingleOrDefault(c => c.LevelName == name);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateLevel(Level level)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry(level).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
