using Microsoft.AspNetCore.Mvc;
using UmLogic.DAL;
using UmLogic.Models;

namespace UmLogic.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Users_DAL _dal;

        public UsersController(Users_DAL dal)
        {
            _dal = dal;
        }


        [HttpGet("{Id}")]
        public IActionResult GetUser(int Id)
        {
            try
            {
                Users user = _dal.GetById(Id);
                if (user.Id == 0)
                {
                    return NotFound(new { errorMessage = $"User details with ID:{Id} not found" });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _dal.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }


        [HttpGet("StudQuesGet")]
        public IActionResult GetAllStudQues()
        {
            try
            {
                var studques = _dal.GetAllStudQues();
                return Ok(studques);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateUser(Users user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorMessage = "Data is invalid" });
                }

                bool result = _dal.CreateUser(user);
                if (!result)
                {
                    return BadRequest(new { errorMessage = "Data is not saved" });
                }

                return Ok(new { successMessage = "Data saved" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        [HttpPost("StudQues")] // Updated route to match React component
        public IActionResult CreateStudQues(StudQuestion studques)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorMessage = "Data is invalid" });
                }

                bool result = _dal.CreateStudQues(studques);
                if (!result)
                {
                    return BadRequest(new { errorMessage = "Data is not saved" });
                }

                return Ok(new { successMessage = "Data saved" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error message for debugging
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        [HttpPut("{userName}")]
        public IActionResult UpdateUser(int Id, Users user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorMessage = "Data is invalid" });
                }

                bool result = _dal.UpdateUser(user);
                if (!result)
                {
                    return BadRequest(new { errorMessage = "Data is not saved" });
                }

                return Ok(new { successMessage = "Data saved" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            try
            {
                bool result = _dal.DeleteUser(Id);
                if (!result)
                {
                    return BadRequest(new { errorMessage = "Data is not deleted" });
                }

                return Ok(new { successMessage = "Data deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        [HttpDelete("StudQuesGet/{Id}")]
        public IActionResult DeleteStudQues(int Id)
        {
            try
            {
                bool result = _dal.DeleteStudQues(Id);
                if (!result)
                {
                    return BadRequest(new { errorMessage = "Data is not deleted" });
                }

                return Ok(new { successMessage = "Data deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }
    }
}
