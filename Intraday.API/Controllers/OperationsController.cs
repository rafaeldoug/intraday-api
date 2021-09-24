using Intraday.Data.Model;
using Intraday.Data.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace Intraday.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {

        // GET api/operations
        /// <summary>
        /// Retorna uma lista de Operações
        /// </summary>
        /// <example>
        /// /api/operations
        /// </example>
        [HttpGet]
        public ActionResult<List<Operation>> Operations()
        {
            return Database.operations;
        }

        // GET api/operations/aaaa-mm-dd
        /// <summary>
        /// Retorna uma lista de operações por Data
        /// </summary>
        [HttpGet("{date}")]
        public ActionResult<List<Operation>> OperationsByDate(DateTime date)
        {
            List<Operation> operationsByDate = Database.operations.FindAll(o => o.Date.ToShortDateString().Equals(date.ToShortDateString()));

            return operationsByDate;

        }

        // GET api/operations/saldo/aaaa-mm-dd
        /// <summary>
        /// Retorna saldo resultante por Data das Operações
        /// </summary>
        /// <example>
        /// R$-525,30
        /// </example>
        [HttpGet("saldo/{date}")]
        public ActionResult<string> OperationsResultByDate(DateTime date)
        {
            List<Operation> operationsByDate = Database.operations.FindAll(o => o.Date.ToShortDateString().Equals(date.ToShortDateString()));
            double total = 0;
            operationsByDate.ForEach(o => total += o.Price);

            return total.ToString("C");

        }

        // POST api/operations
        /// <summary>
        /// Adiciona uma Operação
        /// </summary>
        [HttpPost]
        public ActionResult<Operation> Add([FromBody] Operation operation)
        {
            operation.Id = Guid.NewGuid();
            operation.Date = operation.Date.ToShortDateString().Equals("01/01/0001") ? DateTime.UtcNow : operation.Date;
            Database.operations.Add(operation);

            return operation;
        }

        // PUT api/operations
        /// <summary>
        /// Altera uma Operação
        /// </summary>
        [HttpPut]
        public ActionResult<Operation> Edit([FromBody] Operation operation)
        {
            Operation operationToEdit = Database.operations.Find(o => o.Id.Equals(operation.Id));
            operationToEdit.Price = operation.Price;
            operationToEdit.Date = DateTime.Now;

            return operationToEdit;
        }

        // DELETE api/operations/{id}
        /// <summary>
        /// Remove uma Operação pelo Id
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(Guid id)
        {
            return Database.operations.RemoveAll(o => o.Id.Equals(id));
        }
    }
}
