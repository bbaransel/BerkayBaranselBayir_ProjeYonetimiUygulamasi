using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class PTaskFileManager : IPTaskFileService
    {
        private readonly IPTaskFileRepository _repository;
        private readonly MapperlyConfiguration _mapperly;

        public PTaskFileManager(IPTaskFileRepository repository, MapperlyConfiguration mapperly)
        {
            _repository = repository;
            _mapperly = mapperly;
        }

        public async Task<Response<PTaskFileViewModel>> CreateAsync(PTaskFileViewModel pTaskFileViewModel)
        {
            var file = _mapperly.PTaskFileViewModelToPTaskFile(pTaskFileViewModel);
            var createdFile = await _repository.CreateAsync(file);
            if (createdFile == null) Response<NoContent>.Fail("Dosya yüklenemedi");
            var result = _mapperly.PTaskFileToPTaskFileViewModel(createdFile);
            return Response<PTaskFileViewModel>.Success(result);
        }

        public async Task<Response<List<PTaskFileViewModel>>> GetAllByPTaskIdAsync(int pTaskId)
        {
            var files = await _repository.GetAllAsync(x=>x.PTaskId == pTaskId,
                query=>query.Include(y=>y.PTask));
            if (files == null) Response<NoContent>.Fail("İlgili göreve ait dosya bulunamadı");
            var result = _mapperly.ListPTaskFileToListPTaskFileViewModel(files);
            return Response<List<PTaskFileViewModel>>.Success(result);
        }

        public async Task<Response<PTaskFileViewModel>> GetByIdAsync(int id)
        {
            var file = await _repository.GetAsync(x=>x.Id == id,
                query=>query.Include(y=>y.PTask));
            if (file == null) Response<NoContent>.Fail("İlgili dosya bulunamadı");
            var result = _mapperly.PTaskFileToPTaskFileViewModel(file);
            return Response<PTaskFileViewModel>.Success(result);
        }

        public async Task<Response<NoContent>> HardDeleteAsync(int id)
        {
            var file = await _repository.GetAsync(x => x.Id == id);
            await _repository.HardDeleteAsync(file);
            return Response<NoContent>.Success();
        }
    }
}
