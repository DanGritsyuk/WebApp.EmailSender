namespace WebAppEmailSender.Models
{
    /// <summary>
    /// Модель представления
    /// </summary>
    public class StatusViewModel
    {
        public StatusViewModel() { }

        public StatusViewModel(string status, string failedMessage)
        {
            this.Status = status;
            this.FailedMessage = failedMessage;
        }

        public string Status { get; set; }
        public string FailedMessage { get; set; }
    }
}