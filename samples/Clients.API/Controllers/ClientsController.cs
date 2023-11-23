using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Web.Mvc;
using NetSwissTools.Web.Mvc.Results;
using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace Clients.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1")]
    public class ClientsController : SwissControllerApi
    {
        public ClientsController(ILogger<ClientsController> logger)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client client)
        {
            lock (MemoryStore.Clients)
            {
                client.Id = Guid.NewGuid();

                MemoryStore.Clients.Add(client);
            }
            await Task.Delay(1000);
            return base.Created(client.Id.ToString(), client);
        }

        [HttpPost("Pending")]
        public async Task<IActionResult> PostCreate(Client client)
        {
            lock (MemoryStore.ClientsPending)
            {
                client.Id = Guid.NewGuid();

                MemoryStore.ClientsPending.Add(client);
            }
            await Task.Delay(1000);
            return Created(client.Id.ToString(), client);
        }

        [HttpPost("Simulate/{id}")]
        public async Task<IActionResult> PostSimulate(Guid id)
        {
            await Task.Delay(1000);
            lock (MemoryStore.ClientsPending)
            {
                var client = MemoryStore.ClientsPending.FirstOrDefault(x => x.Id == id);

                if (client == null)
                    return MovedPermanently(id.ToString());

                MemoryStore.Clients.Add(client);
                MemoryStore.ClientsPending.Remove(client);

                return Created(client.Id.ToString(), client);
            }
        }

        [HttpGet]
        public ActionResult<Client> Get()
        {
            lock (MemoryStore.Clients)
            {
                return RequestOK(MemoryStore.Clients);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetById(Guid id)
        {
            lock (MemoryStore.Clients)
            {
                return RequestOK(MemoryStore.Clients.Find(x => x.Id == id));
            }
        }
    }
}