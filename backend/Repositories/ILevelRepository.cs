using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ILevelRepository
    {
        void SaveLevel(Level level);
        Level GetLevelById(int id);
        Level GetLevelByName(string name);
        List<Level> GetLevels();
        void UpdateLevel(Level level);
        void DeleteLevel(Level level);
    }
}
