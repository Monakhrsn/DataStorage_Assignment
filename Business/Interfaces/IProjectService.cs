using Business.Dtos;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
    Task<IEnumerable<Project>> GetProjectsAsync();
    Task<Project?> GetProjectByIdAsync(int id);
    Task<Project?> UpdateProjectAsync(ProjectUpdateForm form, int id);
    Task<bool> DeleteProjectByIdAsync(int id);
}