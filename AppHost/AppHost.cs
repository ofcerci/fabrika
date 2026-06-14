var builder = DistributedApplication.CreateBuilder(args);

// AppHost'un çalıştığı ortamı al (Development / Staging / Production)
var env = builder.Environment.EnvironmentName;

var api = builder.AddProject<Projects.WebAPI>("webapi")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", env);

builder.AddViteApp("angular-admin", "../clients/angular-admin")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(port: 4200, env: "PORT")
    .WithExternalHttpEndpoints();

builder.AddViteApp("vue-portal", "../clients/vue-portal")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(port: 5173, env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
