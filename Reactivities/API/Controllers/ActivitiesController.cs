using System.ComponentModel.DataAnnotations;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseAPIController
    { 


        // Get request that returns list of activities
        [HttpGet] //api/activitites
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            // Send request via mediator 
            return await Mediator.Send(new List.Query());

        }

        [HttpGet("{id}")] //api/activities/someid
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }


        // Creating an Activity returns nothing
        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            await Mediator.Send(new Create.Command{Activity = activity});
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            // add id to activity
            activity.Id = id;
            await Mediator.Send(new Edit.Command{Activity = activity});
            return Ok();


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            await Mediator.Send(new Delete.Command{Id = id});
            return Ok();
        }



    }
}