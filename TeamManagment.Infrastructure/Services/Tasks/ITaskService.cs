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
        Task<string> UpdateAsync(UpdateTaskDto dto);
        Task<string> DeleteAsync(string id);
        Task<UpdateTaskDto> GetAsync(string id);
        Task<Response<TaskViewModel>> GetAllForDataTable(Request request);

    }
}
