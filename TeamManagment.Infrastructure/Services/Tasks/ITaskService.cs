using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Dtos.Tasks;
using TeamManagment.Core.Enums;

namespace TeamManagment.Infrastructure.Services.Tasks
{
    public interface ITaskService
    {
        Task<MyTask> CreateAsync(CreateTaskDto dto);
        Task<int> UpdateAsync(UpdateTaskDto dto);
        Task<int> DeleteAsync(int id);
        Task<UpdateTaskDto> GetAsync(int id);
        Task<Response<TaskViewModel>> GetAllForDataTable(Request request , string AssigneeId, TaskStatee filter);
        Task<Response<TaskViewModel>> GetAllDeletedTask(Request request , string AssigneeId);
        Task<int> MarkAsync(int id, TaskStatee status);

        int RecoverTask(int id);
        int RemoveTask(int id);

    }
}
