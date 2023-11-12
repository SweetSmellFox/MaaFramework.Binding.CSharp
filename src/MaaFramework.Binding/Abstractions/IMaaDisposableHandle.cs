﻿namespace MaaFramework.Binding.Abstractions;

/// <summary>
///     An interface defining member about handles from <see cref="MaaFramework"/>.
/// </summary>
public interface IMaaDisposableHandle : IDisposable
{
    /// <summary>
    ///     Gets the handle to be wrapped.
    /// </summary>
    nint Handle { get; }

    /// <summary>
    ///     Marks a handle as no longer used.
    /// </summary>
    void SetHandleAsInvalid();
}
