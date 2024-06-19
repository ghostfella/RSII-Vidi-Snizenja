namespace VidiSnizenja.Application.Features.Articles.Query.GetAllArticles;

public sealed record GetAllArticlesQueryResponse(
    string? Name, 
    decimal? MinPrice, 
    decimal? MaxPrice, 
    string? ArticleType,
    DateTime? CreatedAt);