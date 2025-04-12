using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Queries/GetAllProducts/GetAllProductsQuery.cs
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Queries.GetAllProducts;

// Un comando simple que no necesita parámetros para obtener todos los productos.
// Devuelve una lista (IEnumerable) de ProductDto.
public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;