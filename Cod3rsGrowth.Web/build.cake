#addin nuget:?package=Cake.Docker

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var solution = "../Cod3rsGrowth.sln";

Task("Restore")
    .Does(() => {
        DotNetRestore(solution);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetBuild(solution, new DotNetBuildSettings()
        {
            NoRestore = true,
            Configuration = configuration
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetTest(solution, new DotNetTestSettings {
            NoRestore = true,
            Configuration = configuration,
            NoBuild = true
        });
    });

Task("Docker-Build")
    .Does(() => {
    var settings = new DockerImageBuildSettings { Tag = new[] {"cod3rsGrowth:latest" }};
    DockerBuild(settings, "..");
});

Task("Default")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Docker-Build")
});

Default

RunTarget(target);
