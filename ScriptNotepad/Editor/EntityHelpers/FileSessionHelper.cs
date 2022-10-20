#nullable enable
using ScriptNotepad.Database.Entity.Entities;

namespace ScriptNotepad.Editor.EntityHelpers;

/// <summary>
/// Helper methods for the <see cref="FileSession"/> entity.
/// </summary>
public static class FileSessionHelper
{
    /// <summary>
    /// Generates, creates and sets a random path for the <see cref="FileSession.TemporaryFilePath"/> property in case the property value is null.
    /// </summary>
    /// <param name="fileSession">The <see cref="FileSession"/> instance.</param>
    /// <returns>The generated or already existing path for temporary files for the session.</returns>
    public static string? SetRandomPath(this FileSession fileSession)
    {
        if (fileSession.TemporaryFilePath == null && fileSession.UseFileSystemOnContents)
        {
            var path = Path.Combine(ApplicationDataDirectory, Path.GetRandomFileName());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            fileSession.TemporaryFilePath = path;

            return path;
        }

        if (!fileSession.UseFileSystemOnContents)
        {
            fileSession.TemporaryFilePath = null;
        }

        return fileSession.TemporaryFilePath;
    }

    /// <summary>
    /// Gets or sets the application data directory for caching files in case the <see cref="FileSession.UseFileSystemOnContents"/> property is set to true.
    /// </summary>
    public static string ApplicationDataDirectory { get; set; } = string.Empty;
}