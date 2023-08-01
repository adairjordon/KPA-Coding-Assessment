using JTA_KPA_Final.Services.FootballRecordBL;
using KPA_JTA2023_Coding_Assessment.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JTA_KPA_Final.Controllers
{
    [Route("api/FootballRecords")]
    [ApiController]
    public class FootballRecordController : ControllerBase
    {
        private readonly IFootballRecordBL _footballRecordBL;
        public FootballRecordController(IFootballRecordBL footballRecordBL)
        {
            _footballRecordBL = footballRecordBL;
        }

        [HttpGet("GetAllFootballRecords")]
        public async Task<ActionResult> GetAllFootballRecords()
        {

            return Ok(await _footballRecordBL.GetAllFootballRecords());
        }

        //included this endpoint, to be used by the unit tests
        [HttpPost("AddFootballRecord")]
        public async Task<ActionResult> AddFootballRecord(FootballRecordVM recordToAdd)
        {
            if (recordToAdd != null)
            {
                //there are only 16 games in a season. Wins + Losses should never be greater than 16.
                if ((recordToAdd.TeamWins + recordToAdd.TeamLosses) > 16)
                {
                    ModelState.AddModelError("Overral Record", "Too many Games total");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _footballRecordBL.AddFootballRecord(recordToAdd));
        }


        [HttpPost("DeleteFootballRecord")]
        public async Task<ActionResult> DeleteFootballRecord([FromBody] int recordToDelete)
        {

            return Ok(await _footballRecordBL.DeleteFootballRecord(recordToDelete));
        }

        [HttpPost("UploadJSONFileForFootballRecords")]
        public async Task<ActionResult> UploadJSONFileForFootballRecords([FromForm] IFormFile File)
        {
            string fileContent = null;
            using (var reader = new StreamReader(File.OpenReadStream()))
            {
                fileContent = reader.ReadToEnd();
            }

            //if its just one json object, then use this path. Would be nice if i could check if its an array with logic instead of just throwing it in a try catch, but that would take more time :)
            try {
                FootballRecordVM thisItemToAdd = JsonConvert.DeserializeObject<FootballRecordVM>(fileContent);
                return Ok(await _footballRecordBL.AddFootballRecord(thisItemToAdd));
            }
            catch (Exception e) {
                //log error 
            }

            //if it fails, maybe it was multiple, thus try this path
            try
            {
                List<FootballRecordVM> theseItemsToAdd = JsonConvert.DeserializeObject<List<FootballRecordVM>>(fileContent);
                return Ok(await _footballRecordBL.AddBulkFootballRecord(theseItemsToAdd));
            }
            catch(Exception e )
            {
               //log error
            }

            return Ok(false);
        }
    }
}
