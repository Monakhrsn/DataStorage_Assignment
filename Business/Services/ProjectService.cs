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
            Description = form.Description,
            CustomerId = form.CustomerId,
            ProductId = form.ProductId,
            UserId = form.UserId,
            StatusId = form.StatusId
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
            p.Status.Id,
            p.UserId,
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
                project.Status.Id,
                project.UserId,
                project.Product.Id
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
        project.StartDate = form.StartDate;
        project.EndDate = form.EndDate;
      //  project.Customer.Id = form.CustomerId;
      //  project.Status.Id = form.StatusId;
      //  project.UserId = form.UserId;
      //  project.Product.Id = form.ProductId;

        project = await _projectRepository.UpdateOneAsync(p => p.Id == id, project);
        return new Project(
            project.Id, 
            project.Title, 
            project.Description, 
            project.StartDate,
            project.EndDate,
            project.CustomerId,
            project.StatusId,
            project.UserId,
            project.ProductId
            );
    }
    // DELETE
    public async Task<bool> DeleteProjectByIdAsync(int id)
    {
        var project = await GetProjectEntityAsync(x => x.Id == id);
        if (project == null)
            return false;
        
        var result = await _projectRepository.DeleteOneAsync(p => p.Id == project.Id);
        return result;
    }
    
    private async Task<ProjectEntity?> GetProjectEntityAsync(Expression<Func<ProjectEntity, bool>> predicate)
    {
        var project = await _projectRepository.GetOneAsync(predicate);
        return project;
    }
}