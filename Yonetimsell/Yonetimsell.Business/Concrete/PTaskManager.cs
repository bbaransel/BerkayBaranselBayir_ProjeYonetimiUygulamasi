using Microsoft.EntityFrameworkCore;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class PTaskManager : IPTaskService
    {
        private readonly IPTaskRepository _repository;
        private readonly MapperlyConfiguration _mapperly;

        public PTaskManager(IPTaskRepository repository, MapperlyConfiguration mapperly)
        {
            _repository = repository;
            _mapperly = mapperly;
        }

        public async Task<Response<NoContent>> ChangePTaskPriorityAsync(int pTaskId, Priority priority)
        {
            var pTask = await _repository.GetAsync(x => x.Id == pTaskId);
            if (pTask == null)
            {
                return Response<NoContent>.Fail("İlgili task bulunamadı");
            }
            await _repository.ChangeTaskPriorityAsync(pTask, priority);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ChangePTaskStatusAsync(int pTaskId, Status status)
        {
            var pTask = await _repository.GetAsync(x => x.Id == pTaskId);
            if (pTask == null)
            {
                return Response<NoContent>.Fail("İlgili task bulunamadı");
            }
            await _repository.ChangeTaskStatusAsync(pTask, status);
            return Response<NoContent>.Success();
        }

        public async Task<Response<PTaskViewModel>> CreateAsync(AddPTaskViewModel addPTaskViewModel)
        {
            var pTask = _mapperly.AddPTaskViewModelToPTask(addPTaskViewModel);
            var createdPTask = await _repository.CreateAsync(pTask);
            if (createdPTask == null)
            {
                return Response<PTaskViewModel>.Fail("Task oluşturulamadı! Sorunun devam etmesi durumunda Yönetici ile iletişime geçiniz.");
            }
            var result = _mapperly.PTaskToPTaskViewModel(createdPTask);
            return Response<PTaskViewModel>.Success(result);
        }

        public async Task<Response<int>> GetActiveTaskCountAsync()
        {
            var count = await _repository.GetCountAsync(x => x.Status != Status.Done);
            return Response<int>.Success(count);
        }

        public async Task<Response<int>> GetActiveTaskCountByProjectIdAsync(int projectId)
        {
            var count = await _repository.GetCountAsync(x => x.ProjectId == projectId && x.Status != Status.Done);
            return Response<int>.Success(count);
        }

        public async Task<Response<int>> GetActiveTaskCountByUserIdAsync(string userId)
        {
            var count = await _repository.GetCountAsync(x => x.UserId == userId && x.Status != Status.Done);
            return Response<int>.Success(count);
        }

        public async Task<Response<List<PTaskViewModel>>> GetAllAsync()
        {
            var pTasks = await _repository.GetAllAsync(
                include: query => query.Include(x => x.User));
            if (pTasks == null)
            {
                return Response<List<PTaskViewModel>>.Fail("Hiç task bulunamadı");
            }
            var result = pTasks.Select(x => new PTaskViewModel
            {
                Id = x.Id,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                UserName = x.User.UserName,
                DueDate = x.DueDate,
                Name = x.Name,
                Priority = x.Priority,
                ProjectId = x.ProjectId,
                UserId = x.UserId,
            }).ToList();
            return Response<List<PTaskViewModel>>.Success(result);
        }

        public async Task<Response<int>> GetAllTaskCountAsync()
        {
            var count = await _repository.GetCountAsync();
            return Response<int>.Success(count);
        }

        public async Task<Response<PTaskViewModel>> GetByIdAsync(int pTaskId)
        {
            var pTask = await _repository.GetAsync(x => x.Id == pTaskId,
                q => q.Include(x => x.User));
            if (pTask == null)
            {
                return Response<PTaskViewModel>.Fail("İlgili task bulunamadı.");
            }
            var result = new PTaskViewModel
            {
                Id = pTask.Id,
                Status = pTask.Status,
                CreatedDate = pTask.CreatedDate,
                Description = pTask.Description,
                UserName = pTask.User.UserName,
                DueDate = pTask.DueDate,
                Name = pTask.Name,
                Priority = pTask.Priority,
                ProjectId = pTask.ProjectId,
                UserId = pTask.UserId,
            };
            return Response<PTaskViewModel>.Success(result);
        }

        public async Task<Response<int>> GetCompletedTaskCountAsync()
        {
            var count = await _repository.GetCountAsync(x => x.Status == Status.Done);
            return Response<int>.Success(count);
        }

        public async Task<Response<int>> GetCompletedTaskCountByProjectIdAsync(int projectId)
        {
            var count = await _repository.GetCountAsync(x => x.ProjectId == projectId && x.Status == Status.Done);
            return Response<int>.Success(count);
        }

        public async Task<Response<int>> GetCompletedTaskCountByUserIdAsync(string userId)
        {
            var count = await _repository.GetCountAsync(x => x.UserId == userId && x.Status == Status.Done);
            return Response<int>.Success(count);
        }

        public async Task<int> GetPTaskProgressPercentageByProjectIdAsync(int projectId)
        {
            var allPTasks = await _repository.GetCountAsync(x => x.ProjectId == projectId);
            var completedPTasks = await _repository.GetCountAsync(x => x.ProjectId == projectId && x.Status == Status.Done);
            if (allPTasks == 0)
            {
                return 0;
            }
            double progressPercentage = ((double)completedPTasks / allPTasks) * 100;
            int result = (int)Math.Ceiling(progressPercentage);
            return result;
        }

        public async Task<Response<List<PTaskViewModel>>> GetTasksByPriorityAsync(string userId, Priority priority)
        {
            var pTasks = await _repository.GetTasksByPriorityAsync(userId, priority);
            if (pTasks == null)
            {
                return Response<List<PTaskViewModel>>.Fail($"{priority} öncelikte task bulunamadı");
            }
            var result = _mapperly.ListPTaskToListPTaskViewModel(pTasks);
            return Response<List<PTaskViewModel>>.Success(result);
        }

        public async Task<Response<List<PTaskViewModel>>> GetTasksByProjectIdAsync(int projectId)
        {
            var pTasks = await _repository.GetTasksByProjectIdAsync(projectId);
            if (pTasks == null)
            {
                return Response<List<PTaskViewModel>>.Fail("İlgili projeye ait task bulunamadı");
            }
            var result = pTasks.Select(x => new PTaskViewModel
            {
                Description = x.Description,
                DueDate = x.DueDate,
                Id = x.Id,
                Name = x.Name,
                Priority = x.Priority,
                ProjectId = x.ProjectId,
                Status = x.Status,
                UserId = x.UserId,
                UserName = x.User.UserName,
                CreatedDate = x.CreatedDate,
            }).ToList();
            return Response<List<PTaskViewModel>>.Success(result);
        }

        public async Task<Response<List<PTaskViewModel>>> GetTasksByStatusAsync(string userId, Status status)
        {
            var pTasks = await _repository.GetTasksByStatusAsync(userId, status);
            if (pTasks == null)
            {
                return Response<List<PTaskViewModel>>.Fail($"{status} durumunda task bulunamadı");
            }
            var result = _mapperly.ListPTaskToListPTaskViewModel(pTasks);
            return Response<List<PTaskViewModel>>.Success(result);
        }

        public async Task<Response<List<PTaskViewModel>>> GetTasksByUserIdAsync(string userId)
        {
            var pTasks = await _repository.GetTasksByUserIdAsync(userId);
            if (pTasks == null)
            {
                return Response<List<PTaskViewModel>>.Fail("İlgili kullanıcıya ait task bulunamadı");
            }
            var result = _mapperly.ListPTaskToListPTaskViewModel(pTasks);
            return Response<List<PTaskViewModel>>.Success(result);
        }

        public async Task<Response<NoContent>> HardDeleteAsync(int pTaskId)
        {
            var pTask = await _repository.GetAsync(x => x.Id == pTaskId);
            if (pTask == null)
            {
                return Response<NoContent>.Fail("İlgili task bulunamadı");
            }
            await _repository.HardDeleteAsync(pTask);
            return Response<NoContent>.Success();
        }

        public async Task<Response<PTaskViewModel>> UpdateAsync(EditPTaskViewModel editPTaskViewModel)
        {
            var pTask = _mapperly.EditPTaskViewModelToPTask(editPTaskViewModel);
            if (pTask == null)
            {
                return Response<PTaskViewModel>.Fail("İlgili task bulunamadı");
            }
            pTask.ModifiedDate = DateTime.Now;
            await _repository.UpdateAsync(pTask);
            var result = _mapperly.PTaskToPTaskViewModel(pTask);
            return Response<PTaskViewModel>.Success(result);
        }
    }
}
