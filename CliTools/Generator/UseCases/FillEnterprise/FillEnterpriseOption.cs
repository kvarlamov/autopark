using System.Collections.Generic;
using CommandLine;
using Generator.UseCases.Abstract;

namespace Generator.UseCases.FillEnterprise
{
    [Verb("fill", HelpText = "Fill enterprises with vehicles and drivers")]
    internal class FillEnterpriseOption : BaseOption
    {
        [Option(longName:"enterprise", shortName:'e', HelpText = "Enterprises ids separated by SPACE")]
        public List<long> EnterpriseList { get; set; }
        
        [Option(longName:"vehicle", shortName:'v', HelpText = "Number of vehicles to fill")]
        public int NumberOfVehicles { get; set; }
    }
}