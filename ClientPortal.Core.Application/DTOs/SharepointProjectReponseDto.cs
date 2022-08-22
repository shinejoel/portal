using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPortal.Core.Application.DTOs
{ 

    public class SharepointProjectReponseDto
    {
        public List<SharepointProjectData> responseData { get; set; }
    }

    public class SharepointProjectData
    {

        public int projectID { get; set; }
        public string projectName { get; set; }
        public string projectUrl { get; set; }
        
        public string projectReferenceID { get; set; }


    }


    public class SharepointTokenDto
    {
        public string access_token { get; set; }
    }

    public class SharepointClientReponseDto
    {
        public List<SharepointClientData> responseData { get; set; }
    }

    public class SharepointClientData
    {
        public int clientId { get; set; }
        public string clientName { get; set; }
        public string clientReferenceId { get; set; }
        public string clientLastVisitedDateTime { get; set; }
        public bool dmsExportEnabled { get; set; }
        public bool exchangeEnabled { get; set; }
        public bool taskDmsExportEnabled { get; set; }
        public string clientCreatedDateTime { get; set; }
        public string clientModifiedDateTime { get; set; }
    }



    public class SharepointUserProjectsData
    {
        public int memberFirmId { get; set; }
        public List<SharepointUserProjectsId> listProjects { get; set; }
        public List<SharepointUsersData> usersData { get; set; }
    }


    public class SharepointUserProjectsId
    {
        public int clientId { get; set; }
        public int projectId { get; set; }
    }

    public class SharepointUserProjectData
    {
        public int memberFirmId { get; set; }
        public int clientId { get; set; }
        public int projectId { get; set; }
        public List<SharepointUsersData> usersData { get; set; }
    }



    public class SharepointUsersData
    {
        public string groupName { get; set; }
        public List<SharepointUserData> userData { get; set; }
    }

    public class SharepointUserData
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string emailAddress { get; set; }
        public string UPN { get; set; }
        public bool isNavision { get; set; }
        public string NavisionUserKey { get; set; }
    }
 

    public class MemberFirmUsersDto
    {
        public string status { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string correlationId { get; set; }
        public List<ResponseDataDTO> responseData { get; set; }

    }

    public class ResponseDataDTO
    {
        public int id { get; set; }
        public string displayName { get; set; }
        public string loginName { get; set; }
        public string emailAddress { get; set; }
        public string userGroupName { get; set; }
    }

}



