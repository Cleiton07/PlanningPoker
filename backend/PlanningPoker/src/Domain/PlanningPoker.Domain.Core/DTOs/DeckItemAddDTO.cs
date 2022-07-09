namespace PlanningPoker.Domain.Core.DTOs
{
    public class DeckItemAddDTO
    {
        public DeckItemAddDTO(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
