using System;
using System.Linq;
using BeersInBulgaria.ViewModels;

namespace BeersInBulgaria.ViewModels
{
    public class BeerViewModel
    {
        public int Id { get; set; }

        public string BeerName { get; set; }

        public string ImageSource { get; set; }

        public string LabelSource { get; set; }

        public string BreweryName { get; set; }

        public float AlcoholByVolume { get; set; }

        public string Description { get; set; }

        public BeerTypeViewModel BeerType { get; set; }
    }
}
