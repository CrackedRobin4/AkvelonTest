using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AkvelonTest.DTO;
using AkvelonTest.Models;
using AkvelonTest.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task = AkvelonTest.Models.Task;

namespace AkvelonTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private AkvelonTestContext _akvelonTestContext;

        public TaskController(AkvelonTestContext akvelonTestContext)
        {
            _akvelonTestContext = akvelonTestContext;
        }
        
        //Get all tasks
        [HttpGet]
        [Route("get-tasks")]
        public ActionResult<List<Task>> GetTasks()
        {
            return Ok(_akvelonTestContext.Tasks.Select(task => new TaskResponse()
            {
                Name = task.Name,
                Project = task.Project.Name,
                Priority = task.Priority,
                Description = task.Description,
                Status = task.Status.Name
            }).ToList());
        }
        
        [HttpPost]
        [Route("create-task")]
        public ActionResult CreateTask(TaskDTO taskDto)
        {
            var task = new Task();
            task.Name = taskDto.Name;
            task.StatusId = taskDto.StatusId;
            task.ProjectId = taskDto.ProjectId;
            task.Priority = taskDto.Priority;
            task.Description = taskDto.Description;
            _akvelonTestContext.Add(task);
            _akvelonTestContext.SaveChanges();
            return Ok("Task added");
        }
        
        [HttpPost]
        [Route("update-task")]
        public ActionResult UpdateTask(int id, TaskDTO taskDto)
        {
            var task = _akvelonTestContext.Tasks.Find(id);
            if (task == null)
                return BadRequest("Task not found");
            task.Name = taskDto.Name;
            task.StatusId = taskDto.StatusId;
            task.ProjectId = taskDto.ProjectId;
            task.Priority = taskDto.Priority;
            task.Description = taskDto.Description;
            _akvelonTestContext.Update(task);
            _akvelonTestContext.SaveChanges();
            return Ok("Task updated");
        }
        
        [HttpPost]
        [Route("delete-task")]
        public ActionResult DeleteTask(int id)
        {
            var task = _akvelonTestContext.Tasks.Find(id);
            if (task == null)
                return BadRequest("Task not found");
            _akvelonTestContext.Tasks.Remove(task);
            _akvelonTestContext.SaveChanges();
            return Ok("Task deleted");
        }
    }
}
