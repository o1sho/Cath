using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public enum ThrownModifier {
    None,
    Fire,
    Wet
}

[RequireComponent(typeof(ThrownItem))]
public class ThrownItemRuntime : MonoBehaviour
{
    private readonly List<ThrownModifier> _mods = new();
    public IReadOnlyList<ThrownModifier> Mods => _mods;

    public IThrowableItem SourceItem { get; private set; }
    public ItemMaterial Material { get; private set; }


    public void Init(IThrowableItem source) {
        SourceItem = source;
        if (source is ThrowableItemData data) Material = data.Material;
    }

    public bool Has(ThrownModifier m) => _mods.Contains(m);

    public void Add(ThrownModifier m) {
        if (m == ThrownModifier.None || _mods.Contains(m)) return;
        _mods.Add(m);
    }
}
