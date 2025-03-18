using NServiceBus;

namespace InspectionReceiveModelDetails.Messages.Dtos
{
    public class ReceiveModelDetailsRequest : IMessage
    {
        public string Username { get; set; }
    }

    public class ReceiveModelDetailsResponse : IMessage
    {
        public List<ReceiveModelDetailsModels> Models { get; set; }
    }

    public class ReceiveModelDetailsModels
    {
        public string ModelURL { get; set; }
        public string InspectionName { get; set; }

        public string status { get; set; }
    }

}
