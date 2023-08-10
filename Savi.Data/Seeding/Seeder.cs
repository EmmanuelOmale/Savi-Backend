﻿using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.Enums;

namespace Savi.Data.Seeding
{
    public static class Seeder
	{
		public static void SeedData(SaviDbContext dbContext)
		{
			if (dbContext.Occupations.Any() || dbContext.IdentityTypes.Any())
			{
				return; // Data already seeded
			}

			var occupations = new List<Occupation>
			{
				new Occupation { Name = "Doctor", Description = "Saving life is my hobby" },
				new Occupation { Name = "Youth Corper", Description = "Currently serving my fatherland" },
				new Occupation { Name = "Teacher", Description = "Government School Teacher" },
				new Occupation { Name = "Business Owner", Description = "I own my own business" }
			};

			dbContext.Occupations.AddRange(occupations);

			var identityTypes = new List<IdentityType>
			{
				new IdentityType { Name = "Driver's License", IdentificationNumber = "DL-001", DocumentImageUrl = "/images/driver_license.jpg" },
				new IdentityType { Name = "Passport", IdentificationNumber = "PPT-002", DocumentImageUrl = "/images/passport.jpg" }
			};

			dbContext.IdentityTypes.AddRange(identityTypes);
			var frequency = new List<SavingsFrequency>
			{
				new SavingsFrequency{Id = 1,FrequencyName = "Daily",CreatedAt = DateTime.Now},
				new SavingsFrequency{Id = 2,FrequencyName = "Weekly",CreatedAt = DateTime.Now},
				new SavingsFrequency{Id = 3,FrequencyName = "Monthly",CreatedAt = DateTime.Now},
			};
			dbContext.SavingFrequencys.AddRange(frequency);

			dbContext.SaveChanges();
		}
	}
}