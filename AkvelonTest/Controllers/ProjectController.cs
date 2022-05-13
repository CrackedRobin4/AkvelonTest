using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AkvelonTest.DTO;
using AkvelonTest.Models;
using AkvelonTest.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AkvelonTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private AkvelonTestContext _akvelonTestContext;

        public ProjectController(AkvelonTestContext akvelonTestContext)
        {
            _akvelonTestContext = akvelonTestContext;
        }

        [HttpGet]
        [Route("get-projects")]
        public ActionResult<List<ProjectResponse>> GetProjects()
        {
            return Ok(_akvelonTestContext.Projects
                .Select(project => new ProjectResponse()
                {
                    Name = project.Name,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Priority = project.Priority,
                    Status = project.Status.Name
                })
                .ToList());
        }
        
        [HttpPost]
        [Route("create-project")]
        public ActionResult CreateProject(ProjectDTO projectDto)
        {
            var project = new Project();
            project.Name = projectDto.Name;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;
            project.Priority = projectDto.Priority;
            project.StatusId = projectDto.StatusId;
            _akvelonTestContext.Projects.Add(project);
            _akvelonTestContext.SaveChanges();
            return Ok("Project created");
        }

        [HttpPost]
        [Route("update-project")]
        public ActionResult UpdateProject(int id, ProjectDTO projectDto)
        {
            var project = _akvelonTestContext.Projects.Find(id);
            if (project == null)
                return BadRequest("Project not found");
            project.Name = projectDto.Name;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;
            project.Priority = projectDto.Priority;
            project.StatusId = projectDto.StatusId;
            _akvelonTestContext.Update(project);
            _akvelonTestContext.SaveChanges();
            return Ok("Project updated");
        }
        
        [HttpPost]
        [Route("delete-project")]
        public ActionResult DeleteProject(int id)
        {
            var project = _akvelonTestContext.Projects.Find(id);
            if (project == null)
                return BadRequest("Project not found");
            _akvelonTestContext.Projects.Remove(project);
            _akvelonTestContext.SaveChanges();
            return Ok("Project deleted");
        }

        [HttpGet]
        [Route("get-tasks")]
        public ActionResult GetTasks(int id)
        {
            var project = _akvelonTestContext.Projects.Find(id);
            if (project == null)
                return BadRequest("Project not found");
            return Ok(_akvelonTestContext.Tasks
                .Where(task => task.ProjectId == id)
                .Include(project => project.Project)
                .Select(tasks => new TaskResponse()
                {
                    Name = tasks.Name,
                    Project = tasks.Project.Name,
                    Priority = tasks.Priority,
                    Description = tasks.Description,
                    Status = tasks.Status.Name
                })
                .ToList());
        }
        
        [HttpPost]
        [Route("add-task")]
        public ActionResult AddTask(int taskId, int projectId)
        {
            var task = _akvelonTestContext.Tasks.Find(taskId);
            if (task == null)
                return BadRequest("Task not found");
            var project = _akvelonTestContext.Projects.Find(projectId);
            if (project == null)
                return BadRequest("Project not found");
            task.ProjectId = projectId;
            _akvelonTestContext.Tasks.Update(task);
            _akvelonTestContext.SaveChanges();
            return Ok("Task added to the project");
        }
        
        [HttpPost]
        [Route("remove-task")]
        public ActionResult RemoveTask(int taskId)
        {
            var task = _akvelonTestContext.Tasks.Find(taskId);
            if (task == null)
                return BadRequest("Task not found");
            task.ProjectId = null;
            _akvelonTestContext.SaveChanges();
            return Ok("Task removed from project");
        }
    }
}
