var builder = DistributedApplication.CreateBuilder(args);

// AppHost'un çalıştığı ortamı al (Development / Staging / Production)
var env = builder.Environment.EnvironmentName;

var api = builder.AddProject<Projects.WebAPI>("webapi")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", env);

// Angular CLI Vite tabanlı değil — ng serve (npm start) ile ayağa kalkar
builder.AddNodeApp("angular-admin", "../clients/angular-admin", "server.js")
    .WithNpm()
    .WithRunScript("start")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(port: 4200, env: "PORT")
    .WithExternalHttpEndpoints();

// Vue + Vite
builder.AddViteApp("vue-portal", "../clients/vue-portal")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(port: 5173, env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
