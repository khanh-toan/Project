using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl
{
    public class LevelRepository : ILevelRepository
    {
        public void DeleteLevel(Level level) => LevelDAO.DeleteLevel(level);

        public Level GetLevelById(int id) => LevelDAO.FindLevelById(id);

        public Level GetLevelByName(string name) => LevelDAO.FindLevelByName(name);

        public List<Level> GetLevels() => LevelDAO.GetLevels();

        public void SaveLevel(Level level) => LevelDAO.SaveLevel(level);

        public void UpdateLevel(Level level) => LevelDAO.UpdateLevel(level);
    }
}
