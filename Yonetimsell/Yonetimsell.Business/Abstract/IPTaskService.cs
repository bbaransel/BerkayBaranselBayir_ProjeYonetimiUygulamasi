using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface IPTaskService
    {
        Task<Response<PTaskViewModel>> CreateAsync(AddPTaskViewModel addPTaskViewModel);
        Task<Response<PTaskViewModel>> UpdateAsync(PTaskViewModel pTaskViewModel);
        Task<Response<NoContent>> HardDelete(int pTaskId);
        Task<Response<NoContent>> SoftDelete(int pTaskId);
        Task<Response<List<PTaskViewModel>>> GetAllAsync();
        Task<Response<PTaskViewModel>> GetByIdAsync(int pTaskId);
        Task<Response<List<PTaskViewModel>>> GetTasksByPriorityAsync(string userId, Priority priority);
        Task<Response<List<PTaskViewModel>>> GetTasksByStatusAsync(string userId, Status status);
        Task<Response<NoContent>> ChangeIsCompletedAsync(int pTaskId);
        Task<Response<NoContent>> ChangePTaskStatusAsync(int pTaskId, Status status);
        Task<Response<NoContent>> ChangePTaskPriorityAsync(int pTaskId, Priority priority);
        Task<Response<List<PTaskViewModel>>> GetTasksByProjectIdAsync(int projectId);
        Task<Response<List<PTaskViewModel>>> GetTasksByUserIdAsync(string userId);


    }
}
