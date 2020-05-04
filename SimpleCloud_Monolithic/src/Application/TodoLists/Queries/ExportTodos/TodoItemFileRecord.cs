using ca_sln.Application.Common.Mappings;
using ca_sln.Domain.Entities;

namespace ca_sln.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
