﻿using Moq;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories.Users;

namespace CommonTestUtilities.Repositories
{
    public class UserUpdateOnlyRepositoryBuilder
    {
        public static IUserUpdateOnlyRepository Build(User user)
        {
            var mock = new Mock<IUserUpdateOnlyRepository>();

            mock.Setup(repository => repository.GetById(user.Id)).ReturnsAsync(user);

            return mock.Object;
        }
    }
}
