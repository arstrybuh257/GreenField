using AutoMapper;
using GreenField.Api.Models.Component;
using GreenField.Api.Models.Crop;
using GreenField.Api.Models.Culture;
using GreenField.Api.Models.Drone;
using GreenField.Api.Models.Field;
using GreenField.Api.Models.Organisation;
using GreenField.Api.Models.Pest;
using GreenField.Api.Models.Pesticide;
using GreenField.Api.Models.Sensor;
using GreenField.Api.Models.User;
using GreenField.Api.Models.Weed;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.ComponentService.Models;
using GreenField.BLL.Services.CultureService.Models;
using GreenField.BLL.Services.Drone.Models;
using GreenField.BLL.Services.FieldService.Models;
using GreenField.BLL.Services.OrganizationService.Models;
using GreenField.BLL.Services.PesticideService.Models;
using GreenField.BLL.Services.PestService.Models;
using GreenField.BLL.Services.SensorService.Models;
using GreenField.BLL.Services.UserService.Models;
using GreenField.BLL.Services.WeedService.Models;
using GreenField.BLL.Types;
using GreenField.DAL.Entities;
using GreenField.DAL.ValueObjects;

namespace GreenField.Api.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<CreateOrganisationRequest, OrganizationDto>();
            CreateMap<UpdateOrganizationRequest, OrganizationDto>();
            CreateMap<OrganizationDto, Organisation>().ReverseMap();
            
            CreateMap<CreateUserRequest, UserDto>();
            CreateMap<UpdateUserRequest, UserDto>();
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<CreateCultureRequest, CultureDto>();
            CreateMap<UpdateCultureRequest, CultureDto>();
            CreateMap<CultureDto, Culture>().ReverseMap();
            
            CreateMap<CreateDroneRequest, DroneDto>();
            CreateMap<UpdateDroneRequest, DroneDto>();
            CreateMap<DroneDto, Drone>().ReverseMap();
            
            CreateMap<CreateFieldRequest, FieldDto>();
            CreateMap<UpdateFieldRequest, FieldDto>();
            CreateMap<FieldDto, Field>().ReverseMap();
            
            CreateMap<CreatePesticideRequest, PesticideDto>();
            CreateMap<UpdatePesticideRequest, PesticideDto>();
            CreateMap<PesticideDto, Pesticide>().ReverseMap();
            
            CreateMap<CreateSensorRequest, SensorDto>();
            CreateMap<UpdateSensorRequest, SensorDto>();
            CreateMap<SensorDto, Sensor>().ReverseMap();
            
            CreateMap<CreatePestRequest, PestDto>();
            CreateMap<UpdatePestRequest, PestDto>();
            CreateMap<PestDto, Pest>().ReverseMap();
            
            CreateMap<CreateWeedRequest, WeedDto>();
            CreateMap<UpdateWeedRequest, WeedDto>();
            CreateMap<WeedDto, Weed>().ReverseMap();
            
            CreateMap<CreateComponentRequest, ComponentDto>();
            CreateMap<UpdateComponentRequest, ComponentDto>();
            CreateMap<ComponentDto, Component>().ReverseMap();
            
            CreateMap<AddCropRequest, CropDto>();
            CreateMap<CropDto, Crop>().ReverseMap();
        }
    }
}
