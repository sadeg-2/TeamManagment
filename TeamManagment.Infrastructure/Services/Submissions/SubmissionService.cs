using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Dtos.Submissions;
using System.Linq.Dynamic.Core;

namespace TeamManagment.Infrastructure.Services.Submissions
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileService _fileService;
        public SubmissionService(ApplicationDbContext db,IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        public async Task<int> CreateSubmission(CreateSubmissionDto dto)
        {
            
            var assignment = _db.Assignments.Include(x => x.Task).SingleOrDefault(x => x.Id == dto.AssignmentId && !x.IsDelete);
            if (assignment == null)
            {
                throw new Exception();
            }
            var submission = new Submission
            {
                AssignmentId = assignment.Id,
                CreatedAt = DateTime.Now,
                TimeLeft = (assignment.Task.DeadLine - DateTime.Now).ToString(),
            };
            _db.Add(submission);
            _db.SaveChanges();
            if (dto.Attatchments != null)
            {
                foreach (var attachmentFile in dto.Attatchments)
                {
                    try
                    {
                        var attachment = new Attatchment();
                        attachment.SubmissionId = submission.Id;
                        attachment.AttachmentUrl = await _fileService.SaveFile(attachmentFile, FolderNames.AttachmentFolder);
                        await _db.Attatchments.AddAsync(attachment);
                        await _db.SaveChangesAsync();
                    }
                    catch (Exception) { 
                    }
                }
            }
            assignment.Task.IsCompleted = Core.Enums.TaskStatee.IsCompleted;
            _db.Update(assignment);
            _db.SaveChanges();
            return submission.Id;
        }

        public async Task<Response<SubmissionViewModel>> GetAllSubmissionDatatable(Request request, int assignmentId)
        {
            Response<SubmissionViewModel> response = new Response<SubmissionViewModel>() { Draw = request.Draw };
            var data = _db.Submissions.Include(x=> x.Attatchments).Where(x=> x.AssignmentId == assignmentId).AsQueryable();
            response.RecordsTotal = data.Count();

            if (request.Search.Value != null)
            {
                data = data.Where(x =>
                    x.CreatedAt.ToString().ToLower().Contains(request.Search.Value.ToLower())
                );
            }
            response.RecordsFiltered = await data.CountAsync();

            //if (request.Order != null && request.Order.Count > 0)
            //{
            //    var sortColumn = request.Columns.ElementAt(request.Order.FirstOrDefault().Column).Name;
            //    var sortDirection = request.Order.FirstOrDefault().Dir;
            //    data = data.OrderBy(string.Concat(sortColumn, " ", sortDirection));
            //}
            response.Data = await data.Skip(request.Start).Take(request.Length).Select(x => new SubmissionViewModel
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt.ToShortDateString(),
                TimeToLeft = x.TimeLeft ,
                attatchmentUrl = x.Attatchments.Select(x=> x.AttachmentUrl).ToList(),
            }).ToListAsync();
            return response;
        }
    }
}
