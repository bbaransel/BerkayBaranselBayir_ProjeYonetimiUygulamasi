using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface IPTaskFileService
    {
        Task<Response<PTaskFileViewModel>> CreateAsync(PTaskFileViewModel pTaskFileViewModel);
        Task<Response<PTaskFileViewModel>> GetByIdAsync(int id);
        Task<Response<NoContent>> HardDeleteAsync(int id);
        Task<Response<List<PTaskFileViewModel>>> GetAllByPTaskIdAsync(int pTaskId);
    }
}
