using System.Collections.Generic;
using BaseTypes;
using Enterprise.Contract.Dto;

namespace Vehicle.Contract.Dto
{
    public class ManagerDto : BaseDto
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public List<long> Enterprises { get; set; }
    }
}