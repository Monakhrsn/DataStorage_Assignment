using System.Linq.Expressions;
using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository): IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    
    // CREATE
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var project = await GetProjectEntityAsync(p => p.Title == form.Title);
        if (project != null)
            return false;

        project = new ProjectEntity()
        {
            Title = form.Title,
            Description = form.Description
        };
        
        var result = await _projectRepository.CreateAsync(project);
        return result != null;
    }
    
    // READ
    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        return projects.Select(p => new Project(
            p.Id,
            p.Title,
            p.Description,
            p.StartDate,
            p.EndDate,
            p.CustomerId,
            p.UserId,
            p.StatusId,
            p.ProductId
            ));
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var project = await GetProjectEntityAsync(p => p.Id == id);
        return project != null
            ? new Project(
                project.Id,
                project.Title,
                project.Description,
                project.StartDate,
                project.EndDate,
                project.CustomerId,
                project.UserId,
                project.StatusId,
                project.ProductId
                )
            : null;
    }
    
    // UPDATE
    public async Task<Project?> UpdateProjectAsync(ProjectUpdateForm form, int id)
    {
        var project = await GetProjectEntityAsync(x => x.Id == id);
        if (project == null)
            return null;       
        
        project.Title = form.Title;
        project.Description = form.Description;

        project = await _projectRepository.UpdateOneAsync(p => p.Id == id, project);
        return new Project(
            project.Id, 
            project.Title, 
            project.Description, 
            project.StartDate,
            project.EndDate,
            project.CustomerId,
            project.UserId,
            project.StatusId,
            project.ProductId
            );
    }
    
    private async Task<ProjectEntity?> GetProjectEntityAsync(Expression<Func<ProjectEntity, bool>> predicate)
    {
        var project = await _projectRepository.GetOneAsync(predicate);
        return project;
    }
}