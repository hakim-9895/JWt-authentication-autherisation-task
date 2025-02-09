
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Modal;


namespace WebApplication7.Controllers


{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static List<TaskModal> _tasks = new List<TaskModal>();
        private int  Taskid=1;
        [HttpPost("add")]
        [Authorize(Roles="User,Admin")]
        public ActionResult Addtask([FromBody] TaskModal task) {
            task.Id = Taskid++;
            _tasks.Add(task);

            var userName = User.Identity.Name;  // The Name claim from the JWT token
            var userRole = User.FindFirst("role")?.Value;  // Accessing the "role" claim

            Console.WriteLine($"User {userName} with role {userRole} added a task.");
            return Ok(task);


        }
        [HttpDelete("id")]
        [Authorize(Roles="Admin")]
        public ActionResult DeleteTask(int id) {
           var task = _tasks.FirstOrDefault(x => x.Id == id);
             _tasks.Remove(task);
            return Ok(task);
        }
    }
}
