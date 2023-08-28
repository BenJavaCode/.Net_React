using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseAPIController
    {
        private readonly DataContext _context;
        public ActivitiesController(DataContext context)
        {
            _context = context;
            
        }

        // Get request that returns list of activities
        [HttpGet] //api/activitites
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {

            return await _context.Activities.ToListAsync();

        }

        [HttpGet("{id}")] //api/activities/someid
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await _context.Activities.FindAsync(id);
        }

    }
}