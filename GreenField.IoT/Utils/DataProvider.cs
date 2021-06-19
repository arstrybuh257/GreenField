using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.IoT.Interfaces;
using GreenField.IoT.Models;

namespace GreenField.IoT.Utils
{
    public class DataProvider : IDataProvider
    {
        private readonly IFieldService _fieldService;
        private readonly IPestService _pestService;
        private readonly IWeedService _weedService;

        public DataProvider(IFieldService fieldService, IPestService pestService, IWeedService weedService)
        {
            _fieldService = fieldService;
            _pestService = pestService;
            _weedService = weedService;
        }

        private List<Field> _fields;
        private List<Pest> _pests;
        private List<Weed> _weeds;

        public async Task<List<Field>> GetFieldsAsync()
        {
            if (_fields == null)
            {
                _fields = await _fieldService.GetFieldsAsync();
            }
            
            return _fields ?? new List<Field>();
        }
        
        public async Task<List<Weed>> GetWeedsAsync()
        {
            if (_weeds == null)
            {
                _weeds = await _weedService.GetWeedsAsync();
            }
            return _weeds ?? new List<Weed>();
        }
        
        public async Task<List<Pest>> GetPestsAsync()
        {
            if (_pests == null)
            {
                _pests = await _pestService.GetPestsAsync();
            }

            return _pests ?? new List<Pest>();
        }
    }
}