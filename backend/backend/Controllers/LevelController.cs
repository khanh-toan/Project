using BusinessObject;
using DataTransfer.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Impl;

namespace backend.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : Controller
    {
        private readonly ILevelRepository levelRepository = new LevelRepository();
 
        
        [HttpGet]
        public IActionResult Get() => Ok(levelRepository.GetLevels());

        [HttpGet("{key}")]
        public ActionResult<Level> GetLevelById(int key)
        {
            var item = levelRepository.GetLevelById(key);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post([FromBody] LevelReq postLevel)
        {
            var tempLevel = levelRepository.GetLevelByName(postLevel.LevelName.Trim());

            if (tempLevel != null)
            {
                return BadRequest("Level already exists.");
            }

            Level level = new Level
            {
                LevelName = postLevel.LevelName.Trim()
            };

            levelRepository.SaveLevel(level);
            return Ok(level);
        }

        [HttpPut("{key}")]
        public IActionResult Put(int key, [FromBody] LevelReq postLevel)
        {
            var level = levelRepository.GetLevelById(key);

            if (level == null)
            {
                return NotFound();
            }

            if (!postLevel.LevelName.Trim().Equals(level.LevelName))
            {
                var tempLevel = levelRepository.GetLevelByName(postLevel.LevelName.Trim());

                if (tempLevel != null)
                {
                    return BadRequest("Level already exists.");
                }

                level.LevelName = postLevel.LevelName.Trim();
            }

            levelRepository.UpdateLevel(level);

            return Ok(level);
        }
    }
}
