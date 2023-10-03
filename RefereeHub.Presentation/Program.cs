using Mapster;
using Microsoft.AspNetCore.Identity;
using RefereeHub.Application.Hubs;
using RefereeHub.Application.Services.Referee;
using RefereeHub.Domain.Helpers;
using RefereeHub.Domain.Referee.Interfaces;
using RefereeHub.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Mapster global config
TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    CreateIdentityUsersAndRoles.Create(userManager, roleManager).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ReportHub>("/reportHub");
app.MapHub<RefereeHub.Application.Hubs.RefereeHub>("/refereeHub");

app.Run();