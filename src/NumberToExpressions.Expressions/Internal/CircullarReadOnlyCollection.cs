﻿namespace NumberToExpressions.Expressions.Internal {
  using System;

  internal sealed class CircullarReadOnlyCollection<TItem> {

    private readonly TItem[] _items;
    private readonly Random _random = new Random();

    internal CircullarReadOnlyCollection(TItem[] items, Int32? seed) {
      _items = items;
      _random = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    internal TItem Next() {
      return _items[IndexWithRoundRobin];
    }

    private Int32 IndexWithRoundRobin => 
      _random.Next(0, _items.Length) % (_items.Length + 1);
  }
}
