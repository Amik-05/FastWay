using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace FastWay
{
    public class EmailSender
    {
        public void SendEmailWithAttachment(string toAddress, string subject, string body, string attachmentFilePath)
        {
            // Создание объекта MailMessage
            MailMessage mail = new MailMessage();

            // Установка отправителя
            mail.From = new MailAddress("fastway.cargo@outlook.com");

            // Установка получателя
            mail.To.Add(new MailAddress(toAddress));

            // Установка темы письма
            mail.Subject = subject;

            // Установка тела письма
            mail.Body = body;

            // Опционально: установка формата тела письма (HTML)
            mail.IsBodyHtml = true;

            // Создание вложения из текстового файла
            Attachment attachment = new Attachment(attachmentFilePath);

            // Добавление вложения к письму
            mail.Attachments.Add(attachment);

            // Создание объекта SmtpClient для отправки письма
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com"); // Укажите адрес SMTP-сервера
            smtpClient.Port = 587; // Укажите порт SMTP-сервера
            smtpClient.Credentials = new NetworkCredential("fastway.cargo@outlook.com", "OrgBr1337!!"); // Укажите учетные данные
            smtpClient.EnableSsl = true; // Включите SSL, если необходимо

            // Отправка письма
            smtpClient.Send(mail);

            // Освобождение ресурсов
            mail.Dispose();
            attachment.Dispose();
        }

        public void SendEmailWithoutAttachment(string subject, string body,string toAddress)
        {
            // Создание объекта MailMessage
            MailMessage mail = new MailMessage();

            // Установка отправителя
            mail.From = new MailAddress("fastway.cargo@outlook.com");

            // Установка получателя
            mail.To.Add(new MailAddress(toAddress));

            // Установка темы письма
            mail.Subject = subject;

            // Установка тела письма
            mail.Body = body;

            // Опционально: установка формата тела письма (HTML)
            mail.IsBodyHtml = true;

            // Создание объекта SmtpClient для отправки письма
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com"); // Укажите адрес SMTP-сервера
            smtpClient.Port = 587; // Укажите порт SMTP-сервера
            smtpClient.Credentials = new NetworkCredential("fastway.cargo@outlook.com", "OrgBr1337!!"); // Укажите учетные данные
            smtpClient.EnableSsl = true; // Включите SSL, если необходимо

            // Отправка письма
            smtpClient.Send(mail);

            // Освобождение ресурсов
            mail.Dispose();
        }
    }
}