using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.IoT.Models;

namespace GreenField.IoT.Interfaces
{
    public interface IDataProvider
    {
        Task<List<Field>> GetFieldsAsync();
        Task<List<Weed>> GetWeedsAsync();
        Task<List<Pest>> GetPestsAsync();
    }
}