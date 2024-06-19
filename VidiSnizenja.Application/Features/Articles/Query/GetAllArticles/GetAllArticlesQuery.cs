using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Articles.Query.GetAllArticles;

public sealed record GetAllArticlesQuery(string? Name, decimal? MinPrice, decimal? MaxPrice, string? PropertyType, int Bedrooms, int Bathroom, List<string> Amenities) : IRequest<Result<IEnumerable<GetAllArticlesQueryResponse>>>;