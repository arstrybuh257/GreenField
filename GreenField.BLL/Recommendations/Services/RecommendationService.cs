using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GreenField.BLL.Recommendations.Types;
using GreenField.BLL.Types;
using GreenField.Common.Messaging.Messages;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;
using GreenField.DAL.ValueObjects;
using LinqKit;

namespace GreenField.BLL.Recommendations.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly ICultureRepository _cultureRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IPesticideRepository _pesticideRepository;
        private readonly IPestRepository _pestRepository;
        private readonly IWeedRepository _weedRepository;

        public RecommendationService(ICultureRepository cultureRepository, IFieldRepository fieldRepository, IPesticideRepository pesticideRepository, IPestRepository pestRepository, IWeedRepository weedRepository)
        {
            _cultureRepository = cultureRepository;
            _fieldRepository = fieldRepository;
            _pesticideRepository = pesticideRepository;
            _pestRepository = pestRepository;
            _weedRepository = weedRepository;
        }

        public async Task<List<NextCropRecommendation>> GetNextCropRecommendationByFieldId(Guid fieldId, DateTime date)
        {
            var field = await _fieldRepository.GetAsync(fieldId);
            Expression<Func<Culture, bool>> culturesPredicate = c => Math.Abs((int) c.MonthToSeed - date.Month) < 2;
            
            var lastCrop = field.Crops.OrderByDescending(c => c.DateTo).FirstOrDefault();
            
            if (lastCrop != null)
            {
                culturesPredicate = culturesPredicate.And(c => c.EnemyCultures.All(x => x != lastCrop.CultureId));

                List<Guid> lastCropComponents = await GetCropComponentsAsync(lastCrop);

                culturesPredicate = culturesPredicate.And(c => !c.ForbiddenComponents.Intersect(lastCropComponents).Any());
            }
            
            var cultures = await _cultureRepository.BrowseAsync(culturesPredicate);
            
            var results = new List<NextCropRecommendation>();
            
            foreach (var culture in cultures)
            {
                var recommendedPesticides = await GetRecommendedPesticidesAsync(culture, field);
                NextCropRecommendation rec;
                if (lastCrop != null)
                {
                    if (culture.FriendlyCultures.Contains(lastCrop.CultureId))
                    {
                        rec = new NextCropRecommendation()
                        {
                            CultureName = culture.Name,
                            Recommendation = RecommendationLevel.HighlyRecommended,
                            Pesticides = recommendedPesticides
                        };
                        results.Add(rec);
                    }
                }
                else
                {
                    rec = new NextCropRecommendation()
                    {
                        CultureName = culture.Name,
                        Recommendation = RecommendationLevel.Neutral,
                        Pesticides = recommendedPesticides
                    };
                    results.Add(rec);
                }
            }
            
            return results;
        }

        public async Task<PestDetectedRecommendation> GetPestDetectedRecommendation(PestDetectedMessage message)
        {
            var field = await _fieldRepository.GetAsync(message.FieldId);

            if (field == null)
            {
                Console.WriteLine($"Field {message.FieldId} was not found.");
                return null;
            }
            
            var pesticides = (await _pesticideRepository.BrowseAsync(x => true));

            if (pesticides == null)
            {
                Console.WriteLine($"Error while getting pesticides.");
                return null;
            }
            
            var pest = await _pestRepository.GetAsync(message.PestId);
            
            if (pest == null)
            {
                Console.WriteLine($"Pest {message.PestId} was not found.");
                return null;
            }
            
            if (field.CurrentCrop == null)
            {
                Console.WriteLine("Field is empty. No need to recommend.");
                return null;
            }
            
            var currentCulture = await _cultureRepository.GetAsync(field.CurrentCrop.CultureId);
            
            pesticides = pesticides.Where(x => pest.Pesticides.Any(p => p.PesticideId == x.Id) 
                                               && !x.ComponentsIds.Intersect(currentCulture.ForbiddenComponents).Any());

            var pesticidesWithDose = pest.Pesticides.Where(x => pesticides.Any(p => p.Id == x.PesticideId)).ToList();
            
            if (pesticidesWithDose.Count == 0)
            {
                Console.WriteLine("Unable to find recommendation");
                return null;
            }

            return new PestDetectedRecommendation()
            {
                Pesticides = pesticidesWithDose
            };
        }

        public async Task<WeedDetectedRecommendation> GetWeedDetectedRecommendation(WeedDetectedMessage message)
        {
            var field = await _fieldRepository.GetAsync(message.FieldId);

            if (field == null)
            {
                Console.WriteLine($"Field {message.FieldId} was not found.");
                return null;
            }
            
            var pesticides = (await _pesticideRepository.BrowseAsync(x => true));

            if (pesticides == null)
            {
                Console.WriteLine($"Error while getting pesticides.");
                return null;
            }
            
            var weed = await _weedRepository.GetAsync(message.WeedId);
            
            if (weed == null)
            {
                Console.WriteLine($"Weed {message.FieldId} was not found.");
                return null;
            }
            
            if (field.CurrentCrop == null)
            {
                Console.WriteLine("Field is empty. No need to recommend.");
                return null;
            }
            
            var currentCulture = await _cultureRepository.GetAsync(field.CurrentCrop.CultureId);
            
            pesticides = pesticides.Where(x => weed.Pesticides.Any(p => p.PesticideId == x.Id) 
                                               && !x.ComponentsIds.Intersect(currentCulture.ForbiddenComponents).Any());

            var pesticidesWithDose = weed.Pesticides.Where(x => pesticides.Any(p => p.Id == x.PesticideId)).ToList();
            
            if (pesticidesWithDose.Count == 0)
            {
                Console.WriteLine("Unable to find recommendation");
                return null;
            }

            return new WeedDetectedRecommendation()
            {
                Pesticides = pesticidesWithDose
            };
        }

        public async Task<ShouldCultureBeSeededOnThisFieldRecommendation> ShouldCultureBeSeededOnThisField
            (Guid fieldId, Guid cultureId, DateTime date)
        {
            var culture = await _cultureRepository.GetAsync(cultureId);
            var field = await _fieldRepository.GetAsync(fieldId);

            if (Math.Abs((int) culture.MonthToSeed - date.Month) > 2)
            {
                return new ShouldCultureBeSeededOnThisFieldRecommendation()
                {
                    Reason = "Selected date is not fit for this culture.",
                    Recommendation = RecommendationLevel.NotRecommended
                };
            }
            
            var lastCrop = field.Crops.OrderByDescending(c => c.DateTo).FirstOrDefault();
            List<PesticideWithDose> recommendedPesticides;
            
            if (lastCrop != null)
            {
                if (culture.EnemyCultures.Any(x => x == lastCrop.CultureId))
                {
                    return new ShouldCultureBeSeededOnThisFieldRecommendation()
                    {
                        Reason = "You shouldn't seed this culture after previous one.",
                        Recommendation = RecommendationLevel.NotRecommended
                    };
                }

                var lastCropComponents = await GetCropComponentsAsync(lastCrop);

                if (culture.ForbiddenComponents.Intersect(lastCropComponents).Any())
                {
                    return new ShouldCultureBeSeededOnThisFieldRecommendation()
                    {
                        Reason = "ForbiddenComponents used with previous crop.",
                        Recommendation = RecommendationLevel.NotRecommended
                    };
                }
                
                recommendedPesticides = await GetRecommendedPesticidesAsync(culture, field);

                if (culture.FriendlyCultures.Contains(lastCrop.CultureId))
                {
                    return new ShouldCultureBeSeededOnThisFieldRecommendation()
                    {
                        Recommendation = RecommendationLevel.HighlyRecommended,
                        RecommendedPesticides = recommendedPesticides
                    };
                }
            }
            
            recommendedPesticides = await GetRecommendedPesticidesAsync(culture, field);
            
            return new ShouldCultureBeSeededOnThisFieldRecommendation()
            {
                Recommendation = RecommendationLevel.Neutral,
                RecommendedPesticides = recommendedPesticides,
                Reason = "No information provided about last crop on this field."
            };
        }
        
        #region Private Methods

        private async Task<List<PesticideWithDose>> GetRecommendedPesticidesAsync(Culture culture, Field field)
        {
            List<PesticideWithDose> pwd = new List<PesticideWithDose>();
            foreach (var cwd in culture.ConsumeComponents)
            {
                var pesticides = await _pesticideRepository
                    .BrowseAsync(x => x.ComponentsIds.Contains(cwd.ComponentId) 
                                      && !x.ComponentsIds.Intersect(culture.ForbiddenComponents).Any());
                
                var pesticide = GetBestPesticide(pesticides);
                pwd.Add(new PesticideWithDose()
                {
                    PesticideId = pesticide.Id,
                    Dose = cwd.Dose * field.Area
                });
            }

            return pwd;
        }

        private Pesticide GetBestPesticide(IEnumerable<Pesticide> pesticides)
        {
            return pesticides.OrderByDescending(x => x.AveragePrice).First();
        }
        
        private async Task<List<Guid>> GetCropComponentsAsync(Crop crop)
        {
            List<Guid> cropComponents = new List<Guid>();

            Expression<Func<Pesticide, bool>> pesticidePredicate = p => true;

            foreach (var pesticideWithDose in crop.PesticideUsed)
            {
                pesticidePredicate = pesticidePredicate.Or(p => p.Id == pesticideWithDose.PesticideId);
            }

            var pesticides = await _pesticideRepository.BrowseAsync(pesticidePredicate);

            foreach (var pesticide in pesticides)
            {
                cropComponents.AddRange(pesticide.ComponentsIds);
            }

            return cropComponents;
        }

        #endregion
    }
}