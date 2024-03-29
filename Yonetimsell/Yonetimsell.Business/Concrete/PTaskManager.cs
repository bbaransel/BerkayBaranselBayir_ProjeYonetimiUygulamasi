﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;
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

        public async Task<Response<NoContent>> ChangeIsCompletedAsync(int pTaskId)
        {
            var pTask = await _repository.GetAsync(x=>x.Id== pTaskId);
            if (pTask == null) Response<NoContent>.Fail("İlgili task bulunamadı");
            await _repository.ChangeTaskIsCompletedAsync(pTask);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ChangePTaskPriorityAsync(int pTaskId, Priority priority)
        {
            var pTask = await _repository.GetAsync(x => x.Id == pTaskId);
            if (pTask == null) Response<NoContent>.Fail("İlgili task bulunamadı");
            await _repository.ChangeTaskPriorityAsync(pTask, priority);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ChangePTaskStatusAsync(int pTaskId, Status status)
        {
            var pTask = await _repository.GetAsync(x => x.Id == pTaskId);
            if (pTask == null) Response<NoContent>.Fail("İlgili task bulunamadı");
            await _repository.ChangeTaskStatusAsync(pTask, status);
            return Response<NoContent>.Success();
        }

        public async Task<Response<PTaskViewModel>> CreateAsync(AddPTaskViewModel addPTaskViewModel)
        {
            var pTask = _mapperly.AddPTaskViewModelToPTask(addPTaskViewModel);
            var createdPTask = await _repository.CreateAsync(pTask);
            if (createdPTask == null) Response<AddPTaskViewModel>.Fail("Task oluşturulamadı! Sorunun devam etmesi durumunda Yönetici ile iletişime geçiniz.");
            var result = _mapperly.PTaskToPTaskViewModel(createdPTask);
            return Response<PTaskViewModel>.Success(result);
        }

        public async Task<Response<List<PTaskViewModel>>> GetAllAsync()
        {
            var pTasks = await _repository.GetAllAsync();
            if (pTasks == null) Response<NoContent>.Fail("Hiç task bulunamadı");
            var result = _mapperly.ListPTaskToListPTaskViewModel(pTasks);
            return Response<List<PTaskViewModel>>.Success(result);
        }

        public async Task<Response<PTaskViewModel>> GetByIdAsync(int pTaskId)
        {
            var pTask = await _repository.GetAsync(x=>x.Id == pTaskId);
            if (pTask == null) Response<NoContent>.Fail("İlgili task bulunamadı.");
            var result = _mapperly.PTaskToPTaskViewModel(pTask);
            return Response<PTaskViewModel>.Success(result);
        }

        public async Task<Response<List<PTaskViewModel>>> GetTasksByPriorityAsync(string userId, Priority priority)
        {
            var pTasks = await _repository.GetTasksByPriorityAsync(userId, priority);
            if (pTasks == null) Response<NoContent>.Fail($"{priority} öncelikte task bulunamadı");
            var result = _mapperly.ListPTaskToListPTaskViewModel(pTasks);
            return Response<List<PTaskViewModel>>.Success(result);
        }

        public async Task<Response<List<PTaskViewModel>>> GetTasksByStatusAsync(string userId, Status status)
        {
            var pTasks = await _repository.GetTasksByStatusAsync(userId, status);
            if (pTasks == null) Response<NoContent>.Fail($"{status} durumunda task bulunamadı");
            var result = _mapperly.ListPTaskToListPTaskViewModel(pTasks);
            return Response<List<PTaskViewModel>>.Success(result);
        }

        public async Task<Response<NoContent>> HardDelete(int pTaskId)
        {
            var pTask = await _repository.GetAsync(x=>x.Id== pTaskId);
            if (pTask == null) Response<NoContent>.Fail("İlgili task bulunamadı");
            await _repository.HardDeleteAsync(pTask);
            return Response<NoContent>.Success();
        }

        public Task<Response<NoContent>> SoftDelete(int pTaskId)
        {
            // Will add later on.
            throw new NotImplementedException();
        }

        public async Task<Response<PTaskViewModel>> UpdateAsync(PTaskViewModel pTaskViewModel)
        {
            var pTask = _mapperly.PTaskViewModelToPTask(pTaskViewModel);
            if (pTask == null) Response<ProjectViewModel>.Fail("İlgili task bulunamadı");
            pTask.ModifiedDate = DateTime.Now;
            await _repository.UpdateAsync(pTask);
            var result = _mapperly.PTaskToPTaskViewModel(pTask);
            return Response<PTaskViewModel>.Success(result);
        }
    }
}
