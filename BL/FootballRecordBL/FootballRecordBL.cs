using KPA_JTA2023_Coding_Assessment;
using KPA_JTA2023_Coding_Assessment.Models;
using KPA_JTA2023_Coding_Assessment.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JTA_KPA_Final.Services.FootballRecordBL
{
    public class FootballRecordBL: IFootballRecordBL
    {
        private readonly APIDbContext _context;
        public FootballRecordBL(APIDbContext context)
        {
            _context = context;
        }

        public async Task<List<FootballRecordVM>> GetAllFootballRecords()
        {
            List<FootballRecordVM> returnList = new List<FootballRecordVM>();
            List<football_record> dbList = await _context.football_record.Include("FootballTeam").ToListAsync();

            if (dbList != null && dbList.Count() > 0)
            {
                returnList = dbList.ConvertAll(x => new FootballRecordVM
                {
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    TeamLosses = x.TeamLosses,
                    TeamWins = x.TeamWins,
                    FootballTeamId = x.FootballTeamId,
                    TeamName = x.FootballTeam?.TeamName
                });
            }

            return returnList;
        }

        public async Task<bool> AddFootballRecord(FootballRecordVM recordToAdd)
        {
            try
            {
                if (recordToAdd != null)
                {
                    football_record newRecord = new football_record();
                    newRecord.ModifiedDate = DateTime.UtcNow;
                    newRecord.CreatedDate = DateTime.UtcNow;
                    newRecord.FootballTeamId = await GetTeamIdByString(recordToAdd.TeamName);
                    newRecord.TeamLosses = recordToAdd.TeamLosses;
                    newRecord.TeamWins = recordToAdd.TeamWins;
                    //if id is 0, then we couldnt find a team, and we couldnt add a team for whatever reason.
                    //skip adding this team, but add the rest of the teams/records from the json
                    if (newRecord.FootballTeamId > 0)
                    {
                        _context.football_record.Add(newRecord);
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                //log a bug
            }
            return false;
        }

        public async Task<bool> DeleteFootballRecord(int recordToDelete)
        {
            try
            {
                football_record dbRecord = await _context.football_record.Where(x => x.Id == recordToDelete).FirstOrDefaultAsync();
                if (dbRecord != null)
                {
                    _context.Remove(dbRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                //log a bug
            }
            return false;
        }

        public async Task<bool> AddBulkFootballRecord(List<FootballRecordVM> recordToAdd)
        {
            try
            {
                if (recordToAdd != null && recordToAdd.Count() > 0)
                {
                    foreach (FootballRecordVM thisRecord in recordToAdd)
                    {
                        football_record newRecord = new football_record();
                        newRecord.ModifiedDate = DateTime.UtcNow;
                        newRecord.CreatedDate = DateTime.UtcNow;
                        newRecord.FootballTeamId = await GetTeamIdByString(thisRecord.TeamName);
                        newRecord.TeamLosses = thisRecord.TeamLosses;
                        newRecord.TeamWins = thisRecord.TeamWins;
                        //if id is 0, then we couldnt find a team, and we couldnt add a team for whatever reason.
                        //skip adding this team, but add the rest of the teams/records from the json
                        if (newRecord.FootballTeamId > 0)
                        {
                            _context.football_record.Add(newRecord);
                        }
                        
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                //log a bug
            }
            return false;
        }

        public async Task<int> GetTeamIdByString(string teamName)
        {
            football_team dbList = await _context.football_team.Where(x => x.TeamName.ToLower().Trim() == teamName.ToLower().Trim()).FirstOrDefaultAsync();

            if (dbList != null)
            {
                return dbList.Id;
            }
            else
            {
                //could not find the team, therefor lets add the team, and then return the id of the newly added team
                return await AddNewTeam(teamName);
            }
        }

        public async Task<int> AddNewTeam(string teamName)
        {
            try
            {

                football_team newTeam = new football_team();
                newTeam.ModifiedDate = DateTime.UtcNow;
                newTeam.CreatedDate = DateTime.UtcNow;
                newTeam.TeamName = teamName;

                _context.football_team.Add(newTeam);
                await _context.SaveChangesAsync();
                return newTeam.Id;
                
            }
            catch (Exception e)
            {
                //log a bug
            }
            return 0;
        }
    }
}
