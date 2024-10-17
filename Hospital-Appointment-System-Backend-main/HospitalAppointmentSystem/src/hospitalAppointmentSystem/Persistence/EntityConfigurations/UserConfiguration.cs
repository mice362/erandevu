﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Hashing;
using System.Reflection.Emit;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u=>u.NationalIdentity).IsUnique();

        builder.Property(u => u.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(u => u.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(x => x.DateOfBirth).HasColumnName("DateOfBirth");
        builder.Property(x => x.NationalIdentity).HasColumnName("NationalIdentity").HasMaxLength(11);
        builder.Property(u => u.Phone).HasColumnName("Phone").IsRequired();
        builder.Property(u => u.Address).HasColumnName("Address");
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType").IsRequired();
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        builder .HasIndex(u => u.Email).IsUnique();
        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasMany(u => u.UserOperationClaims);
        builder.HasMany(u => u.RefreshTokens);
        builder.HasMany(u => u.EmailAuthenticators);
        builder.HasMany(u => u.OtpAuthenticators);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);


   
    }

    public static Guid AdminId { get; } = Guid.NewGuid();
    private IEnumerable<User> _seeds
    {
        get
        {
            HashingHelper.CreatePasswordHash(
                password: "Password1!",
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User adminUser =
                new()
                {
                    Id = AdminId,
                    FirstName = "Fatma",
                    LastName = "Birel",
                    DateOfBirth = new DateOnly(2000, 11, 20),
                    NationalIdentity = "12345678901",
                    Phone = "05279563492",
                    Address = "Tekirdağ",
                    Email = "fatmabireltr@gmail.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

            yield return adminUser;
        }
    }
}
