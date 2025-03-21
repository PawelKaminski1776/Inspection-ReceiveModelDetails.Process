using System;
using InspectionReceiveModelDetails.Messages;
using InspectionReceiveModelDetails.Messages.Dtos;

namespace InspectionReceiveModelDetails.Controllers.DtoFactory;
public class DtoFactory : IDtoFactory
{
    public object CreateDto(string dtoType, params object[] args)
    {
        if (args == null || args.Length == 0)
            throw new ArgumentException("Arguments cannot be null or empty.");

        switch (dtoType.ToLower())
        {
        
            case "receievemodeldetailsdto":
                if (args.Length < 2 || !(args[0] is string))
                    throw new ArgumentException("Invalid arguments for messageRequest.");

                return new ReceiveModelDetailsRequest
                {
                    Username = (string)args[0]
                };

            default:
                throw new ArgumentException($"Invalid DTO type: {dtoType}");
        }
    }

    public object UseDto(string dtoType, object dto)
    {
        if (dto == null)
            throw new ArgumentException("DTO object cannot be null.");

        switch (dtoType.ToLower())
        {
            case "receievemodeldetailsdto":
                return dto;
            default:
                throw new ArgumentException($"Invalid DTO type: {dtoType}");
        }
    }
}
