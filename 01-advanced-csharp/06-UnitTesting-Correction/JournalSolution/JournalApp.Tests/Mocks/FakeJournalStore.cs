using JournalApp.Models;
using JournalApp.Storage;

namespace JournalApp.Tests.Mocks;

public sealed class FakeJournalStore : IJournalStore
{
  private readonly Dictionary<Guid, JournalEntry> _entries = new();

  public async Task SaveAsync(JournalEntry entry, CancellationToken ct = default)
  {
    _entries[entry.Id] = entry;
  }
  public async Task<IReadOnlyList<JournalEntry>> QueryAsync(Func<JournalEntry, bool> predicate, CancellationToken ct = default)
  {
    var results = _entries.Values.Where(predicate).ToList();
    return results;
  }
  public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
  {
    if (!_entries.ContainsKey(id)) return false;
    _entries.Remove(id);
    return true;
  }

  private async Task<IReadOnlyList<JournalEntry>> ReadAllAsync(CancellationToken ct)
  {
    return [.. _entries.Values];
  }

  private async Task WriteAllAsync(List<JournalEntry> list, CancellationToken ct)
  {
    foreach (var entry in list)
    {
      _entries[entry.Id] = entry;
    }
  }
}