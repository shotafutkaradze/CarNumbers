using MediatR;

namespace Application.Commands.CarNumberCommands
{
    public class DeleteCarNumberCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
