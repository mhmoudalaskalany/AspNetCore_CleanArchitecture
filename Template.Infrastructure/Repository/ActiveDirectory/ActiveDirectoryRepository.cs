using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Configuration;
using Template.Common.DTO.Identity.Account;
using Template.Common.DTO.Identity.User;
using Template.Common.Infrastructure.Repository.ActiveDirectory;

namespace Template.Infrastructure.Repository.ActiveDirectory
{
    public class ActiveDirectoryRepository : IActiveDirectoryRepository
    {
        private readonly IConfiguration _configuration;
        public ActiveDirectoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActiveDirectoryUserDto LoginAsync(LoginParameters parameters)
        {
            try
            {
                var username = _configuration["ActiveDirectory:ADUserName"];
                var password = _configuration["ActiveDirectory:AdPassword"];
                var container = _configuration["ActiveDirectory:ADContainer"];
                var domain = _configuration["ActiveDirectory:ADDomain"];
                using var context = new PrincipalContext(ContextType.Domain, domain, container, username, password);
                if (context.ValidateCredentials(parameters.Username, parameters.Password))
                {
                    using var userPrincipal = new UserPrincipal(context)
                    {
                        SamAccountName = parameters.Username
                    };
                    using var principalSearcher = new PrincipalSearcher(userPrincipal);
                    var result = principalSearcher.FindOne();
                    if (result != null)
                    {
                        DirectoryEntry de = (DirectoryEntry)result.GetUnderlyingObject();
                        string fName =
                            de.Properties["givenName"]?.Value != null
                                ? de.Properties["givenName"].Value.ToString()
                                : "";
                        string lName = de.Properties["sn"]?.Value != null
                            ? de.Properties["sn"].Value.ToString()
                            : "";

                        string uName =
                            de.Properties["samAccountName"]?.Value != null
                                ? de.Properties["samAccountName"].Value.ToString()
                                : "";

                        string principal =
                            de.Properties["userPrincipalName"]?.Value != null
                                ? de.Properties["userPrincipalName"].Value.ToString()
                                : "";
                        string employeeId =
                           de.Properties["employeeId"]?.Value != null
                               ? de.Properties["employeeId"].Value.ToString()
                               : "";
                        var user = new ActiveDirectoryUserDto
                        {
                            FirstName = fName,
                            LastName = lName,
                            LogonName = uName,
                            EmployeeId = employeeId,
                            Principal = principal
                        };
                        return user;
                    }

                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
