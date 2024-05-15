﻿using MaaFramework.Binding.Abstractions;
using MaaFramework.Binding.Custom;

namespace MaaFramework.Binding;

/// <summary>
///     An interface defining wrapped members for MaaInstance with generic handle.
/// </summary>
/// <typeparam name="T">The type of handle.</typeparam>
public interface IMaaInstance<T> : IMaaInstance, IMaaDisposableHandle<T>
{
    /// <summary>
    ///     Gets the resource or inits to bind a <see cref="IMaaResource"/>.
    /// </summary>
    /// <exception cref="MaaBindException"/>
    new IMaaResource<T> Resource { get; init; }

    /// <summary>
    ///     Gets the controller or inits to bind a <see cref="IMaaController"/>.
    /// </summary>
    /// <exception cref="MaaBindException"/>
    new IMaaController<T> Controller { get; init; }
}

/// <summary>
///     An interface defining wrapped members for MaaInstance.
/// </summary>
public interface IMaaInstance : IMaaCommon, IMaaOption<InstanceOption>, IMaaPost, IMaaDisposable
{
    /// <summary>
    ///     Gets or sets whether disposes the <see cref="Resource"/> or the <see cref="Controller"/> or uninits <see cref="Toolkit"/> when <see cref="IDisposable.Dispose"/> was invoked.
    /// </summary>
    DisposeOptions DisposeOptions { get; set; }

    /// <summary>
    ///     Gets the resource.
    /// </summary>
    /// <exception cref="MaaBindException"/>
    IMaaResource Resource { get; }

    /// <summary>
    ///     Gets the controller.
    /// </summary>
    /// <exception cref="MaaBindException"/>
    IMaaController Controller { get; }

    /// <summary>
    ///     Gets or sets the toolkit.
    /// </summary>
    /// <remarks>
    ///     Not automatically calls <see cref="IMaaToolkitConfig.Init"/>.
    /// </remarks>
    IMaaToolkit Toolkit { get; set; }

    /// <summary>
    ///     Gets or sets the utility.
    /// </summary>
    IMaaUtility Utility { get; set; }

    /// <summary>
    ///     Gets whether the <see cref="IMaaInstance"/> is fully initialized.
    /// </summary>
    /// <value>
    ///     true if the <see cref="IMaaInstance"/> was fully initialized; otherwise, false.
    /// </value>
    bool Initialized { get; }

    /// <summary>
    ///     Registers a <see cref="IMaaCustomAction"/> or <see cref="IMaaCustomRecognizer"/> in the <see cref="IMaaInstance"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="IMaaCustomAction"/> or <see cref="IMaaCustomRecognizer"/>.</typeparam>
    /// <param name="name">The new name.</param>
    /// <param name="custom">The <see cref="IMaaCustomAction"/> or <see cref="IMaaCustomRecognizer"/>.</param>
    /// <returns>
    ///     true if the custom action or recognizer was registered successfully; otherwise, false.
    /// </returns>
    /// <exception cref="ArgumentException"/>
    bool Register<T>(string name, T custom) where T : IMaaCustomTask; // TODOa 缺少测试用例

    /// <inheritdoc cref="Register{T}(string, T)"/>
    bool Register<T>(T custom) where T : IMaaCustomTask;

    /// <summary>
    ///     Unregisters a <see cref="IMaaCustomAction"/> or <see cref="IMaaCustomRecognizer"/> in the <see cref="IMaaInstance"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="IMaaCustomAction"/> or <see cref="IMaaCustomRecognizer"/>.</typeparam>
    /// <param name="name">The name of <see cref="IMaaCustomAction"/> or <see cref="IMaaCustomRecognizer"/>.</param>
    /// <returns>
    ///     true if the custom action or recognizer was unregistered successfully; otherwise, false.
    /// </returns>
    /// <exception cref="ArgumentException"/>
    bool Unregister<T>(string name) where T : IMaaCustomTask;

    /// <inheritdoc cref="Unregister{T}(string)"/>
    /// <param name="custom">The <see cref="IMaaCustomAction"/> or <see cref="IMaaCustomRecognizer"/>.</param>
    bool Unregister<T>(T custom) where T : IMaaCustomTask;

    /// <summary>
    ///     Clears all <see cref="IMaaCustomAction"/> or <see cref="IMaaCustomRecognizer"/> in the <see cref="IMaaInstance"/>.
    /// </summary>
    /// <typeparam name="T">THe <see cref="IMaaCustomAction"/> or <see cref="IMaaCustomRecognizer"/>.</typeparam>
    /// <returns>
    ///     true if custom actions or recognizers were cleared successfully; otherwise, false.
    /// </returns>
    bool Clear<T>() where T : IMaaCustomTask;

    /// <summary>
    ///     Appends a async job of executing a maa task, could be called multiple times.
    /// </summary>
    /// <param name="taskEntryName">The name of task entry.</param>
    /// <param name="taskParam">The param of task, which could be parsed to a JSON.</param>
    /// <returns>A task job.</returns>
    IMaaJob AppendTask(string taskEntryName, string taskParam = "{}");

    /// <summary>
    ///     Gets whether the all maa tasks finished.
    /// </summary>
    /// <value>
    ///     true if all tasks finished; otherwise, false.
    /// </value>
    bool AllTasksFinished { get; }

    /// <summary>
    ///     Stops the binded <see cref="IMaaResource"/>, the binded <see cref="IMaaController"/>, all appended tasks. 
    /// </summary>
    bool Abort();
}
