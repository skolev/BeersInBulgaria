using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BeerStatsClient.Models
{
    [DataContract]
    public class BeerModel
    {
        [DataMember(Name = "id")]
        public int BeerId { get; set; }

        [DataMember(Name = "name")]
        public string BeerName { get; set; }

        [DataMember(Name = "brewery")]
        public string BreweryName { get; set; }

        [DataMember(Name = "abv")]
        public float AlcoholByVolume { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "image")]
        public string ImageSource { get; set; }

        [DataMember(Name = "label")]
        public string LabelSource { get; set; }

        [DataMember(Name = "rating")]
        public int? Rating { get; set; }

        [DataMember(Name = "type")]
        public string BeerType { get; set; }
    }
}