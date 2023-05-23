using System.Threading.Tasks;
using AutoPark.Api.Models;

namespace Vehicle.Contract
{
    public interface IAccountService
    {
        public Task<LoginViewModel> CheckLogin(LoginViewModel loginDto);
    }
}