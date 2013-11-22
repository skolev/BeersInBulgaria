using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BeerStatsClient.Models
{
    [DataContract]
    public class BeerTypesModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "beers")]
        public IEnumerable<BeerModel> Beers { get; set; }
    }
}
