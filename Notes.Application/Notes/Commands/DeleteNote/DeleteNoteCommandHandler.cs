using MediatR;
using Notes.Application.Interfaces;
using Notes.Application.Common.Exceptions;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
    {
        private readonly INoteDBContext noteDbContext;

        public DeleteNoteCommandHandler(INoteDBContext noteDBContext)
        {
            this.noteDbContext = noteDBContext;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await noteDbContext.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundExeption(nameof(Note), request.Id);
            }

            noteDbContext.Notes.Remove(entity);
            await noteDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
