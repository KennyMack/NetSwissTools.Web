using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Web.Mvc;
using NetSwissTools.Web.Mvc.Helpers;
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
        ClientService _ClientService;
        public ClientsController(ILogger<ClientsController> logger,
            ClientService clientService)
        {
            _ClientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client client)
        {
            if (!ModelState.IsValid)
                return base.BadRequest(ModelState);

            await Task.Delay(1);
            var created = _ClientService.Add(client);

            return this.CreatedResultOperation(created.Id.ToString(), created, _ClientService);
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

        [HttpGet("page/{page}/size/{pageSize}")]
        public async Task<IActionResult> GetListPagedAsync([FromRoute] int page, [FromRoute] int pageSize, CancellationToken cancellation) =>
            await this.GetAllPagedAsync(cancellation, _ClientService, page, pageSize);

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