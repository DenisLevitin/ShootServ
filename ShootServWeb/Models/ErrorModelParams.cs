using System;
using BO;

namespace ShootingCompetitionsRequests.Models
{
    /// <summary>
    /// Модель для сообщения об ошибке
    /// </summary>
    public class ErrorModelParams
    {
        public bool IsOk { get; set; }

        public string ErrorMessage { get; set; }

        public Exception Exc { get; set; }

        public ErrorModelParams()
        {
            IsOk = true;
        }

        public ErrorModelParams(ResultInfo result)
        {
            IsOk = result.IsOk;
            ErrorMessage = result.ErrorMessage;
            Exc = result.Exc;
        }
    }
}