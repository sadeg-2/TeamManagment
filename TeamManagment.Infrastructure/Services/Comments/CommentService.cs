using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Dtos.Comments;

namespace TeamManagment.Infrastructure.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _db;
        public CommentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public CommentViewModel CreateComment(CreateCommentDto dto)
        {
            var assignment = _db.Assignments.SingleOrDefault(x=> x.Id == dto.AssignmentID);
            if (assignment == null)
            {
                throw new Exception();
            }
            var comment = new Comment
            {
                Message = dto.Message,
                CreatedAt = DateTime.Now,
                CreatedBy = dto.WriterId,
                WriterId = dto.WriterId,
                TaskId = assignment.TaskId,
                WriterUserName = dto.WriterUserName ,
            };
            _db.Add(comment);
            _db.SaveChanges();
            var vm = new CommentViewModel
            {
                CreatedAt = comment.CreatedAt.ToShortTimeString() +"  "+ comment.CreatedAt.ToShortDateString(),
                Id = comment.Id,
                ImageUrl = _db.Users.Single(x => x.Id == comment.WriterId).ImageUrl,
                Message = dto.Message,
                UserName = comment.WriterUserName ,
            };
            return vm;

        }

        public int Delete(int itemId)
        {
            var comment = _db.Comments.SingleOrDefault(x => !x.IsDelete && x.Id == itemId);
            if (comment == null) {
                throw new Exception();
            }
            comment.IsDelete = true;
            _db.Update(comment);
            _db.SaveChanges();
            return comment.Id;
        }

        public List<CommentViewModel> GetAllComments(int assignmentId)
        {
            var assignment = _db.Assignments.SingleOrDefault(x => x.Id == assignmentId);
            if (assignment == null)
            {
                throw new Exception();
            }
            var comments = _db.Comments.Where(x => x.TaskId == assignment.TaskId && !x.IsDelete).Select(
                    comment => new CommentViewModel
                    {
                        CreatedAt = comment.CreatedAt.ToShortTimeString() +"  "+ comment.CreatedAt.ToShortDateString(),
                        Id = comment.Id,
                        Message = comment.Message,
                        UserName = comment.WriterUserName,
                        ImageUrl = _db.Users.Single(x=> x.Id == comment.WriterId).ImageUrl ,
                    }
                ).ToList();
            return comments;
        }
    
    }
}
