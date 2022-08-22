using ClientPortal.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPortal.Core.Application.External.Sharepoint
{
    public interface ISharepointService
    {
        Task<List<SharepointProjectData>> GetProjectsFromSharePoint();
        Task<List<SharepointProjectData>> GetProjectsFromSharePoint(int clientId);
        Task<List<SharepointProjectData>> GetAllProjectsFromSharePoint();
        Task<List<SharepointClientData>> GetClientsFromSharePoint();
        Task<List<HttpResponseMessage>> AddProjectsUserSharePoint(SharepointUserProjectsData data);
        Task<HttpResponseMessage> AddProjectUserSharePoint(SharepointUserProjectData data);
        Task<MemberFirmUsersDto> GetMemberFirmUsersSharePoint();

    }
}
