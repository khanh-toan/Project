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

        public IActionResult Get() => Ok(levelRepository.GetLevels());
    }
}
