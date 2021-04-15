using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;

namespace GreenField.BLL.PesticideAdvisor
{
    public class PesticideAdvisor : IPesticideAdvisor
    {
        private IPesticideStrategy _strategy;
        private IFieldService _fieldService;

        public PesticideAdvisor(IPesticideStrategy strategy, IFieldService fieldService)
        {
            _strategy = strategy;
            _fieldService = fieldService;
        }
        
        public void SetStrategy(IPesticideStrategy strategy)
        {
            _strategy = strategy;
        }

        public Task<List<PesticideRecommendation>> GetRecommendation()
        {
            throw new System.NotImplementedException();
        }
    }
}