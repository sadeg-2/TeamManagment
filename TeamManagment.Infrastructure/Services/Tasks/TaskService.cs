namespace TeamManagment.Infrastructure.Services.Tasks
{
    public class TaskService : ITaskService
    {
        public Task<MyTask> CreateAsync(CreateTaskDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<TaskViewModel>> GetAllForDataTable(Request request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateTaskDto> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(UpdateTaskDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
