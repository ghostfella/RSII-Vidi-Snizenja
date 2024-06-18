namespace VidiSnizenja_API.Features.Articles;

public static class ArticleEndpoint
{
    private const string article = "article";
    private const string article_path = $"api/{article}";
    private const string article_tag_name = "Article endpoints";

    public static IEndpointRouteBuilder MapArticleEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(article_path)
            .AllowAnonymous()
            .WithTags(article_tag_name)
            .WithOpenApi();

        return builder;
    }
}
