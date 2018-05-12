using System.Threading.Tasks;
using TodoApi.Models.Entities;
using TodoApi.Data;
using TodoApi.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly TodoContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, IMapper mapper, TodoContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = appDbContext;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _context.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

        [HttpGet("{email}", Name = "GetByEmail")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);//_context.Users.FirstOrDefault(t => t.email == email);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpGet("GetAllUsers")]
        public IEnumerable<AppUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }
    }
}
