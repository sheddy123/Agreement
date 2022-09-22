using Microsoft.AspNetCore.Identity;

namespace AgreementManagement.Data
{
    public class RegistrationUser : IdentityUser
    {
        [PersonalData]
        public string? Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}


