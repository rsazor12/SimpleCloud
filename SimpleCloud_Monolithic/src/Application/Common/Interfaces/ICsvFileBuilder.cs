using ca_sln.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace ca_sln.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
