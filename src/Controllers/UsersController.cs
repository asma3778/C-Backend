using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sda_3_online_Backend_Teamwork.src.Entity;
using sda_3_online_Backend_Teamwork.src.Services.User;
using sda_3_online_Backend_Teamwork.src.Utils;
using static sda_3_online_Backend_Teamwork.src.DTO.UserDTO;

namespace sda_3_online_Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        protected readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateOne(
            [FromBody] UserCreateDto createUserDto
        )
        {
            var userCreated = await _userService.CreateOneAsync(createUserDto);
            return Ok(userCreated);
        }

        [HttpPost("createAdmin")]
        public async Task<ActionResult<UserReadDto>> CreateAdmin(
            [FromBody] UserCreateDto createUserDto
        )
        {
            var userCreated = await _userService.CreateAdminAsync(createUserDto);
            return Ok(userCreated);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserReadDto>>> GetAll(
            int pageNumber = 1,
            int pageSize = 2
        )
        {
            var userList = await _userService.GetAllAsync();

            // Calculate the total number of items
            var totalItems = userList.Count;

            // Get the paginated list
            var paginatedUsers = userList
                .Skip((pageNumber - 1) * pageSize) // Skip items for the previous pages
                .Take(pageSize) // Take only the number of items for the current page
                .ToList();

            // Create the response object
            var response = new
            {
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Users = paginatedUsers,
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UserUpdateDto updateUserDto)
        {
            var result = await _userService.UpdateOneAsync(id, updateUserDto);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteOneAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        //add Token
        [HttpPost("signIn")]
        public async Task<ActionResult<string>> SignInUser([FromBody] SignInDto createDto)
        {
            var token = await _userService.SignInAsync(createDto);
            return Ok(token);
        }
    }
}





/*
       [HttpPost("signup")]
       
        public ActionResult SingUpUser(User newUser)
        {

          PasswordUtils.HashPassword(newUser.Password, out string hashedPassword , out byte[] salt);

          newUser.Password = hashedPassword;

          newUser.Salt = salt;

          users.Add(newUser);

           return Created($"/api/users/{newUser.UserId}" , newUser );
        }*/
