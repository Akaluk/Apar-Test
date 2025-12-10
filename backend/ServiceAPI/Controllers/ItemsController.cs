using Microsoft.AspNetCore.Mvc;
using ServiceAPI.Models;
using ServiceAPI.Services;

namespace ServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _service;

        public ItemsController(ItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(new { items });
        }

        [HttpPost("approve")]
        public async Task<IActionResult> Approve([FromBody] ApproveRequest request)
        {
            await _service.ApproveAsync(request.Ids, request.Reason);
            return Ok(new { success = true });
        }

        [HttpPost("reject")]
        public async Task<IActionResult> Reject([FromBody] ApproveRequest request)
        {
            await _service.RejectAsync(request.Ids, request.Reason);
            return Ok(new { success = true });
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Item item)
        {
            await _service.AddAsync(item);
            return Ok(item);
        }
    }

    public class ApproveRequest
    {
        public List<int> Ids { get; set; } = new List<int>();
        public string Reason { get; set; } = "";
    }
}