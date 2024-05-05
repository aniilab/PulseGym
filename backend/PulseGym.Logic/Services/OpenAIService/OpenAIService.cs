using Microsoft.Extensions.Configuration;

using OpenAI_API;

using PulseGym.Entities.Exceptions;
using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Services;

public class OpenAIService : IOpenAIService
{
    private readonly IOpenAIAPI api;

    private readonly IConfiguration _configuration;

    public OpenAIService(IConfiguration configuration)
    {
        _configuration = configuration;

        api = new OpenAIAPI(_configuration.GetSection("OpenAI:ApiKey").Value);
    }

    public async Task<string> GenerateSchedule(string scheduleType, ClientViewDTO client)
    {
        var chat = api.Chat.CreateConversation();

        var input = GeneratePrompt(scheduleType, client);
        chat.AppendUserInput(input);

        var response = await chat.GetResponseFromChatbotAsync();

        return response;
    }

    private string GeneratePrompt(string scheduleType, ClientViewDTO client)
    {
        string prompt = $"Hello! I will ask you to generate a {scheduleType} schedule. Reply with JSON object-list which consists of the following objects: \n";

        switch (scheduleType)
        {
            case "workout":
                {
                    prompt += "{ \"weekDay\", \"workoutType\",  \"workoutDuration\",  \"exercises\" : {\"exerciseName\", \"count\", \"setNumber\"} }";
                    break;
                }
            case "meal":
                {
                    prompt += "{ \"weekDay\", \"meals\": {\"mealType\", \"dishName\",  \"weight\",  \"caloriesCount\"}}";
                    break;
                }
            default:
                {
                    throw new BadInputException("Wrong OpenAI prompt");
                }
        }

        prompt += $"\n The schedule should be suitable for a person with following characteristics: weight: {client.InitialWeight}, height: {client.InitialHeight}, goal: {client.Goal}";
        prompt += "\n Return with JSON-object only, without any additional information or messages, no greetings etc.";

        return prompt;
    }
}
