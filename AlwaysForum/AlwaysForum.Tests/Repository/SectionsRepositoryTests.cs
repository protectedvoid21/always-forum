using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.Sections;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Tests.Repository;

public class SectionsRepositoryTests {
    private readonly SectionsRepository _repository;
    private readonly ForumDbContext _dbContext;

    public SectionsRepositoryTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _dbContext = new ForumDbContext(options);

        _repository = new SectionsRepository(_dbContext);
    }

    [Theory]
    [InlineData("SampleSection")]
    public async Task Add_NewSection_ShouldBeAddedInDatabase(string name) {
        await _repository.AddAsync(name, "Description");

        Assert.Single(_dbContext.Sections);
    }

    [Fact]
    public async Task Get_AllSections_ReturnsAllSectionsList() {
        await _dbContext.Sections.AddRangeAsync(
            new Section { Name = "Section1", Description = "Desc1" },
            new Section { Name = "Section2", Description = "Desc2" },
            new Section { Name = "Section3", Description = "Desc3" });

        await _dbContext.SaveChangesAsync();
        var sectionList = await _repository.GetAllAsync();

        Assert.Equal(3, sectionList.Count());
    }

    [Fact]
    public async Task Get_OneSection_GetsCertainSection() {
        await _repository.AddAsync("SampleSection", "Description");

        var sectionId = (await _dbContext.Sections.FirstAsync()).Id;
        var section = await _repository.GetAsync(sectionId);

        Assert.Equal("SampleSection", section.Name);
        Assert.Equal("Description", section.Description);
    }

    [Theory]
    [InlineData("FirstName", "UpdatedName")]
    public async Task Update_CertainSection_DataIsChangedForSection(string previousName, string updateName) {
        await _repository.AddAsync(previousName, "Description");
        var section = await _dbContext.Sections.FirstAsync();

        await _repository.UpdateAsync(section.Id, updateName, "Description");

        Assert.Equal(updateName, section.Name);
    }

    [Fact]
    public async Task Delete_Section_DatabaseDeletesSection() {
        await _repository.AddAsync("SectionName", "Description");
        Assert.Single(_dbContext.Sections);

        var section = await _dbContext.Sections.FirstAsync();
        await _repository.DeleteAsync(section.Id);

        Assert.Empty(_dbContext.Sections);
    }
}