using InvestorFlow.Services;
using Microsoft.AspNetCore.Mvc;
using InvestorFlow.Interfaces;

namespace InvestorFlow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase, IContactController
    {

        [HttpPost]
        [Route("CreateContact")]
        public bool CreateContact(string Name, string Email = "", string PhoneNumber = "")
        {
            ContactService contactService = new ContactService();
            return contactService.CreateContact(Name, Email, PhoneNumber);
        }

        [HttpGet]
        [Route("ReadContact")]
        public string ReadContact(string Name)
        {
            ContactService contactService = new ContactService();
            return contactService.ReadContact(Name);
        }

        [HttpPut]
        [Route("UpdateContact")]
        public bool UpdateContact(string Name, string NewName = "", string Email = "", string PhoneNumber = "")
        {
            ContactService contactService = new ContactService();
            return contactService.UpdateContact(Name, NewName, Email, PhoneNumber);
        }

        [HttpDelete]
        [Route("DeleteContact")]
        public bool DeleteContact(string Name)
        {
            ContactService contactService = new ContactService();
            return contactService.DeleteContact(Name);
        }

        [HttpPut]
        [Route("AssignContact")]
        public bool AssignContact(string Name, int FundId)
        {
            ContactService contactService = new ContactService();
            return contactService.AssignContact(Name, FundId);
        }

        [HttpPut]
        [Route("UnassignContact")]
        public bool UnassignContact(string Name, int FundId)
        {
            ContactService contactService = new ContactService();
            return contactService.UnassignContact(Name, FundId);
        }

        [HttpGet]
        [Route("ListContacts")]
        public string ListContacts(int FundId)
        {
            ContactService contactService = new ContactService();
            return contactService.ListContactsForFund(FundId);
        }

    }
}
