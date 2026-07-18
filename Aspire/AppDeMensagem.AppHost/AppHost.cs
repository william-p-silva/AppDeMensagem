var builder = DistributedApplication.CreateBuilder(args);

var backEnd = builder.AddProject<Projects.AppDeMensagem_WebApi>("back-end");

builder.AddProject<Projects.AppDeMensagem_Web>("fornt-end")
    .WithReference(backEnd);

builder.Build().Run();
