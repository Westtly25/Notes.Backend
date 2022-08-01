using System;
using MediatR;

namespace Notes.Application.Notes.Queries
{
    public class GetNoteDetailsQuery : IRequest<>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
