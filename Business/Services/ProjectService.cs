using System.Linq.Expressions;
using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Presentation.WebAPI.Exceptions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository): IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    
    // CREATE
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var project = await GetProjectEntityAsync(p => p.Title == form.Title);
        if (project != null)
            throw new ProjectAlreadyExistedException();

        project = new ProjectEntity()
        {
            Title = form.Title,
            Description = form.Description,
            StartDate = DateTime.Parse(form.StartDate),
            EndDate = form.EndDate.IsNullOrEmpty() ? null : DateTime.Parse(form.EndDate!),
            CustomerId = form.CustomerId,
            ProductId = form.ProductId,
            ManagerId = form.ManagerId,
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
            p.Status != null ? p.Status.Id : throw new Exception($"Status is null for project {p.Id}"),
            p.ManagerId,
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
                project.ManagerId,
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
        project.StartDate = DateTime.Parse(form.StartDate);
        project.EndDate = form.EndDate.IsNullOrEmpty() ? null : DateTime.Parse(form.EndDate!);
        project.CustomerId = form.CustomerId;
        project.StatusId = form.StatusId;
        project.ManagerId = form.ManagerId;
        project.ProductId = form.ProductId;

        project = await _projectRepository.UpdateOneAsync(p => p.Id == id, project);
        return new Project(
            project.Id, 
            project.Title, 
            project.Description, 
            project.StartDate,
            project.EndDate,
            project.CustomerId,
            project.StatusId,
            project.ManagerId,
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