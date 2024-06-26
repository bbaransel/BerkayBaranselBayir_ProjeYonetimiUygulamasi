﻿using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface IPTaskService
    {
        Task<Response<PTaskViewModel>> CreateAsync(AddPTaskViewModel addPTaskViewModel);
        Task<Response<PTaskViewModel>> UpdateAsync(EditPTaskViewModel editPTaskViewModel);
        Task<Response<NoContent>> HardDeleteAsync(int pTaskId);
        Task<Response<List<PTaskViewModel>>> GetAllAsync();
        Task<Response<PTaskViewModel>> GetByIdAsync(int pTaskId);
        Task<Response<List<PTaskViewModel>>> GetTasksByPriorityAsync(string userId, Priority priority);
        Task<Response<List<PTaskViewModel>>> GetTasksByStatusAsync(string userId, Status status);
        Task<Response<NoContent>> ChangePTaskStatusAsync(int pTaskId, Status status);
        Task<Response<NoContent>> ChangePTaskPriorityAsync(int pTaskId, Priority priority);
        Task<Response<List<PTaskViewModel>>> GetTasksByProjectIdAsync(int projectId);
        Task<Response<List<PTaskViewModel>>> GetTasksByUserIdAsync(string userId);
        Task<Response<int>> GetAllTaskCountAsync();
        Task<Response<int>> GetActiveTaskCountAsync();
        Task<Response<int>> GetCompletedTaskCountAsync();
        Task<Response<int>> GetActiveTaskCountByUserIdAsync(string userId);
        Task<Response<int>> GetCompletedTaskCountByUserIdAsync(string userId);
        Task<Response<int>> GetActiveTaskCountByProjectIdAsync(int projectId);
        Task<Response<int>> GetCompletedTaskCountByProjectIdAsync(int projectId);
        Task<int> GetPTaskProgressPercentageByProjectIdAsync(int projectId);
    }
}
