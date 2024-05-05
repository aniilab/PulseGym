using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Services;

public interface IOpenAIService
{
    Task<string> GenerateSchedule(string scheduleType, ClientViewDTO client);
}
