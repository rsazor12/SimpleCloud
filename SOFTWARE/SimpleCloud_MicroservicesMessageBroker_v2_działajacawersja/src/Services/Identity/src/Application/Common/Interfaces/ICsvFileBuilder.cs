using SimpleCloudMonolithic.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace SimpleCloudMonolithic.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
