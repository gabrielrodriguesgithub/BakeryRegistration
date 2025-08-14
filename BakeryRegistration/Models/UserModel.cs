using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BakeryRegistration.Models;

public class UserModel : IdentityUser
{
    
    public string CNPJ { get; set; }
    public UserModel()
    {

    }
}