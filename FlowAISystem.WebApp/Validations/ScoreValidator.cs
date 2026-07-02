

namespace FlowAISystem.Data.DTOs.Score
{
    public class ScoreValidator
    {
        public static string? Validate(ScoreCreateDto dto)
        {
            
           if (dto.Value < 0 || dto.Value > 100)
            {
                return "Score must be between 0 and 100.";
            }
            return null; // No validation errors
        }
    }
    }