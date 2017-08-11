using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTime
{
    public static class Constants
    {
        public static string StandardRole => "Standard";
        public static string ManagerRole => "Manager";

        public static string EditStatusOperation => "EditStatus";
        public static string EditDescOperation => "EditDescription";

        public static OperationAuthorizationRequirement EditStatus = new OperationAuthorizationRequirement { Name = Constants.EditStatusOperation };
        public static OperationAuthorizationRequirement EditDesc = new OperationAuthorizationRequirement { Name = Constants.EditDescOperation };
    }
}
