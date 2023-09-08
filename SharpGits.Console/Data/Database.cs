namespace SharpGits.Console.Data;

public class Database
{
    private const string GitSubDirName = ".git";
    private const string RefsSubDirName = "refs";
    private const string ObjectsSubDirName = "objects";

    public static void Init(string workspacePath)
    {
        string gitPath = Path.Combine(workspacePath, GitSubDirName);
        CreateGitDirectories(gitPath);
        CreateHeadFile(gitPath);
    }

    private static void CreateGitDirectories(string gitPath)
    {        
        Directory.CreateDirectory(gitPath);
        Directory.CreateDirectory(Path.Combine(gitPath, RefsSubDirName));
        Directory.CreateDirectory(Path.Combine(gitPath, ObjectsSubDirName));
    }

    private static void CreateHeadFile(string gitPath) =>
        File.WriteAllText(path: Path.Combine(gitPath, "HEAD"), contents: @"ref: refs/heads/main" + Environment.NewLine);
    
}