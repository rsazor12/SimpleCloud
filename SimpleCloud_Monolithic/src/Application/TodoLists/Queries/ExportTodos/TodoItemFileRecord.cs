using SimpleCloudMonolithic.Application.Common.Mappings;
using SimpleCloudMonolithic.Domain.Entities;

namespace SimpleCloudMonolithic.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
