using Domain.Entities;
using Domain.Models;

namespace Domain.ViewModel
{
    public class UserViewModel : User
    {
        public string Token { get; set; }
    }

}
