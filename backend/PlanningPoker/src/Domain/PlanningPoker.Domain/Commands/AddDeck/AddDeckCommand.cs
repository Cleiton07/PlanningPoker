using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Notification;
using PlanningPoker.Domain.Queries.DeckQueries;

namespace PlanningPoker.Domain.Commands.AddDeck
{
    public class AddDeckCommand : Notifiable, IRequest<AddDeckResponseDTO>
    {
        public AddDeckCommand(string name, IList<DeckItemAddDTO> items)
        {
            DeckName = name?.Trim();
            Items = items?.Where(item => item is not null)?.ToList() ?? new List<DeckItemAddDTO>();
        }


        public string DeckName { get; private set; }
        public IList<DeckItemAddDTO> Items { get; private set; }


        public override async Task SubscribeRulesAsync(IMediator mediator, CancellationToken cancellationToken = default)
        {
            var nameValidationResult = await NameIsValidAsync(mediator, cancellationToken);
            var listItemsValidationResult = ListItemsIsValid();

            var contract = new Contract<AddDeckCommand>()
                .IsTrue(nameValidationResult.IsValid, nameof(DeckName), nameValidationResult.Msg)
                .IsTrue(listItemsValidationResult.IsValid, nameof(Items), listItemsValidationResult.Msg);

            var itemsValidationResult = ItemsIsValid();
            foreach (var itemValidationResult in itemsValidationResult)
                contract.IsTrue(itemValidationResult.IsValid, $"{nameof(Items)}[{itemValidationResult.ItemIndex}]", itemValidationResult.Msg);

            AddNotifications(contract);
        }

        private async Task<(bool IsValid, string Msg)> NameIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(DeckName))
                return (false, "Deck name is required");

            if (DeckName.Trim().Length > 20)
                return (false, "A deck name must contain a maximum of 20 characters");

            if (await mediator.Send(new GetExistsDeckByNameQuery(DeckName), cancellationToken))
                return (false, "There is already a deck with this name");

            return (true, "");
        }

        private (bool IsValid, string Msg) ListItemsIsValid()
        {
            if (Items == null || !Items.Any())
                return (false, "Items is required");

            if (Items.Count < 2)
                return (false, "A deck must contain at least 2 items");

            if (Items.Count > 20)
                return (false, "A deck must contain a maximum of 20 items");

            return (true, "");
        }

        private IList<(bool IsValid, int ItemIndex, string Msg)> ItemsIsValid()
        {
            if (Items != null && Items.Any())
                return Items.Select((item, index) =>
                {
                    if (string.IsNullOrWhiteSpace(item.Value))
                        return (false, index, "Item value is required");

                    var itemValue = item.Value.ToUpper().Trim();
                    if (itemValue.Length > 2)
                        return (false, index, "A deck item value must contain a maximum of 2 characters");

                    var itemsValueEquals = Items.Where(x => x.Value.ToUpper().Trim() == itemValue).ToList();
                    if (itemsValueEquals.IndexOf(item) > 0)
                        return (false, index, "Cannot repeat item value in a deck");

                    return (true, index, "");
                }).ToList();

            return new List<(bool, int, string)>();
        }
    }
}
