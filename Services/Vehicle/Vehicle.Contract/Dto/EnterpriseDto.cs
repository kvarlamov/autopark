using System.Collections.Generic;
using BaseTypes;
using Driver.Contract.Dto;
using Vehicle.Contract.Dto;

namespace Enterprise.Contract.Dto
{
    public class EnterpriseDto : BaseDto
    {
        public string Name { get; set; }
        
        public string City { get; set; }

        public int Code { get; set; }

        public int NumberOfStaff { get; set; }

        public List<long> Vehicles { get; set; }

        public List<long> Drivers { get; set; }
    }
}