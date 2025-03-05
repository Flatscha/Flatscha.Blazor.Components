using AngleSharp.Dom;
using Flatscha.Blazor.Components.Contracts.Interfaces.Services;
using Flatscha.Blazor.Components.General;
using Flatscha.Blazor.Components.Search;
using Flatscha.Blazor.Components.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Flatscha.Blazor.Components.Tests.Search
{
    public class SearchTest : BaseComponentTest
    {
        [Fact]
        public void Search()
        {
            var items = new List<int> { 1, 2, 3, 4, 5, 33, 55 };

            var cut = RenderComponent<SearchComponent<int>>(parameters =>
            {
                parameters.Add(p => p.Items, items);
                parameters.Add(p => p.SearchExpression, (items, searchValue) => items.Where(x => x.ToString().Contains(searchValue)));
                parameters.Add(p => p.ItemTemplate, item => item.ToString());
            });

            var searchItems = cut.FindAll(".search-item");
            Assert.Equal(items.Count, searchItems.Count);

            var searchInput = cut.Find("input[type=\"text\"]");

            searchInput.Input(3);

            searchItems = cut.FindAll(".search-item");
            Assert.Equal(2, searchItems.Count);
        }

        [Fact]
        public void Select()
        {
            var items = new List<int> { 1, 2, 3, 4, 5, 33, 55 };
            int? res = null;

            var cut = RenderComponent<SearchComponent<int>>(parameters =>
            {
                parameters.Add(p => p.Items, items);
                parameters.Add(p => p.ItemTemplate, item => item.ToString());
                parameters.Add(p => p.OnSelected, item => res = item);
            });

            var searchItem = cut.FindAll(".search-item")[2];
            Assert.NotNull(searchItem);

            Assert.Null(res);
            searchItem.Click();
            Assert.Equal(3, res);
        }

        [Fact]
        public void Create()
        {
            var mockFavoriteHandler = new Mock<IFavoriteHandler>();
            Services.AddSingleton(mockFavoriteHandler.Object);

            var items = new List<int> { 1, 2, 3, 4, 5, 33, 55 };

            var cut = RenderComponent<SearchModalComponent<int, int>>(parameters =>
            {
                parameters.Add(p => p.Items, items);
                parameters.Add(p => p.FavoriteKey, "Test");
                parameters.Add(p => p.FavoriteObject, x => x);
                parameters.Add(p => p.Name, x => x.ToString());
                parameters.Add(p => p.DisableOrder, true);
            });

            var searchItems = cut.FindAll(".search-item");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var searchItem = searchItems[i];

                Assert.NotNull(searchItem);

                var name = searchItem.GetElementsByClassName("search-item-name").SingleOrDefault();

                Assert.Equal(item.ToString(), name?.InnerHtml);

                var fav = searchItem.GetElementsByClassName("flatscha-icon").SingleOrDefault();

                Assert.NotNull(fav);
            }
        }

        [Fact]
        public void Favorite()
        {
            var mockFavoriteHandler = new Mock<IFavoriteHandler>();
            Services.AddSingleton(mockFavoriteHandler.Object);

            var items = new List<int> { 1, 2, 3, 4, 5, 33, 55 };

            var cut = RenderComponent<SearchModalComponent<int, int>>(parameters =>
            {
                parameters.Add(p => p.Items, items);
                parameters.Add(p => p.FavoriteKey, "Test");
                parameters.Add(p => p.FavoriteObject, x => x);
                parameters.Add(p => p.Name, x => x.ToString());
                parameters.Add(p => p.DisableOrder, true);
            });

            var favIcon = cut.FindAll(".flatscha-icon")[3];

            favIcon.Click();
            mockFavoriteHandler.Verify(x => x.ToggleFavorite("Test", 4, It.IsAny<Func<int, int>>()), Times.Once);

            favIcon = cut.FindAll(".flatscha-icon")[3];
            favIcon.Click();
            mockFavoriteHandler.Verify(x => x.ToggleFavorite("Test", 4, It.IsAny<Func<int, int>>()), Times.Exactly(2));
        }
    }
}
