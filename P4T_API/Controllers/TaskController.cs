using Business.Models.DTOs;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace P4P_API.Controllers;

[Route("/")]
public class TaskController : Controller
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        
        return View(this._taskService.Get());
    }

    [HttpGet]
    [Route("Form")]
    public IActionResult FormCreate()
    {
        return View(new TaskRequestDTO()
        {
            DueDate = DateTime.Today
        });
        
    }
    [HttpPost]
    [Route("Create")]
    public IActionResult CreateTaskPost(TaskRequestDTO taskRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return View("FormCreate", taskRequestDto);
        }

        try
        {
            _taskService.Save(taskRequestDto);
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return RedirectToAction("Index");

        }

    }
    [HttpGet]
    [Route("Edit/{id}")]
    public IActionResult FormEdit(Guid id)
    {
        try
        {

            var task = _taskService.GetById(id);
            return View(new TaskUpdateRequestDTO()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority
            });
        }
        catch (Exception)
        {
            return RedirectToAction("Index");
        }
    }

    
    [HttpPost]
    [Route("EditPost")]
    public IActionResult EditTaskPost(TaskUpdateRequestDTO taskRequestDto)
    {
        try
        {

            if (!ModelState.IsValid)
            {
                return View("FormEdit", taskRequestDto);
            }

            _taskService.UpdateById(taskRequestDto.Id, taskRequestDto);
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return RedirectToAction("Index");

        }
    }
    
    [HttpPost]
    [Route("Remove/{id}")]
    public IActionResult RemoveTask(Guid id)
    {
        try
        {
            _taskService.DeleteTaskById(id);
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return RedirectToAction("Index");
        }
        
    }

}