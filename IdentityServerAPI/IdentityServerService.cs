using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcIdentityServer;
namespace UserManagementAPI
{
    public class UserManagementService : IdentityServerProvider.IdentityServerProviderBase
    {
        public override Task<DumpyCountResponse> GetDumyCount(DumpyCountRequest request, ServerCallContext context) {
            return Task.FromResult(new DumpyCountResponse { Temp = "1000" });
        }
            
    }
}
