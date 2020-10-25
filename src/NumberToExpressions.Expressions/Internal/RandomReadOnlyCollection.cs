namespace NumberToExpressions.Expressions.Internal {
  using System;

  internal sealed class RandomReadOnlyCollection<TItem> {

    private readonly TItem[] _items;
    private readonly Random _random = new Random();

    internal RandomReadOnlyCollection(TItem[] items, Int32? seed) {
      _items = items;
      _random = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    internal TItem Next() {
      return _items[RandomIndex];
    }

    private Int32 RandomIndex => 
      _random.Next(0, _items.Length);
  }
}
