using Microsoft.AspNetCore.Mvc;
using InspectionReceiveModelDetails.Messages.Dtos;
using InspectionReceiveModelDetails.Controllers.DtoFactory;

namespace InspectionReceiveModelDetails.Controllers
{
    [ApiController]
    [Route("Api/ReceiveModelDetails")]
    public class ReceiveModelDetailsController : BaseController
    {
        public ReceiveModelDetailsController(IMessageSession messageSession, IDtoFactory dtoFactory)
            : base(messageSession, dtoFactory) { }

        [HttpGet("GetModels")]
        public async Task<IActionResult> AddAccount(string username)
        {
            ReceiveModelDetailsRequest request;
            if (username != null)
            {
                request = new ReceiveModelDetailsRequest
                {
                    Username = username
                };
            }
            else
            {
                throw new ArgumentException($"Username is null");
            }

            try
            {
                var response = await _messageSession.Request<ReceiveModelDetailsResponse>(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error while processing the request: {ex.Message}");
            }
        }
    }

}
