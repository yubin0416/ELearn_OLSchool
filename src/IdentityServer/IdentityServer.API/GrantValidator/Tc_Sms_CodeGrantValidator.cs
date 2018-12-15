using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.API.Services;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace IdentityServer.API.GrantValidator
{
    public class Tc_Sms_CodeGrantValidator : IExtensionGrantValidator
    {
        private readonly IUserService _UserService;

        public Tc_Sms_CodeGrantValidator(IUserService UserService)
        {
            _UserService = UserService;
        }

        public string GrantType => "tc_sms_code";

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

            var Teacher = await _UserService.Teacher_LoginBySms(phone,code);
            if (Teacher == null)
            {
                context.Result = ErrorGrantValidationResult;
                return;
            }
            var claims = new List<Claim>() { new Claim("role", "teacher") };
            context.Result = new GrantValidationResult(Teacher.ID,GrantType, claims);
        }
    }

}
