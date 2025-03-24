using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Thunders.TechTest.ApiService.App.Contracts.Requests;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Shared.Extensions;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [SwaggerTag("Criação de transação de pagamento")]
    [Route("v{version:apiVersion}/toll-transactions")]
    public class TollTransactionsController : ControllerBase
    {
        /// <summary>
        /// Criação de transação de pagamento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="service"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCreditLine(
            [FromRoute, Required] Guid id,
            [FromBody, Required] CreateTollTransactionRequest request,
            [FromServices] ICreateTollTransactionService service,
            CancellationToken cancellationToken = default
        ) {
            var result = await service.Execute(id, request, cancellationToken);

            return result.Match<IActionResult>(
              onSuccess: response => Ok(response.Value),
              onFailure: errors => errors.ToHttpErrors());
        }
    }
}
