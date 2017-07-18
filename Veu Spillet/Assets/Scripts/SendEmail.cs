using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class SendEmail : MonoBehaviour {

	void Awake (){
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("kenneth.berle@gmail.com");
		mail.To.Add("nionfrag@gmail.com");
		mail.Subject = "VEU-Spillet invitation";
		mail.Body = "This is an automated message, you have been invited to join VEU-Spillet";

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("kenneth.berle@gmail.com", "290889kk") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = delegate(
			object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
			return true;
		};
		smtpServer.Send(mail);
		Debug.Log("Success!");
	}
}