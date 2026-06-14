var builder = DistributedApplication.CreateBuilder(args);

// AppHost'un çalıştığı ortamı al (Development / Staging / Production)
var env = builder.Environment.EnvironmentName;

var api = builder.AddProject<Projects.WebAPI>("webapi")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", env);

// Angular CLI — ng serve --port %PORT% (package.json start script reads PORT env)
builder.AddNodeApp("angular-admin", "../clients/angular-admin", "node_modules/.bin/ng")
    .WithNpm()
    .WithRunScript("start")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();

// Vue + Vite — PORT env'i otomatik okur
builder.AddViteApp("vue-portal", "../clients/vue-portal")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
