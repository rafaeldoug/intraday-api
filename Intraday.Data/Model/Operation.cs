using System;

namespace Intraday.Data.Model
{
    public class Operation
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $" Id: {Id,-3} | Resultado: {Price:F} | Data: {Date.Date:d}";
        }

    }
}
