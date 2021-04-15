using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.PesticideAdvisor
{
    public interface IPesticideAdvisor
    {
        Task<List<PesticideRecommendation>> GetRecommendation();
    }
}