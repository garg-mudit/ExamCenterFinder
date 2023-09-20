using ExamCenterFinder.API.Data.Context;
using ExamCenterFinder.API.Data.Entities;
using Newtonsoft.Json;

namespace ExamCenterFinder.API.Data.InitialSeed
{
    public static class DbSeed
    {
        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.ExamCenters.Any())
            {
                var examCentersData = File.ReadAllText("Data/InitialSeed/ExamCentersData.json");
                var examCenters = JsonConvert.DeserializeObject<List<ExamCenter>>(examCentersData);

                if (examCenters != null)
                {
                    context.ExamCenters.AddRange(examCenters);

                    var examCenterSlotsData = File.ReadAllText("Data/InitialSeed/ExamCenterSlotsData.json");
                    var examCenterSlots = JsonConvert.DeserializeObject<List<ExamCenterSlot>>(examCenterSlotsData);

                    if (examCenterSlots != null)
                    {
                        context.ExamCenterSlots.AddRange(examCenterSlots);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
