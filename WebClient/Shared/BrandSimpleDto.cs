using System.Collections.Generic;

namespace WebClient.Shared
{
    public class BrandSimpleDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class DriverSimpleDto
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
    }
    
    public class EnterpriseDto
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string City { get; set; }

        public int Code { get; set; }

        public int NumberOfStaff { get; set; }

        public List<long> Vehicles { get; set; }

        public List<long> Drivers { get; set; }

        public List<long> Managers { get; set; }
    }
}