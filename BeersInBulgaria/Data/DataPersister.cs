using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerStatsClient.Data;
using BeerStatsClient.Models;
using BeersInBulgaria.ViewModels;

namespace BeersInBulgaria.Data
{
    public class DataPersister
    {
        private const string BaseServicesUrl = "http://beerstats.apphb.com/api/";

        public static async Task<IEnumerable<BeerTypeViewModel>> GetAllBeersTypes()
        {
            var beerTypeModels =
                await HttpRequester.Get<IEnumerable<BeerTypesModel>>(BaseServicesUrl + "beertypes");
            var beerTypes = from beerType in beerTypeModels
                            select new BeerTypeViewModel
                            {
                                Id = beerType.Id,
                                Name = beerType.Name,
                                Description = beerType.Description,
                                BeersEnum = (from beer in beerType.Beers
                                             select new BeerViewModel()
                                             {
                                                 Id = beer.BeerId,
                                                 BeerName = beer.BeerName,
                                                 LabelSource = beer.LabelSource,
                                                 ImageSource = beer.ImageSource,
                                                 AlcoholByVolume = beer.AlcoholByVolume,
                                                 BreweryName = beer.BreweryName,
                                                 Description = beer.Description,
                                             })
                            };
            return beerTypes;
        }

        public static async Task<IEnumerable<BeerViewModel>> GetAllBeers()
        {
            var beerModels =
                await HttpRequester.Get<IEnumerable<BeerModel>>(BaseServicesUrl + "beers");
            var beers = from beer in beerModels
                        select new BeerViewModel()
                        {
                            Id = beer.BeerId,
                            BeerName = beer.BeerName,
                            LabelSource = beer.LabelSource,
                            ImageSource = beer.ImageSource,
                            AlcoholByVolume = beer.AlcoholByVolume,
                            BreweryName = beer.BreweryName,
                            Description = beer.Description,
                        };
            return beers;
        }
    }
}