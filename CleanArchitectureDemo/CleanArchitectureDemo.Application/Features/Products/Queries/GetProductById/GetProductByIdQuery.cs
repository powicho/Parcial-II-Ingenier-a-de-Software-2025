using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Queries/GetProductById/GetProductByIdQuery.cs
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Queries.GetProductById;

// Comando que lleva el ID del producto que queremos buscar.
// Devuelve un ProductDto o null si no se encuentra.
public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
