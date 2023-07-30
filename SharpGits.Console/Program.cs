using CommandLine;
using SharpGits.Console.Data;
using SharpGits.Console.Verbs;

Parser parser = new(s => { s.AutoVersion = false; });

return parser
    .ParseArguments<InitOptions, CommitOptions>(args)
    .MapResult(
        (InitOptions opts) => RunInit(opts),
        (CommitOptions opts) => RunCommit(opts),
        errs => NotParsed(errs, args));

static int RunInit(InitOptions options)
{
    // TODO allow dir to be passed in
    var dirToInit = Directory.GetCurrentDirectory();
    Database.Init(dirToInit);
    return 0;
}

static int RunCommit(CommitOptions options)
{
    // TODO revisit this later. It only currently only exists so we have two CLI commands and therefore uses the correct MapResult() overload
    return 1;
}

static int NotParsed(IEnumerable<Error> errors, string[] args)
{
    Console.WriteLine("Invalid option selected");
    return 1;
}