using KPA_JTA2023_Coding_Assessment.ViewModels;

namespace JTA_KPA_Final.Services.FootballRecordBL
{
    public interface IFootballRecordBL
    {
        Task<List<FootballRecordVM>> GetAllFootballRecords();

        Task<bool> AddFootballRecord(FootballRecordVM recordToAdd);

        Task<bool> DeleteFootballRecord(int recordToDelete);

        Task<bool> AddBulkFootballRecord(List<FootballRecordVM> recordToAdd);

    }
}
