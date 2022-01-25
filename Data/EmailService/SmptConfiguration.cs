namespace WebAppEmailSender.EmailService
{
    /// <summary>
    /// Настройки SMPT
    /// </summary>
    public class SmptConfiguration
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Электронный адрес отправителя
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Адрес SMPT-сервера
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Порт
        /// </summary>
        public int Port { get; set; }
    }
}