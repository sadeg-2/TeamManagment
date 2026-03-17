using TeamManagment.Core.Dtos.Submissions;

namespace TeamManagment.Infrastructure.Services.Submissions
{
    public interface ISubmissionService
    {
        Task<int> CreateSubmission(CreateSubmissionDto dto);
        Task<Response<SubmissionViewModel>> GetAllSubmissionDatatable(Request request, int assignmentId);

    }
}
