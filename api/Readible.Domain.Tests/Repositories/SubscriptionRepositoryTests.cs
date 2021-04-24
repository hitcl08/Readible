using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Readible.Domain.Tests.Repositories
{
    public class SubscriptionRepositoryTests
    {
        private readonly SubscriptionRepository _subscriptionRepository;

        public SubscriptionRepositoryTests()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ReadibleContext>();
            dbContextOptionsBuilder.UseSqlServer("Server=tcp:readible.database.windows.net,1433;Initial Catalog=readible-db;Persist Security Info=False;User ID=liamhitchcock;Password=4fBuJ976YAUdwRXQ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .EnableSensitiveDataLogging();


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SubscriptionViewModel, Subscription>();
            });

            var mapper = config.CreateMapper();


            var context = new ReadibleContext(dbContextOptionsBuilder.Options);
            _subscriptionRepository = new SubscriptionRepository(context, mapper);
        }

        [Fact(Skip ="no need to add more subscriptions")]
        public async Task AddUserSubscription_ShouldReturnTrue_ForValidUserId()
        {
            // arrange
            var userId = 1;

            // act
            var isAdded = await _subscriptionRepository.Add(userId);

            // assert
            Assert.True(isAdded);
        }

        [Fact(Skip = "no need to delete subscriptions")]
        public async Task DeleteUserSubscription_ShouldReturnTrue_ForValidUserId()
        {
            // arrange
            var userId = 1;

            // act
            var isAdded = await _subscriptionRepository.DeleteByUserId(userId);

            // assert
            Assert.True(isAdded);
        }

        [Fact]
        public async Task DeleteUserSubscription_ShouldReturnFalse_ForInvalidUserId()
        {
            // arrange
            var userId = -1;

            // act
            var isAdded = await _subscriptionRepository.DeleteByUserId(userId);

            // assert
            Assert.False(isAdded);
        }

        [Fact]
        public void GetSubscription_ShouldReturnUserSubscription_ForValidUserId()
        {
            // arrange
            var userId = 1;

            // act
            var subscription = _subscriptionRepository.GetSubscriptionBySubscriptionId(userId);

            // assert
            Assert.NotNull(subscription);
        }

        [Fact]
        public void GetSubscription_ShouldReturnUserSubscription_ForInvalidUserId()
        {
            // arrange
            var userId = -1;

            // act
            var subscription = _subscriptionRepository.GetSubscriptionBySubscriptionId(userId);

            // assert
            Assert.Null(subscription);
        }

        [Fact]
        public async Task GetSubscriptions_ShouldReturnListOfSubscription_ForValidRequest()
        {
            // arrange

            // act
            var subscriptions = await _subscriptionRepository.GetSubscriptions();

            // assert
            Assert.NotEmpty(subscriptions);
        }
    }
}
