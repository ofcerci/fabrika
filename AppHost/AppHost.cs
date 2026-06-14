var builder = DistributedApplication.CreateBuilder(args);

// AppHost'un çalıştığı ortamı al (Development / Staging / Production)
var env = builder.Environment.EnvironmentName;

var api = builder.AddProject<Projects.WebAPI>("webapi")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", env);

builder.AddNpmApp("angular-admin", "../clients/angular-admin", "dev")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(port: 4200, env: "PORT")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("vue-portal", "../clients/vue-portal", "dev")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(port: 5173, env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
