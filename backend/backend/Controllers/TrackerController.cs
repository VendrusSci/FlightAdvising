using Microsoft.AspNetCore.Mvc;
using Contexts;
using Contexts.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using backend.DTOs;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerController : Controller
    {
        private readonly IConfiguration _config;
        private readonly TrackerContext _context;

        public TrackerController(IConfiguration config, TrackerContext context)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult GetTrackedItems()
        {
            var items = _context.TrackedItems.ToList();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrackedItem(string name)
        {
            var newItem = await _context.TrackedItems.AddAsync(new TrackedItem { Description = name, DateCreated = DateTime.Now });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Could not add an item with this name. Check for pre-existing tracker");
            }

            return Ok(newItem.Entity);
        }

        [HttpGet("{id}")]
        public IActionResult GetTrackedItemData(int id, DateTime? startDate, DateTime? endDate)
        {
            var itemDataQuery = _context.TrackedItemValues.Where(x => x.TrackedItemId == id);
            if (itemDataQuery.Count() == 0)
            {
                if (!_context.TrackedItems.Any(x => x.TrackedItemId == id))
                    return BadRequest("No matching trackers");

                return Ok("No data present for this tracker");
            }

            if (startDate is not null)
                itemDataQuery = itemDataQuery.Where(x => x.Date >= startDate);
            if (endDate is not null)
                itemDataQuery = itemDataQuery.Where(x => x.Date <= endDate);

            var results = new TrackedItemResults
            {
                Entries = itemDataQuery.ToList(),
                MaxGemValue = itemDataQuery.OrderByDescending(x => x.GemValue).FirstOrDefault(),
                MinGemValue = itemDataQuery.OrderBy(x => x.GemValue).FirstOrDefault(),
                MaxTreasureValue = itemDataQuery.OrderByDescending(x => x.TreasureValue).FirstOrDefault(),
                MinTreasureValue = itemDataQuery.OrderBy(x => x.TreasureValue).FirstOrDefault(),
            };

            return Ok(results);
        }

        [HttpPost("{id}")]
        public IActionResult AddTrackedItemData(int id, [FromBody]List<TrackedItemValue> values)
        {
            //Validate tracker target
            if (!_context.TrackedItems.Any(x => x.TrackedItemId == id))
                return BadRequest("No matching trackers");

            foreach(var item in values)
            {
                item.TrackedItemId = id;
                if ((item.TreasureValue is null || item.TreasureValue == 0) && (item.GemValue is null || item.GemValue == 0))
                    return BadRequest($"Entry for {item.Date.ToShortDateString()} has no price associated, please fix data and try again");

                if (item.GemValue == 0) item.GemValue = null;
                if (item.TreasureValue == 0) item.TreasureValue = null;
            }
            
            try
            {
                _context.TrackedItemValues.AddRange(values);
                _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Unable to add data to the tracker; check format and try again");
            }

            return Ok();
        }
    }
}
