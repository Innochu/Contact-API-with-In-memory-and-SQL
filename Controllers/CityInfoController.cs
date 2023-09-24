using City.Models;
using City_Info.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace City_Info.Controllers
{
	[ApiController]
	[Route ("api/[controller]")]
	public class CityInfoController : Controller
	{
		private readonly CityDBContext _cityDBContext;

		public CityInfoController(CityDBContext cityDBContext)
        {
			_cityDBContext = cityDBContext;
		}


		[HttpGet]
       public async Task<IActionResult> GetContacts()
		{
			return Ok(await _cityDBContext.Contacts.ToListAsync());
			//return View();
		}


		[HttpPost]
		public async Task<IActionResult> AddRequest(AddContactRequest addContactRequest) //pass the new model you created as a parameter in this post method
		{
			//create an instance of the original model  and envelop with thier properties
			var contact = new Contact()
			{
				Id = Guid.NewGuid(),
				FullName = addContactRequest.FullName,
				Email = addContactRequest.Email,
				PhoneNumber = addContactRequest.PhoneNumber,
				Address = addContactRequest.Address,
			};

			//inject the DBContext, go to the table from DBset i.e Contacts and add all parameters asynchronously
			await _cityDBContext.Contacts.AddAsync(contact);
			await _cityDBContext.SaveChangesAsync();                 //save all changes async
			
			return Ok(contact);                                        // return contact with an Ok response
		}

		[HttpPut]
		[Route ("{id:guid}")]
		public async Task<IActionResult> UpdateRequest([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
		{
			var contact = await _cityDBContext.Contacts.FindAsync(id);
			

			if (contact !=null)
			{
				contact.FullName = updateContactRequest.FullName;
				contact.Email = updateContactRequest.Email;
				contact.PhoneNumber = updateContactRequest.PhoneNumber;
				contact.Address = updateContactRequest.Address;


				await _cityDBContext.SaveChangesAsync();
				return Ok(contact);
			
			}
			return NotFound();
			


		}
	}
}
