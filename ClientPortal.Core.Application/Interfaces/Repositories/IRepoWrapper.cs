using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPortal.Core.Application.Interfaces.Repositories
{
    public interface IRepoWrapper
    {
        IGetAllClientProjectsRepo GetAllClientProjectsRepo { get; }
    }
}
