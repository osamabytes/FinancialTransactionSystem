using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FinancialTrackingSystem.Models;
using System.Text.Json;
using FinancialTrackingSystem.Services;

namespace FinancialTrackingSystem
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            Transaction transaction = await JsonSerializer.DeserializeAsync<Transaction>(req.Body);
            if(transaction == null)
                return new BadRequestResult();
            log.LogInformation("Payload Received: ", transaction);
            var validator = new Validator();
            var isValidTransaction = validator.Validate(transaction);
            var serializedTransaction = Newtonsoft.Json.JsonConvert.SerializeObject(transaction);
            if (isValidTransaction)
                RabbitMQService.PublishToConsumer("queue.api.processing.transaction", "rabbitmq.financial.sender.processing.routingkey", serializedTransaction);
            else
                RabbitMQService.PublishToConsumer("queue.api.holding.transaction", "rabbitmq.financial.sender.holding.routingkey", serializedTransaction);
            return new OkObjectResult(new
            {
                payload = serializedTransaction,
                isValid = isValidTransaction
            });
        }
    }
}
