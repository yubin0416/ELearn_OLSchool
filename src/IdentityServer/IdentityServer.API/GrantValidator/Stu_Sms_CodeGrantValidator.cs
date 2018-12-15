using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.API.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace IdentityServer.API.GrantValidator
{
    public class Stu_Sms_CodeGrantValidator : IExtensionGrantValidator
    {
        private readonly IUserService _UserService;

        private readonly PersistedGrantDbContext _persistedGrantDbContext;


        public Stu_Sms_CodeGrantValidator(IUserService UserService, PersistedGrantDbContext persistedGrantDbContext)
        {
            _UserService = UserService;
            _persistedGrantDbContext = persistedGrantDbContext;
        }

        public string GrantType => "stu_sms_code";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            string phone = context.Request.Raw["phone"];
            string code = context.Request.Raw["code"];
            var ErrorGrantValidationResult = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(code))
            {
                context.Result = ErrorGrantValidationResult;
                return;
            }
           
            var student =  await  _UserService.Student_LoginBySms(phone,code);
            if (student == null)
            {
                context.Result = ErrorGrantValidationResult;
                return;
            }
            var claims = new List<Claim>() { new Claim("role","student")};

           var tokenlist =  _persistedGrantDbContext.PersistedGrants.Where(vn => vn.SubjectId == student.ID).ToList();
            _persistedGrantDbContext.PersistedGrants.RemoveRange(tokenlist);
            await _persistedGrantDbContext.SaveChangesAsync();

            context.Result = new GrantValidationResult(student.ID, GrantType, claims);
        }
    }
}
