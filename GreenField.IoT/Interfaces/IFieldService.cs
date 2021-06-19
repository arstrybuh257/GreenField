using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.IoT.Models;

namespace GreenField.IoT.Interfaces
{
    public interface IFieldService
    {
        public Task<Field> GetFieldAsync(Guid fieldId);

        public Task<List<Field>> GetFieldsAsync();
    }
}