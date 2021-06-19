using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.IoT.Models;

namespace GreenField.IoT.Interfaces
{
    public interface IPestService
    {
        public Task<List<Pest>> GetPestsAsync();
    }
}