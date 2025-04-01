using System.ComponentModel.DataAnnotations;
using Renting.Application.Interfaces.InputPorts;
using Renting.Application.Interfaces.OutputPorts;
using Renting.Application.UseCases.CreateVehicle;
using Renting.Application.UseCases.RentVehicle;
using Renting.Application.UseCases.ReturnVehicle;
using Renting.Domain.Interfaces;
using Renting.WebApi.Presenters;
using Renting.Infrastructure.Persistence.Repositories;
using Renting.Infrastructure.UnitOfWork;
using Renting.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

// ...

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios del caso de uso
builder.Services.AddScoped<ICreateVehicleUseCase, CreateVehicleUseCase>();
builder.Services.AddScoped<ICreateVehicleOutputPort, CreateVehiclePresenter>();
builder.Services.AddScoped<CreateVehiclePresenter>();
builder.Services.AddScoped<IRentVehicleUseCase, RentVehicleUseCase>();
builder.Services.AddScoped<IRentVehicleOutputPort, RentVehiclePresenter>();
builder.Services.AddScoped<RentVehiclePresenter>();
builder.Services.AddScoped<IReturnVehicleUseCase, ReturnVehicleUseCase>();
builder.Services.AddScoped<IReturnVehicleOutputPort, ReturnVehiclePresenter>();
builder.Services.AddScoped<ReturnVehiclePresenter>();

// Infraestructura (temporal: mocks si no tienes aún la implementación real)
builder.Services.AddScoped<IVehicleRepository, InMemoryVehicleRepository>(); // Temporal
builder.Services.AddScoped<IUnitOfWork, FakeUnitOfWork>(); // Temporal
builder.Services.AddScoped<ICustomerRepository, InMemoryCustomerRepository>();
builder.Services.AddScoped<IRentalRepository, InMemoryRentalRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run("http://0.0.0.0:5000");

public partial class Program { }