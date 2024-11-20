using AutoMapper;
using EmberAPI.Dtos;
using EmberAPI.Models;

namespace EmberAPI.APIMapper;

public class ApiMapper : Profile
{
    public ApiMapper()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<Client, ClientPOSTDto>().ReverseMap();
        CreateMap<Instance, InstanceDto>().ReverseMap();
        CreateMap<CreatedUser, CreatedUserDto>().ReverseMap();
        CreateMap<CreatedUser, CreatedUserPOSTDto>().ReverseMap();
        CreateMap<DataCenter, DataCenterDto>().ReverseMap();
        CreateMap<DBRole, DBRoleDto>().ReverseMap();
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<FacturaDetalle, FacturaDetalleDto>().ReverseMap();
        CreateMap<Factura, FacturaDto>().ReverseMap();
        CreateMap<PaymentInfo, PaymentInfoDto>().ReverseMap();
        CreateMap<TicketDetail, TicketDetailDto>().ReverseMap();
        CreateMap<Ticket, TicketDto>().ReverseMap();
    }
}