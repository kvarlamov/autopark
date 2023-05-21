using System.Threading.Tasks;
using AutoPark.Api.Models;

namespace Vehicle.Contract
{
    public interface IAccountService
    {
        public Task<LoginDto> CheckLogin(LoginDto loginDto);
    }
}