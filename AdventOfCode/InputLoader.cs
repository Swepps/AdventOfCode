using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode
{
    public static class InputLoader
    {
        private static string? cachedRoot;

        public static string Load(int year, int day)
        {
            var root = cachedRoot ??= FindProjectRoot();

            var path = Path.Combine(root, "Inputs", year.ToString(), $"Day{day}.txt");

            if (!File.Exists(path))
                throw new FileNotFoundException($"Input not found: {path}");

            return File.ReadAllText(path);
        }

        private static string FindProjectRoot()
        {
            var startDir =
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new Exception("Failed to determine assembly directory.");

            var current = new DirectoryInfo(startDir);

            // First: walk upward until we find the .git directory
            DirectoryInfo? repoRoot = null;

            while (current != null)
            {
                if (Directory.Exists(Path.Combine(current.FullName, ".git")))
                {
                    repoRoot = current;
                    break;
                }

                current = current.Parent;
            }

            if (repoRoot == null)
                throw new DirectoryNotFoundException(
                    "Failed to locate repository root (.git folder)."
                );

            // Second: search *downward* from repo root for the folder containing Inputs/
            // while preventing escape outside the repo.
            var projectRoot = FindInputsFolderParent(repoRoot);
            return projectRoot.FullName;
        }

        private static DirectoryInfo FindInputsFolderParent(DirectoryInfo repoRoot)
        {
            // Search all subfolders for an 'Inputs' directory
            foreach (var dir in repoRoot.GetDirectories("*", SearchOption.AllDirectories))
            {
                if (dir.Name.Equals("Inputs", StringComparison.OrdinalIgnoreCase))
                    return dir.Parent ?? repoRoot;
            }

            throw new DirectoryNotFoundException(
                "Could not locate the 'Inputs' directory inside the repository."
            );
        }
    }
}
