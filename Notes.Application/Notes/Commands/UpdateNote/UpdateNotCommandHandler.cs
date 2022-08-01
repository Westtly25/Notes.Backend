using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Notes.Application.Interfaces;
using Notes.Application.Common.Exceptions;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNotCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly INoteDBContext noteDBContext;

        public UpdateNotCommandHandler(INoteDBContext dBContext)
        {
            noteDBContext = dBContext;
        }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await noteDBContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundExeption(nameof(Note), request.Id);
            }

            entity.Details = request.Details;
            entity.Title = request.Title;
            entity.EditDate = DateTime.Now;

            await noteDBContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
