using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Dtos.Tasks;

namespace TeamManagment.Infrastructure.Services.Tasks
{
    public interface ITaskService
    {
        Task<MyTask> CreateAsync(CreateTaskDto dto);
        Task<int> UpdateAsync(UpdateTaskDto dto);
        Task<int> DeleteAsync(int id);
        Task<UpdateTaskDto> GetAsync(int id);
        Task<Response<TaskViewModel>> GetAllForDataTable(Request request);
        Task<int> MarkAsync(int id);

    }
}
