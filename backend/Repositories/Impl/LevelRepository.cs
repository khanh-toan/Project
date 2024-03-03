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
        public void DeleteLevel(Level level)
        {
            throw new NotImplementedException();
        }

        public Level GetLevelById(int id)
        {
            throw new NotImplementedException();
        }

        public Level GetLevelByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Level> GetLevels() => LevelDAO.GetLevels();

        public void SaveLevel(Level level)
        {
            throw new NotImplementedException();
        }

        public void UpdateLevel(Level level)
        {
            throw new NotImplementedException();
        }
    }
}
