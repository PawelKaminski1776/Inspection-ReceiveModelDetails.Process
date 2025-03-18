using InspectionReceiveModelDetails.Channel;
using InspectionReceiveModelDetails.Messages.Dtos;

namespace InspectionReceiveModelDetails.Handlers
{
    public class ReceiveModelDetailsHandler : IHandleMessages<ReceiveModelDetailsRequest>
    {
        ReceiveModelDetailsService _receiveModelDetailsService;
        public ReceiveModelDetailsHandler(ReceiveModelDetailsService receiveModelDetailsService)
        {
            this._receiveModelDetailsService = receiveModelDetailsService;
        }

        public async Task Handle(ReceiveModelDetailsRequest message, IMessageHandlerContext context)
        {
            try
            {
                var userexists = await _receiveModelDetailsService.CheckIfUserExists(message.Username);

                ReceiveModelDetailsResponse response = new ReceiveModelDetailsResponse();
                response.Models = new List<ReceiveModelDetailsModels>();

                if (userexists == "User Not Found")
                {
                    await context.Reply(response);
                }

                else
                {
                    var allModels = await _receiveModelDetailsService.GetAllModels(message);
                    if(allModels == null)
                    {
                        await context.Reply(response);
                    }
                    foreach (var model in allModels)
                    {
                        var modelDetails = new ReceiveModelDetailsModels
                        {
                            ModelURL = model.GetValue("Model_URL").AsString, 
                            InspectionName = model.GetValue("InspectionName").AsString,
                            status = model.GetValue("status").AsString,
                        };

                        response.Models.Add(modelDetails);
                    }

                    await context.Reply(response);
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while processing the message: {ex.Message}");

                throw;
            }
        }
    }
}
