using Microsoft.AspNetCore.Mvc;
using MMW.Website.Api.Models;
using MMW.Website.Api.MsGraphApi.Mails;

namespace MMW.Website.Api.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        [Route("api/v1/contact")]
        public IActionResult CreateContactFormular([FromBody] ContactData data)
        {
            var content = "";
            content +=
                "<h3>Vielen Dank für Ihre Anfrage, einer unserer Spezialisten wird sich in kürze bei Ihnen melden.</h3>";
            content += "<br/>";
            content += "<p><strong>Anfragedaten:<strong></p>";
            content += "<table>";
            content += $"<tr><td>Kategorie:</td><td>{data.Categorie}</td></tr>";
            content += $"<tr><td>Name:</td><td>{data.FullName}</td></tr>";
            content += $"<tr><td>Email:</td><td>{data.Email}</td></tr>";
            content += "<tr><td>Frage:</td><td></td></tr>";
            content += "</table>";
            content += $"<p>{data.Content}</p>";
            content += "<br/>";
            content += "<hr/>";
            content +=
                "<p>Anfrage erstellt auf <a style=\"color: blue;\" href=\"https://mmw.industries\">mmw.industries</a><p>";

            var noReplyUser = MsGraphApi.Users.User.ById("00e22e2c-b68e-473c-9a95-58ebe798cfe7");
            var contactMail =
                new MailToSend($"MMW | Ihre Anfrage zu dem Thema {data.Categorie}", content, data.Email, "",
                    "office@mmw.industries");
            var ret = noReplyUser.SendMail(contactMail);
            return Ok(new { status = ret });
        }
    }
}