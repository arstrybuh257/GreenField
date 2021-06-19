using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.IoT.Models;

namespace GreenField.IoT.Interfaces
{
    public interface IWeedService
    {
        public Task<List<Weed>> GetWeedsAsync();
    }
}