using Cint.CodingChallenge.Model.DBSet;
using Microsoft.Extensions.DependencyInjection;

namespace Cint.CodingChallenge.Data
{
    public class DatabaseInitialiser
    {
        public static async Task InitialiseAsync(IServiceProvider services)
        {
            await AddData(services.GetRequiredService<DatabaseContext>());
        }

        private static async Task AddData(DatabaseContext context)
        {
            await context.Surveys.AddRangeAsync(new[] {
                new Survey {
                    Id = Guid.NewGuid(),
                    Name = "Fruit Survey",
                    Description = "Survey about Fruit",
                    LengthMinutes = 7,
                    IncentiveEuros = 2.34  // 0.31
                },
                new Survey {
                    Id = Guid.NewGuid(),
                    Name = "Pet Survey",
                    Description = "Survey about Pets",
                    LengthMinutes = 9,
                    IncentiveEuros = 3.78  // 0.42
                },
                new Survey {
                    Id = Guid.NewGuid(),
                    Name = "Hobby Survey",
                    Description = "Survey about Hobbies",
                    LengthMinutes = 4,
                    IncentiveEuros = 4.51  // 1.12
                },
                new Survey {
                    Id = Guid.NewGuid(),
                    Name = "Travel Survey",
                    Description = "Survey about Travel Destinations",
                    LengthMinutes = 15,
                    IncentiveEuros = 2.09  // 0.13
                },
                new Survey {
                    Id = Guid.NewGuid(),
                    Name = "Food Survey",
                    Description = "Survey about Favorite Foods",
                    LengthMinutes = 2,
                    IncentiveEuros = 1.12  // 0.56
                }
            });

            await context.SaveChangesAsync();
        }
    }
}
