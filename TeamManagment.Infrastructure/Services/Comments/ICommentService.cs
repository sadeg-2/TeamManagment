using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Dtos.Comments;

namespace TeamManagment.Infrastructure.Services.Comments
{
    public interface ICommentService
    {
        CommentViewModel CreateComment(CreateCommentDto dto);
        List<CommentViewModel> GetAllComments(int assignmentId);
        int Delete(int itemId);


    }
}
