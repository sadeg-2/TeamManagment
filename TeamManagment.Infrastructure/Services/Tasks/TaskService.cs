using System.Linq.Dynamic.Core;

namespace TeamManagment.Infrastructure.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public TaskService(ApplicationDbContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        public async Task<MyTask> CreateAsync(CreateTaskDto dto)
        {
            var task = _mapper.Map<MyTask>(dto);
            task.CreatedAt = DateTime.Now;
            task.CreatedBy = "Me";
            await _db.AddAsync(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var task = await _db.Tasks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (task == null)
            {
                throw new Exception();
            }
            task.IsDelete = true;
            _db.Update(task);
            _db.SaveChanges();
            return task.Id;
        }

        public async Task<Response<TaskViewModel>> GetAllForDataTable(Request request)
        {
            var c = request;
            Response<TaskViewModel> response = new Response<TaskViewModel>() { Draw = request.Draw };

            var data = _db.Tasks.AsQueryable();

            response.RecordsTotal = data.Count();

            if (request.Search.Value != null)
            {
                data = data.Where(x =>
                    string.IsNullOrEmpty(request.Search.Value) ||
                    x.Title.ToLower().Contains(request.Search.Value.ToLower()) ||
                    x.Description.ToLower().Contains(request.Search.Value.ToLower()) 
                    //||
                    //x.Assignee.UserName.ToLower().Contains(request.Search.Value.ToLower())
                );
            }
            response.RecordsFiltered = await data.CountAsync();

            if (request.Order != null && request.Order.Count > 0)
            {
                var sortColumn = request.Columns.ElementAt(request.Order.FirstOrDefault().Column).Name;
                var sortDirection = request.Order.FirstOrDefault().Dir;
                data = data.OrderBy(string.Concat(sortColumn, " ", sortDirection));
            }
            var dataList = await data.Skip(c.Start).Take(c.Length).ToListAsync();
            var users = _mapper.Map<List<TaskViewModel>>(dataList);
            response.Data = _mapper.Map<IEnumerable<TaskViewModel>>(users);

            return response;
        }

        public async Task<UpdateTaskDto> GetAsync(int id)
        {
            var task = await _db.Tasks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (task == null)
            {
                throw new Exception();
            }
            return _mapper.Map<UpdateTaskDto>(task);
        }

        public async Task<int> UpdateAsync(UpdateTaskDto dto)
        {
            var task = await _db.Tasks.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (task == null)
            {
                throw new Exception();
            }
            var updatedTask = _mapper.Map(source: dto,destination: task);
            _db.Update(updatedTask);
            await _db.SaveChangesAsync();
            return task.Id;
        }
        public async Task<int> MarkAsync(int id)
        {
            var task = await _db.Tasks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (task == null)
            {
                throw new Exception();
            }
            task.IsCompleted = true;
            _db.Update(task);
            await _db.SaveChangesAsync();
            return id;
        }

    }
}
