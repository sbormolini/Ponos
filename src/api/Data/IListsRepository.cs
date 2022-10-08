using Ponos.Shared.Models;

namespace Ponos.Api.Data;

public interface IListsRepository
{
    Task AddListAsync(TodoList list);
    Task AddListItemAsync(TodoItem item);
    Task DeleteListAsync(string listId);
    Task DeleteListItemAsync(string listId, string itemId);
    Task<TodoList?> GetListAsync(string listId);
    Task<TodoItem?> GetListItemAsync(string listId, string itemId);
    Task<IEnumerable<TodoItem>> GetListItemsAsync(string listId, int? skip, int? batchSize);
    Task<IEnumerable<TodoItem>> GetListItemsByStateAsync(string listId, string state, int? skip, int? batchSize);
    Task<IEnumerable<TodoList>> GetListsAsync(int? skip, int? batchSize);
    Task UpdateList(TodoList existingList);
    Task UpdateListItem(TodoItem existingItem);
}