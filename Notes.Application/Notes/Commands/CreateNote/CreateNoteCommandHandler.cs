using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Domain;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INoteDBContext dbContext;

        public CreateNoteCommandHandler(INoteDBContext noteDBContext)
        {
            dbContext = noteDBContext;
        }

        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await dbContext.Notes.AddAsync(note, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}
