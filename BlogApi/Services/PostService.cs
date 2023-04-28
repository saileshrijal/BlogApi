using System.Transactions;
using BlogApi.Dtos;
using BlogApi.Models;
using BlogApi.Services.Interface;
using Repository.Interface;

namespace BlogApi.Services;
public class PostService : IPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostRepository _postRepository;
    private readonly IPostCategoryRepository _postCategoryRepository;

    public PostService(IUnitOfWork unitOfWork, IPostRepository postRepository, IPostCategoryRepository postCategoryRepository)
    {
        _unitOfWork = unitOfWork;
        _postRepository = postRepository;
        _postCategoryRepository = postCategoryRepository;
    }

    public async Task CreateAsync(PostDto postDto, List<int> categoryIds)
    {
        var post = new Post
        {
            Title = postDto.Title,
            Description = postDto.Description,
            ShortDescription = postDto.ShortDescription,
            CreatedDate = DateTime.Now,
            Slug = GenerateSlug(postDto.Title!),
            ApplicationUserId = postDto.ApplicationUserId!,
            IsPublished = postDto.IsPublished,
            PostCategories = categoryIds.Select(x => new PostCategory
            {
                CategoryId = x
            }).ToList(),
            ThumbnailUrl = postDto.ThumbnailUrl
        };
        await _unitOfWork.CreateAsync(post);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var post = await _postRepository.Get(x => x.Id == id);
        var postCategories = await _postCategoryRepository.Find(x => x.PostId == id);
        await _unitOfWork.DeleteRangeAsync(postCategories);
        await _unitOfWork.DeleteAsync(post);
        await _unitOfWork.SaveAsync();
        tx.Complete();
    }

    public async Task UpdateAsync(int id, PostDto postDto, List<int> categoryIds)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var post = await _postRepository.GetById(id);
        post.Title = postDto.Title;
        post.Description = postDto.Description;
        post.ShortDescription = postDto.ShortDescription;
        post.IsPublished = postDto.IsPublished;
        if(post.Title != null) { post.Slug  = GenerateSlug(post.Title); }
        if(postDto.ThumbnailUrl != null)
        {
            post.ThumbnailUrl = postDto.ThumbnailUrl;
        }
        
        var postCategories = await _postCategoryRepository.GetPostCategoriesByPostId(id);

        var CategoryIdsToRemove = postCategories.Select(x => x.CategoryId).Except(categoryIds).ToList();

        var CategoryIdsToAdd = categoryIds.Except(postCategories.Select(x => x.CategoryId)).ToList();


        if (CategoryIdsToRemove.Any())
        {
            var postCategoriesToRemove = postCategories.Where(x => CategoryIdsToRemove.Contains(x.CategoryId)).ToList();
            await _unitOfWork.DeleteRangeAsync(postCategoriesToRemove);
        }

        if (CategoryIdsToAdd.Any())
        {
            post.PostCategories = CategoryIdsToAdd.Select(x => new PostCategory
            {
                CategoryId = x,
                PostId = id
            }).ToList();
        }
        await _unitOfWork.UpdateAsync(post);
        await _unitOfWork.SaveAsync();
        tx.Complete();
    }

    private static string GenerateSlug(string name)
    {
        var slug = name.Trim().ToLower().Replace(" ", "-") + "-" + Guid.NewGuid().ToString().ToString()[..5];
        return slug;
    }
}


